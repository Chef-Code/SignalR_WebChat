using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Spades : Suit
    {
        public Spades()
        {
            Name = "spades";
        }
        public override string Type { get { return "Spades"; } }

    }
}