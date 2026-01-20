using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace sharelock_desktop.Utils;
public class OAuthHelper : IDisposable
{
    private HttpListener? _httpListener;
    private string? _state;
    private TaskCompletionSource<OAuthResult>? _authCompletionSource;
    private CancellationTokenSource? _timeoutCancellationSource;
    private bool _disposed;
    public async Task<OAuthResult> StartOAuthFlowAsync(OAuthProvider? provider = null)
    {
        _authCompletionSource = new TaskCompletionSource<OAuthResult>();
        _timeoutCancellationSource = new CancellationTokenSource();

        _state = GenerateRandomString(32);

        try
        {

            await StartLocalServerAsync();

            var loginUrl = BuildLoginUrl(provider, _state);

            Process.Start(new ProcessStartInfo(loginUrl) { UseShellExecute = true });

            var timeoutTask = Task.Delay(TimeSpan.FromMinutes(5), _timeoutCancellationSource.Token);

            var completedTask = await Task.WhenAny(
                _authCompletionSource.Task,
                timeoutTask
            );

            if (completedTask == timeoutTask)
            {
                return new OAuthResult { Success = false, Error = "登入逾時，請重試" };
            }

            return await _authCompletionSource.Task;
        }
        catch (OperationCanceledException)
        {
            return new OAuthResult { Success = false, Error = "登入已取消" };
        }
        catch (Exception ex)
        {
            return new OAuthResult { Success = false, Error = $"登入失敗：{ex.Message}" };
        }
        finally
        {
            StopLocalServer();
        }
    }
    public void CancelLogin()
    {
        _timeoutCancellationSource?.Cancel();
        _authCompletionSource?.TrySetResult(new OAuthResult { Success = false, Error = "登入已取消" });
        StopLocalServer();
    }
    private Task StartLocalServerAsync()
    {
        _httpListener = new HttpListener();
        _httpListener.Prefixes.Add("http://localhost:8765/");

        try
        {
            _httpListener.Start();
        }
        catch (HttpListenerException ex)
        {
            throw new InvalidOperationException($"無法啟動本地伺服器，埠口 8765 可能被佔用：{ex.Message}", ex);
        }

        _ = Task.Run(async () =>
        {
            try
            {
                while (_httpListener?.IsListening == true)
                {
                    var context = await _httpListener.GetContextAsync();
                    _ = HandleRequestAsync(context);
                }
            }
            catch (ObjectDisposedException)
            {

            }
            catch (HttpListenerException)
            {

            }
        });

        return Task.CompletedTask;
    }
    private void StopLocalServer()
    {
        try
        {
            _httpListener?.Stop();
            _httpListener?.Close();
            _httpListener = null;
        }
        catch
        {

        }
    }
    private async Task HandleRequestAsync(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        try
        {
            var path = request.Url?.AbsolutePath ?? "";

            switch (path)
            {
                case "/callback":
                    await HandleCallbackAsync(request, response);
                    break;

                case "/auth-result":
                    await HandleAuthResultAsync(request, response);
                    break;

                default:
                    response.StatusCode = 404;
                    await WriteResponseAsync(response, "Not Found", "text/plain");
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"處理請求時發生錯誤: {ex.Message}");
            try
            {
                response.StatusCode = 500;
                await WriteResponseAsync(response, "Internal Server Error", "text/plain");
            }
            catch { }
        }
    }
    private async Task HandleCallbackAsync(HttpListenerRequest request, HttpListenerResponse response)
    {
        var query = HttpUtility.ParseQueryString(request.Url?.Query ?? "");
        var state = query["state"];
        var error = query["error"];

        if (!string.IsNullOrEmpty(state) && state != _state)
        {
            await WriteResponseAsync(response, GetErrorHtml("安全驗證失敗，請重新登入"), "text/html");
            _authCompletionSource?.TrySetResult(new OAuthResult
            {
                Success = false,
                Error = "安全驗證失敗"
            });
            return;
        }

        if (!string.IsNullOrEmpty(error))
        {
            var errorDesc = query["error_description"] ?? error;
            await WriteResponseAsync(response, GetErrorHtml(errorDesc), "text/html");
            _authCompletionSource?.TrySetResult(new OAuthResult
            {
                Success = false,
                Error = errorDesc
            });
            return;
        }

        var idToken = query["idToken"] ?? query["id_token"];
        if (!string.IsNullOrEmpty(idToken))
        {
            var result = new OAuthResult
            {
                Success = true,
                IdToken = idToken,
                RefreshToken = query["refreshToken"] ?? query["refresh_token"],
                UserId = query["uid"] ?? query["userId"],
                Email = query["email"],
                DisplayName = query["displayName"] ?? query["name"],
                PhotoUrl = query["photoUrl"] ?? query["photoURL"] ?? query["picture"]
            };

            await WriteResponseAsync(response, GetSuccessHtml(), "text/html");
            _authCompletionSource?.TrySetResult(result);
            return;
        }

        await WriteResponseAsync(response, GetWaitingHtml(), "text/html");
    }
    private async Task HandleAuthResultAsync(HttpListenerRequest request, HttpListenerResponse response)
    {

        response.Headers.Add("Access-Control-Allow-Origin", FirebaseConfig.WebAppUrl);
        response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

        if (request.HttpMethod == "OPTIONS")
        {
            response.StatusCode = 204;
            response.Close();
            return;
        }

        if (request.HttpMethod != "POST")
        {
            response.StatusCode = 405;
            await WriteResponseAsync(response, "Method Not Allowed", "text/plain");
            return;
        }

        try
        {

            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            var body = await reader.ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

            if (data == null)
            {
                response.StatusCode = 400;
                await WriteResponseAsync(response, "{\"success\":false,\"error\":\"Invalid request body\"}", "application/json");
                return;
            }

            if (data.TryGetValue("state", out var state) && state != _state)
            {
                response.StatusCode = 400;
                await WriteResponseAsync(response, "{\"success\":false,\"error\":\"State mismatch\"}", "application/json");
                _authCompletionSource?.TrySetResult(new OAuthResult
                {
                    Success = false,
                    Error = "安全驗證失敗"
                });
                return;
            }

            if (data.TryGetValue("error", out var error) && !string.IsNullOrEmpty(error))
            {
                data.TryGetValue("error_description", out var errorDesc);
                await WriteResponseAsync(response, "{\"success\":false}", "application/json");
                _authCompletionSource?.TrySetResult(new OAuthResult
                {
                    Success = false,
                    Error = errorDesc ?? error
                });
                return;
            }

            data.TryGetValue("idToken", out var idToken);
            data.TryGetValue("refreshToken", out var refreshToken);
            data.TryGetValue("uid", out var uid);
            data.TryGetValue("email", out var email);
            data.TryGetValue("displayName", out var displayName);
            data.TryGetValue("photoUrl", out var photoUrl);

            if (string.IsNullOrEmpty(idToken))
            {
                response.StatusCode = 400;
                await WriteResponseAsync(response, "{\"success\":false,\"error\":\"Missing idToken\"}", "application/json");
                return;
            }

            var result = new OAuthResult
            {
                Success = true,
                IdToken = idToken,
                RefreshToken = refreshToken,
                UserId = uid,
                Email = email,
                DisplayName = displayName,
                PhotoUrl = photoUrl
            };

            await WriteResponseAsync(response, "{\"success\":true}", "application/json");
            _authCompletionSource?.TrySetResult(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"處理 auth-result 失敗: {ex.Message}");
            response.StatusCode = 500;
            await WriteResponseAsync(response, $"{{\"success\":false,\"error\":\"{ex.Message}\"}}", "application/json");
        }
    }
    private static async Task WriteResponseAsync(HttpListenerResponse response, string content, string contentType)
    {
        var buffer = Encoding.UTF8.GetBytes(content);
        response.ContentType = $"{contentType}; charset=utf-8";
        response.ContentLength64 = buffer.Length;
        await response.OutputStream.WriteAsync(buffer);
        response.OutputStream.Close();
    }
    private static string BuildLoginUrl(OAuthProvider? provider, string state)
    {
        var baseUrl = FirebaseConfig.WebAppUrl;
        var redirectUri = Uri.EscapeDataString(FirebaseConfig.RedirectUri);
        var callbackUrl = Uri.EscapeDataString($"http://localhost:8765/auth-result");

        var url = $"{baseUrl}/login?desktop=true&redirect_uri={redirectUri}&callback_url={callbackUrl}&state={state}";

        if (provider.HasValue)
        {
            var providerParam = provider.Value == OAuthProvider.Google ? "google" : "github";
            url += $"&provider={providerParam}";
        }

        return url;
    }
    private static string GenerateRandomString(int length)
    {
        var random = new byte[length];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(random);
        return Convert.ToBase64String(random)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "")
            [..Math.Min(length, Convert.ToBase64String(random).Length)];
    }
    private static string GetWaitingHtml()
    {
        return @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>正在登入 - ShareLock</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Microsoft JhengHei', sans-serif;
            background: linear-gradient(135deg, #1e3a5f 0%, #0f172a 100%);
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            text-align: center;
            padding: 40px;
            background: rgba(255,255,255,0.1);
            border-radius: 16px;
            backdrop-filter: blur(10px);
        }
        .spinner {
            width: 50px;
            height: 50px;
            border: 4px solid rgba(255,255,255,0.3);
            border-top-color: #3b82f6;
            border-radius: 50%;
            animation: spin 1s linear infinite;
            margin: 0 auto 20px;
        }
        @keyframes spin {
            to { transform: rotate(360deg); }
        }
        h1 { margin: 0 0 10px 0; font-size: 24px; }
        p { color: #94a3b8; margin: 0; }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""spinner""></div>
        <h1>正在登入中...</h1>
        <p>請在瀏覽器中完成登入，此頁面將自動關閉。</p>
    </div>
</body>
</html>";
    }
    private static string GetSuccessHtml()
    {
        return @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>登入成功 - ShareLock</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Microsoft JhengHei', sans-serif;
            background: linear-gradient(135deg, #1e3a5f 0%, #0f172a 100%);
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .container {
            text-align: center;
            padding: 40px;
            background: rgba(255,255,255,0.1);
            border-radius: 16px;
            backdrop-filter: blur(10px);
        }
        .success-icon {
            font-size: 64px;
            margin-bottom: 20px;
            color: #22c55e;
        }
        h1 { margin: 0 0 10px 0; }
        p { color: #94a3b8; margin: 0; }
    </style>
    <script>
        setTimeout(function() { window.close(); }, 2000);
    </script>
</head>
<body>
    <div class=""container"">
        <div class=""success-icon"">✓</div>
        <h1>登入成功！</h1>
        <p>您可以關閉此視窗並返回 ShareLock 桌面應用程式。</p>
        <p style=""margin-top: 10px; font-size: 12px;"">此視窗將在 2 秒後自動關閉...</p>
    </div>
</body>
</html>";
    }
    private static string GetErrorHtml(string error)
    {
        var escapedError = HttpUtility.HtmlEncode(error);
        return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>登入失敗 - ShareLock</title>
    <style>
        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Microsoft JhengHei', sans-serif;
            background: linear-gradient(135deg, #5f1e1e 0%, #0f172a 100%);
            color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }}
        .container {{
            text-align: center;
            padding: 40px;
            background: rgba(255,255,255,0.1);
            border-radius: 16px;
            backdrop-filter: blur(10px);
        }}
        .error-icon {{
            font-size: 64px;
            margin-bottom: 20px;
            color: #ef4444;
        }}
        h1 {{ margin: 0 0 10px 0; color: #f87171; }}
        p {{ color: #94a3b8; margin: 0; }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""error-icon"">✗</div>
        <h1>登入失敗</h1>
        <p>{escapedError}</p>
        <p style=""margin-top: 15px; font-size: 14px;"">請關閉此視窗並重試。</p>
    </div>
</body>
</html>";
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        StopLocalServer();
        _timeoutCancellationSource?.Cancel();
        _timeoutCancellationSource?.Dispose();
        GC.SuppressFinalize(this);
    }
}
public class OAuthResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public string? AuthorizationCode { get; set; }
    public string? CodeVerifier { get; set; }
    public string? IdToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? UserId { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
}
