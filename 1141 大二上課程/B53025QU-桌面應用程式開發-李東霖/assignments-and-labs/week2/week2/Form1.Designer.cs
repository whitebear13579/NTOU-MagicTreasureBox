namespace week2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Font = new Font("Microsoft JhengHei UI", 11F);
            button1.Location = new Point(754, 417);
            button1.Name = "button1";
            button1.Size = new Size(105, 37);
            button1.TabIndex = 0;
            button1.Text = "確定";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_3;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Font = new Font("Microsoft JhengHei UI", 11F);
            button2.Location = new Point(643, 417);
            button2.Name = "button2";
            button2.Size = new Size(105, 37);
            button2.TabIndex = 1;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 136);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(578, 43);
            label1.TabIndex = 2;
            label1.Text = "請問你接受繼續被 Rick Roll 嗎? uwu";
            label1.Click += label1_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.WhiteSmoke;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(12, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(847, 310);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Microsoft JhengHei UI", 10F);
            checkBox1.Location = new Point(12, 387);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(83, 26);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "我同意";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Enabled = false;
            checkBox2.Font = new Font("Microsoft JhengHei UI", 10F);
            checkBox2.Location = new Point(101, 387);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(236, 26);
            checkBox2.TabIndex = 5;
            checkBox2.Text = "我不同意（哈哈你不能點）";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(871, 466);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            ForeColor = Color.Black;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Click += button1_Click;
            Paint += Form1_Paint;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private PictureBox pictureBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}
