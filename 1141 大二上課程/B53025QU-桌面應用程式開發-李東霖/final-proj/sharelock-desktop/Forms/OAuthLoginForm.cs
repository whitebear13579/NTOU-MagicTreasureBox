using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using sharelock_desktop.Utils;
using System.Text.Json;

namespace sharelock_desktop.Forms;
public partial class OAuthLoginForm : Form
{
    private WebView2? _webView;
    private readonly OAuthProvider _provider;
    private bool _loginCompleted;
    private int _extractAttempts;
    private const int MaxExtractAttempts = 20;

    public OAuthResult? Result { get; private set; }

    public OAuthLoginForm(OAuthProvider provider)
    {
        _provider = provider;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Text = _provider == OAuthProvider.Google ? "使用 Google 登入" : "使用 GitHub 登入";
        ClientSize = new Size(520, 750);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimizeBox = true;
        MinimumSize = new Size(400, 500);
        BackColor = Color.FromArgb(30, 30, 30);

        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScaleDimensions = new SizeF(96F, 96F);

        _webView = new WebView2
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(_webView);

        Load += OAuthLoginForm_Load;
        FormClosing += OAuthLoginForm_FormClosing;

        ResumeLayout(false);
    }

    private async void OAuthLoginForm_Load(object? sender, EventArgs e)
    {
        try
        {
            var userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ShareLock", "WebView2");

            Directory.CreateDirectory(userDataFolder);

            var env = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            if (_webView != null)
            {
                await _webView.EnsureCoreWebView2Async(env);

                _webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
                _webView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                _webView.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;

                var loginUrl = GetLoginUrl();
                _webView.CoreWebView2.Navigate(loginUrl);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"初始化瀏覽器失敗：{ex.Message}\n\n請確保已安裝 Microsoft Edge WebView2 Runtime。",
                "錯誤",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            Result = new OAuthResult { Success = false, Error = ex.Message };
            DialogResult = DialogResult.Abort;
            Close();
        }
    }

    private string GetLoginUrl()
    {
        return $"{FirebaseConfig.WebAppUrl}/login";
    }

    private void CoreWebView2_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        var currentUrl = _webView?.CoreWebView2?.Source ?? "";
        System.Diagnostics.Debug.WriteLine($"URL changed: {currentUrl}");

        if (currentUrl.Contains("/dashboard") && !_loginCompleted)
        {
            _loginCompleted = true;
            _extractAttempts = 0;
            _ = ExtractUserInfoWithRetryAsync();
        }
    }

