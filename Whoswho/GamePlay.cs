using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Net;
using System.Net.Sockets;

namespace Whoswho
{

    public partial class GamePlay : Form, IMessageManager
    {
        // Socket for TCP Connection and reference for server EndPoint
        private TCPConnection tcpSocket;
        private EndPoint remoteEndPoint;

        // List of Pictureboxes the imgaes will populate
        private List<PictureBox> characters = new List<PictureBox>();
        private static String userCharacter;
        private String peopleSet;
        private bool guessing = false;

        // Controls for the chat function
        private Panel chatPanel;
        private ListBox chatContents;
        private TextBox messasgeTextBox;
        private int unreadMessages = 0;

        // Timers of turns, duration and activity checking
        private System.Timers.Timer totalTimer, turnTimer;
        private System.Windows.Forms.Timer activityTimer;
        private int minTotal, secTotal;
        private int secRemaining = 30;

        public GamePlay(String mode, EndPoint serverEP)
        {
            InitializeComponent();
            peopleSet = mode;
            remoteEndPoint = serverEP;
            // Maake a TCP conenection to the server
            SetTCPConnection(remoteEndPoint);
                        
            // Set label to players name as defined in the main menu class 
            lblPlayerName1.Text = MainMenu.player.PlayerName;
            
            // Prevent any early user input
            DisableControls();
            
            // Prompt user to chose their character selection with flashing text
            lblTimer.Start();

            // Create chat panel
            CreateChatPanel();
        }

        private void SetTCPConnection(EndPoint serverEP)
        {
            // Start TCP Socket
            tcpSocket = new TCPConnection();
            tcpSocket.connectionSocket.Blocking = true;
            try
            {
                tcpSocket.connectionSocket.Connect(serverEP);
                tcpSocket.connectionSocket.Blocking = false;
            }
            catch
            {
            }
            TimerSettings();
        }

        private void DisableControls()
        {
            // Initially disable all controls
            btnYes.Visible = false;
            btnNo.Visible = false;

            btnAskQ.Visible = false;
            btnGuess.Visible = false;

            txtQuestion.Enabled = false;
        }

        private void CreateChatPanel()
        {
            // Main chat panel to show and hide
            chatPanel = new Panel
            {
                Size = new Size(400, 340),
                Location = new Point(500, 100),
                BorderStyle = BorderStyle.None,
            };
            
            // Shows record of conversation
            chatContents = new ListBox
            {
                Location = new Point(10, 60),
                Font = new Font("Arial", 14.25f, FontStyle.Bold),
                Size = new Size(380, 240),
                BorderStyle = BorderStyle.None
            };
            chatPanel.Controls.Add(chatContents);

            // Message to be sent to opponent
            messasgeTextBox = new TextBox {
                Location = new Point(10, 305),
                Font = new Font("Arial", 14.25f, FontStyle.Bold),
                Size = new Size(260, 40),
                BorderStyle = BorderStyle.None
            };
            chatPanel.Controls.Add(messasgeTextBox);

            // Button to hide panel
            Button hide = new Button
            {
                Text = "Hide",
                TextAlign = ContentAlignment.MiddleCenter,
                //Font = new Font("Arial", 14.25f, FontStyle.Bold),
                Location = new Point(280, 10),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
            };
            hide.FlatAppearance.BorderSize = 0;
            hide.Click += new EventHandler((sender, e) => chatPanel.Hide());
            chatPanel.Controls.Add(hide);

            // Button to send message to opponent
            Button send = new Button
            {
                Text = "Send",
                TextAlign = ContentAlignment.MiddleCenter,
               // Font = new Font("Arial", 14.25f, FontStyle.Bold),
                Location = new Point(280, 300),
                Size = new Size(100, 30),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
            };
            send.FlatAppearance.BorderSize = 0;
            send.Click += new EventHandler((sender, e) => SendGameMessage());
            chatPanel.Controls.Add(send);

            // Add chat panel to game form
            gamePnl.Controls.Add(chatPanel);
            chatPanel.BringToFront();
            chatPanel.Hide();
        }

