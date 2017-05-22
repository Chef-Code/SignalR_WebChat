using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class AppUser
    {
        public AppUser()
        {
            PinochleHand = new PinochleHand();
            
        }
        public AppUser(string Alias, string Hash) :this()
        {
            this.Alias = Alias;
            this.Hash = Hash;
            this.AppUserId = Guid.NewGuid().ToString("d");
            Matches = new List<int>();
        }
        public string ConnectionId { get; set; }
        public string AppUserId { get; set; }
        public string Alias { get; set; }
        public string Hash { get; set; }
        public string Group { get; set; }
        public bool IsPlaying { get; set; }
        public bool HasTeam { get; set; }
        public bool PassBid { get; set; }
        public bool IsDealer { get; set; }
        public PinochleHand PinochleHand { get; set; }
        public int Bid { get; set; }        
        public void SetBid(int Bid)
        {
            this.Bid = Bid;
        }     
        public List<int> Matches { get; set; }
        public int MeldScore { get; set; }
        public List<Trick> TricksTaken { get; set; }
        #region Methods
        public void DeclareTrumpSuit(Suit TrumpSuit)
        {
            PinochleHand.TrumpSuit = TrumpSuit;
        }
        public int PointersTaken()
        {
            var pointersTaken = 0;
            foreach(var trick in TricksTaken)
            {
                pointersTaken += trick.TrickValue;
            }
            return pointersTaken;
        }
        #endregion
    }
}