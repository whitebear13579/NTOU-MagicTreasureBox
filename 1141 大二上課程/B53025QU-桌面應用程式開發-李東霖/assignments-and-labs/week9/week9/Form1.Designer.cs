namespace week9
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
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            button4 = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(805, 406);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(12, 435);
            button1.Name = "button1";
            button1.Size = new Size(116, 29);
            button1.TabIndex = 1;
            button1.Text = "加入 Person";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(134, 435);
            button2.Name = "button2";
            button2.Size = new Size(132, 29);
            button2.TabIndex = 2;
            button2.Text = "加入 Employee";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(272, 435);
            button3.Name = "button3";
            button3.Size = new Size(114, 29);
            button3.TabIndex = 3;
            button3.Text = "加入 Pratice";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(646, 435);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(171, 27);
            textBox1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 11F, FontStyle.Bold);
            label1.Location = new Point(550, 438);
            label1.Name = "label1";
            label1.Size = new Size(91, 24);
            label1.TabIndex = 5;
            label1.Text = "股票代碼:";
            // 
            // button4
            // 
            button4.Location = new Point(392, 435);
            button4.Name = "button4";
            button4.Size = new Size(111, 29);
            button4.TabIndex = 6;
            button4.Text = "清空文字欄";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(829, 476);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private TextBox textBox1;
        private Label label1;
        private Button button4;
    }
}
