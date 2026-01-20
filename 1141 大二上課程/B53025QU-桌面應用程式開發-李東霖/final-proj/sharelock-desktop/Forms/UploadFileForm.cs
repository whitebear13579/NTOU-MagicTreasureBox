using AntdUI;
using FontAwesome.Sharp;
using sharelock_desktop.Models;
using sharelock_desktop.Services;
using sharelock_desktop.Utils;

using WinPanel = System.Windows.Forms.Panel;
using WinLabel = System.Windows.Forms.Label;

namespace sharelock_desktop.Forms;

public class UploadFileForm : Form
{
    private const int MaxDays = 14;
    private const int MaxRecipients = 10;
    private const long MaxFileSize = 300 * 1024 * 1024;
    private const int FormPadding = 24;
    private const int CopyButtonSize = 36;
    private const int ContainerRadius = 8;

    private WinPanel? _contentContainer;
    private AntdUI.Button? _cancelButton;
    private AntdUI.Button? _nextButton;

    private WinPanel? _dropZoneContainer;
    private WinLabel? _selectedFileLabel;
    private WinLabel? _selectedFileSizeLabel;

    private AntdUI.Progress? _uploadProgress;
    private WinLabel? _uploadStatusLabel;

    private AntdUI.Input? _displayNameInput;
    private AntdUI.Select? _shareModeSelect;
    private AntdUI.InputNumber? _maxDownloadsInput;
    private AntdUI.DatePicker? _expiresDatePicker;
    private WinPanel? _pinContainer;
    private WinLabel? _pinValueLabel;

    private WinLabel? _shareUrlLabel;
    private WinLabel? _shareIdLabel;

    private int _currentStep = 1;
    private string? _selectedFilePath;
    private string? _uploadedStoragePath;
    private long _uploadedFileSize;
    private string? _generatedPin;
    private string? _shareUrl;
    private string? _shareId;
    private CancellationTokenSource? _uploadCts;
    private bool _isUploading;
    private bool _isCreatingShare;

    private readonly List<AntdUI.Panel> _stepCircles = new();
    private readonly List<WinLabel> _stepNumbers = new();
    private readonly List<WinLabel> _stepTexts = new();

    public event EventHandler<UploadCompletedEventArgs>? UploadCompleted;

    public UploadFileForm()
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

        Text = "上傳檔案";
        Size = new Size(520, 700);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = ThemeManager.BackgroundColor;

        var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sharelock.ico");
        if (File.Exists(iconPath))
        {
            Icon = new System.Drawing.Icon(iconPath);
        }

        BuildUI();
        ShowStep1_SelectFile();

