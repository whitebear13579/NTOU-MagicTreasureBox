using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace week11_udp_server
{
    public partial class Form1 : Form
    {
        static Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static void sendMsg()
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6001);
            while (true)
            {
                string msg = "test";//自行調整
                server.SendTo(Encoding.UTF8.GetBytes(msg), point);

                Thread.Sleep(1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6000));//綁定端口號和IP
            Debug.WriteLine("服務端已經開啓"); //可自行調整秀至GUI元件上
            //Thread t1 = new Thread(ReciveMsg);//開啓接收消息綫程
            //t1.Start();
            Thread t2 = new Thread(sendMsg);//開啓發送消息綫程
            t2.Start();
        }
    }
}
