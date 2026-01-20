using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Models;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using AntdTabPage = AntdUI.TabPage;
using WinPanel = System.Windows.Forms.Panel;
using AppIconHelper = sharelock_desktop.Utils.IconHelper;

namespace sharelock_desktop.Controls;

public partial class FilesControl : UserControl
{
    private AntdUI.Tabs? _tabs;
    private AntdUI.Input? _searchInput;
    private AntdUI.Button? _uploadButton;
    private WinPanel? _filesContainer;
    private WinPanel? _emptyPanel;

    private string _currentTab = "myFiles";
    private string _searchQuery = "";
    private List<Models.FileInfo> _allFiles = new();
    private bool _isLoading;
    private bool _isInitialized;
    private int _lastWidth;

    private const int CardWidth = 320;
    private const int CardHeight = 220;
    private const int CardGap = 20;

    public FilesControl()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.UserPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw, true);
        UpdateStyles();

        InitializeComponent();
        Load += FilesControl_Load;
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        BackColor = ThemeManager.BackgroundColor;
        Dock = DockStyle.Fill;
        Padding = new Padding(0);

        ResumeLayout(false);
    }

    private void BuildUI()
    {
        SuspendLayout();
        Controls.Clear();

        int currentY = 0;
        int contentWidth = Width - SystemInformation.VerticalScrollBarWidth;
        if (contentWidth < 400) contentWidth = 400;

        var folderIcon = new IconPictureBox
        {
            IconChar = IconChar.Folder,
            IconSize = 34,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(40, 40),
            Location = new Point(0, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(folderIcon);

        var titleLabel = new AntdUI.Label
        {
            Text = "我的檔案",
            Font = new Font(ThemeManager.FontFamily, 24, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(48, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(titleLabel);

        _uploadButton = new AntdUI.Button
        {
            Text = "上傳檔案",
            Type = TTypeMini.Primary,
            Size = new Size(130, 44),
            Location = new Point(contentWidth - 130, currentY - 4),
            IconSvg = "<svg viewBox=\"0 0 512 512\"><path d=\"M288 109.3V352c0 17.7-14.3 32-32 32s-32-14.3-32-32V109.3l-73.4 73.4c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3l128-128c12.5-12.5 32.8-12.5 45.3 0l128 128c12.5 12.5 12.5 32.8 0 45.3s-32.8 12.5-45.3 0L288 109.3zM64 352H192c0 35.3 28.7 64 64 64s64-28.7 64-64H448c35.3 0 64 28.7 64 64v32c0 35.3-28.7 64-64 64H64c-35.3 0-64-28.7-64-64V416c0-35.3 28.7-64 64-64zM432 456a24 24 0 1 0 0-48 24 24 0 1 0 0 48z\"/></svg>"
        };
        _uploadButton.Click += UploadButton_Click;
        Controls.Add(_uploadButton);

        currentY += 60;

        _tabs = new AntdUI.Tabs
        {
            Location = new Point(0, currentY),
            Size = new Size(450, 48),
            Type = TabType.Card
        };

        _tabs.Pages.Add(new AntdTabPage { Text = "我的檔案", Name = "myFiles" });
        _tabs.Pages.Add(new AntdTabPage { Text = "與我共用", Name = "sharedWithMe" });
        _tabs.Pages.Add(new AntdTabPage { Text = "已失效", Name = "expired" });

        _tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
        Controls.Add(_tabs);

        _searchInput = new AntdUI.Input
        {
            PlaceholderText = "搜尋檔案...",
            Size = new Size(280, 44),
            Location = new Point(contentWidth - 280, currentY + 2)
        };
        _searchInput.TextChanged += SearchInput_TextChanged;
        Controls.Add(_searchInput);

        currentY += 70;

        int containerHeight = Height - currentY - 20;
        if (containerHeight < 200) containerHeight = 200;

        _filesContainer = new WinPanel
        {
            Location = new Point(0, currentY),
            Size = new Size(contentWidth, containerHeight),
            BackColor = Color.Transparent,
            AutoScroll = true
        };
        UIConstants.EnableDoubleBuffering(_filesContainer);
        Controls.Add(_filesContainer);

        _emptyPanel = new WinPanel
        {
            Size = new Size(contentWidth, 200),
            BackColor = Color.Transparent,
            Visible = false
        };
        UIConstants.EnableDoubleBuffering(_emptyPanel);
        Controls.Add(_emptyPanel);

        ResumeLayout(true);
    }

    private void UploadButton_Click(object? sender, EventArgs e)
    {
        using var uploadForm = new Forms.UploadFileForm();
        uploadForm.UploadCompleted += async (s, args) =>
        {
            await LoadFilesAsync();
        };

        if (uploadForm.ShowDialog(this.ParentForm) == DialogResult.OK)
        {
            _ = LoadFilesAsync();
        }
    }

    private async void FilesControl_Load(object? sender, EventArgs e)
    {
        if (_isInitialized) return;
        _isInitialized = true;
        _lastWidth = Width;

        BuildUI();
        UpdateEmptyPanelPosition();
        await LoadFilesAsync();
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        if (!_isInitialized) return;

        if (Math.Abs(Width - _lastWidth) > 30)
        {
            _lastWidth = Width;
            UpdateLayoutSizes();
            FilterFiles();
        }
    }

    private void UpdateLayoutSizes()
    {
        int contentWidth = Width - SystemInformation.VerticalScrollBarWidth;
        if (contentWidth < 400) contentWidth = 400;

        if (_uploadButton != null)
            _uploadButton.Location = new Point(contentWidth - 130, _uploadButton.Location.Y);

        if (_searchInput != null)
            _searchInput.Location = new Point(contentWidth - 280, _searchInput.Location.Y);

        if (_filesContainer != null)
        {
            int containerHeight = Height - _filesContainer.Location.Y - 20;
            if (containerHeight < 200) containerHeight = 200;
            _filesContainer.Size = new Size(contentWidth, containerHeight);
        }

        if (_emptyPanel != null)
        {
            _emptyPanel.Size = new Size(contentWidth, 200);
        }

        UpdateEmptyPanelPosition();
    }

    private void UpdateEmptyPanelPosition()
    {
        if (_emptyPanel == null || _filesContainer == null) return;

        _emptyPanel.Location = new Point(0, _filesContainer.Location.Y + 50);
    }

    private void Tabs_SelectedIndexChanged(object? sender, IntEventArgs e)
    {
        _currentTab = e.Value switch
        {
            0 => "myFiles",
            1 => "sharedWithMe",
            2 => "expired",
            _ => "myFiles"
        };

        _ = LoadFilesAsync();
    }

    private void SearchInput_TextChanged(object? sender, EventArgs e)
    {
        _searchQuery = _searchInput?.Text ?? "";
        FilterFiles();
    }

    private async Task LoadFilesAsync()
    {
        if (_isLoading) return;
        _isLoading = true;

        try
        {
            ShowLoading();

            var response = await ServiceManager.Instance.Files.GetFilesAsync(_currentTab);

            if (response?.Files != null)
            {
                _allFiles = response.Files;
                FilterFiles();
            }
            else
            {
                _allFiles = new List<Models.FileInfo>();
                FilterFiles();
            }
        }
        catch (ApiException ex) when (ex.StatusCode == 404)
        {
            ShowCenteredEmptyState(IconChar.ExclamationCircle, "API 暫時無法使用", true);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"載入檔案失敗: {ex.Message}");
            ShowCenteredEmptyState(IconChar.ExclamationCircle, "載入失敗，請稍後再試", true);
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void ShowLoading()
    {
        if (InvokeRequired)
        {
            Invoke(ShowLoading);
            return;
        }

        _filesContainer?.Controls.Clear();
        _filesContainer!.Visible = false;
        ShowCenteredEmptyState(IconChar.Spinner, "載入中...");
    }

    private void ShowCenteredEmptyState(IconChar icon, string message, bool isError = false)
    {
        if (InvokeRequired)
        {
            Invoke(() => ShowCenteredEmptyState(icon, message, isError));
            return;
        }

        _filesContainer!.Visible = false;
        _emptyPanel!.Controls.Clear();

        int panelWidth = _emptyPanel.Width;

        var iconBox = new IconPictureBox
        {
            IconChar = icon,
            IconSize = 72,
            IconColor = isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor,
            Size = new Size(80, 80),
            Location = new Point((panelWidth - 80) / 2, 20),
            BackColor = Color.Transparent
        };
        _emptyPanel.Controls.Add(iconBox);

        var label = new AntdUI.Label
        {
            Text = message,
            Font = new Font(ThemeManager.FontFamily, 14f, FontStyle.Regular),
            ForeColor = isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor,
            AutoSize = false,
            Size = new Size(panelWidth, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 110),
            BackColor = Color.Transparent
        };
        _emptyPanel.Controls.Add(label);

        UpdateEmptyPanelPosition();
        _emptyPanel.Visible = true;
    }

    private void FilterFiles()
    {
        if (InvokeRequired)
        {
            Invoke(FilterFiles);
            return;
        }

        _filesContainer?.SuspendLayout();
        _filesContainer?.Controls.Clear();

        var filteredFiles = string.IsNullOrWhiteSpace(_searchQuery)
            ? _allFiles
            : _allFiles.Where(f => f.DisplayName.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filteredFiles.Count == 0)
        {
            _filesContainer?.ResumeLayout(false);
            _filesContainer!.Visible = false;

            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                var (icon, message) = GetEmptyMessageForTab();
                ShowCenteredEmptyState(icon, message);
            }
            else
            {
                ShowCenteredEmptyState(IconChar.Search, "沒有符合搜尋條件的檔案");
            }
        }
        else
        {
            _emptyPanel!.Visible = false;

            int containerWidth = _filesContainer?.Width ?? 800;
            int cardsPerRow = Math.Max(1, (containerWidth + CardGap) / (CardWidth + CardGap));

            int x = 0;
            int y = 0;
            int col = 0;

            foreach (var file in filteredFiles)
            {
                var card = CreateFileCard(file);
                card.Location = new Point(x, y);
                _filesContainer?.Controls.Add(card);

                col++;
                if (col >= cardsPerRow)
                {
                    col = 0;
                    x = 0;
                    y += CardHeight + CardGap;
                }
                else
                {
                    x += CardWidth + CardGap;
                }
            }

            _filesContainer?.ResumeLayout(true);
            _filesContainer!.Visible = true;
        }
    }

    private (IconChar icon, string message) GetEmptyMessageForTab()
    {
        return _currentTab switch
        {
            "myFiles" => (IconChar.FolderOpen, "您還沒有上傳任何檔案"),
            "sharedWithMe" => (IconChar.Users, "沒有人與您分享檔案"),
            "expired" => (IconChar.Clock, "沒有已失效的檔案"),
            _ => (IconChar.FolderOpen, "目前沒有檔案")
        };
    }

    private WinPanel CreateFileCard(Models.FileInfo file)
    {
        var card = new WinPanel
        {
            Size = new Size(CardWidth, CardHeight),
            BackColor = ThemeManager.CardBackgroundColor,
            Cursor = Cursors.Hand,
            Padding = new Padding(UIConstants.PaddingLG)
        };
        ThemeManager.ApplyRoundedCorners(card, 12);
        UIConstants.EnableDoubleBuffering(card);

        var fileIcon = new IconPictureBox
        {
            IconChar = AppIconHelper.GetFileTypeIcon(file.ContentType),
            IconSize = 32,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(38, 38),
            Location = new Point(UIConstants.PaddingLG, UIConstants.PaddingLG),
            BackColor = Color.Transparent
        };
        card.Controls.Add(fileIcon);

        var nameLabel = new AntdUI.Label
        {
            Text = file.DisplayName.Length > 26 ? file.DisplayName[..26] + "..." : file.DisplayName,
            Font = new Font(ThemeManager.FontFamily, 12f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingLG + 48, UIConstants.PaddingLG + 4),
            BackColor = Color.Transparent
        };
        if (file.DisplayName.Length > 26)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(nameLabel, file.DisplayName);
        }
        card.Controls.Add(nameLabel);

        var sizeLabel = new AntdUI.Label
        {
            Text = file.FormattedSize,
            Font = new Font(ThemeManager.FontFamily, 10f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingLG + 48, UIConstants.PaddingLG + 28),
            BackColor = Color.Transparent
        };
        card.Controls.Add(sizeLabel);

        var modeTag = new AntdUI.Tag
        {
            Text = GetShareModeText(file.ShareMode),
            Size = new Size(60, 26),
            Location = new Point(UIConstants.PaddingLG, 85)
        };
        card.Controls.Add(modeTag);

        var statusTag = new AntdUI.Tag
        {
            Text = file.StatusText,
            Size = new Size(70, 26),
            Location = new Point(90, 85),
            Type = file.IsExpired || file.Revoked ? TTypeMini.Warn : TTypeMini.Success
        };
        card.Controls.Add(statusTag);

        var viewIcon = new IconPictureBox
        {
            IconChar = IconChar.Eye,
            IconSize = 16,
            IconColor = ThemeManager.TextSecondaryColor,
            Size = new Size(20, 20),
            Location = new Point(UIConstants.PaddingLG, 125),
            BackColor = Color.Transparent
        };
        card.Controls.Add(viewIcon);

        var viewLabel = new AntdUI.Label
        {
            Text = $"{file.ViewCount}",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(42, 125),
            BackColor = Color.Transparent
        };
        card.Controls.Add(viewLabel);

        var downloadIcon = new IconPictureBox
        {
            IconChar = IconChar.Download,
            IconSize = 16,
            IconColor = ThemeManager.TextSecondaryColor,
            Size = new Size(20, 20),
            Location = new Point(85, 125),
            BackColor = Color.Transparent
        };
        card.Controls.Add(downloadIcon);

        var downloadLabel = new AntdUI.Label
        {
            Text = $"{file.DownloadCount}",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(107, 125),
            BackColor = Color.Transparent
        };
        card.Controls.Add(downloadLabel);

        int buttonY = 165;

        var downloadButton = new AntdUI.Button
        {
            Text = "下載",
            Type = TTypeMini.Primary,
            Size = new Size(75, 38),
            Location = new Point(UIConstants.PaddingLG, buttonY),
            Enabled = !file.IsExpired && !file.Revoked
        };
        downloadButton.Click += async (s, e) => await DownloadFileAsync(file);
        card.Controls.Add(downloadButton);

        if (_currentTab == "myFiles" || _currentTab == "expired")
        {
            var deleteButton = new AntdUI.Button
            {
                Text = "刪除",
                Type = TTypeMini.Error,
                Size = new Size(75, 38),
                Location = new Point(105, buttonY)
            };
            deleteButton.Click += async (s, e) => await DeleteFileAsync(file);
            card.Controls.Add(deleteButton);
        }

        if (_currentTab == "myFiles" && !file.IsExpired && !file.Revoked && !string.IsNullOrEmpty(file.ShareId))
        {
            var copyLinkButton = new AntdUI.Button
            {
                Text = "複製連結",
                Type = TTypeMini.Default,
                Size = new Size(100, 38),
                Location = new Point(195, buttonY)
            };
            copyLinkButton.Click += (s, e) => CopyShareLink(file);
            card.Controls.Add(copyLinkButton);
        }

        card.Click += (s, e) => ShowFileDetail(file);

        return card;
    }

    private void ShowFileDetail(Models.FileInfo file)
    {
        using var dialog = new Forms.FileDetailForm(file);
        dialog.ShowDialog(this.ParentForm);
    }

    private async Task DownloadFileAsync(Models.FileInfo file)
    {
        try
        {
            var response = await ServiceManager.Instance.Files.InitiateDownloadAsync(
                file.Id, file.ShareId);

            if (response == null)
            {
                MessageBox.Show("無法取得下載資訊", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (response.RequiresVerification)
            {
                var url = $"{FirebaseConfig.WebAppUrl}{response.RedirectUrl}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
                return;
            }

            if (!string.IsNullOrEmpty(response.DownloadUrl))
            {
                using var saveDialog = new SaveFileDialog
                {
                    FileName = file.DisplayName,
                    Title = "儲存檔案"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await ServiceManager.Instance.Download.DownloadFileAsync(
                        response.DownloadUrl, saveDialog.FileName);

                    MessageBox.Show("下載完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"下載失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task DeleteFileAsync(Models.FileInfo file)
    {
        var result = MessageBox.Show($"確定要刪除「{file.DisplayName}」嗎？\n此操作無法復原。",
            "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result != DialogResult.Yes) return;

        try
        {
            await ServiceManager.Instance.Files.DeleteFileAsync(file.Id);
            MessageBox.Show("刪除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            await LoadFilesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"刪除失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CopyShareLink(Models.FileInfo file)
    {
        if (string.IsNullOrEmpty(file.ShareId)) return;

        var shareUrl = $"{FirebaseConfig.WebAppUrl}/share/{file.ShareId}";
        Clipboard.SetText(shareUrl);
        MessageBox.Show("分享連結已複製到剪貼簿", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static string GetShareModeText(string? shareMode)
    {
        return shareMode switch
        {
            "public" => "公開",
            "pin" => "密碼",
            "account" => "帳號",
            "device" => "裝置",
            _ => "公開"
        };
    }
}
