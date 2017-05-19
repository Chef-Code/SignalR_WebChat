using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Diamonds : Suit
    {
        public Diamonds()
        {
            Name = "diamonds";
        }
        public override string Type { get { return "Diamonds"; } }
    }
}