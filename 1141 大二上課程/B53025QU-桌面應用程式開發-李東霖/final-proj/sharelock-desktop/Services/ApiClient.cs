using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using sharelock_desktop.Models;

namespace sharelock_desktop.Services;

public class ApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientHandler _handler;
    private const string BaseUrl = "https://www.sharelock.qzz.io";

    public ApiClient()
    {
        _handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true,
            AllowAutoRedirect = true
        };

        _httpClient = new HttpClient(_handler)
        {
            BaseAddress = new Uri(BaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("*/*", 0.8));
        
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", "zh-TW,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        _httpClient.DefaultRequestHeaders.Add("Pragma", "no-cache");
    }

    public void SetAuthToken(string? token)
    {
        System.Diagnostics.Debug.WriteLine($"[ApiClient] 設定 Auth Token: {(string.IsNullOrEmpty(token) ? "空" : token[..Math.Min(20, token.Length)] + "...")}");
        
        _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token)
            ? null
            : new AuthenticationHeaderValue("Bearer", token);
    }

    private static string BuildApiUrl(string endpoint)
    {
        if (endpoint.StartsWith("/"))
        {
            return $"/api{endpoint}";
        }
        return $"/api/{endpoint}";
    }

    public async Task<T?> GetAsync<T>(string endpoint) where T : class
    {
        var apiPath = BuildApiUrl(endpoint);
        var fullUrl = $"{BaseUrl}{apiPath}";
        System.Diagnostics.Debug.WriteLine($"[ApiClient] GET {fullUrl}");
        
        try
        {
            var response = await _httpClient.GetAsync(apiPath);
            var content = await response.Content.ReadAsStringAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應狀態: {(int)response.StatusCode} {response.StatusCode}");
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應內容類型: {response.Content.Headers.ContentType}");
            
            var logContent = content.Length > 500 ? content[..500] + "..." : content;
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應內容: {logContent}");

            if (content.Contains("cf-browser-verification") || content.Contains("cf_clearance"))
            {
                System.Diagnostics.Debug.WriteLine($"[ApiClient] 偵測到 Cloudflare 驗證頁面");
                throw new ApiException("請求被 Cloudflare 阻擋，請稍後再試", 403);
            }

            if (!response.IsSuccessStatusCode)
            {
                if (content.TrimStart().StartsWith("<"))
                {
                    System.Diagnostics.Debug.WriteLine($"[ApiClient] 收到 HTML 回應而非 JSON");
                    throw new ApiException($"伺服器回傳了非 JSON 格式的回應 (HTTP {(int)response.StatusCode})", (int)response.StatusCode);
                }
                
                var errorResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                throw new ApiException(
                    errorResponse?.Error ?? $"API 錯誤: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            if (content.TrimStart().StartsWith("<"))
            {
                System.Diagnostics.Debug.WriteLine($"[ApiClient] 成功狀態但收到 HTML 回應");
                throw new ApiException("伺服器回傳了非 JSON 格式的回應", 200);
            }

            return JsonConvert.DeserializeObject<T>(content);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (JsonException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] JSON 解析錯誤: {ex.Message}");
            throw new ApiException($"JSON 解析錯誤: {ex.Message}", 0, ex);
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 網路錯誤: {ex.Message}");
            throw new ApiException($"網路錯誤: {ex.Message}", 0, ex);
        }
        catch (TaskCanceledException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 請求逾時");
            throw new ApiException("請求逾時", 408, ex);
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        where TResponse : class
    {
        var apiPath = BuildApiUrl(endpoint);
        var fullUrl = $"{BaseUrl}{apiPath}";
        System.Diagnostics.Debug.WriteLine($"[ApiClient] POST {fullUrl}");
        
        try
        {
            var json = JsonConvert.SerializeObject(data);
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 請求內容: {(json.Length > 200 ? json[..200] + "..." : json)}");
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiPath, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應狀態: {(int)response.StatusCode} {response.StatusCode}");
            
            var logContent = responseContent.Length > 500 ? responseContent[..500] + "..." : responseContent;
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應內容: {logContent}");

            if (!response.IsSuccessStatusCode)
            {
                if (responseContent.TrimStart().StartsWith("<"))
                {
                    throw new ApiException($"伺服器回傳了非 JSON 格式的回應 (HTTP {(int)response.StatusCode})", (int)response.StatusCode);
                }
                
                var errorResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                throw new ApiException(
                    errorResponse?.Error ?? $"API 錯誤: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            if (responseContent.TrimStart().StartsWith("<"))
            {
                throw new ApiException("伺服器回傳了非 JSON 格式的回應", 200);
            }

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (JsonException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] JSON 解析錯誤: {ex.Message}");
            throw new ApiException($"JSON 解析錯誤: {ex.Message}", 0, ex);
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 網路錯誤: {ex.Message}");
            throw new ApiException($"網路錯誤: {ex.Message}", 0, ex);
        }
        catch (TaskCanceledException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 請求逾時");
            throw new ApiException("請求逾時", 408, ex);
        }
    }

    public async Task<TResponse?> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data)
        where TResponse : class
    {
        var apiPath = BuildApiUrl(endpoint);
        var fullUrl = $"{BaseUrl}{apiPath}";
        System.Diagnostics.Debug.WriteLine($"[ApiClient] PATCH {fullUrl}");
        
        try
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Patch, apiPath) { Content = content };

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應狀態: {(int)response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                if (responseContent.TrimStart().StartsWith("<"))
                {
                    throw new ApiException($"伺服器回傳了非 JSON 格式的回應 (HTTP {(int)response.StatusCode})", (int)response.StatusCode);
                }
                
                var errorResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                throw new ApiException(
                    errorResponse?.Error ?? $"API 錯誤: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 網路錯誤: {ex.Message}");
            throw new ApiException($"網路錯誤: {ex.Message}", 0, ex);
        }
    }

    public async Task<TResponse?> DeleteAsync<TResponse>(string endpoint) where TResponse : class
    {
        var apiPath = BuildApiUrl(endpoint);
        var fullUrl = $"{BaseUrl}{apiPath}";
        System.Diagnostics.Debug.WriteLine($"[ApiClient] DELETE {fullUrl}");
        
        try
        {
            var response = await _httpClient.DeleteAsync(apiPath);
            var content = await response.Content.ReadAsStringAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應狀態: {(int)response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                if (content.TrimStart().StartsWith("<"))
                {
                    throw new ApiException($"伺服器回傳了非 JSON 格式的回應 (HTTP {(int)response.StatusCode})", (int)response.StatusCode);
                }
                
                var errorResponse = JsonConvert.DeserializeObject<ApiResponse>(content);
                throw new ApiException(
                    errorResponse?.Error ?? $"API 錯誤: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            return JsonConvert.DeserializeObject<TResponse>(content);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 網路錯誤: {ex.Message}");
            throw new ApiException($"網路錯誤: {ex.Message}", 0, ex);
        }
    }

    public async Task<TResponse?> DeleteAsync<TRequest, TResponse>(string endpoint, TRequest data)
        where TResponse : class
    {
        var apiPath = BuildApiUrl(endpoint);
        var fullUrl = $"{BaseUrl}{apiPath}";
        System.Diagnostics.Debug.WriteLine($"[ApiClient] DELETE (with body) {fullUrl}");
        
        try
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, apiPath) { Content = content };

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 回應狀態: {(int)response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                if (responseContent.TrimStart().StartsWith("<"))
                {
                    throw new ApiException($"伺服器回傳了非 JSON 格式的回應 (HTTP {(int)response.StatusCode})", (int)response.StatusCode);
                }
                
                var errorResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                throw new ApiException(
                    errorResponse?.Error ?? $"API 錯誤: {response.StatusCode}",
                    (int)response.StatusCode);
            }

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        catch (ApiException)
        {
            throw;
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 網路錯誤: {ex.Message}");
            throw new ApiException($"網路錯誤: {ex.Message}", 0, ex);
        }
    }

    public async Task<byte[]> DownloadFileAsync(string url)
    {
        System.Diagnostics.Debug.WriteLine($"[ApiClient] 下載檔案: {url}");
        
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 下載錯誤: {ex.Message}");
            throw new ApiException($"下載錯誤: {ex.Message}", 0, ex);
        }
    }

    public async Task DownloadFileToStreamAsync(string url, Stream outputStream, IProgress<long>? progress = null)
    {
        System.Diagnostics.Debug.WriteLine($"[ApiClient] 串流下載檔案: {url}");
        
        try
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            var buffer = new byte[8192];
            long totalRead = 0;
            int bytesRead;

            while ((bytesRead = await contentStream.ReadAsync(buffer)) > 0)
            {
                await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                totalRead += bytesRead;
                progress?.Report(totalRead);
            }
            
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 下載完成，共 {totalRead} bytes");
        }
        catch (HttpRequestException ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ApiClient] 下載錯誤: {ex.Message}");
            throw new ApiException($"下載錯誤: {ex.Message}", 0, ex);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        _handler.Dispose();
        GC.SuppressFinalize(this);
    }
}

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(string message, int statusCode, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
