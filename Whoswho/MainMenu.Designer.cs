namespace Whoswho
{
    partial class MainMenu
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
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlStart = new System.Windows.Forms.Panel();
            this.pnlGameSetting = new System.Windows.Forms.Panel();
            this.listboxAvaPlayers = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWeird = new System.Windows.Forms.RadioButton();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.btnSpectate = new System.Windows.Forms.Label();
            this.btnRemoveP = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Label();
            this.btnAddP = new System.Windows.Forms.Label();
            this.lblLobby = new System.Windows.Forms.Label();
            this.lblAvaPlayers = new System.Windows.Forms.Label();
            this.listBoxLobby = new System.Windows.Forms.ListBox();
            this.lblPort2 = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Label();
            this.pnlStart.SuspendLayout();
            this.pnlGameSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayerName.Location = new System.Drawing.Point(520, 460);
            this.txtPlayerName.MaxLength = 8;
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(263, 44);
            this.txtPlayerName.TabIndex = 1;
            this.txtPlayerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPlayerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPlayerName_KeyDown);
            this.txtPlayerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPlayerName_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(498, 49);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(308, 55);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Who\'s Who?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(477, 413);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(349, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter Player Name Here :";
            // 
            // pnlStart
            // 
            this.pnlStart.Controls.Add(this.pnlGameSetting);
            this.pnlStart.Controls.Add(this.lblTitle);
            this.pnlStart.Controls.Add(this.txtPort);
            this.pnlStart.Controls.Add(this.txtPlayerName);
            this.pnlStart.Controls.Add(this.lblPort);
            this.pnlStart.Controls.Add(this.label1);
            this.pnlStart.Controls.Add(this.btnStart);
            this.pnlStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStart.Location = new System.Drawing.Point(0, 0);
            this.pnlStart.Name = "pnlStart";
            this.pnlStart.Size = new System.Drawing.Size(1284, 661);
            this.pnlStart.TabIndex = 3;
            // 
            // pnlGameSetting
            // 
            this.pnlGameSetting.Controls.Add(this.listboxAvaPlayers);
            this.pnlGameSetting.Controls.Add(this.groupBox1);
            this.pnlGameSetting.Controls.Add(this.btnSpectate);
            this.pnlGameSetting.Controls.Add(this.btnRemoveP);
            this.pnlGameSetting.Controls.Add(this.btnPlay);
            this.pnlGameSetting.Controls.Add(this.btnAddP);
            this.pnlGameSetting.Controls.Add(this.lblLobby);
            this.pnlGameSetting.Controls.Add(this.lblAvaPlayers);
            this.pnlGameSetting.Controls.Add(this.listBoxLobby);
            this.pnlGameSetting.Controls.Add(this.lblPort2);
            this.pnlGameSetting.Controls.Add(this.lblIP);
            this.pnlGameSetting.Controls.Add(this.lblPlayerName);
            this.pnlGameSetting.Location = new System.Drawing.Point(0, 0);
            this.pnlGameSetting.Name = "pnlGameSetting";
            this.pnlGameSetting.Size = new System.Drawing.Size(1284, 661);
            this.pnlGameSetting.TabIndex = 3;
            this.pnlGameSetting.Visible = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWeird);
            this.groupBox1.Controls.Add(this.rbNormal);
            this.groupBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(549, 482);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 138);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character Mode";
            // 
            // rbWeird
            // 
            this.rbWeird.AutoSize = true;
            this.rbWeird.Location = new System.Drawing.Point(59, 81);
            this.rbWeird.Name = "rbWeird";
            this.rbWeird.Size = new System.Drawing.Size(88, 28);
            this.rbWeird.TabIndex = 1;
            this.rbWeird.TabStop = true;
            this.rbWeird.Text = "Weird";
            this.rbWeird.UseVisualStyleBackColor = true;
            // 
            // rbNormal
            // 
            this.rbNormal.AutoSize = true;
            this.rbNormal.Location = new System.Drawing.Point(59, 47);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(103, 28);
            this.rbNormal.TabIndex = 0;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Normal";
            this.rbNormal.UseVisualStyleBackColor = true;
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
            // btnRemoveP
            // 
            this.btnRemoveP.BackColor = System.Drawing.Color.DarkGray;
            this.btnRemoveP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveP.ForeColor = System.Drawing.Color.Black;
            this.btnRemoveP.Location = new System.Drawing.Point(549, 305);
            this.btnRemoveP.Name = "btnRemoveP";
            this.btnRemoveP.Size = new System.Drawing.Size(223, 49);
            this.btnRemoveP.TabIndex = 7;
            this.btnRemoveP.Text = "Remove Player";
            this.btnRemoveP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRemoveP.Click += new System.EventHandler(this.BtnRemoveP_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.DarkGray;
            this.btnPlay.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Location = new System.Drawing.Point(163, 528);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(178, 49);
            this.btnPlay.TabIndex = 7;
            this.btnPlay.Text = "Play";
            this.btnPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
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
            this.btnAddP.Text = "Add Player";
            this.btnAddP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddP.Click += new System.EventHandler(this.BtnAddP_Click);
            // 
            // lblLobby
            // 
            this.lblLobby.BackColor = System.Drawing.Color.DarkGray;
            this.lblLobby.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLobby.ForeColor = System.Drawing.Color.Black;
            this.lblLobby.Location = new System.Drawing.Point(65, 188);
            this.lblLobby.Name = "lblLobby";
            this.lblLobby.Size = new System.Drawing.Size(375, 49);
            this.lblLobby.TabIndex = 5;
            this.lblLobby.Text = "Lobby";
            this.lblLobby.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // listBoxLobby
            // 
            this.listBoxLobby.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLobby.FormattingEnabled = true;
            this.listBoxLobby.ItemHeight = 18;
            this.listBoxLobby.Location = new System.Drawing.Point(65, 237);
            this.listBoxLobby.Name = "listBoxLobby";
            this.listBoxLobby.Size = new System.Drawing.Size(375, 58);
            this.listBoxLobby.TabIndex = 1;
            // 
            // lblPort2
            // 
            this.lblPort2.AutoSize = true;
            this.lblPort2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort2.Location = new System.Drawing.Point(534, 86);
            this.lblPort2.Name = "lblPort2";
            this.lblPort2.Size = new System.Drawing.Size(69, 28);
            this.lblPort2.TabIndex = 0;
            this.lblPort2.Text = ":Port";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(534, 49);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(44, 28);
            this.lblIP.TabIndex = 0;
            this.lblIP.Text = ":IP";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.Location = new System.Drawing.Point(95, 34);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(191, 32);
            this.lblPlayerName.TabIndex = 0;
            this.lblPlayerName.Text = ": PlayerName";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(520, 347);
            this.txtPort.MaxLength = 4;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(263, 44);
            this.txtPort.TabIndex = 1;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPlayerName_KeyDown);
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtPlayerName_KeyPress);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(608, 300);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(86, 32);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port :";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.Font = new System.Drawing.Font("Arial Rounded MT Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(520, 528);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(263, 66);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            this.btnStart.MouseEnter += new System.EventHandler(this.BtnStart_MouseEnter);
            this.btnStart.MouseLeave += new System.EventHandler(this.BtnStart_MouseLeave);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 661);
            this.Controls.Add(this.pnlStart);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Who\'s Who?";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.pnlStart.ResumeLayout(false);
            this.pnlStart.PerformLayout();
            this.pnlGameSetting.ResumeLayout(false);
            this.pnlGameSetting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlStart;
        private System.Windows.Forms.Panel pnlGameSetting;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblAvaPlayers;
        private System.Windows.Forms.Label lblLobby;
        private System.Windows.Forms.ListBox listBoxLobby;
        private System.Windows.Forms.Label btnAddP;
        private System.Windows.Forms.Label btnSpectate;
        private System.Windows.Forms.Label btnRemoveP;
        private System.Windows.Forms.Label btnPlay;
        private System.Windows.Forms.Label btnStart;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWeird;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ListBox listboxAvaPlayers;
        private System.Windows.Forms.Label lblPort2;
        private System.Windows.Forms.TextBox txtPlayerName;
    }
}

