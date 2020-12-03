using System;
using System.Net.Sockets;

namespace Whoswho
{
    class GameManagement
    {
        // Game info stored
        private String player1Choice;
        private String player2Choice;

        private String player1Name;
        private String player2Name;

        private bool playerTurn;
        private int round;

        // Names to produce game images
        private Names name = new Names();

        public string Player1Choice { get => player1Choice; set => player1Choice = value; }
        public string Player2Choice { get => player2Choice; set => player2Choice = value; }    

        public GameManagement()
        {
            // Start the round at 0
            round = 0;

            // Randomly choose who goes first
            Random rand = new Random();
            if (rand.NextDouble() > 0.5)
            {
                playerTurn = true;
            }
            else
            {
                playerTurn = false;
            }
        }
        
        public String DisplayPeople()
        {
            // Get produce a list a 20 names to send to both players
            String[] gameNames = new String[20];
            Random nameNum = new Random();
            for (int i = 0; i < 20; i++)
            {
                int numberchoosen = nameNum.Next(0, name.NameCharacter.Count);
                gameNames[i] = name.NameCharacter[numberchoosen];
                name.NameCharacter.RemoveAt(numberchoosen);
            }
            
            String gameNamesSingleString = String.Join("*", gameNames);

            return gameNamesSingleString;
        }

        public void StoreCharacterChoice(String characterChoice, String name, Socket[] players)
        {
            // If the first choice is empty store character choice here
            if (player1Choice == null)
            {
                player1Choice = characterChoice;
                player1Name = name;
            }
            else
            {
                // Store second players choice here
                Player2Choice = characterChoice;
                player2Name = name;

                // Start game
                GameMessageStruct gameMessage1 = new GameMessageStruct
                {
                    messageType = 1,
                    activeTurn = playerTurn
                };
                byte[] data1 = PDU.SerializeStruct(gameMessage1);
                
                GameMessageStruct gameMessage2 = new GameMessageStruct {
                    messageType = 1,
                    activeTurn = !playerTurn
                };
                byte[] data2 = PDU.SerializeStruct(gameMessage2);

                // Send both players game starring order
                players[0].Send(data1, SocketFlags.None);
                players[1].Send(data2, SocketFlags.None);
            }
        }

        public Socket TurnSelector(String name, Socket[] players)
        {
            // Determines whose turn it is
            if (round % 2 == 0)
            {
                return players[1];
            }
            else
            {
                return players[0];
            }            
        }

        public void RanOutOfTime(String name, Socket[] players)
        {
            // Start the other players turn
            GameMessageStruct gameMessage = new GameMessageStruct {
                // No other field needed
                messageType = 2
            };
            byte[] data = PDU.SerializeStruct(gameMessage);
            TurnSelector(name, players).Send(data, SocketFlags.None);
        }

        public void QuestionAsked(String quest, String name, Socket[] players)
        {
            // Relay question asked
            GameMessageStruct gameMessage = new GameMessageStruct {
                // No other field needed
                messageType = 3,
                text = quest
            };
            byte[] data = PDU.SerializeStruct(gameMessage);
            TurnSelector(name, players).Send(data, SocketFlags.None);
        }

        public void GuessMade(String guess, String name, Socket[] players)
        {
            // Check with game info if guess made was correct
            if (name == player1Name)
            {
                if (guess == Player2Choice)
                {
                    GameMessageStruct gameMessage1 = new GameMessageStruct
                    {
                        // No other field needed
                        messageType = 4,
                        text = "win"
                    };
                    byte[] data1 = PDU.SerializeStruct(gameMessage1);
                    players[0].Send(data1, SocketFlags.None);
                    GameMessageStruct gameMessage2 = new GameMessageStruct

                    {
                        // No other field needed
                        messageType = 4,
                        text = "lose"
                    };
                    byte[] data2 = PDU.SerializeStruct(gameMessage2);
                    TurnSelector(name, players).Send(data2, SocketFlags.None);
                }
                else
                {
                    GameMessageStruct gameMessage1 = new GameMessageStruct
                    {
                        // No other field needed
                        messageType = 4,
                        text = "youwrong"
                    };
                    byte[] data1 = PDU.SerializeStruct(gameMessage1);
                    players[0].Send(data1, SocketFlags.None);
                    GameMessageStruct gameMessage2 = new GameMessageStruct

                    {
                        // No other field needed
                        messageType = 4,
                        text = "theywrong"
                    };
                    byte[] data2 = PDU.SerializeStruct(gameMessage2);
                    TurnSelector(name, players).Send(data2, SocketFlags.None);
                }
                
            }
            else if (name == player2Name)
            {
                if (guess == Player1Choice)
                {
                    GameMessageStruct gameMessage1 = new GameMessageStruct
                    {
                        // No other field needed
                        messageType = 4,
                        text = "win"
                    };
                    byte[] data1 = PDU.SerializeStruct(gameMessage1);
                    players[1].Send(data1, SocketFlags.None);
                    GameMessageStruct gameMessage2 = new GameMessageStruct

                    {
                        // No other field needed
                        messageType = 4,
                        text = "lose"
                    };
                    byte[] data2 = PDU.SerializeStruct(gameMessage2);
                    TurnSelector(name, players).Send(data2, SocketFlags.None);
                }
                else
                {
                    GameMessageStruct gameMessage1 = new GameMessageStruct
                    {
                        // No other field needed
                        messageType = 4,
                        text = "youwrong"
                    };
                    byte[] data1 = PDU.SerializeStruct(gameMessage1);
                    players[1].Send(data1, SocketFlags.None);
                    GameMessageStruct gameMessage2 = new GameMessageStruct

                    {
                        // No other field needed
                        messageType = 4,
                        text = "theywrong"
                    };
                    byte[] data2 = PDU.SerializeStruct(gameMessage2);
                    TurnSelector(name, players).Send(data2, SocketFlags.None);
                }

            }            
        }

        public void ResponseMade(bool yesNo, String name, Socket[] players)
        {
            // Relay response
            GameMessageStruct gameMessage = new GameMessageStruct
            {
                // No other field needed
                messageType = 5,
                response = yesNo
            };
            byte[] data = PDU.SerializeStruct(gameMessage);
            TurnSelector(name, players).Send(data, SocketFlags.None);
        }

        public void ChatMessageSent(String textMessage, String name, Socket[] players)
        {
            // Send received chat message to both players
            GameMessageStruct gameMessage = new GameMessageStruct {
                messageType = 6,
                playerName = name,
                text = textMessage
            };
            byte[] data = PDU.SerializeStruct(gameMessage);
            foreach (Socket item in players)
            {
                item.Send(data, SocketFlags.None);
            }
        }
    }
}
