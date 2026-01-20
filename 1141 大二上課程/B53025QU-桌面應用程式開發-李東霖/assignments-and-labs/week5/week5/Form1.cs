namespace week5
{
    public partial class Form1 : Form
    {
        bool isRunning = false;
        public Form1()
        {
            InitializeComponent();
            toolTip1.SetToolTip(button1, "開始計時器");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (numericUpDown1.Value >= numericUpDown1.Maximum)
            {
                numericUpDown1.Value = 0;
            }
            else
            {
                numericUpDown1.Value += (decimal)0.01;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                button1.Text = "Stop";
                MessageBox.Show("Timer will be start.", "week5 Program", MessageBoxButtons.OK, MessageBoxIcon.Information);
                timer1.Enabled = true;
                return;
            }
            else
            {
                isRunning = false;
                button1.Text = "Start";
                timer1.Enabled = false;
                return;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 uwu = new Form2();
            uwu.ShowDialog(this);
            if (uwu.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = "按下了" + uwu.DialogResult.ToString();
            }
            else if (uwu.DialogResult == System.Windows.Forms.DialogResult.Cancel)
            {
                textBox1.Text = "按下了" + uwu.DialogResult.ToString();
            }
            else
            {
                textBox1.Text = "按下了" + uwu.DialogResult.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "沒有任何值";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
