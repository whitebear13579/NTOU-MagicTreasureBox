using sharelock_desktop.Models;
using sharelock_desktop.Utils;
using System.Text.Json;

namespace sharelock_desktop.Services;
public class FirebaseAuthService
{
    private readonly HttpClient _httpClient;
    private const string FirebaseAuthUrl = "https://identitytoolkit.googleapis.com/v1/accounts";

    public FirebaseAuthService()
    {
        _httpClient = new HttpClient();
    }
    public async Task<FirebaseAuthResult> SignInWithEmailPasswordAsync(string email, string password)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:signInWithPassword?key={FirebaseConfig.ApiKey}";

            var request = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<FirebaseSignInResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                {
                    return new FirebaseAuthResult
                    {
                        Success = true,
                        IdToken = result.IdToken,
                        RefreshToken = result.RefreshToken,
                        UserId = result.LocalId,
                        Email = result.Email,
                        DisplayName = result.DisplayName
                    };
                }
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "EMAIL_NOT_FOUND" => "找不到此電子郵件帳號",
                "INVALID_PASSWORD" => "密碼錯誤",
                "USER_DISABLED" => "此帳號已被停用",
                "INVALID_EMAIL" => "電子郵件格式不正確",
                "INVALID_LOGIN_CREDENTIALS" => "帳號或密碼錯誤",
                "TOO_MANY_ATTEMPTS_TRY_LATER" => "登入嘗試次數過多，請稍後再試",
                _ => errorResponse?.Error?.Message ?? "登入失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"登入失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseAuthResult> SendPasswordResetEmailAsync(string email)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:sendOobCode?key={FirebaseConfig.ApiKey}";

            var request = new
            {
                requestType = "PASSWORD_RESET",
                email
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new FirebaseAuthResult
                {
                    Success = true,
                    Email = email
                };
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "EMAIL_NOT_FOUND" => "找不到此電子郵件帳號",
                "INVALID_EMAIL" => "電子郵件格式不正確",
                "TOO_MANY_ATTEMPTS_TRY_LATER" => "嘗試次數過多，請稍後再試",
                _ => errorResponse?.Error?.Message ?? "發送失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"發送失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseAuthResult> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var url = $"https://securetoken.googleapis.com/v1/token?key={FirebaseConfig.ApiKey}";

            var request = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            var content = new FormUrlEncodedContent(request);
            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<FirebaseRefreshResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                {
                    return new FirebaseAuthResult
                    {
                        Success = true,
                        IdToken = result.Id_Token,
                        RefreshToken = result.Refresh_Token,
                        UserId = result.User_Id
                    };
                }
            }

