using AntdUI;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Utils;
public static class ThemeManager
{

    public const string FontFamily = "Microsoft JhengHei UI";
    public static void InitializeDarkTheme()
    {

        Config.Mode = TMode.Dark;
    }

    #region Colors
    public static Color BackgroundColor => Color.FromArgb(30, 30, 30);
    public static Color CardBackgroundColor => Color.FromArgb(45, 45, 45);
    public static Color SidebarBackgroundColor => Color.FromArgb(25, 25, 25);
    public static Color SelectedBackgroundColor => Color.FromArgb(59, 130, 246, 30);
    public static Color HoverBackgroundColor => Color.FromArgb(255, 255, 255, 10);
    public static Color BorderColor => Color.FromArgb(64, 64, 64);
    public static Color TextPrimaryColor => Color.FromArgb(240, 240, 240);
    public static Color TextSecondaryColor => Color.FromArgb(160, 160, 160);
    public static Color TextDisabledColor => Color.FromArgb(100, 100, 100);
    public static Color PrimaryColor => Color.FromArgb(59, 130, 246);
    public static Color PrimaryHoverColor => Color.FromArgb(96, 165, 250);
    public static Color SuccessColor => Color.FromArgb(34, 197, 94);
    public static Color WarningColor => Color.FromArgb(234, 179, 8);
    public static Color ErrorColor => Color.FromArgb(239, 68, 68);
    public static Color InfoColor => Color.FromArgb(59, 130, 246);

    #endregion

    #region Fonts
    public static Font RegularFont => new(FontFamily, UIConstants.FontSizeMD, FontStyle.Regular);
    public static Font SmallFont => new(FontFamily, UIConstants.FontSizeXS, FontStyle.Regular);
    public static Font SubtitleFont => new(FontFamily, UIConstants.FontSizeSM, FontStyle.Regular);
    public static Font TitleFont => new(FontFamily, UIConstants.FontSizeTitle, FontStyle.Bold);
    public static Font PageTitleFont => new(FontFamily, UIConstants.FontSizePageTitle, FontStyle.Bold);
    public static Font HeroFont => new(FontFamily, UIConstants.FontSizeHero, FontStyle.Bold);
    public static Font ButtonFont => new(FontFamily, UIConstants.FontSizeSM, FontStyle.Regular);
    public static Font MenuFont => new(FontFamily, UIConstants.FontSizeMD, FontStyle.Regular);

    #endregion

    #region Form Setup
    public static void ApplyThemeToForm(Form form)
    {
        form.BackColor = BackgroundColor;
        form.ForeColor = TextPrimaryColor;
        form.Font = RegularFont;

        form.AutoScaleMode = AutoScaleMode.Dpi;
    }

    #endregion

    #region Control Styling Helpers
    public static AntdUI.Label CreateLabel(
        string text,
        Font? font = null,
        Color? foreColor = null,
        ContentAlignment alignment = ContentAlignment.MiddleLeft)
    {
        return new AntdUI.Label
        {
            Text = text,
            Font = font ?? RegularFont,
            ForeColor = foreColor ?? TextPrimaryColor,
            AutoSize = true,
            TextAlign = alignment,

            AutoEllipsis = true,
            Padding = new Padding(UIConstants.SafeTextLeftPadding, 0, UIConstants.SafeTextLeftPadding, 0)
        };
    }
    public static AntdUI.Label CreateFixedWidthLabel(
        string text,
        int width,
        Font? font = null,
        Color? foreColor = null,
        ContentAlignment alignment = ContentAlignment.MiddleLeft)
    {
        var label = CreateLabel(text, font, foreColor, alignment);
        label.AutoSize = false;
        label.Size = new Size(width, UIConstants.MeasureTextHeight(text, font ?? RegularFont, width) + 4);
        label.AutoEllipsis = true;
        return label;
    }
    public static WinPanel CreateCardPanel(int width, int height)
    {
        var panel = new WinPanel
        {
            Size = new Size(width, height),
            BackColor = CardBackgroundColor,
            Padding = UIConstants.CardPadding,
            Margin = new Padding(0, 0, UIConstants.GapMD, UIConstants.GapMD)
        };
        return panel;
    }
    public static void ApplyRoundedCorners(WinPanel panel, int radius = 8)
    {
        panel.Paint += (s, e) =>
        {
            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            var rect = panel.ClientRectangle;

            int safeRadius = Math.Min(radius, Math.Min(rect.Width / 2, rect.Height / 2));
            if (safeRadius < 1) safeRadius = 1;

            int diameter = safeRadius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        };
    }
    public static WinPanel CreateElevatedCard(int width, int height, int elevation = 1)
    {
        var panel = CreateCardPanel(width, height);

        int brightness = Math.Min(45 + (elevation * 3), 60);
        panel.BackColor = Color.FromArgb(brightness, brightness, brightness);

        ApplyRoundedCorners(panel);
        return panel;
    }

    #endregion
}
