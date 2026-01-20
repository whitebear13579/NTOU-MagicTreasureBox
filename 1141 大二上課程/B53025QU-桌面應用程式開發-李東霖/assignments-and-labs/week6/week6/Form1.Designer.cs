namespace week6
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
            panel1 = new Panel();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            resetImg = new Button();
            panel3 = new Panel();
            label1 = new Label();
            button7 = new Button();
            button8 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(248, 121);
            panel1.TabIndex = 1;
            // 
            // button6
            // 
            button6.Location = new Point(248, 65);
            button6.Margin = new Padding(12);
            button6.Name = "button6";
            button6.Size = new Size(94, 29);
            button6.TabIndex = 7;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(130, 65);
            button5.Margin = new Padding(12);
            button5.Name = "button5";
            button5.Size = new Size(94, 29);
            button5.TabIndex = 6;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 65);
            button4.Margin = new Padding(12);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 5;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(248, 12);
            button3.Margin = new Padding(12);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 4;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(130, 9);
            button2.Margin = new Padding(12);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Margin = new Padding(12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.BackgroundImageLayout = ImageLayout.None;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(266, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(522, 426);
            panel2.TabIndex = 2;
            panel2.Paint += panel2_Paint;
            panel2.MouseClick += panel2_MouseClick;
            panel2.MouseDown += panel2_MouseDown;
            panel2.MouseMove += panel2_MouseMove;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 20F);
            label3.Location = new Point(19, 12);
            label3.Name = "label3";
            label3.Size = new Size(107, 43);
            label3.TabIndex = 1;
            label3.Text = "圖層2";
            label3.Click += label3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(202, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(115, 367);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // resetImg
            // 
            resetImg.Location = new Point(166, 409);
            resetImg.Name = "resetImg";
            resetImg.Size = new Size(94, 29);
            resetImg.TabIndex = 3;
            resetImg.Text = "reset";
            resetImg.UseVisualStyleBackColor = true;
            resetImg.Click += resetImg_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.IndianRed;
            panel3.Controls.Add(label1);
            panel3.Location = new Point(166, 161);
            panel3.Name = "panel3";
            panel3.Size = new Size(250, 203);
            panel3.TabIndex = 4;
            panel3.Paint += panel3_Paint;
            panel3.MouseClick += panel3_MouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 20F);
            label1.Location = new Point(19, 14);
            label1.Name = "label1";
            label1.Size = new Size(107, 43);
            label1.TabIndex = 0;
            label1.Text = "圖層1";
            label1.Click += label1_Click;
            // 
            // button7
            // 
            button7.Location = new Point(12, 409);
            button7.Name = "button7";
            button7.Size = new Size(148, 29);
            button7.TabIndex = 5;
            button7.Text = "open paint";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(12, 359);
            button8.Name = "button8";
            button8.Size = new Size(148, 29);
            button8.TabIndex = 6;
            button8.Text = "open flowLayout";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(panel3);
            Controls.Add(resetImg);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "week6";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button2;
        private Button button1;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Button resetImg;
        private Panel panel3;
        private Label label1;
        private Label label3;
        private Button button7;
        private Button button8;
    }
}
