using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Forms;
public class DeleteAccountForm : Form
{
    private AntdUI.Input? _passwordInput;
    private AntdUI.Button? _confirmButton;
    private AntdUI.Button? _cancelButton;
    private AntdUI.Label? _errorLabel;

    private bool _isProcessing;
    public bool DeleteSucceeded { get; private set; }

    public DeleteAccountForm()
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

        Text = "刪除帳號";
        Size = new Size(480, 460);
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

        var iconBox = new IconPictureBox
        {
            IconChar = IconChar.ExclamationTriangle,
            IconSize = 28,
            IconColor = ThemeManager.ErrorColor,
            Size = new Size(32, 32),
            Location = new Point(0, 4),
            BackColor = Color.Transparent
        };
        titlePanel.Controls.Add(iconBox);

        var titleLabel = new AntdUI.Label
        {
            Text = "刪除帳號",
            Font = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold),
            ForeColor = ThemeManager.ErrorColor,
            AutoSize = true,
            Location = new Point(40, 6),
            BackColor = Color.Transparent
        };
        titlePanel.Controls.Add(titleLabel);
        Controls.Add(titlePanel);
        currentY += 50;

        var warningCard = new WinPanel
        {
            Location = new Point(padding, currentY),
            Size = new Size(contentWidth, 130),
            BackColor = Color.FromArgb(80, 211, 47, 47)
        };
        ThemeManager.ApplyRoundedCorners(warningCard, 8);

        var warningTitle = new AntdUI.Label
        {
            Text = "若繼續，此操作將會影響：",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = Color.FromArgb(255, 200, 200),
            AutoSize = true,
            Location = new Point(16, 12),
            BackColor = Color.Transparent
        };
        warningCard.Controls.Add(warningTitle);

        var warningItems = new[]
        {
            "• 永久刪除您帳號中的所有資料",
            "• 立即登出所有裝置",
            "• 此操作無法復原，我們也無法恢復您的任何資料"
        };

        int itemY = 38;
        foreach (var item in warningItems)
        {
            var itemLabel = new AntdUI.Label
            {
                Text = item,
                Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
                ForeColor = Color.FromArgb(255, 200, 200),
                AutoSize = true,
                Location = new Point(16, itemY),
                BackColor = Color.Transparent
            };
            warningCard.Controls.Add(itemLabel);
            itemY += 24;
        }

        Controls.Add(warningCard);
        currentY += 145;

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
        currentY += 30;

        var passwordLabel = new AntdUI.Label
        {
            Text = "輸入密碼來確認",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(passwordLabel);
        currentY += 28;

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
            Text = "確認刪除帳號",
            Type = TTypeMini.Error,
            Size = new Size(140, 44),
            Location = new Point(ClientSize.Width - padding - 140, buttonY)
        };
        _confirmButton.Click += ConfirmButton_Click;
        Controls.Add(_confirmButton);
    }

    private async void ConfirmButton_Click(object? sender, EventArgs e)
    {
        if (_isProcessing) return;

        var password = _passwordInput?.Text;

        if (string.IsNullOrEmpty(password))
        {
            ShowError("請輸入密碼");
            return;
        }

        _isProcessing = true;
        _confirmButton!.Loading = true;
        _confirmButton.Enabled = false;
        _cancelButton!.Enabled = false;
        HideError();

        try
        {
            var result = await DeleteAccountAsync(password);

            if (result.Success)
            {
                DeleteSucceeded = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                ShowError(result.Error ?? "刪除失敗");
            }
        }
        catch (Exception ex)
        {
            ShowError($"刪除失敗：{ex.Message}");
        }
        finally
        {
            _isProcessing = false;
            _confirmButton!.Loading = false;
            _confirmButton.Enabled = true;
            _cancelButton!.Enabled = true;
        }
    }

    private async Task<DeleteAccountResult> DeleteAccountAsync(string password)
    {
        try
        {
            var session = SessionManager.Instance;

            var reAuthResult = await ServiceManager.Instance.FirebaseAuth.SignInWithEmailPasswordAsync(
                session.Email!, password);

            if (!reAuthResult.Success)
            {
                return new DeleteAccountResult
                {
                    Success = false,
                    Error = reAuthResult.Error ?? "密碼驗證失敗"
                };
            }

            var deleteResult = await ServiceManager.Instance.FirebaseAuth.DeleteAccountAsync(
                reAuthResult.IdToken!);

            if (deleteResult.Success)
            {

                try
                {
                    await ServiceManager.Instance.Auth.LogoutAsync();
                }
                catch
                {

                }

                return new DeleteAccountResult { Success = true };
            }

            return new DeleteAccountResult
            {
                Success = false,
                Error = deleteResult.Error ?? "刪除失敗"
            };
        }
        catch (Exception ex)
        {
            return new DeleteAccountResult
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

    private class DeleteAccountResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
