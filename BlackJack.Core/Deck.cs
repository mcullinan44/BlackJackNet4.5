using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Blackjack.Core
{
    public class Deck
    {
        private readonly List<Card> cardList;
        private Bitmap[,] _cards;

        public Deck() {
            cardList = new List<Card>();
            var suitList = new List<Suit>();
            suitList.Add(Suit.Clubs);
            suitList.Add(Suit.Diamonds);
            suitList.Add(Suit.Hearts);
            suitList.Add(Suit.Spades);
            foreach (Suit suit in suitList)
            {
                for (int i = 2; i <= 14; i++)
                {
                    int value = 0;
                    CardType type;
                    if (i <= 10)
                    {
                        value = i;
                        type = CardType.Numeric;
                    }
                    else
                    {
                        value = i <= 13 ? 10 : 11;
                        type = (i == 11 ? CardType.Jack : i == 12 ? CardType.Queen : i == 13 ? CardType.King : CardType.Ace);
                    }
                    cardList.Add(new Card(suit, value, type));
                }
            }
        }

        public void Shuffle()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < cardList.Count; i++)
            {
                int pos = rand.Next(cardList.Count - 1);
                Card thisCard = cardList[i];
                thisCard.IsDealt = false;
                Card replaceCard = cardList[pos];
                replaceCard.IsDealt = false;
                cardList[i] = replaceCard;
                cardList[pos] = thisCard;
            }
        }

        public List<Card> CardList
        {
            get { return this.cardList;  }
        }
    }
}
