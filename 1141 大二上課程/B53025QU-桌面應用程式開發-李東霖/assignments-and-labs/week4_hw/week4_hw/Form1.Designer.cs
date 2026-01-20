namespace week4_hw
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
            fileToolStrip = new ToolStripMenuItem();
            openNewFile = new ToolStripMenuItem();
            loadDoc = new ToolStripMenuItem();
            saveAsNew = new ToolStripMenuItem();
            儲存檔案ToolStripMenuItem = new ToolStripMenuItem();
            autoSave = new ToolStripMenuItem();
            exitProgram = new ToolStripMenuItem();
            aboutToolStrip = new ToolStripMenuItem();
            about = new ToolStripMenuItem();
            datalistView = new ListView();
            studentID = new ColumnHeader();
            name = new ColumnHeader();
            password = new ColumnHeader();
            addTime = new ColumnHeader();
            studentDataGroup = new GroupBox();
            clearTextBoxAlways = new CheckBox();
            pwdValue = new TextBox();
            studentName = new TextBox();
            passwordLabel = new Label();
            studentNameLabel = new Label();
            studentIdLabel = new Label();
            studentsId = new TextBox();
            addValue = new Button();
            delValue = new Button();
            modifyValue = new Button();
            clearList = new Button();
            statusStrip1 = new StatusStrip();
            autoSaveStatus = new ToolStripStatusLabel();
            slice = new ToolStripStatusLabel();
            loadDocPath = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            studentDataGroup.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(224, 224, 224);
            menuStrip1.Font = new Font("Microsoft JhengHei UI", 12F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStrip, aboutToolStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(634, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStrip
            // 
            fileToolStrip.DropDownItems.AddRange(new ToolStripItem[] { openNewFile, loadDoc, saveAsNew, 儲存檔案ToolStripMenuItem, autoSave, exitProgram });
            fileToolStrip.Name = "fileToolStrip";
            fileToolStrip.Size = new Size(53, 24);
            fileToolStrip.Text = "檔案";
            // 
            // openNewFile
            // 
            openNewFile.Name = "openNewFile";
            openNewFile.Size = new Size(142, 24);
            openNewFile.Text = "開新檔案";
            openNewFile.Click += openNewFile_Click;
            // 
            // loadDoc
            // 
            loadDoc.Name = "loadDoc";
            loadDoc.Size = new Size(142, 24);
            loadDoc.Text = "載入檔案";
            loadDoc.Click += 載入文件ToolStripMenuItem_Click;
            // 
            // saveAsNew
            // 
            saveAsNew.Name = "saveAsNew";
            saveAsNew.Size = new Size(142, 24);
            saveAsNew.Text = "另存新檔";
            saveAsNew.Click += saveAsNew_Click;
            // 
            // 儲存檔案ToolStripMenuItem
            // 
            儲存檔案ToolStripMenuItem.Name = "儲存檔案ToolStripMenuItem";
            儲存檔案ToolStripMenuItem.Size = new Size(142, 24);
            儲存檔案ToolStripMenuItem.Text = "儲存檔案";
            儲存檔案ToolStripMenuItem.Click += saveFile_Click;
            // 
            // autoSave
            // 
            autoSave.Name = "autoSave";
            autoSave.Size = new Size(142, 24);
            autoSave.Text = "自動儲存";
            autoSave.Click += autoSave_Click;
            // 
            // exitProgram
            // 
            exitProgram.Name = "exitProgram";
            exitProgram.Size = new Size(142, 24);
            exitProgram.Text = "結束程式";
            exitProgram.Click += exitProgram_Click;
            // 
            // aboutToolStrip
            // 
            aboutToolStrip.DropDownItems.AddRange(new ToolStripItem[] { about });
            aboutToolStrip.Name = "aboutToolStrip";
            aboutToolStrip.Size = new Size(53, 24);
            aboutToolStrip.Text = "關於";
            // 
            // about
            // 
            about.Name = "about";
            about.Size = new Size(147, 24);
            about.Text = "(ヾﾉ･ω･`)";
            about.Click += about_Click;
            // 
            // datalistView
            // 
            datalistView.BackColor = Color.White;
            datalistView.Columns.AddRange(new ColumnHeader[] { studentID, name, password, addTime });
            datalistView.ForeColor = Color.Black;
            datalistView.GridLines = true;
            datalistView.Location = new Point(11, 37);
            datalistView.Margin = new Padding(2, 9, 2, 2);
            datalistView.Name = "datalistView";
            datalistView.Size = new Size(393, 316);
            datalistView.TabIndex = 1;
            datalistView.UseCompatibleStateImageBehavior = false;
            datalistView.View = View.Details;
            datalistView.SelectedIndexChanged += datalistView_SelectedIndexChanged;
            // 
            // studentID
            // 
            studentID.Text = "學號（ID）";
            studentID.Width = 80;
            // 
            // name
            // 
            name.Text = "姓名";
            name.Width = 80;
            // 
            // password
            // 
            password.Text = "密碼";
            password.Width = 80;
            // 
            // addTime
            // 
            addTime.Text = "新增時間";
            addTime.Width = 180;
            // 
            // studentDataGroup
            // 
            studentDataGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            studentDataGroup.Controls.Add(clearTextBoxAlways);
            studentDataGroup.Controls.Add(pwdValue);
            studentDataGroup.Controls.Add(studentName);
            studentDataGroup.Controls.Add(passwordLabel);
            studentDataGroup.Controls.Add(studentNameLabel);
            studentDataGroup.Controls.Add(studentIdLabel);
            studentDataGroup.Controls.Add(studentsId);
            studentDataGroup.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            studentDataGroup.ForeColor = Color.White;
            studentDataGroup.Location = new Point(416, 37);
            studentDataGroup.Margin = new Padding(2);
            studentDataGroup.Name = "studentDataGroup";
            studentDataGroup.Padding = new Padding(2);
            studentDataGroup.Size = new Size(207, 180);
            studentDataGroup.TabIndex = 2;
            studentDataGroup.TabStop = false;
            studentDataGroup.Text = "學生資料";
            studentDataGroup.Enter += groupBox1_Enter;
            // 
            // clearTextBoxAlways
            // 
            clearTextBoxAlways.AutoSize = true;
            clearTextBoxAlways.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            clearTextBoxAlways.Location = new Point(5, 143);
            clearTextBoxAlways.Name = "clearTextBoxAlways";
            clearTextBoxAlways.Size = new Size(153, 22);
            clearTextBoxAlways.TabIndex = 11;
            clearTextBoxAlways.Text = "加入列表後清除輸入";
            clearTextBoxAlways.UseVisualStyleBackColor = true;
            clearTextBoxAlways.CheckedChanged += clearTextBoxAlways_CheckedChanged;
            // 
            // pwdValue
            // 
            pwdValue.Location = new Point(75, 110);
            pwdValue.Margin = new Padding(2);
            pwdValue.Name = "pwdValue";
            pwdValue.Size = new Size(128, 28);
            pwdValue.TabIndex = 5;
            // 
            // studentName
            // 
            studentName.Location = new Point(75, 69);
            studentName.Margin = new Padding(2);
            studentName.Name = "studentName";
            studentName.Size = new Size(128, 28);
            studentName.TabIndex = 4;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            passwordLabel.Location = new Point(5, 113);
            passwordLabel.Margin = new Padding(2, 0, 2, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(78, 18);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "密　　碼：";
            // 
            // studentNameLabel
            // 
            studentNameLabel.AutoSize = true;
            studentNameLabel.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            studentNameLabel.Location = new Point(5, 73);
            studentNameLabel.Margin = new Padding(2, 0, 2, 0);
            studentNameLabel.Name = "studentNameLabel";
            studentNameLabel.Size = new Size(78, 18);
            studentNameLabel.TabIndex = 2;
            studentNameLabel.Text = "學生姓名：";
            // 
            // studentIdLabel
            // 
            studentIdLabel.AutoSize = true;
            studentIdLabel.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            studentIdLabel.Location = new Point(5, 36);
            studentIdLabel.Margin = new Padding(2, 0, 2, 0);
            studentIdLabel.Name = "studentIdLabel";
            studentIdLabel.Size = new Size(93, 18);
            studentIdLabel.TabIndex = 1;
            studentIdLabel.Text = "學號（ID）：";
            // 
            // studentsId
            // 
            studentsId.Location = new Point(102, 31);
            studentsId.Margin = new Padding(2);
            studentsId.Name = "studentsId";
            studentsId.Size = new Size(101, 28);
            studentsId.TabIndex = 0;
            studentsId.TextChanged += studentsId_TextChanged;
            // 
            // addValue
            // 
            addValue.Font = new Font("Microsoft JhengHei UI", 10F);
            addValue.Location = new Point(416, 225);
            addValue.Margin = new Padding(6);
            addValue.Name = "addValue";
            addValue.Size = new Size(83, 31);
            addValue.TabIndex = 3;
            addValue.Text = "新增資料";
            addValue.UseVisualStyleBackColor = true;
            addValue.Click += addValue_Click;
            // 
            // delValue
            // 
            delValue.Font = new Font("Microsoft JhengHei UI", 10F);
            delValue.Location = new Point(540, 225);
            delValue.Margin = new Padding(6);
            delValue.Name = "delValue";
            delValue.Size = new Size(83, 31);
            delValue.TabIndex = 7;
            delValue.Text = "刪除資料";
            delValue.UseVisualStyleBackColor = true;
            delValue.Click += delValue_Click;
            // 
            // modifyValue
            // 
            modifyValue.Font = new Font("Microsoft JhengHei UI", 10F);
            modifyValue.Location = new Point(416, 268);
            modifyValue.Margin = new Padding(6);
            modifyValue.Name = "modifyValue";
            modifyValue.Size = new Size(83, 31);
            modifyValue.TabIndex = 8;
            modifyValue.Text = "修改資料";
            modifyValue.UseVisualStyleBackColor = true;
            modifyValue.Click += modifyValue_Click;
            // 
            // clearList
            // 
            clearList.Font = new Font("Microsoft JhengHei UI", 10F);
            clearList.Location = new Point(540, 268);
            clearList.Margin = new Padding(6);
            clearList.Name = "clearList";
            clearList.Size = new Size(83, 31);
            clearList.TabIndex = 9;
            clearList.Text = "清空列表";
            clearList.UseVisualStyleBackColor = true;
            clearList.Click += clearList_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(64, 64, 64);
            statusStrip1.Items.AddRange(new ToolStripItem[] { autoSaveStatus, slice, loadDocPath });
            statusStrip1.Location = new Point(0, 368);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(634, 23);
            statusStrip1.TabIndex = 10;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.ItemClicked += statusStrip1_ItemClicked;
            // 
            // autoSaveStatus
            // 
            autoSaveStatus.BackColor = Color.FromArgb(64, 64, 64);
            autoSaveStatus.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            autoSaveStatus.ForeColor = Color.White;
            autoSaveStatus.Margin = new Padding(6, 3, 0, 2);
            autoSaveStatus.Name = "autoSaveStatus";
            autoSaveStatus.Size = new Size(106, 18);
            autoSaveStatus.Text = "自動儲存已開啟";
            autoSaveStatus.Click += autoSaveStatus_Click;
            // 
            // slice
            // 
            slice.Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Bold);
            slice.ForeColor = Color.White;
            slice.Name = "slice";
            slice.Size = new Size(10, 18);
            slice.Text = "·";
            // 
            // loadDocPath
            // 
            loadDocPath.BackColor = Color.FromArgb(64, 64, 64);
            loadDocPath.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            loadDocPath.ForeColor = Color.White;
            loadDocPath.Name = "loadDocPath";
            loadDocPath.Size = new Size(78, 18);
            loadDocPath.Text = "未開啟檔案";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(634, 391);
            Controls.Add(statusStrip1);
            Controls.Add(clearList);
            Controls.Add(modifyValue);
            Controls.Add(delValue);
            Controls.Add(addValue);
            Controls.Add(studentDataGroup);
            Controls.Add(datalistView);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MaximumSize = new Size(650, 430);
            MinimumSize = new Size(650, 430);
            Name = "Form1";
            Text = "學生資料管理系統";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            studentDataGroup.ResumeLayout(false);
            studentDataGroup.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStrip;
        private ToolStripMenuItem openNewFile;
        private ToolStripMenuItem saveAsNew;
        private ToolStripMenuItem loadDoc;
        private ToolStripMenuItem aboutToolStrip;
        private ToolStripMenuItem about;
        private ToolStripMenuItem autoSave;
        private ToolStripMenuItem exitProgram;
        private ListView datalistView;
        private ColumnHeader studentID;
        private ColumnHeader name;
        private ColumnHeader password;
        private ColumnHeader addTime;
        private GroupBox studentDataGroup;
        private Label passwordLabel;
        private Label studentNameLabel;
        private Label studentIdLabel;
        private TextBox studentsId;
        private TextBox pwdValue;
        private TextBox studentName;
        private Button addValue;
        private Button delValue;
        private Button modifyValue;
        private Button clearList;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel autoSaveStatus;
        private ToolStripStatusLabel loadDocPath;
        private CheckBox clearTextBoxAlways;
        private ToolStripMenuItem 儲存檔案ToolStripMenuItem;
        private ToolStripStatusLabel slice;
    }
}
