using AntdUI;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Forms;
public partial class LoginForm : Form
{
    private AntdUI.Input? _emailInput;
    private AntdUI.Input? _passwordInput;
    private AntdUI.Button? _loginButton;
    private AntdUI.Button? _googleButton;
    private AntdUI.Button? _githubButton;
    private AntdUI.Label? _errorLabel;
    private AntdUI.Button? _signupButton;
    private AntdUI.Button? _forgotPasswordButton;
    private PictureBox? _logoPictureBox;

    private bool _isLoading;
    private readonly FirebaseAuthService _firebaseAuth;
    private const int DesignWidth = 550;
    private const int DesignHeight = 850;
    private const int ContentWidth = 440;

    public LoginForm()
    {
        _firebaseAuth = new FirebaseAuthService();
        InitializeComponent();
        ThemeManager.ApplyThemeToForm(this);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Text = "ShareLock - 登入";
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = ThemeManager.BackgroundColor;

        var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharelock.ico");
        if (File.Exists(iconPath))
        {
            Icon = new Icon(iconPath);
        }

        ControlBox = true;
        MaximizeBox = false;
        MinimizeBox = true;
        FormBorderStyle = FormBorderStyle.FixedSingle;

        AutoScaleMode = AutoScaleMode.None;
        ClientSize = new Size(DesignWidth, DesignHeight);

        int startX = (DesignWidth - ContentWidth) / 2;
        int currentY = 20;

        int logoWidth = 380;
        int logoHeight = 180;
        _logoPictureBox = new PictureBox
        {
            Size = new Size(logoWidth, logoHeight),
            Location = new Point((DesignWidth - logoWidth) / 2, currentY-20),
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };
        LoadLogoImage();
        Controls.Add(_logoPictureBox);

        var desktopLabel = CreateLabel("桌面版", 10, ThemeManager.TextSecondaryColor);
        desktopLabel.Size = new Size(60, 24);
        desktopLabel.Location = new Point(_logoPictureBox.Right - 60, _logoPictureBox.Bottom - 52);
        Controls.Add(desktopLabel);
        desktopLabel.BringToFront();

        currentY = _logoPictureBox.Bottom;

        var titleLabel = CreateLabel("登入你的帳號", 20, ThemeManager.TextPrimaryColor, FontStyle.Bold);
        titleLabel.AutoSize = false;
        titleLabel.Size = new Size(DesignWidth, 50);
        titleLabel.Location = new Point(0, currentY);
        titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(titleLabel);
        currentY += 65;

        _errorLabel = CreateLabel("", 10, ThemeManager.ErrorColor);
        _errorLabel.AutoSize = false;
        _errorLabel.Size = new Size(ContentWidth, 28);
        _errorLabel.Location = new Point(startX, currentY);
        _errorLabel.TextAlign = ContentAlignment.MiddleCenter;
        _errorLabel.Visible = false;
        Controls.Add(_errorLabel);
        currentY += 32;

        var emailLabel = CreateLabel("電子郵件", 11, ThemeManager.TextPrimaryColor);
        emailLabel.AutoSize = false;
        emailLabel.Size = new Size(ContentWidth, 28);
        emailLabel.Location = new Point(startX + 4, currentY);
        Controls.Add(emailLabel);
        currentY += 32;

        _emailInput = new AntdUI.Input
        {
            PlaceholderText = "輸入電子郵件",
            Size = new Size(ContentWidth, 48),
            Location = new Point(startX, currentY+8),
        };
        _emailInput.KeyDown += Input_KeyDown;
        Controls.Add(_emailInput);
        currentY += 65;

        var passwordLabel = CreateLabel("密碼", 11, ThemeManager.TextPrimaryColor);
        passwordLabel.AutoSize = false;
        passwordLabel.Size = new Size(ContentWidth, 28);
        passwordLabel.Location = new Point(startX + 4, currentY);
        Controls.Add(passwordLabel);
        currentY += 32;

        _passwordInput = new AntdUI.Input
        {
            PlaceholderText = "輸入密碼",
            Size = new Size(ContentWidth, 48),
            Location = new Point(startX, currentY+8),
            UseSystemPasswordChar = true,
        };
        _passwordInput.KeyDown += Input_KeyDown;
        Controls.Add(_passwordInput);
        currentY += 58;

        _forgotPasswordButton = new AntdUI.Button
        {
            Text = "忘記密碼？",
            Type = TTypeMini.Default,
            Ghost = true,
            BorderWidth = 0,
            ForeColor = ThemeManager.PrimaryColor,
            Size = new Size(120, 40),
            Location = new Point(startX + ContentWidth - 120, currentY)
        };
        _forgotPasswordButton.Click += ForgotPasswordButton_Click;
        Controls.Add(_forgotPasswordButton);
        currentY += 50;

        _loginButton = new AntdUI.Button
        {
            Text = "登入",
            Type = TTypeMini.Primary,
            Size = new Size(ContentWidth, 52),
            Location = new Point(startX, currentY)
        };
        _loginButton.Click += LoginButton_Click;
        Controls.Add(_loginButton);
        currentY += 72;

        int lineWidth = (ContentWidth - 50) / 2;

        var leftLine = new WinPanel
        {
            Size = new Size(lineWidth, 1),
            Location = new Point(startX, currentY + 14),
            BackColor = ThemeManager.BorderColor
        };
        Controls.Add(leftLine);

        var orLabel = CreateLabel("或者", 10, ThemeManager.TextSecondaryColor);
        orLabel.AutoSize = false;
        orLabel.Size = new Size(50, 30);
        orLabel.Location = new Point(startX + lineWidth, currentY);
        orLabel.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(orLabel);

        var rightLine = new WinPanel
        {
            Size = new Size(lineWidth, 1),
            Location = new Point(startX + lineWidth + 50, currentY + 14),
            BackColor = ThemeManager.BorderColor
        };
        Controls.Add(rightLine);
        currentY += 45;

        int oauthButtonWidth = (ContentWidth - 16) / 2;

        _googleButton = new AntdUI.Button
        {
            Text = "　Google 登入",
            Type = TTypeMini.Default,
            Size = new Size(oauthButtonWidth, 48),
            Location = new Point(startX, currentY),
            IconSvg = IconHelper.GoogleSvg
        };
        _googleButton.Click += GoogleButton_Click;
        Controls.Add(_googleButton);

        _githubButton = new AntdUI.Button
        {
            Text = "　GitHub 登入",
            Type = TTypeMini.Default,
            Size = new Size(oauthButtonWidth, 48),
            Location = new Point(startX + oauthButtonWidth + 16, currentY),
            IconSvg = IconHelper.GitHubSvg
        };
        _githubButton.Click += GitHubButton_Click;
        Controls.Add(_githubButton);
        currentY += 72;

        var noAccountLabel = CreateLabel("還沒有帳號？", 11, ThemeManager.TextSecondaryColor);
        noAccountLabel.AutoSize = false;
        noAccountLabel.Size = new Size(120, 40);
        int totalWidth = 230;
        int centerStartX = startX + (ContentWidth - totalWidth) / 2;
        noAccountLabel.Location = new Point(centerStartX, currentY);
        noAccountLabel.TextAlign = ContentAlignment.MiddleLeft;
        Controls.Add(noAccountLabel);

        _signupButton = new AntdUI.Button
        {
            Text = "立即註冊",
            Type = TTypeMini.Default,
            Ghost = true,
            BorderWidth = 0,
            ForeColor = ThemeManager.PrimaryColor,
            Size = new Size(120, 40),
            Location = new Point(centerStartX + 120, currentY)
        };
        _signupButton.Click += SignupButton_Click;
        Controls.Add(_signupButton);
        currentY += 60;

        var copyrightLabel = CreateLabel("© 2025 ShareLock. All Rights Reserved.", 9, ThemeManager.TextSecondaryColor);
        copyrightLabel.AutoSize = false;
        copyrightLabel.Size = new Size(DesignWidth, 28);
        copyrightLabel.Location = new Point(0, currentY + 56);
        copyrightLabel.TextAlign = ContentAlignment.MiddleCenter;
        Controls.Add(copyrightLabel);

        ResumeLayout(false);
    }
    private AntdUI.Label CreateLabel(string text, float fontSize, Color foreColor, FontStyle fontStyle = FontStyle.Regular)
    {
        return new AntdUI.Label
        {
            Text = text,
            Font = new Font(ThemeManager.FontFamily, fontSize, fontStyle),
            ForeColor = foreColor,
            AutoSize = true
        };
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

    private void Input_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && !_isLoading)
        {
            e.SuppressKeyPress = true;
            LoginButton_Click(sender, e);
        }
    }

    private async void LoginButton_Click(object? sender, EventArgs e)
    {
        if (_isLoading) return;

        var email = _emailInput?.Text?.Trim() ?? "";
        var password = _passwordInput?.Text ?? "";

        if (string.IsNullOrEmpty(email))
        {
            ShowError("請輸入電子郵件");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("請輸入密碼");
            return;
        }

        SetLoading(true);
        HideError();

        try
        {
            var result = await _firebaseAuth.SignInWithEmailPasswordAsync(email, password);

            if (result.Success && !string.IsNullOrEmpty(result.IdToken))
            {
                var userInfo = await _firebaseAuth.GetUserInfoAsync(result.IdToken);

                SessionManager.Instance.SetSession(
                    result.IdToken,
                    result.RefreshToken,
                    result.UserId,
                    result.Email ?? userInfo?.Email,
                    result.DisplayName ?? userInfo?.DisplayName,
                    userInfo?.PhotoUrl
                );

                try
                {
                    ServiceManager.Instance.SetAuthToken(result.IdToken);
                    await ServiceManager.Instance.Auth.CreateSessionAsync(result.IdToken);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"建立伺服器會話失敗（不影響登入）: {ex.Message}");
                }

                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            ShowError(result.Error ?? "登入失敗，請重試");
        }
        catch (Exception ex)
        {
            ShowError($"登入失敗：{ex.Message}");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private async void GoogleButton_Click(object? sender, EventArgs e)
    {
        await StartOAuthLoginAsync(OAuthProvider.Google);
    }

    private async void GitHubButton_Click(object? sender, EventArgs e)
    {
        await StartOAuthLoginAsync(OAuthProvider.GitHub);
    }

    private async Task StartOAuthLoginAsync(OAuthProvider provider)
    {
        if (_isLoading) return;

        SetLoading(true);
        HideError();

        try
        {
            using var oauthForm = new OAuthLoginForm(provider);
            var dialogResult = oauthForm.ShowDialog(this);

            if (dialogResult == DialogResult.OK && oauthForm.Result?.Success == true)
            {
                var result = oauthForm.Result;

                if (string.IsNullOrEmpty(result.IdToken))
                {
                    ShowError("登入失敗：無法獲取認證 Token");
                    return;
                }

                SessionManager.Instance.SetSession(
                    result.IdToken,
                    result.RefreshToken,
                    result.UserId,
                    result.Email,
                    result.DisplayName,
                    result.PhotoUrl
                );

                try
                {
                    ServiceManager.Instance.SetAuthToken(result.IdToken);
                    await ServiceManager.Instance.Auth.CreateSessionAsync(result.IdToken);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"建立伺服器會話失敗（不影響登入）: {ex.Message}");
                }

                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (dialogResult == DialogResult.Abort)
            {
                ShowError(oauthForm.Result?.Error ?? "瀏覽器初始化失敗");
            }
            else if (oauthForm.Result?.Error != null && oauthForm.Result.Error != "使用者取消登入")
            {
                ShowError(oauthForm.Result.Error);
            }
        }
        catch (Exception ex)
        {
            ShowError($"登入失敗：{ex.Message}");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private void SignupButton_Click(object? sender, EventArgs e)
    {
        var url = $"{FirebaseConfig.WebAppUrl}/signup";
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
        {
            UseShellExecute = true
        });
    }

    private async void ForgotPasswordButton_Click(object? sender, EventArgs e)
    {
        var email = _emailInput?.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email))
        {
            ShowError("請先輸入電子郵件地址");
            return;
        }

        SetLoading(true);
        HideError();

        try
        {
            var result = await _firebaseAuth.SendPasswordResetEmailAsync(email);

            if (result.Success)
            {
                ShowSuccess("密碼重設信已發送至您的信箱");
            }
            else
            {
                ShowError(result.Error ?? "發送失敗，請稍後再試");
            }
        }
        catch (Exception ex)
        {
            ShowError($"發送失敗：{ex.Message}");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private void SetLoading(bool isLoading)
    {
        _isLoading = isLoading;

        if (_loginButton != null)
        {
            _loginButton.Loading = isLoading;
            _loginButton.Enabled = !isLoading;
        }

        if (_googleButton != null)
            _googleButton.Enabled = !isLoading;

        if (_githubButton != null)
            _githubButton.Enabled = !isLoading;

        if (_emailInput != null)
            _emailInput.Enabled = !isLoading;

        if (_passwordInput != null)
            _passwordInput.Enabled = !isLoading;
    }

    private void ShowError(string message)
    {
        if (_errorLabel != null)
        {
            _errorLabel.ForeColor = ThemeManager.ErrorColor;
            _errorLabel.Text = message;
            _errorLabel.Visible = true;
        }
    }

    private void ShowSuccess(string message)
    {
        if (_errorLabel != null)
        {
            _errorLabel.ForeColor = ThemeManager.SuccessColor;
            _errorLabel.Text = message;
            _errorLabel.Visible = true;
        }
    }

    private void HideError()
    {
        if (_errorLabel != null)
        {
            _errorLabel.Visible = false;
        }
    }
}
