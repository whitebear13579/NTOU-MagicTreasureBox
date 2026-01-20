namespace week3_hw
{
    public partial class mainWindows : Form
    {
        private string currentFilePath = "";
        private bool isAutoSaveEnabled = false;
        private System.Windows.Forms.Timer autoSaveTimer;
        private System.Windows.Forms.Timer labelResetTimer;
        private bool isTextChanged = false;

        public mainWindows()
        {
            InitializeComponent();
            richTextBox1.ForeColor = Color.White;
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
            
            autoSaveTimer = new System.Windows.Forms.Timer();
            autoSaveTimer.Interval = 30000;
            // auto sace every 30 seconds
            autoSaveTimer.Tick += AutoSaveTimer_Tick;

            // auto save label timer (5 seconds)
            labelResetTimer = new System.Windows.Forms.Timer();
            labelResetTimer.Interval = 1500;
            labelResetTimer.Tick += LabelResetTimer_Tick;
            
            UpdateStatusLabels();
            UpdateMenuStates();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
            UpdateStatusLabels();
            UpdateMenuStates();
        }

        private void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            if (isAutoSaveEnabled && !string.IsNullOrEmpty(currentFilePath) && isTextChanged)
            {
                SaveFile(currentFilePath);
            }
        }

        private void LabelResetTimer_Tick(object sender, EventArgs e)
        {
            labelResetTimer.Stop();
            UpdateStatusLabels();
        }

        private void UpdateStatusLabels()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                label3.Text = "未開啟檔案";
            }
            else
            {
                label3.Text = $"已開啟：{Path.GetFileName(currentFilePath)}";
            }

            if (isAutoSaveEnabled)
            {
                label1.Text = "自動儲存已開啟";
            }
            else
            {
                label1.Text = "自動儲存已關閉";
            }
            autoSave.Checked = isAutoSaveEnabled;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openNewFile_Click(object sender, EventArgs e)
        {   
            if (ConfirmSaveChanges())
            {
                richTextBox1.Clear();
                currentFilePath = "";
                isTextChanged = false;
                UpdateStatusLabels();
                UpdateMenuStates();
            }
        }

        private void UpdateMenuStates()
        {
            bool hasContent = !string.IsNullOrEmpty(richTextBox1.Text.Trim());
            bool hasOpenFile = !string.IsNullOrEmpty(currentFilePath);
            
            openNewFile.Enabled = hasContent || hasOpenFile;
        }

        private void saveToNewFile_Click(object sender, EventArgs e)
        {
            SaveAsDocument();
        }

        private void loadDoc_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "文字檔 (*.txt)|*.txt|RTF 檔案 (*.rtf)|*.rtf";
                    openFileDialog.Title = "開啟文字文件";
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            currentFilePath = openFileDialog.FileName;
                            if (Path.GetExtension(currentFilePath).ToLower() == ".rtf")
                            {
                                richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                            }
                            else
                            {
                                richTextBox1.Text = File.ReadAllText(currentFilePath);
                            }
                            isTextChanged = false;
                            UpdateStatusLabels();
                            UpdateMenuStates();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"載入檔案失敗: {ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void SaveDocument()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAsDocument();
            }
            else
            {
                SaveFile(currentFilePath);
            }
        }

        private void SaveAsDocument()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "文字檔 (*.txt)|*.txt|RTF 檔案 (*.rtf)|*.rtf";
                saveFileDialog.Title = "另存新檔";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    SaveFile(currentFilePath);
                }
            }
        }

        private void SaveFile(string filePath)
        {
            try
            {
                if (Path.GetExtension(filePath).ToLower() == ".rtf")
                {
                    richTextBox1.SaveFile(filePath, RichTextBoxStreamType.RichText);
                }
                else
                {
                    File.WriteAllText(filePath, richTextBox1.Text);
                }
                isTextChanged = false;
                UpdateStatusLabels();
                
                label1.Text = "這份檔案已儲存";
                labelResetTimer.Stop();
                labelResetTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存檔案失敗: {ex.Message}", "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void autoSave_Click(object sender, EventArgs e)
        {
            isAutoSaveEnabled = !isAutoSaveEnabled;
            if (isAutoSaveEnabled)
            {
                autoSaveTimer.Start();
            }
            else
            {
                autoSaveTimer.Stop();
            }
            
            autoSave.Checked = isAutoSaveEnabled;
            
            UpdateStatusLabels();
        }

        private void closeApp_Click(object sender, EventArgs e)
        {
            if (ConfirmSaveChanges())
            {
                Application.Exit();
            }
        }

        private void 編輯ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cut_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.Cut();
                label1.Text = "所選文字已剪下";
                labelResetTimer.Stop();
                labelResetTimer.Start();
            }
        }

        // 複製
        private void copy_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.Copy();
                label1.Text = "所選文字已複製";
                labelResetTimer.Stop();
                labelResetTimer.Start();
            }
        }

        private void paste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Paste();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.SelectedText = "";
            }
        }

        private void find_Click(object sender, EventArgs e)
        {
            string searchText = Microsoft.VisualBasic.Interaction.InputBox("請輸入欲尋找的內容:", "尋找", "");
            if (!string.IsNullOrEmpty(searchText))
            {
                int index = richTextBox1.Find(searchText, RichTextBoxFinds.None);
                if (index == -1)
                {
                    MessageBox.Show($"在文件中找不到： {searchText}", "尋找", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void fontSet_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = richTextBox1.SelectionFont ?? richTextBox1.Font;
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionFont = fontDialog.Font;
                }
            }
        }

        private void colorSet_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = richTextBox1.SelectionColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SelectionColor = colorDialog.Color;
                }
            }
        }

        private void SetFontSize(float size)
        {
            Font currentFont = richTextBox1.SelectionFont ?? richTextBox1.Font;
            richTextBox1.SelectionFont = new Font(currentFont.FontFamily, size, currentFont.Style);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e) => SetFontSize(12f);
        private void toolStripMenuItem8_Click(object sender, EventArgs e) => SetFontSize(16f);
        private void toolStripMenuItem9_Click(object sender, EventArgs e) => SetFontSize(20f);
        private void toolStripMenuItem10_Click(object sender, EventArgs e) => SetFontSize(24f);
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => SetFontSize(28f);
        private void toolStripMenuItem12_Click(object sender, EventArgs e) => SetFontSize(32f);
        private void toolStripMenuItem13_Click(object sender, EventArgs e) => SetFontSize(36f);
        private void toolStripMenuItem14_Click(object sender, EventArgs e) => SetFontSize(40f);
        private void toolStripMenuItem15_Click(object sender, EventArgs e) => SetFontSize(44f);
        private void toolStripMenuItem16_Click(object sender, EventArgs e) => SetFontSize(48f);

        // 關於程式
        private void aboutMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("文字編輯器 ver1.0\n\nNTOU CS2B 01357101\nYI HONG, HUANG\n\nThis Windows Form App is the week3 assignment for the course,\n\"Desktop Application Development\" (B53025QU).\n\nAuthor : github/whitebear13579\nApp Licensed to :\n                             Ciallo (∠·ω )⌒★.", 
                          "關於文字編輯器", MessageBoxButtons.OK);
        }

        private bool ConfirmSaveChanges()
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("文件已被修改，儲存變更？", "儲存文件", 
                                                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SaveDocument();
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void 程式主選單_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!ConfirmSaveChanges())
            {
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }
    }
}
