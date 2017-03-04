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
        void OnReceiveServerMessage(string message);
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
        private bool runSendThread = false;
        private bool runReceiveThread = false;
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
                throw ex;
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
            runSendThread = false;
            runReceiveThread = false;
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
            runSendThread = true;
            while (runSendThread)
            {
                if (isSend)
                {
                    SendToServer();
                }
            }
        }

        private void ReceiveThread()
        {
            byte[] recvBuffer = new byte[MAX_BUF_SIZE];
            runReceiveThread = true;
            while (runReceiveThread)
            {
                try
                {
                    int recvLength = clientSocket.Receive(recvBuffer);
                    string recvData = Encoding.Default.GetString(recvBuffer, 0, recvLength);
                    if (clientListener != null)
                        clientListener.OnReceiveServerMessage(recvData);
                }
                catch (SocketException ex)
                {
                    Disconnected();
                    break;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Disconnected()
        {
            Disconnect();
            if (clientListener != null)
                clientListener.OnDisconnected();
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