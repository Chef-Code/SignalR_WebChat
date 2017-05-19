using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class PinochlePlayer
    {
        private PinochleHand hand;
        public PinochlePlayer()
        {
            hand = new PinochleHand();

        }
        public int PinochlePlayerId { get; set; }
        public string Name { get; set; }
        public int Bid { get; set; }

        public PinochleHand Hand
        {
            get { return hand; }
            set { hand = value; }
        }

        public void DeclareTrumpSuit(Suit TrumpSuit)
        {
            hand.TrumpSuit = TrumpSuit;
        }
        public void SetBid(int Bid)
        {
            this.Bid = Bid;
        }

    }
}