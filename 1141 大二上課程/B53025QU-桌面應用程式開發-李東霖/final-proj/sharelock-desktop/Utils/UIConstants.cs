using System.Drawing.Text;

namespace sharelock_desktop.Utils;
public static class UIConstants
{
    #region Spacing Constants
    public const int PaddingXS = 4;
    public const int PaddingSM = 8;
    public const int PaddingMD = 12;
    public const int PaddingLG = 16;
    public const int PaddingXL = 24;
    public const int PaddingXXL = 32;
    public const int PagePaddingLeft = 40;
    public const int PagePaddingTop = 32;
    public const int PagePaddingRight = 40;
    public const int PagePaddingBottom = 32;
    public const int GapSM = 8;
    public const int GapMD = 12;
    public const int GapLG = 16;
    public const int SectionGap = 24;

    #endregion

    #region Icon Sizes
    public const int IconSizeXS = 12;
    public const int IconSizeSM = 16;
    public const int IconSizeMD = 20;
    public const int IconSizeLG = 24;
    public const int IconSizeXL = 32;
    public const int IconSizeXXL = 48;
    public const int IconSizeHuge = 64;

    #endregion

    #region Control Dimensions
    public const int SidebarWidth = 300;
    public const int SidebarCollapsedWidth = 64;
    public const int SidebarPadding = 12;
    public const int MenuItemHeight = 50;
    public const int MenuIconSize = 22;
    public const float MenuFontSize = 13f;
    public const int ButtonHeight = 36;
    public const int ButtonHeightSM = 32;
    public const int ButtonHeightLG = 44;
    public const int InputHeight = 40;
    public const int CardMinWidth = 280;
    public const int CardMaxWidth = 400;
    public const int ListItemHeight = 60;
    public const int ListItemHeightLG = 72;
    public const int AvatarSizeSM = 32;
    public const int AvatarSizeMD = 48;
    public const int AvatarSizeLG = 64;
    public const int AvatarSizeXL = 80;
    public const int LogoWidth = 220;
    public const int LogoHeight = 110;

    #endregion

    #region Font Sizes
    public const float FontSizeXS = 9f;
    public const float FontSizeSM = 10f;
    public const float FontSizeMD = 11f;
    public const float FontSizeLG = 12f;
    public const float FontSizeTitle = 14f;
    public const float FontSizePageTitle = 20f;
    public const float FontSizeHero = 24f;

    #endregion

    #region Text Clipping Prevention
    public const int SafeTextLeftPadding = 4;
    public static int MeasureTextWidth(string text, Font font, Graphics? g = null)
    {
        if (string.IsNullOrEmpty(text)) return 0;

        bool disposeGraphics = g == null;
        g ??= Graphics.FromHwnd(IntPtr.Zero);

        try
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var size = TextRenderer.MeasureText(g, text, font,
                new Size(int.MaxValue, int.MaxValue),
                TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix);
            return size.Width + SafeTextLeftPadding * 2;
        }
        finally
        {
            if (disposeGraphics) g.Dispose();
        }
    }
    public static int MeasureTextHeight(string text, Font font, int maxWidth, Graphics? g = null)
    {
        if (string.IsNullOrEmpty(text)) return 0;

        bool disposeGraphics = g == null;
        g ??= Graphics.FromHwnd(IntPtr.Zero);

        try
        {
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var size = TextRenderer.MeasureText(g, text, font,
                new Size(maxWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
            return size.Height;
        }
        finally
        {
            if (disposeGraphics) g.Dispose();
        }
    }
    public static string TruncateWithEllipsis(string text, Font font, int maxWidth, Graphics? g = null)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (maxWidth <= 0) return string.Empty;

        int measuredWidth = MeasureTextWidth(text, font, g);
        if (measuredWidth <= maxWidth) return text;

        const string ellipsis = "...";
        int ellipsisWidth = MeasureTextWidth(ellipsis, font, g);
        int targetWidth = maxWidth - ellipsisWidth;

        if (targetWidth <= 0) return ellipsis;

        for (int i = text.Length - 1; i > 0; i--)
        {
            string truncated = text[..i];
            int width = MeasureTextWidth(truncated, font, g);
            if (width <= targetWidth)
            {
                return truncated + ellipsis;
            }
        }

        return ellipsis;
    }
    public static bool CheckTextClipping(Control control, out string warning)
    {
        warning = string.Empty;

        if (control is Label label && !string.IsNullOrEmpty(label.Text))
        {
            int textWidth = MeasureTextWidth(label.Text, label.Font);
            if (textWidth > label.Width)
            {
                warning = $"Label '{label.Name}' text may be clipped: text width {textWidth}px > control width {label.Width}px";
                return true;
            }
        }
        else if (control is Button button && !string.IsNullOrEmpty(button.Text))
        {
            int textWidth = MeasureTextWidth(button.Text, button.Font);
            int availableWidth = button.Width - button.Padding.Horizontal - 16;
            if (textWidth > availableWidth)
            {
                warning = $"Button '{button.Name}' text may be clipped: text width {textWidth}px > available {availableWidth}px";
                return true;
            }
        }

        return false;
    }
    public static List<string> ScanForClippingIssues(Control container)
    {
        var issues = new List<string>();
        ScanControlsRecursive(container, issues);
        return issues;
    }

    private static void ScanControlsRecursive(Control control, List<string> issues)
    {
        if (CheckTextClipping(control, out string warning))
        {
            issues.Add(warning);
        }

        foreach (Control child in control.Controls)
        {
            ScanControlsRecursive(child, issues);
        }
    }

    #endregion

    #region DPI Helpers
    public static float GetDpiScale()
    {
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        return g.DpiX / 96f;
    }
    public static int ScaleForDpi(int value)
    {
        return (int)(value * GetDpiScale());
    }
    public static float ScaleFontForDpi(float baseFontSize)
    {
        return baseFontSize;
    }

    #endregion

    #region Layout Helpers
    public static TableLayoutPanel CreateSafeTableLayout(int columns, int rows)
    {
        var table = new TableLayoutPanel
        {
            AutoSize = false,
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };

        table.ColumnCount = columns;
        table.RowCount = rows;

        return table;
    }
    public static TableLayoutPanel CreateIconTextLayout(int iconColumnWidth = IconSizeLG + GapSM)
    {
        var layout = CreateSafeTableLayout(2, 1);
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, iconColumnWidth));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        return layout;
    }
    public static Padding StandardPadding => new(PaddingLG);
    public static Padding CardPadding => new(PaddingMD);
    public static Padding ListItemPadding => new(PaddingMD, PaddingSM, PaddingMD, PaddingSM);
    public static Padding PagePadding => new(PagePaddingLeft, PagePaddingTop, PagePaddingRight, PagePaddingBottom);
    public static void EnableDoubleBuffering(Control control)
    {
        typeof(Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
            .SetValue(control, true, null);
    }

    #endregion
}