        ResumeLayout(false);
    }

    private void BuildUI()
    {
        int contentWidth = ClientSize.Width - FormPadding * 2;
        int currentY = FormPadding;

        var titleLabel = new WinLabel
        {
            Name = "TitleLabel",
            Text = "上傳檔案",
            Font = new Font(ThemeManager.FontFamily, 18, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = ThemeManager.BackgroundColor,
            Location = new Point(FormPadding, currentY),
            Size = new Size(contentWidth, 36),
            TextAlign = ContentAlignment.MiddleCenter
        };
        Controls.Add(titleLabel);
        currentY += 60;

        DrawStepIndicator(currentY, contentWidth);
        currentY += 70;

        int buttonY = ClientSize.Height - FormPadding - 50;
        int contentHeight = buttonY - currentY - 12;
        
        _contentContainer = new WinPanel
        {
            Location = new Point(FormPadding, currentY),
            Size = new Size(contentWidth, contentHeight),
            BackColor = Color.Transparent
        };
        UIConstants.EnableDoubleBuffering(_contentContainer);
        Controls.Add(_contentContainer);

        _cancelButton = new AntdUI.Button
        {
            Text = "取消",
            Type = TTypeMini.Default,
            Size = new Size(100, 44),
            Location = new Point(FormPadding, buttonY)
        };
        _cancelButton.Click += CancelButton_Click;
        Controls.Add(_cancelButton);

        _nextButton = new AntdUI.Button
        {
            Text = "選擇檔案",
            Type = TTypeMini.Primary,
            Size = new Size(130, 44),
            Location = new Point(ClientSize.Width - FormPadding - 130, buttonY)
        };
        _nextButton.Click += NextButton_Click;
        Controls.Add(_nextButton);
    }

    private void DrawStepIndicator(int y, int width)
    {
        var steps = new[] { "選擇檔案", "上傳中", "分享設定", "完成" };
        int stepWidth = width / steps.Length;

        _stepCircles.Clear();
        _stepNumbers.Clear();
        _stepTexts.Clear();

        for (int i = 0; i < steps.Length; i++)
        {
            int stepNum = i + 1;
            int centerX = FormPadding + i * stepWidth + stepWidth / 2;

            var circlePanel = new AntdUI.Panel
            {
                Size = new Size(32, 32),
                Location = new Point(centerX - 16, y),
                Back = ThemeManager.CardBackgroundColor,
                Radius = 16
            };

            var numLabel = new WinLabel
            {
                Text = stepNum.ToString(),
                Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Bold),
                ForeColor = ThemeManager.TextSecondaryColor,
                BackColor = Color.Transparent,
                Size = new Size(32, 32),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 0)
            };
            circlePanel.Controls.Add(numLabel);
            Controls.Add(circlePanel);

            _stepCircles.Add(circlePanel);
            _stepNumbers.Add(numLabel);

            var textLabel = new WinLabel
            {
                Text = steps[i],
                Font = new Font(ThemeManager.FontFamily, 9f),
                ForeColor = ThemeManager.TextSecondaryColor,
                BackColor = Color.Transparent,
                Size = new Size(stepWidth, 22),
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(FormPadding + i * stepWidth, y + 38)
            };
            Controls.Add(textLabel);
            _stepTexts.Add(textLabel);
        }
    }

    private void UpdateStepIndicator()
    {
        for (int i = 0; i < 4; i++)
        {
            bool isActive = (i + 1) <= _currentStep;
            bool isCurrent = (i + 1) == _currentStep;

            if (i < _stepCircles.Count)
            {
                _stepCircles[i].Back = isActive ? ThemeManager.PrimaryColor : ThemeManager.CardBackgroundColor;
            }
            if (i < _stepNumbers.Count)
            {
                _stepNumbers[i].ForeColor = isActive ? Color.White : ThemeManager.TextSecondaryColor;
            }
            if (i < _stepTexts.Count)
            {
                _stepTexts[i].ForeColor = isActive ? ThemeManager.TextPrimaryColor : ThemeManager.TextSecondaryColor;
                _stepTexts[i].Font = new Font(ThemeManager.FontFamily, 9f, isCurrent ? FontStyle.Bold : FontStyle.Regular);
            }
        }
    }

    private void ShowStep1_SelectFile()
    {
        _contentContainer?.Controls.Clear();
        _currentStep = 1;
        UpdateStepIndicator();

        int panelWidth = _contentContainer?.Width ?? 440;

        _dropZoneContainer = new WinPanel
        {
            Size = new Size(panelWidth, 180),
            Location = new Point(0, 10),
            BackColor = ThemeManager.CardBackgroundColor,
            Cursor = Cursors.Hand
        };
        ThemeManager.ApplyRoundedCorners(_dropZoneContainer, 12);
        UIConstants.EnableDoubleBuffering(_dropZoneContainer);

        var uploadIcon = new IconPictureBox
        {
            IconChar = IconChar.CloudUploadAlt,
            IconSize = 52,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(60, 60),
            Location = new Point((panelWidth - 60) / 2, 28),
            BackColor = Color.Transparent
        };
        _dropZoneContainer.Controls.Add(uploadIcon);

        var hintLabel = new WinLabel
        {
            Text = "點擊選擇檔案或拖放檔案到此處",
            Font = new Font(ThemeManager.FontFamily, 12f),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 28),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 98)
        };
        _dropZoneContainer.Controls.Add(hintLabel);

        var sizeHintLabel = new WinLabel
        {
            Text = "單一檔案最大 300MB",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 24),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 132)
        };
        _dropZoneContainer.Controls.Add(sizeHintLabel);

        _dropZoneContainer.Click += (s, e) => SelectFile();
        _dropZoneContainer.AllowDrop = true;
        _dropZoneContainer.DragEnter += DropZone_DragEnter;
        _dropZoneContainer.DragLeave += DropZone_DragLeave;
        _dropZoneContainer.DragDrop += DropZone_DragDrop;

        _contentContainer?.Controls.Add(_dropZoneContainer);

        _selectedFileLabel = new WinLabel
        {
            Text = "",
            Font = new Font(ThemeManager.FontFamily, 11f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, 205),
            Size = new Size(panelWidth, 24),
            Visible = false
        };
        _contentContainer?.Controls.Add(_selectedFileLabel);

        _selectedFileSizeLabel = new WinLabel
        {
            Text = "",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, 232),
            Size = new Size(panelWidth, 22),
            Visible = false
        };
        _contentContainer?.Controls.Add(_selectedFileSizeLabel);

        _nextButton!.Text = "選擇檔案";
        _nextButton.Enabled = true;
        _cancelButton!.Text = "取消";
        _cancelButton.Visible = true;
    }

    private void DropZone_DragEnter(object? sender, DragEventArgs e)
    {
        if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
        {
            e.Effect = DragDropEffects.Copy;
            if (_dropZoneContainer != null)
            {
                _dropZoneContainer.BackColor = Color.FromArgb(60, ThemeManager.PrimaryColor.R, ThemeManager.PrimaryColor.G, ThemeManager.PrimaryColor.B);
            }
        }
    }

    private void DropZone_DragLeave(object? sender, EventArgs e)
    {
        if (_dropZoneContainer != null)
        {
            _dropZoneContainer.BackColor = ThemeManager.CardBackgroundColor;
        }
    }

    private void DropZone_DragDrop(object? sender, DragEventArgs e)
    {
        if (_dropZoneContainer != null)
        {
            _dropZoneContainer.BackColor = ThemeManager.CardBackgroundColor;
        }

        var files = e.Data?.GetData(DataFormats.FileDrop) as string[];
        if (files?.Length > 0)
        {
            HandleFileSelected(files[0]);
        }
    }

    private void SelectFile()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "選擇要上傳的檔案",
            Filter = "所有檔案 (*.*)|*.*"
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            HandleFileSelected(dialog.FileName);
        }
    }

    private void HandleFileSelected(string filePath)
    {
        var fileInfo = new System.IO.FileInfo(filePath);

        if (!fileInfo.Exists)
        {
            AntdUI.Message.error(this, "檔案不存在", autoClose: 3);
            return;
        }

        if (fileInfo.Length > MaxFileSize)
        {
            AntdUI.Message.error(this, $"檔案大小超過限制（最大 {MaxFileSize / 1024 / 1024}MB）", autoClose: 3);
            return;
        }

        _selectedFilePath = filePath;

        _selectedFileLabel!.Text = $"已選擇：{TruncateFileName(fileInfo.Name, 45)}";
        _selectedFileLabel.Visible = true;

        _selectedFileSizeLabel!.Text = $"大小：{FormatFileSize(fileInfo.Length)}";
        _selectedFileSizeLabel.Visible = true;

        _nextButton!.Text = "開始上傳";
    }

    private async void ShowStep2_Uploading()
    {
        if (string.IsNullOrEmpty(_selectedFilePath))
        {
            AntdUI.Message.warn(this, "請先選擇檔案", autoClose: 2);
            return;
        }

        _contentContainer?.Controls.Clear();
        _currentStep = 2;
        UpdateStepIndicator();

        int panelWidth = _contentContainer?.Width ?? 440;
        var fileInfo = new System.IO.FileInfo(_selectedFilePath);

        var fileNameLabel = new WinLabel
        {
            Text = TruncateFileName(fileInfo.Name, 45),
            Font = new Font(ThemeManager.FontFamily, 14f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 32),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 60)
        };
        _contentContainer?.Controls.Add(fileNameLabel);

        var fileSizeLabel = new WinLabel
        {
            Text = FormatFileSize(fileInfo.Length),
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 24),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 95)
        };
        _contentContainer?.Controls.Add(fileSizeLabel);

        _uploadProgress = new AntdUI.Progress
        {
            Location = new Point(30, 150),
            Size = new Size(panelWidth - 60, 24),
            Value = 0,
            Shape = TShapeProgress.Round
        };
        _contentContainer?.Controls.Add(_uploadProgress);

        _uploadStatusLabel = new WinLabel
        {
            Text = "準備上傳...",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 26),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 190)
        };
        _contentContainer?.Controls.Add(_uploadStatusLabel);

        _nextButton!.Text = "上傳中...";
        _nextButton.Enabled = false;
        _cancelButton!.Text = "取消上傳";

        await StartUploadAsync();
    }

    private async Task StartUploadAsync()
    {
        _isUploading = true;
        _uploadCts = new CancellationTokenSource();

        try
        {
            var progress = new Progress<UploadProgressInfo>(info =>
            {
                if (_uploadProgress != null)
                {
                    _uploadProgress.Value = (float)info.Percentage;
                }
                if (_uploadStatusLabel != null)
                {
                    _uploadStatusLabel.Text = $"{info.Status} ({info.Percentage:F0}%)";
                }
            });

            var userId = SessionManager.Instance.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("請先登入");
            }

            var result = await ServiceManager.Instance.Upload.UploadFileAsync(
                _selectedFilePath!,
                userId,
                progress,
                _uploadCts.Token);

            if (result.Success)
            {
                _uploadedStoragePath = result.StoragePath;
                _uploadedFileSize = result.FileSize;
                ShowStep3_ShareSettings();
            }
            else
            {
                throw new Exception(result.Error ?? "上傳失敗");
            }
        }
        catch (OperationCanceledException)
        {
            ShowStep1_SelectFile();
            AntdUI.Message.info(this, "上傳已取消", autoClose: 2);
        }
        catch (Exception ex)
        {
            ShowStep1_SelectFile();
            AntdUI.Message.error(this, $"上傳失敗：{ex.Message}", autoClose: 4);
        }
        finally
        {
            _isUploading = false;
            _uploadCts?.Dispose();
            _uploadCts = null;
        }
    }

    private void ShowStep3_ShareSettings()
    {
        _contentContainer?.Controls.Clear();
        _currentStep = 3;
        UpdateStepIndicator();

        int panelWidth = _contentContainer?.Width ?? 440;
        int currentY = 0;
        var fileInfo = new System.IO.FileInfo(_selectedFilePath!);

        var nameLabel = new WinLabel
        {
            Text = "檔案名稱",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, currentY),
            Size = new Size(100, 20)
        };
        _contentContainer?.Controls.Add(nameLabel);
        currentY += 22;

        _displayNameInput = new AntdUI.Input
        {
            Text = fileInfo.Name,
            Size = new Size(panelWidth, 40),
            Location = new Point(0, currentY)
        };
        _contentContainer?.Controls.Add(_displayNameInput);
        currentY += 50;

        var modeLabel = new WinLabel
        {
            Text = "分享模式",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, currentY),
            Size = new Size(100, 20)
        };
        _contentContainer?.Controls.Add(modeLabel);
        currentY += 22;

        _shareModeSelect = new AntdUI.Select
        {
            Size = new Size(panelWidth, 40),
            Location = new Point(0, currentY),
            List = true
        };
        _shareModeSelect.Items.AddRange(new object[]
        {
            new AntdUI.SelectItem("public", "公開（任何人可存取）"),
            new AntdUI.SelectItem("pin", "密碼保護（需輸入 PIN）"),
            new AntdUI.SelectItem("account", "帳號綁定（首個綁定帳號）"),
            new AntdUI.SelectItem("device", "裝置綁定（首個綁定裝置）")
        });
        _shareModeSelect.SelectedIndex = 0;
        _shareModeSelect.SelectedIndexChanged += ShareMode_Changed;
        _contentContainer?.Controls.Add(_shareModeSelect);
        currentY += 50;

        int halfWidth = (panelWidth - 16) / 2;

        var downloadsLabel = new WinLabel
        {
            Text = "下載次數",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, currentY),
            Size = new Size(100, 20)
        };
        _contentContainer?.Controls.Add(downloadsLabel);

        var expiresLabel = new WinLabel
        {
            Text = "到期日",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(halfWidth + 16, currentY),
            Size = new Size(150, 20)
        };
        _contentContainer?.Controls.Add(expiresLabel);
        currentY += 22;

        _maxDownloadsInput = new AntdUI.InputNumber
        {
            Value = 10,
            Minimum = 1,
            Maximum = 999,
            Size = new Size(halfWidth, 40),
            Location = new Point(0, currentY)
        };
        _contentContainer?.Controls.Add(_maxDownloadsInput);

        _expiresDatePicker = new AntdUI.DatePicker
        {
            Value = DateTime.Now.AddDays(7),
            Size = new Size(halfWidth, 40),
            Location = new Point(halfWidth + 16, currentY),
            MinDate = DateTime.Now.Date,
            MaxDate = DateTime.Now.Date.AddDays(MaxDays)
        };
        _contentContainer?.Controls.Add(_expiresDatePicker);
        currentY += 52;

        _pinContainer = new WinPanel
        {
            Size = new Size(panelWidth, 62),
            Location = new Point(0, currentY),
            BackColor = ThemeManager.CardBackgroundColor,
            Visible = false
        };
        ThemeManager.ApplyRoundedCorners(_pinContainer, ContainerRadius);
        UIConstants.EnableDoubleBuffering(_pinContainer);

        var pinIcon = new IconPictureBox
        {
            IconChar = IconChar.Key,
            IconSize = 16,
            IconColor = ThemeManager.PrimaryColor,
            Size = new Size(20, 20),
            Location = new Point(12, 18),
            BackColor = ThemeManager.CardBackgroundColor
        };
        _pinContainer.Controls.Add(pinIcon);

        var pinTitleLabel = new WinLabel
        {
            Text = "分享 PIN 碼",
            Font = new Font(ThemeManager.FontFamily, 9f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = ThemeManager.CardBackgroundColor,
            Location = new Point(36, 10),
            Size = new Size(80, 16)
        };
        _pinContainer.Controls.Add(pinTitleLabel);

        _pinValueLabel = new WinLabel
        {
            Text = "------",
            Font = new Font("Consolas", 14f, FontStyle.Bold),
            ForeColor = ThemeManager.PrimaryColor,
            BackColor = ThemeManager.CardBackgroundColor,
            Location = new Point(36, 28),
            Size = new Size(120, 20),
            TextAlign = ContentAlignment.MiddleLeft
        };
        _pinContainer.Controls.Add(_pinValueLabel);

        var copyPinButton = new AntdUI.Button
        {
            Type = TTypeMini.Primary,
            Size = new Size(CopyButtonSize, CopyButtonSize),
            Location = new Point(panelWidth - CopyButtonSize - 10, 10),
            IconSvg = "<svg viewBox=\"0 0 448 512\"><path d=\"M208 0H332.1c12.7 0 24.9 5.1 33.9 14.1l67.9 67.9c9 9 14.1 21.2 14.1 33.9V336c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V48c0-26.5 21.5-48 48-48zM48 128h80v64H64V448H256V416h64v48c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V176c0-26.5 21.5-48 48-48z\"/></svg>"
        };
        copyPinButton.Click += (s, e) =>
        {
            if (!string.IsNullOrEmpty(_generatedPin))
            {
                Clipboard.SetText(_generatedPin);
                AntdUI.Message.success(this, "PIN 碼已複製到剪貼簿", autoClose: 2);
            }
        };
        _pinContainer.Controls.Add(copyPinButton);

        _contentContainer?.Controls.Add(_pinContainer);

        _nextButton!.Text = "建立分享";
        _nextButton.Enabled = true;
        _cancelButton!.Text = "取消";
    }

    private void ShareMode_Changed(object? sender, IntEventArgs e)
    {
        if (_shareModeSelect == null || e.Value < 0 || e.Value >= _shareModeSelect.Items.Count) return;
        
        string mode = e.Value switch
        {
            0 => "public",
            1 => "pin",
            2 => "account",
            3 => "device",
            _ => "public"
        };

        System.Diagnostics.Debug.WriteLine($"[ShareMode] Changed to index: {e.Value}, mode: {mode}");

        if (mode == "pin")
        {
            _generatedPin = UploadService.GeneratePin();
            System.Diagnostics.Debug.WriteLine($"[ShareMode] Generated PIN: {_generatedPin}");
            
            if (_pinValueLabel != null)
            {
                _pinValueLabel.Text = _generatedPin;
                _pinValueLabel.Refresh();
            }
            if (_pinContainer != null)
            {
                _pinContainer.Visible = true;
                _pinContainer.Refresh();
            }
        }
        else
        {
            if (_pinContainer != null)
            {
                _pinContainer.Visible = false;
            }
            _generatedPin = null;
        }
    }

    private async void ShowStep4_Complete()
    {
        _nextButton!.Text = "建立中...";
        _nextButton.Enabled = false;
        _cancelButton!.Enabled = false;
        _isCreatingShare = true;

        try
        {
            int selectedIndex = _shareModeSelect?.SelectedIndex ?? 0;
            string shareMode = selectedIndex switch
            {
                0 => "public",
                1 => "pin",
                2 => "account",
                3 => "device",
                _ => "public"
            };
            var fileInfo = new System.IO.FileInfo(_selectedFilePath!);

            System.Diagnostics.Debug.WriteLine($"[CreateShare] Starting... Mode: {shareMode}, PIN: {_generatedPin}");

            var request = new CreateShareRequest
            {
                StoragePath = _uploadedStoragePath!,
                FileName = fileInfo.Name,
                FileSize = _uploadedFileSize > 0 ? _uploadedFileSize : fileInfo.Length,
                ContentType = GetContentType(_selectedFilePath!),
                DisplayName = _displayNameInput?.Text ?? fileInfo.Name,
                ExpiresAt = _expiresDatePicker?.Value ?? DateTime.Now.AddDays(7),
                MaxDownloads = (int)(_maxDownloadsInput?.Value ?? 10),
                ShareMode = shareMode,
                Pin = shareMode == "pin" ? _generatedPin : null,
                Recipients = new List<string>()
            };

            var response = await ServiceManager.Instance.Upload.CreateShareAsync(request);

            System.Diagnostics.Debug.WriteLine($"[CreateShare] Response: Success={response?.Success}, ShareId={response?.ShareId}, Error={response?.Error}");

            if (response?.Success == true && !string.IsNullOrEmpty(response.ShareId))
            {
                _shareId = response.ShareId;
                _shareUrl = response.ShareUrl ?? $"{FirebaseConfig.WebAppUrl}/share/{_shareId}";
                DisplayCompletionUI();
            }
            else
            {
                var errorMsg = response?.Error ?? "建立分享連結失敗";
                System.Diagnostics.Debug.WriteLine($"[CreateShare] Failed: {errorMsg}");
                AntdUI.Message.error(this, errorMsg, autoClose: 4);
                
                _nextButton.Text = "建立分享";
                _nextButton.Enabled = true;
                _cancelButton.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CreateShare] Exception: {ex.Message}");
            AntdUI.Message.error(this, $"建立分享失敗：{ex.Message}", autoClose: 4);
            
            _nextButton.Text = "建立分享";
            _nextButton.Enabled = true;
            _cancelButton.Enabled = true;
        }
        finally
        {
            _isCreatingShare = false;
        }
    }

    private void DisplayCompletionUI()
    {
        _contentContainer?.Controls.Clear();
        _currentStep = 4;
        UpdateStepIndicator();

        int panelWidth = _contentContainer?.Width ?? 440;
        int containerHeight = _contentContainer?.Height ?? 301;
        
        System.Diagnostics.Debug.WriteLine($"[DisplayCompletionUI] Container size: {panelWidth} x {containerHeight}, HasPin: {!string.IsNullOrEmpty(_generatedPin)}");

        var successIcon = new IconPictureBox
        {
            IconChar = IconChar.CheckCircle,
            IconSize = 48,
            IconColor = ThemeManager.SuccessColor,
            Size = new Size(56, 56),
            Location = new Point((panelWidth - 56) / 2, 0),
            BackColor = Color.Transparent
        };
        _contentContainer?.Controls.Add(successIcon);

        var successLabel = new WinLabel
        {
            Text = "分享連結已建立！",
            Font = new Font(ThemeManager.FontFamily, 14f, FontStyle.Bold),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = Color.Transparent,
            Size = new Size(panelWidth, 26),
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 58)
        };
        _contentContainer?.Controls.Add(successLabel);

        int currentY = 95;

        var urlTitleLabel = new WinLabel
        {
            Text = "分享連結",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, currentY),
            Size = new Size(100, 18)
        };
        _contentContainer?.Controls.Add(urlTitleLabel);
        currentY += 20;

        var urlContainer = new WinPanel
        {
            Size = new Size(panelWidth, 40),
            Location = new Point(0, currentY),
            BackColor = ThemeManager.CardBackgroundColor
        };
        ThemeManager.ApplyRoundedCorners(urlContainer, ContainerRadius);
        UIConstants.EnableDoubleBuffering(urlContainer);

        _shareUrlLabel = new WinLabel
        {
            Text = TruncateUrl(_shareUrl ?? "", 32),
            Font = new Font(ThemeManager.FontFamily, 9f),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = ThemeManager.CardBackgroundColor,
            Size = new Size(panelWidth - CopyButtonSize - 24, 40),
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(12, 0)
        };
        urlContainer.Controls.Add(_shareUrlLabel);

        var copyUrlButton = new AntdUI.Button
        {
            Type = TTypeMini.Primary,
            Size = new Size(CopyButtonSize, CopyButtonSize),
            Location = new Point(panelWidth - CopyButtonSize - 4, 2),
            IconSvg = "<svg viewBox=\"0 0 448 512\"><path d=\"M208 0H332.1c12.7 0 24.9 5.1 33.9 14.1l67.9 67.9c9 9 14.1 21.2 14.1 33.9V336c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V48c0-26.5 21.5-48 48-48zM48 128h80v64H64V448H256V416h64v48c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V176c0-26.5 21.5-48 48-48z\"/></svg>"
        };
        copyUrlButton.Click += (s, e) =>
        {
            if (!string.IsNullOrEmpty(_shareUrl))
            {
                Clipboard.SetText(_shareUrl);
                AntdUI.Message.success(this, "連結已複製", autoClose: 2);
            }
        };
        urlContainer.Controls.Add(copyUrlButton);
        _contentContainer?.Controls.Add(urlContainer);
        currentY += 48;

        var idTitleLabel = new WinLabel
        {
            Text = "分享代碼",
            Font = new Font(ThemeManager.FontFamily, 10f),
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Location = new Point(0, currentY),
            Size = new Size(100, 18)
        };
        _contentContainer?.Controls.Add(idTitleLabel);
        currentY += 20;

        var idContainer = new WinPanel
        {
            Size = new Size(panelWidth, 40),
            Location = new Point(0, currentY),
            BackColor = ThemeManager.CardBackgroundColor
        };
        ThemeManager.ApplyRoundedCorners(idContainer, ContainerRadius);
        UIConstants.EnableDoubleBuffering(idContainer);

        _shareIdLabel = new WinLabel
        {
            Text = _shareId ?? "",
            Font = new Font("Consolas", 10f),
            ForeColor = ThemeManager.TextPrimaryColor,
            BackColor = ThemeManager.CardBackgroundColor,
            Size = new Size(panelWidth - CopyButtonSize - 24, 40),
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(12, 0)
        };
        idContainer.Controls.Add(_shareIdLabel);

        var copyIdButton = new AntdUI.Button
        {
            Type = TTypeMini.Default,
            Size = new Size(CopyButtonSize, CopyButtonSize),
            Location = new Point(panelWidth - CopyButtonSize - 4, 2),
            IconSvg = "<svg viewBox=\"0 0 448 512\"><path d=\"M208 0H332.1c12.7 0 24.9 5.1 33.9 14.1l67.9 67.9c9 9 14.1 21.2 14.1 33.9V336c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V48c0-26.5 21.5-48 48-48zM48 128h80v64H64V448H256V416h64v48c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V176c0-26.5 21.5-48 48-48z\"/></svg>"
        };
        copyIdButton.Click += (s, e) =>
        {
            if (!string.IsNullOrEmpty(_shareId))
            {
                Clipboard.SetText(_shareId);
                AntdUI.Message.success(this, "代碼已複製", autoClose: 2);
            }
        };
        idContainer.Controls.Add(copyIdButton);
        _contentContainer?.Controls.Add(idContainer);
        currentY += 48;

        if (!string.IsNullOrEmpty(_generatedPin))
        {
            System.Diagnostics.Debug.WriteLine($"[DisplayCompletionUI] Adding PIN at Y={currentY}, PIN={_generatedPin}");
            
            var pinTitleLabel = new WinLabel
            {
                Text = "PIN 碼",
                Font = new Font(ThemeManager.FontFamily, 10f),
                ForeColor = ThemeManager.TextSecondaryColor,
                BackColor = Color.Transparent,
                Location = new Point(0, currentY),
                Size = new Size(100, 18)
            };
            _contentContainer?.Controls.Add(pinTitleLabel);
            currentY += 20;

            var pinContainer = new WinPanel
            {
                Size = new Size(panelWidth, 44),
                Location = new Point(0, currentY),
                BackColor = ThemeManager.CardBackgroundColor
            };
            ThemeManager.ApplyRoundedCorners(pinContainer, ContainerRadius);
            UIConstants.EnableDoubleBuffering(pinContainer);

            var pinIcon = new IconPictureBox
            {
                IconChar = IconChar.Key,
                IconSize = 16,
                IconColor = ThemeManager.PrimaryColor,
                Size = new Size(20, 20),
                Location = new Point(12, 12),
                BackColor = ThemeManager.CardBackgroundColor
            };
            pinContainer.Controls.Add(pinIcon);

            var pinValueLabel = new WinLabel
            {
                Text = _generatedPin,
                Font = new Font("Consolas", 14f, FontStyle.Bold),
                ForeColor = ThemeManager.PrimaryColor,
                BackColor = ThemeManager.CardBackgroundColor,
                Size = new Size(180, 44),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(38, 0)
            };
            pinContainer.Controls.Add(pinValueLabel);

            var copyPinButton = new AntdUI.Button
            {
                Type = TTypeMini.Primary,
                Size = new Size(CopyButtonSize, CopyButtonSize),
                Location = new Point(panelWidth - CopyButtonSize - 4, 4),
                IconSvg = "<svg viewBox=\"0 0 448 512\"><path d=\"M208 0H332.1c12.7 0 24.9 5.1 33.9 14.1l67.9 67.9c9 9 14.1 21.2 14.1 33.9V336c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V48c0-26.5 21.5-48 48-48zM48 128h80v64H64V448H256V416h64v48c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V176c0-26.5 21.5-48 48-48z\"/></svg>"
            };
            copyPinButton.Click += (s, e) =>
            {
                Clipboard.SetText(_generatedPin);
                AntdUI.Message.success(this, "PIN 碼已複製", autoClose: 2);
            };
            pinContainer.Controls.Add(copyPinButton);
            _contentContainer?.Controls.Add(pinContainer);
            
            System.Diagnostics.Debug.WriteLine($"[DisplayCompletionUI] PIN container added, ends at Y={currentY + 44}");
        }

        _nextButton!.Text = "完成";
        _nextButton.Enabled = true;
        _cancelButton!.Visible = false;
    }

    private void NextButton_Click(object? sender, EventArgs e)
    {
        switch (_currentStep)
        {
            case 1:
                if (string.IsNullOrEmpty(_selectedFilePath))
                {
                    SelectFile();
                }
                else
                {
                    ShowStep2_Uploading();
                }
                break;
            case 3:
                ShowStep4_Complete();
                break;
            case 4:
                UploadCompleted?.Invoke(this, new UploadCompletedEventArgs
                {
                    ShareId = _shareId,
                    ShareUrl = _shareUrl
                });
                DialogResult = DialogResult.OK;
                Close();
                break;
        }
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        if (_isUploading && _uploadCts != null)
        {
            var result = AntdUI.Modal.open(new AntdUI.Modal.Config(this, "確定要取消上傳嗎？", "確認")
            {
                Icon = TType.Warn,
                OkText = "確定",
                CancelText = "繼續上傳"
            });
            if (result == DialogResult.OK)
            {
                _uploadCts.Cancel();
            }
        }
        else
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private static string FormatFileSize(long bytes)
    {
        if (bytes <= 0) return "0 B";
        string[] sizes = { "B", "KB", "MB", "GB" };
        int order = 0;
        double len = bytes;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len /= 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    private static string TruncateFileName(string fileName, int maxLength)
    {
        if (string.IsNullOrEmpty(fileName) || fileName.Length <= maxLength)
            return fileName ?? "";

        var ext = Path.GetExtension(fileName);
        var nameOnly = Path.GetFileNameWithoutExtension(fileName);

        int keepLen = (maxLength - 3 - ext.Length) / 2;
        if (keepLen < 3) keepLen = 3;

        return $"{nameOnly[..keepLen]}...{nameOnly[^keepLen..]}{ext}";
    }

    private static string TruncateUrl(string url, int maxLength)
    {
        var display = url.Replace("https://", "").Replace("http://", "");
        if (display.Length <= maxLength) return display;

        int keepLen = (maxLength - 3) / 2;
        return $"{display[..keepLen]}...{display[^keepLen..]}";
    }

    private static string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".txt" => "text/plain",
            ".html" or ".htm" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".json" => "application/json",
            ".xml" => "application/xml",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".webp" => "image/webp",
            ".svg" => "image/svg+xml",
            ".mp3" => "audio/mpeg",
            ".wav" => "audio/wav",
            ".mp4" => "video/mp4",
            ".webm" => "video/webm",
            ".avi" => "video/x-msvideo",
            ".mov" => "video/quicktime",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            ".7z" => "application/x-7z-compressed",
            ".tar" => "application/x-tar",
            ".gz" => "application/gzip",
            _ => "application/octet-stream"
        };
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_isUploading)
        {
            var result = AntdUI.Modal.open(new AntdUI.Modal.Config(this, "上傳中，確定要關閉嗎？", "確認")
            {
                Icon = TType.Warn,
                OkText = "確定",
                CancelText = "繼續上傳"
            });
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            _uploadCts?.Cancel();
        }
        base.OnFormClosing(e);
    }
}

public class UploadCompletedEventArgs : EventArgs
{
    public string? ShareId { get; set; }
    public string? ShareUrl { get; set; }
}
