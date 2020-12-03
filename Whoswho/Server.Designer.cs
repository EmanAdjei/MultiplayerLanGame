namespace Whoswho
{
    partial class Server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlServer = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.listboxAvaPlayers = new System.Windows.Forms.ListBox();
            this.btnSpectate = new System.Windows.Forms.Label();
            this.btnBanPlayer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddP = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblAvaPlayers = new System.Windows.Forms.Label();
            this.lblClientsConnected = new System.Windows.Forms.Label();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.pnlServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlServer
            // 
            this.pnlServer.Controls.Add(this.txtMessage);
            this.pnlServer.Controls.Add(this.listBoxStatus);
            this.pnlServer.Controls.Add(this.listboxAvaPlayers);
            this.pnlServer.Controls.Add(this.btnSpectate);
            this.pnlServer.Controls.Add(this.btnBanPlayer);
            this.pnlServer.Controls.Add(this.label1);
            this.pnlServer.Controls.Add(this.btnAddP);
            this.pnlServer.Controls.Add(this.lblStatus);
            this.pnlServer.Controls.Add(this.lblMessage);
            this.pnlServer.Controls.Add(this.lblAvaPlayers);
            this.pnlServer.Controls.Add(this.lblClientsConnected);
            this.pnlServer.Controls.Add(this.lblServerPort);
            this.pnlServer.Controls.Add(this.lblServerIP);
            this.pnlServer.Controls.Add(this.lblServer);
            this.pnlServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServer.Location = new System.Drawing.Point(0, 0);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(1284, 661);
            this.pnlServer.TabIndex = 4;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(65, 186);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(375, 20);
            this.txtMessage.TabIndex = 10;
            // 
            // listboxAvaPlayers
            // 
            this.listboxAvaPlayers.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxAvaPlayers.FormattingEnabled = true;
            this.listboxAvaPlayers.ItemHeight = 18;
            this.listboxAvaPlayers.Location = new System.Drawing.Point(928, 98);
            this.listboxAvaPlayers.Name = "listboxAvaPlayers";
            this.listboxAvaPlayers.Size = new System.Drawing.Size(296, 472);
            this.listboxAvaPlayers.TabIndex = 9;
            this.listboxAvaPlayers.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ListboxAvaPlayers_Format);
            // 
            // btnSpectate
            // 
            this.btnSpectate.BackColor = System.Drawing.Color.DarkGray;
            this.btnSpectate.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpectate.ForeColor = System.Drawing.Color.Black;
            this.btnSpectate.Location = new System.Drawing.Point(549, 359);
            this.btnSpectate.Name = "btnSpectate";
            this.btnSpectate.Size = new System.Drawing.Size(223, 49);
            this.btnSpectate.TabIndex = 7;
            this.btnSpectate.Text = "Spectate";
            this.btnSpectate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBanPlayer
            // 
            this.btnBanPlayer.BackColor = System.Drawing.Color.DarkGray;
            this.btnBanPlayer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBanPlayer.ForeColor = System.Drawing.Color.Black;
            this.btnBanPlayer.Location = new System.Drawing.Point(549, 305);
            this.btnBanPlayer.Name = "btnBanPlayer";
            this.btnBanPlayer.Size = new System.Drawing.Size(223, 49);
            this.btnBanPlayer.TabIndex = 7;
            this.btnBanPlayer.Text = "Ban Player";
            this.btnBanPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBanPlayer.Click += new System.EventHandler(this.BtnBanPlayer_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(965, 590);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 49);
            this.label1.TabIndex = 7;
            this.label1.Text = "Close";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.ServerClose);
            // 
            // btnAddP
            // 
            this.btnAddP.BackColor = System.Drawing.Color.DarkGray;
            this.btnAddP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddP.ForeColor = System.Drawing.Color.Black;
            this.btnAddP.Location = new System.Drawing.Point(549, 251);
            this.btnAddP.Name = "btnAddP";
            this.btnAddP.Size = new System.Drawing.Size(223, 49);
            this.btnAddP.TabIndex = 7;
            this.btnAddP.Text = "Check";
            this.btnAddP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddP.Click += new System.EventHandler(this.CheckUDPActivity);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.DarkGray;
            this.lblStatus.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(65, 251);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(375, 49);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.DarkGray;
            this.lblMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(65, 134);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(375, 49);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvaPlayers
            // 
            this.lblAvaPlayers.BackColor = System.Drawing.Color.DarkGray;
            this.lblAvaPlayers.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvaPlayers.ForeColor = System.Drawing.Color.Black;
            this.lblAvaPlayers.Location = new System.Drawing.Point(928, 49);
            this.lblAvaPlayers.Name = "lblAvaPlayers";
            this.lblAvaPlayers.Size = new System.Drawing.Size(296, 49);
            this.lblAvaPlayers.TabIndex = 3;
            this.lblAvaPlayers.Text = "Available Players";
            this.lblAvaPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClientsConnected
            // 
            this.lblClientsConnected.AutoSize = true;
            this.lblClientsConnected.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientsConnected.Location = new System.Drawing.Point(96, 86);
            this.lblClientsConnected.Name = "lblClientsConnected";
            this.lblClientsConnected.Size = new System.Drawing.Size(226, 28);
            this.lblClientsConnected.TabIndex = 0;
            this.lblClientsConnected.Text = ":ClientsConnected";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerPort.Location = new System.Drawing.Point(534, 86);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(69, 28);
            this.lblServerPort.TabIndex = 0;
            this.lblServerPort.Text = ":Port";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerIP.Location = new System.Drawing.Point(534, 49);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(44, 28);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = ":IP";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(95, 34);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(104, 32);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxStatus.FormattingEnabled = true;
            this.listBoxStatus.ItemHeight = 15;
            this.listBoxStatus.Location = new System.Drawing.Point(65, 304);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.Size = new System.Drawing.Size(375, 319);
            this.listBoxStatus.TabIndex = 9;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.pnlServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.pnlServer.ResumeLayout(false);
            this.pnlServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.ListBox listboxAvaPlayers;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblAvaPlayers;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblClientsConnected;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label btnSpectate;
        private System.Windows.Forms.Label btnBanPlayer;
        private System.Windows.Forms.Label btnAddP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxStatus;
    }
}