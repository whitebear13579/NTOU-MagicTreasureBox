using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Forms;

public class ChangeEmailForm : Form
{
    private AntdUI.Input? _newEmailInput;
    private AntdUI.Input? _passwordInput;
    private AntdUI.Button? _confirmButton;
    private AntdUI.Button? _cancelButton;
    private AntdUI.Label? _errorLabel;

    private bool _isProcessing;

    public ChangeEmailForm()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.OptimizedDoubleBuffer, true);
        UpdateStyles();

        InitializeComponent();
        ThemeManager.ApplyThemeToForm(this);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Text = "變更電子郵件";
        Size = new Size(450, 430);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = ThemeManager.BackgroundColor;
        Padding = new Padding(UIConstants.PaddingXL);

        var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharelock.ico");
        if (File.Exists(iconPath))
        {
            Icon = new System.Drawing.Icon(iconPath);
        }

        BuildUI();

        ResumeLayout(false);
    }

    private void BuildUI()
    {
        int padding = UIConstants.PaddingXL;
        int contentWidth = ClientSize.Width - padding * 2;
        int currentY = padding;

        var titlePanel = new WinPanel
        {
            Location = new Point(padding, currentY),
            Size = new Size(contentWidth, 40),
            BackColor = Color.Transparent
        };

        var titleLabel = new AntdUI.Label
        {
            Text = "變更電子郵件",
            Font = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(0, 6),
            BackColor = Color.Transparent
        };
        titlePanel.Controls.Add(titleLabel);
        Controls.Add(titlePanel);
        currentY += 50;

        var descLabel = new AntdUI.Label
        {
            Text = "輸入新的電子郵件地址和您的密碼進行驗證",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(descLabel);
        currentY += 30;

        _errorLabel = new AntdUI.Label
        {
            Text = "",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.ErrorColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent,
            Visible = false
        };
        Controls.Add(_errorLabel);
        currentY += 35;

        var emailLabel = new AntdUI.Label
        {
            Text = "新的電子郵件",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(emailLabel);
        currentY += 30;

        _newEmailInput = new AntdUI.Input
        {
            PlaceholderText = "輸入新的電子郵件",
            Size = new Size(contentWidth, 44),
            Location = new Point(padding, currentY)
        };
        Controls.Add(_newEmailInput);
        currentY += 55;

        var passwordLabel = new AntdUI.Label
        {
            Text = "密碼驗證",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(passwordLabel);
        currentY += 30;

        _passwordInput = new AntdUI.Input
        {
            PlaceholderText = "輸入您的密碼",
            Size = new Size(contentWidth, 44),
            Location = new Point(padding, currentY),
            UseSystemPasswordChar = true
        };
        Controls.Add(_passwordInput);

        int buttonY = ClientSize.Height - 70;

        _cancelButton = new AntdUI.Button
        {
            Text = "取消",
            Type = TTypeMini.Default,
            Size = new Size(100, 44),
            Location = new Point(padding, buttonY)
        };
        _cancelButton.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };
        Controls.Add(_cancelButton);

        _confirmButton = new AntdUI.Button
        {
            Text = "確認變更",
            Type = TTypeMini.Primary,
            Size = new Size(120, 44),
            Location = new Point(ClientSize.Width - padding - 120, buttonY)
        };
        _confirmButton.Click += ConfirmButton_Click;
        Controls.Add(_confirmButton);
    }

    private async void ConfirmButton_Click(object? sender, EventArgs e)
    {
        if (_isProcessing) return;

        var newEmail = _newEmailInput?.Text?.Trim();
        var password = _passwordInput?.Text;

        if (string.IsNullOrEmpty(newEmail))
        {
            ShowError("請輸入新的電子郵件地址");
            return;
        }

        if (!IsValidEmail(newEmail))
        {
            ShowError("電子郵件格式不正確");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("請輸入密碼");
            return;
        }

        var session = SessionManager.Instance;
        if (newEmail == session.Email)
        {
            ShowError("新電子郵件與目前相同");
            return;
        }

        _isProcessing = true;
        _confirmButton!.Loading = true;
        _confirmButton.Enabled = false;
        _cancelButton!.Enabled = false;
        HideError();

        try
        {
            var result = await UpdateEmailAsync(newEmail, password);

            if (result.Success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                ShowError(result.Error ?? "變更失敗");
            }
        }
        catch (Exception ex)
        {
            ShowError($"變更失敗：{ex.Message}");
        }
        finally
        {
            _isProcessing = false;
            _confirmButton!.Loading = false;
            _confirmButton.Enabled = true;
            _cancelButton!.Enabled = true;
        }
    }

    private async Task<EmailUpdateResult> UpdateEmailAsync(string newEmail, string password)
    {
        try
        {
            var session = SessionManager.Instance;

            var reAuthResult = await ServiceManager.Instance.FirebaseAuth.SignInWithEmailPasswordAsync(
                session.Email!, password);

            if (!reAuthResult.Success)
            {
                return new EmailUpdateResult
                {
                    Success = false,
                    Error = reAuthResult.Error ?? "密碼驗證失敗"
                };
            }

            var updateResult = await ServiceManager.Instance.FirebaseAuth.UpdateEmailAsync(
                reAuthResult.IdToken!, newEmail);

            if (updateResult.Success)
            {
                SessionManager.Instance.UpdateEmail(newEmail);
                if (!string.IsNullOrEmpty(updateResult.IdToken))
                {
                    SessionManager.Instance.UpdateToken(updateResult.IdToken, updateResult.RefreshToken);
                    ServiceManager.Instance.SetAuthToken(updateResult.IdToken);
                }

                return new EmailUpdateResult { Success = true };
            }

            return new EmailUpdateResult
            {
                Success = false,
                Error = updateResult.Error ?? "更新失敗"
            };
        }
        catch (Exception ex)
        {
            return new EmailUpdateResult
            {
                Success = false,
                Error = ex.Message
            };
        }
    }

    private void ShowError(string message)
    {
        if (_errorLabel != null)
        {
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

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private class EmailUpdateResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
