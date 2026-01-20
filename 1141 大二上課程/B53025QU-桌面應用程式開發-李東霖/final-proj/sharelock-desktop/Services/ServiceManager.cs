namespace sharelock_desktop.Services;
public class ServiceManager : IDisposable
{
    private static ServiceManager? _instance;
    private static readonly object _lock = new();

    private readonly ApiClient _apiClient;

    public AuthService Auth { get; }
    public FileService Files { get; }
    public ShareService Share { get; }
    public NotificationService Notifications { get; }
    public StorageService Storage { get; }
    public DownloadService Download { get; }
    public StatisticsService Statistics { get; }
    public UploadService Upload { get; }
    public UserService User { get; }
    public FirebaseAuthService FirebaseAuth { get; }

    public static ServiceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new ServiceManager();
                }
            }
            return _instance;
        }
    }

    private ServiceManager()
    {
        _apiClient = new ApiClient();

        Auth = new AuthService(_apiClient);
        Files = new FileService(_apiClient);
        Share = new ShareService(_apiClient);
        Notifications = new NotificationService(_apiClient);
        Storage = new StorageService(_apiClient);
        Download = new DownloadService(_apiClient);
        Statistics = new StatisticsService(_apiClient);
        Upload = new UploadService(_apiClient);
        User = new UserService(_apiClient);
        FirebaseAuth = new FirebaseAuthService();

        var session = SessionManager.Instance;
        if (session.IsLoggedIn && !string.IsNullOrEmpty(session.IdToken))
        {
            _apiClient.SetAuthToken(session.IdToken);
        }
    }
    public void SetAuthToken(string? token)
    {
        _apiClient.SetAuthToken(token);
    }
    public void ClearAuth()
    {
        _apiClient.SetAuthToken(null);
    }

    public void Dispose()
    {
        _apiClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