    private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (!e.IsSuccess)
        {
            System.Diagnostics.Debug.WriteLine($"Navigation failed: {e.WebErrorStatus}");
        }
    }

    private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        try
        {
            var message = e.TryGetWebMessageAsString();
            System.Diagnostics.Debug.WriteLine($"WebMessage received: {message}");

            if (!string.IsNullOrEmpty(message))
            {
                ProcessAuthResult(message);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error processing web message: {ex.Message}");
        }
    }

    private void ProcessAuthResult(string json)
    {
        try
        {
            var authResult = JsonSerializer.Deserialize<AuthResultDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (authResult?.Success == true && !string.IsNullOrEmpty(authResult.IdToken))
            {
                Result = new OAuthResult
                {
                    Success = true,
                    IdToken = authResult.IdToken,
                    RefreshToken = authResult.RefreshToken,
                    UserId = authResult.Uid,
                    Email = authResult.Email,
                    DisplayName = authResult.DisplayName,
                    PhotoUrl = authResult.PhotoUrl
                };

                System.Diagnostics.Debug.WriteLine($"Successfully got user info: {authResult.Email}");

                if (!IsDisposed)
                {
                    Invoke(() =>
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    });
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error parsing auth result: {ex.Message}");
        }
    }

    private async Task ExtractUserInfoWithRetryAsync()
    {
        await Task.Delay(2000);

        while (_extractAttempts < MaxExtractAttempts && !IsDisposed && Result == null)
        {
            _extractAttempts++;
            System.Diagnostics.Debug.WriteLine($"Attempting to extract user info (attempt {_extractAttempts})...");

            await TryExtractUserInfoAsync();

            if (Result != null)
            {
                return;
            }

            await Task.Delay(1000);
        }

        if (!IsDisposed && Result == null)
        {
            Invoke(() =>
            {
                Result = new OAuthResult
                {
                    Success = false,
                    Error = "無法獲取登入資訊，請重試"
                };
                DialogResult = DialogResult.Cancel;
                Close();
            });
        }
    }

    private async Task TryExtractUserInfoAsync()
    {
        try
        {
            if (_webView?.CoreWebView2 == null || IsDisposed) return;

            var listDbScript = @"
                (function() {
                    return new Promise(function(resolve) {
                        if (!indexedDB.databases) {
                            resolve(JSON.stringify({ error: 'databases() not supported' }));
                            return;
                        }
                        indexedDB.databases().then(function(dbs) {
                            resolve(JSON.stringify({ databases: dbs.map(function(d) { return d.name; }) }));
                        }).catch(function(e) {
                            resolve(JSON.stringify({ error: e.message }));
                        });
                    });
                })();
            ";

            var dbListResult = await _webView.CoreWebView2.ExecuteScriptAsync(listDbScript);
            System.Diagnostics.Debug.WriteLine($"IndexedDB list: {dbListResult}");

            var script = GetFirebaseExtractionScript();
            await _webView.CoreWebView2.ExecuteScriptAsync(script);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error extracting user info: {ex.Message}");
        }
    }

    private static string GetFirebaseExtractionScript()
    {

        return @"
(function() {

    var dbName = 'firebaseLocalStorageDb';
    var storeName = 'firebaseLocalStorage';

    var request = indexedDB.open(dbName);

    request.onerror = function(event) {
        console.log('IndexedDB open error');
        window.chrome.webview.postMessage(JSON.stringify({ success: false, error: 'IndexedDB open error' }));
    };

    request.onsuccess = function(event) {
        var db = event.target.result;
        console.log('IndexedDB opened, stores:', Array.from(db.objectStoreNames));

        if (!db.objectStoreNames.contains(storeName)) {
            console.log('Store not found');
            window.chrome.webview.postMessage(JSON.stringify({ success: false, error: 'Store not found' }));
            return;
        }

        var transaction = db.transaction([storeName], 'readonly');
        var store = transaction.objectStore(storeName);
        var getAllRequest = store.getAll();

        getAllRequest.onsuccess = function() {
            var results = getAllRequest.result;
            console.log('IndexedDB results count:', results ? results.length : 0);

            for (var i = 0; i < (results || []).length; i++) {
                var item = results[i];
                console.log('Item key:', item ? item.fbase_key : 'null');

                var value = item ? item.value : null;
                if (value && value.stsTokenManager && value.stsTokenManager.accessToken) {
                    console.log('Found valid auth data!');
                    var result = {
                        success: true,
                        uid: value.uid || '',
                        email: value.email || '',
                        displayName: value.displayName || '',
                        photoUrl: value.photoURL || '',
                        idToken: value.stsTokenManager.accessToken,
                        refreshToken: value.stsTokenManager.refreshToken || ''
                    };
                    window.chrome.webview.postMessage(JSON.stringify(result));
                    return;
                }
            }

            console.log('No valid auth data found in IndexedDB');
            window.chrome.webview.postMessage(JSON.stringify({ success: false, error: 'No auth data in IndexedDB' }));
        };

        getAllRequest.onerror = function() {
            console.log('IndexedDB getAll error');
            window.chrome.webview.postMessage(JSON.stringify({ success: false, error: 'IndexedDB getAll error' }));
        };
    };
})();
";
    }

    private void OAuthLoginForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (Result == null)
        {
            Result = new OAuthResult { Success = false, Error = "使用者取消登入" };
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _webView?.Dispose();
        }
        base.Dispose(disposing);
    }

    private class AuthResultDto
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? Uid { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
