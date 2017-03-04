using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form, ServerListener
    {
        private int port = 8008;

        private List<IntPtr> clientHandles;
        private Dictionary<IntPtr, ClientStatus> clientStatusTable;
        private Dictionary<string, CommandBase> clientCommands;

        public ServerForm()
        {
            InitializeComponent();
            NecessaryInitialization();
            ResetUIValues();
            BindClientCommands();
        }

        private void NecessaryInitialization()
        {
            ServerManager.Instance.SetServerListener(this);
            clientHandles = new List<IntPtr>();
            clientStatusTable = new Dictionary<IntPtr, ClientStatus>();
            clientCommands = new Dictionary<string, CommandBase>();
            clientList.Items.Clear();
        }

        private void ResetUIValues()
        {
            bulbStateText.Text = "";
            temperatureStateText.Text = "";
            humidityStateText.Text = "";
            SetButtonsEnabled(false);
        }

        private void BindClientCommands()
        {
            clientCommands.Add(CommandConstant.COMMAND_LIGHT_STATE, new LightStateCommand("LightState", OnLightStateCommandHandler));
            clientCommands.Add(CommandConstant.COMMAND_TEMPERATURE, new TemperatureCommand("Temerature", OnTemperatureCommandHandler));
            clientCommands.Add(CommandConstant.COMMAND_HUMIDITY, new HumidityCommand("Humidity", OnHumidityCommandHandler));
        }

        private void OnStartServerCheckedChanged(object sender, EventArgs e)
        {
            if (startServer.Checked) {
                try
                {
                    ServerManager.Instance.StartTCPServer(port);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            if (clientPosition >= 0)
            {
                clientHandles.RemoveAt(clientPosition);
                clientList.Items.RemoveAt(clientPosition);
                clientStatusTable.Remove(handle);
            }
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
                string lightIsOn = clientStatus.LightBulbIsOn ? "off" : "on";
                string message = CommandConstant.COMMAND_SERVER_COMMAND_PREFIX + 
                                CommandConstant.COMMAND_LIGHT_STATE + CommandConstant.COMMAND_DELIMITER +
                                lightIsOn;
                IntPtr clientHandle = clientHandles[clientList.SelectedIndex];
                ServerManager.Instance.SendToClient(clientHandle, message);
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
            EnsureClientStatusInstantiated(handle);
            if (IsClientCommand(message))
            {
                PostClientMessage(handle, message);
            }
            UpdateClientValues();
        }

        private void EnsureClientStatusInstantiated(IntPtr handle)
        {
            if (!clientStatusTable.ContainsKey(handle))
            {
                ClientStatus status = new ClientStatus();
                clientStatusTable.Add(handle, status);
            }
        }

        private bool IsClientCommand(string message)
        {
            return message.StartsWith(CommandConstant.COMMAND_CLIENT_COMMAND_PREFIX);
        }

        private void PostClientMessage(IntPtr handle, string message)
        {
            message = message.Substring(CommandConstant.COMMAND_CLIENT_COMMAND_PREFIX.Length);
            string[] commands = message.Split('\n');
            foreach (string command in commands)
            {
                PostClientCommand(handle, command);
            }
        }

        private void PostClientCommand(IntPtr handle, string command)
        {
            string clientCommand, value;
            if (TryParseClientCommand(command, out clientCommand, out value))
            {
                CommandBase commandBase;
                if (clientCommands.TryGetValue(clientCommand, out commandBase))
                {
                    commandBase.ExecuteCommand(handle, value);
                }
            }
        }

        private bool TryParseClientCommand(string command, out string clientCommand, out string value)
        {
            clientCommand = "";
            value = "";

            command = command.Trim();
            string[] vals = command.Split(CommandConstant.COMMAND_DELIMITER);
            if (vals == null || vals.Length < 2)
                return false;

            clientCommand = vals[0].Trim();
            value = vals[1].Trim();
            return true;
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
            if (clientList.SelectedIndex >= 0)
            {
                SetUIValues();
            }
            else
            {
                ResetUIValues();
            }
        }

        private void SetUIValues()
        {
            ClientStatus clentStatus = GetSelectedClientStatus();
            if (clentStatus != null)
            {
                SetUITexts(clentStatus);
                SetButtonsEnabled(true);
            } else
            {
                MessageBox.Show("无法获取客户端数据");
            }
        }

        private void SetUITexts(ClientStatus clentStatus)
        {
            bulbStateText.Text = clentStatus.LightBulbIsOn ? "亮" : "暗";
            temperatureStateText.Text = clentStatus.Temperature.ToString();
            humidityStateText.Text = clentStatus.Humidity.ToString();
            btnClientLightSwitch.Text = clentStatus.LightBulbIsOn ? "关灯" : "开灯";
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnClientLightSwitch.Enabled = enabled;
            btnClientTemperatureSetting.Enabled = enabled;
            btnClientHumiditySetting.Enabled = enabled;
        }

        private void OnBtnClientTemperatureSettingClicked(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex < 0)
            {
                MessageBox.Show("未选中客户端");
                return;
            }

            if(ValidText(temperatureValueText.Text))
                SendToSelectedClient(CommandConstant.COMMAND_TEMPERATURE, temperatureValueText.Text);
        }

        private void OnBtnClientHumiditySettingClicked(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex < 0)
            {
                MessageBox.Show("未选中客户端");
                return;
            }

            if (ValidText(humidityValueText.Text))
                SendToSelectedClient(CommandConstant.COMMAND_HUMIDITY, humidityValueText.Text);
        }

        private bool ValidText(string text)
        {
            if (temperatureValueText.Text == "")
            {
                MessageBox.Show("值不能为空");
                return false;
            }

            bool isNumber = Util.IsNumber(temperatureValueText.Text);
            if (!isNumber)
            {
                MessageBox.Show("输入数字不合法");
                return false;
            }
            return true;
        }

        void SendToSelectedClient(string commandType, string commandValue)
        {
            string message = CommandConstant.COMMAND_SERVER_COMMAND_PREFIX +
                    commandType + CommandConstant.COMMAND_DELIMITER + commandValue;
            IntPtr clientHandle = clientHandles[clientList.SelectedIndex];
            ServerManager.Instance.SendToClient(clientHandle, message);
        }

        private void OnLightStateCommandHandler(params object[] args)
        {
            if (args == null || args.Length < 2)
                return;
            IntPtr handle = (IntPtr)args[0];
            string value = (string)args[1];
            bool lightIsOn = value.ToLower() == "on" ? true : false;
            SetClientLightState(handle, lightIsOn);
        }

        private void OnTemperatureCommandHandler(params object[] args)
        {
            if (args == null || args.Length < 2)
                return;

            IntPtr handle = (IntPtr)args[0];
            string value = (string)args[1];
            float temperature = ParseStringToFloatValue(value);
            SetClientTemperature(handle, temperature);
        }

        private void OnHumidityCommandHandler(params object[] args)
        {
            if (args == null || args.Length < 2)
                return;
            IntPtr handle = (IntPtr)args[0];
            string value = (string)args[1];
            float humidity = ParseStringToFloatValue(value);
            SetClientHumidity(handle, humidity);
        }

        private float ParseStringToFloatValue(string value)
        {
            float result = 0;
            try
            {
                result = float.Parse(value);
            }
            catch (System.Exception ex)
            {
                result = 0;
            }
            return result;
        }

        private void SetClientLightState(IntPtr handle, bool lightIsOn)
        {
            ClientStatus clientStatus;
            if (clientStatusTable.TryGetValue(handle, out clientStatus))
            {
                clientStatus.LightBulbIsOn = lightIsOn;
            }
        }

        private void SetClientTemperature(IntPtr handle, float temperature)
        {
            ClientStatus clientStatus;
            if (clientStatusTable.TryGetValue(handle, out clientStatus))
            {
                clientStatus.Temperature = temperature;
            }
        }

        private void SetClientHumidity(IntPtr handle, float humidity)
        {
            ClientStatus clientStatus;
            if (clientStatusTable.TryGetValue(handle, out clientStatus))
            {
                clientStatus.Humidity = humidity;
            }
        }
    }
}
