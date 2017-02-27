using System;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Client
{
    public partial class ClientForm : Form, ClientListener
    {
        private const string DEFAULT_SERVER_IP_ADDRESS = "127.0.0.1";

        private const string CMD_SET_BULB_STATE_ON = "SetBulbState:on";
        private const string CMD_SET_BULB_STATE_OFF = "SetBulbState:off";
        private const string CMD_SET_TEMPERATURE_VALUE = "SetTemperatureValue:";
        private const string CMD_SET_HUMIDITY_VALUE = "SetHumidityValue:";
        private const string CMD_BULB_STATE_ON = "BulbState:on";
        private const string CMD_BULB_STATE_OFF = "BulbState:off";
        private const string CMD_TEMPERATURE_VALUE = "Temperature:";
        private const string CMD_HUMIDITY_VALUE = "Humidity:";

        private int port = 8008;
        private bool lightIsOn = true;
        private Image lightOnImage;
        private Image lightOffImage;
        private const string LIGHT_ON_IMAGE_NAME = "LightBright.bmp";
        private const string LIGHT_OFF_IMAGE_NAME = "LightDark.bmp";
        public ClientForm()
        {
            InitializeComponent();
            SetDefaultServerIpAddress();

            ClientManager.Instance.SetClientListener(this);
            LoadLightImages();
            SetLightBulbImage();
        }

        private void LoadLightImages()
        {
            try
            {
                lightOnImage = Image.FromFile(LIGHT_ON_IMAGE_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show("未能加载图片："+ LIGHT_ON_IMAGE_NAME);
            }
            try
            {
                lightOffImage = Image.FromFile(LIGHT_OFF_IMAGE_NAME);
            }
            catch (Exception ex)
            {
                MessageBox.Show("未能加载图片：" + LIGHT_ON_IMAGE_NAME);
            }
        }

        private void SetLightBulbImage()
        {
            lightBulbPictureBox.Image = lightIsOn ? lightOnImage : lightOffImage;
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
                ConnectToServer();
            }
            else {
                ClientManager.Instance.Disconnect();
            }
        }

        private void ConnectToServer()
        {
            string ipAddress = GenerateIpAddress();
            bool success = ClientManager.Instance.ConnectToServer(ipAddress, port);
            if (!success)
            {
                MessageBox.Show("无法连接服务器");
                connectToServerCheckBox.Checked = false;
            }

            TellServerStatus();
        }

        private void TellServerStatus()
        {
            string bulbState = lightIsOn ? CMD_BULB_STATE_ON : CMD_BULB_STATE_OFF;
            ClientManager.Instance.SendToServer(bulbState);
            Thread.Sleep(30);

            string temperature = temperatureTextBox.Text == "" ? "0" : temperatureTextBox.Text;
            ClientManager.Instance.SendToServer(CMD_TEMPERATURE_VALUE + temperature);
            Thread.Sleep(30);

            string humidity = humidityText.Text == "" ? "0" : humidityText.Text;
            ClientManager.Instance.SendToServer(CMD_HUMIDITY_VALUE + humidity);
        }

        public void OnDisconnected()
        {
            ClientManager.Instance.Disconnect();
            connectToServerCheckBox.Checked = false;
        }

        private void OnBtnBulbStateSwitch(object sender, EventArgs e)
        {
            lightIsOn = !lightIsOn;
            if (lightIsOn)
            {
                btnBulbStateSwitch.Text = "关灯";
                ClientManager.Instance.SendToServer(CMD_BULB_STATE_ON);
            }
            else {
                btnBulbStateSwitch.Text = "开灯";
                ClientManager.Instance.SendToServer(CMD_BULB_STATE_OFF);
            }
            SetLightBulbImage();
        }

        public void OnReceiveServerMessage(string message)
        {
            if(message.Equals(CMD_SET_BULB_STATE_ON))
            {
                lightIsOn = true;
                btnBulbStateSwitch.Text = "关灯";
                SetLightBulbImage();
            }
            else if (message.Equals(CMD_SET_BULB_STATE_OFF))
            {
                lightIsOn = false;
                btnBulbStateSwitch.Text = "开灯";
                SetLightBulbImage();
            }
            else if (message.StartsWith(CMD_SET_TEMPERATURE_VALUE))
            {
                string temp = message.Substring(CMD_SET_TEMPERATURE_VALUE.Length);
                temperatureTextBox.Text = temp;
            }
            else if (message.StartsWith(CMD_SET_HUMIDITY_VALUE))
            {
                string humidity = message.Substring(CMD_SET_HUMIDITY_VALUE.Length);
                humidityText.Text = humidity;
            }
            TellServerStatus();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
