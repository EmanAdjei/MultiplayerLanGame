using System;
using System.Net;

namespace Whoswho
{
    public class Player
    {
        // Ready only fields should not be changed
        private readonly String playerName;        
        private PlayerStatus status;                
        private readonly EndPoint playerEndPoint;       
        
        public string PlayerName => playerName;        
        public PlayerStatus Status { get => status; set => status=value; }
        public EndPoint PlayerEndPoint => playerEndPoint;    

        public Player(String playerName, EndPoint playerEndPoint )
        {
            // Constructor sets status to available by deflault
            this.playerName = playerName;
            this.status = PlayerStatus.Available;
            this.playerEndPoint = playerEndPoint;
            
            Console.WriteLine($"PLayerName: {PlayerName} || EndPoint: {PlayerEndPoint}");
        }

        // Status explicitly defined
        public Player (String playerName, EndPoint playerEndPoint, int status)
        {
            this.playerName = playerName;
            this.status = (PlayerStatus)status;
            this.playerEndPoint = playerEndPoint;

            Console.WriteLine($"PLayerName: {PlayerName} || EndPoint: {PlayerEndPoint}");
        }

    }
}
