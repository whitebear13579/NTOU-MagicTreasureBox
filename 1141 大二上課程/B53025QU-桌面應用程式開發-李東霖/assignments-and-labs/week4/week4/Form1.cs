using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace week4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int counter = 0;
        int ptr = 0;
        Stopwatch sw = new Stopwatch();
        Font font1 = new Font("arial black", 16, FontStyle.Regular);
        Font font2 = new Font("arial", 12, FontStyle.Bold);
        private void TryIt_Click(object sender, EventArgs e)
        {
            if (counter % 4 == 0) richTextBox1.SelectionColor = Color.Black;
            if (counter % 4 == 1) richTextBox1.SelectionColor = Color.Red;
            if (counter % 4 == 2) richTextBox1.SelectionFont = font1;
            if (counter % 4 == 3) richTextBox1.SelectionFont = font2;
            string last_elapseTime_ms = sw.ElapsedMilliseconds.ToString();
            sw.Restart();
            string timeStamp = DateTime.Now.ToString("HH:mm:ss.f ");
            string text = timeStamp + textBox1.Text + " " + counter
            + " T=" + last_elapseTime_ms + "ms\n";
            //string text_history = richTextBox1.Text;
            //text += text_history;
            //richTextBox1.ResetText();
            if (counter % 4 == 0) richTextBox1.SelectionColor = Color.Black;
            if (counter % 4 == 1) richTextBox1.SelectionColor = Color.Red;
            if (counter % 4 == 2) richTextBox1.SelectionFont = font1;
            if (counter % 4 == 3) richTextBox1.SelectionFont = font2;
            richTextBox1.AppendText(text);
            richTextBox1.ScrollToCaret();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.GetItemChecked(i))
                    listBox1.Items.Add(checkedListBox1.Items[i].ToString());

            listBox1.TopIndex = listBox1.Items.Count - 1;

            counter++;
            progressBar1.Value = counter;

            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("你按了那傻逼按鈕" + progressBar1.Maximum.ToString() + "次");
                counter = 0;
            }

            ListViewItem lvi = new ListViewItem();
            lvi.Text = monthCalendar1.SelectionStart.ToShortDateString();

            if (listBox1.Items.Count > 0)
            {
                lvi.SubItems.Add(listBox1.Items[listBox1.Items.Count - 1].ToString());
            }
            else
            {
                lvi.SubItems.Add("沒選");
            }

            if ( textBox1.Text == "")
                lvi.SubItems.Add("-");
            else
                lvi.SubItems.Add(textBox1.Text);

            listView1.Items.Add(lvi);
            listView1.EnsureVisible(listView1.Items.Count - 1);

            sw.Stop();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            label1.Text = $"你選的日期：{monthCalendar1.SelectionStart.ToShortDateString()}";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = $"你選了：{dateTimePicker1.Value.ToLongTimeString()}";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
