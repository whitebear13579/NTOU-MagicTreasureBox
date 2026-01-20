using Newtonsoft.Json;

namespace sharelock_desktop.Models;

public class ValidateUploadRequest
{
    [JsonProperty("fileSize")]
    public long FileSize { get; set; }
}

public class ValidateUploadResponse
{
    [JsonProperty("allowed")]
    public bool Allowed { get; set; }

    [JsonProperty("availableBytes")]
    public long AvailableBytes { get; set; }

    [JsonProperty("usedBytes")]
    public long UsedBytes { get; set; }

    [JsonProperty("quotaBytes")]
    public long QuotaBytes { get; set; }

    [JsonProperty("validationToken")]
    public string? ValidationToken { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class ConfirmUploadRequest
{
    [JsonProperty("validationToken")]
    public string ValidationToken { get; set; } = string.Empty;

    [JsonProperty("storagePath")]
    public string StoragePath { get; set; } = string.Empty;

    [JsonProperty("actualSize")]
    public long ActualSize { get; set; }
}
public class ConfirmUploadResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("confirmedSize")]
    public long ConfirmedSize { get; set; }

    [JsonProperty("newUsedBytes")]
    public long NewUsedBytes { get; set; }

    [JsonProperty("availableBytes")]
    public long AvailableBytes { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }
}
public class CreateShareRequest
{
    [JsonProperty("fileId")]
    public string? FileId { get; set; }

    [JsonProperty("storagePath")]
    public string? StoragePath { get; set; }

    [JsonProperty("fileName")]
    public string? FileName { get; set; }

    [JsonProperty("fileSize")]
    public long FileSize { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("expiresAt")]
    public DateTime ExpiresAt { get; set; }

    [JsonProperty("maxDownloads")]
    public int MaxDownloads { get; set; }

    [JsonProperty("shareMode")]
    public string ShareMode { get; set; } = "public";

    [JsonProperty("pin")]
    public string? Pin { get; set; }

    [JsonProperty("recipients")]
    public List<string> Recipients { get; set; } = new();
}
public class CreateShareResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("shareId")]
    public string? ShareId { get; set; }

    [JsonProperty("shareUrl")]
    public string? ShareUrl { get; set; }

    [JsonProperty("fileId")]
    public string? FileId { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}

public class UploadProgressInfo
{
    public long BytesTransferred { get; set; }
    public long TotalBytes { get; set; }
    public double Percentage => TotalBytes > 0 ? (double)BytesTransferred / TotalBytes * 100 : 0;
    public string Status { get; set; } = "準備中";
}
public static class ShareModeConstants
{
    public const string Public = "public";
    public const string Pin = "pin";
    public const string Account = "account";
    public const string Device = "device";

    public static string GetDisplayName(string mode)
    {
        return mode switch
        {
            Public => "公開（任何人可存取）",
            Pin => "密碼保護（需輸入 PIN）",
            Account => "帳號綁定（首個綁定帳號）",
            Device => "裝置綁定（首個綁定裝置）",
            _ => "公開"
        };
    }
}
