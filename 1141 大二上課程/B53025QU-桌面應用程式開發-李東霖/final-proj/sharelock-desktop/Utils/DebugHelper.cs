using System.Diagnostics;

namespace sharelock_desktop.Utils;

public static class DebugHelper
{
    public static void ScanFormForClippingIssues(Form form)
    {
#if DEBUG
        Debug.WriteLine($"=== 開始掃描表單 '{form.Text}' 的裁切問題 ===");
        
        var issues = UIConstants.ScanForClippingIssues(form);
        
        if (issues.Count == 0)
        {
            Debug.WriteLine("? 未發現潛在的裁切問題");
        }
        else
        {
            Debug.WriteLine($"? 發現 {issues.Count} 個潛在問題：");
            foreach (var issue in issues)
            {
                Debug.WriteLine($"  - {issue}");
            }
        }
        
        Debug.WriteLine($"=== 掃描完成 ===");
#endif
    }

    public static void DumpControlHierarchy(Control control, int maxDepth = 5)
    {
#if DEBUG
        Debug.WriteLine($"=== 控件層級結構 ===");
        DumpControlRecursive(control, 0, maxDepth);
        Debug.WriteLine($"=== 結束 ===");
#endif
    }

    private static void DumpControlRecursive(Control control, int depth, int maxDepth)
    {
        if (depth > maxDepth) return;

        string indent = new string(' ', depth * 2);
        string typeName = control.GetType().Name;
        string name = string.IsNullOrEmpty(control.Name) ? "(unnamed)" : control.Name;
        string size = $"{control.Width}x{control.Height}";
        string location = $"({control.Left},{control.Top})";
        string dock = control.Dock != DockStyle.None ? $" Dock={control.Dock}" : "";
        string text = control is Label lbl && !string.IsNullOrEmpty(lbl.Text) 
            ? $" Text=\"{TruncateForDebug(lbl.Text, 20)}\"" 
            : "";

        Debug.WriteLine($"{indent}[{typeName}] {name} Size={size} Loc={location}{dock}{text}");

        foreach (Control child in control.Controls)
        {
            DumpControlRecursive(child, depth + 1, maxDepth);
        }
    }

    public static void LogDpiInfo()
    {
#if DEBUG
        Debug.WriteLine($"=== DPI 資訊 ===");
        
        float dpiScale = UIConstants.GetDpiScale();
        Debug.WriteLine($"DPI 縮放因子: {dpiScale:F2}");
        Debug.WriteLine($"有效 DPI: {dpiScale * 96}");
        
        using var g = Graphics.FromHwnd(IntPtr.Zero);
        Debug.WriteLine($"Graphics DpiX: {g.DpiX}");
        Debug.WriteLine($"Graphics DpiY: {g.DpiY}");
        
        Debug.WriteLine($"=== 結束 ===");
#endif
    }

    public static void MeasureTextDebug(string text, Font font, int containerWidth)
    {
#if DEBUG
        int textWidth = UIConstants.MeasureTextWidth(text, font);
        bool willClip = textWidth > containerWidth;
        
        Debug.WriteLine($"文字測量: \"{TruncateForDebug(text, 30)}\"");
        Debug.WriteLine($"  字體: {font.Name} {font.Size}pt {font.Style}");
        Debug.WriteLine($"  文字寬度: {textWidth}px");
        Debug.WriteLine($"  容器寬度: {containerWidth}px");
        Debug.WriteLine($"  可能裁切: {(willClip ? "是 ?" : "否 ?")}");
#endif
    }

    public static void HighlightControl(Control control, Color? color = null)
    {
#if DEBUG
        color ??= Color.Red;
        control.Paint += (s, e) =>
        {
            using var pen = new Pen(color.Value, 2);
            var rect = control.ClientRectangle;
            rect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(pen, rect);
        };
        control.Invalidate();
#endif
    }

    public static void ShowControlInfo(Control control)
    {
#if DEBUG
        control.Paint += (s, e) =>
        {
            var info = $"{control.GetType().Name}\n{control.Width}x{control.Height}";
            using var font = new Font("Consolas", 8);
            using var brush = new SolidBrush(Color.FromArgb(200, Color.Yellow));
            using var bgBrush = new SolidBrush(Color.FromArgb(150, Color.Black));
            
            var size = e.Graphics.MeasureString(info, font);
            var rect = new RectangleF(2, 2, size.Width + 4, size.Height + 2);
            
            e.Graphics.FillRectangle(bgBrush, rect);
            e.Graphics.DrawString(info, font, brush, 4, 2);
        };
        control.Invalidate();
#endif
    }

    private static string TruncateForDebug(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "(empty)";
        if (text.Length <= maxLength) return text;
        return text[..(maxLength - 3)] + "...";
    }

    public static void ValidateLayout(Control control)
    {
#if DEBUG
        Debug.WriteLine($"=== 布局驗證: {control.GetType().Name} '{control.Name}' ===");
        
        if (control is TableLayoutPanel tlp)
        {
            Debug.WriteLine($"TableLayoutPanel: {tlp.ColumnCount} 列, {tlp.RowCount} 行");
            
            for (int i = 0; i < tlp.ColumnStyles.Count; i++)
            {
                var style = tlp.ColumnStyles[i];
                Debug.WriteLine($"  列 {i}: {style.SizeType} = {style.Width}");
            }
            
            for (int i = 0; i < tlp.RowStyles.Count; i++)
            {
                var style = tlp.RowStyles[i];
                Debug.WriteLine($"  行 {i}: {style.SizeType} = {style.Height}");
            }
        }
        
        if (control is FlowLayoutPanel flp)
        {
            Debug.WriteLine($"FlowLayoutPanel: 方向={flp.FlowDirection}, 自動換行={flp.WrapContents}, 自動捲動={flp.AutoScroll}");
        }
        
        Debug.WriteLine($"Dock: {control.Dock}");
        Debug.WriteLine($"Anchor: {control.Anchor}");
        Debug.WriteLine($"AutoSize: {control.AutoSize}");
        
        Debug.WriteLine($"=== 驗證完成 ===");
#endif
    }
}
