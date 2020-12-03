using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Whoswho
{
    public partial class MainMenu : Form, IMessageManager
    {
        // Socket initailsed as UDP
        private UDPConnection udpSocket;

        // Game player info for chat form
        public static Player player;
        public static Player opp;

        // Used to create player objects
        private IPEndPoint playerEP;

        // Server EndPoint details
        private EndPoint serverTCPEndPoint;
        private EndPoint serverUDPEndPoint;

        // Thread safe timer
        private static System.Windows.Forms.Timer requestTimer;

        // List of players in the lobby updated from server
        private BindingList<Player> players = new BindingList<Player>();
        
        public MainMenu()
        {
            InitializeComponent();
            StartGUI();
            // Give default value which user can change
            txtPort.Text = "8002";
            // Preselect normal mode to play on
            rbNormal.Select();
        }

        private void StartGUI()
        {
            MaximizeBox = false;
            btnStart.BackColor = Color.FromArgb(37, 104, 170);
            btnStart.ForeColor = Color.FromArgb(255, 255, 255);

            // Setting up list box to actively display player list
            listboxAvaPlayers.DataSource = players;
            listboxAvaPlayers.DisplayMember = "playerName";
        }

        private void EmptyError()
        {
            // Checks the user has entered the right values for name and port
            if ((string.IsNullOrWhiteSpace(txtPlayerName.Text)))
            {
                MessageBox.Show("Please Enter your Player Name!");
            }
            else if (!(Convert.ToInt32(txtPort.Text) >= 8000 && (Convert.ToInt32(txtPort.Text) <= 8009)))
            {
                MessageBox.Show("Please Enter the Port Number Between 8000 and 8009");
            }
            else
            {
                // Set up UDP Socket and bind to users choice
                udpSocket = new UDPConnection();
                udpSocket.BindPort(txtPort.Text);

                // Create a new Player object for user
                player = new Player(txtPlayerName.Text, playerEP = new IPEndPoint(Connection.GetLocalIP(), Convert.ToInt16(txtPort.Text, 10)));

                // Start checking for recevied messages and broadcast the users details
                StartrequestTimer();
                SendPlayerDetails(player);

                // Set the pnlGameSettins text
                lblPlayerName.Text = $"{player.PlayerName} : {player.Status}";
                lblIP.Text = "IP: " + Connection.GetLocalIP();
                lblPort2.Text = "Port: " + txtPort.Text;
                listBoxLobby.Items.Add(player.PlayerName);
                GetNumberOfPlayers();
                btnRemoveP.Enabled = false;
                pnlGameSetting.Visible = true;
            }
        }

        private void SendPlayerDetails(Player player)
        {
            try
            {
                // Create a string from the users player details and broadcast
                String playerDetails = $"{player.PlayerName}:{player.PlayerEndPoint}";
                if (playerDetails.Equals(""))
                {
                    playerDetails = "error";
                }
                byte[] byData = Encoding.ASCII.GetBytes(playerDetails);
                udpSocket.Broadcast(byData, "8008");
                Console.WriteLine("Player data message sent");
            }
            // Silently handle any exceptions
            catch
            {
                Console.WriteLine("No message sent");
            }
        }

        private void StartrequestTimer()
        {
            // Settings for timers to check for received UDP data
            requestTimer = new System.Windows.Forms.Timer(); 
            requestTimer.Tick += new EventHandler(ReceiveUDPData);
            requestTimer.Interval = 500;
            //requestTimer.Enabled = false;
            requestTimer.Start();
        }

        private void GetNumberOfPlayers()
        {
            // Updates the Number of available players for the user 
            //lblAvaPlayers.Text = $"Available Players ({listboxAvaPlayers.Items.Count})";
            lblAvaPlayers.Text = $"Available Players ({players.Count})";
        }

        private void ReceiveUDPData(Object myObject, EventArgs myEventArgs)
        {   
            try
            {
                // Check if a message has been received (is waiting in the buffer)
                EndPoint localEndPoint = udpSocket.endPoint;
                byte[] receiveBuffer = new byte[1024];
                int receiveByteCount = udpSocket.connectionSocket.ReceiveFrom(receiveBuffer, ref localEndPoint);
                byte[] data = new byte[receiveByteCount];

                // A message has been recevied
                if (0 < receiveByteCount)
                {
                    // Put the received message into an array and pass it into the UDPMessage manager 
                    try
                    {
                        String[] receivedData = Encoding.ASCII.GetString(receiveBuffer, 0, receiveByteCount).Split('*');
                        Console.WriteLine("Data Received");
                        
                        UDPMessageManager(receivedData);
                    }
                    catch
                    {
                    }
                }
                receiveByteCount = 0;
            }
            // Catch any errors
            catch
            {
            }
        }

        public void UDPMessageManager(String[] receivedData)
        {
            // Based on the contents of teh message, different actions are taken
            try
            {
                if (receivedData.Contains("You are Banned"))
                {
                    BanPlayer();
                }
                else if (receivedData[0].Contains("8009"))
                {
                    String ep = String.Concat(receivedData);
                    receivedData = ep.Split(':');
                    serverTCPEndPoint = new IPEndPoint(IPAddress.Parse(receivedData[0]), Convert.ToInt16(receivedData[1]));
                    serverUDPEndPoint = new IPEndPoint(IPAddress.Parse(receivedData[0]), Convert.ToInt16("8008"));
                    Console.WriteLine("Server EP received, {0}", ep);
                }
                else if (receivedData[0].Contains("Join"))
                {
                    String[] modeData = receivedData[0].Split(':');
                    new GamePlay(modeData[1], serverTCPEndPoint).Show();
                }
                else
                {
                    players.Clear();

                    // Adds the new list of playewrs received from the server 
                    foreach (String item in receivedData)
                    {
                        string[] person = item.Split(':');
                        Player player = new Player(person[0], playerEP = new IPEndPoint(IPAddress.Parse(person[1]), Convert.ToInt16(person[2])));
                        players.Add(player);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }        
        }

        private void BanPlayer()
        {
            // Automatically shuts down the players connection
            udpSocket.CloseSocket();
            MessageBox.Show("You Have Been Banned");
            Application.Exit();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            EmptyError();
        }

        private void TxtPlayerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void TxtPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                // Calls the EmptyError method after the enter key has been pressed 
                EmptyError();
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (listBoxLobby.Items.Count == 2)
            {
                // If there are two names in the lobbybox start a game in the choosen mode
                String mode = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(radioButton => radioButton.Checked).Text.ToString();

                // Send opponent details to serevr to start their game too
                String oppDetails = $"{mode}:GameOpp:{opp.PlayerEndPoint}";
                if (oppDetails.Equals(""))
                {
                    oppDetails = "error";
                }
                byte[] data = Encoding.ASCII.GetBytes(oppDetails);

                udpSocket.connectionSocket.SendTo(data, serverUDPEndPoint);
                Console.WriteLine("{0} sent to {1}", oppDetails, serverUDPEndPoint);

                new GamePlay(mode, serverTCPEndPoint).Show();
            }
            else
            {
                MessageBox.Show("Select a Player to Play With");
            }

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            EmptyError();
        }

        private void BtnStart_MouseEnter(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.FromArgb(18, 62, 105);
            btnStart.ForeColor = Color.FromArgb(184, 180, 180);
        }

        private void BtnStart_MouseLeave(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.FromArgb(37, 104, 170);
            btnStart.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void BtnAddP_Click(object sender, EventArgs e)
        {
            // if the conditions are right add  an opponent to the game lobby
            if (listboxAvaPlayers.SelectedIndex < 0)
            {
                MessageBox.Show("Select a Player to Play With");
            }
            else if (((Player)listboxAvaPlayers.SelectedItem).PlayerName.Contains(player.PlayerName))                
            {
                MessageBox.Show("You can't compete with yourself!!");
            }
            else if (((Player)listboxAvaPlayers.SelectedItem).Status.Equals(PlayerStatus.InGame))
            {
                MessageBox.Show($"{((Player)listboxAvaPlayers.SelectedItem).PlayerName} is currently in a game");
            }
            else
            {
                listBoxLobby.Items.Add(listboxAvaPlayers.GetItemText(listboxAvaPlayers.SelectedItem));
                opp = (Player)listboxAvaPlayers.SelectedItem;
                // Players.Add(new Player(listboxAvaPlayers.GetItemText(listboxAvaPlayers.SelectedItem).ToUpper(), "8000"));
                //listboxAvaPlayers.Items.Remove(listboxAvaPlayers.SelectedItem);
                btnRemoveP.Enabled = true;
                btnAddP.Enabled = false;
                GetNumberOfPlayers();
            }

        }

        private void BtnRemoveP_Click(object sender, EventArgs e)
        {
            try
            {
                // Removes the opponent from the game lobby
                if ( listBoxLobby.SelectedItem.ToString() != player.PlayerName)
                {
                    listBoxLobby.Items.Remove(listBoxLobby.SelectedItem);
                    btnRemoveP.Enabled = false;
                    btnAddP.Enabled = true;
                    GetNumberOfPlayers();
                    opp = null;
                }
                else
                {
                    MessageBox.Show("Illegal Remove Made");
                }
            }
            catch
            {
                MessageBox.Show("Please Select Player to Remove");
            }

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Focuses on the input for the user's name
            txtPlayerName.Select();
        }

        public void TCPMessageManager(GameMessageStruct gameMessageStruct)
        {
            // Method implemented from IMessage manager Interface, but not needed
        }

        private void ListboxAvaPlayers_Format(object sender, ListControlConvertEventArgs e)
        {
            // Displays the available players names and status
            string playerName = ((Player)e.ListItem).PlayerName.ToString();
            string status = ((Player)e.ListItem).Status.ToString();

            e.Value = playerName + "     -     (" + status + ")";
        }
    }
}
