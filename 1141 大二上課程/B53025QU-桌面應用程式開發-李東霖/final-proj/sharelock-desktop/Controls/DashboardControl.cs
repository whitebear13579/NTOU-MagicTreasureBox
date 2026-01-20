using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Models;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;
using System.Runtime.InteropServices;

using WinPanel = System.Windows.Forms.Panel;
using AppIconHelper = sharelock_desktop.Utils.IconHelper;

namespace sharelock_desktop.Controls;

public partial class DashboardControl : UserControl
{
    private const int WS_HSCROLL = 0x100000;
    private const int GWL_STYLE = -16;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private AntdUI.Label? _welcomeLabel;
    private AntdUI.Label? _descLabel;
    
    private AntdUI.Progress? _storageProgress;
    private AntdUI.Label? _storageLabel;
    private AntdUI.Label? _storageStatusLabel;
    private IconPictureBox? _storageStatusIcon;
    private WinPanel? _storageContainer;
    private WinPanel? _invitationsContainer;
    private WinPanel? _notificationsContainer;
    private WinPanel? _recentFilesContainer;

    private bool _isLoading;
    private bool _isInitialized;
    private int _lastWidth;
    
    private const int ItemHorizontalPadding = 20;

    private List<NotificationInfo> _cachedNotifications = new();

    public DashboardControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        UpdateStyles();

        InitializeComponent();
        Load += DashboardControl_Load;
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        BackColor = ThemeManager.BackgroundColor;
        Dock = DockStyle.Fill;
        AutoScroll = true;
        Padding = new Padding(0);
        Name = "DashboardControl";

