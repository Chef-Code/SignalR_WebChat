using SignalR_WebChat.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class PinochleDealer
    {
        private Deck deck;
        private int[] dealToEachAmount = new int[4] { 2, 3, 4, 5 }; //how many cards are dealt out each circulation to each player.
        public PinochleDealer()
        {
            deck = new Deck();
        }
        public int PinochleDealerId { get; set; }
        public Deck Deck
        {
            get { return deck; }
            set { deck = value; }
        }
        public List<Card> Deal(int Amount)
        {
            var i = (Amount - 1);
            var dealtCards = Deck.Cards.Take(Amount).ToList();
            if (Deck.Cards.Count >= Amount)
            {
                Deck.Cards.RemoveRange(0, Amount);
            }


            return dealtCards;
        }
        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < this.Deck.Cards.Count; i++)
            {
                var temp = Deck.Cards[i];
                var index = rand.Next(0, Deck.Cards.Count);
                Deck.Cards[i] = Deck.Cards[index];
                Deck.Cards[index] = temp;

            }

        }
        public string ShowCards()
        {
            var cards = Deck.Cards;
            string allCards = null;
            foreach (var card in cards)
            {
                allCards += string.Format("{0} {1}, \n", card.Value, card.Suit);
            };

            return allCards;
        }

        public void CutCards()
        {
            var leftHand = Deck.Cards.Take(40);
            var rightHand = Deck.Cards.TakeLast(40);


            var result = rightHand.Union(leftHand).ToList();

            this.Deck.Cards = result;

        }

        public void RiffleCards()
        {
            var leftHand = Deck.Cards.Take(40).ToList();
            var rightHand = Deck.Cards.TakeLast(40).ToList();

            var riffledDeck = new List<Card>();

            for (int i = 0; i < rightHand.Count; i++)
            {
                riffledDeck.Add(leftHand[i]);
                riffledDeck.Add(rightHand[i]);
            }

            Deck.Cards = riffledDeck;
        }
    }
}