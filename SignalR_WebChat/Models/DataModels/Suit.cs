using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{

    public abstract class Suit
    {
        //[JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]

        //public int SuitId { get; set; }
        public abstract string Type { get; }
        //[JsonProperty(TypeNameHandling = TypeNameHandling.Auto)]
        public string Name { get; set; }
    }
}