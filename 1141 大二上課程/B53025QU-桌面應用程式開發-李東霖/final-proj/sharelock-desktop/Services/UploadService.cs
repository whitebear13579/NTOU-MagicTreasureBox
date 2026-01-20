using sharelock_desktop.Models;
using sharelock_desktop.Utils;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace sharelock_desktop.Services;

public class UploadService
{
    private readonly ApiClient _apiClient;
    private readonly FirestoreService _firestoreService;
    private const string BucketName = "sharelock-bbf0f.firebasestorage.app";
    private const long MaxFileSize = 300 * 1024 * 1024;

    public UploadService(ApiClient apiClient)
    {
        _apiClient = apiClient;
        _firestoreService = new FirestoreService();
    }

    public async Task<ValidateUploadResponse?> ValidateUploadAsync(long fileSize)
    {
        var request = new ValidateUploadRequest { FileSize = fileSize };
        return await _apiClient.PostAsync<ValidateUploadRequest, ValidateUploadResponse>(
            "/storage/validate-upload", request);
    }

    public async Task<ConfirmUploadResponse?> ConfirmUploadAsync(string validationToken, string storagePath, long actualSize)
    {
        var request = new ConfirmUploadRequest
        {
            ValidationToken = validationToken,
            StoragePath = storagePath,
            ActualSize = actualSize
        };
        return await _apiClient.PostAsync<ConfirmUploadRequest, ConfirmUploadResponse>(
            "/storage/confirm-upload", request);
    }

    public async Task<UploadResult> UploadFileAsync(
        string filePath,
        string userId,
        IProgress<UploadProgressInfo>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var result = new UploadResult();
        var fileInfo = new System.IO.FileInfo(filePath);

        if (!fileInfo.Exists)
        {
            result.Error = "檔案不存在";
            return result;
        }

        if (fileInfo.Length > MaxFileSize)
        {
            result.Error = $"檔案大小超過限制（最大 {MaxFileSize / 1024 / 1024}MB）";
            return result;
        }

        try
        {
            progress?.Report(new UploadProgressInfo { Status = "驗證儲存空間..." });

            var validation = await ValidateUploadAsync(fileInfo.Length);
            if (validation == null)
            {
                result.Error = "無法驗證儲存空間";
                return result;
            }

            if (!validation.Allowed)
            {
                result.Error = validation.Message ?? "儲存空間不足";
                return result;
            }

            var validationToken = validation.ValidationToken!;

            progress?.Report(new UploadProgressInfo { Status = "正在上傳..." });

            var safeFileName = SanitizeFileName(fileInfo.Name);
            var storagePath = $"user_uploads/{userId}/{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{safeFileName}";

            System.Diagnostics.Debug.WriteLine($"[Upload] Starting upload to: {storagePath}");

            var (uploadSuccess, downloadToken) = await UploadToFirebaseStorageAsync(
                filePath, storagePath, fileInfo.Length, progress, cancellationToken);

            if (!uploadSuccess)
            {
                result.Error = "上傳到儲存空間失敗";
                return result;
            }

            progress?.Report(new UploadProgressInfo
            {
                BytesTransferred = fileInfo.Length,
                TotalBytes = fileInfo.Length,
                Status = "確認上傳..."
            });

            var confirmation = await ConfirmUploadAsync(validationToken, storagePath, fileInfo.Length);
            if (confirmation == null || !confirmation.Success)
            {
                result.Error = confirmation?.Error ?? "確認上傳失敗";
                return result;
            }

            result.Success = true;
            result.StoragePath = storagePath;
            result.FileSize = fileInfo.Length;
            result.FileName = fileInfo.Name;
            result.DownloadToken = downloadToken;

            return result;
        }
        catch (OperationCanceledException)
        {
            result.Error = "上傳已取消";
            return result;
        }
        catch (Exception ex)
        {
            result.Error = $"上傳失敗：{ex.Message}";
            return result;
        }
    }

