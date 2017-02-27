using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public interface ClientListener
    {
        void OnDisconnected();
    }

    public class ClientManager
    {
        private const int MAX_BUF_SIZE = 1024;
        private Socket clientSocket;
        private Thread sendThread;

        private Thread receiveThread;
        private ClientListener clientListener;
        private bool isSend;
        private string sendToServerMessage;

        private static object guard = new object();
        private static ClientManager instance;

        public static ClientManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (guard)
                    {
                        if (instance == null)
                            instance = new ClientManager();
                    }
                }
                return instance;
            }
        }

        private ClientManager()
        {
        }

        public void SetClientListener(ClientListener listener)
        {
            clientListener = listener;
        }

        public bool ConnectToServer(string ipAddress, int port)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ip, port));
            }
            catch (Exception ex)
            {
                return false;
            }

            LaunchSendRecvThread();
            return true;
        }

        public void Disconnect()
        {
            if (clientSocket != null)
            {
                try
                {
                    clientSocket.Close();
                }
                catch (Exception ex)
                {
                }
                clientSocket = null;
            }
        }

        private void LaunchSendRecvThread()
        {
            sendThread = new Thread(SendThread) { Name = "SendThread", IsBackground = true };
            sendThread.Start();

            receiveThread = new Thread(ReceiveThread) { Name = "ReceiveThread", IsBackground = true };
            receiveThread.Start();
        }

        private void SendThread()
        {
            byte[] recvBuffer = new byte[MAX_BUF_SIZE];
            while (true)
            {
                if (isSend)
                {
                    SendToServer();
                }
                if (IsDisconnected())
                {
                    Disconnected();
                    break;
                }
            }
        }

        private void ReceiveThread()
        {
            byte[] recvBuffer = new byte[MAX_BUF_SIZE];
            while (true)
            {
                try
                {
                    int recvLength = clientSocket.Receive(recvBuffer);
                    string recvData = Encoding.Default.GetString(recvBuffer, 0, recvLength);
                    MessageBox.Show(recvData);
                }
                catch (Exception ex)
                {
                }
                if (IsDisconnected())
                {
                    Disconnected();
                    break;
                }
            }
            MessageBox.Show("Server disconnected.");
        }

        private void Disconnected()
        {
            Disconnect();
            if (clientListener != null)
                clientListener.OnDisconnected();
        }

        private bool IsDisconnected()
        {
            try
            {
                bool isDisconnected = clientSocket.Poll(10, SelectMode.SelectRead);
                return isDisconnected;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private void SendToServer()
        {
            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(sendToServerMessage));
            }
            catch (Exception ex)
            {
            }
            isSend = false;
        }

        public void SendToServer(string message)
        {
            if (message.Length > 0)
            {
                isSend = true;
                sendToServerMessage = message;
            }
        }
    }
}


//namespace SocketClient
//{
//    class Program
//    {
//        private static byte[] result = new byte[1024];
//        static void Main(string[] args)
//        {
//            //设定服务器IP地址
//            IPAddress ip = IPAddress.Parse("127.0.0.1");
//            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            try
//            {
//                clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口
//            }
//            catch
//            {
//                Console.WriteLine("连接服务器失败，请按回车键退出！");
//                return;
//            }
//            //通过clientSocket接收数据
//            int receiveLength = clientSocket.Receive(result);
//            Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
//            //通过 clientSocket 发送数据
//            for (int i = 0; i < 10; i++)
//            {
//                try
//                {
//                    Thread.Sleep(1000);    //等待1秒钟
//                    string sendMessage = "client send Message Hellp" + DateTime.Now;
//                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
//                    Console.WriteLine("向服务器发送消息：{0}" + sendMessage);
//                }
//                catch
//                {
//                    clientSocket.Shutdown(SocketShutdown.Both);
//                    clientSocket.Close();
//                    break;
//                }
//            }
//            Console.WriteLine("发送完毕，按回车键退出");
//            Console.ReadLine();
//        }
//    }
//}

