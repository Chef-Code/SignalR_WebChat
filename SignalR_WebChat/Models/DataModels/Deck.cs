using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Deck
    {
        public string face;
        public string suit;
        public string indexedCard;

        List<Suit> suits = new List<Suit>()
        {
            new Clubs(),
            new Diamonds(),
            new Hearts(),
            new Spades()
        };
        string[] faces = new string[5] { "jack", "queen", "king", "ten", "ace" };
        int[] sameCardIndex = new int[4] { 1, 2, 3, 4 };

        public string[] indexedCards = new string[80];

        public int deckIndex;

        private List<Card> cards = new List<Card>();

        public Deck()
        {
            Cards = FillListCards();
        }
        public List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }

        public int Count
        {
            get
            {
                return cards.Count;
            }
        }
        public Card this[int index]
        {
            get
            {
                return cards[index];
            }
        }

        public int this[Card card]
        {
            get
            {
                return cards.FindIndex(c =>
                              c.Value == card.Value &&
                              c.Suit == card.Suit &&
                              c.SameCardIndex == card.SameCardIndex);
            }
        }

        public bool Empty
        {
            get
            {
                if (Count != 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        private string[] FillStringArrayCards()
        {
            deckIndex = 0;

            for (int x = 0; x <= 3; x++)
            {
                for (int i = 0; i < faces.Count(); i++)
                {
                    face = faces[i].ToString();

                    for (int k = 0; k < suits.Count(); k++)
                    {
                        suit = suits[k].ToString();
                        indexedCard = face + suit + sameCardIndex[x].ToString();

                        indexedCards[deckIndex] += indexedCard;
                        deckIndex++;
                    }
                }
            }

            return indexedCards;
        }

        private List<Card> FillListCards()
        {
            var deckCards = new List<Card>();
            for (int x = 0; x < sameCardIndex.Length; x++)
            {
                for (int i = 0; i < faces.Length; i++)
                {
                    for (int k = 0; k < suits.Count; k++)
                    {
                        Card newCard = new Card(suits[k], faces[i], sameCardIndex[x]);
                        deckCards.Add(newCard);
                    }
                }
            }
            return deckCards;
        }


    }
}