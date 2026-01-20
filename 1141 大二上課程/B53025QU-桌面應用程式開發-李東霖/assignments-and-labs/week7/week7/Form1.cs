namespace week7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //安裝鍵盤掛鉤
            var k_hook = new KeyboardHook();
            k_hook.KeyDownEvent += new KeyEventHandler(hook_KeyDown);
            k_hook.Start();
        }

        private void hook_KeyDown(object sender, KeyEventArgs e)
        {
            //判斷是否按下alt + a
            if (e.KeyValue == (int)Keys.C && (int)Control.ModifierKeys == (int)Keys.Alt)
            //if (e.KeyValue == (int)Keys.A )
            {
                MessageBox.Show("按下了Alt + C");
            }
        }
    }
}
