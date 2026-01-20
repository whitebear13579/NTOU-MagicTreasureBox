using Newtonsoft.Json;

namespace sharelock_desktop.Models;
public class UserInfo
{
    [JsonProperty("uid")]
    public string Uid { get; set; } = string.Empty;

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("photoURL")]
    public string? PhotoUrl { get; set; }
}
public class SessionResponse
{
    [JsonProperty("authenticated")]
    public bool Authenticated { get; set; }

    [JsonProperty("uid")]
    public string? Uid { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }
}
public class CreateSessionRequest
{
    [JsonProperty("idToken")]
    public string IdToken { get; set; } = string.Empty;
}
public class CreateSessionResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("uid")]
    public string? Uid { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class RecordLoginRequest
{
    [JsonProperty("userId")]
    public string? UserId { get; set; }

    [JsonProperty("attemptedEmail")]
    public string? AttemptedEmail { get; set; }

    [JsonProperty("device")]
    public string Device { get; set; } = string.Empty;

    [JsonProperty("userAgent")]
    public string UserAgent { get; set; } = string.Empty;

    [JsonProperty("ip")]
    public string Ip { get; set; } = string.Empty;

    [JsonProperty("location")]
    public string Location { get; set; } = string.Empty;

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("provider")]
    public string? Provider { get; set; }

    [JsonProperty("errorMessage")]
    public string? ErrorMessage { get; set; }
}
public class AccountStatistics
{
    [JsonProperty("accountDays")]
    public int AccountDays { get; set; }

    [JsonProperty("filesShared")]
    public int FilesShared { get; set; }

    [JsonProperty("filesReceived")]
    public int FilesReceived { get; set; }

    [JsonProperty("lastLoginAt")]
    public DateTime? LastLoginAt { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? CreatedAt { get; set; }
    public string FormattedLastLogin
    {
        get
        {
            if (LastLoginAt == null) return "從未登入";
            var localTime = LastLoginAt.Value.ToLocalTime();
            return localTime.ToString("yyyy/MM/dd HH:mm");
        }
    }
    public string FormattedCreatedAt
    {
        get
        {
            if (CreatedAt == null) return "未知";
            var localTime = CreatedAt.Value.ToLocalTime();
            return localTime.ToString("yyyy/MM/dd");
        }
    }
}
public class AccountStatisticsResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("statistics")]
    public AccountStatistics? Statistics { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class UpdateDisplayNameRequest
{
    [JsonProperty("displayName")]
    public string DisplayName { get; set; } = string.Empty;
}
public class UpdateDisplayNameResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("displayName")]
    public string? DisplayName { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class DeleteAllFilesRequest
{
    [JsonProperty("confirmText")]
    public string ConfirmText { get; set; } = "DELETE";
}
public class DeleteAllFilesResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("deletedCount")]
    public int DeletedCount { get; set; }

    [JsonProperty("totalSizeDeleted")]
    public long TotalSizeDeleted { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
public class DeleteAccountResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }
}
