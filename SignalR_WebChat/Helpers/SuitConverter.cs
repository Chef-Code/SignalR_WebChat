using Newtonsoft.Json.Linq;
using PinochleDeck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Helpers
{
    public class SuitConverter : AbstractJsonConverter<Suit>
    {
        protected override Suit Create(Type objectType, JObject jObject)
        {
            string typeName = (jObject["Type"]).ToString();
            switch(typeName)
            {
                case "Clubs":
                    return new Clubs();
                case "Diamonds":
                    return new Clubs();
                case "Hearts":
                    return new Clubs();
                case "Spades":
                    return new Clubs();
                default:
                    return null;
            }
           
        }
    }
}