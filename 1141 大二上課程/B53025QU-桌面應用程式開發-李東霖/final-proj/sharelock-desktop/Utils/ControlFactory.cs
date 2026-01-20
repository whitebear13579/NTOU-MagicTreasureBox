using FontAwesome.Sharp;

using WinPanel = System.Windows.Forms.Panel;

namespace sharelock_desktop.Utils;

public static class ControlFactory
{
    #region Panel Helpers

    public static WinPanel CreateTransparentPanel(DockStyle dock = DockStyle.None)
    {
        var panel = new WinPanel
        {
            BackColor = Color.Transparent,
            Dock = dock,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        EnableDoubleBuffering(panel);
        return panel;
    }

    public static WinPanel CreateContentPanel()
    {
        var panel = new WinPanel
        {
            Dock = DockStyle.Fill,
            BackColor = ThemeManager.BackgroundColor,
            Padding = UIConstants.StandardPadding,
            AutoScroll = true
        };
        EnableDoubleBuffering(panel);
        return panel;
    }

    public static WinPanel CreateCard(int width, int height, int margin = 0)
    {
        var card = new WinPanel
        {
            Size = new Size(width, height),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = UIConstants.CardPadding,
            Margin = new Padding(0, 0, margin, margin)
        };
        EnableDoubleBuffering(card);
        ThemeManager.ApplyRoundedCorners(card, 10);
        return card;
    }

    public static WinPanel CreateListItem(int width, int height = 0)
    {
        height = height > 0 ? height : UIConstants.ListItemHeight;
        var item = new WinPanel
        {
            Size = new Size(width, height),
            BackColor = ThemeManager.CardBackgroundColor,
            Padding = UIConstants.ListItemPadding,
            Margin = new Padding(0, 0, 0, UIConstants.GapSM),
            Cursor = Cursors.Hand
        };
        EnableDoubleBuffering(item);
        ThemeManager.ApplyRoundedCorners(item, 6);
        return item;
    }

    #endregion

    #region FlowLayoutPanel Helpers

    public static FlowLayoutPanel CreateHorizontalFlow(bool autoSize = true)
    {
        var panel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoSize = autoSize,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        EnableDoubleBuffering(panel);
        return panel;
    }

    public static FlowLayoutPanel CreateVerticalFlow(bool autoSize = false, bool autoScroll = true)
    {
        var panel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = autoSize,
            AutoScroll = autoScroll,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0)
        };
        EnableDoubleBuffering(panel);
        return panel;
    }

    public static FlowLayoutPanel CreateCardGrid()
    {
        var panel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = true,
            AutoScroll = true,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0, UIConstants.PaddingMD, 0, UIConstants.PaddingMD)
        };
        EnableDoubleBuffering(panel);
        return panel;
    }

    #endregion

    #region TableLayoutPanel Helpers

    public static TableLayoutPanel CreateIconTextTable(int iconColumnWidth)
    {
        var table = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0),
            AutoSize = true
        };
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, iconColumnWidth));
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        EnableDoubleBuffering(table);
        return table;
    }

    public static TableLayoutPanel CreateFlexTable(int columns, params float[] percentages)
    {
        var table = new TableLayoutPanel
        {
            ColumnCount = columns,
            RowCount = 1,
            BackColor = Color.Transparent,
            Margin = new Padding(0),
            Padding = new Padding(0),
            Dock = DockStyle.Fill
        };

        for (int i = 0; i < columns; i++)
        {
            float percent = i < percentages.Length ? percentages[i] : 100f / columns;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));
        }
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        EnableDoubleBuffering(table);
        return table;
    }

    #endregion

    #region Icon Helpers

    public static IconPictureBox CreateIcon(
        IconChar icon,
        int size = UIConstants.IconSizeMD,
        Color? color = null)
    {
        return new IconPictureBox
        {
            IconChar = icon,
            IconSize = size,
            IconColor = color ?? ThemeManager.PrimaryColor,
            Size = new Size(size + 4, size + 4),
            BackColor = Color.Transparent,
            Margin = new Padding(0)
        };
    }

    public static FlowLayoutPanel CreateIconLabel(
        IconChar icon,
        string text,
        int iconSize = UIConstants.IconSizeMD,
        Color? iconColor = null,
        Font? font = null,
        Color? textColor = null)
    {
        var panel = CreateHorizontalFlow();

        var iconBox = CreateIcon(icon, iconSize, iconColor);
        iconBox.Margin = new Padding(0, 0, UIConstants.GapSM, 0);
        panel.Controls.Add(iconBox);

        var label = new AntdUI.Label
        {
            Text = text,
            Font = font ?? ThemeManager.RegularFont,
            ForeColor = textColor ?? ThemeManager.TextPrimaryColor,
            AutoSize = true,
            BackColor = Color.Transparent
        };
        panel.Controls.Add(label);

        return panel;
    }

    public static WinPanel CreateSectionHeader(
        IconChar icon,
        string title,
        int iconSize = UIConstants.IconSizeLG)
    {
        var panel = CreateTransparentPanel();
        panel.Height = 40;

        var flow = CreateHorizontalFlow();
        flow.Location = new Point(0, 0);

        var iconBox = CreateIcon(icon, iconSize);
        iconBox.Margin = new Padding(0, 2, UIConstants.GapSM, 0);
        flow.Controls.Add(iconBox);

        var titleLabel = new AntdUI.Label
        {
            Text = title,
            Font = ThemeManager.TitleFont,
            ForeColor = ThemeManager.TextPrimaryColor,
            AutoSize = true,
            BackColor = Color.Transparent
        };
        flow.Controls.Add(titleLabel);

        panel.Controls.Add(flow);
        return panel;
    }

    #endregion

    #region Empty State Helpers

    public static WinPanel CreateEmptyState(
        IconChar icon,
        string message,
        bool isError = false,
        int iconSize = UIConstants.IconSizeHuge)
    {
        var panel = CreateTransparentPanel(DockStyle.Fill);

        var contentPanel = new WinPanel
        {
            Size = new Size(300, 180),
            BackColor = Color.Transparent
        };
        EnableDoubleBuffering(contentPanel);

        var iconColor = isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor;

        var iconBox = CreateIcon(icon, iconSize, iconColor);
        iconBox.Location = new Point((contentPanel.Width - iconSize) / 2, 20);
        contentPanel.Controls.Add(iconBox);

        var label = new AntdUI.Label
        {
            Text = message,
            Font = ThemeManager.TitleFont,
            ForeColor = isError ? ThemeManager.ErrorColor : ThemeManager.TextSecondaryColor,
            Size = new Size(280, 60),
            Location = new Point(10, 100),
            TextAlign = ContentAlignment.TopCenter,
            AutoSize = false,
            BackColor = Color.Transparent
        };
        contentPanel.Controls.Add(label);

        panel.Resize += (s, e) =>
        {
            contentPanel.Location = new Point(
                Math.Max(0, (panel.Width - contentPanel.Width) / 2),
                Math.Max(0, (panel.Height - contentPanel.Height) / 2)
            );
        };

        panel.Controls.Add(contentPanel);
        return panel;
    }

    #endregion

    #region Button Helpers

    public static IconButton CreateIconButton(
        IconChar icon,
        string text,
        int iconSize = UIConstants.IconSizeSM,
        Color? iconColor = null)
    {
        var button = new IconButton
        {
            IconChar = icon,
            IconSize = iconSize,
            IconColor = iconColor ?? ThemeManager.TextSecondaryColor,
            Text = text,
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextAlign = ContentAlignment.MiddleLeft,
            FlatStyle = FlatStyle.Flat,
            ForeColor = ThemeManager.TextSecondaryColor,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand,
            Font = ThemeManager.ButtonFont,
            Padding = new Padding(UIConstants.PaddingSM, 0, UIConstants.PaddingSM, 0)
        };
        button.FlatAppearance.BorderSize = 0;
        return button;
    }

    #endregion

    #region Text Helpers

    public static string SafeTruncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (text.Length <= maxLength) return text;
        return text[..(maxLength - 1)] + "¡K";
    }

    public static AntdUI.Label CreateAutoTruncateLabel(
        string text,
        int maxWidth,
        Font? font = null,
        Color? foreColor = null)
    {
        font ??= ThemeManager.RegularFont;

        var label = new AntdUI.Label
        {
            Text = UIConstants.TruncateWithEllipsis(text, font, maxWidth),
            Font = font,
            ForeColor = foreColor ?? ThemeManager.TextPrimaryColor,
            AutoSize = false,
            Size = new Size(maxWidth, (int)(font.GetHeight() * 1.3f)),
            TextAlign = ContentAlignment.MiddleLeft,
            BackColor = Color.Transparent
        };

        if (text != label.Text)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(label, text);
        }

        return label;
    }

    #endregion

    #region Double Buffering Helper

    public static void EnableDoubleBuffering(Control control)
    {
        typeof(Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
            .SetValue(control, true, null);
    }

    #endregion
}
