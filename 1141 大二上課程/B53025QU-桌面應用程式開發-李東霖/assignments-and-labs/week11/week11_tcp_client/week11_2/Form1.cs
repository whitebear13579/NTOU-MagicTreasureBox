using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace week11_2
{
    public partial class Form1 : Form
    {
        private Socket socket;
        private Thread sendMsg;
        private Thread receiveMsg;
        private volatile bool isRunning = false;

        private void ShowMsg(string s)
        {
            Debug.WriteLine(s);
        }
        private void SendMsgToServer()
        {
            while (isRunning)
            {
                try
                {
                    string inputText = "";
                    if (textBox1.InvokeRequired)
                    {
                        textBox1.Invoke(new Action(() => inputText = textBox1.Text));
                    }
                    else
                    {
                        inputText = textBox1.Text;
                    }
                    
                    if (!string.IsNullOrEmpty(inputText))
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(inputText);
                        socket.Send(buffer);
                    }
                    Thread.Sleep(1500); // Avoid tight loop
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
        private void ReceiveMesgFromServer()
        {
            while (isRunning)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int rec = socket.Receive(buffer);
                    if (rec == 0)
                    {
                        ShowMsg("Server Loss!");
                        break;
                    }
                    string receText = System.Text.Encoding.UTF8.GetString(buffer, 0, rec);
                    ShowMsg("Server :" + receText);
                }
                catch (Exception)
                {
                    break;
                }
            }

        }
        private void ClientSocket(string ip, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress myIp = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(myIp, port);
            socket.Connect(point);
            ShowMsg("Connect Succese! " + socket.RemoteEndPoint.ToString());
            
            isRunning = true;
            sendMsg = new Thread(SendMsgToServer);
            receiveMsg = new Thread(ReceiveMesgFromServer);
            receiveMsg.IsBackground = true;
            sendMsg.IsBackground = true;
            receiveMsg.Start();
            sendMsg.Start();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = "127.0.0.1"; //可自定義
            int port = 6000;//可自定義
            ClientSocket(ip, port);
        }
    }
}
