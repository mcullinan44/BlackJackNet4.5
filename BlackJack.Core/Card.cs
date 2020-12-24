using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Card
    {
        private readonly Suit suit;
        private CardType cardType;
        private int value;
        private int index;

        public Card(Suit suit, int pointValue, CardType type) 
        {
            this.suit = suit;
            this.value = pointValue;
            this.cardType = type;
            this.IsDealt = false;
            this.Index = type == Core.CardType.Numeric ? pointValue : type == Core.CardType.Jack ? 11 : type == Core.CardType.Queen ? 12 : type == Core.CardType.King ? 13 : 1;
        }

        public Suit CardSuit
        {
            get
            {
                return suit;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }
            set { this.value = value; }
        }

        public CardType CardType
        {
            get
            {
                return cardType;
            }
            set { cardType = value; }
        }

        public bool IsDealt
        {
            get;
            set; 
        }

        public int Index
        {
            get;
            set;
        }

        public override string ToString()
        {
            return  (CardType == Core.CardType.Ace ? "A" : CardType == Core.CardType.Jack ? "J" : CardType == Core.CardType.Queen ? "Q" : CardType == Core.CardType.King ? "K" : Value.ToString()) + " " +
                (CardSuit == Suit.Clubs ? "\u2663" : CardSuit == Suit.Spades ? "\u2660" : CardSuit == Suit.Diamonds ? "\u2666" : "\u2665");
        }

    }
}
