using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form, ServerListener
    {
        private int port = 6000;
        private List<IntPtr> clientHandles;
        public ServerForm()
        {
            InitializeComponent();
            NecessaryInitialization();
        }

        private void NecessaryInitialization()
        {
            ServerManager.Instance.SetServerListener(this);
            clientHandles = new List<IntPtr>();
            clientList.Items.Clear();
        }

        private void StartServer_CheckedChanged(object sender, EventArgs e)
        {
            if (startServer.Checked) {
                bool success = ServerManager.Instance.StartTCPServer(port);
                if (!success)
                {
                    MessageBox.Show("启动服务器失败");
                    startServer.Checked = false;
                }
            }
            else {
                ServerManager.Instance.StopTCPServer();
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerManager.Instance.IsRunning)
            {
                ServerManager.Instance.StopTCPServer();
            }
        }

        public void OnClientJoined(IntPtr handle, string ipAddress)
        {
            clientHandles.Add(handle);
            clientList.Items.Add(ipAddress);
        }

        public void OnClientRemoved(IntPtr handle)
        {
            int clientPosition = FindClientPosition(handle);
            clientHandles.RemoveAt(clientPosition);
            clientList.Items.RemoveAt(clientPosition);
        }

        private int FindClientPosition(IntPtr handle)
        {
            int numClients = clientHandles.Count;
            for (int i = 0; i < numClients; ++i)
            {
                if (clientHandles[i] == handle)
                    return i;
            }
            return -1;
        }

        public void OnServerDisconnected()
        {
            clientList.Items.Clear();
        }
    }
}
