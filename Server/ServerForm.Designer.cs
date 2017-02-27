namespace Server
{
    partial class ServerForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.startServer = new System.Windows.Forms.CheckBox();
            this.clientList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bulbStateText = new System.Windows.Forms.TextBox();
            this.temperatureStateText = new System.Windows.Forms.TextBox();
            this.humidityStateText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.humidityValueText = new System.Windows.Forms.TextBox();
            this.temperatureValueText = new System.Windows.Forms.TextBox();
            this.btnClientHumiditySetting = new System.Windows.Forms.Button();
            this.btnClientTemperatureSetting = new System.Windows.Forms.Button();
            this.btnClientLightSwitch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // startServer
            // 
            this.startServer.AutoSize = true;
            this.startServer.Font = new System.Drawing.Font("宋体", 10F);
            this.startServer.Location = new System.Drawing.Point(30, 12);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(96, 18);
            this.startServer.TabIndex = 0;
            this.startServer.Text = "启动服务器";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.CheckedChanged += new System.EventHandler(this.StartServer_CheckedChanged);
            // 
            // clientList
            // 
            this.clientList.FormattingEnabled = true;
            this.clientList.Items.AddRange(new object[] {
            "client1",
            "client2",
            "client3"});
            this.clientList.Location = new System.Drawing.Point(14, 22);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(234, 277);
            this.clientList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "灯泡状态:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "温度:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "湿度:";
            // 
            // bulbStateText
            // 
            this.bulbStateText.Location = new System.Drawing.Point(95, 29);
            this.bulbStateText.Name = "bulbStateText";
            this.bulbStateText.ReadOnly = true;
            this.bulbStateText.Size = new System.Drawing.Size(58, 23);
            this.bulbStateText.TabIndex = 4;
            this.bulbStateText.Text = "亮";
            this.bulbStateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // temperatureStateText
            // 
            this.temperatureStateText.Location = new System.Drawing.Point(95, 68);
            this.temperatureStateText.Name = "temperatureStateText";
            this.temperatureStateText.ReadOnly = true;
            this.temperatureStateText.Size = new System.Drawing.Size(58, 23);
            this.temperatureStateText.TabIndex = 4;
            this.temperatureStateText.Text = "20";
            this.temperatureStateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // humidityStateText
            // 
            this.humidityStateText.Location = new System.Drawing.Point(95, 103);
            this.humidityStateText.Name = "humidityStateText";
            this.humidityStateText.ReadOnly = true;
            this.humidityStateText.Size = new System.Drawing.Size(58, 23);
            this.humidityStateText.TabIndex = 4;
            this.humidityStateText.Text = "30";
            this.humidityStateText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.humidityValueText);
            this.groupBox1.Controls.Add(this.temperatureValueText);
            this.groupBox1.Controls.Add(this.btnClientHumiditySetting);
            this.groupBox1.Controls.Add(this.btnClientTemperatureSetting);
            this.groupBox1.Controls.Add(this.btnClientLightSwitch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(294, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 158);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "客户端控制";
            // 
            // humidityValueText
            // 
            this.humidityValueText.Location = new System.Drawing.Point(78, 124);
            this.humidityValueText.Name = "humidityValueText";
            this.humidityValueText.Size = new System.Drawing.Size(75, 23);
            this.humidityValueText.TabIndex = 5;
            // 
            // temperatureValueText
            // 
            this.temperatureValueText.Location = new System.Drawing.Point(78, 78);
            this.temperatureValueText.Name = "temperatureValueText";
            this.temperatureValueText.Size = new System.Drawing.Size(75, 23);
            this.temperatureValueText.TabIndex = 5;
            // 
            // btnClientHumiditySetting
            // 
            this.btnClientHumiditySetting.Location = new System.Drawing.Point(170, 124);
            this.btnClientHumiditySetting.Name = "btnClientHumiditySetting";
            this.btnClientHumiditySetting.Size = new System.Drawing.Size(75, 23);
            this.btnClientHumiditySetting.TabIndex = 4;
            this.btnClientHumiditySetting.Text = "设置";
            this.btnClientHumiditySetting.UseVisualStyleBackColor = true;
            // 
            // btnClientTemperatureSetting
            // 
            this.btnClientTemperatureSetting.Location = new System.Drawing.Point(170, 78);
            this.btnClientTemperatureSetting.Name = "btnClientTemperatureSetting";
            this.btnClientTemperatureSetting.Size = new System.Drawing.Size(75, 23);
            this.btnClientTemperatureSetting.TabIndex = 4;
            this.btnClientTemperatureSetting.Text = "设置";
            this.btnClientTemperatureSetting.UseVisualStyleBackColor = true;
            // 
            // btnClientLightSwitch
            // 
            this.btnClientLightSwitch.Location = new System.Drawing.Point(78, 34);
            this.btnClientLightSwitch.Name = "btnClientLightSwitch";
            this.btnClientLightSwitch.Size = new System.Drawing.Size(75, 23);
            this.btnClientLightSwitch.TabIndex = 4;
            this.btnClientLightSwitch.Text = "开灯";
            this.btnClientLightSwitch.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 14);
            this.label7.TabIndex = 3;
            this.label7.Text = "湿度控制: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 3;
            this.label6.Text = "温度控制: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "灯泡控制:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientList);
            this.groupBox2.Location = new System.Drawing.Point(9, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 309);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户端列表:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.humidityStateText);
            this.groupBox3.Controls.Add(this.temperatureStateText);
            this.groupBox3.Controls.Add(this.bulbStateText);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(294, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(193, 156);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "客户端状态";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 355);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startServer);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.MaximizeBox = false;
            this.Name = "ServerForm";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox startServer;
        private System.Windows.Forms.ListBox clientList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox bulbStateText;
        private System.Windows.Forms.TextBox temperatureStateText;
        private System.Windows.Forms.TextBox humidityStateText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClientLightSwitch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox temperatureValueText;
        private System.Windows.Forms.TextBox humidityValueText;
        private System.Windows.Forms.Button btnClientTemperatureSetting;
        private System.Windows.Forms.Button btnClientHumiditySetting;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

