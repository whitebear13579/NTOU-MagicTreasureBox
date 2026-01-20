namespace week5
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
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("root0 child1");
            TreeNode treeNode2 = new TreeNode("root0 child2");
            TreeNode treeNode3 = new TreeNode("root0 child3");
            TreeNode treeNode4 = new TreeNode("root0", new TreeNode[] { treeNode1, treeNode2, treeNode3 });
            TreeNode treeNode5 = new TreeNode("root1 child1");
            TreeNode treeNode6 = new TreeNode("root1 child2");
            TreeNode treeNode7 = new TreeNode("root1 child3 child1");
            TreeNode treeNode8 = new TreeNode("root1 child3 child2");
            TreeNode treeNode9 = new TreeNode("root1 child3", new TreeNode[] { treeNode7, treeNode8 });
            TreeNode treeNode10 = new TreeNode("root1 child4");
            TreeNode treeNode11 = new TreeNode("root1", new TreeNode[] { treeNode5, treeNode6, treeNode9, treeNode10 });
            TreeNode treeNode12 = new TreeNode("root2 child1");
            TreeNode treeNode13 = new TreeNode("root2 child2");
            TreeNode treeNode14 = new TreeNode("root2", new TreeNode[] { treeNode12, treeNode13 });
            numericUpDown1 = new NumericUpDown();
            timer1 = new System.Windows.Forms.Timer(components);
            button1 = new Button();
            treeView1 = new TreeView();
            toolTip1 = new ToolTip(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            radioButton5 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            tabPage2 = new TabPage();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button2 = new Button();
            textBox1 = new TextBox();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDown1.Location = new Point(12, 12);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // timer1
            // 
            timer1.Interval = 1;
            timer1.Tick += timer1_Tick;
            // 
            // button1
            // 
            button1.Location = new Point(38, 45);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // treeView1
            // 
            treeView1.CheckBoxes = true;
            treeView1.Location = new Point(12, 80);
            treeView1.Name = "treeView1";
            treeNode1.Name = "節點1";
            treeNode1.Text = "root0 child1";
            treeNode2.Name = "節點2";
            treeNode2.Text = "root0 child2";
            treeNode3.Name = "節點4";
            treeNode3.Text = "root0 child3";
            treeNode4.Name = "節點0";
            treeNode4.Text = "root0";
            treeNode5.Name = "節點5";
            treeNode5.Text = "root1 child1";
            treeNode6.Name = "節點6";
            treeNode6.Text = "root1 child2";
            treeNode7.Name = "節點8";
            treeNode7.Text = "root1 child3 child1";
            treeNode8.Name = "節點10";
            treeNode8.Text = "root1 child3 child2";
            treeNode9.Name = "節點7";
            treeNode9.Text = "root1 child3";
            treeNode10.Name = "節點9";
            treeNode10.Text = "root1 child4";
            treeNode11.Name = "節點3";
            treeNode11.Text = "root1";
            treeNode12.Name = "節點12";
            treeNode12.Text = "root2 child1";
            treeNode13.Name = "節點13";
            treeNode13.Text = "root2 child2";
            treeNode14.Name = "節點11";
            treeNode14.Text = "root2";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode4, treeNode11, treeNode14 });
            treeView1.Size = new Size(150, 224);
            treeView1.TabIndex = 2;
            // 
            // toolTip1
            // 
            toolTip1.Popup += toolTip1_Popup;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(168, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(250, 292);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(radioButton5);
            tabPage1.Controls.Add(radioButton4);
            tabPage1.Controls.Add(radioButton3);
            tabPage1.Controls.Add(radioButton2);
            tabPage1.Controls.Add(radioButton1);
            tabPage1.Location = new Point(4, 28);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(242, 260);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "頁面一";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(6, 122);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(50, 23);
            radioButton5.TabIndex = 4;
            radioButton5.TabStop = true;
            radioButton5.Text = "UP";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(6, 93);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(62, 23);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "YOU";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(6, 64);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(63, 23);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "GIVE";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(6, 35);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(87, 23);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "GONNA";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(6, 6);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(78, 23);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "NEVER";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBox5);
            tabPage2.Controls.Add(checkBox4);
            tabPage2.Controls.Add(checkBox3);
            tabPage2.Controls.Add(checkBox2);
            tabPage2.Controls.Add(checkBox1);
            tabPage2.Location = new Point(4, 28);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(242, 260);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "頁面二";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 122);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(79, 23);
            checkBox5.TabIndex = 4;
            checkBox5.Text = "NEVER";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(6, 93);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(88, 23);
            checkBox4.TabIndex = 3;
            checkBox4.Text = "GONNA";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(6, 64);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(55, 23);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "LET";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 35);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(63, 23);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "YOU";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 6);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(81, 23);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "DOWN";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Bold);
            button2.Location = new Point(424, 81);
            button2.Name = "button2";
            button2.Size = new Size(166, 58);
            button2.TabIndex = 4;
            button2.Text = "開新視窗";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(424, 40);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 27);
            textBox1.TabIndex = 5;
            textBox1.Text = "沒有任何值";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft JhengHei UI", 9F);
            button3.Location = new Point(424, 145);
            button3.Name = "button3";
            button3.Size = new Size(166, 27);
            button3.TabIndex = 6;
            button3.Text = "重設 Text Box";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(tabControl1);
            Controls.Add(treeView1);
            Controls.Add(button1);
            Controls.Add(numericUpDown1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numericUpDown1;
        private System.Windows.Forms.Timer timer1;
        private Button button1;
        private TreeView treeView1;
        private ToolTip toolTip1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TabPage tabPage2;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button2;
        private TextBox textBox1;
        private Button button3;
    }
}
