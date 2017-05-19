using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Card
    {
        public Card()
        {
            this.Suit = Suit;
            this.Value = Value;
            this.SameCardIndex = SameCardIndex;
            //this.CardImage = $"/Images/pinochle_cards/{Value}_of_{Suit.Name}.png";
            //this.CardId = $"{Value}{Suit.Name}{SameCardIndex.ToString()}";
        }
        public Card(Suit Suit)
        {
            this.Suit = Suit;
            this.Value = "joker";
            this.SameCardIndex = 1;
        }
        public Card(string Value)
        {
            this.Suit = Suit;
            this.Value = Value;
            this.SameCardIndex = SameCardIndex;
        }

        public Card(Suit Suit, string Value)
        {
            this.Suit = Suit;
            this.Value = Value;
            this.SameCardIndex = SameCardIndex;
            this.CardImage = $"/Images/pinochle_cards/{Value}_of_{Suit.Name}.png";
            this.CardId = $"{Value}{Suit.Name}{SameCardIndex.ToString()}";
        }
        public Card(Suit Suit, string Value, int SameCardIndex)
        {
            this.Suit = Suit;
            this.Value = Value;
            this.SameCardIndex = SameCardIndex;
            this.CardImage = $"/Images/pinochle_cards/{Value}_of_{Suit.Name}.png";
            this.CardId = $"{Value}{Suit.Name}{SameCardIndex.ToString()}";
        }
        public bool TrumpSuit(Suit a)
        {
            if (Suit.Name == a.Name)
                return true;
            else
                return false;
        }
        //public CardBack CardBack { get; set; }
        //public CardFront  CardFront { get; set; }
        public string CardId { get; set; }
        public Suit Suit { get; set; }
        public string CardImage { get; set; }
        public string Value { get; set; }

        public int SameCardIndex { get; set; }

        public int Rank(Card a)
        {
            if (IsAce(a) || IsTen(a) || IsKing(a))
                return 1;
            else
                return 0;
        }

        public bool SameSuit(Card b)
        {
            if (this.Suit.Name == b.Suit.Name)
                return true;
            else
                return false;
        }
        public bool SameValue(Card b)
        {
            if (this.Value == b.Value)
                return true;
            else
                return false;
        }

        public bool SameSuitAndValue(Card b)
        {
            if (SameSuit(b) && SameValue(b))
                return true;
            else
                return false;
        }

        public bool SameIndex(Card b)
        {
            if (this.SameCardIndex == b.SameCardIndex)
                return true;
            else
                return false;
        }

        public bool SameCard(Card b)
        {
            if (SameSuitAndValue(b) && SameIndex(b))
                return true;
            else
                return false;
        }

        public bool IsJack()
        {
            if (this.Value == "jack")
                return true;
            else
                return false;
        }
        public bool IsJack(Card a)
        {
            if (a.Value == "jack")
                return true;
            else
                return false;
        }

        public bool IsQueen()
        {
            if (this.Value == "queen")
                return true;
            else
                return false;
        }

        public bool IsQueen(Card a)
        {
            if (a.Value == "queen")
                return true;
            else
                return false;
        }

        public bool IsKing()
        {
            if (this.Value == "king")
                return true;
            else
                return false;
        }

        public bool IsKing(Card a)
        {
            if (a.Value == "king")
                return true;
            else
                return false;
        }

        public bool IsTen()
        {
            if (this.Value == "ten")
                return true;
            else
                return false;
        }

        public bool IsTen(Card a)
        {
            if (a.Value == "ten")
                return true;
            else
                return false;
        }

        public bool IsAce()
        {
            if (this.Value == "ace")
                return true;
            else
                return false;
        }

        public bool IsAce(Card a)
        {
            if (a.Value == "ace")
                return true;
            else
                return false;
        }

        public bool IsClubs()
        {
            if (this.Suit.Name == "clubs")
                return true;
            else
                return false;
        }
        public bool IsClubs(Card a)
        {
            if (a.Suit.Name == "clubs")
                return true;
            else
                return false;
        }

        public bool IsDiamonds()
        {
            if (this.Suit.Name == "diamonds")
                return true;
            else
                return false;
        }

        public bool IsDiamonds(Card a)
        {
            if (a.Suit.Name == "diamonds")
                return true;
            else
                return false;
        }

        public bool IsHearts()
        {
            if (this.Suit.Name == "hearts")
                return true;
            else
                return false;
        }

        public bool IsHearts(Card a)
        {
            if (a.Suit.Name == "hearts")
                return true;
            else
                return false;
        }

        public bool IsSpades()
        {
            if (this.Suit.Name == "spades")
                return true;
            else
                return false;
        }

        public bool IsSpades(Card a)
        {
            if (a.Suit.Name == "spades")
                return true;
            else
                return false;
        }
    }
}