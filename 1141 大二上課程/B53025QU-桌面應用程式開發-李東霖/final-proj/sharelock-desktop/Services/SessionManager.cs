using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace sharelock_desktop.Services;
public class SessionManager
{
    private static SessionManager? _instance;
    private static readonly object _lock = new();

    private string? _idToken;
    private string? _refreshToken;
    private string? _userId;
    private string? _email;
    private string? _displayName;
    private string? _photoUrl;
    private DateTime? _tokenExpiry;

    private const string SessionFileName = "session.dat";

    public static SessionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new SessionManager();
                }
            }
            return _instance;
        }
    }

    private SessionManager()
    {
        LoadSession();
    }
    public bool IsLoggedIn => !string.IsNullOrEmpty(_idToken) && !string.IsNullOrEmpty(_userId);
    public string? UserId => _userId;
    public string? Email => _email;
    public string? DisplayName => _displayName;
    public string? PhotoUrl => _photoUrl;
    public string? IdToken => _idToken;
    public bool IsTokenExpired => _tokenExpiry.HasValue && DateTime.UtcNow >= _tokenExpiry.Value;
    public void SetSession(string idToken, string? refreshToken, string userId,
        string? email, string? displayName, string? photoUrl, DateTime? tokenExpiry = null)
    {
        _idToken = idToken;
        _refreshToken = refreshToken;
        _userId = userId;
        _email = email;
        _displayName = displayName;
        _photoUrl = photoUrl;
        _tokenExpiry = tokenExpiry ?? DateTime.UtcNow.AddHours(1);

        SaveSession();
    }
    public void UpdateToken(string idToken, string? refreshToken = null, DateTime? tokenExpiry = null)
    {
        _idToken = idToken;
        if (refreshToken != null)
        {
            _refreshToken = refreshToken;
        }
        _tokenExpiry = tokenExpiry ?? DateTime.UtcNow.AddHours(1);
        SaveSession();
    }
    public void UpdateDisplayName(string? displayName)
    {
        _displayName = displayName;
        SaveSession();
    }
    public void UpdateEmail(string? email)
    {
        _email = email;
        SaveSession();
    }
    public void ClearSession()
    {
        _idToken = null;
        _refreshToken = null;
        _userId = null;
        _email = null;
        _displayName = null;
        _photoUrl = null;
        _tokenExpiry = null;

        DeleteSessionFile();
    }
    private void SaveSession()
    {
        try
        {
            var sessionData = new SessionData
            {
                IdToken = _idToken,
                RefreshToken = _refreshToken,
                UserId = _userId,
                Email = _email,
                DisplayName = _displayName,
                PhotoUrl = _photoUrl,
                TokenExpiry = _tokenExpiry
            };

            var json = JsonConvert.SerializeObject(sessionData);
            var encrypted = ProtectData(json);

            var appDataPath = GetAppDataPath();
            Directory.CreateDirectory(appDataPath);
            var filePath = Path.Combine(appDataPath, SessionFileName);
            File.WriteAllBytes(filePath, encrypted);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"儲存會話失敗: {ex.Message}");
        }
    }
    private void LoadSession()
    {
        try
        {
            var filePath = Path.Combine(GetAppDataPath(), SessionFileName);
            if (!File.Exists(filePath)) return;

            var encrypted = File.ReadAllBytes(filePath);
            var json = UnprotectData(encrypted);

            if (string.IsNullOrEmpty(json)) return;

            var sessionData = JsonConvert.DeserializeObject<SessionData>(json);
            if (sessionData == null) return;

            _idToken = sessionData.IdToken;
            _refreshToken = sessionData.RefreshToken;
            _userId = sessionData.UserId;
            _email = sessionData.Email;
            _displayName = sessionData.DisplayName;
            _photoUrl = sessionData.PhotoUrl;
            _tokenExpiry = sessionData.TokenExpiry;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"載入會話失敗: {ex.Message}");
            DeleteSessionFile();
        }
    }
    private void DeleteSessionFile()
    {
        try
        {
            var filePath = Path.Combine(GetAppDataPath(), SessionFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch
        {

        }
    }
    private static string GetAppDataPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(appData, "ShareLock");
    }
    private static byte[] ProtectData(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        return ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
    }
    private static string UnprotectData(byte[] data)
    {
        try
        {
            var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decrypted);
        }
        catch
        {
            return string.Empty;
        }
    }
    private class SessionData
    {
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
