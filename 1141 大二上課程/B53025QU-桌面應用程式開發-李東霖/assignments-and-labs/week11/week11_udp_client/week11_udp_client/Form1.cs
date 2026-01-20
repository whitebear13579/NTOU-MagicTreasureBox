using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace week11_udp_client
{
    public partial class Form1 : Form
    {
        static Socket client;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static void ReciveMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用來保存發送方的ip和端口號
                byte[] buffer = new byte[1024];
                int length = client.ReceiveFrom(buffer, ref point);//接收數據報
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                Debug.WriteLine(point.ToString() + message); //可自行調整秀至GUI元件上
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6001));//綁定端口號和IP
            Debug.WriteLine("服務端已經開啓"); //可自行調整秀至GUI元件上
            Thread t1 = new Thread(ReciveMsg);//開啓接收消息綫程
            t1.Start();
            //Thread t2 = new Thread(sendMsg);//開啓發送消息綫程
            //t2.Start();

        }
    }
}
