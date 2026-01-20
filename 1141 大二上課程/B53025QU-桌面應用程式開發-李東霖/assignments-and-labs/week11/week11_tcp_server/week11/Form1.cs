using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace week11
{
    public partial class Form1 : Form
    {
        static Socket socketListener;
        static bool isServerRunning = false;

        private static void ShowMsg(string s)
        {
            Debug.WriteLine(s);
        }

        public static void Server(int myPort, int allowNum)
        {
            //實作監聽用Socket，
            //AddressFamily.InterNetwork表示利用IP4協議
            //SocketType.Stream 因為我們要使用TCP協議，需使用流式的Socket
            //ProtocolType.Tcp 選用TCP協議
            socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //指定伺服器IP，這邊利用IPAddress.Any方法得到本機的IP，讓這方法可以運用更靈活
            IPAddress ip = IPAddress.Any;

            //設置應用程序的端口
            int port = myPort;

            //利用IPEndPoint這個類，將伺服器端的IP還有端口帶入給socketListener
            IPEndPoint point = new IPEndPoint(ip, port);

            //將point綁定給socketListener
            socketListener.Bind(point);
            ShowMsg("Listening..."); // 訊息顯示方式的函數(另建)

            //設定幾個程序可以連至本伺服器
            socketListener.Listen(allowNum);
        }

        public static void ReceiveClient(object socketObj)
        {
            Socket socketSender = socketObj as Socket;
            while (true)
            {
                try
                {
                    //數據傳送是由數組的方式傳送
                    //創立一個數組來儲存客戶端所回傳的訊息
                    byte[] buffer = new byte[1024];

                    //讀取字節數
                    int rece = socketSender.Receive(buffer);
                    //如果客戶端離開所得到的字節數會等於0，跳出此循環
                    if (rece == 0)
                    {
                        ShowMsg(string.Format("Client : {0} + 下線了", socketSender.RemoteEndPoint.ToString()));
                        break;
                    }

                    //載入System.Text的命名空間，利用GetString方法讀取字節，
                    //第一個引數代表要讀取的byte[]
                    //第二個引數代表從左邊數來第幾個字開始讀取
                    //每次讀取的字節數
                    string clientMsg = Encoding.UTF8.GetString(buffer, 0, rece);
                    ShowMsg(string.Format("Client : {0}", clientMsg));
                    
                    //回覆客戶端
                    string msg = "我收到你的訊息了\n";
                    byte[] sendBuffer = Encoding.UTF8.GetBytes(msg);
                    socketSender.Send(sendBuffer);
                    ShowMsg(("Client IP = " + socketSender.RemoteEndPoint.ToString()) + $" Send a message: {clientMsg}");
                }
                catch (SocketException)
                {
                    ShowMsg("Client Disconnect");
                    break;
                }
            }
        }

        public static void ServerSender( object listenerObj )
        {
            Socket listener = listenerObj as Socket;
            while (true)
            {
                //利用Accept方法接收監聽用Socket資料
                Socket socketSender = listener.Accept();
                //如果連線成功利用socketSender.RemoteEndPoint取得所連線到的Socket的IP和Port
                ShowMsg(("Client IP = " + socketSender.RemoteEndPoint.ToString()) + " Connect Succes!");

                Thread th1 = new Thread(ReceiveClient);
                th1.IsBackground = true;
                th1.Start(socketSender);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isServerRunning)
            {
                return;
            }

            try
            {
                Server(6000, 1);
                Thread th = new Thread(ServerSender);
                th.IsBackground = true;
                th.Start(socketListener);
                isServerRunning = true;
            }
            catch (SocketException ex)
            {
                ShowMsg($"server error:\n{ex.Message}");
            }
        }
    }
}