        private void SendGameMessage()
        {
            // Add text to chatbox
            //chatContents.Items.Add($"{MainMenu.player.PlayerName}: {messasgeTextBox.Text}"); 

            // Send message to server to relay to opponent
            try
            {
                GameMessageStruct gameMessage = new GameMessageStruct {
                    messageType = 5,
                    playerName = MainMenu.player.PlayerName,
                    text = messasgeTextBox.Text
                };
                byte[] data = PDU.SerializeStruct(gameMessage);

                tcpSocket.connectionSocket.Send(data, SocketFlags.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            messasgeTextBox.Clear();
        }

        private void TurnControls()
        {
            // Only AskQ and Guess buttons visible
            btnYes.Visible = false;
            btnNo.Visible = false;

            btnAskQ.Visible = true;
            btnGuess.Visible = true;

            txtQuestion.Enabled = true;
        }

        private void ResponseControls()
        {
            // Only response buttons visible
            btnYes.Visible = true;
            btnNo.Visible = true;

            btnAskQ.Visible = false;
            btnGuess.Visible = false;

            txtQuestion.Enabled = false;
        }
        
        private void TimerSettings()
        {
            // All timers set up, only the activity timer starts
            totalTimer = new System.Timers.Timer(1000);
            totalTimer.Elapsed += UpdateTotalTime;
            turnTimer = new System.Timers.Timer(1000);
            turnTimer.Elapsed += UpdateTurnTime;

            activityTimer = new System.Windows.Forms.Timer();
            activityTimer.Tick += new EventHandler(CheckActivity);
            activityTimer.Interval = 100;
            activityTimer.Enabled = false;
            activityTimer.Start();

        }

        private void CheckActivity(object sender, EventArgs e)
        {
            // Check buffer for TCP messages
            try
            {
                EndPoint localEndPoint = remoteEndPoint;
                byte[] ReceiveBuffer = new byte[1024];
                int iReceiveByteCount = tcpSocket.connectionSocket.ReceiveFrom(ReceiveBuffer, ref localEndPoint);
                byte[] data = new byte[iReceiveByteCount];
                Array.Copy(ReceiveBuffer, data, iReceiveByteCount); //make sure byte[] is the same length as the recieved byte[]

                // If there is a message deserialize it and pass it into the TCP Message Manager
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
            catch 
            {
            }            
        }

        public void TCPMessageManager(GameMessageStruct gameMessage)
        {
            // Based on the message type trigger different methods
            switch (gameMessage.messageType)
            {
                case 0:
                    // Display characters based on servers list of names
                    DisplayPeople(peopleSet, gameMessage.nameslist);
                    break;
                case 1:
                    // Decide which players turn
                    if (gameMessage.activeTurn == true)
                    {
                        TurnControls();
                        turnTimer.Start();
                    }
                    totalTimer.Start();
                    break;
                case 2:
                    // Player ran out of time
                    MessageBox.Show("Opponent ran out of time");
                    TurnControls();
                    StartTurn();
                    break;
                case 3:
                    // Question Recieved
                    txtQuestion.Text = gameMessage.text;
                    ResponseControls();
                    StartTurn();
                    break;
                case 4:
                    // Guess made
                    if (gameMessage.guess == "win")
                    {
                        MessageBox.Show("You Guessed Right, You Win!!");
                        // Shut down timer, close socket and finally close the form
                        CloseGame();
                    }
                    else if (gameMessage.guess == "lose")
                    {
                        MessageBox.Show($"{MainMenu.opp.PlayerName} guessed Right, You Lose!!");
                        CloseGame();
                    }
                    else if (gameMessage.guess == "youwrong")
                    {
                        MessageBox.Show("Wrong guess");
                        DisableControls();
                    }
                    else if (gameMessage.guess == "theywrong")// wrong guess
                    {
                        MessageBox.Show($"{MainMenu.opp.PlayerName} guessed wrong");
                        StartTurn();
                    }
                    break;
                case 5:
                    // Display characters based on servers list of names
                    if (gameMessage.response)
                    {
                        MessageBox.Show("YES");
                    }
                    else
                    {
                        MessageBox.Show("NO");
                    }
                    break;
                case 6:
                    // Receive chat message
                    chatContents.Items.Add($"{gameMessage.playerName} : {gameMessage.text}");
                    unreadMessages++;
                    btnChat.Text = $"Chat({unreadMessages})";
                    break;
            }
        }

        private void DisplayPeople(String mode, String namesList)
        {
            // Split names into array and assign pictures accordingly
            String[] namesArray = namesList.Split('*');
            String folderPath = Directory.GetCurrentDirectory() + $"\\{mode}\\";
            foreach (PictureBox p in pnlGameScreen.Controls)
            {
                characters.Add(p);
            }

            // Foreach character add the right image and name as a tag
            for (int i = 0; i < 20; i++)
            {
                characters[i].Image = Image.FromFile(folderPath + $"{namesArray[i]}.JPG");
                characters[i].Tag = namesArray[i];
                characters[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }
            ActivateCharEvent();
        }

        private void ActivateCharEvent()
        {
            // Foreach picture add the SelectedChar method
            foreach (PictureBox p in characters)
            {
                p.Click += new EventHandler((sender, e) => SelectedChar(p));
            }
        }

        private void SelectedChar(PictureBox picbox)
        {
            // Get the image name
            string imageName = picbox.Tag.ToString();
            string grey = "Grey";

            // Choose a character to be your choice
            if (pbSelected.Image == null)
            {
                pbSelected.Image = picbox.Image;
                userCharacter = picbox.Tag.ToString();

                Console.WriteLine($"{MainMenu.player.PlayerName} has selected {userCharacter} as their charcter");
                lblTimer.Stop();
                lblSelectCharacter.Dispose();

                // Sent selected choice to server
                try
                {
                    GameMessageStruct gameMessage = new GameMessageStruct
                    {
                        messageType = 0,
                        playerName = MainMenu.player.PlayerName,
                        characterChoice = userCharacter
                    };

                    byte[] data = PDU.SerializeStruct(gameMessage);

                    tcpSocket.connectionSocket.Send(data, SocketFlags.None);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }           
                
            }
            // Make a guess
            else if (guessing && imageName.Contains(grey) == false)
            {
                DialogResult result = MessageBox.Show($"You have guessed {imageName}", "YOU HAVE MADE A GUESS!!!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Send guess to server
                    try
                    {
                        GameMessageStruct gameMessage = new GameMessageStruct
                        {
                            messageType = 3,
                            playerName = MainMenu.player.PlayerName,
                            guess = imageName
                        };

                        byte[] data = PDU.SerializeStruct(gameMessage);

                        tcpSocket.connectionSocket.Send(data, SocketFlags.None);

                        txtQuestion.Clear();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    BtnGuess_Click(new object(), new EventArgs());
                }
                else
                {
                    BtnGuess_Click(new object(), new EventArgs());
                }
            }
            // Toggle Grayscale image
            else
            {                
                if (imageName.Contains(grey) == false)
                {
                    picbox.Image = Image.FromFile(Directory.GetCurrentDirectory() + $"\\{peopleSet}\\{picbox.Tag}{grey}.JPG");
                    picbox.Tag = $"{imageName}{grey}";
                }
                else
                {
                    if (guessing == false)
                    {
                        picbox.Tag = imageName.Remove(imageName.IndexOf(grey), grey.Length);
                        picbox.Image = Image.FromFile(Directory.GetCurrentDirectory() + $"\\{peopleSet}\\{picbox.Tag}.JPG");
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Flash the "Select Character Text"
            if (lblSelectCharacter.ForeColor != Color.Black)
            {
                lblSelectCharacter.ForeColor = Color.Black;
                lblTimer.Interval = 600;
            }
            else
            {
                lblSelectCharacter.ForeColor = Color.FromArgb(240, 240, 240);
                lblTimer.Interval = 400;
            }
        }

        private void UpdateTotalTime(object sender, ElapsedEventArgs e)
        {
            // Shows elasped time the game has lasted
            try
            {
                Invoke(new Action(() =>
                {
                    secTotal += 1;
                    if (secTotal == 60)
                    {
                        secTotal = 0;
                        minTotal += 1;
                    }
                    if (minTotal == 60)
                    {
                        // Close Game and set player status to available
                        MessageBox.Show("Game Time Expired");
                        try
                        {
                            GameMessageStruct gameMessage = new GameMessageStruct {
                                messageType = 6,
                                playerName = MainMenu.player.PlayerName,
                                playerStatus = 0,
                            };

                            byte[] data = PDU.SerializeStruct(gameMessage);

                            tcpSocket.connectionSocket.Send(data, SocketFlags.None);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        BtnLeave_Click(sender, e);
                    }
                    // Format timer
                    lblTotalTimer.Text = string.Format("{0}:{1}", minTotal.ToString().PadLeft(2, '0'), secTotal.ToString().PadLeft(2, '0'));
                }
            ));
            }
            catch
            {
            }
        }

        private void UpdateTurnTime(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                // Shows time remaining for player's turn
                secRemaining -= 1;
                if (secRemaining == 0)
                {
                    // Player ran out of time
                    try
                    {
                        GameMessageStruct gameMessage = new GameMessageStruct {
                            messageType = 1,
                            playerName = MainMenu.player.PlayerName
                        };

                        byte[] data = PDU.SerializeStruct(gameMessage);

                        tcpSocket.connectionSocket.Send(data, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }                    
                    DisableControls();
                    turnTimer.Stop();
                }
                if (secRemaining <= 10)
                {
                    lblTurnTimer.ForeColor = Color.Red;
                }
                else
                {
                    lblTurnTimer.ForeColor = Color.Black;
                }
                lblTurnTimer.Text = string.Format("00:{0}", secRemaining.ToString().PadLeft(2, '0'));
            }
            ));
        }

        private void StartTurn()
        {
            // Reset the time the player has and start the turn timer
            secRemaining = 30;
            TurnControls();
            turnTimer.Start();
        }

        private void BtnLeave_Click(object sender, EventArgs e)
        {
            CloseGame();
        }

        private void CloseGame()
        {
            // Shut down timer, close socket and finally close the form
            totalTimer.Dispose();
            turnTimer.Dispose();
            tcpSocket.CloseSocket();
            Close();
        }

        private void BtnChat_Click(object sender, EventArgs e)
        {
            // Show chat panel 
            chatPanel.Show();
            unreadMessages = 0;
            btnChat.Text = $"Chat({unreadMessages})";
        }

        private void BtnAskQ_Click (object sender, EventArgs e)
        {
            // Send players question to server to relay to opponent
            try
            {
                GameMessageStruct gameMessage = new GameMessageStruct {
                    messageType = 2,
                    playerName = MainMenu.player.PlayerName,
                    text = txtQuestion.Text
                };

                byte[] data = PDU.SerializeStruct(gameMessage);

                tcpSocket.connectionSocket.Send(data, SocketFlags.None);

                txtQuestion.Clear();
                DisableControls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            // Response to question received sent back
            try
            {
                GameMessageStruct gameMessage = new GameMessageStruct
                {
                    messageType = 4,
                    playerName = MainMenu.player.PlayerName,
                    response = true
                };

                byte[] data = PDU.SerializeStruct(gameMessage);

                tcpSocket.connectionSocket.Send(data, SocketFlags.None);
                DisableControls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            // Response to question received sent back
            try
            {
                GameMessageStruct gameMessage = new GameMessageStruct
                {
                    messageType = 4,
                    playerName = MainMenu.player.PlayerName,
                    response = false
                };

                byte[] data = PDU.SerializeStruct(gameMessage);

                tcpSocket.connectionSocket.Send(data, SocketFlags.None);
                DisableControls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BtnGuess_Click(object sender, EventArgs e)
        {
            // Criteria to activate guessing option
            if (btnGuess.BackColor == Color.DarkGray)
            {
                btnGuess.BackColor = Color.Turquoise;
            }
            else
            {
                btnGuess.BackColor = Color.DarkGray;
            }
            guessing = !guessing;
        }

        public void UDPMessageManager(String[] message)
        {
            // inherited from IMessagesManager, but unneeded
        }
    }
}
