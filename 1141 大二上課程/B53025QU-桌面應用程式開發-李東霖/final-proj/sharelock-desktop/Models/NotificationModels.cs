using Newtonsoft.Json;

namespace sharelock_desktop.Models;
public class NotificationInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("shareId")]
    public string? ShareId { get; set; }

    [JsonProperty("fileId")]
    public string? FileId { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("delivered")]
    public bool Delivered { get; set; }

    [JsonProperty("deliveredAt")]
    public DateTime? DeliveredAt { get; set; }

    [JsonProperty("senderInfo")]
    public SenderInfo? SenderInfo { get; set; }

    [JsonProperty("fileInfo")]
    public NotificationFileInfo? FileInfo { get; set; }

    public string TypeDisplayName => Type switch
    {
        "share-invite" => "分享邀請",
        "share-accepted" => "邀請已接受",
        "share-rejected" => "邀請已拒絕",
        "download-complete" => "下載完成",
        "share-expired" => "分享過期",
        _ => "通知"
    };

    public string RelativeTime
    {
        get
        {
            if (!CreatedAt.HasValue) return "未知時間";
            var diff = DateTime.UtcNow - CreatedAt.Value;
            if (diff.TotalMinutes < 1) return "剛剛";
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes} 分鐘前";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours} 小時前";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays} 天前";
            return CreatedAt.Value.ToString("yyyy/MM/dd");
        }
    }
}
public class SenderInfo
{
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("photoURL")]
    public string? PhotoUrl { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }
}
public class NotificationFileInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonProperty("size")]
    public long Size { get; set; }

    [JsonProperty("contentType")]
    public string? ContentType { get; set; }

    [JsonProperty("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    [JsonProperty("shareMode")]
    public string? ShareMode { get; set; }
}
public class NotificationListResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("notifications")]
    public List<NotificationInfo>? Notifications { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class RespondInvitationRequest
{
    [JsonProperty("notificationId")]
    public string NotificationId { get; set; } = string.Empty;

    [JsonProperty("shareId")]
    public string ShareId { get; set; } = string.Empty;

    [JsonProperty("action")]
    public string Action { get; set; } = string.Empty;
}
