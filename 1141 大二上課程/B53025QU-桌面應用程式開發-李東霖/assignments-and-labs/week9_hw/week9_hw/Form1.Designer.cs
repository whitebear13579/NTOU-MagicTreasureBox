namespace week9_hw
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openNewFile = new ToolStripMenuItem();
            loadFile = new ToolStripMenuItem();
            saveFile = new ToolStripMenuItem();
            saveNewFile = new ToolStripMenuItem();
            autoSaveButton = new ToolStripMenuItem();
            exitProgram = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutProgramButton = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            clearListButton = new Button();
            delSelectionButton = new Button();
            fetchDataButton = new Button();
            stockLabel = new Label();
            linkLabel1 = new LinkLabel();
            stockCode = new TextBox();
            autoSaveLabel = new Label();
            label3 = new Label();
            fileOpenLabel = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(224, 224, 224);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1032, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openNewFile, loadFile, saveFile, saveNewFile, autoSaveButton, exitProgram });
            fileToolStripMenuItem.Font = new Font("Microsoft JhengHei UI", 12F);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(66, 29);
            fileToolStripMenuItem.Text = "檔案";
            // 
            // openNewFile
            // 
            openNewFile.Name = "openNewFile";
            openNewFile.Size = new Size(178, 30);
            openNewFile.Text = "開新檔案";
            // 
            // loadFile
            // 
            loadFile.Name = "loadFile";
            loadFile.Size = new Size(178, 30);
            loadFile.Text = "載入檔案";
            // 
            // saveFile
            // 
            saveFile.Name = "saveFile";
            saveFile.Size = new Size(178, 30);
            saveFile.Text = "儲存檔案";
            saveFile.Click += 令存新檔ToolStripMenuItem_Click;
            // 
            // saveNewFile
            // 
            saveNewFile.Name = "saveNewFile";
            saveNewFile.Size = new Size(178, 30);
            saveNewFile.Text = "另存新檔";
            // 
            // autoSaveButton
            // 
            autoSaveButton.Name = "autoSaveButton";
            autoSaveButton.Size = new Size(178, 30);
            autoSaveButton.Text = "自動儲存";
            // 
            // exitProgram
            // 
            exitProgram.Name = "exitProgram";
            exitProgram.Size = new Size(178, 30);
            exitProgram.Text = "結束程式";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutProgramButton });
            aboutToolStripMenuItem.Font = new Font("Microsoft JhengHei UI", 12F);
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(66, 29);
            aboutToolStripMenuItem.Text = "關於";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click_1;
            // 
            // aboutProgramButton
            // 
            aboutProgramButton.Name = "aboutProgramButton";
            aboutProgramButton.Size = new Size(224, 30);
            aboutProgramButton.Text = "(>ω<)";
            aboutProgramButton.Click += aboutProgramToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 46);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(753, 392);
            dataGridView1.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(clearListButton);
            groupBox1.Controls.Add(delSelectionButton);
            groupBox1.Controls.Add(fetchDataButton);
            groupBox1.Controls.Add(stockLabel);
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Controls.Add(stockCode);
            groupBox1.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(771, 46);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(249, 283);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "股票代碼查詢";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // clearListButton
            // 
            clearListButton.ForeColor = Color.Black;
            clearListButton.Location = new Point(61, 230);
            clearListButton.Name = "clearListButton";
            clearListButton.Padding = new Padding(3);
            clearListButton.Size = new Size(133, 37);
            clearListButton.TabIndex = 5;
            clearListButton.Text = "清空列表";
            clearListButton.UseVisualStyleBackColor = true;
            // 
            // delSelectionButton
            // 
            delSelectionButton.ForeColor = Color.Black;
            delSelectionButton.Location = new Point(61, 174);
            delSelectionButton.Name = "delSelectionButton";
            delSelectionButton.Padding = new Padding(3);
            delSelectionButton.Size = new Size(133, 37);
            delSelectionButton.TabIndex = 4;
            delSelectionButton.Text = "刪除所選";
            delSelectionButton.UseVisualStyleBackColor = true;
            // 
            // fetchDataButton
            // 
            fetchDataButton.ForeColor = Color.Black;
            fetchDataButton.Location = new Point(61, 118);
            fetchDataButton.Name = "fetchDataButton";
            fetchDataButton.Padding = new Padding(3);
            fetchDataButton.Size = new Size(133, 37);
            fetchDataButton.TabIndex = 3;
            fetchDataButton.Text = "查詢資料";
            fetchDataButton.UseVisualStyleBackColor = true;
            // 
            // stockLabel
            // 
            stockLabel.AutoSize = true;
            stockLabel.Location = new Point(6, 45);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(112, 25);
            stockLabel.TabIndex = 2;
            stockLabel.Text = "股票代碼：";
            stockLabel.Click += label1_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            linkLabel1.LinkColor = Color.FromArgb(146, 196, 234);
            linkLabel1.Location = new Point(47, 81);
            linkLabel1.Margin = new Padding(3);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(167, 22);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "股票資訊來自 TWSE ";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // stockCode
            // 
            stockCode.Location = new Point(124, 42);
            stockCode.Name = "stockCode";
            stockCode.Size = new Size(119, 33);
            stockCode.TabIndex = 0;
            stockCode.TextChanged += textBox1_TextChanged;
            // 
            // autoSaveLabel
            // 
            autoSaveLabel.AutoSize = true;
            autoSaveLabel.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            autoSaveLabel.ForeColor = Color.White;
            autoSaveLabel.Location = new Point(12, 454);
            autoSaveLabel.Name = "autoSaveLabel";
            autoSaveLabel.Size = new Size(152, 25);
            autoSaveLabel.TabIndex = 3;
            autoSaveLabel.Text = "自動儲存已關閉";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(170, 454);
            label3.Name = "label3";
            label3.Size = new Size(17, 25);
            label3.TabIndex = 4;
            label3.Text = "·";
            // 
            // fileOpenLabel
            // 
            fileOpenLabel.AutoSize = true;
            fileOpenLabel.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            fileOpenLabel.ForeColor = Color.White;
            fileOpenLabel.Location = new Point(193, 454);
            fileOpenLabel.Name = "fileOpenLabel";
            fileOpenLabel.Size = new Size(152, 25);
            fileOpenLabel.TabIndex = 5;
            fileOpenLabel.Text = "未開啟任何檔案";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1032, 488);
            Controls.Add(fileOpenLabel);
            Controls.Add(label3);
            Controls.Add(autoSaveLabel);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1050, 535);
            Name = "Form1";
            Text = "股票追蹤小程式";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem openNewFile;
        private ToolStripMenuItem loadFile;
        private ToolStripMenuItem saveFile;
        private ToolStripMenuItem saveNewFile;
        private ToolStripMenuItem autoSaveButton;
        private ToolStripMenuItem exitProgram;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private LinkLabel linkLabel1;
        private TextBox stockCode;
        private Label stockLabel;
        private Button clearListButton;
        private Button delSelectionButton;
        private Button fetchDataButton;
        private Label autoSaveLabel;
        private Label label3;
        private Label fileOpenLabel;
        private ToolStripMenuItem aboutProgramButton;
    }
}
