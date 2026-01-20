using sharelock_desktop.Forms;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

namespace sharelock_desktop;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ThemeManager.InitializeDarkTheme();
        Application.SetDefaultFont(new Font("Microsoft JhengHei UI", UIConstants.FontSizeSM, FontStyle.Regular));
        var session = SessionManager.Instance;
        if (session.IsLoggedIn)
        {
            ServiceManager.Instance.SetAuthToken(session.IdToken);
            Application.Run(new MainForm());
        }
        else
        {
            using var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                ServiceManager.Instance.SetAuthToken(session.IdToken);
                Application.Run(new MainForm());
            }
        }
    }
}