        ResumeLayout(false);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            cp.Style &= ~WS_HSCROLL;
            return cp;
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        HideHorizontalScrollBar();
    }

    private void HideHorizontalScrollBar()
    {
        if (!IsHandleCreated) return;
        
        int style = GetWindowLong(Handle, GWL_STYLE);
        style &= ~WS_HSCROLL;
        SetWindowLong(Handle, GWL_STYLE, style);
    }

    private void BuildUI()
    {
        SuspendLayout();
        Controls.Clear();

        int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        int contentWidth = Width - scrollBarWidth - 2;
        if (contentWidth < 400) contentWidth = 400;

        var session = SessionManager.Instance;
        var greeting = AppIconHelper.GetTimeBasedGreeting();

        int currentY = 0;

        var greetingIcon = CreateIcon(GetGreetingIcon(), 34, ThemeManager.PrimaryColor);
        greetingIcon.Location = new Point(0, currentY);
        Controls.Add(greetingIcon);

        _welcomeLabel = new AntdUI.Label
        {
            Text = greeting,
            Font = new Font(ThemeManager.FontFamily, 24, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(48, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(_welcomeLabel);
        currentY += 50;

        _descLabel = new AntdUI.Label
        {
            Text = $"{session.DisplayName ?? session.Email ?? "使用者"}，歡迎回來 ShareLock！",
            Font = new Font(ThemeManager.FontFamily, 13f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = false,
            Size = new Size(contentWidth, 35),
            Location = new Point(0, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(_descLabel);
        currentY += 50;

        var storageHeader = CreateSectionHeader(IconChar.ChartPie, "儲存空間");
        storageHeader.Location = new Point(0, currentY);
        Controls.Add(storageHeader);
        currentY += 48;

        _storageContainer = CreateStorageCard(contentWidth);
        _storageContainer.Location = new Point(ItemHorizontalPadding, currentY);
        Controls.Add(_storageContainer);
        currentY += _storageContainer.Height + UIConstants.SectionGap;

        var invitationsHeader = CreateSectionHeader(IconChar.Share, "分享邀請");
        invitationsHeader.Location = new Point(0, currentY);
        Controls.Add(invitationsHeader);
        currentY += 48;

        _invitationsContainer = new WinPanel
        {
            Location = new Point(0, currentY),
            Size = new Size(contentWidth, 150),
            BackColor = Color.Transparent,
            AutoScroll = false
        };
        UIConstants.EnableDoubleBuffering(_invitationsContainer);
        Controls.Add(_invitationsContainer);
        currentY += 160;

        var notificationsHeader = CreateSectionHeader(IconChar.Bell, "通知中心");
        notificationsHeader.Location = new Point(0, currentY);
        Controls.Add(notificationsHeader);
        currentY += 48;

        _notificationsContainer = new WinPanel
        {
            Location = new Point(0, currentY),
            Size = new Size(contentWidth, 280),
            BackColor = Color.Transparent,
            AutoScroll = false
        };
        UIConstants.EnableDoubleBuffering(_notificationsContainer);
        Controls.Add(_notificationsContainer);
        currentY += 290;

        var recentFilesHeader = CreateSectionHeader(IconChar.Clock, "最近使用的檔案");
        recentFilesHeader.Location = new Point(0, currentY);
        Controls.Add(recentFilesHeader);
        currentY += 48;

        _recentFilesContainer = new WinPanel
        {
            Location = new Point(0, currentY),
            Size = new Size(contentWidth, 300),
            BackColor = Color.Transparent,
            AutoScroll = false
        };
        UIConstants.EnableDoubleBuffering(_recentFilesContainer);
        Controls.Add(_recentFilesContainer);

        ResumeLayout(true);
    }

    private static IconChar GetGreetingIcon()
    {
        var hour = DateTime.Now.Hour;
        return hour switch
        {
            >= 5 and < 17 => IconChar.Sun,
            >= 17 and < 21 => IconChar.CloudSun,
            _ => IconChar.Moon
        };
    }

    private IconPictureBox CreateIcon(IconChar icon, int size, Color color)
    {
        return new IconPictureBox
        {
            IconChar = icon,
            IconSize = size,
            IconColor = color,
            Size = new Size(size + 6, size + 6),
            BackColor = Color.Transparent
        };
    }

    private WinPanel CreateSectionHeader(IconChar icon, string title)
    {
        var panel = new WinPanel
        {
            Size = new Size(450, 44),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(panel);

        var iconBox = CreateIcon(icon, 26, ThemeManager.PrimaryColor);
        iconBox.Location = new Point(0, 8);
        panel.Controls.Add(iconBox);

        var titleLabel = new AntdUI.Label
        {
            Text = title,
            Font = new Font(ThemeManager.FontFamily, 15f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(36, 10),
            BackColor = Color.Transparent
        };
        panel.Controls.Add(titleLabel);

        return panel;
    }

    private WinPanel CreateStorageCard(int containerWidth)
    {
        int itemWidth = containerWidth - ItemHorizontalPadding * 2;
        
        var card = new WinPanel
        {
            Size = new Size(itemWidth, 100),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingLG)
        };
        ThemeManager.ApplyRoundedCorners(card, 8);
        UIConstants.EnableDoubleBuffering(card);

        _storageStatusIcon = new IconPictureBox
        {
            IconChar = IconChar.Spinner,
            IconSize = 18,
            IconColor = ThemeManager.TextSecondaryColor,
            Size = new Size(24, 24),
            Location = new Point(UIConstants.PaddingLG, UIConstants.PaddingLG + 2),
            BackColor = Color.Transparent
        };
        card.Controls.Add(_storageStatusIcon);

        _storageStatusLabel = new AntdUI.Label
        {
            Text = "載入中...",
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingLG + 28, UIConstants.PaddingLG + 5),
            BackColor = Color.Transparent
        };
        card.Controls.Add(_storageStatusLabel);

        _storageLabel = new AntdUI.Label
        {
            Text = "-- / --",
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(itemWidth - 150, UIConstants.PaddingLG + 5),
            BackColor = Color.Transparent
        };
        card.Controls.Add(_storageLabel);

        _storageProgress = new AntdUI.Progress
        {
            Location = new Point(UIConstants.PaddingLG, 55),
            Size = new Size(itemWidth - UIConstants.PaddingLG * 2, 20),
            Value = 0,
            Shape = TShapeProgress.Round
        };
        card.Controls.Add(_storageProgress);

        return card;
    }

    private async void DashboardControl_Load(object? sender, EventArgs e)
    {
        if (_isInitialized) return;
        _isInitialized = true;
        _lastWidth = Width;

        BuildUI();
        await LoadDashboardDataAsync();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (!_isInitialized) return;

        HideHorizontalScrollBar();

        if (Math.Abs(Width - _lastWidth) > 30)
        {
            _lastWidth = Width;
            UpdateContainerWidths();
        }
    }

    private void UpdateContainerWidths()
    {
        int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        int contentWidth = Width - scrollBarWidth - 2;
        if (contentWidth < 400) contentWidth = 400;

        if (_descLabel != null)
            _descLabel.Width = contentWidth;

        if (_storageContainer != null)
        {
            int storageWidth = contentWidth - ItemHorizontalPadding * 2;
            _storageContainer.Width = storageWidth;
            if (_storageProgress != null)
            {
                _storageProgress.Width = storageWidth - UIConstants.PaddingLG * 2;
            }
            if (_storageLabel != null)
            {
                _storageLabel.Location = new Point(storageWidth - 150, _storageLabel.Location.Y);
            }
        }

        if (_invitationsContainer != null)
            _invitationsContainer.Width = contentWidth;

        if (_notificationsContainer != null)
            _notificationsContainer.Width = contentWidth;

        if (_recentFilesContainer != null)
            _recentFilesContainer.Width = contentWidth;

        if (_notificationsContainer != null && _notificationsContainer.Controls.Count > 0)
        {
            _ = LoadNotificationsAsync();
        }
        if (_recentFilesContainer != null && _recentFilesContainer.Controls.Count > 0)
        {
            _ = LoadRecentFilesAsync();
        }
    }

    private async Task LoadDashboardDataAsync()
    {
        if (_isLoading) return;
        _isLoading = true;

        var storageTask = LoadStorageUsageAsync();
        var notificationsTask = LoadNotificationsAsync();
        var recentFilesTask = LoadRecentFilesAsync();

        try
        {
            await Task.WhenAll(storageTask, notificationsTask, recentFilesTask);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Dashboard] 載入資料時發生錯誤: {ex.Message}");
        }
        finally
        {
            _isLoading = false;
        }
    }

    private async Task LoadStorageUsageAsync()
    {
        try
        {
            var usage = await ServiceManager.Instance.Storage.GetUsageAsync();

            if (usage != null)
            {
                Invoke(() =>
                {
                    if (_storageLabel != null)
                    {
                        _storageLabel.Text = $"{usage.FormattedUsed} / {usage.FormattedQuota}";
                    }
                    if (_storageStatusLabel != null && _storageStatusIcon != null)
                    {
                        if (usage.Percentage >= 85)
                        {
                            _storageStatusIcon.IconChar = IconChar.ExclamationTriangle;
                            _storageStatusIcon.IconColor = ThemeManager.WarningColor;
                            _storageStatusLabel.Text = $"可用空間不足 {100 - usage.Percentage:F0}%";
                            _storageStatusLabel.ForeColor = ThemeManager.WarningColor;
                        }
                        else
                        {
                            _storageStatusIcon.IconChar = IconChar.CheckCircle;
                            _storageStatusIcon.IconColor = ThemeManager.SuccessColor;
                            _storageStatusLabel.Text = $"可用空間還剩 {100 - usage.Percentage:F0}%";
                            _storageStatusLabel.ForeColor = ThemeManager.SuccessColor;
                        }
                    }
                    if (_storageProgress != null)
                    {
                        _storageProgress.Value = (float)(usage.Percentage / 100.0);
                    }
                });
            }
            else
            {
                Invoke(() =>
                {
                    if (_storageLabel != null)
                        _storageLabel.Text = "-- / --";
                    if (_storageStatusLabel != null && _storageStatusIcon != null)
                    {
                        _storageStatusIcon.IconChar = IconChar.QuestionCircle;
                        _storageStatusIcon.IconColor = ThemeManager.TextSecondaryColor;
                        _storageStatusLabel.Text = "無法取得資料";
                        _storageStatusLabel.ForeColor = ThemeManager.TextSecondaryColor;
                    }
                });
            }
        }
        catch (ApiException ex) when (ex.StatusCode == 401)
        {
            Invoke(() =>
            {
                if (_storageStatusLabel != null && _storageStatusIcon != null)
                {
                    _storageStatusIcon.IconChar = IconChar.Lock;
                    _storageStatusIcon.IconColor = ThemeManager.ErrorColor;
                    _storageStatusLabel.Text = "認證已過期";
                    _storageStatusLabel.ForeColor = ThemeManager.ErrorColor;
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Dashboard] 載入儲存空間失敗: {ex.Message}");
            Invoke(() =>
            {
                if (_storageStatusLabel != null && _storageStatusIcon != null)
                {
                    _storageStatusIcon.IconChar = IconChar.TimesCircle;
                    _storageStatusIcon.IconColor = ThemeManager.ErrorColor;
                    _storageStatusLabel.Text = "載入失敗";
                    _storageStatusLabel.ForeColor = ThemeManager.ErrorColor;
                }
            });
        }
    }

    private async Task LoadNotificationsAsync()
    {
        try
        {
            var response = await ServiceManager.Instance.Notifications.GetNotificationsAsync();

            Invoke(() =>
            {
                _invitationsContainer?.Controls.Clear();
                _notificationsContainer?.Controls.Clear();

                int contentWidth = _notificationsContainer?.Width ?? 700;
                int itemWidth = contentWidth - ItemHorizontalPadding * 2;

                if (response?.Notifications != null && response.Notifications.Any())
                {
                    _cachedNotifications = response.Notifications.ToList();

                    var invitations = response.Notifications
                        .Where(n => n.Type == "share-invite" && !n.Delivered)
                        .Take(3)
                        .ToList();

                    if (invitations.Any())
                    {
                        int x = ItemHorizontalPadding;
                        foreach (var inv in invitations)
                        {
                            var card = CreateInvitationCard(inv);
                            card.Location = new Point(x, 0);
                            _invitationsContainer?.Controls.Add(card);
                            x += card.Width + UIConstants.GapLG;
                        }
                    }
                    else
                    {
                        AddCenteredEmptyMessage(_invitationsContainer, IconChar.Share, "沒有待處理的分享邀請");
                    }

                    var notifications = response.Notifications.Take(5).ToList();
                    int y = 0;
                    foreach (var notif in notifications)
                    {
                        var item = CreateNotificationItem(notif, itemWidth);
                        item.Location = new Point(ItemHorizontalPadding, y);
                        _notificationsContainer?.Controls.Add(item);
                        y += item.Height + UIConstants.GapSM;
                    }
                }
                else
                {
                    _cachedNotifications.Clear();
                    AddCenteredEmptyMessage(_invitationsContainer, IconChar.Share, "沒有待處理的分享邀請");
                    AddCenteredEmptyMessage(_notificationsContainer, IconChar.Bell, "沒有通知");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Dashboard] 載入通知失敗: {ex.Message}");
            Invoke(() =>
            {
                AddCenteredEmptyMessage(_invitationsContainer, IconChar.ExclamationCircle, "載入失敗", true);
                AddCenteredEmptyMessage(_notificationsContainer, IconChar.ExclamationCircle, "載入失敗", true);
            });
        }
    }

    private async Task LoadRecentFilesAsync()
    {
        try
        {
            var response = await ServiceManager.Instance.Files.GetRecentFilesAsync();

            Invoke(() =>
            {
                _recentFilesContainer?.Controls.Clear();

                int contentWidth = _recentFilesContainer?.Width ?? 700;
                int itemWidth = contentWidth - ItemHorizontalPadding * 2;

                if (response?.Files != null && response.Files.Any())
                {
                    int y = 0;
                    foreach (var file in response.Files.Take(3))
                    {
                        var item = CreateRecentFileItem(file, itemWidth);
                        item.Location = new Point(ItemHorizontalPadding, y);
                        _recentFilesContainer?.Controls.Add(item);
                        y += item.Height + UIConstants.GapMD;
                    }

                    if (_recentFilesContainer != null)
                    {
                        _recentFilesContainer.Height = y + 20;
                    }
                }
                else
                {
                    AddCenteredEmptyMessage(_recentFilesContainer, IconChar.Clock, "沒有最近使用的檔案");
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Dashboard] 載入最近檔案失敗: {ex.Message}");
            Invoke(() =>
            {
                AddCenteredEmptyMessage(_recentFilesContainer, IconChar.ExclamationCircle, "載入失敗", true);
            });
        }
    }

    private void AddCenteredEmptyMessage(WinPanel? panel, IconChar icon, string message, bool isError = false)
    {
        if (panel == null) return;

        var container = new WinPanel
        {
            Size = new Size(panel.Width, 120),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(container);

        var iconBox = CreateIcon(icon, 52, isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor);
        iconBox.Location = new Point((container.Width - 58) / 2, 10);
        container.Controls.Add(iconBox);

        var label = new AntdUI.Label
        {
            Text = message,
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Regular),
            ForeColor = isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor,
            AutoSize = false,
            Size = new Size(container.Width, 35),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 75),
            BackColor = Color.Transparent
        };
        container.Controls.Add(label);

        container.Location = new Point(0, 10);
        panel.Controls.Add(container);
    }

    private WinPanel CreateInvitationCard(NotificationInfo invitation)
    {
        var card = new WinPanel
        {
            Size = new Size(300, 140),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingLG)
        };
        ThemeManager.ApplyRoundedCorners(card, 10);
        UIConstants.EnableDoubleBuffering(card);

        var senderLabel = new AntdUI.Label
        {
            Text = $"來自 {invitation.SenderInfo?.DisplayName ?? "某人"}",
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingLG, UIConstants.PaddingLG),
            BackColor = Color.Transparent
        };
        card.Controls.Add(senderLabel);

        var fileLabel = new AntdUI.Label
        {
            Text = invitation.FileInfo?.DisplayName ?? "檔案",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingLG, 48),
            BackColor = Color.Transparent
        };
        card.Controls.Add(fileLabel);

        var acceptButton = new AntdUI.Button
        {
            Text = "接受",
            Type = TTypeMini.Success,
            Location = new Point(UIConstants.PaddingLG, 95),
            Size = new Size(85, 36)
        };
        acceptButton.Click += async (s, e) => await RespondToInvitationAsync(invitation, "accept");
        card.Controls.Add(acceptButton);

        var rejectButton = new AntdUI.Button
        {
            Text = "拒絕",
            Type = TTypeMini.Error,
            Location = new Point(115, 95),
            Size = new Size(85, 36)
        };
        rejectButton.Click += async (s, e) => await RespondToInvitationAsync(invitation, "reject");
        card.Controls.Add(rejectButton);

        return card;
    }

    private WinPanel CreateNotificationItem(NotificationInfo notification, int width)
    {
        var item = new WinPanel
        {
            Size = new Size(width, 68),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingLG)
        };
        ThemeManager.ApplyRoundedCorners(item, 8);
        UIConstants.EnableDoubleBuffering(item);

        var iconBox = CreateIcon(GetNotificationIcon(notification.Type), 24, GetNotificationColor(notification.Type));
        iconBox.Location = new Point(UIConstants.PaddingLG, 20);
        item.Controls.Add(iconBox);

        var typeLabel = new AntdUI.Label
        {
            Text = notification.TypeDisplayName,
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(56, 14),
            BackColor = Color.Transparent
        };
        item.Controls.Add(typeLabel);

        var messageLabel = new AntdUI.Label
        {
            Text = notification.Message ?? notification.FileInfo?.DisplayName ?? "",
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(56, 38),
            BackColor = Color.Transparent
        };
        item.Controls.Add(messageLabel);

        var timeLabel = new AntdUI.Label
        {
            Text = notification.RelativeTime,
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(width - 160, 24),
            BackColor = Color.Transparent
        };
        item.Controls.Add(timeLabel);

        var deleteButton = new AntdUI.Button
        {
            Type = TTypeMini.Default,
            Size = new Size(32, 32),
            Location = new Point(width - 48, 18),
            IconSvg = "<svg viewBox=\"0 0 448 512\"><path d=\"M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z\"/></svg>"
        };
        deleteButton.Click += async (s, e) => await DeleteNotificationAsync(notification);
        item.Controls.Add(deleteButton);

        return item;
    }

    private async Task DeleteNotificationAsync(NotificationInfo notification)
    {
        try
        {
            _cachedNotifications.RemoveAll(n => n.Id == notification.Id);
            RefreshNotificationsUI();

            var result = await ServiceManager.Instance.Notifications.DeleteNotificationAsync(notification.Id);

            if (result == null || !result.Success)
            {
                await LoadNotificationsAsync();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Dashboard] 刪除通知失敗: {ex.Message}");
            await LoadNotificationsAsync();
        }
    }

    private void RefreshNotificationsUI()
    {
        if (InvokeRequired)
        {
            Invoke(RefreshNotificationsUI);
            return;
        }

        _invitationsContainer?.Controls.Clear();
        _notificationsContainer?.Controls.Clear();

        int contentWidth = _notificationsContainer?.Width ?? 700;
        int itemWidth = contentWidth - ItemHorizontalPadding * 2;

        if (_cachedNotifications.Any())
        {
            var invitations = _cachedNotifications
                .Where(n => n.Type == "share-invite" && !n.Delivered)
                .Take(3)
                .ToList();

            if (invitations.Any())
            {
                int x = ItemHorizontalPadding;
                foreach (var inv in invitations)
                {
                    var card = CreateInvitationCard(inv);
                    card.Location = new Point(x, 0);
                    _invitationsContainer?.Controls.Add(card);
                    x += card.Width + UIConstants.GapLG;
                }
            }
            else
            {
                AddCenteredEmptyMessage(_invitationsContainer, IconChar.Share, "沒有待處理的分享邀請");
            }

            var notifications = _cachedNotifications.Take(5).ToList();
            int y = 0;
            foreach (var notif in notifications)
            {
                var item = CreateNotificationItem(notif, itemWidth);
                item.Location = new Point(ItemHorizontalPadding, y);
                _notificationsContainer?.Controls.Add(item);
                y += item.Height + UIConstants.GapSM;
            }
        }
        else
        {
            AddCenteredEmptyMessage(_invitationsContainer, IconChar.Share, "沒有待處理的分享邀請");
            AddCenteredEmptyMessage(_notificationsContainer, IconChar.Bell, "沒有通知");
        }
    }

    private static IconChar GetNotificationIcon(string? type)
    {
        return type switch
        {
            "share-invite" => IconChar.Envelope,
            "share-accepted" => IconChar.CheckCircle,
            "share-rejected" => IconChar.TimesCircle,
            "file-downloaded" => IconChar.Download,
            "file-viewed" => IconChar.Eye,
            _ => IconChar.Bell
        };
    }

    private static Color GetNotificationColor(string? type)
    {
        return type switch
        {
            "share-invite" => ThemeManager.PrimaryColor,
            "share-accepted" => ThemeManager.SuccessColor,
            "share-rejected" => ThemeManager.ErrorColor,
            _ => ThemeManager.TextSecondaryColor
        };
    }

    private WinPanel CreateRecentFileItem(Models.FileInfo file, int width)
    {
        var item = new WinPanel
        {
            Size = new Size(width, 72),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = new Padding(UIConstants.PaddingLG),
            Cursor = Cursors.Hand
        };
        ThemeManager.ApplyRoundedCorners(item, 8);
        UIConstants.EnableDoubleBuffering(item);

        var iconBox = CreateIcon(AppIconHelper.GetFileTypeIcon(file.ContentType), 28, ThemeManager.PrimaryColor);
        iconBox.Location = new Point(UIConstants.PaddingLG, 20);
        item.Controls.Add(iconBox);

        int maxNameLength = Math.Max(20, (width - 250) / 8);
        var nameLabel = new AntdUI.Label
        {
            Text = file.DisplayName.Length > maxNameLength ? file.DisplayName[..maxNameLength] + "..." : file.DisplayName,
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(60, 16),
            BackColor = Color.Transparent
        };
        item.Controls.Add(nameLabel);

        var sizeLabel = new AntdUI.Label
        {
            Text = file.FormattedSize,
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(60, 42),
            BackColor = Color.Transparent
        };
        item.Controls.Add(sizeLabel);

        var statusLabel = new AntdUI.Label
        {
            Text = file.StatusText,
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = file.IsExpired || file.Revoked ? ThemeManager.WarningColor : ThemeManager.SuccessColor,
            AutoSize = true,
            Location = new Point(width - 100, 26),
            BackColor = Color.Transparent
        };
        item.Controls.Add(statusLabel);

        item.Click += (s, e) => ShowFileDetailDialog(file);
        foreach (Control c in item.Controls)
        {
            c.Click += (s, e) => ShowFileDetailDialog(file);
            c.Cursor = Cursors.Hand;
        }

        return item;
    }

    private void ShowFileDetailDialog(Models.FileInfo file)
    {
        using var dialog = new Forms.FileDetailForm(file);
        dialog.ShowDialog(this.ParentForm);
    }

    private async Task RespondToInvitationAsync(NotificationInfo invitation, string action)
    {
        try
        {
            await ServiceManager.Instance.Notifications.RespondToInvitationAsync(
                invitation.Id, invitation.ShareId ?? "", action);

            await LoadNotificationsAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"操作失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