            return new FirebaseAuthResult
            {
                Success = false,
                Error = "Token 更新失敗"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"Token 更新失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseUserInfo?> GetUserInfoAsync(string idToken)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:lookup?key={FirebaseConfig.ApiKey}";

            var request = new { idToken };
            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<FirebaseLookupResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Users?.Length > 0)
                {
                    var user = result.Users[0];
                    return new FirebaseUserInfo
                    {
                        UserId = user.LocalId,
                        Email = user.Email,
                        DisplayName = user.DisplayName,
                        PhotoUrl = user.PhotoUrl,
                        EmailVerified = user.EmailVerified
                    };
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
    public async Task<FirebaseAuthResult> UpdateProfileAsync(string idToken, string? displayName = null, string? photoUrl = null)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:update?key={FirebaseConfig.ApiKey}";

            var requestObj = new Dictionary<string, object>
            {
                { "idToken", idToken },
                { "returnSecureToken", true }
            };

            if (displayName != null)
            {
                requestObj["displayName"] = displayName;
            }

            if (photoUrl != null)
            {
                requestObj["photoUrl"] = photoUrl;
            }

            var jsonContent = JsonSerializer.Serialize(requestObj);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<FirebaseUpdateProfileResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                {
                    return new FirebaseAuthResult
                    {
                        Success = true,
                        IdToken = result.IdToken,
                        RefreshToken = result.RefreshToken,
                        UserId = result.LocalId,
                        Email = result.Email,
                        DisplayName = result.DisplayName,
                        PhotoUrl = result.PhotoUrl
                    };
                }
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "INVALID_ID_TOKEN" => "登入已過期，請重新登入",
                "USER_NOT_FOUND" => "找不到使用者",
                "TOKEN_EXPIRED" => "登入已過期，請重新登入",
                _ => errorResponse?.Error?.Message ?? "更新失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"更新失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseAuthResult> SendEmailVerificationAsync(string idToken)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:sendOobCode?key={FirebaseConfig.ApiKey}";

            var request = new
            {
                requestType = "VERIFY_EMAIL",
                idToken
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new FirebaseAuthResult
                {
                    Success = true
                };
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "INVALID_ID_TOKEN" => "登入已過期，請重新登入",
                "USER_NOT_FOUND" => "找不到使用者",
                "TOO_MANY_ATTEMPTS_TRY_LATER" => "已達速率限制，請稍後再試",
                _ => errorResponse?.Error?.Message ?? "發送失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"發送失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseAuthResult> UpdateEmailAsync(string idToken, string newEmail)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:update?key={FirebaseConfig.ApiKey}";

            var request = new
            {
                idToken,
                email = newEmail,
                returnSecureToken = true
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<FirebaseUpdateProfileResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result != null)
                {
                    return new FirebaseAuthResult
                    {
                        Success = true,
                        IdToken = result.IdToken,
                        RefreshToken = result.RefreshToken,
                        UserId = result.LocalId,
                        Email = result.Email
                    };
                }
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "INVALID_ID_TOKEN" => "登入已過期，請重新登入",
                "EMAIL_EXISTS" => "此電子郵件已被使用",
                "INVALID_EMAIL" => "電子郵件格式不正確",
                "CREDENTIAL_TOO_OLD_LOGIN_AGAIN" => "請重新登入後再試",
                _ => errorResponse?.Error?.Message ?? "更新失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"更新失敗：{ex.Message}"
            };
        }
    }
    public async Task<FirebaseAuthResult> DeleteAccountAsync(string idToken)
    {
        try
        {
            var url = $"{FirebaseAuthUrl}:delete?key={FirebaseConfig.ApiKey}";

            var request = new
            {
                idToken
            };

            var jsonContent = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return new FirebaseAuthResult
                {
                    Success = true
                };
            }

            var errorResponse = JsonSerializer.Deserialize<FirebaseErrorResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var errorMessage = errorResponse?.Error?.Message switch
            {
                "INVALID_ID_TOKEN" => "登入已過期，請重新登入",
                "USER_NOT_FOUND" => "找不到使用者",
                "CREDENTIAL_TOO_OLD_LOGIN_AGAIN" => "請重新登入後再試",
                _ => errorResponse?.Error?.Message ?? "刪除失敗"
            };

            return new FirebaseAuthResult
            {
                Success = false,
                Error = errorMessage
            };
        }
        catch (HttpRequestException ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"網路連線失敗：{ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new FirebaseAuthResult
            {
                Success = false,
                Error = $"刪除失敗：{ex.Message}"
            };
        }
    }
}
public class FirebaseAuthResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public string? IdToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
}
public class FirebaseUserInfo
{
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public bool EmailVerified { get; set; }
}

internal class FirebaseSignInResponse
{
    public string? IdToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? LocalId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public bool Registered { get; set; }
}

internal class FirebaseRefreshResponse
{
    public string? Id_Token { get; set; }
    public string? Refresh_Token { get; set; }
    public string? User_Id { get; set; }
}

internal class FirebaseLookupResponse
{
    public FirebaseLookupUser[]? Users { get; set; }
}

internal class FirebaseLookupUser
{
    public string? LocalId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public bool EmailVerified { get; set; }
}

internal class FirebaseErrorResponse
{
    public FirebaseError? Error { get; set; }
}

internal class FirebaseError
{
    public int Code { get; set; }
    public string? Message { get; set; }
}

internal class FirebaseUpdateProfileResponse
{
    public string? IdToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? LocalId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
}
public class AuthService
{
    private readonly ApiClient _apiClient;

    public AuthService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<CreateSessionResponse?> CreateSessionAsync(string idToken)
    {
        var request = new CreateSessionRequest { IdToken = idToken };
        return await _apiClient.PostAsync<CreateSessionRequest, CreateSessionResponse>(
            "/auth/session", request);
    }
    public async Task<SessionResponse?> VerifySessionAsync()
    {
        return await _apiClient.GetAsync<SessionResponse>("/auth/session");
    }
    public async Task<ApiResponse?> LogoutAsync()
    {
        return await _apiClient.DeleteAsync<ApiResponse>("/auth/session");
    }
    public async Task<ApiResponse?> RecordLoginAsync(RecordLoginRequest request)
    {
        return await _apiClient.PostAsync<RecordLoginRequest, ApiResponse>(
            "/auth/record-login", request);
    }
}
public class FileService
{
    private readonly ApiClient _apiClient;

