using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;
using sharelock_desktop.Controls;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Forms;
public partial class MainForm : Form
{
    private WinPanel? _sidePanel;
    private WinPanel? _contentPanel;
    private AntdUI.Avatar? _userAvatar;
    private AntdUI.Label? _userNameLabel;
    private AntdUI.Label? _emailLabel;
    private PictureBox? _logoPictureBox;

    private AntdUI.Button? _dashboardMenuItem;
    private AntdUI.Button? _filesMenuItem;
    private AntdUI.Button? _settingsMenuItem;
    private AntdUI.Button? _aboutMenuItem;
    private AntdUI.Button? _logoutMenuItem;

    private DashboardControl? _dashboardControl;
    private FilesControl? _filesControl;
    private SettingsControl? _settingsControl;

    private string _currentPage = "dashboard";

    public MainForm()
    {
        InitializeComponent();
        ThemeManager.ApplyThemeToForm(this);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Text = "ShareLock - 高效安全的檔案分享軟體";
        ClientSize = new Size(1280, 800);
        MinimumSize = new Size(1024, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ThemeManager.BackgroundColor;

        var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharelock.ico");
        if (File.Exists(iconPath))
        {
            Icon = new System.Drawing.Icon(iconPath);
        }

        ControlBox = true;
        MaximizeBox = true;
        MinimizeBox = true;
        FormBorderStyle = FormBorderStyle.Sizable;

        AutoScaleMode = AutoScaleMode.Dpi;
        AutoScaleDimensions = new SizeF(96F, 96F);

        var mainContainer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = ThemeManager.BackgroundColor,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, UIConstants.SidebarWidth));
        mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        _sidePanel = CreateSidePanel();
        mainContainer.Controls.Add(_sidePanel, 0, 0);

        _contentPanel = CreateContentPanel();
        mainContainer.Controls.Add(_contentPanel, 1, 0);

        Controls.Add(mainContainer);

        Load += MainForm_Load;

