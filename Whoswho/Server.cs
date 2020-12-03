using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;

namespace Whoswho
{
    public partial class Server : Form, IMessageManager
    {
        // Server need both a UDP and TCP Socket
        private UDPConnection udpSocket;
        private TCPConnection tcpSocket;

        // Used to constuct player objects
        private IPEndPoint playerEP;

        // EndPoint to send to detected clients
        private EndPoint serverEP;
        private byte[] serverDetails;

        private const int maxPlayersNumber = 10;
        private int playerNumber = 0;

        // Master list of connected clients
        private BindingList<Player> players = new BindingList<Player>();

        // List of banned Endpoints to reject connections
        private List<IPEndPoint> bannedPlayerEndPoints = new List<IPEndPoint>();

        // Keeps state and flow of game
        private GameManagement gameManagement = new GameManagement();

        // Game sockets
        private Socket[] currentGame = new Socket[2];
        private bool gameStarted = false;

        private int clientsConnected;

        // Thread Safe timers to check for UDP and TCP activity
        private static System.Windows.Forms.Timer activityTimerUDP;
        private static System.Windows.Forms.Timer activityTimerTCP;


        public Server()
        {
            InitializeComponent();

            // Gets local IP address
            IPAddress serverIPAddress = Connection.GetLocalIP(); 
            lblServerIP.Text = $"IP: {serverIPAddress}";
            
            // Default port number, no option to change
            lblServerPort.Text = "Ports: 8008 and 8009";  
            clientsConnected = 0;
            lblClientsConnected.Text = $"Clients Connected: {clientsConnected}";

            // Setup Sockects
            udpSocket = new UDPConnection();
            udpSocket.BindPort("8008");

            tcpSocket = new TCPConnection();
            tcpSocket.BindPort("8009");
            tcpSocket.Listen(maxPlayersNumber);

            serverEP = new IPEndPoint(IPAddress.Parse(serverIPAddress.ToString()), Convert.ToInt16("8009"));
            serverDetails = Encoding.ASCII.GetBytes(serverEP.ToString());

            // Make the list of players the data source for the listbox
            listboxAvaPlayers.DataSource = players;
            listboxAvaPlayers.DisplayMember = "playerName";

            StartrequestTimer();
        }

        private void StartrequestTimer()
        {
            // Set up each timers interval and Method to evoke
            activityTimerUDP = new System.Windows.Forms.Timer();
            activityTimerUDP.Tick += new EventHandler(CheckUDPActivity);
            activityTimerUDP.Interval = 500;
            activityTimerUDP.Start();

            activityTimerTCP = new System.Windows.Forms.Timer();
            activityTimerTCP.Tick += new EventHandler(CheckTCPActivity);
            activityTimerTCP.Interval = 100;
            activityTimerTCP.Start();
        }
        
        private void CheckUDPActivity(Object myObject, EventArgs myEventArgs)
        {
            // Check UDP Socket for messages from clients
            try
            {
                EndPoint localEndPoint = udpSocket.endPoint;
                byte[] receiveBuffer = new byte[1024];
                int receiveByteCount = udpSocket.connectionSocket.ReceiveFrom(receiveBuffer, ref localEndPoint);
                byte[] data = new byte[receiveByteCount];

                // if a messgae is received, send to UDPMessageManager
                if (0 < receiveByteCount)
                {
                    try
                    {
                        String[] playerDetails = Encoding.ASCII.GetString(receiveBuffer, 0, receiveByteCount).Split(':');
                        Console.WriteLine("Data Receieved");
                        
                        UDPMessageManager(playerDetails);
                    }
                    catch
                    {
                    }
                }
                // Clear Buffer
                receiveByteCount = 0;
            }
            catch
            {
            }            
        }

