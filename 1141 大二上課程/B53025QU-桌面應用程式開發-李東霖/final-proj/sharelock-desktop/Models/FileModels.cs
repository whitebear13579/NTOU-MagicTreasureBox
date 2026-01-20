using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace sharelock_desktop.Models;
public class FlexibleSizeConverter : JsonConverter<long>
{
    public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return 0;

        if (reader.TokenType == JsonToken.Integer)
            return Convert.ToInt64(reader.Value);

        if (reader.TokenType == JsonToken.Float)
            return Convert.ToInt64(reader.Value);

        if (reader.TokenType == JsonToken.String)
        {
            var str = reader.Value?.ToString();
            if (string.IsNullOrEmpty(str))
                return 0;

            if (long.TryParse(str, out var result))
                return result;

            return ParseFormattedSize(str);
        }

        return 0;
    }

    public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    private static long ParseFormattedSize(string sizeStr)
    {
        var parts = sizeStr.Trim().Split(' ');
        if (parts.Length != 2)
            return 0;

        if (!double.TryParse(parts[0], out var value))
            return 0;

        var unit = parts[1].ToUpperInvariant();
        return unit switch
        {
            "B" => (long)value,
            "KB" => (long)(value * 1024),
            "MB" => (long)(value * 1024 * 1024),
            "GB" => (long)(value * 1024 * 1024 * 1024),
            "TB" => (long)(value * 1024 * 1024 * 1024 * 1024),
            _ => 0
        };
    }
}
public class FileInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("displayName")]
    public string? DisplayNameRaw { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    public string DisplayName => DisplayNameRaw ?? Name ?? string.Empty;

    [JsonProperty("originalName")]
    public string? OriginalName { get; set; }

    [JsonProperty("sizeBytes")]
    public long? SizeBytes { get; set; }

    [JsonProperty("size")]
    [JsonConverter(typeof(FlexibleSizeConverter))]
    public long SizeRaw { get; set; }

    public long Size => SizeBytes ?? SizeRaw;

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    [JsonProperty("expiryDate")]
    public string? ExpiryDateStr { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("sharedDate")]
    public string? SharedDate { get; set; }

    [JsonProperty("viewCount")]
    public int? ViewCountRaw { get; set; }

    [JsonProperty("views")]
    public int? Views { get; set; }

    public int ViewCount => ViewCountRaw ?? Views ?? 0;

    [JsonProperty("downloadCount")]
    public int? DownloadCountRaw { get; set; }

    [JsonProperty("downloads")]
    public int? Downloads { get; set; }

    public int DownloadCount => DownloadCountRaw ?? Downloads ?? 0;

    [JsonProperty("sharedWith")]
    public List<string>? SharedWith { get; set; }

    [JsonProperty("shareMode")]
    public string? ShareMode { get; set; }

    [JsonProperty("remainingDownloads")]
    public int? RemainingDownloads { get; set; }

    [JsonProperty("maxDownloads")]
    public int? MaxDownloads { get; set; }

    [JsonProperty("revoked")]
    public bool Revoked { get; set; }

    [JsonProperty("shareId")]
    public string? ShareId { get; set; }

    [JsonProperty("ownerEmail")]
    public string? OwnerEmail { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("isProtected")]
    public bool? IsProtected { get; set; }

    [JsonProperty("revokedDate")]
    public string? RevokedDate { get; set; }

    [JsonProperty("lastAccessedAt")]
    public DateTime? LastAccessedAt { get; set; }

    [JsonProperty("accessType")]
    public string? AccessType { get; set; }

    public string FormattedSize => FormatBytes(Size);

    public bool IsExpired
    {
        get
        {
            if (Status == "expired")
                return true;
            if (ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow)
                return true;
            return false;
        }
    }

    public string StatusText => Revoked ? "已撤銷" : (IsExpired ? "已過期" : "有效");

    private static string FormatBytes(long bytes)
    {
        if (bytes <= 0)
            return "0 B";

        string[] sizes = ["B", "KB", "MB", "GB"];
        int order = 0;
        double len = bytes;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}
public class FileListResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("files")]
    public List<FileInfo>? Files { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class FileDetail
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("originalName")]
    public string? OriginalName { get; set; }

    [JsonProperty("size")]
    [JsonConverter(typeof(FlexibleSizeConverter))]
    public long Size { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("shareMode")]
    public string? ShareMode { get; set; }

    [JsonProperty("maxDownloads")]
    public int MaxDownloads { get; set; }

    [JsonProperty("remainingDownloads")]
    public int RemainingDownloads { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    [JsonProperty("revoked")]
    public bool Revoked { get; set; }

    [JsonProperty("views")]
    public int Views { get; set; }

    [JsonProperty("downloads")]
    public int Downloads { get; set; }

    [JsonProperty("isOwner")]
    public bool IsOwner { get; set; }

    [JsonProperty("ownerEmail")]
    public string? OwnerEmail { get; set; }

    [JsonProperty("shareInfo")]
    public ShareInfo? ShareInfo { get; set; }

    [JsonProperty("recipients")]
    public List<RecipientInfo>? Recipients { get; set; }
}
public class ShareInfo
{
    [JsonProperty("shareId")]
    public string ShareId { get; set; } = string.Empty;

    [JsonProperty("shareUrl")]
    public string ShareUrl { get; set; } = string.Empty;

    [JsonProperty("valid")]
    public bool Valid { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }
}
public class RecipientInfo
{
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("photoURL")]
    public string? PhotoUrl { get; set; }

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }
}
public class FileDetailResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("file")]
    public FileDetail? File { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class DownloadRequest
{
    [JsonProperty("fileId")]
    public string FileId { get; set; } = string.Empty;

    [JsonProperty("shareId")]
    public string? ShareId { get; set; }
}
public class DownloadResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("downloadUrl")]
    public string? DownloadUrl { get; set; }

    [JsonProperty("requiresVerification")]
    public bool RequiresVerification { get; set; }

    [JsonProperty("redirectUrl")]
    public string? RedirectUrl { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class DeleteFileRequest
{
    [JsonProperty("fileId")]
    public string FileId { get; set; } = string.Empty;
}
public class UpdateFileRequest
{
    [JsonProperty("fileId")]
    public string FileId { get; set; } = string.Empty;

    [JsonProperty("updates")]
    public FileUpdates Updates { get; set; } = new();
}
public class FileUpdates
{
    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("maxDownloads")]
    public int? MaxDownloads { get; set; }

    [JsonProperty("revoked")]
    public bool? Revoked { get; set; }
}
