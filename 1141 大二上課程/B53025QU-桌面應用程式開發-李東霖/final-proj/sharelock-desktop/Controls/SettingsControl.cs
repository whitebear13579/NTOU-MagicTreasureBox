using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Models;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;
using AntdLabel = AntdUI.Label;
using AntdButton = AntdUI.Button;

namespace sharelock_desktop.Controls;

public partial class SettingsControl : UserControl
{
    private const string PenSvg = "<svg viewBox=\"0 0 512 512\"><path d=\"M362.7 19.3L314.3 67.7 444.3 197.7l48.4-48.4c25-25 25-65.5 0-90.5L453.3 19.3c-25-25-65.5-25-90.5 0zm-71 71L58.6 323.5c-10.4 10.4-18 23.3-22.2 37.4L1 481.2C-1.5 489.7 .8 498.8 7 505s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L421.7 220.3 291.7 90.3z\"/></svg>";
    private const string MailSvg = "<svg viewBox=\"0 0 512 512\"><path d=\"M48 64C21.5 64 0 85.5 0 112c0 15.1 7.1 29.3 19.2 38.4L236.8 313.6c11.4 8.5 27 8.5 38.4 0L492.8 150.4c12.1-9.1 19.2-23.3 19.2-38.4c0-26.5-21.5-48-48-48H48zM0 176V384c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V176L294.4 339.2c-22.8 17.1-54 17.1-76.8 0L0 176z\"/></svg>";
    private const string InfoSvg = "<svg viewBox=\"0 0 512 512\"><path d=\"M256 512A256 0 1 0 256 0a256 256 0 1 0 0 512zM216 336h24V272H216c-13.3 0-24-10.7-24-24s10.7-24 24-24h48c13.3 0 24 10.7 24 24v88h8c13.3 0 24 10.7 24 24s-10.7 24-24 24H216c-13.3 0-24-10.7-24-24s10.7-24 24-24zm40-208a32 32 0 1 1 0 64 32 32 0 1 1 0-64z\"/></svg>";

    private AntdUI.Avatar? _avatar;
    private AntdLabel? _nameLabel;
    private AntdLabel? _emailLabel;
    private AntdUI.Input? _nameInput;
    private AntdButton? _saveNameButton;
    private AntdButton? _cancelEditButton;
    private AntdButton? _editNameButton;
    private AntdUI.Tag? _emailVerifiedTag;
    private AntdButton? _emailActionButton;
    private WinPanel? _nameDisplayPanel;
    private WinPanel? _nameEditPanel;

    private AntdLabel? _storageUsedLabel;
    private AntdLabel? _filesSharedLabel;
    private AntdLabel? _filesReceivedLabel;
    private AntdLabel? _fileCountLabel;

    private bool _isInitialized;
    private bool _isLoading;
    private bool _isEditingName;
    private bool _emailVerified;

    public SettingsControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        UpdateStyles();

