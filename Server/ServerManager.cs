using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public interface ServerListener
    {
        void OnClientJoined(IntPtr handle, string ipAddress);
        void OnClientRemoved(IntPtr handle);
        void OnReceiveClientMessage(IntPtr handle, string message);
        void OnServerDisconnected();
    }

    public class ServerManager
    {
        private const int BACK_LOG = 20;
        private const int MAX_BUF_SIZE = 1024;

        private ServerListener serverListener;
        private Socket serverSocket;
        private IPEndPoint ipEndPoint;
        private List<Socket> connectedClients;
        private bool runAcceptThread;
        private bool isSend;
        private string sendToClientMessage;

        private static object guard = new object();
        private static object recvGuard = new object();
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
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ipEndPoint = new IPEndPoint(IPAddress.Any, port);
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(BACK_LOG);
            }
            catch (Exception ex)
            {
                return false;
            }

            LuanchAcceptThread();
            LuanchSendThread();
            return true;
        }

        public void StopTCPServer()
        {
            if (runAcceptThread)
            {
                CloseSocket();
                RemoveAllClients();
                runAcceptThread = false;
                if (serverListener != null)
                    serverListener.OnServerDisconnected();
            }
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
            Thread acceptThread = new Thread(AcceptThread) { Name = "AcceptThread", IsBackground = true };
            acceptThread.Start();
        }

        private void LuanchSendThread()
        {
            runAcceptThread = true;
            Thread sendThread = new Thread(SendThread) { Name = "SendThread", IsBackground = true };
            sendThread.Start();
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
        }

        private void SendThread()
        {
            while(runAcceptThread)
            {
                if (isSend)
                {
                    SendToClients();
                    isSend = false;
                }
            }
        }

        private void SendToClients()
        {
            EndPoint remotePoint = ipEndPoint as EndPoint;
            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                try
                {
                    connectedClients[i].SendTo(Encoding.ASCII.GetBytes(sendToClientMessage), remotePoint);
                }
                catch (Exception ex)
                {
                }
            }
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
            while (true)
            {
                try
                {
                    ClearBuffer(recvBuffer);
                    lock (recvGuard)
                    {
                        int recvLength = clientSocket.ReceiveFrom(recvBuffer, ref remotePoint);
                        string recvData = Encoding.Default.GetString(recvBuffer, 0, recvLength);
                        if (serverListener != null)
                            serverListener.OnReceiveClientMessage(clientSocket.Handle, recvData);
                    }
                }
                catch (Exception ex)
                {
                }
                if (ClientIsNotAlive(clientSocket))
                {
                    lock(guard)
                    {
                        RemoveClient(clientSocket.Handle);
                    }
                    break;
                }
            }
        }

        private bool ClientIsNotAlive(Socket client)
        {
            try
            {
                bool isDead = client.Poll(10, SelectMode.SelectRead);
                int clientIndex = FindClientIndex(client.Handle);
                return isDead || (clientIndex < 0);
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private int FindClientIndex(IntPtr handle)
        {

            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                if (connectedClients[i].Handle == handle)
                {
                    return i;
                }
            }
            return -1;
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

        private bool RemoveClient(IntPtr handle)
        {
            bool isRemoved = false;

            int numClients = connectedClients.Count;
            for (int i = 0; i < numClients; ++i)
            {
                if (connectedClients[i].Handle == handle)
                {
                    connectedClients[i].Close();
                    connectedClients.RemoveAt(i);
                    isRemoved = true;
                    break;
                }
            }

            if (isRemoved && serverListener != null)
                serverListener.OnClientRemoved(handle);
            return isRemoved;
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

        public void SendToClient(string message)
        {
            if (message.Length > 0)
            {
                isSend = true;
                sendToClientMessage = message;
            }
        }
    }
}
