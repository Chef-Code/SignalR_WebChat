using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Hearts : Suit
    {
        public Hearts()
        {
            Name = "hearts";
        }
        public override string Type { get { return "Hearts"; } }

    }
}