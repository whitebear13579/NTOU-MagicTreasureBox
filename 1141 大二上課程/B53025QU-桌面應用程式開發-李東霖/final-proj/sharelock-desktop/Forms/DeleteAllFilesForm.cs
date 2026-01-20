using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Forms;
public class DeleteAllFilesForm : Form
{
    private AntdUI.Input? _confirmInput;
    private AntdUI.Button? _confirmButton;
    private AntdUI.Button? _cancelButton;
    private AntdUI.Label? _errorLabel;

    private bool _isProcessing;
    public int DeletedCount { get; private set; }
    public bool DeleteSucceeded { get; private set; }

    public DeleteAllFilesForm()
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

        Text = "刪除所有檔案";
        Size = new Size(480, 450);
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
            Text = "刪除所有檔案",
            Font = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold),
            ForeColor = ThemeManager.ErrorColor,
            AutoSize = true,
            Location = new Point(0, 6),
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
            Text = "若繼續，此操作將會刪除以下資料：",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = Color.FromArgb(255, 200, 200),
            AutoSize = true,
            Location = new Point(16, 12),
            BackColor = Color.Transparent
        };
        warningCard.Controls.Add(warningTitle);

        var warningItems = new[]
        {
            "• 您帳號中的所有檔案",
            "• 曾創建過的所有分享連結",
            "• 帳號中關於檔案分享的統計資訊"
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
            itemY += 30;
        }

        var extraWarning = new AntdUI.Label
        {
            Text = "此操作無法復原！",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Bold),
            ForeColor = Color.FromArgb(255, 180, 180),
            AutoSize = true,
            Location = new Point(16, itemY + 10),
            BackColor = Color.Transparent
        };
        warningCard.Controls.Add(extraWarning);

        Controls.Add(warningCard);
        currentY += 125;

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

        var confirmLabel = new AntdUI.Label
        {
            Text = "輸入 'DELETE' 以確認",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(confirmLabel);
        currentY += 28;

        _confirmInput = new AntdUI.Input
        {
            PlaceholderText = "DELETE",
            Size = new Size(contentWidth, 44),
            Location = new Point(padding, currentY)
        };
        _confirmInput.TextChanged += (s, e) =>
        {
            _confirmButton!.Enabled = _confirmInput.Text == "DELETE";
        };
        Controls.Add(_confirmInput);

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
            Text = "確認刪除",
            Type = TTypeMini.Error,
            Size = new Size(120, 44),
            Location = new Point(ClientSize.Width - padding - 120, buttonY),
            Enabled = false
        };
        _confirmButton.Click += ConfirmButton_Click;
        Controls.Add(_confirmButton);
    }

    private async void ConfirmButton_Click(object? sender, EventArgs e)
    {
        if (_isProcessing) return;

        if (_confirmInput?.Text != "DELETE")
        {
            ShowError("請輸入 'DELETE' 以確認");
            return;
        }

        _isProcessing = true;
        _confirmButton!.Loading = true;
        _confirmButton.Enabled = false;
        _cancelButton!.Enabled = false;
        HideError();

        try
        {
            var response = await ServiceManager.Instance.Files.DeleteAllFilesAsync();

            if (response?.Success == true)
            {
                DeleteSucceeded = true;
                DeletedCount = response.DeletedCount;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                ShowError(response?.Error ?? "刪除失敗");
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
            _confirmButton.Enabled = _confirmInput?.Text == "DELETE";
            _cancelButton!.Enabled = true;
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
}