    public async Task<CreateShareResponse?> CreateShareAsync(CreateShareRequest request)
    {
        var userId = SessionManager.Instance.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return new CreateShareResponse { Success = false, Error = "未登入" };
        }

        try
        {
            string? pinHash = null;
            if (request.ShareMode == "pin" && !string.IsNullOrEmpty(request.Pin))
            {
                pinHash = HashPin(request.Pin);
                System.Diagnostics.Debug.WriteLine($"[CreateShare] PIN mode, hash: {pinHash}");
            }

            var fileId = FirestoreService.GenerateDocumentId();
            var downloadUrl = !string.IsNullOrEmpty(request.StoragePath)
                ? GetDownloadUrl(request.StoragePath)
                : "";

            System.Diagnostics.Debug.WriteLine($"[CreateShare] Creating file record: {fileId}");

            var fileResult = await _firestoreService.CreateFileRecordAsync(
                fileId: fileId,
                ownerUid: userId,
                originalName: request.FileName ?? request.DisplayName,
                displayName: request.DisplayName,
                size: request.FileSize,
                contentType: request.ContentType ?? "application/octet-stream",
                storagePath: request.StoragePath ?? "",
                downloadUrl: downloadUrl,
                expiresAt: request.ExpiresAt,
                maxDownloads: request.MaxDownloads,
                shareMode: request.ShareMode,
                pinHash: pinHash
            );

            if (!fileResult.Success)
            {
                return new CreateShareResponse { Success = false, Error = fileResult.Error };
            }

            var shareId = FirestoreService.GenerateDocumentId();
            System.Diagnostics.Debug.WriteLine($"[CreateShare] Creating share record: {shareId}");

            var shareResult = await _firestoreService.CreateShareRecordAsync(
                shareId: shareId,
                fileId: fileId,
                ownerUid: userId,
                shareMode: request.ShareMode,
                pinHash: pinHash
            );

            if (!shareResult.Success)
            {
                return new CreateShareResponse { Success = false, Error = shareResult.Error };
            }

            await _firestoreService.IncrementUserFilesSharedAsync(userId);

            var shareUrl = $"{FirebaseConfig.WebAppUrl}/share/{shareId}";

            System.Diagnostics.Debug.WriteLine($"[CreateShare] Success! ShareId: {shareId}, URL: {shareUrl}");

            return new CreateShareResponse
            {
                Success = true,
                ShareId = shareId,
                ShareUrl = shareUrl,
                FileId = fileId
            };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CreateShare] Error: {ex.Message}");
            return new CreateShareResponse { Success = false, Error = ex.Message };
        }
    }

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = new StringBuilder();
        
        foreach (var c in fileName)
        {
            if (!invalidChars.Contains(c))
            {
                sanitized.Append(c);
            }
            else
            {
                sanitized.Append('_');
            }
        }
        
        return sanitized.ToString();
    }

    private async Task<(bool Success, string? DownloadToken)> UploadToFirebaseStorageAsync(
        string filePath,
        string storagePath,
        long totalBytes,
        IProgress<UploadProgressInfo>? progress,
        CancellationToken cancellationToken)
    {
        try
        {
            var idToken = SessionManager.Instance.IdToken;
            if (string.IsNullOrEmpty(idToken))
            {
                System.Diagnostics.Debug.WriteLine("[Upload] IdToken is null or empty");
                return (false, null);
            }

            var encodedPath = Uri.EscapeDataString(storagePath);
            var contentType = GetContentType(filePath);

            var uploadUrl = $"https://firebasestorage.googleapis.com/v0/b/{BucketName}/o?uploadType=media&name={encodedPath}";

            System.Diagnostics.Debug.WriteLine($"[Upload] Upload URL: {uploadUrl}");
            System.Diagnostics.Debug.WriteLine($"[Upload] File size: {totalBytes}, Content-Type: {contentType}");

            // 使用 HttpClientHandler 禁用緩衝以獲得準確的上傳進度
            using var handler = new HttpClientHandler();
            using var httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromMinutes(30);
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", idToken);

            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 
                bufferSize: 81920, useAsync: true);

            int bufferSize = totalBytes switch
            {
                < 1024 * 1024 => 8192,
                < 10 * 1024 * 1024 => 32768,
                _ => 81920
            };

            var content = new ProgressStreamContent(fileStream, totalBytes, progress, bufferSize);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            using var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl)
            {
                Content = content
            };

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            System.Diagnostics.Debug.WriteLine($"[Upload] Response status: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[Upload] Response body: {responseBody}");

            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"[Upload] Upload failed: {response.StatusCode} - {responseBody}");
                return (false, null);
            }

            string? downloadToken = null;
            try
            {
                var uploadResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);
                downloadToken = uploadResponse?.downloadTokens?.ToString();
            }
            catch
            {
            }

            return (true, downloadToken);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Upload] Exception: {ex.GetType().Name}: {ex.Message}");
            throw;
        }
    }

    private static string GetDownloadUrl(string storagePath)
    {
        var encodedPath = Uri.EscapeDataString(storagePath);
        return $"https://firebasestorage.googleapis.com/v0/b/{BucketName}/o/{encodedPath}?alt=media";
    }

    private static string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".txt" => "text/plain",
            ".html" or ".htm" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            ".7z" => "application/x-7z-compressed",
            ".tar" => "application/x-tar",
            ".gz" => "application/gzip",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".ico" => "image/x-icon",
            ".mp3" => "audio/mpeg",
            ".wav" => "audio/wav",
            ".ogg" => "audio/ogg",
            ".mp4" => "video/mp4",
            ".webm" => "video/webm",
            ".avi" => "video/x-msvideo",
            ".mov" => "video/quicktime",
            ".mkv" => "video/x-matroska",
            _ => "application/octet-stream"
        };
    }

    public static string GeneratePin()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        var number = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
        return number.ToString();
    }

    public static string HashPin(string pin)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}

