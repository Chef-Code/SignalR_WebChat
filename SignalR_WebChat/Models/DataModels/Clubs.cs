using Newtonsoft.Json;
using SignalR_WebChat.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Clubs : Suit
    {
        public Clubs()
        {        
            Name = "clubs";
        }
        public override string Type { get { return "Clubs"; } }
    }
}