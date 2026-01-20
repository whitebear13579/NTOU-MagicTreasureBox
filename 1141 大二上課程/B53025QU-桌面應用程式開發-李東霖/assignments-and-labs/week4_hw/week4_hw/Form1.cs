using System.Collections.Generic;
using System.Text;

namespace week4_hw
{
    public partial class Form1 : Form
    {
        bool clearInputBox = false;
        bool isModifying = false;
        bool autoSaveEnabled = false;
        bool hasOpenedFile = false;
        bool hasUnsavedChanges = false;
        string currentFilePath = "";
        ListViewItem modifyingItem = null;
        
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateFileMenuState();
            UpdateStatusStrip();
        }

        private void UpdateFileMenuState()
        {
            openNewFile.Enabled = hasOpenedFile || datalistView.Items.Count > 0;
        }

        private void UpdateStatusStrip()
        {
            autoSaveStatus.Text = autoSaveEnabled ? "自動儲存已開啟" : "自動儲存已關閉";
            
            loadDocPath.Text = hasOpenedFile ? $"已開啟檔案：{currentFilePath}" : "未開啟檔案";
        }

        private void 載入文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (datalistView.Items.Count > 0 && hasUnsavedChanges)
            {
                DialogResult result = MessageBox.Show("確定離開嗎？所有未儲存的資料將會遺失。", "バイバイ？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV 檔案 (*.csv)|*.csv";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "選擇要載入的 CSV 檔案";
                openFileDialog.RestoreDirectory = true;
                bool errorOccurdWhileLoading = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        errorOccurdWhileLoading = LoadCsvFile(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        hasOpenedFile = true;
                        UpdateFileMenuState();
                        UpdateStatusStrip();
                        if (!errorOccurdWhileLoading)
                        {
                            MessageBox.Show($"從 {System.IO.Path.GetFileName(openFileDialog.FileName)} 載入 {datalistView.Items.Count} 筆資料。", "載入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (datalistView.Items.Count > 0)
                        {
                            MessageBox.Show($"部分資料行格式錯誤，程式已自動跳過。\n請檢查 CSV 檔案的完整性。\n僅從文件 {openFileDialog.FileName} 中載入 {datalistView.Items.Count} 筆資料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"檔案 {openFileDialog.FileName} 中沒有任何有效的資料行。\n請檢查 CSV 檔案的完整性。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"嘗試載入檔案 {openFileDialog.FileName} 時發生錯誤：\n{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool LoadCsvFile(string filePath)
        {
            datalistView.Items.Clear();
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            
            bool errorOccurdWhileLoading = false;

            int startIndex = 1;
            
            for (int i = startIndex; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;

                string[] values = ParseCsvLine(line);
                
                if (values.Length >= 3 && !string.IsNullOrWhiteSpace(values[0]) && !string.IsNullOrWhiteSpace(values[1]) && !string.IsNullOrWhiteSpace(values[2]))
                {
                    ListViewItem item = new ListViewItem(values[0].Trim());
                    item.SubItems.Add(values[1].Trim());
                    item.SubItems.Add(values[2].Trim());
                    
                    string timeValue = "-";
                    if (values.Length >= 4 && !string.IsNullOrEmpty(values[3]))
                    {
                        if (DateTime.TryParse(values[3], out DateTime parsedTime))
                        {
                            timeValue = values[3];
                        }
                    }
                    
                    item.SubItems.Add(timeValue);
                    datalistView.Items.Add(item);
                }
                else
                {
                    errorOccurdWhileLoading = true;
                }
            }

            hasUnsavedChanges = false;
            return errorOccurdWhileLoading;

        }

        private string[] ParseCsvLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            StringBuilder currentField = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                
                if (c == '"')
                {
                    inQuotes = !inQuotes;
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
            return result.ToArray();
        }

        private void openNewFile_Click(object sender, EventArgs e)
        {
            if ( datalistView.Items.Count > 0 && hasUnsavedChanges )
            {
                DialogResult result = MessageBox.Show("開新檔案嗎？未儲存的所有項目將遺失", "開新檔案？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            } else
            {
                DialogResult result = MessageBox.Show("開新檔案嗎？所有已讀入的資料將清空", "開新檔案？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            datalistView.Items.Clear();
            currentFilePath = "";
            hasOpenedFile = false;
            hasUnsavedChanges = false;
            UpdateFileMenuState();
            UpdateStatusStrip();
        }

        private void saveAsNew_Click(object sender, EventArgs e)
        {
            if (datalistView.Items.Count == 0)
            {
                DialogResult result = MessageBox.Show("沒有任何資料可以儲存，仍要存檔？", "另存新檔", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV 檔案 (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.Title = "另存新檔";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SaveCsvFile(saveFileDialog.FileName);
                        currentFilePath = saveFileDialog.FileName;
                        hasOpenedFile = true;
                        UpdateFileMenuState();
                        UpdateStatusStrip();
                        MessageBox.Show($"已將 {datalistView.Items.Count} 筆資料儲存至 {currentFilePath}", "儲存檔案", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"儲存檔案時發生錯誤：\n{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {

            if (!hasOpenedFile || string.IsNullOrEmpty(currentFilePath))
            {
                saveAsNew_Click(sender, e);
                return;
            }

            try
            {
                SaveCsvFile(currentFilePath);
                ShowTemporaryStatusMessage($"資料已儲存至： {System.IO.Path.GetFileName(currentFilePath)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存檔案到 {currentFilePath} 時發生錯誤：\n{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ShowTemporaryStatusMessage(string message)
        {
            string originalText = loadDocPath.Text;
            loadDocPath.Text = message;
            await Task.Delay(1500);
            loadDocPath.Text = originalText;
        }

        private void SaveCsvFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("學號,姓名,密碼,新增時間");

                foreach (ListViewItem item in datalistView.Items)
                {
                    string line = $"\"{item.Text}\",\"{item.SubItems[1].Text}\",\"{item.SubItems[2].Text}\",\"{item.SubItems[3].Text}\"";
                    writer.WriteLine(line);
                }
            }
            
            hasUnsavedChanges = false;
        }

        private void autoSave_Click(object sender, EventArgs e)
        {
            autoSaveEnabled = !autoSaveEnabled;
            autoSave.Checked = autoSaveEnabled;
            UpdateStatusStrip();

            string message = autoSaveEnabled ? "自動儲存已開啟" : "自動儲存已關閉";
        }

        private void AutoSaveIfEnabled()
        {
            if (autoSaveEnabled && hasOpenedFile && !string.IsNullOrEmpty(currentFilePath))
            {
                try
                {
                    SaveCsvFile(currentFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"自動儲存檔案時發生錯誤：\n{ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void exitProgram_Click(object sender, EventArgs e)
        {
            if (datalistView.Items.Count > 0 && hasUnsavedChanges)
            {
                DialogResult result = MessageBox.Show("確定離開嗎？所有未儲存的資料將會遺失。", "バイバイ？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No) return;
            }
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void addValue_Click(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            if (studentsId.Text == "" || studentName.Text == "" || pwdValue.Text == "")
            {
                MessageBox.Show("請輸入完整資料", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem data = new ListViewItem();
            data.Text = studentsId.Text;
            data.SubItems.Add(studentName.Text);
            data.SubItems.Add(pwdValue.Text);
            data.SubItems.Add(currentTime.ToString("yyyy/MM/dd HH:mm:ss"));
            datalistView.Items.Add(data);

            hasUnsavedChanges = true;

            if (clearInputBox)
            {
                studentsId.Clear();
                studentName.Clear();
                pwdValue.Clear();
            }
            datalistView.EnsureVisible(datalistView.Items.Count - 1);
            
            UpdateFileMenuState();
            AutoSaveIfEnabled();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void autoSaveStatus_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void about_Click(object sender, EventArgs e)
        {
            MessageBox.Show("學生資料管理系統 ver1.0\n\nNTOU CS2B 01357101\nYI HONG, HUANG\n\nThis Windows Form App is the week4 assignment for the course,\n" +
                "\"Desktop Application Development\" (B53025QU).\n\nAuthor : github/whitebear13579\nApp Licensed to :\n                             (ヾﾉ･ω･`).",
              "關於學生資料管理系統", MessageBoxButtons.OK);
        }

        private void datalistView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void studentsId_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearList_Click(object sender, EventArgs e)
        {
            if (datalistView.Items.Count == 0)
            {
                MessageBox.Show("列表中沒有任何資料", "列表為空", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult result = MessageBox.Show("確定要清除所有資料嗎？", "清除資料", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;
            
            datalistView.Items.Clear();
            hasUnsavedChanges = true;
            UpdateFileMenuState();
            AutoSaveIfEnabled();
        }

        private void clearTextBoxAlways_CheckedChanged(object sender, EventArgs e)
        {
            if (clearTextBoxAlways.Checked) clearInputBox = true;
            else clearInputBox = false;
        }

        private void delValue_Click(object sender, EventArgs e)
        {
            if (datalistView.Items.Count == 0)
            {
                MessageBox.Show("列表中沒有任何資料", "列表為空", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (datalistView.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show($"確定要刪除選中的資料嗎？\n學號：{datalistView.SelectedItems[0].Text}",
                    "刪除資料？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    datalistView.Items.Remove(datalistView.SelectedItems[0]);
                    hasUnsavedChanges = true;
                    MessageBox.Show("所選資料已刪除", "資料已刪除", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateFileMenuState();
                    AutoSaveIfEnabled();
                }
                return;
            }

            if (studentName.Text != "" || pwdValue.Text != "")
            {
                MessageBox.Show("刪除資料填寫學號即可，或者直接選取列表", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (studentsId.Text == "")
            {
                MessageBox.Show("請輸入學號", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string delData = studentsId.Text;

            List<ListViewItem> matchingItems = new List<ListViewItem>();
            foreach (ListViewItem item in datalistView.Items)
            {
                if (item.Text == delData)
                {
                    matchingItems.Add(item);
                }
            }

            if (matchingItems.Count == 0)
            {
                MessageBox.Show("找不到符合的學號資料", "請檢查學號", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (matchingItems.Count == 1)
            {
                DialogResult result = MessageBox.Show($"確定要刪除這筆資料嗎？\n學號：{matchingItems[0].Text}\n姓名：{matchingItems[0].SubItems[1].Text}",
                    "刪除資料？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    datalistView.Items.Remove(matchingItems[0]);
                    hasUnsavedChanges = true;
                    MessageBox.Show("資料已刪除", "刪除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (clearInputBox)
                    {
                        studentsId.Clear();
                    }
                    UpdateFileMenuState();
                    AutoSaveIfEnabled();
                }
                return;
            }

            DialogResult multipleResult = MessageBox.Show($"找到 {matchingItems.Count} 筆相同學號的資料，確定要刪除所有資料嗎？",
                "刪除全部資料？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (multipleResult == DialogResult.Yes)
            {
                foreach (ListViewItem item in matchingItems)
                {
                    datalistView.Items.Remove(item);
                }
                hasUnsavedChanges = true;
                MessageBox.Show($"已刪除 {matchingItems.Count} 筆資料", "資料已刪除", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateFileMenuState();
                AutoSaveIfEnabled();
            }
            else
            {
                DialogResult oldestResult = MessageBox.Show("是否刪除最舊的資料？",
                    "刪除最舊資料？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (oldestResult == DialogResult.Yes)
                {
                    ListViewItem oldestItem = matchingItems[0];
                    DateTime oldestTime = DateTime.Parse(oldestItem.SubItems[3].Text);

                    foreach (ListViewItem item in matchingItems)
                    {
                        DateTime itemTime = DateTime.Parse(item.SubItems[3].Text);
                        if (itemTime < oldestTime)
                        {
                            oldestTime = itemTime;
                            oldestItem = item;
                        }
                    }

                    datalistView.Items.Remove(oldestItem);
                    hasUnsavedChanges = true;
                    MessageBox.Show($"從列表中刪除了最舊的資料\n新增時間：{oldestItem.SubItems[3].Text}",
                        "資料已刪除", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateFileMenuState();
                    AutoSaveIfEnabled();
                }
            }

            if (clearInputBox)
            {
                studentsId.Clear();
            }
        }

        private void modifyValue_Click(object sender, EventArgs e)
        {
            if (!isModifying)
            {
                StartModifyMode();
            }
            else
            {
                FinishModifyMode();
            }
        }

        private void StartModifyMode()
        {
            if (datalistView.SelectedItems.Count > 0)
            {
                modifyingItem = datalistView.SelectedItems[0];
                LoadDataToInputBoxes(modifyingItem);
                EnableModifyMode(modifyingItem);
                return;
            }

            if (studentsId.Text == "")
            {
                MessageBox.Show("請輸入學號或選擇要修改的資料", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (studentName.Text != "" || pwdValue.Text != "")
            {
                MessageBox.Show("修改資料請填寫學號即可，或者直接選取列表", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string searchId = studentsId.Text;
            List<ListViewItem> matchingItems = new List<ListViewItem>();
            
            foreach (ListViewItem item in datalistView.Items)
            {
                if (item.Text == searchId)
                {
                    matchingItems.Add(item);
                }
            }

            if (matchingItems.Count == 0)
            {
                MessageBox.Show("找不到符合的學號資料", "請檢查學號", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (matchingItems.Count > 1)
            {
                ListViewItem newestItem = matchingItems[0];
                DateTime newestTime = DateTime.Parse(newestItem.SubItems[3].Text);

                foreach (ListViewItem item in matchingItems)
                {
                    DateTime itemTime = DateTime.Parse(item.SubItems[3].Text);
                    if (itemTime > newestTime)
                    {
                        newestTime = itemTime;
                        newestItem = item;
                    }
                }
                modifyingItem = newestItem;
            }
            else
            {
                modifyingItem = matchingItems[0];
            }

            LoadDataToInputBoxes(modifyingItem);
            EnableModifyMode(modifyingItem);
        }

        private void LoadDataToInputBoxes(ListViewItem item)
        {
            studentsId.Text = item.Text;
            studentName.Text = item.SubItems[1].Text;
            pwdValue.Text = item.SubItems[2].Text;
        }

        private void EnableModifyMode(ListViewItem item)
        {
            isModifying = true;
            modifyValue.Text = "完成修改";
            
            addValue.Enabled = false;
            delValue.Enabled = false;
            clearList.Enabled = false;
            
            autoSaveStatus.Text = "已開啟修改模式";
            MessageBox.Show($"嘗試修改學號： {item.Text} 於 {item.SubItems[3].Text} 的資料", "已進入修改模式", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FinishModifyMode()
        {

            if (studentsId.Text == "" || studentName.Text == "" || pwdValue.Text == "")
            {
                MessageBox.Show("請輸入完整資料", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime currentTime = DateTime.Now;
            modifyingItem.Text = studentsId.Text;
            modifyingItem.SubItems[1].Text = studentName.Text;
            modifyingItem.SubItems[2].Text = pwdValue.Text;
            modifyingItem.SubItems[3].Text = currentTime.ToString("yyyy/MM/dd HH:mm:ss");

            hasUnsavedChanges = true;

            datalistView.EnsureVisible(datalistView.Items.IndexOf(modifyingItem));

            isModifying = false;
            modifyValue.Text = "修改資料";
            addValue.Enabled = true;
            delValue.Enabled = true;
            clearList.Enabled = true;
            UpdateStatusStrip();

            modifyingItem = null;

            MessageBox.Show("所指定資料已更新", "修改完成", MessageBoxButtons.OK, MessageBoxIcon.Information);

            studentsId.Clear();
            studentName.Clear();
            pwdValue.Clear();
            
            AutoSaveIfEnabled();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (datalistView.Items.Count > 0 && hasUnsavedChanges)
            {
                DialogResult result = MessageBox.Show("確定離開嗎？所有未儲存的資料將會遺失。", "バイバイ？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
