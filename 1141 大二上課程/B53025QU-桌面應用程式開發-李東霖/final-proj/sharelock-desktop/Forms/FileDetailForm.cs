using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;
using AppIconHelper = sharelock_desktop.Utils.IconHelper;
using FileInfoModel = sharelock_desktop.Models.FileInfo;

namespace sharelock_desktop.Forms;
public class FileDetailForm : Form
{
    private readonly FileInfoModel _file;

    public FileDetailForm(FileInfoModel file)
    {
        _file = file;
        InitializeComponent();
        ThemeManager.ApplyThemeToForm(this);
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        Text = "檔案詳情";
        Size = new Size(500, 520);
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

        int currentY = UIConstants.PaddingXL;
        int contentWidth = ClientSize.Width - UIConstants.PaddingXL * 2;

        var fileIcon = new IconPictureBox
        {
            IconChar = AppIconHelper.GetFileTypeIcon(_file.ContentType),
            IconSize = 48,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(56, 56),
            Location = new Point(UIConstants.PaddingXL, currentY),
            BackColor = Color.Transparent
        };
        Controls.Add(fileIcon);

        var displayFileName = TruncateFileName(_file.DisplayName, 20);

        var nameLabel = new AntdUI.Label
        {
            Text = displayFileName,
            Font = new Font(ThemeManager.FontFamily, 12, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = false,
            Size = new Size(contentWidth - 70, 30),
            Location = new Point(UIConstants.PaddingXL + 66, currentY + 5),
            BackColor = Color.Transparent
        };

        if (displayFileName != _file.DisplayName)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(nameLabel, _file.DisplayName);
        }
        Controls.Add(nameLabel);

        var sizeLabel = new AntdUI.Label
        {
            Text = _file.FormattedSize,
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingXL + 66, currentY + 35),
            BackColor = Color.Transparent
        };
        Controls.Add(sizeLabel);

        currentY += 80;

        var separator1 = new WinPanel
        {
            Size = new Size(contentWidth, 1),
            Location = new Point(UIConstants.PaddingXL, currentY),
            BackColor = ThemeManager.BorderColor
        };
        Controls.Add(separator1);
        currentY += 20;

        currentY = AddInfoRow("檔案類型", _file.ContentType ?? "未知", currentY, contentWidth);
        currentY = AddInfoRow("分享模式", GetShareModeText(_file.ShareMode), currentY, contentWidth);
        currentY = AddInfoRow("狀態", _file.StatusText, currentY, contentWidth,
            _file.IsExpired || _file.Revoked ? ThemeManager.WarningColor : ThemeManager.SuccessColor);
        currentY = AddInfoRow("瀏覽次數", _file.ViewCount.ToString(), currentY, contentWidth);
        currentY = AddInfoRow("下載次數", _file.DownloadCount.ToString(), currentY, contentWidth);

        if (_file.RemainingDownloads.HasValue && _file.MaxDownloads.HasValue)
        {
            currentY = AddInfoRow("剩餘下載", $"{_file.RemainingDownloads} / {_file.MaxDownloads}", currentY, contentWidth);
        }

        if (!string.IsNullOrEmpty(_file.OwnerEmail))
        {
            currentY = AddInfoRow("擁有者", _file.OwnerEmail, currentY, contentWidth);
        }

        currentY += 20;

        var separator2 = new WinPanel
        {
            Size = new Size(contentWidth, 1),
            Location = new Point(UIConstants.PaddingXL, currentY),
            BackColor = ThemeManager.BorderColor
        };
        Controls.Add(separator2);
        currentY += 25;

        int buttonY = ClientSize.Height - 70;
        int buttonWidth = 120;
        int buttonGap = 16;

        var downloadButton = new AntdUI.Button
        {
            Text = "下載檔案",
            Type = TTypeMini.Primary,
            Size = new Size(buttonWidth, 42),
            Location = new Point(UIConstants.PaddingXL, buttonY),
            Enabled = !_file.IsExpired && !_file.Revoked
        };
        downloadButton.Click += async (s, e) => await DownloadFileAsync();
        Controls.Add(downloadButton);

        if (!string.IsNullOrEmpty(_file.ShareId))
        {
            var copyLinkButton = new AntdUI.Button
            {
                Text = "複製連結",
                Type = TTypeMini.Default,
                Size = new Size(buttonWidth, 42),
                Location = new Point(UIConstants.PaddingXL + buttonWidth + buttonGap, buttonY)
            };
            copyLinkButton.Click += (s, e) => CopyShareLink();
            Controls.Add(copyLinkButton);
        }

        var closeButton = new AntdUI.Button
        {
            Text = "關閉",
            Type = TTypeMini.Default,
            Size = new Size(100, 42),
            Location = new Point(contentWidth - 80, buttonY)
        };
        closeButton.Click += (s, e) => Close();
        Controls.Add(closeButton);

        ResumeLayout(false);
    }
    private static string TruncateFileName(string? fileName, int maxLength)
    {
        if (string.IsNullOrEmpty(fileName))
            return "未知檔案";

        var extension = Path.GetExtension(fileName);
        var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

        if (nameWithoutExt.Length <= maxLength)
            return fileName;

        int keepLength = (maxLength - 3) / 2;
        if (keepLength < 3) keepLength = 3;

        var front = nameWithoutExt.Substring(0, keepLength);
        var back = nameWithoutExt.Substring(nameWithoutExt.Length - keepLength);

        return $"{front}...{back}{extension}";
    }

    private int AddInfoRow(string label, string value, int y, int width, Color? valueColor = null)
    {
        var labelControl = new AntdUI.Label
        {
            Text = label,
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Regular),
            ForeColor = ThemeManager.TextSecondaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingXL, y),
            BackColor = Color.Transparent
        };
        Controls.Add(labelControl);

        var valueControl = new AntdUI.Label
        {
            Text = value,
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Bold),
            ForeColor = valueColor ?? ThemeManager.TextPrimaryColor,
            AutoSize = true,
            Location = new Point(UIConstants.PaddingXL + 120, y),
            BackColor = Color.Transparent
        };
        Controls.Add(valueControl);

        return y + 32;
    }

    private static string GetShareModeText(string? shareMode)
    {
        return shareMode switch
        {
            "public" => "公開",
            "pin" => "密碼保護",
            "account" => "帳號綁定",
            "device" => "裝置綁定",
            _ => "公開"
        };
    }

    private async Task DownloadFileAsync()
    {
        try
        {

            bool isSharedFile = !string.IsNullOrEmpty(_file.ShareId) &&
                               !string.IsNullOrEmpty(_file.OwnerEmail);

            if (isSharedFile)
            {

                var url = $"{FirebaseConfig.WebAppUrl}/share/{_file.ShareId}";
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
                Close();
                return;
            }

            var response = await ServiceManager.Instance.Files.InitiateDownloadAsync(_file.Id, _file.ShareId);

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
                Close();
                return;
            }

            if (!string.IsNullOrEmpty(response.DownloadUrl))
            {
                using var saveDialog = new SaveFileDialog
                {
                    FileName = _file.DisplayName,
                    Title = "儲存檔案"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await ServiceManager.Instance.Download.DownloadFileAsync(
                        response.DownloadUrl, saveDialog.FileName);

                    MessageBox.Show("下載完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"下載失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CopyShareLink()
    {
        if (string.IsNullOrEmpty(_file.ShareId)) return;

        var shareUrl = $"{FirebaseConfig.WebAppUrl}/share/{_file.ShareId}";
        Clipboard.SetText(shareUrl);
        MessageBox.Show("分享連結已複製到剪貼簿", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
