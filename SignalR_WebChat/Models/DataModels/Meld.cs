﻿using SignalR_WebChat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Meld
    {
        #region fields
        private List<MeldType> meldTypes;
        private List<MeldCombination> meldCombinations;
        #endregion

        #region Constructors
        public Meld()
        {
            meldCombinations = MeldCombinations;
            meldTypes = MeldTypes;
        }
        public Meld(PinochleHand Hand)
        {
            TrumpSuit = Hand.TrumpSuit;
            meldCombinations = MeldCombinations;
            meldTypes = MeldTypes;
            HandInQuestion = Hand;
        }
        #endregion

        #region Properties
        public int MeldId { get; set; }
        public PinochleHand HandInQuestion { get; set; }
        public Suit TrumpSuit { get; set; }
        public List<MeldCombination> MeldCombinations
        {
            get { return meldCombinations; }
            set { meldCombinations = value; }
        }
        public List<MeldType> MeldTypes
        {
            get { return meldTypes; }
            set { meldTypes = value; }
        }

        public int Score { get; set; }
        public List<Card> TrumpKings
        {
            get
            {
                var trumpKings = HandInQuestion.TrumpCards.Where(c => c.IsKing()).ToList();
                if (trumpKings != null)
                    return trumpKings;
                else
                    return new List<Card>();
            }
        }
        public List<Card> TrumpQueens
        {
            get
            {
                var trumpQueens = HandInQuestion.TrumpCards.Where(c => c.IsQueen()).ToList();
                if (trumpQueens != null)
                    return trumpQueens;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Aces
        {
            get
            {
                var aces = HandInQuestion.Cards.Where(c => c.IsAce()).ToList();
                if (aces != null)
                    return aces;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Kings
        {
            get
            {
                var kings = HandInQuestion.Cards.Where(c => c.IsKing()).ToList();
                if (kings != null)
                    return kings;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Queens
        {
            get
            {
                var queens = HandInQuestion.Cards.Where(c => c.IsQueen()).ToList();
                if (queens != null)
                    return queens;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Jacks
        {
            get
            {
                var jacks = HandInQuestion.Cards.Where(c => c.IsJack()).ToList();
                if (jacks != null)
                    return jacks;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Tens
        {
            get
            {
                var tens = HandInQuestion.Cards.Where(c => c.IsTen()).ToList();
                if (tens != null)
                    return tens;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Clubs
        {
            get
            {
                var clubs = HandInQuestion.Cards.Where(c => c.Suit.Name == "clubs").ToList();
                if (clubs != null)
                    return clubs;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Diamonds
        {
            get
            {
                var diamonds = HandInQuestion.Cards.Where(c => c.Suit.Name == "diamonds").ToList();
                if (diamonds != null)
                    return diamonds;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Hearts
        {
            get
            {
                var hearts = HandInQuestion.Cards.Where(c => c.Suit.Name == "hearts").ToList();
                if (hearts != null)
                    return hearts;
                else
                    return new List<Card>();
            }
        }
        public List<Card> Spades
        {
            get
            {
                var spades = HandInQuestion.Cards.Where(c => c.Suit.Name == "spades").ToList();
                if (spades != null)
                    return spades;
                else
                    return new List<Card>();
            }
        }
        public List<Card> KingOfClubs
        {
            get
            {
                var kingOfClubs = HandInQuestion.Cards.Where(c => c.Suit.Name == "clubs" && c.IsKing()).ToList();
                if (kingOfClubs != null)
                    return kingOfClubs;
                else
                    return new List<Card>();
            }
        }
        public List<Card> KingOfDiamonds
        {
            get
            {
                var kingOfDiamonds = HandInQuestion.Cards.Where(c => c.Suit.Name == "diamonds" && c.IsKing()).ToList();
                if (kingOfDiamonds != null)
                    return kingOfDiamonds;
                else
                    return new List<Card>();
            }
        }
        public List<Card> KingOfHearts
        {
            get
            {
                var kingOfHearts = HandInQuestion.Cards.Where(c => c.Suit.Name == "hearts" && c.IsKing()).ToList();
                if (kingOfHearts != null)
                    return kingOfHearts;
                else
                    return new List<Card>();
            }
        }
        public List<Card> KingOfSpades
        {
            get
            {
                var kingOfSpades = HandInQuestion.Cards.Where(c => c.Suit.Name == "spades" && c.IsKing()).ToList();
                if (kingOfSpades != null)
                    return kingOfSpades;
                else
                    return new List<Card>();
            }
        }
        public List<Card> QueenOfClubs
        {
            get
            {
                var queenOfClubs = HandInQuestion.Cards.Where(c => c.Suit.Name == "clubs" && c.IsQueen()).ToList();
                if (queenOfClubs != null)
                    return queenOfClubs;
                else
                    return new List<Card>();
            }
        }
        public List<Card> QueenOfDiamonds
        {
            get
            {
                var queenOfDiamonds = HandInQuestion.Cards.Where(c => c.Suit.Name == "diamonds" && c.IsQueen()).ToList();
                if (queenOfDiamonds != null)
                    return queenOfDiamonds;
                else
                    return new List<Card>();
            }
        }
        public List<Card> QueenOfHearts
        {
            get
            {
                var queenOfHearts = HandInQuestion.Cards.Where(c => c.Suit.Name == "hearts" && c.IsQueen()).ToList();
                if (queenOfHearts != null)
                    return queenOfHearts;
                else
                    return new List<Card>();
            }
        }
        public List<Card> QueenOfSpades
        {
            get
            {
                var queenOfSpades = HandInQuestion.Cards.Where(c => c.Suit.Name == "spades" && c.IsQueen()).ToList();
                if (queenOfSpades != null)
                    return queenOfSpades;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NonTrumpKings
        {
            get
            {
                var kings = HandInQuestion.NonTrumpCards.Where(c => c.IsKing()).ToList();
                if (kings != null)
                    return kings;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_KingOfClubs
        {
            get
            {
                var kingOfClubs = NonTrumpKings.Where(c => c.Suit.Name == "clubs").ToList();
                if (kingOfClubs != null)
                    return kingOfClubs;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_KingOfDiamonds
        {
            get
            {
                var kingOfDiamonds = NonTrumpKings.Where(c => c.Suit.Name == "diamonds").ToList();
                if (kingOfDiamonds != null)
                    return kingOfDiamonds;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_KingOfHearts
        {
            get
            {
                var kingOfHearts = NonTrumpKings.Where(c => c.Suit.Name == "hearts").ToList();
                if (kingOfHearts != null)
                    return kingOfHearts;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_KingOfSpades
        {
            get
            {
                var kingOfSpades = NonTrumpKings.Where(c => c.Suit.Name == "spades").ToList();
                if (kingOfSpades != null)
                    return kingOfSpades;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NonTrumpQueens
        {
            get
            {
                var queens = HandInQuestion.NonTrumpCards.Where(c => c.IsQueen()).ToList();
                if (queens != null)
                    return queens;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_QueenOfClubs
        {
            get
            {
                var queenOfClubs = NonTrumpQueens.Where(c => c.Suit.Name == "clubs").ToList();
                if (queenOfClubs != null)
                    return queenOfClubs;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_QueenOfDiamonds
        {
            get
            {
                var queenOfDiamonds = NonTrumpQueens.Where(c => c.Suit.Name == "diamonds").ToList();
                if (queenOfDiamonds != null)
                    return queenOfDiamonds;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_QueenOfHearts
        {
            get
            {
                var queenOfHearts = NonTrumpQueens.Where(c => c.Suit.Name == "hearts").ToList();
                if (queenOfHearts != null)
                    return queenOfHearts;
                else
                    return new List<Card>();
            }
        }
        public List<Card> NT_QueenOfSpades
        {
            get
            {
                var queenOfSpades = NonTrumpQueens.Where(c => c.Suit.Name == "spades").ToList();
                if (queenOfSpades != null)
                    return queenOfSpades;
                else
                    return new List<Card>();
            }
        }
        #endregion

        #region Methods
        public int Runs() //Run: Ace, Ten, King, Queen, Jack of trumps
        {
            var h = HandInQuestion;
            var tc = h.TrumpCards;

            if (tc.HasAce<Card>() && tc.HasKing<Card>() && tc.HasQueen<Card>() && tc.HasJack<Card>() && tc.HasTen<Card>())
            {
                var aces = tc.Where(c => c.IsAce()).ToList();      // this can be refactored to tc.Count() which will eliminate 
                var kings = tc.Where(c => c.IsKing()).ToList();    // the extra computation and the lengths[][] array
                var queens = tc.Where(c => c.IsQueen()).ToList();
                var jacks = tc.Where(c => c.IsJack()).ToList();
                var tens = tc.Where(c => c.IsTen()).ToList();

                var lengths = new List<List<Card>>() { aces, kings, queens, jacks, tens };

                lengths.OrderBy(i => i.Count).ToList(); //lengths[4] has the least amount when using OrderBy

                if (lengths[4].Count == lengths[0].Count)
                {
                    return lengths[0].Count;
                }
                else if (lengths[4].Count == lengths[1].Count)
                {
                    return lengths[1].Count;
                }
                else if (lengths[4].Count == lengths[2].Count)
                {
                    return lengths[2].Count;
                }
                else if (lengths[4].Count == lengths[3].Count)
                {
                    return lengths[3].Count;
                }
                else
                {
                    return lengths[4].Count;
                }
            }

            return 0;
        }

        public int RoyalMarriages() //Royal Marriage: King and Queen of trumps
        {
            var royalMarriages = 0;

            var trumpKings = TrumpKings.Count;
            var trumpQueens = TrumpQueens.Count;

            var runs = Runs();

            if (trumpKings > runs && trumpQueens > runs)
            {
                if (trumpKings > trumpQueens)
                {
                    royalMarriages = trumpQueens - runs;
                }
                else if (trumpQueens > trumpKings)
                {
                    royalMarriages = trumpKings - runs;
                }
                else
                {
                    royalMarriages = trumpKings - runs; //this could be either trumpKings || trumpQueens - runs
                }
            }

            return royalMarriages;
        }
        public List<Suit> RoyalMarriageSuits()
        {
            var suits = new List<Suit>();

            if(KingOfClubs.Count > 0 && QueenOfClubs.Count > 0)
            {
                suits.Add(new Clubs());
            }
            if (KingOfDiamonds.Count > 0 && QueenOfDiamonds.Count > 0)
            {
                suits.Add(new Diamonds());
            }
            if (KingOfHearts.Count > 0 && QueenOfHearts.Count > 0)
            {
                suits.Add(new Hearts());
            }
            if (KingOfSpades.Count > 0 && QueenOfSpades.Count > 0)
            {
                suits.Add(new Spades());
            }

            return suits;
        }

        public int Marriages() //Marriage: Kings and Queen of the SAME suit, *NOT trumps*
        {
            var marriages = 0;

            var clubMarriages = MarriagesHelper(NT_KingOfClubs, NT_QueenOfClubs);
            var diamondMarriages = MarriagesHelper(NT_KingOfDiamonds, NT_QueenOfDiamonds);
            var heartMarriages = MarriagesHelper(NT_KingOfHearts, NT_QueenOfHearts);
            var spadeMarriages = MarriagesHelper(NT_KingOfSpades, NT_QueenOfSpades);

            marriages = (clubMarriages + diamondMarriages + heartMarriages + spadeMarriages);

            return marriages;
        }
        private int MarriagesHelper(List<Card> Kings, List<Card> Queens)
        {
            var marriages = 0;
            var kings = Kings.Count;
            var queens = Queens.Count;

            if (kings >= 1 && queens >= 1) //at least 1 marriage
            {
                if (kings > queens)
                {
                    marriages = queens;
                }
                else if (queens > kings)
                {
                    marriages = kings;
                }
                else
                {
                    marriages = kings; //this could be either kings || queens 
                }
            }

            return marriages;
        }
        public int Pinochles() //Pinochle: Jack of diamonds & Queen of spades
        {
            var pinochles = 0;

            var hand = this.HandInQuestion;

            var jackOfDiamonds = hand.Cards.Count(c => c.IsJack() && c.Suit.Name == "diamonds");
            var queenOfSpades = hand.Cards.Count(c => c.IsQueen() && c.Suit.Name == "spades");

            if (jackOfDiamonds >= 1 && queenOfSpades >= 1) //at least 1 pinochle
            {
                if (jackOfDiamonds > queenOfSpades)
                {
                    pinochles = queenOfSpades;
                }
                else if (queenOfSpades > jackOfDiamonds)
                {
                    pinochles = jackOfDiamonds;
                }
                else
                {
                    pinochles = jackOfDiamonds; //this could be either jackOfDiamonds || queenOfSpades 
                }
            }

            return pinochles;
        }
        public int AcesAround() //Aces around: An Ace in each suit
        {
            var acesAround = 0;

            var aces = Aces;

            if(aces.Count > 0)
                acesAround = AroundHelper(aces);
           
            return acesAround;
        }
        public int KingsAround() //Kings around: A King in each suit
        {
            var kingsAround = 0;

            var kings = Kings;

            if (kings.Count > 0)
                kingsAround = AroundHelper(kings);

            return kingsAround;
        }
        public int QueensAround() //Queens around: A Queen in each suit
        {
            var queensAround = 0;

            var queens = Queens;

            if (queens.Count > 0)
                queensAround = AroundHelper(queens);

            return queensAround;
        }
        public int JacksAround() //Jacks around: A Jack in each suit
        {
            var jacksAround = 0;

            var jacks = Jacks;

            if (jacks.Count > 0)
                jacksAround = AroundHelper(jacks);

            return jacksAround;
        }
        private int AroundHelper(List<Card> list)
        {
            var arounds = 0;

            if (list.HasClub<Card>() && list.HasDiamond<Card>() && list.HasHeart<Card>() && list.HasSpade<Card>())
            {
                var clubs = list.Where(c => c.IsClubs()).ToList();
                var diamonds = list.Where(c => c.IsDiamonds()).ToList();
                var hearts = list.Where(c => c.IsHearts()).ToList();
                var spades = list.Where(c => c.IsSpades()).ToList();

                var lengths = new List<List<Card>> { clubs, diamonds, hearts, spades };

                lengths.OrderBy(i => i.Count).ToList(); //lengths[4] has the least amount when using OrderBy

                if (lengths[3].Count == lengths[0].Count)
                {
                    return lengths[0].Count;
                }
                else if (lengths[3].Count == lengths[1].Count)
                {
                    return lengths[1].Count;
                }
                else if (lengths[3].Count == lengths[2].Count)
                {
                    return lengths[2].Count;
                }
                else
                {
                    return lengths[3].Count;
                }
            }
            return arounds;
        }
        public int GetScore()
        {
            var score = 0;

            var runs = Runs();
            var royalMarriages = RoyalMarriages();
            var marriages = Marriages();
            var pinochles = Pinochles();
            var acesAround = AcesAround();
            var kingsAround = KingsAround();
            var queensAround = QueensAround();
            var jacksAround = JacksAround();

            switch(runs)
            {
                case 1: score += 15;
                    break;
                case 2: score += 150;
                    break;
                case 3: score += 225;
                    break;
                case 4: score += 300;
                    break;
                default :
                    score +=0;
                    break;
            }

            switch (royalMarriages)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 8;
                    break;
                case 3:
                    score += 12;
                    break;
                case 4:
                    score += 16;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (marriages)
            {
                case 1:
                    score += 2;
                    break;
                case 2:
                    score += 4;
                    break;
                case 3:
                    score += 6;
                    break;
                case 4:
                    score += 8;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (pinochles)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 30;
                    break;
                case 3:
                    score += 60;
                    break;
                case 4:
                    score += 90;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (acesAround)
            {
                case 1:
                    score += 10;
                    break;
                case 2:
                    score += 100;
                    break;
                case 3:
                    score += 150;
                    break;
                case 4:
                    score += 200;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (kingsAround)
            {
                case 1:
                    score += 8;
                    break;
                case 2:
                    score += 80;
                    break;
                case 3:
                    score += 120;
                    break;
                case 4:
                    score += 160;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (queensAround)
            {
                case 1:
                    score += 6;
                    break;
                case 2:
                    score += 60;
                    break;
                case 3:
                    score += 90;
                    break;
                case 4:
                    score += 120;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (jacksAround)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 40;
                    break;
                case 3:
                    score += 60;
                    break;
                case 4:
                    score += 80;
                    break;
                default:
                    score += 0;
                    break;
            }

            return score;
        }
        public int GetScore(ScoringOptions options)
        {
            var score = 0;

            var runs = Runs();
            var royalMarriages = RoyalMarriages();
            var marriages = Marriages();
            var pinochles = Pinochles();
            var acesAround = AcesAround();
            var kingsAround = KingsAround();
            var queensAround = QueensAround();
            var jacksAround = JacksAround();

            switch (runs)
            {
                case 1:
                    score += 15;
                    break;
                case 2:
                    score += 150;
                    break;
                case 3:
                    score += 225;
                    break;
                case 4:
                    score += 300;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (royalMarriages)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 8;
                    break;
                case 3:
                    score += 12;
                    break;
                case 4:
                    score += 16;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (marriages)
            {
                case 1:
                    score += 2;
                    break;
                case 2:
                    score += 4;
                    break;
                case 3:
                    score += 6;
                    break;
                case 4:
                    score += 8;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (pinochles)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 30;
                    break;
                case 3:
                    score += 60;
                    break;
                case 4:
                    score += 90;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (acesAround)
            {
                case 1:
                    score += 10;
                    break;
                case 2:
                    score += 100;
                    break;
                case 3:
                    score += 150;
                    break;
                case 4:
                    score += 200;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (kingsAround)
            {
                case 1:
                    score += 8;
                    break;
                case 2:
                    score += 80;
                    break;
                case 3:
                    score += 120;
                    break;
                case 4:
                    score += 160;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (queensAround)
            {
                case 1:
                    score += 6;
                    break;
                case 2:
                    score += 60;
                    break;
                case 3:
                    score += 90;
                    break;
                case 4:
                    score += 120;
                    break;
                default:
                    score += 0;
                    break;
            }

            switch (jacksAround)
            {
                case 1:
                    score += 4;
                    break;
                case 2:
                    score += 40;
                    break;
                case 3:
                    score += 60;
                    break;
                case 4:
                    score += 80;
                    break;
                default:
                    score += 0;
                    break;
            }

            return score;
        }
        #endregion

    }
}