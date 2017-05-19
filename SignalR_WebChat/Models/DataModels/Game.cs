using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Game
    {
        public Game()
        {
            Team1 = new Team();
            Team2 = new Team();
        }
        public string GameId { get; set; }
        public AppUser Player1 { get; set; }
        public AppUser Player2 { get; set; }
        public AppUser Player3 { get; set; }
        public AppUser Player4 { get; set; }

        public Team Team1 { get; set; }

        public Team Team2 { get; set; }

        public int Bid { get; set; }
        public Suit TrumpSuit { get; set; }
        public Meld Meld { get; set; }
    }
}