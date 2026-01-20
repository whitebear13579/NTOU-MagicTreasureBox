namespace week13
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
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            button1 = new Button();
            hScrollBar1 = new HScrollBar();
            button2 = new Button();
            webcam = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1008, 578);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            button1.Location = new Point(926, 603);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(12, 603);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(428, 26);
            hScrollBar1.TabIndex = 2;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(826, 603);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "video";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // webcam
            // 
            webcam.Location = new Point(726, 603);
            webcam.Name = "webcam";
            webcam.Size = new Size(94, 29);
            webcam.TabIndex = 4;
            webcam.Text = "webcam";
            webcam.UseVisualStyleBackColor = true;
            webcam.Click += webcam_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 644);
            Controls.Add(webcam);
            Controls.Add(button2);
            Controls.Add(hScrollBar1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private HScrollBar hScrollBar1;
        private Button button2;
        private Button webcam;
    }
}
