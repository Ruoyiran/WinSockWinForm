using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    public interface ServerListener
    {
        void OnClientJoined(IntPtr handle, string ipAddress);
    }

    public class ServerManager
    {
        private const int BACK_LOG = 20;
        private const int MAX_BUF_SIZE = 1024;

        private ServerListener serverListener;
        private Socket serverSocket;
        private IPEndPoint ipEndPoint;
        private Thread acceptThread;
        private Thread sendThread;
        private List<Socket> connectedClients;
        private bool runAcceptThread;
        private bool runSendThread;
        private static object guard = new object();
        private static ServerManager instance;
        public static ServerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (guard)
                    {
                        if (instance == null)
                            instance = new ServerManager();
                    }
                }
                return instance;
            }
        }

        public bool IsRunning
        {
            get
            {
                return runAcceptThread;
            }
        }

        private ServerManager()
        {
            connectedClients = new List<Socket>();
        }

        public void SetServerListener(ServerListener listener)
        {
            serverListener = listener;
        }

        public bool StartTCPServer(int port)
        {
            if (runAcceptThread) return false;
            
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            try
            {
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(BACK_LOG);
            }
            catch (Exception ex)
            {
                return false;
            }

            LuanchAcceptThread();
            return true;
        }

        public void StopTCPServer()
        {
            CloseSocket();
            RemoveAllClients();
            runAcceptThread = false;
        }

        private void CloseSocket()
        {
            serverSocket.Close();
            serverSocket.Dispose();
            serverSocket = null;
        }

        private void LuanchAcceptThread()
        {
            runAcceptThread = true;
            acceptThread = new Thread(AcceptThread) { Name = "AcceptThread", IsBackground = true };
            acceptThread.Start();
        }

        private void AcceptThread()
        {
            while (runAcceptThread)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();
                    ClientJoined(clientSocket);
                }
                catch (Exception ex)
                {
                }
            }
            acceptThread = null;
        }

        private void ClientJoined(Socket clientSocket)
        {
            connectedClients.Add(clientSocket);
            CreateReceiveThread(clientSocket);
            if (serverListener != null)
            {
                IPEndPoint ipEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
                serverListener.OnClientJoined(clientSocket.Handle, ipEndPoint.Address.ToString());
            }
        }

        private void CreateReceiveThread(Socket clientSocket)
        {
            Thread receiveThread = new Thread(new ParameterizedThreadStart(ReceiveThread)) { IsBackground = true };
            receiveThread.Start(clientSocket);
        }

        private void ReceiveThread(object obj)
        {
            Socket clientSocket = obj as Socket;
            EndPoint remotePoint = ipEndPoint as EndPoint;
            byte[] recvBuffer = new byte[MAX_BUF_SIZE];
            while (ClientIsAlive(clientSocket.Handle))
            {
                ClearBuffer(recvBuffer);
                try
                {
                    clientSocket.ReceiveFrom(recvBuffer, ref remotePoint);
                    string recvData = Encoding.Default.GetString(recvBuffer);
                    MessageBox.Show(recvData);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private bool ClientIsAlive(IntPtr handle)
        {
            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                if (connectedClients[i].Handle == handle)
                    return true;
            }
            return false;
        }

        private void ClearBuffer(byte[] buffer)
        {
            if (buffer == null)
                return;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 0;
            }
        }

        private void RemoveClient(IntPtr handle)
        {
            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                if (connectedClients[i].Handle == handle)
                {
                    connectedClients[i].Close();
                    connectedClients.RemoveAt(i);
                    break;
                }
            }
        }

        private void RemoveAllClients()
        {
            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                connectedClients[i].Close();
            }
            connectedClients.Clear();
        }

        //static void Main(string[] args)
        //{
        //    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 6000);
        //    socket.Bind(localEP);
        //    socket.Listen(20);
        //    Console.WriteLine("\t\t***************服务器端***************\n");
        //    Console.WriteLine("\t\t\t服务器端IP: 192.168.1.103");
        //    Console.WriteLine("\t\t\t服务器端端口号: 6000");
        //    EndPoint remotePoint = localEP;
        //    Console.WriteLine("\t\t**************************************\n");
        //    Console.WriteLine("服务器已启动!\n");
        //    while (true)
        //    {
        //        byte[] buf = new byte[MAX_BUF_SIZE];
        //        try
        //        {

        //            Socket socketClient = socket.Accept();
        //            socketClient.ReceiveFrom(buf, ref remotePoint);
        //            //string data = Encoding.Default.GetString(GetEffData(buf));
        //            //Console.WriteLine("客户端： " + data);
        //            byte[] d = Encoding.ASCII.GetBytes("(0.1,0.1,0.1,0.2,0.2,0.2)\n");
        //            socketClient.SendTo(d, remotePoint);
        //            buf = null;
        //            socketClient.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("error");
        //            Console.WriteLine(ex.Message.ToString());
        //            socket.Close();
        //            socket.Dispose();
        //            break;
        //        }

        //    }

        //}
    }
}