        private void CheckTCPActivity(object sender, EventArgs myEventArgs)
        {
            // Check TCP Connections Backlog for connection requests
            try
            {
                if (playerNumber < 2)
                {
                    try
                    {
                        // Add each player as a new socket
                        currentGame[playerNumber] = tcpSocket.connectionSocket.Accept();
                        currentGame[playerNumber].Blocking = false;
                        playerNumber++;

                        if (playerNumber == 2)
                        {
                            gameStarted = true;
                        }
                    }
                    catch (SocketException se) // Handle socket-related exception
                    {   // If an exception occurs, display an error message
                        if (10053 == se.ErrorCode || 10054 == se.ErrorCode) // Remote end closed the connection
                        {
                            tcpSocket.CloseSocket();
                        }
                        else if (10035 != se.ErrorCode)
                        {   // Ignore error messages relating to normal behaviour of non-blocking sockets
                            MessageBox.Show(se.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }                    
                }
                else if (gameStarted == true)
                {
                    // Diagnostics message
                    listBoxStatus.Items.Add("Game Has Started");
                    gameStarted = false;

                    // Send same list of characters to each client
                    GameMessageStruct gameMessage = new GameMessageStruct
                    {
                        messageType = 0,
                        nameslist = gameManagement.DisplayPeople()
                    };

                    byte[] data = PDU.SerializeStruct(gameMessage);

                    foreach (Socket client in currentGame)
                    {
                        client.Send(data, SocketFlags.None);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Then check for game activity
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    EndPoint localEndPoint = tcpSocket.endPoint;
                    byte[] ReceiveBuffer = new byte[1024];
                    int iReceiveByteCount = currentGame[i].ReceiveFrom(ReceiveBuffer, ref localEndPoint);
                    byte[] data = new byte[iReceiveByteCount];
                    Array.Copy(ReceiveBuffer, data, iReceiveByteCount); //make sure byte[] is the same length as the recieved byte[]

                    if (0 < iReceiveByteCount)
                    {
                        try
                        {
                            GameMessageStruct gameMessage = PDU.DeserializeStruct(data); //convert byte[] to structure so it can be read

                            TCPMessageManager(gameMessage);
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine(error.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void GetNumberOfClients()
        {
            lblClientsConnected.Text = $"Clients Connected: {clientsConnected}";
        }

        public void UDPMessageManager(String[] message)
        {
            // Launch a players game as requested
            if (message.Contains("GameOpp"))
            {
                String join = $"Join:{message[0]}";
                byte[] data = Encoding.ASCII.GetBytes(join);
                udpSocket.connectionSocket.SendTo(data, new IPEndPoint(IPAddress.Parse(message[2]), Convert.ToInt16(message[3])));
                Console.WriteLine("{0} sent to {1}", join, message[2]);
            }
            else
            {
                Player player = new Player(message[0], playerEP = new IPEndPoint(IPAddress.Parse(message[1]), Convert.ToInt16(message[2])));
                //If the endpoint is on the ban list call the ban method
                if (bannedPlayerEndPoints.Contains(player.PlayerEndPoint))
                {
                    BannedPlayer(player);
                    // Diagnostics message
                    listBoxStatus.Items.Add("Attempted Connection From Banned Player");
                }
                else
                {
                    try
                    {
                        udpSocket.connectionSocket.SendTo(serverDetails, player.PlayerEndPoint);
                        Console.WriteLine($"{player.PlayerName} sent to {player.PlayerEndPoint}");
                    }
                    // Silently handle any exceptions
                    catch
                    {
                        Console.WriteLine("No message sent");
                    }

                    players.Add(player);
                    clientsConnected++;
                    GetNumberOfClients();
                    // Diagnostics message
                    listBoxStatus.Items.Add($"{player.PlayerName} logged on");
                    UpdateAllConnectedPlayers(players);
                }

            }
            
        }

        public void TCPMessageManager(GameMessageStruct gameMessage)
        {
            switch (gameMessage.messageType)
            {
                case 0:
                    // Start the game
                    gameManagement.StoreCharacterChoice(gameMessage.characterChoice,gameMessage.playerName, currentGame);
                    break;
                case 1:
                    // Player Timer has elapsed
                    gameManagement.RanOutOfTime(gameMessage.playerName, currentGame);
                    break;
                case 2:
                    // Question Received
                    gameManagement.QuestionAsked(gameMessage.text, gameMessage.playerName, currentGame);
                    break;
                case 3:
                    // Guess Received
                    gameManagement.GuessMade(gameMessage.guess, gameMessage.playerName, currentGame);
                    break;
                case 4:
                    // Response to guess
                    gameManagement.ResponseMade(gameMessage.response, gameMessage.playerName, currentGame);
                    break;
                case 5:
                    // Chat messages
                    gameManagement.ChatMessageSent(gameMessage.text, gameMessage.playerName, currentGame);
                    break;
                case 6:
                    // Player Status Change
                    //StatusManager(gameMessage.text, gameMessage.playerName, currentGame);
                    break;
            }
        }

        private void UpdateAllConnectedPlayers(BindingList<Player> players)
        {   
            // Send message to each connected client informing of the total number of connected clients
            String playerDetails = "";

            foreach (Player item in players)
            {
                playerDetails += $"{item.PlayerName}:{item.PlayerEndPoint}*";
            }

            playerDetails = playerDetails.Substring(0, playerDetails.Length - 1);

            byte[] byData = Encoding.ASCII.GetBytes(playerDetails);

            foreach (Player client in players)
            {
                try
                {
                    udpSocket.connectionSocket.SendTo(byData, client.PlayerEndPoint);
                    Console.WriteLine($"{client.PlayerName} sent Update Message");
                }
                // Silently handle any exceptions
                catch
                {
                    Console.WriteLine("No message sent");
                }
            }
        }        

        private void BtnBanPlayer_Click(object sender, EventArgs e)
        {
            if (listboxAvaPlayers.SelectedIndex < 0)
            {
                MessageBox.Show("Select Player to Ban");
            }
            else
            {
                //string cheater = listboxAvaPlayers.GetItemText(listboxAvaPlayers.SelectedItem);
                string cheater = ((Player)listboxAvaPlayers.SelectedItem).PlayerName;
                DialogResult result = MessageBox.Show($"Do you want to ban {((Player)listboxAvaPlayers.SelectedItem).PlayerName}?", "Ban Player", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Player bannedPlayer = (Player)listboxAvaPlayers.SelectedItem;

                    bannedPlayerEndPoints.Add((IPEndPoint)bannedPlayer.PlayerEndPoint);

                    BannedPlayer(bannedPlayer);

                    players.Remove(bannedPlayer);
                    if (players.Count > 0)
                    {
                        UpdateAllConnectedPlayers(players);
                    }

                    clientsConnected--;
                    GetNumberOfClients();
                }
            }
        }

        private void BannedPlayer(Player bannedPlayer)
        {
            String bannedNote = "You are Banned";
            byte[] banData = Encoding.ASCII.GetBytes(bannedNote);

            // send ban message to specific player
            try
            {
                udpSocket.connectionSocket.SendTo(banData, bannedPlayer.PlayerEndPoint);
                listBoxStatus.Items.Add($"{bannedPlayer.PlayerName} Banned!!");
            }
            // Silently handle any exceptions
            catch
            {
            }
        }

        private void ListboxAvaPlayers_Format(object sender, ListControlConvertEventArgs e)
        {
            // Displays the available players names and status
            string playerName = ((Player)e.ListItem).PlayerName.ToString();
            string status = ((Player)e.ListItem).Status.ToString();

            e.Value = playerName + "     -     (" + status + ")";
        }

        private void ServerClose(object sender, EventArgs e)
        {
            udpSocket.CloseSocket();
            tcpSocket.CloseSocket();
            Application.Exit();
        }
    }
}
