using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Trick
    {
        public List<Card> Cards { get; set; }
        public int TrickValue
        {
            get
            {
                var trickValue = 0;

                foreach(var card in Cards)
                {
                    if(card.IsAce() || card.IsTen() || card.IsKing())
                    {
                        trickValue += 1;
                    }
                }
                return trickValue;
            }
        }
    }
}