public class UploadResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public string? StoragePath { get; set; }
    public string? FileName { get; set; }
    public long FileSize { get; set; }
    public string? DownloadToken { get; set; }
}

internal class ProgressStreamContent : HttpContent
{
    private readonly Stream _stream;
    private readonly long _totalBytes;
    private readonly IProgress<UploadProgressInfo>? _progress;
    private readonly int _bufferSize;

    public ProgressStreamContent(Stream stream, long totalBytes, IProgress<UploadProgressInfo>? progress, int bufferSize = 81920)
    {
        _stream = stream;
        _totalBytes = totalBytes;
        _progress = progress;
        _bufferSize = bufferSize;

        Headers.ContentLength = totalBytes;
    }

    protected override async Task SerializeToStreamAsync(Stream stream, System.Net.TransportContext? context)
    {
        var buffer = new byte[_bufferSize];
        long bytesTransferred = 0;
        int bytesRead;
        var lastReportTime = DateTime.UtcNow;

        _progress?.Report(new UploadProgressInfo
        {
            BytesTransferred = 0,
            TotalBytes = _totalBytes,
            Status = "上傳中..."
        });

        while ((bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            await stream.WriteAsync(buffer, 0, bytesRead);
            await stream.FlushAsync();
            bytesTransferred += bytesRead;

            var now = DateTime.UtcNow;
            if ((now - lastReportTime).TotalMilliseconds >= 50 || 
                bytesTransferred == _totalBytes ||
                (bytesTransferred * 100 / _totalBytes) > ((bytesTransferred - bytesRead) * 100 / _totalBytes))
            {
                _progress?.Report(new UploadProgressInfo
                {
                    BytesTransferred = bytesTransferred,
                    TotalBytes = _totalBytes,
                    Status = "上傳中..."
                });
                lastReportTime = now;
            }
        }

        _progress?.Report(new UploadProgressInfo
        {
            BytesTransferred = _totalBytes,
            TotalBytes = _totalBytes,
            Status = "上傳中..."
        });
    }

    protected override bool TryComputeLength(out long length)
    {
        length = _totalBytes;
        return true;
    }
}
