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
            this.btnSendToServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverIpAddress
            // 
            this.serverIpAddress.BackColor = System.Drawing.Color.White;
            this.serverIpAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.serverIpAddress.Location = new System.Drawing.Point(21, 53);
            this.serverIpAddress.Name = "serverIpAddress";
            this.serverIpAddress.Size = new System.Drawing.Size(154, 24);
            this.serverIpAddress.TabIndex = 0;
            // 
            // connectToServerCheckBox
            // 
            this.connectToServerCheckBox.AutoSize = true;
            this.connectToServerCheckBox.Location = new System.Drawing.Point(21, 20);
            this.connectToServerCheckBox.Name = "connectToServerCheckBox";
            this.connectToServerCheckBox.Size = new System.Drawing.Size(84, 16);
            this.connectToServerCheckBox.TabIndex = 1;
            this.connectToServerCheckBox.Text = "连接服务器";
            this.connectToServerCheckBox.UseVisualStyleBackColor = true;
            this.connectToServerCheckBox.CheckedChanged += new System.EventHandler(this.OnConnectServerChanged);
            // 
            // btnSendToServer
            // 
            this.btnSendToServer.Location = new System.Drawing.Point(104, 113);
            this.btnSendToServer.Name = "btnSendToServer";
            this.btnSendToServer.Size = new System.Drawing.Size(75, 23);
            this.btnSendToServer.TabIndex = 2;
            this.btnSendToServer.Text = "Send";
            this.btnSendToServer.UseVisualStyleBackColor = true;
            this.btnSendToServer.Click += new System.EventHandler(this.OnBtnSendToServer);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 345);
            this.Controls.Add(this.btnSendToServer);
            this.Controls.Add(this.connectToServerCheckBox);
            this.Controls.Add(this.serverIpAddress);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IPInputControl.Ctrl.IPInput serverIpAddress;
        private System.Windows.Forms.CheckBox connectToServerCheckBox;
        private System.Windows.Forms.Button btnSendToServer;
    }
}

