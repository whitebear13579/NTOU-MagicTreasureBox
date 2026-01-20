using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Button[] b = new Button[70];
            for (int i = 0; i < b.Length; ++i)
            {
                b[i] = new Button();
                b[i].Text = "按鈕" + i.ToString();
                b[i].Size = new System.Drawing.Size(100, 30);
                b[i].Click += new EventHandler(button_Click);
                flowLayoutPanel1.Controls.Add(b[i]);
            }
            this.Controls.Add(flowLayoutPanel1);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // 建立 FlowLayoutPanel 控制項
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill; // 佔滿整個視窗
        }

        private void button_Click(object sender, EventArgs e)
        {
            // 在這裡可以寫點擊按鈕後的處理邏輯
            MessageBox.Show($"你按下了按鈕：{((Button)sender).Text}");
        }
    }
}