        ResumeLayout(false);
    }

    private WinPanel CreateSidePanel()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = ThemeManager.SidebarBackgroundColor,
            Padding = new Padding(0)
        };
        UIConstants.EnableDoubleBuffering(panel);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 1,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        var headerPanel = CreateSideHeader();
        layout.Controls.Add(headerPanel, 0, 0);

        var menuPanel = CreateSideMenu();
        layout.Controls.Add(menuPanel, 0, 1);

        var footerPanel = CreateSideFooter();
        layout.Controls.Add(footerPanel, 0, 2);

        panel.Controls.Add(layout);
        return panel;
    }

    private WinPanel CreateSideHeader()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent,
            Padding = new Padding(UIConstants.SidebarPadding)
        };
        UIConstants.EnableDoubleBuffering(panel);

        int logoWidth = UIConstants.LogoWidth;
        int logoHeight = UIConstants.LogoHeight;
        _logoPictureBox = new PictureBox
        {
            Size = new Size(logoWidth, logoHeight),
            Location = new Point((UIConstants.SidebarWidth - logoWidth) / 2, 8),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };
        LoadLogoImage();
        panel.Controls.Add(_logoPictureBox);

        var desktopLabel = new AntdUI.Label
        {
            Text = "桌面版",
            Font = new Font(ThemeManager.FontFamily, 9f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            BackColor = Color.Transparent
        };
        desktopLabel.Location = new Point(_logoPictureBox.Right - 40, _logoPictureBox.Bottom - 32);
        panel.Controls.Add(desktopLabel);
        desktopLabel.BringToFront();

        var session = SessionManager.Instance;
        int userInfoY = _logoPictureBox.Bottom + 12;

        int avatarSize = UIConstants.AvatarSizeSM + 8;
        int textGap = 12;
        int estimatedTextWidth = 160;
        int totalUserInfoWidth = avatarSize + textGap + estimatedTextWidth;
        int userInfoStartX = (UIConstants.SidebarWidth - totalUserInfoWidth) / 2;

        _userAvatar = new AntdUI.Avatar
        {
            Location = new Point(userInfoStartX, userInfoY),
            Round = true,
            Size = new Size(avatarSize, avatarSize)
        };

        if (!string.IsNullOrEmpty(session.PhotoUrl))
        {
            LoadUserAvatar(session.PhotoUrl);
        }
        else
        {
            _userAvatar.Text = GetUserInitial(session);
        }
        panel.Controls.Add(_userAvatar);

        int textStartX = userInfoStartX + avatarSize + textGap;

        _userNameLabel = new AntdUI.Label
        {
            Text = ControlFactory.SafeTruncate(session.DisplayName ?? session.Email ?? "使用者", 18),
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextPrimaryColor,
            Location = new Point(textStartX, userInfoY + 4),
            AutoSize = true,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(_userNameLabel);

        _emailLabel = new AntdUI.Label
        {
            Text = ControlFactory.SafeTruncate(session.Email ?? "", 20),
            Font = new Font(ThemeManager.FontFamily, 9f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            Location = new Point(textStartX, userInfoY + 26),
            AutoSize = true,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(_emailLabel);

        return panel;
    }

    private void LoadLogoImage()
    {
        try
        {
            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.png");
            if (File.Exists(iconPath))
            {
                _logoPictureBox!.Image = Image.FromFile(iconPath);
            }
            else
            {
                var projectIconPath = @"D:\Project\NTOU-CSharp\sharelock-desktop\sharelock-desktop\icon.png";
                if (File.Exists(projectIconPath))
                {
                    _logoPictureBox!.Image = Image.FromFile(projectIconPath);
                }
            }
        }
        catch
        {

        }
    }

    private WinPanel CreateSideMenu()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent,
            Padding = new Padding(0)
        };
        UIConstants.EnableDoubleBuffering(panel);

        int buttonWidth = UIConstants.LogoWidth;
        int menuStartX = (UIConstants.SidebarWidth - buttonWidth) / 2;

        var menuContainer = new FlowLayoutPanel
        {
            Location = new Point(menuStartX, UIConstants.PaddingMD),
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent,
            Padding = new Padding(0)
        };

        _dashboardMenuItem = CreateMenuButton(IconChar.Home, "資訊主頁", "dashboard", true, buttonWidth);
        menuContainer.Controls.Add(_dashboardMenuItem);

        _filesMenuItem = CreateMenuButton(IconChar.Folder, "我的檔案", "files", false, buttonWidth);
        menuContainer.Controls.Add(_filesMenuItem);

        _settingsMenuItem = CreateMenuButton(IconChar.Cog, "帳號設定", "settings", false, buttonWidth);
        menuContainer.Controls.Add(_settingsMenuItem);

        _aboutMenuItem = CreateMenuButton(IconChar.InfoCircle, "d(`･∀･)b", "about", false, buttonWidth);
        menuContainer.Controls.Add(_aboutMenuItem);

        panel.Controls.Add(menuContainer);
        return panel;
    }
    private AntdUI.Button CreateMenuButton(IconChar icon, string text, string pageId, bool isSelected, int buttonWidth)
    {
        int buttonHeight = UIConstants.MenuItemHeight;

        var button = new AntdUI.Button
        {
            Text = "   " + text,
            IconSvg = GetMenuIconSvg(icon),
            Size = new Size(buttonWidth, buttonHeight),
            Margin = new Padding(0, 0, 0, UIConstants.GapSM),
            Tag = pageId,
            Ghost = true,
            BorderWidth = 0,
            Font = new Font(ThemeManager.FontFamily, UIConstants.MenuFontSize, FontStyle.Regular),
            Radius = 8
        };

        UpdateMenuButtonStyle(button, isSelected);
        button.Click += (s, e) => NavigateToPage(pageId);

        return button;
    }
    private static string GetMenuIconSvg(IconChar icon)
    {
        return icon switch
        {
            IconChar.Home => "<svg viewBox=\"0 0 576 512\"><path d=\"M575.8 255.5c0 18-15 32.1-32 32.1h-32l.7 160.2c0 2.7-.2 5.4-.5 8.1V472c0 22.1-17.9 40-40 40H456c-1.1 0-2.2 0-3.3-.1c-1.4 .1-2.8 .1-4.2 .1H416 392c-22.1 0-40-17.9-40-40V376c0-13.3-10.7-24-24-24H248c-13.3 0-24 10.7-24 24v96c0 22.1-17.9 40-40 40H160 128.1c-1.5 0-3-.1-4.5-.2c-1.2 .1-2.4 .2-3.6 .2H104c-22.1 0-40-17.9-40-40V360c0-.9 0-1.9 .1-2.8V287.6H32c-18 0-32-14-32-32.1c0-9 3-17 10-24L266.4 8c7-7 15-8 22-8s15 2 21 7L564.8 231.5c8 7 12 15 11 24z\"/></svg>",
            IconChar.Folder => "<svg viewBox=\"0 0 512 512\"><path d=\"M64 480H448c35.3 0 64-28.7 64-64V160c0-35.3-28.7-64-64-64H288c-10.1 0-19.6-4.7-25.6-12.8L243.2 57.6C231.1 41.5 212.1 32 192 32H64C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64z\"/></svg>",
            IconChar.Cog => "<svg viewBox=\"0 0 512 512\"><path d=\"M495.9 166.6c3.2 8.7 .5 18.4-6.4 24.6l-43.3 39.4c1.1 8.3 1.7 16.8 1.7 25.4s-.6 17.1-1.7 25.4l43.3 39.4c6.9 6.2 9.6 15.9 6.4 24.6c-4.4 11.9-9.7 23.3-15.8 34.3l-4.7 8.1c-6.6 11-14 21.4-22.1 31.2c-5.9 7.2-15.7 9.6-24.5 6.8l-55.7-17.7c-13.4 10.3-28.2 18.9-44 25.4l-12.5 57.1c-2 9.1-9 16.3-18.2 17.8c-13.8 2.3-28 3.5-42.5 3.5s-28.7-1.2-42.5-3.5c-9.2-1.5-16.2-8.7-18.2-17.8l-12.5-57.1c-15.8-6.5-30.6-15.1-44-25.4L83.1 425.9c-8.8 2.8-18.6 .3-24.5-6.8c-8.1-9.8-15.5-20.2-22.1-31.2l-4.7-8.1c-6.1-11-11.4-22.4-15.8-34.3c-3.2-8.7-.5-18.4 6.4-24.6l43.3-39.4C64.6 273.1 64 264.6 64 256s.6-17.1 1.7-25.4L22.4 191.2c-6.9-6.2-9.6-15.9-6.4-24.6c4.4-11.9 9.7-23.3 15.8-34.3l4.7-8.1c6.6-11 14-21.4 22.1-31.2c5.9-7.2 15.7-9.6 24.5-6.8l55.7 17.7c13.4-10.3 28.2-18.9 44-25.4l12.5-57.1c2-9.1 9-16.3 18.2-17.8C227.3 1.2 241.5 0 256 0s28.7 1.2 42.5 3.5c9.2 1.5 16.2 8.7 18.2 17.8l12.5 57.1c15.8 6.5 30.6 15.1 44 25.4l55.7-17.7c8.8-2.8 18.6-.3 24.5 6.8c8.1 9.8 15.5 20.2 22.1 31.2l4.7 8.1c6.1 11 11.4 22.4 15.8 34.3zM256 336a80 80 0 1 0 0-160 80 80 0 1 0 0 160z\"/></svg>",
            IconChar.InfoCircle => "<svg viewBox=\"0 0 416 416\"><path d=\"M356.004,61.156c-81.37-81.47-213.377-81.551-294.848-0.182c-81.47,81.371-81.552,213.379-0.181,294.85c81.369,81.47,213.378,81.551,294.849,0.181C437.293,274.636,437.375,142.626,356.004,61.156z M237.6,340.786c0,3.217-2.607,5.822-5.822,5.822h-46.576c-3.215,0-5.822-2.605-5.822-5.822V167.885c0-3.217,2.607-5.822,5.822-5.822h46.576c3.215,0,5.822,2.604,5.822,5.822V340.786z M208.49,137.901c-18.618,0-33.766-15.146-33.766-33.765c0-18.617,15.147-33.766,33.766-33.766c18.619,0,33.766,15.148,33.766,33.766C242.256,122.755,227.107,137.901,208.49,137.901z\"/></svg>",
            IconChar.SignOutAlt => "<svg viewBox=\"0 0 512 512\"><path d=\"M377.9 105.9L500.7 228.7c7.2 7.2 11.3 17.1 11.3 27.3s-4.1 20.1-11.3 27.3L377.9 406.1c-6.4 6.4-15 9.9-24 9.9c-18.7 0-33.9-15.2-33.9-33.9l0-62.1-128 0c-17.7 0-32-14.3-32-32l0-64c0-17.7 14.3-32 32-32l128 0 0-62.1c0-18.7 15.2-33.9 33.9-33.9c9 0 17.6 3.6 24 9.9zM160 96L96 96c-17.7 0-32 14.3-32 32l0 256c0 17.7 14.3 32 32 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32l-64 0c-53 0-96-43-96-96L0 128C0 75 43 32 96 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32z\"/></svg>",
            _ => ""
        };
    }
    private static void UpdateMenuButtonStyle(AntdUI.Button? button, bool isSelected)
    {
        if (button == null) return;

        if (isSelected)
        {
            button.Type = TTypeMini.Primary;
            button.Ghost = false;
            button.ForeColor = Color.White;
        }
        else
        {
            button.Type = TTypeMini.Default;
            button.Ghost = true;
            button.ForeColor = ThemeManager.TextPrimaryColor;
        }
    }

    private void UpdateMenuSelection(string pageId)
    {
        UpdateMenuButtonStyle(_dashboardMenuItem, pageId == "dashboard");
        UpdateMenuButtonStyle(_filesMenuItem, pageId == "files");
        UpdateMenuButtonStyle(_settingsMenuItem, pageId == "settings");
        UpdateMenuButtonStyle(_aboutMenuItem, pageId == "about");
    }

    private WinPanel CreateSideFooter()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.Transparent,
            Padding = new Padding(0)
        };
        UIConstants.EnableDoubleBuffering(panel);

        int buttonWidth = UIConstants.LogoWidth;
        int buttonStartX = (UIConstants.SidebarWidth - buttonWidth) / 2;

        var linksContainer = new FlowLayoutPanel
        {
            AutoSize = true,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            BackColor = Color.Transparent,
            Padding = new Padding(0)
        };

        var webButton = CreateLinkButton("開啟線上版", $"{FirebaseConfig.WebAppUrl}", buttonWidth);
        linksContainer.Controls.Add(webButton);

        var privacyButton = CreateLinkButton("隱私權政策", $"{FirebaseConfig.WebAppUrl}/privacy-policy", buttonWidth);
        linksContainer.Controls.Add(privacyButton);

        var termsButton = CreateLinkButton("服務條款", $"{FirebaseConfig.WebAppUrl}/terms-of-service", buttonWidth);
        linksContainer.Controls.Add(termsButton);

        panel.Controls.Add(linksContainer);

        _logoutMenuItem = new AntdUI.Button
        {
            Text = "  登出",
            IconSvg = GetMenuIconSvg(IconChar.SignOutAlt),
            Size = new Size(buttonWidth, UIConstants.MenuItemHeight),
            BorderWidth = 0,
            Font = new Font(ThemeManager.FontFamily, UIConstants.MenuFontSize, FontStyle.Regular),
            Radius = 8,
            Type = TTypeMini.Error,
            Ghost = false
        };
        _logoutMenuItem.Click += LogoutButton_Click;
        panel.Controls.Add(_logoutMenuItem);

        panel.Resize += (s, e) =>
        {
            if (_logoutMenuItem != null && linksContainer != null)
            {

                int logoutX = (panel.Width - _logoutMenuItem.Width) / 2;
                int logoutY = panel.Height - _logoutMenuItem.Height - 10;
                _logoutMenuItem.Location = new Point(Math.Max(0, logoutX), Math.Max(0, logoutY));

                int linksX = (panel.Width - linksContainer.Width) / 2;
                int linksY = logoutY - linksContainer.Height - 8;
                linksContainer.Location = new Point(Math.Max(0, linksX), Math.Max(0, linksY));
            }
        };

        return panel;
    }

    private AntdUI.Button CreateLinkButton(string text, string url, int width)
    {
        var button = new AntdUI.Button
        {
            Text = text,
            Size = new Size(width, 32),
            Margin = new Padding(0, 0, 0, 4),
            Ghost = true,
            BorderWidth = 0,
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            Radius = 6
        };

        button.Click += (s, e) =>
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        };

        return button;
    }

    private WinPanel CreateContentPanel()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = ThemeManager.BackgroundColor,
            Padding = UIConstants.PagePadding
        };
        UIConstants.EnableDoubleBuffering(panel);
        return panel;
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        if (!SessionManager.Instance.IsLoggedIn)
        {
            ShowLoginForm();
            return;
        }

        ServiceManager.Instance.SetAuthToken(SessionManager.Instance.IdToken);
        NavigateToPage("dashboard");
    }

    private void NavigateToPage(string pageName)
    {

        if (pageName == "about")
        {
            MessageBox.Show("Share Lock Desktop v1.0.0\n\nNTOU CS2B 01357101\nYI HONG, HUANG\n\nThis Windows Form App is the Final Project for the course,\n" + "\"Desktop Application Development\" (B53025QU).\n\nAuthor : github/whitebear13579\nApp Licensed to :\n                             d(`･∀･)b.\r\n", "關於Share Lock 桌面版", MessageBoxButtons.OK);
            return;
        }

        if (_currentPage == pageName && _contentPanel?.Controls.Count > 0)
            return;

        _currentPage = pageName;
        _contentPanel?.Controls.Clear();

        UpdateMenuSelection(pageName);

        UserControl? pageControl = pageName switch
        {
            "dashboard" => _dashboardControl ??= new DashboardControl(),
            "files" => _filesControl ??= new FilesControl(),
            "settings" => _settingsControl ??= new SettingsControl(),
            _ => null
        };

        if (pageControl != null)
        {
            pageControl.Dock = DockStyle.Fill;
            _contentPanel?.Controls.Add(pageControl);
        }
    }

    private async void LogoutButton_Click(object? sender, EventArgs e)
    {
        var result = MessageBox.Show("你確定要登出嗎？", "バイバイ",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            try
            {
                await ServiceManager.Instance.Auth.LogoutAsync();
            }
            catch
            {

            }

            SessionManager.Instance.ClearSession();
            ServiceManager.Instance.ClearAuth();
            ShowLoginForm();
        }
    }

    private void ShowLoginForm()
    {
        Hide();

        using var loginForm = new LoginForm();
        var result = loginForm.ShowDialog();

        if (result == DialogResult.OK)
        {
            UpdateUserInfo();
            ServiceManager.Instance.SetAuthToken(SessionManager.Instance.IdToken);

            _dashboardControl?.Dispose();
            _dashboardControl = null;
            _filesControl?.Dispose();
            _filesControl = null;
            _settingsControl?.Dispose();
            _settingsControl = null;

            NavigateToPage("dashboard");
            Show();
        }
        else
        {
            Application.Exit();
        }
    }
    public void SwitchToLoginForm()
    {

        _dashboardControl?.Dispose();
        _dashboardControl = null;
        _filesControl?.Dispose();
        _filesControl = null;
        _settingsControl?.Dispose();
        _settingsControl = null;

        _contentPanel?.Controls.Clear();

        SessionManager.Instance.ClearSession();
        ServiceManager.Instance.ClearAuth();

        ShowLoginForm();
    }

    private void UpdateUserInfo()
    {
        var session = SessionManager.Instance;

        if (_userNameLabel != null)
        {
            _userNameLabel.Text = ControlFactory.SafeTruncate(session.DisplayName ?? session.Email ?? "使用者", 18);
        }

        if (_emailLabel != null)
        {
            _emailLabel.Text = ControlFactory.SafeTruncate(session.Email ?? "", 20);
        }

        if (_userAvatar != null)
        {
            if (!string.IsNullOrEmpty(session.PhotoUrl))
            {
                LoadUserAvatar(session.PhotoUrl);
            }
            else
            {
                _userAvatar.Text = GetUserInitial(session);
                _userAvatar.Image = null;
            }
        }
    }

    private static string GetUserInitial(SessionManager session)
    {
        var name = session.DisplayName ?? session.Email ?? "U";
        return name.Length > 0 ? name[..1].ToUpper() : "U";
    }

    private async void LoadUserAvatar(string url)
    {
        try
        {
            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(url);
            using var ms = new MemoryStream(imageBytes);
            var image = Image.FromStream(ms);

            if (_userAvatar != null && !_userAvatar.IsDisposed)
            {
                Invoke(() =>
                {
                    _userAvatar.Image = image;
                });
            }
        }
        catch
        {
            if (_userAvatar != null && !_userAvatar.IsDisposed)
            {
                _userAvatar.Text = GetUserInitial(SessionManager.Instance);
            }
        }
    }
}
