using System;
using System.Text;
using System.Windows.Forms;
namespace Client
{
    public partial class ClientForm : Form, ClientListener
    {
        private const string DEFAULT_SERVER_IP_ADDRESS = "127.0.0.1";
        private int port = 6000;

        public ClientForm()
        {
            InitializeComponent();
            SetDefaultServerIpAddress();

            ClientManager.Instance.SetClientListener(this);
        }

        private void SetDefaultServerIpAddress()
        {
            string[] vals = DEFAULT_SERVER_IP_ADDRESS.Split('.');
            if (vals.Length < 4)
                return;
            serverIpAddress.txt_1.Text = vals[0];
            serverIpAddress.txt_2.Text = vals[1];
            serverIpAddress.txt_3.Text = vals[2];
            serverIpAddress.txt_4.Text = vals[3];
        }

        private string GenerateIpAddress()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(serverIpAddress.txt_1.Text);
            sb.Append(".");
            sb.Append(serverIpAddress.txt_2.Text);
            sb.Append(".");
            sb.Append(serverIpAddress.txt_3.Text);
            sb.Append(".");
            sb.Append(serverIpAddress.txt_4.Text);

            return sb.ToString();
        }

        private void OnConnectServerChanged(object sender, System.EventArgs e)
        {
            if (connectToServerCheckBox.Checked)
            {
                string ipAddress = GenerateIpAddress();
                bool success = ClientManager.Instance.ConnectToServer(ipAddress, port);
                if (!success)
                {
                    MessageBox.Show("无法连接服务器");
                    connectToServerCheckBox.Checked = false;
                }
            }
            else {
                ClientManager.Instance.Disconnect();
            }
        }

        public void OnDisconnected()
        {
            ClientManager.Instance.Disconnect();
            connectToServerCheckBox.Checked = false;
        }

        private void OnBtnSendToServer(object sender, EventArgs e)
        {
            ClientManager.Instance.SendToServer("Hello, server");
        }
    }
}
