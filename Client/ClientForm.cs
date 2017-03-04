using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Client
{
    public partial class ClientForm : Form, ClientListener
    {
        private const int DEFAULT_PORT_NUMBER = 8008;
        private const string DEFAULT_SERVER_IP_ADDRESS = "127.0.0.1";
        private const bool DEFAULT_LIGHT_IS_ON = true;
        private const float DEFAULT_TEMPERATURE = 20;
        private const float DEFAULT_HUMIDITY = 30;
        private const float DEFAULT_TEMPERATURE2 = 15;
        private const float DEFAULT_HUMIDITY2 = 25;

        private const string LIGHT_ON_IMAGE_NAME = "LightBright.bmp";
        private const string LIGHT_OFF_IMAGE_NAME = "LightDark.bmp";

        private Image lightOnImage;
        private Image lightOffImage;
        private ClientStatus clientStatus;
        private Dictionary<string, CommandBase> serverCommands;

        public ClientForm()
        {
            InitializeComponent();
            LoadLightImages();

            SetDefaultServerIpAddress();
            InitializeClientStatus();
            SetUIValues();
            BindServerCommands();

            RegistClientListener();
        }

        private void LoadLightImages()
        {
            lightOnImage = LoadImage(LIGHT_ON_IMAGE_NAME);
            lightOffImage = LoadImage(LIGHT_OFF_IMAGE_NAME);
        }

        private Image LoadImage(string imageName)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(imageName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("未能加载图片：" + imageName);
            }
            return image;
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

        private void InitializeClientStatus()
        {
            clientStatus = new ClientStatus();
            clientStatus.LightIsOn = DEFAULT_LIGHT_IS_ON;
            clientStatus.Temperature = DEFAULT_TEMPERATURE;
            clientStatus.Humidity = DEFAULT_HUMIDITY;
            clientStatus.Temperature2 = DEFAULT_TEMPERATURE2;
            clientStatus.Humidity2 = DEFAULT_HUMIDITY2;
        }

        private void SetUIValues()
        {
            SetLightBulbImageAndButtonText();
            SetTemperatureAndHumidity();
        }

        private void BindServerCommands()
        {
            serverCommands = new Dictionary<string, CommandBase>();
            serverCommands.Add(CommandConstant.COMMAND_LIGHT_STATE, new LightStateCommand("LightState", OnLightStateCommandHandler));
            serverCommands.Add(CommandConstant.COMMAND_TEMPERATURE, new TemperatureCommand("Temerature", OnTemperatureCommandHandler));
            serverCommands.Add(CommandConstant.COMMAND_HUMIDITY, new HumidityCommand("Humidity", OnHumidityCommandHandler));
            serverCommands.Add(CommandConstant.COMMAND_TEMPERATURE2, new TemperatureCommand("Temerature2", OnTemperatureCommandHandler2));
            serverCommands.Add(CommandConstant.COMMAND_HUMIDITY2, new HumidityCommand("Humidity2", OnHumidityCommandHandler2));
        }

        private void OnLightStateCommandHandler(object[] args)
        {
            if (args == null || args.Length < 1)
                return;

            string value = (string)args[0];
            clientStatus.LightIsOn = value.ToLower() == "on" ? true : false;
        }

        private void OnTemperatureCommandHandler(object[] args)
        {
            if (args == null || args.Length < 1)
                return;

            string value = (string)args[0];
            clientStatus.Temperature = ParseStringToFloatValue(value);
        }

        private void OnHumidityCommandHandler(object[] args)
        {
            if (args == null || args.Length < 1)
                return;

            string value = (string)args[0];
            clientStatus.Humidity = ParseStringToFloatValue(value);
        }

        private void OnTemperatureCommandHandler2(object[] args)
        {
            if (args == null || args.Length < 1)
                return;

            string value = (string)args[0];
            clientStatus.Temperature2 = ParseStringToFloatValue(value);
        }

        private void OnHumidityCommandHandler2(object[] args)
        {
            if (args == null || args.Length < 1)
                return;

            string value = (string)args[0];
            clientStatus.Humidity2 = ParseStringToFloatValue(value);
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

        private void SetLightBulbImageAndButtonText()
        {
            lightBulbPictureBox.Image = clientStatus.LightIsOn ? lightOnImage : lightOffImage;
            btnBulbStateSwitch.Text = clientStatus.LightIsOn ?  "关灯": "开灯";
        }

        private void SetTemperatureAndHumidity()
        {
            temperatureTextBox.Text = clientStatus.Temperature.ToString();
            humidityTextBox.Text = clientStatus.Humidity.ToString();
            temperatureTextBox2.Text = clientStatus.Temperature2.ToString();
            humidityTextBox2.Text = clientStatus.Humidity2.ToString();
        }

        private void RegistClientListener()
        {
            ClientManager.Instance.SetClientListener(this);
        }

        private void OnConnectServerChanged(object sender, System.EventArgs e)
        {
            if (connectToServerCheckBox.Checked)
            {
                try
                {
                    ConnectToServer();
                    TellServerClientStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connectToServerCheckBox.Checked = false;
                }
            }
            else {
                ClientManager.Instance.Disconnect();
            }
        }

        private void ConnectToServer()
        {
            string ipAddress = GenerateIpAddress();
            try
            {
                ClientManager.Instance.ConnectToServer(ipAddress, DEFAULT_PORT_NUMBER);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private void TellServerClientStatus()
        {
            string sendMessage = GenerateSendMessage();
            ClientManager.Instance.SendToServer(sendMessage);
        }

        private string GenerateSendMessage()
        {
            string clientLightState = clientStatus.LightIsOn ? "on" : "off";
            string clientTemperature = clientStatus.Temperature.ToString();
            string clientHumidity = clientStatus.Humidity.ToString();
            string clientTemperature2 = clientStatus.Temperature2.ToString();
            string clientHumidity2 = clientStatus.Humidity2.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(CommandConstant.COMMAND_CLIENT_COMMAND_PREFIX);
            AppendCommandToStringBuilder(ref sb, CommandConstant.COMMAND_LIGHT_STATE + CommandConstant.COMMAND_DELIMITER, clientLightState);
            AppendCommandToStringBuilder(ref sb, CommandConstant.COMMAND_TEMPERATURE + CommandConstant.COMMAND_DELIMITER, clientTemperature);
            AppendCommandToStringBuilder(ref sb, CommandConstant.COMMAND_HUMIDITY + CommandConstant.COMMAND_DELIMITER, clientHumidity);
            AppendCommandToStringBuilder(ref sb, CommandConstant.COMMAND_TEMPERATURE2 + CommandConstant.COMMAND_DELIMITER, clientTemperature2);
            AppendCommandToStringBuilder(ref sb, CommandConstant.COMMAND_HUMIDITY2 + CommandConstant.COMMAND_DELIMITER, clientHumidity2);

            return sb.ToString();
        }

        private void AppendCommandToStringBuilder(ref StringBuilder sb, string command, string value)
        {
            sb.Append(command);
            sb.Append(value);
            sb.Append("\n");
        }

        public void OnDisconnected()
        {
            ClientManager.Instance.Disconnect();
            connectToServerCheckBox.Checked = false;
        }

        private void OnBtnBulbStateSwitch(object sender, EventArgs e)
        {
            clientStatus.LightIsOn = !clientStatus.LightIsOn;
            SetLightBulbImageAndButtonText();
            TellServerClientLightState();
        }

        private void TellServerClientLightState()
        {
            string lightState = clientStatus.LightIsOn ? "on" : "off";
            string message = CommandConstant.COMMAND_CLIENT_COMMAND_PREFIX +
                            CommandConstant.COMMAND_LIGHT_STATE + CommandConstant.COMMAND_DELIMITER +
                            lightState;
            ClientManager.Instance.SendToServer(message);
        }

        public void OnReceiveServerMessage(string message)
        {
            if (IsServerCommand(message))
            {
                PostServerMessage(message);
                SetUIValues();
                TellServerClientStatus();
            }
        }

        private bool IsServerCommand(string message)
        {
            return message.StartsWith(CommandConstant.COMMAND_SERVER_COMMAND_PREFIX);
        }

        private void PostServerMessage(string message)
        {
            message = message.Substring(CommandConstant.COMMAND_CLIENT_COMMAND_PREFIX.Length);
            string[] commands = message.Split('\n');
            foreach (string command in commands)
            {
                PostServerCommand(command);
            }
        }

        private void PostServerCommand(string command)
        {
            string clientCommand, value;
            if (TryParseClientCommand(command, out clientCommand, out value))
            {
                CommandBase commandBase;
                if (serverCommands.TryGetValue(clientCommand, out commandBase))
                {
                    commandBase.ExecuteCommand(value);
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

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (connectToServerCheckBox.Checked)
            {
                ClientManager.Instance.Disconnect();
            }
        }
    }
}
