using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;//設定button1為OK
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;//設定button為Cancel
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
