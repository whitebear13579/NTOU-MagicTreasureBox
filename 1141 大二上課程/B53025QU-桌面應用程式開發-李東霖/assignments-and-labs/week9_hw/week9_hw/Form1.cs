using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace week9_hw
{
    public partial class Form1 : Form
    {
        private string? currentFilePath = null;
        private bool isAutoSaveEnabled = false;
        private System.Windows.Forms.Timer? autoSaveTimer;

        public Form1()
        {
            InitializeComponent();
            SetupEventHandlers();
            SetupAutoSaveTimer();
            UpdateFileOpenLabel();
            UpdateMenuItemStates();
        }

        private void SetupEventHandlers()
        {
            fetchDataButton.Click += FetchDataButton_Click;
            delSelectionButton.Click += DelSelectionButton_Click;
            clearListButton.Click += ClearListButton_Click;

            openNewFile.Click += OpenNewFile_Click;
            loadFile.Click += LoadFile_Click;
            saveFile.Click += SaveFile_Click;
            saveNewFile.Click += SaveNewFile_Click;
            autoSaveButton.Click += AutoSaveButton_Click;
            exitProgram.Click += ExitProgram_Click;
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            dataGridView1.RowsAdded += DataGridView1_DataChanged;
            dataGridView1.RowsRemoved += DataGridView1_DataChanged;
        }

        private void SetupAutoSaveTimer()
        {
            autoSaveTimer = new System.Windows.Forms.Timer();
            autoSaveTimer.Interval = 30000;
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
        }

        private void UpdateFileOpenLabel()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                fileOpenLabel.Text = "未開啟任何檔案";
            }
            else
            {
                fileOpenLabel.Text = $"檔案：{Path.GetFileName(currentFilePath)}";
            }
        }

        private void UpdateMenuItemStates()
        {
            int actualRowCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    actualRowCount++;
                }
            }

            bool hasData = actualRowCount > 0 && dataGridView1.Columns.Count > 0;
            openNewFile.Enabled = hasData;
            saveFile.Enabled = hasData;
            saveNewFile.Enabled = hasData;
        }

        private async void FetchDataButton_Click(object? sender, EventArgs e)
        {
            string code = stockCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("請輸入股票代碼！", "輸入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                fetchDataButton.Enabled = false;
                fetchDataButton.Text = "查詢中...";
                Cursor = Cursors.WaitCursor;

                DateTime date = DateTime.Now;
                string jsonData = await FetchStockDataAsync(code, date);

                JObject json = JObject.Parse(jsonData);
                string? stat = json["stat"]?.ToString();
                int? total = json["total"]?.Value<int>();

                if (total == 0 || (stat != null && stat.Contains("沒有符合條件的資料")))
                {
                    MessageBox.Show($"股票代碼： {code} 不存在或無資料！\n請確認股票代碼是否正確。", "資料查詢失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    stockCode.Clear();
                    return;
                }

                ParseAndDisplayStockData(jsonData, code);
                UpdateMenuItemStates();
                stockCode.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢失敗：{ex.Message}", "資料查詢失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fetchDataButton.Enabled = true;
                fetchDataButton.Text = "查詢資料";
                Cursor = Cursors.Default;
            }
        }

        private async Task<string> FetchStockDataAsync(string stockCode, DateTime date)
        {
            using (var client = new HttpClient())
            {
                string url = $"https://www.twse.com.tw/exchangeReport/STOCK_DAY?date={date.ToString("yyyyMMdd")}&stockNo={stockCode}";
                var response = await client.GetStringAsync(url);
                return response;
            }
        }

        private void ParseAndDisplayStockData(string jsonData, string stockCode)
        {
            try
            {
                JObject json = JObject.Parse(jsonData);

                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("股票代碼", "股票代碼");

                    JArray? fields = json["fields"] as JArray;
                    if (fields != null)
                    {
                        foreach (var field in fields)
                        {
                            dataGridView1.Columns.Add(field.ToString(), field.ToString());
                        }
                    }
                }

                JArray? data = json["data"] as JArray;
                if (data != null)
                {
                    foreach (JArray row in data)
                    {
                        List<string> rowData = new List<string>();

                        rowData.Add(stockCode);

                        foreach (var cell in row)
                        {
                            rowData.Add(cell.ToString());
                        }

                        dataGridView1.Rows.Add(rowData.ToArray());
                    }
                }

                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                throw new Exception($"解析 JSON 失敗：{ex.Message}");
            }
        }

        private void DelSelectionButton_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇要刪除的行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("確定要刪除所選的行嗎？", "確定刪除嗎？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }
                UpdateMenuItemStates();
            }
        }

        private void ClearListButton_Click(object? sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("列表已經是空的！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("確定要清空欄位資料嗎？所有未儲存的資料都將遺失。", "確定清空嗎？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                UpdateMenuItemStates();
            }
        }

        private void OpenNewFile_Click(object? sender, EventArgs e)
        {
            int actualRowCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    actualRowCount++;
                }
            }

            bool hasData = actualRowCount > 0 && dataGridView1.Columns.Count > 0;

            if (!hasData)
            {
                return;
            }

            var result = MessageBox.Show("開新檔案會清空目前欄位資料，所有未儲存的資料都將遺失。\n確定要繼續嗎？", "開新檔案嗎？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                currentFilePath = null;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                UpdateFileOpenLabel();
                UpdateMenuItemStates();
            }
        }

        private void LoadFile_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        LoadCsvFile(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        UpdateFileOpenLabel();
                        UpdateMenuItemStates();
                        MessageBox.Show("檔案載入成功！", "載入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"載入檔案失敗：{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadCsvFile(string filePath)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            if (lines.Length == 0)
            {
                throw new Exception("檔案是空的！");
            }

            List<string> headers = ParseCsvLine(lines[0]);
            foreach (string header in headers)
            {
                dataGridView1.Columns.Add(header, header);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    List<string> cells = ParseCsvLine(lines[i]);
                    
                    while (cells.Count < headers.Count)
                    {
                        cells.Add("");
                    }
                    
                    dataGridView1.Rows.Add(cells.ToArray());
                }
            }

            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private List<string> ParseCsvLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            StringBuilder currentField = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        currentField.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(currentField.ToString());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }

            result.Add(currentField.ToString());

            return result;
        }

        private void SaveFile_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveNewFile_Click(sender, e);
            }
            else
            {
                try
                {
                    SaveCsvFile(currentFilePath);
                    MessageBox.Show($"檔案已成功儲存。", "檔案已儲存！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"儲存檔案失敗：{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveNewFile_Click(object? sender, EventArgs e)
        {
            int actualRowCount = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    actualRowCount++;
                }
            }

            if (actualRowCount == 0 || dataGridView1.Columns.Count == 0)
            {
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = $"股票資料_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SaveCsvFile(saveFileDialog.FileName);
                        currentFilePath = saveFileDialog.FileName;
                        UpdateFileOpenLabel();
                        MessageBox.Show($"檔案已儲存至以下位置：\n{currentFilePath}", "檔案已儲存！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"儲存檔案失敗：{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SaveCsvFile(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            List<string> headers = new List<string>();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                headers.Add($"\"{column.HeaderText}\"");
            }
            sb.AppendLine(string.Join(",", headers));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    List<string> cells = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string value = cell.Value?.ToString() ?? "";
                        value = value.Replace("\"", "\"\"");
                        cells.Add($"\"{value}\"");
                    }
                    sb.AppendLine(string.Join(",", cells));
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void AutoSaveButton_Click(object? sender, EventArgs e)
        {
            isAutoSaveEnabled = !isAutoSaveEnabled;

            if (isAutoSaveEnabled)
            {
                autoSaveTimer?.Start();
                autoSaveLabel.Text = "自動儲存已開啟";
                autoSaveButton.Checked = true;
            }
            else
            {
                autoSaveTimer?.Stop();
                autoSaveLabel.Text = "自動儲存已關閉";
                autoSaveButton.Checked = false;
            }
        }

        private void AutoSaveTimer_Tick(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                int actualRowCount = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        actualRowCount++;
                    }
                }

                if (actualRowCount > 0)
                {
                    try
                    {
                        SaveCsvFile(currentFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"程式發生未知的錯誤：{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DataGridView1_DataChanged(object? sender, EventArgs e)
        {
            UpdateMenuItemStates();
        }

        private void ExitProgram_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("確定離開嗎？所有未儲存的資料將會遺失。", "關閉程式？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AboutToolStripMenuItem_Click(object? sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 令存新檔ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://www.twse.com.tw/",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"無法開啟網頁：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("股票追蹤小程式 ver1.0\n\nNTOU CS2B 01357101\nYI HONG, HUANG\n\nThis Windows Form App is the week9 assignment for the course,\n" + "\"Desktop Application Development\" (B53025QU).\n\nAuthor : github/whitebear13579\nApp Licensed to :\n                             (>ω<).\r\n", "關於股票追蹤小程式", MessageBoxButtons.OK);
        }
    }
}
