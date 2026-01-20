namespace week3_hw
{
    partial class mainWindows
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
            appMainMenu = new MenuStrip();
            file = new ToolStripMenuItem();
            openNewFile = new ToolStripMenuItem();
            saveToNewFile = new ToolStripMenuItem();
            loadDoc = new ToolStripMenuItem();
            saveDoc = new ToolStripMenuItem();
            autoSave = new ToolStripMenuItem();
            closeApp = new ToolStripMenuItem();
            edit = new ToolStripMenuItem();
            cut = new ToolStripMenuItem();
            copy = new ToolStripMenuItem();
            paste = new ToolStripMenuItem();
            delete = new ToolStripMenuItem();
            find = new ToolStripMenuItem();
            selectAll = new ToolStripMenuItem();
            format = new ToolStripMenuItem();
            fontSet = new ToolStripMenuItem();
            colorSet = new ToolStripMenuItem();
            sizeSet = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem12 = new ToolStripMenuItem();
            toolStripMenuItem13 = new ToolStripMenuItem();
            toolStripMenuItem14 = new ToolStripMenuItem();
            toolStripMenuItem15 = new ToolStripMenuItem();
            toolStripMenuItem16 = new ToolStripMenuItem();
            about = new ToolStripMenuItem();
            aboutMenu = new ToolStripMenuItem();
            label1 = new Label();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            appMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // appMainMenu
            // 
            appMainMenu.BackColor = Color.LightGray;
            appMainMenu.Font = new Font("Microsoft JhengHei UI", 10F);
            appMainMenu.Items.AddRange(new ToolStripItem[] { file, edit, format, about });
            appMainMenu.Location = new Point(0, 0);
            appMainMenu.Name = "appMainMenu";
            appMainMenu.Size = new Size(972, 26);
            appMainMenu.TabIndex = 1;
            appMainMenu.Text = "menuStrip2";
            appMainMenu.ItemClicked += 程式主選單_ItemClicked;
            // 
            // file
            // 
            file.BackColor = Color.LightGray;
            file.DropDownItems.AddRange(new ToolStripItem[] { openNewFile, saveToNewFile, loadDoc, saveDoc, autoSave, closeApp });
            file.ForeColor = Color.Black;
            file.ImageScaling = ToolStripItemImageScaling.None;
            file.Name = "file";
            file.Size = new Size(48, 22);
            file.Text = "檔案";
            file.Click += toolStripMenuItem1_Click;
            // 
            // openNewFile
            // 
            openNewFile.Name = "openNewFile";
            openNewFile.Size = new Size(132, 22);
            openNewFile.Text = "開新檔案";
            openNewFile.Click += openNewFile_Click;
            // 
            // saveToNewFile
            // 
            saveToNewFile.Name = "saveToNewFile";
            saveToNewFile.Size = new Size(132, 22);
            saveToNewFile.Text = "另存新檔";
            saveToNewFile.Click += saveToNewFile_Click;
            // 
            // loadDoc
            // 
            loadDoc.Name = "loadDoc";
            loadDoc.Size = new Size(132, 22);
            loadDoc.Text = "載入文件";
            loadDoc.Click += loadDoc_Click;
            // 
            // saveDoc
            // 
            saveDoc.Name = "saveDoc";
            saveDoc.Size = new Size(132, 22);
            saveDoc.Text = "儲存檔案";
            saveDoc.Click += toolStripMenuItem4_Click;
            // 
            // autoSave
            // 
            autoSave.Name = "autoSave";
            autoSave.Size = new Size(132, 22);
            autoSave.Text = "自動儲存";
            autoSave.Click += autoSave_Click;
            // 
            // closeApp
            // 
            closeApp.Name = "closeApp";
            closeApp.Size = new Size(132, 22);
            closeApp.Text = "結束程式";
            closeApp.Click += closeApp_Click;
            // 
            // edit
            // 
            edit.DropDownItems.AddRange(new ToolStripItem[] { cut, copy, paste, delete, find, selectAll });
            edit.Name = "edit";
            edit.Size = new Size(76, 22);
            edit.Text = "編輯文件";
            edit.Click += 編輯ToolStripMenuItem_Click;
            // 
            // cut
            // 
            cut.Name = "cut";
            cut.Size = new Size(104, 22);
            cut.Text = "剪下";
            cut.Click += cut_Click;
            // 
            // copy
            // 
            copy.Name = "copy";
            copy.Size = new Size(104, 22);
            copy.Text = "複製";
            copy.Click += copy_Click;
            // 
            // paste
            // 
            paste.Name = "paste";
            paste.Size = new Size(104, 22);
            paste.Text = "貼上";
            paste.Click += paste_Click;
            // 
            // delete
            // 
            delete.Name = "delete";
            delete.Size = new Size(104, 22);
            delete.Text = "刪除";
            delete.Click += delete_Click;
            // 
            // find
            // 
            find.Name = "find";
            find.Size = new Size(104, 22);
            find.Text = "尋找";
            find.Click += find_Click;
            // 
            // selectAll
            // 
            selectAll.Name = "selectAll";
            selectAll.Size = new Size(104, 22);
            selectAll.Text = "全選";
            selectAll.Click += selectAll_Click;
            // 
            // format
            // 
            format.DropDownItems.AddRange(new ToolStripItem[] { fontSet, colorSet, sizeSet });
            format.Name = "format";
            format.Size = new Size(76, 22);
            format.Text = "格式設定";
            // 
            // fontSet
            // 
            fontSet.Name = "fontSet";
            fontSet.Size = new Size(180, 22);
            fontSet.Text = "字型設定";
            fontSet.Click += fontSet_Click;
            // 
            // colorSet
            // 
            colorSet.Name = "colorSet";
            colorSet.Size = new Size(180, 22);
            colorSet.Text = "色彩設定";
            colorSet.Click += colorSet_Click;
            // 
            // sizeSet
            // 
            sizeSet.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9, toolStripMenuItem10, toolStripMenuItem11, toolStripMenuItem12, toolStripMenuItem13, toolStripMenuItem14, toolStripMenuItem15, toolStripMenuItem16 });
            sizeSet.Name = "sizeSet";
            sizeSet.Size = new Size(180, 22);
            sizeSet.Text = "字體大小";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(180, 22);
            toolStripMenuItem7.Text = "12";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(180, 22);
            toolStripMenuItem8.Text = "16";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(180, 22);
            toolStripMenuItem9.Text = "20";
            toolStripMenuItem9.Click += toolStripMenuItem9_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(180, 22);
            toolStripMenuItem10.Text = "24";
            toolStripMenuItem10.Click += toolStripMenuItem10_Click;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(180, 22);
            toolStripMenuItem11.Text = "28";
            toolStripMenuItem11.Click += toolStripMenuItem11_Click;
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new Size(180, 22);
            toolStripMenuItem12.Text = "32";
            toolStripMenuItem12.Click += toolStripMenuItem12_Click;
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new Size(180, 22);
            toolStripMenuItem13.Text = "36";
            toolStripMenuItem13.Click += toolStripMenuItem13_Click;
            // 
            // toolStripMenuItem14
            // 
            toolStripMenuItem14.Name = "toolStripMenuItem14";
            toolStripMenuItem14.Size = new Size(180, 22);
            toolStripMenuItem14.Text = "40";
            toolStripMenuItem14.Click += toolStripMenuItem14_Click;
            // 
            // toolStripMenuItem15
            // 
            toolStripMenuItem15.Name = "toolStripMenuItem15";
            toolStripMenuItem15.Size = new Size(180, 22);
            toolStripMenuItem15.Text = "44";
            toolStripMenuItem15.Click += toolStripMenuItem15_Click;
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new Size(180, 22);
            toolStripMenuItem16.Text = "48";
            toolStripMenuItem16.Click += toolStripMenuItem16_Click;
            // 
            // about
            // 
            about.DropDownItems.AddRange(new ToolStripItem[] { aboutMenu });
            about.Name = "about";
            about.Size = new Size(76, 22);
            about.Text = "關於程式";
            // 
            // aboutMenu
            // 
            aboutMenu.Name = "aboutMenu";
            aboutMenu.Size = new Size(174, 22);
            aboutMenu.Text = "Ciallo (∠·ω )⌒★";
            aboutMenu.Click += aboutMenu_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(854, 508);
            label1.Name = "label1";
            label1.Size = new Size(106, 18);
            label1.TabIndex = 2;
            label1.Text = "自動儲存已開啟";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click_1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(12, 508);
            label3.Name = "label3";
            label3.Size = new Size(78, 18);
            label3.TabIndex = 4;
            label3.Text = "未開啟檔案";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += label3_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.FromArgb(66, 66, 66);
            richTextBox1.Location = new Point(12, 39);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(948, 466);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // mainWindows
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            ClientSize = new Size(972, 535);
            Controls.Add(richTextBox1);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(appMainMenu);
            Name = "mainWindows";
            Text = "文字編輯器";
            appMainMenu.ResumeLayout(false);
            appMainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip appMainMenu;
        private ToolStripMenuItem file;
        private ToolStripMenuItem openNewFile;
        private ToolStripMenuItem loadDoc;
        private ToolStripMenuItem saveDoc;
        private ToolStripMenuItem autoSave;
        private ToolStripMenuItem edit;
        private ToolStripMenuItem format;
        private ToolStripMenuItem about;
        private ToolStripMenuItem closeApp;
        private ToolStripMenuItem cut;
        private ToolStripMenuItem copy;
        private ToolStripMenuItem paste;
        private ToolStripMenuItem delete;
        private ToolStripMenuItem find;
        private ToolStripMenuItem selectAll;
        private ToolStripMenuItem fontSet;
        private ToolStripMenuItem colorSet;
        private ToolStripMenuItem sizeSet;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripMenuItem toolStripMenuItem14;
        private ToolStripMenuItem toolStripMenuItem15;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripMenuItem aboutMenu;
        private Label label1;
        private Label label3;
        private RichTextBox richTextBox1;
        private ToolStripMenuItem saveToNewFile;
    }
}
