using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week6
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color color = Color.FromArgb(255, 255, 255, 0); //透明質(透明0-255不透明),R,G,B
            System.Drawing.Pen myPen;
            SolidBrush myBrush = new SolidBrush(Color.Black);

            myPen = new System.Drawing.Pen(color, 5);//筆粗為5
            myPen.DashStyle = DashStyle.Dot; //設定點線
            g.DrawLine(myPen, 20, 10, 300, 100);

            myPen.DashStyle = DashStyle.Dash; //設定虛線
            g.DrawRectangle(myPen, 0, 0, 100, 100); //以左上座標(x,y)+高度+寬度畫一空心矩型 。
            g.FillRectangle(myBrush, new Rectangle(0, 0, 20, 30)); //畫一實心矩型

            myPen.DashStyle = DashStyle.DashDot; //設定點虛線
            g.DrawEllipse(myPen, 220, 220, 25, 35);
            g.FillEllipse(myBrush, new Rectangle(220, 220, 25, 35));

            myPen.StartCap = LineCap.ArrowAnchor; //起點Cap設為 ArrowAnchor 樣式
            myPen.StartCap = LineCap.Flat; //起點Cap設為 Flat 樣式
            myPen.EndCap = LineCap.DiamondAnchor; //起點Cap設為 DiamondAnchor 樣式
            myPen.EndCap = LineCap.Triangle; //起點Cap設為 ArrowAnchorTriangle 樣式

            myPen.DashStyle = DashStyle.Dot; //設定點線
            g.DrawLine(myPen, 50, 10, 36, 13);

            System.Drawing.Font myFont = new System.Drawing.Font("Arial", 16);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            g.DrawString("Sample Text", myFont, myBrush, x, y, drawFormat);

            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            g.DrawString("Sample Text", myFont, myBrush, x, y, drawFormat);
        }
    }
}
