using System;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form, ServerListener
    {
        private int port = 6000;

        public ServerForm()
        {
            InitializeComponent();
            ServerManager.Instance.SetServerListener(this);
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
            clientList.Items.Add(ipAddress);
        }
    }
}
