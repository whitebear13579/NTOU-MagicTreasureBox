using Newtonsoft.Json;
using sharelock_desktop.Utils;
using System.Security.Cryptography;
using System.Text;

namespace sharelock_desktop.Services;

public class FirestoreService
{
    private readonly HttpClient _httpClient;
    private const string FirestoreBaseUrl = "https://firestore.googleapis.com/v1";
    private const string ProjectId = "sharelock-bbf0f";
    private const string DatabaseId = "userdata";
    
    public FirestoreService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public static string GenerateDocumentId()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[20];
        rng.GetBytes(bytes);
        var result = new StringBuilder(20);
        foreach (var b in bytes)
        {
            result.Append(chars[b % chars.Length]);
        }
        return result.ToString();
    }

    public async Task<FirestoreResult> CreateFileRecordAsync(
        string fileId,
        string ownerUid,
        string originalName,
        string displayName,
        long size,
        string contentType,
        string storagePath,
        string downloadUrl,
        DateTime expiresAt,
        int maxDownloads,
        string shareMode,
        string? pinHash = null)
    {
        var idToken = SessionManager.Instance.IdToken;
        if (string.IsNullOrEmpty(idToken))
        {
            return new FirestoreResult { Success = false, Error = "未登入" };
        }

        try
        {
            var url = $"{FirestoreBaseUrl}/projects/{ProjectId}/databases/{DatabaseId}/documents/files?documentId={fileId}";

            var fields = new Dictionary<string, object>
            {
                ["ownerUid"] = new { stringValue = ownerUid },
                ["originalName"] = new { stringValue = originalName },
                ["displayName"] = new { stringValue = displayName },
                ["size"] = new { integerValue = size.ToString() },
                ["contentType"] = new { stringValue = contentType },
                ["storagePath"] = new { stringValue = storagePath },
                ["downloadURL"] = new { stringValue = downloadUrl },
                ["createdAt"] = new { timestampValue = DateTime.UtcNow.ToString("o") },
                ["expiresAt"] = new { timestampValue = expiresAt.ToUniversalTime().ToString("o") },
                ["maxDownloads"] = new { integerValue = maxDownloads.ToString() },
                ["remainingDownloads"] = new { integerValue = maxDownloads.ToString() },
                ["shareMode"] = new { stringValue = shareMode },
                ["revoked"] = new { booleanValue = false },
                ["allowedDevices"] = new { arrayValue = new { values = Array.Empty<object>() } }
            };

            if (!string.IsNullOrEmpty(pinHash))
            {
                fields["pinHash"] = new { stringValue = pinHash };
            }

            var document = new { fields };
            var jsonContent = JsonConvert.SerializeObject(document);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {idToken}");

            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateFile URL: {url}");
            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateFile body: {jsonContent}");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateFile response: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[Firestore] Response body: {responseBody}");

            if (response.IsSuccessStatusCode)
            {
                return new FirestoreResult { Success = true, DocumentId = fileId };
            }

            return new FirestoreResult { Success = false, Error = $"建立檔案記錄失敗: {response.StatusCode} - {responseBody}" };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateFile error: {ex.Message}");
            return new FirestoreResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<FirestoreResult> CreateShareRecordAsync(
        string shareId,
        string fileId,
        string ownerUid,
        string shareMode,
        string? pinHash = null)
    {
        var idToken = SessionManager.Instance.IdToken;
        if (string.IsNullOrEmpty(idToken))
        {
            return new FirestoreResult { Success = false, Error = "未登入" };
        }

        try
        {
            var url = $"{FirestoreBaseUrl}/projects/{ProjectId}/databases/{DatabaseId}/documents/shares?documentId={shareId}";

            var fields = new Dictionary<string, object>
            {
                ["fileId"] = new { stringValue = fileId },
                ["ownerUid"] = new { stringValue = ownerUid },
                ["createdAt"] = new { timestampValue = DateTime.UtcNow.ToString("o") },
                ["valid"] = new { booleanValue = true },
                ["shareMode"] = new { stringValue = shareMode }
            };

            if (!string.IsNullOrEmpty(pinHash))
            {
                fields["pinHash"] = new { stringValue = pinHash };
            }

            var document = new { fields };
            var jsonContent = JsonConvert.SerializeObject(document);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {idToken}");

            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateShare URL: {url}");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateShare response: {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[Firestore] Response body: {responseBody}");

            if (response.IsSuccessStatusCode)
            {
                return new FirestoreResult { Success = true, DocumentId = shareId };
            }

            return new FirestoreResult { Success = false, Error = $"建立分享記錄失敗: {response.StatusCode} - {responseBody}" };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Firestore] CreateShare error: {ex.Message}");
            return new FirestoreResult { Success = false, Error = ex.Message };
        }
    }

    public async Task<FirestoreResult> IncrementUserFilesSharedAsync(string userId)
    {
        var idToken = SessionManager.Instance.IdToken;
        if (string.IsNullOrEmpty(idToken))
        {
            return new FirestoreResult { Success = false, Error = "未登入" };
        }

        try
        {
            var getUrl = $"{FirestoreBaseUrl}/projects/{ProjectId}/databases/{DatabaseId}/documents/users/{userId}";
            
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {idToken}");
            
            var getResponse = await _httpClient.GetAsync(getUrl);
            int currentCount = 0;

            if (getResponse.IsSuccessStatusCode)
            {
                var getBody = await getResponse.Content.ReadAsStringAsync();
                if (getBody.Contains("totalFilesShared"))
                {
                    var doc = JsonConvert.DeserializeObject<dynamic>(getBody);
                    var totalFilesSharedField = doc?.fields?.totalFilesShared;
                    if (totalFilesSharedField != null)
                    {
                        string? intValue = totalFilesSharedField?.integerValue?.ToString();
                        if (!string.IsNullOrEmpty(intValue))
                        {
                            int.TryParse(intValue, out currentCount);
                        }
                    }
                }
            }

            var url = $"{FirestoreBaseUrl}/projects/{ProjectId}/databases/{DatabaseId}/documents/users/{userId}?updateMask.fieldPaths=totalFilesShared";

            var document = new
            {
                fields = new Dictionary<string, object>
                {
                    ["totalFilesShared"] = new { integerValue = (currentCount + 1).ToString() }
                }
            };

            var jsonContent = JsonConvert.SerializeObject(document);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = content
            };
            request.Headers.Add("Authorization", $"Bearer {idToken}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return new FirestoreResult { Success = true };
            }

            return new FirestoreResult { Success = false, Error = "更新用戶統計失敗" };
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Firestore] IncrementUserFilesShared error: {ex.Message}");
            return new FirestoreResult { Success = false, Error = ex.Message };
        }
    }

    public static string GetDownloadUrl(string storagePath, string downloadToken)
    {
        var encodedPath = Uri.EscapeDataString(storagePath);
        return $"https://firebasestorage.googleapis.com/v0/b/sharelock-bbf0f.firebasestorage.app/o/{encodedPath}?alt=media&token={downloadToken}";
    }
}

public class FirestoreResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public string? DocumentId { get; set; }
}
