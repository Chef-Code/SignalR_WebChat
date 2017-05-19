using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class PinochleHand
    {
        #region fields
        private int meld;
        private List<Card> cards;
        private List<Card> trumpCards;
        private List<Card> nonTrumpCards;
        private List<Suit> nonTrumpSuits;
        private List<Card> aces;
        private List<Card> kings;
        private List<Card> queens;
        private List<Card> jacks;
        private List<Card> tens;
        #endregion

        #region Constructors
        public PinochleHand()
        {
            this.cards = new List<Card>();
        }

        public PinochleHand(Suit TrumpSuit)
        {
            this.cards = new List<Card>();
            this.TrumpSuit = TrumpSuit;
            this.TrumpCards = trumpCards;
            this.NonTrumpCards = nonTrumpCards;
            this.aces = Aces;
            this.kings = Kings;
            this.queens = Queens;
            this.jacks = Jacks;
            this.tens = Tens;
        }

        public PinochleHand(int Meld)
        {
            this.cards = new List<Card>();
            this.meld = Meld;
        }
        #endregion

        #region Properties
        public int PinochleHandId { get; set; }
        public int Meld
        {
            get; //TODO: write this

            set;
        }
        public List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }

        public List<Card> TrumpCards
        {
            get
            {
                if(TrumpSuit != null)
                {
                    trumpCards = Cards.Where(c => c.Suit.Name == TrumpSuit.Name).ToList();
                    return trumpCards;
                }
                else
                {
                    return null;
                }

            }
            set { trumpCards = value; }
        }

        public List<Card> NonTrumpCards
        {
            get
            {
                if (TrumpSuit != null)
                {
                    nonTrumpCards = Cards.Where(c => c.Suit.Name != TrumpSuit.Name).ToList();
                    return nonTrumpCards;
                }
                else
                {
                    return null;
                }
            }
            set { nonTrumpCards = value; }
        }

        public Suit TrumpSuit { get; set; }

        public List<Suit> NonTrumpSuits
        {
            get
            {
                var allSuits = new List<Suit>() { new Clubs(), new Diamonds(), new Hearts(), new Spades() };
                if(this.TrumpSuit != null)
                {
                    nonTrumpSuits = allSuits.Where(s => s.Name == this.TrumpSuit.Name).ToList();
                }
                else
                {
                    nonTrumpSuits = allSuits;
                }
                return nonTrumpSuits;
            }
            set { nonTrumpSuits = value; }
        }


        public List<Card> Aces
        {
            get
            {
                aces = Cards.Where(c => c.Value == "ace").ToList();
                return aces;
            }
            set
            {
                var listOfAces = Cards.Where(c => c.Value == "ace").ToList();
                foreach (var card in listOfAces)
                {
                    value.Add(card);
                }
            }
        }


        public List<Card> Kings
        {
            get
            {
                kings = Cards.Where(c => c.Value == "king").ToList();
                return kings;
            }
            set { kings = value; }
        }


        public List<Card> Queens
        {
            get
            {
                queens = Cards.Where(c => c.Value == "queen").ToList();
                return queens;
            }
            set { queens = value; }
        }

        public List<Card> Jacks
        {
            get
            {
                jacks = Cards.Where(c => c.Value == "jack").ToList();
                return jacks;
            }
            set { jacks = value; }
        }


        public List<Card> Tens
        {
            get
            {
                tens = Cards.Where(c => c.Value == "ten").ToList();
                return tens;
            }
            set { tens = value; }
        }
        #endregion

        #region Indexers
        public Card this[int index]  //POST int, GET Card
        {
            get
            {
                return cards[index];
            }
        }

        public int this[Card card] //POST Card, GET int
        {
            get
            {
                return cards.FindIndex(c =>
                c.Value == card.Value &&
                c.Suit == card.Suit &&
                c.SameCardIndex == card.SameCardIndex);
            }
        }
        #endregion

        #region Methods
        public bool HasTen()
        {
            if (this.cards.Any(c => c.IsTen()))
                return true;
            else
                return false;
        }

        public bool HasJack()
        {
            if (this.cards.Any(c => c.IsJack()))
                return true;
            else
                return false;
        }

        public bool HasQueen()
        {
            if (this.cards.Any(c => c.IsQueen()))
                return true;
            else
                return false;
        }

        public bool HasKing()
        {
            if (this.cards.Any(c => c.IsKing()))
                return true;
            else
                return false;
        }

        public bool HasAce()
        {
            if (this.cards.Any(c => c.IsAce()))
                return true;
            else
                return false;
        }
        public bool IsRun()
        {
            foreach (var card in TrumpCards)
            {
                if (HasTen() && HasQueen() && HasKing() && HasJack() && HasAce())
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsRoyalMarriage()
        {
            foreach (var card in TrumpCards)
            {
                if (HasQueen() && HasKing())
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMarriage()
        {
            return false;
        }

        public bool IsPinochle(PinochleHand ph)
        {
            foreach (var card in ph.cards)
            {
                //this[card.IsJack()];
            }
            return false;
        }

        public bool IsAcesAround()
        {
            return false;
        }

        public bool IsKingsAround()
        {
            return false;
        }

        public bool IsQueensAround()
        {
            return false;
        }

        public bool IsJacksAround()
        {
            return false;
        }
        #endregion

    }
}