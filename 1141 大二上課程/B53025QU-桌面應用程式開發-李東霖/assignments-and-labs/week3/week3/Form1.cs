using System.Diagnostics;

namespace week3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = @"https://www.microsoft.com",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(processStartInfo);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index;
            if (int.TryParse(textBox2.Text, out index) && index >= 0 && index < listBox1.Items.Count)
            {
                label1.Text = listBox1.Items[index].ToString();
            }
            else
            {
                label1.Text = "µL®ÄªºÀË¯Á";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
