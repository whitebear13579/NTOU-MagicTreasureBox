namespace week4
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
            // 移除預設的 ListViewItem 因為我們要用程式碼動態加入
            richTextBox1 = new RichTextBox();
            TryIt = new Button();
            textBox1 = new TextBox();
            checkedListBox1 = new CheckedListBox();
            listBox1 = new ListBox();
            progressBar1 = new ProgressBar();
            monthCalendar1 = new MonthCalendar();
            label1 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            listView1 = new ListView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(854, 181);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // TryIt
            // 
            TryIt.ForeColor = SystemColors.WindowText;
            TryIt.Location = new Point(772, 199);
            TryIt.Name = "TryIt";
            TryIt.Size = new Size(94, 29);
            TryIt.TabIndex = 1;
            TryIt.Text = "Try it !";
            TryIt.UseVisualStyleBackColor = true;
            TryIt.Click += TryIt_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(553, 199);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(213, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // checkedListBox1
            // 
            checkedListBox1.CheckOnClick = true;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Nerver", "Gonna", "Give", "You", "Up" });
            checkedListBox1.Location = new Point(6, 21);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(105, 312);
            checkedListBox1.TabIndex = 3;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 19;
            listBox1.Location = new Point(138, 21);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(105, 308);
            listBox1.TabIndex = 4;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(553, 232);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(313, 68);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 5;
            progressBar1.Click += progressBar1_Click;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(11, 21);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 6;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F);
            label1.ForeColor = SystemColors.WindowText;
            label1.Location = new Point(11, 228);
            label1.Name = "label1";
            label1.Size = new Size(112, 25);
            label1.TabIndex = 7;
            label1.Text = "請選擇日期";
            label1.Click += label1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(11, 267);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(262, 27);
            dateTimePicker1.TabIndex = 8;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 12F);
            label3.ForeColor = SystemColors.WindowText;
            label3.Location = new Point(11, 297);
            label3.Name = "label3";
            label3.Size = new Size(132, 25);
            label3.TabIndex = 10;
            label3.Text = "選一下日期咩";
            label3.Click += label3_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(checkedListBox1);
            groupBox1.Location = new Point(12, 211);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(250, 339);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "列表勾選與顯示";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(monthCalendar1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Location = new Point(268, 211);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(279, 339);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "日期選擇器";
            // 
            // listView1
            // 
            listView1.GridLines = true;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("日期", 100);
            listView1.Columns.Add("項目", 100);
            listView1.Columns.Add("輸入", 100);
            listView1.Location = new Point(553, 309);
            listView1.Name = "listView1";
            listView1.Size = new Size(313, 241);
            listView1.TabIndex = 13;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 562);
            Controls.Add(listView1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            Controls.Add(TryIt);
            Controls.Add(richTextBox1);
            ForeColor = SystemColors.Highlight;
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button TryIt;
        private TextBox textBox1;
        private CheckedListBox checkedListBox1;
        private ListBox listBox1;
        private ProgressBar progressBar1;
        private MonthCalendar monthCalendar1;
        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListView listView1;
    }
}
