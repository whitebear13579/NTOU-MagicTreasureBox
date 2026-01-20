namespace sharelock_desktop.Utils;
public static class FirebaseConfig
{

    public const string ApiKey = "";
    public const string AuthDomain = "";
    public const string ProjectId = "";
    public const string StorageBucket = "";
    public const string MessagingSenderId = "";
    public const string AppId = "";

    public const string WebAppUrl = "https://www.sharelock.qzz.io";
    public const string ApiBaseUrl = "https://www.sharelock.qzz.io/api";

    public const string OAuthLoginUrl = "https://www.sharelock.qzz.io/login";

    public const string RedirectUri = "http://localhost:8765/callback";
}
public enum OAuthProvider
{
    Google,
    GitHub
}
