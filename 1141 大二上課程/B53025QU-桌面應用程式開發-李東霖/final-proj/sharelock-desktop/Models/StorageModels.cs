using Newtonsoft.Json;

namespace sharelock_desktop.Models;
public class StorageUsage
{
    [JsonProperty("usedBytes")]
    public long UsedBytes { get; set; }

    [JsonProperty("quotaBytes")]
    public long QuotaBytes { get; set; }

    [JsonProperty("usedMB")]
    public double UsedMB { get; set; }

    [JsonProperty("quotaMB")]
    public double QuotaMB { get; set; }

    [JsonProperty("usedGB")]
    public double UsedGB { get; set; }

    [JsonProperty("quotaGB")]
    public double QuotaGB { get; set; }

    [JsonProperty("percentage")]
    public double Percentage { get; set; }

    [JsonProperty("formattedUsed")]
    public string FormattedUsed { get; set; } = string.Empty;

    [JsonProperty("formattedQuota")]
    public string FormattedQuota { get; set; } = string.Empty;

    [JsonProperty("fileCount")]
    public int FileCount { get; set; }
}
public class StatisticsOverview
{
    [JsonProperty("filesShared")]
    public int FilesShared { get; set; }

    [JsonProperty("filesReceived")]
    public int FilesReceived { get; set; }
}
public class ShareInfoResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("shareData")]
    public ShareData? ShareData { get; set; }

    [JsonProperty("fileData")]
    public ShareFileData? FileData { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class ShareData
{
    [JsonProperty("fileId")]
    public string FileId { get; set; } = string.Empty;

    [JsonProperty("ownerUid")]
    public string OwnerUid { get; set; } = string.Empty;

    [JsonProperty("boundUid")]
    public string? BoundUid { get; set; }

    [JsonProperty("valid")]
    public bool Valid { get; set; }
}
public class ShareFileData
{
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("originalName")]
    public string? OriginalName { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    [JsonProperty("remainingDownloads")]
    public int RemainingDownloads { get; set; }

    [JsonProperty("maxDownloads")]
    public int MaxDownloads { get; set; }

    [JsonProperty("shareMode")]
    public string? ShareMode { get; set; }

    [JsonProperty("allowedDevices")]
    public List<object>? AllowedDevices { get; set; }
}
public class VerifyPinRequest
{
    [JsonProperty("shareId")]
    public string ShareId { get; set; } = string.Empty;

    [JsonProperty("pin")]
    public string Pin { get; set; } = string.Empty;
}
public class VerifyPinResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("sessionToken")]
    public string? SessionToken { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class IssueDownloadUrlRequest
{
    [JsonProperty("shareId")]
    public string ShareId { get; set; } = string.Empty;

    [JsonProperty("sessionToken")]
    public string? SessionToken { get; set; }
}
public class IssueDownloadUrlResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("downloadUrl")]
    public string? DownloadUrl { get; set; }

    [JsonProperty("fileName")]
    public string? FileName { get; set; }

    [JsonProperty("fileSize")]
    public long FileSize { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
