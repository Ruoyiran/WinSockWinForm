using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form, ServerListener
    {
        private const string CMD_SET_BULB_STATE_ON = "SetBulbState:on";
        private const string CMD_SET_BULB_STATE_OFF = "SetBulbState:off";
        private const string CMD_SET_TEMPERATURE_VALUE = "SetTemperatureValue:";
        private const string CMD_SET_HUMIDITY_VALUE = "SetHumidityValue:";
        private const string CMD_BULB_STATE_ON = "BulbState:on";
        private const string CMD_BULB_STATE_OFF = "BulbState:off";
        private const string CMD_TEMPERATURE_VALUE = "Temperature:";
        private const string CMD_HUMIDITY_VALUE = "Humidity:";

        private int port = 8008;
        private List<IntPtr> clientHandles;
        private Dictionary<IntPtr, ClientStatus> clientStatusTable;

        public ServerForm()
        {
            InitializeComponent();
            ResetUIValues();
            NecessaryInitialization();
        }

        private void ResetUIValues()
        {
            bulbStateText.Text = "";
            temperatureStateText.Text = "";
            humidityStateText.Text = "";
            btnClientLightSwitch.Enabled = false;
            btnClientTemperatureSetting.Enabled = false;
            btnClientHumiditySetting.Enabled = false;
        }

        private void NecessaryInitialization()
        {
            ServerManager.Instance.SetServerListener(this);
            clientHandles = new List<IntPtr>();
            clientStatusTable = new Dictionary<IntPtr, ClientStatus>();
            clientList.Items.Clear();
        }

        private void OnStartServerCheckedChanged(object sender, EventArgs e)
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
            string clientName = "Client" + (clientList.Items.Count + 1);
            clientList.Items.Add(clientName + ": " + ipAddress);
        }

        public void OnClientRemoved(IntPtr handle)
        {
            int clientPosition = FindClientPosition(handle);
            clientHandles.RemoveAt(clientPosition);
            clientList.Items.RemoveAt(clientPosition);
            clientStatusTable.Remove(handle);
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
            ResetUIValues();
        }

        private void OnClientBulbSwitchClicked(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex < 0)
            {
                MessageBox.Show("未选中客户端");
                return;
            }

            ClientStatus clientStatus = GetSelectedClientStatus();

            if (clientStatus != null)
            {
                if(clientStatus.BulbState)
                {
                    btnClientLightSwitch.Text = "开灯";
                    ServerManager.Instance.SendToClient(CMD_SET_BULB_STATE_OFF);
                } else
                {
                    btnClientLightSwitch.Text = "关灯";
                    ServerManager.Instance.SendToClient(CMD_SET_BULB_STATE_ON);
                }
            }
        }

        private ClientStatus GetSelectedClientStatus()
        {
            if (clientList.SelectedIndex < 0)
            {
                return null;
            }

            IntPtr handle = clientHandles[clientList.SelectedIndex];
            if (!clientStatusTable.ContainsKey(handle))
            {
                return null;
            }

            ClientStatus clentStatus = clientStatusTable[handle];
            return clentStatus;
        }

        public void OnReceiveClientMessage(IntPtr handle, string message)
        {
            if (!clientStatusTable.ContainsKey(handle))
            {
                ClientStatus status = new ClientStatus();
                clientStatusTable.Add(handle, status);
            }

            ClientStatus clientStatus = clientStatusTable[handle];
            if (message.Equals(CMD_BULB_STATE_ON))
            {
                clientStatus.BulbState = true;
            }
            else if (message.Equals(CMD_BULB_STATE_OFF))
            {
                clientStatus.BulbState = false;
            }
            else if (message.StartsWith(CMD_TEMPERATURE_VALUE))
            {
                string temp = message.Substring(CMD_TEMPERATURE_VALUE.Length);
                clientStatus.Temperature = float.Parse(temp);
            } else if (message.StartsWith(CMD_HUMIDITY_VALUE))
            {
                string humidity = message.Substring(CMD_HUMIDITY_VALUE.Length);
                clientStatus.Humidity = float.Parse(humidity);
            }

            UpdateClientValues();
        }

        private void OnSelectedValueChanged(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex < 0)
            {
                ResetUIValues();
                return;
            }

            UpdateClientValues();
        }

        private void UpdateClientValues()
        {
            ClientStatus clentStatus = GetSelectedClientStatus();
            if (clentStatus != null)
            {
                bulbStateText.Text = clentStatus.BulbState ? "亮" : "暗";
                temperatureStateText.Text = clentStatus.Temperature.ToString();
                humidityStateText.Text = clentStatus.Humidity.ToString();

                SetButtonState(clentStatus);
            } else
            {
                ResetUIValues();
            }
        }

        private void SetButtonState(ClientStatus clentStatus)
        {
            btnClientLightSwitch.Enabled = true;
            btnClientTemperatureSetting.Enabled = true;
            btnClientHumiditySetting.Enabled = true;
            btnClientLightSwitch.Text = clentStatus.BulbState ? "关灯" : "开灯";
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void OnBtnClientTemperatureSettingClicked(object sender, EventArgs e)
        {
            if (temperatureValueText.Text == "")
            {
                MessageBox.Show("温度值不能为空");
                return;
            }
            ServerManager.Instance.SendToClient(CMD_SET_TEMPERATURE_VALUE + temperatureValueText.Text);
        }

        private void OnBtnClientHumiditySettingClicked(object sender, EventArgs e)
        {
            if (humidityStateText.Text == "")
            {
                MessageBox.Show("湿度值不能为空");
                return;
            }
            ServerManager.Instance.SendToClient(CMD_SET_HUMIDITY_VALUE + humidityValueText.Text);
        }
    }
}
