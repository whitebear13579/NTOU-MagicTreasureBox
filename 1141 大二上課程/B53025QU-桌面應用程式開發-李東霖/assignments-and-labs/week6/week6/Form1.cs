namespace week6
{
    public partial class Form1 : Form
    {
        private Point mouse_offset;

        public Form1()
        {
            InitializeComponent();

            // 調整圖片大小 - 讓整個 PictureBox 控件變小
            pictureBox1.Size = new Size(120, 350); // 寬120，高350

            // 設置為 Zoom 模式：保持圖片比例，完整顯示圖片內容
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // 調整初始位置，讓圖片在 panel2 中央
            pictureBox1.Location = new Point(
                (panel2.Width - pictureBox1.Width) / 2,
                (panel2.Height - pictureBox1.Height) / 2
            );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 正確顯示各個面板的圖層索引
            UpdateAllLabels();
        }

        // 新增方法：更新所有標籤顯示
        private void UpdateAllLabels()
        {
            label1.Text = this.Controls.GetChildIndex(panel3).ToString(); // panel3 的索引顯示在 label1
            label3.Text = this.Controls.GetChildIndex(panel2).ToString(); // panel2 的索引顯示在 label3

            // 顯示 panel2 內部控件的層級信息（除錯用）
            System.Diagnostics.Debug.WriteLine($"Panel2 內部控件層級:");
            for (int i = 0; i < panel2.Controls.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine($"  索引 {i}: {panel2.Controls[i].Name}");
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            int currentIndex = this.Controls.GetChildIndex(panel2);

            if (e.Button == MouseButtons.Left)
            {
                // 左鍵：往前移動（索引減少，圖層提升）
                if (currentIndex > 0)
                    this.Controls.SetChildIndex(panel2, currentIndex - 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 右鍵：往後移動（索引增加，圖層下降）
                if (currentIndex < this.Controls.Count - 1)
                    this.Controls.SetChildIndex(panel2, currentIndex + 1);
            }

            // 更新所有標籤，因為 panel2 移動會影響整體層級
            UpdateAllLabels();
        }

        private void panel3_MouseClick(object sender, MouseEventArgs e)
        {
            int currentIndex = this.Controls.GetChildIndex(panel3);

            if (e.Button == MouseButtons.Left)
            {
                // 左鍵：往前移動（索引減少，圖層提升）
                if (currentIndex > 0)
                    this.Controls.SetChildIndex(panel3, currentIndex - 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 右鍵：往後移動（索引增加，圖層下降）
                if (currentIndex < this.Controls.Count - 1)
                    this.Controls.SetChildIndex(panel3, currentIndex + 1);
            }

            // 更新所有標籤
            UpdateAllLabels();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
        }

        // pictureBox1 的滑鼠按下事件
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // 記錄滑鼠在圖片內部的相對位置
            mouse_offset = new Point(-e.X, -e.Y);
        }

        // pictureBox1 的滑鼠移動事件
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pic = (PictureBox)sender;

                // 取得滑鼠在螢幕上的位置
                Point mousePos = Control.MousePosition;

                // 轉換為相對於 Form 的座標
                mousePos = this.PointToClient(mousePos);

                // 套用偏移量，讓圖片跟隨滑鼠移動
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);

                // 轉換為相對於 panel2 的座標
                mousePos = panel2.PointToClient(this.PointToScreen(mousePos));

                // 設定圖片新位置
                pic.Location = mousePos;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void resetImg_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(
                (panel2.Width - pictureBox1.Width) / 2,
                (panel2.Height - pictureBox1.Height) / 2
            );
            MessageBox.Show("圖片已重置到初始位置", "Week 6", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // 創建並顯示 Form2
            Form2 form2 = new Form2();
            form2.Show(); // 使用 Show() 讓 Form2 作為非模態對話框顯示
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}
