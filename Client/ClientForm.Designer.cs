namespace Client
{
    partial class ClientForm
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
            this.serverIpAddress = new IPInputControl.Ctrl.IPInput();
            this.connectToServerCheckBox = new System.Windows.Forms.CheckBox();
            this.btnBulbStateSwitch = new System.Windows.Forms.Button();
            this.lightBulbPictureBox = new System.Windows.Forms.PictureBox();
            this.temperatureTextBox = new System.Windows.Forms.TextBox();
            this.humidityTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.humidityTextBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.temperatureTextBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lightBulbPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // serverIpAddress
            // 
            this.serverIpAddress.BackColor = System.Drawing.Color.White;
            this.serverIpAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverIpAddress.Location = new System.Drawing.Point(24, 57);
            this.serverIpAddress.Name = "serverIpAddress";
            this.serverIpAddress.Size = new System.Drawing.Size(179, 26);
            this.serverIpAddress.TabIndex = 0;
            // 
            // connectToServerCheckBox
            // 
            this.connectToServerCheckBox.AutoSize = true;
            this.connectToServerCheckBox.Location = new System.Drawing.Point(24, 22);
            this.connectToServerCheckBox.Name = "connectToServerCheckBox";
            this.connectToServerCheckBox.Size = new System.Drawing.Size(96, 18);
            this.connectToServerCheckBox.TabIndex = 1;
            this.connectToServerCheckBox.Text = "连接服务器";
            this.connectToServerCheckBox.UseVisualStyleBackColor = true;
            this.connectToServerCheckBox.CheckedChanged += new System.EventHandler(this.OnConnectServerChanged);
            // 
            // btnBulbStateSwitch
            // 
            this.btnBulbStateSwitch.Location = new System.Drawing.Point(121, 122);
            this.btnBulbStateSwitch.Name = "btnBulbStateSwitch";
            this.btnBulbStateSwitch.Size = new System.Drawing.Size(87, 25);
            this.btnBulbStateSwitch.TabIndex = 2;
            this.btnBulbStateSwitch.Text = "关灯";
            this.btnBulbStateSwitch.UseVisualStyleBackColor = true;
            this.btnBulbStateSwitch.Click += new System.EventHandler(this.OnBtnBulbStateSwitch);
            // 
            // lightBulbPictureBox
            // 
            this.lightBulbPictureBox.Location = new System.Drawing.Point(48, 100);
            this.lightBulbPictureBox.Name = "lightBulbPictureBox";
            this.lightBulbPictureBox.Size = new System.Drawing.Size(63, 61);
            this.lightBulbPictureBox.TabIndex = 3;
            this.lightBulbPictureBox.TabStop = false;
            // 
            // temperatureTextBox
            // 
            this.temperatureTextBox.Location = new System.Drawing.Point(102, 183);
            this.temperatureTextBox.MaxLength = 5;
            this.temperatureTextBox.Name = "temperatureTextBox";
            this.temperatureTextBox.Size = new System.Drawing.Size(116, 23);
            this.temperatureTextBox.TabIndex = 4;
            this.temperatureTextBox.Text = "20";
            this.temperatureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // humidityTextBox
            // 
            this.humidityTextBox.Location = new System.Drawing.Point(102, 264);
            this.humidityTextBox.MaxLength = 5;
            this.humidityTextBox.Name = "humidityTextBox";
            this.humidityTextBox.Size = new System.Drawing.Size(116, 23);
            this.humidityTextBox.TabIndex = 4;
            this.humidityTextBox.Text = "30";
            this.humidityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "温度1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "湿度1:";
            // 
            // humidityTextBox2
            // 
            this.humidityTextBox2.Location = new System.Drawing.Point(102, 309);
            this.humidityTextBox2.MaxLength = 5;
            this.humidityTextBox2.Name = "humidityTextBox2";
            this.humidityTextBox2.Size = new System.Drawing.Size(116, 23);
            this.humidityTextBox2.TabIndex = 4;
            this.humidityTextBox2.Text = "30";
            this.humidityTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "湿度2:";
            // 
            // temperatureTextBox2
            // 
            this.temperatureTextBox2.Location = new System.Drawing.Point(102, 223);
            this.temperatureTextBox2.MaxLength = 5;
            this.temperatureTextBox2.Name = "temperatureTextBox2";
            this.temperatureTextBox2.Size = new System.Drawing.Size(116, 23);
            this.temperatureTextBox2.TabIndex = 4;
            this.temperatureTextBox2.Text = "20";
            this.temperatureTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "温度2:";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 374);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.humidityTextBox2);
            this.Controls.Add(this.humidityTextBox);
            this.Controls.Add(this.temperatureTextBox2);
            this.Controls.Add(this.temperatureTextBox);
            this.Controls.Add(this.lightBulbPictureBox);
            this.Controls.Add(this.btnBulbStateSwitch);
            this.Controls.Add(this.connectToServerCheckBox);
            this.Controls.Add(this.serverIpAddress);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.lightBulbPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IPInputControl.Ctrl.IPInput serverIpAddress;
        private System.Windows.Forms.CheckBox connectToServerCheckBox;
        private System.Windows.Forms.Button btnBulbStateSwitch;
        private System.Windows.Forms.PictureBox lightBulbPictureBox;
        private System.Windows.Forms.TextBox temperatureTextBox;
        private System.Windows.Forms.TextBox humidityTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox humidityTextBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox temperatureTextBox2;
        private System.Windows.Forms.Label label4;
    }
}