    public FileService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<FileListResponse?> GetFilesAsync(string type = "myFiles")
    {
        return await _apiClient.GetAsync<FileListResponse>($"/files/list?type={type}");
    }
    public async Task<FileDetailResponse?> GetFileDetailAsync(string fileId)
    {
        return await _apiClient.GetAsync<FileDetailResponse>($"/files/detail?fileId={fileId}");
    }
    public async Task<FileListResponse?> GetRecentFilesAsync()
    {
        return await _apiClient.GetAsync<FileListResponse>("/files/recent");
    }
    public async Task<DownloadResponse?> InitiateDownloadAsync(string fileId, string? shareId = null)
    {
        var request = new DownloadRequest { FileId = fileId, ShareId = shareId };
        return await _apiClient.PostAsync<DownloadRequest, DownloadResponse>("/files/download", request);
    }
    public async Task<ApiResponse?> UpdateFileAsync(string fileId, FileUpdates updates)
    {
        var request = new UpdateFileRequest { FileId = fileId, Updates = updates };
        return await _apiClient.PatchAsync<UpdateFileRequest, ApiResponse>("/files/update", request);
    }
    public async Task<ApiResponse?> DeleteFileAsync(string fileId)
    {
        var request = new DeleteFileRequest { FileId = fileId };
        return await _apiClient.DeleteAsync<DeleteFileRequest, ApiResponse>("/files/delete", request);
    }
    public async Task<DeleteAllFilesResponse?> DeleteAllFilesAsync()
    {
        var request = new DeleteAllFilesRequest { ConfirmText = "DELETE" };
        return await _apiClient.DeleteAsync<DeleteAllFilesRequest, DeleteAllFilesResponse>(
            "/files/delete-all", request);
    }
}
public class ShareService
{
    private readonly ApiClient _apiClient;

    public ShareService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<ShareInfoResponse?> GetShareInfoAsync(string shareId)
    {
        var request = new { shareId };
        return await _apiClient.PostAsync<object, ShareInfoResponse>("/share/get-info", request);
    }
    public async Task<VerifyPinResponse?> VerifyPinAsync(string shareId, string pin)
    {
        var request = new VerifyPinRequest { ShareId = shareId, Pin = pin };
        return await _apiClient.PostAsync<VerifyPinRequest, VerifyPinResponse>("/share/verify-pin", request);
    }
    public async Task<ApiResponse?> BindAccountAsync(string shareId, string userId)
    {
        var request = new { shareId, userId };
        return await _apiClient.PostAsync<object, ApiResponse>("/share/bind-account", request);
    }
}
public class NotificationService
{
    private readonly ApiClient _apiClient;

    public NotificationService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<NotificationListResponse?> GetNotificationsAsync()
    {
        return await _apiClient.GetAsync<NotificationListResponse>("/notifications/list");
    }
    public async Task<ApiResponse?> DeleteNotificationAsync(string notificationId)
    {
        return await _apiClient.DeleteAsync<ApiResponse>(
            $"/notifications/delete?notificationId={notificationId}");
    }
    public async Task<ApiResponse?> RespondToInvitationAsync(string notificationId, string shareId, string action)
    {
        var request = new RespondInvitationRequest
        {
            NotificationId = notificationId,
            ShareId = shareId,
            Action = action
        };
        return await _apiClient.PostAsync<RespondInvitationRequest, ApiResponse>(
            "/notifications/respond", request);
    }
}
public class StorageService
{
    private readonly ApiClient _apiClient;

    public StorageService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<StorageUsage?> GetUsageAsync()
    {
        return await _apiClient.GetAsync<StorageUsage>("/storage/usage");
    }
}
public class DownloadService
{
    private readonly ApiClient _apiClient;

    public DownloadService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<IssueDownloadUrlResponse?> IssueDownloadUrlAsync(string shareId, string? sessionToken = null)
    {
        var request = new IssueDownloadUrlRequest
        {
            ShareId = shareId,
            SessionToken = sessionToken
        };
        return await _apiClient.PostAsync<IssueDownloadUrlRequest, IssueDownloadUrlResponse>(
            "/download/issue-url", request);
    }
    public async Task DownloadFileAsync(string downloadUrl, string savePath, IProgress<long>? progress = null)
    {
        using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await _apiClient.DownloadFileToStreamAsync(downloadUrl, fileStream, progress);
    }
}
public class StatisticsService
{
    private readonly ApiClient _apiClient;

    public StatisticsService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<StatisticsOverview?> GetOverviewAsync()
    {
        return await _apiClient.GetAsync<StatisticsOverview>("/statistics/overview");
    }
}
public class UserService
{
    private readonly ApiClient _apiClient;

    public UserService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    public async Task<UpdateDisplayNameResponse?> UpdateDisplayNameAsync(string displayName)
    {
        var request = new UpdateDisplayNameRequest { DisplayName = displayName };
        return await _apiClient.PatchAsync<UpdateDisplayNameRequest, UpdateDisplayNameResponse>(
            "/user/display-name", request);
    }
    public async Task<DeleteAccountResponse?> DeleteAccountAsync()
    {
        return await _apiClient.DeleteAsync<DeleteAccountResponse>("/user/account");
    }
}