        InitializeComponent();
    }

    private void ShowSuccessNotification(string message)
    {
        var parentForm = this.ParentForm;
        if (parentForm != null && !parentForm.IsDisposed)
        {
            AntdUI.Notification.success(parentForm, "操作成功", message, AntdUI.TAlignFrom.Top, Font);
        }
    }

    private void ShowErrorNotification(string message)
    {
        var parentForm = this.ParentForm;
        if (parentForm != null && !parentForm.IsDisposed)
        {
            AntdUI.Notification.error(parentForm, "發生錯誤", message, AntdUI.TAlignFrom.Top, Font);
        }
    }

    private void ShowWarningNotification(string message)
    {
        var parentForm = this.ParentForm;
        if (parentForm != null && !parentForm.IsDisposed)
        {
            AntdUI.Notification.warn(parentForm, "提示", message, AntdUI.TAlignFrom.Top, Font);
        }
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        BackColor = ThemeManager.BackgroundColor;
        Dock = DockStyle.Fill;
        Padding = new Padding(0);
        AutoScroll = true;

        Load += SettingsControl_Load;

        ResumeLayout(false);
    }

    private async void SettingsControl_Load(object? sender, EventArgs e)
    {
        if (_isInitialized) return;
        _isInitialized = true;

        await LoadUserInfoAsync();
        BuildUI();
        await LoadStatisticsAsync();
    }

    private async Task LoadUserInfoAsync()
    {
        try
        {
            var session = SessionManager.Instance;
            if (!string.IsNullOrEmpty(session.IdToken))
            {
                var userInfo = await ServiceManager.Instance.FirebaseAuth.GetUserInfoAsync(session.IdToken);
                if (userInfo != null)
                {
                    _emailVerified = userInfo.EmailVerified;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Settings] 載入使用者資訊失敗: {ex.Message}");
        }
    }

    private int MeasureTextWidth(string text, Font font)
    {
        return TextRenderer.MeasureText(text, font).Width;
    }

    private void BuildUI()
    {
        SuspendLayout();
        Controls.Clear();

        int currentY = 0;
        int availableWidth = Width - SystemInformation.VerticalScrollBarWidth - 20;
        int contentWidth = Math.Max(600, availableWidth);

        var titlePanel = CreatePageTitle();
        titlePanel.Location = new Point(0, currentY);
        Controls.Add(titlePanel);
        currentY += 60;

        var profileCard = CreateProfileCard(contentWidth);
        profileCard.Location = new Point(0, currentY);
        Controls.Add(profileCard);
        currentY += profileCard.Height + UIConstants.SectionGap;

        var statsCard = CreateStatisticsCard(contentWidth);
        statsCard.Location = new Point(0, currentY);
        Controls.Add(statsCard);
        currentY += statsCard.Height + UIConstants.SectionGap;

        var dangerCard = CreateDangerZoneCard(contentWidth);
        dangerCard.Location = new Point(0, currentY);
        Controls.Add(dangerCard);
        currentY += dangerCard.Height + UIConstants.SectionGap;

        var versionPanel = new WinPanel
        {
            Size = new Size(contentWidth, 30),
            Location = new Point(0, currentY),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(versionPanel);

        var infoIcon = new IconPictureBox
        {
            IconChar = IconChar.InfoCircle,
            IconSize = 16,
            IconColor = ThemeManager.TextSecondaryColor,
            Size = new Size(20, 20),
            Location = new Point(0, 5),
            BackColor = Color.Transparent
        };
        versionPanel.Controls.Add(infoIcon);

        var versionLabel = new AntdLabel
        {
            Text = "ShareLock Desktop v1.0.0 | 由 AntdUI 與 .NET 8.0 強力驅動。",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(26, 5),
            BackColor = Color.Transparent
        };
        versionPanel.Controls.Add(versionLabel);
        Controls.Add(versionPanel);

        ResumeLayout(true);
    }

    private WinPanel CreatePageTitle()
    {
        var panel = new WinPanel
        {
            Size = new Size(400, 50),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(panel);

        var icon = new IconPictureBox
        {
            IconChar = IconChar.Cog,
            IconSize = 32,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(38, 38),
            Location = new Point(0, 6),
            BackColor = Color.Transparent
        };
        panel.Controls.Add(icon);

        var title = new AntdLabel
        {
            Text = "帳號設定",
            Font = new Font(ThemeManager.FontFamily, 22, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(46, 8),
            BackColor = Color.Transparent
        };
        panel.Controls.Add(title);

        return panel;
    }

    private WinPanel CreateProfileCard(int width)
    {
        var card = new WinPanel
        {
            Size = new Size(width, 180),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingXL)
        };
        ThemeManager.ApplyRoundedCorners(card, 12);
        UIConstants.EnableDoubleBuffering(card);

        var session = SessionManager.Instance;
        int padding = UIConstants.PaddingXL;
        int avatarSize = 100;

        int avatarY = (card.Height - avatarSize) / 2;
        _avatar = new AntdUI.Avatar
        {
            Location = new Point(padding, avatarY),
            Round = true,
            Size = new Size(avatarSize, avatarSize),
            Text = (session.DisplayName ?? session.Email ?? "U").Substring(0, 1).ToUpper()
        };
        card.Controls.Add(_avatar);

        if (!string.IsNullOrEmpty(session.PhotoUrl))
        {
            LoadAvatarAsync(session.PhotoUrl);
        }

        int textStartX = padding + avatarSize + 30;
        int rightButtonX = width - 160 - padding;

        int nameHeight = 36;
        int emailHeight = 36;
        int gap = 8;
        int totalTextHeight = nameHeight + gap + emailHeight;
        int textStartY = (card.Height - totalTextHeight) / 2;

        _nameDisplayPanel = new WinPanel
        {
            Location = new Point(textStartX, textStartY),
            Size = new Size(rightButtonX - textStartX - 10, nameHeight),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(_nameDisplayPanel);

        var nameFont = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold);
        _nameLabel = new AntdLabel
        {
            Text = session.DisplayName ?? "使用者",
            Font = nameFont,
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(0, 4),
            BackColor = Color.Transparent
        };
        _nameDisplayPanel.Controls.Add(_nameLabel);

        int nameWidth = MeasureTextWidth(_nameLabel.Text, nameFont);
        _editNameButton = new AntdButton
        {
            IconSvg = PenSvg,
            Text = "編輯",
            Type = TTypeMini.Default,
            Ghost = true,
            Size = new Size(80, 30),
            Font = new Font(ThemeManager.FontFamily, 10f),
            Location = new Point(Math.Min(nameWidth + 10, _nameDisplayPanel.Width - 90), 3)
        };
        _editNameButton.Click += (s, e) => EnterEditMode();
        _nameDisplayPanel.Controls.Add(_editNameButton);

        card.Controls.Add(_nameDisplayPanel);

        _nameEditPanel = new WinPanel
        {
            Location = new Point(textStartX, textStartY),
            Size = new Size(rightButtonX - textStartX - 10, 40),
            BackColor = Color.Transparent,
            Visible = false
        };
        UIConstants.EnableDoubleBuffering(_nameEditPanel);

        _nameInput = new AntdUI.Input
        {
            Text = session.DisplayName ?? "",
            Size = new Size(200, 36),
            Location = new Point(0, 0),
            PlaceholderText = "輸入新名稱"
        };
        _nameEditPanel.Controls.Add(_nameInput);

        _saveNameButton = new AntdButton
        {
            Text = "儲存",
            Type = TTypeMini.Primary,
            Size = new Size(70, 36),
            Location = new Point(210, 0)
        };
        _saveNameButton.Click += async (s, e) => await SaveDisplayNameAsync();
        _nameEditPanel.Controls.Add(_saveNameButton);

        _cancelEditButton = new AntdButton
        {
            Text = "取消",
            Type = TTypeMini.Default,
            Ghost = true,
            Size = new Size(70, 36),
            Location = new Point(290, 0)
        };
        _cancelEditButton.Click += (s, e) => ExitEditMode();
        _nameEditPanel.Controls.Add(_cancelEditButton);

        card.Controls.Add(_nameEditPanel);

        var emailPanel = new WinPanel
        {
            Location = new Point(textStartX, textStartY + nameHeight + gap),
            Size = new Size(rightButtonX - textStartX - 10, emailHeight),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(emailPanel);

        var emailFont = new Font(ThemeManager.FontFamily, 12f, FontStyle.Regular);
        _emailLabel = new AntdLabel
        {
            Text = session.Email ?? "",
            Font = emailFont,
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(0, 8),
            BackColor = Color.Transparent
        };
        emailPanel.Controls.Add(_emailLabel);

        int emailWidth = MeasureTextWidth(_emailLabel.Text, emailFont);
        int tagX = emailWidth + 16;

        _emailVerifiedTag = new AntdUI.Tag
        {
            Text = _emailVerified ? "已驗證" : "未驗證",
            Type = _emailVerified ? TTypeMini.Success : TTypeMini.Error,
            Size = new Size(70, 26),
            Font = new Font(ThemeManager.FontFamily, 9f),
            Location = new Point(tagX, 5)
        };
        emailPanel.Controls.Add(_emailVerifiedTag);

        _emailActionButton = new AntdButton
        {
            IconSvg = _emailVerified ? PenSvg : MailSvg,
            Text = _emailVerified ? "編輯" : "寄發驗證信",
            Type = TTypeMini.Default,
            Ghost = true,
            Size = new Size(_emailVerified ? 80 : 115, 30),
            Font = new Font(ThemeManager.FontFamily, 10f),
            Location = new Point(tagX + 78, 3)
        };
        _emailActionButton.Click += async (s, e) => await HandleEmailActionAsync();
        emailPanel.Controls.Add(_emailActionButton);

        card.Controls.Add(emailPanel);

        var webEditButton = new AntdButton
        {
            Text = "在網頁版編輯",
            Type = TTypeMini.Primary,
            Size = new Size(140, 44),
            Location = new Point(rightButtonX, (card.Height - 44) / 2)
        };
        webEditButton.Click += (s, e) => OpenUrl($"{FirebaseConfig.WebAppUrl}/dashboard/settings");
        card.Controls.Add(webEditButton);

        return card;
    }

    private WinPanel CreateStatisticsCard(int width)
    {
        var card = new WinPanel
        {
            Size = new Size(width, 200),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingXL)
        };
        ThemeManager.ApplyRoundedCorners(card, 12);
        UIConstants.EnableDoubleBuffering(card);

        int padding = UIConstants.PaddingXL;

        var icon = new IconPictureBox
        {
            IconChar = IconChar.ChartBar,
            IconSize = 22,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(28, 28),
            Location = new Point(padding, padding),
            BackColor = Color.Transparent
        };
        card.Controls.Add(icon);

        var title = new AntdLabel
        {
            Text = "帳號統計",
            Font = new Font(ThemeManager.FontFamily, 15f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(padding + 34, padding + 2),
            BackColor = Color.Transparent
        };
        card.Controls.Add(title);

        int statsY = padding + 50;
        int availableWidth = width - padding * 2;
        int colWidth = availableWidth / 4;

        var storagePanel = CreateStatItem(IconChar.Database, "已用空間", "載入中...", padding, statsY, colWidth);
        _storageUsedLabel = FindValueLabel(storagePanel);
        card.Controls.Add(storagePanel);

        var fileCountPanel = CreateStatItem(IconChar.File, "檔案數量", "載入中...", padding + colWidth, statsY, colWidth);
        _fileCountLabel = FindValueLabel(fileCountPanel);
        card.Controls.Add(fileCountPanel);

        var sharedPanel = CreateStatItem(IconChar.Share, "我的分享", "載入中...", padding + colWidth * 2, statsY, colWidth);
        _filesSharedLabel = FindValueLabel(sharedPanel);
        card.Controls.Add(sharedPanel);

        var receivedPanel = CreateStatItem(IconChar.Download, "收到分享", "載入中...", padding + colWidth * 3, statsY, colWidth);
        _filesReceivedLabel = FindValueLabel(receivedPanel);
        card.Controls.Add(receivedPanel);

        return card;
    }

    private AntdLabel? FindValueLabel(WinPanel panel)
    {
        foreach (Control ctrl in panel.Controls)
        {
            if (ctrl is AntdLabel label && label.Font.Bold)
                return label;
        }
        return null;
    }

    private WinPanel CreateStatItem(IconChar icon, string label, string value, int x, int y, int width)
    {
        var panel = new WinPanel
        {
            Location = new Point(x, y),
            Size = new Size(width, 120),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(panel);

        int centerX = width / 2;
        int iconSize = 38;

        var iconBox = new IconPictureBox
        {
            IconChar = icon,
            IconSize = 32,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(iconSize, iconSize),
            BackColor = Color.Transparent,
            Location = new Point(centerX - iconSize / 2, 0)
        };
        panel.Controls.Add(iconBox);

        var labelFont = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular);
        var labelCtrl = new AntdLabel
        {
            Text = label,
            Font = labelFont,
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = false,
            Size = new Size(width, 22),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
            Location = new Point(0, 46)
        };
        panel.Controls.Add(labelCtrl);

        var valueFont = new Font(ThemeManager.FontFamily, 16f, FontStyle.Bold);
        var valueCtrl = new AntdLabel
        {
            Text = value,
            Font = valueFont,
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = false,
            Size = new Size(width, 28),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.Transparent,
            Location = new Point(0, 74)
        };
        panel.Controls.Add(valueCtrl);

        return panel;
    }

    private WinPanel CreateDangerZoneCard(int width)
    {
        int padding = UIConstants.PaddingXL;
        int buttonX = width - 140 - padding;
        int cardHeight = 240;

        var card = new WinPanel
        {
            Size = new Size(width, cardHeight),
            BackColor = Color.FromArgb(55, 40, 40),
            Padding = new Padding(padding)
        };
        ThemeManager.ApplyRoundedCorners(card, 12);
        UIConstants.EnableDoubleBuffering(card);

        var icon = new IconPictureBox
        {
            IconChar = IconChar.ExclamationTriangle,
            IconSize = 22,
            IconColor = ThemeManager.ErrorColor,
            Size = new Size(28, 28),
            Location = new Point(padding, padding),
            BackColor = Color.Transparent
        };
        card.Controls.Add(icon);

        var title = new AntdLabel
        {
            Text = "危險操作",
            Font = new Font(ThemeManager.FontFamily, 15f, FontStyle.Bold),
            ForeColor = ThemeManager.ErrorColor,
            AutoSize = true,
            Location = new Point(padding + 34, padding + 2),
            BackColor = Color.Transparent
        };
        card.Controls.Add(title);

        var warningLabel = new AntdLabel
        {
            Text = "以下操作無法復原，請謹慎操作",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, padding + 35),
            BackColor = Color.Transparent
        };
        card.Controls.Add(warningLabel);

        int buttonY = padding + 70;

        var deleteFilesLabel = new AntdLabel
        {
            Text = "刪除所有檔案",
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(padding, buttonY + 8),
            BackColor = Color.Transparent
        };
        card.Controls.Add(deleteFilesLabel);

        var deleteFilesDesc = new AntdLabel
        {
            Text = "永久刪除您上傳的所有檔案，此操作無法復原",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, buttonY + 32),
            BackColor = Color.Transparent
        };
        card.Controls.Add(deleteFilesDesc);

        var deleteFilesButton = new AntdButton
        {
            Text = "刪除所有檔案",
            Type = TTypeMini.Error,
            Size = new Size(130, 40),
            Location = new Point(buttonX, buttonY + 5)
        };
        deleteFilesButton.Click += async (s, e) => await DeleteAllFilesAsync();
        card.Controls.Add(deleteFilesButton);

        buttonY += 70;

        var deleteAccountLabel = new AntdLabel
        {
            Text = "刪除帳號",
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(padding, buttonY + 8),
            BackColor = Color.Transparent
        };
        card.Controls.Add(deleteAccountLabel);

        var deleteAccountDesc = new AntdLabel
        {
            Text = "永久刪除您的帳號和所有資料，此操作無法復原",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(padding, buttonY + 32),
            BackColor = Color.Transparent
        };
        card.Controls.Add(deleteAccountDesc);

        var deleteAccountButton = new AntdButton
        {
            Text = "刪除帳號",
            Type = TTypeMini.Error,
            Size = new Size(130, 40),
            Location = new Point(buttonX, buttonY + 5)
        };
        deleteAccountButton.Click += async (s, e) => await DeleteAccountAsync();
        card.Controls.Add(deleteAccountButton);

        return card;
    }

    private void EnterEditMode()
    {
        if (_isEditingName) return;
        _isEditingName = true;

        _nameInput!.Text = SessionManager.Instance.DisplayName ?? "";
        _nameDisplayPanel!.Visible = false;
        _nameEditPanel!.Visible = true;
        _nameInput.Focus();
    }

    private void ExitEditMode()
    {
        _isEditingName = false;
        _nameDisplayPanel!.Visible = true;
        _nameEditPanel!.Visible = false;
    }

    private async Task SaveDisplayNameAsync()
    {
        var newName = _nameInput?.Text?.Trim();
        if (string.IsNullOrEmpty(newName))
        {
            ShowWarningNotification("請輸入有效的名稱");
            return;
        }

        if (newName.Length > 20)
        {
            ShowWarningNotification("名稱不能超過 20 個字元");
            return;
        }

        _saveNameButton!.Loading = true;
        _saveNameButton.Enabled = false;
        _cancelEditButton!.Enabled = false;

        try
        {
            var session = SessionManager.Instance;

            var result = await ServiceManager.Instance.FirebaseAuth.UpdateProfileAsync(
                session.IdToken!,
                displayName: newName
            );

            if (result.Success)
            {
                SessionManager.Instance.UpdateDisplayName(result.DisplayName ?? newName);

                if (!string.IsNullOrEmpty(result.IdToken))
                {
                    SessionManager.Instance.UpdateToken(result.IdToken, result.RefreshToken);
                    ServiceManager.Instance.SetAuthToken(result.IdToken);
                }

                _nameLabel!.Text = result.DisplayName ?? newName;
                _avatar!.Text = newName.Substring(0, 1).ToUpper();

                var nameFont = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold);
                int nameWidth = MeasureTextWidth(_nameLabel.Text, nameFont);
                _editNameButton!.Location = new Point(
                    Math.Min(nameWidth + 10, _nameDisplayPanel!.Width - 90), 3);

                ExitEditMode();
                ShowSuccessNotification("名稱已更新！");
            }
            else
            {
                ShowErrorNotification(result.Error ?? "更新失敗");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Settings] 更新名稱失敗: {ex.Message}");
            ShowErrorNotification($"更新失敗：{ex.Message}");
        }
        finally
        {
            _saveNameButton!.Loading = false;
            _saveNameButton.Enabled = true;
            _cancelEditButton!.Enabled = true;
        }
    }

    private async Task HandleEmailActionAsync()
    {
        if (_emailVerified)
        {
            using var changeEmailForm = new Forms.ChangeEmailForm();
            var result = changeEmailForm.ShowDialog(this.ParentForm);

            if (result == DialogResult.OK)
            {
                _emailLabel!.Text = SessionManager.Instance.Email ?? "";
                
                var emailFont = new Font(ThemeManager.FontFamily, 12f, FontStyle.Regular);
                int emailWidth = MeasureTextWidth(_emailLabel.Text, emailFont);
                int tagX = emailWidth + 16;
                
                _emailVerifiedTag!.Text = "未驗證";
                _emailVerifiedTag.Type = TTypeMini.Error;
                _emailVerifiedTag.Location = new Point(tagX, 5);
                
                _emailActionButton!.IconSvg = MailSvg;
                _emailActionButton.Text = "寄發驗證信";
                _emailActionButton.Size = new Size(115, 30);
                _emailActionButton.Location = new Point(tagX + 78, 3);
                
                _emailVerified = false;
                
                ShowSuccessNotification("電子郵件已變更，請驗證新的電子郵件地址");
            }
        }
        else
        {
            await SendVerificationEmailAsync();
        }
    }

    private async Task SendVerificationEmailAsync()
    {
        _emailActionButton!.Loading = true;
        _emailActionButton.Enabled = false;

        try
        {
            var session = SessionManager.Instance;
            var result = await ServiceManager.Instance.FirebaseAuth.SendEmailVerificationAsync(session.IdToken!);

            if (result.Success)
            {
                ShowSuccessNotification("驗證郵件已發送！請檢查您的信箱");
            }
            else
            {
                ShowErrorNotification(result.Error ?? "發送失敗");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Settings] 發送驗證信失敗: {ex.Message}");
            ShowErrorNotification($"發送失敗：{ex.Message}");
        }
        finally
        {
            _emailActionButton!.Loading = false;
            _emailActionButton.Enabled = true;
        }
    }

    private async Task LoadStatisticsAsync()
    {
        if (_isLoading) return;
        _isLoading = true;

        try
        {
            var storageTask = ServiceManager.Instance.Storage.GetUsageAsync();
            var statisticsTask = ServiceManager.Instance.Statistics.GetOverviewAsync();

            await Task.WhenAll(storageTask, statisticsTask);

            var storage = await storageTask;
            var statistics = await statisticsTask;

            if (IsDisposed) return;

            Invoke(() =>
            {
                if (storage != null)
                {
                    UpdateStatLabel(_storageUsedLabel, storage.FormattedUsed);
                    UpdateStatLabel(_fileCountLabel, $"{storage.FileCount} 個");
                }
                else
                {
                    UpdateStatLabel(_storageUsedLabel, "N/A");
                    UpdateStatLabel(_fileCountLabel, "N/A");
                }

                if (statistics != null)
                {
                    UpdateStatLabel(_filesSharedLabel, $"{statistics.FilesShared} 個");
                    UpdateStatLabel(_filesReceivedLabel, $"{statistics.FilesReceived} 個");
                }
                else
                {
                    UpdateStatLabel(_filesSharedLabel, "N/A");
                    UpdateStatLabel(_filesReceivedLabel, "N/A");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Settings] 載入統計失敗: {ex.Message}");
            if (!IsDisposed)
            {
                Invoke(() =>
                {
                    UpdateStatLabel(_storageUsedLabel, "N/A");
                    UpdateStatLabel(_fileCountLabel, "N/A");
                    UpdateStatLabel(_filesSharedLabel, "N/A");
                    UpdateStatLabel(_filesReceivedLabel, "N/A");
                });
            }
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void UpdateStatLabel(AntdLabel? label, string text)
    {
        if (label == null || label.IsDisposed) return;
        label.Text = text;
    }

    private async Task DeleteAllFilesAsync()
    {
        using var deleteFilesForm = new Forms.DeleteAllFilesForm();
        var dialogResult = deleteFilesForm.ShowDialog(this.ParentForm);

        if (dialogResult == DialogResult.OK && deleteFilesForm.DeleteSucceeded)
        {
            ShowSuccessNotification($"已成功刪除 {deleteFilesForm.DeletedCount} 個檔案");
            await LoadStatisticsAsync();
        }
    }

    private async Task DeleteAccountAsync()
    {
        using var deleteAccountForm = new Forms.DeleteAccountForm();
        var dialogResult = deleteAccountForm.ShowDialog(this.ParentForm);

        if (dialogResult == DialogResult.OK && deleteAccountForm.DeleteSucceeded)
        {
            SessionManager.Instance.ClearSession();

            if (this.ParentForm is Forms.MainForm mainForm)
            {
                mainForm.SwitchToLoginForm();
            }
        }
    }

    private async void LoadAvatarAsync(string url)
    {
        try
        {
            using var httpClient = new HttpClient();
            var imageBytes = await httpClient.GetByteArrayAsync(url);
            using var ms = new MemoryStream(imageBytes);
            var image = Image.FromStream(ms);

            if (_avatar != null && !_avatar.IsDisposed)
            {
                Invoke(() => { _avatar.Image = image; });
            }
        }
        catch
        {
        }
    }

    private static void OpenUrl(string url)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
        {
            UseShellExecute = true
        });
    }
}
