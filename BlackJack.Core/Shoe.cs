using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Shoe
    {
        private readonly List<Deck> deckList;
        
        public Shoe(int numberOfDecks)
        {
            deckList = new List<Deck>(numberOfDecks);
            while (deckList.Count < numberOfDecks)
            {
                var deck = new Deck();
               
                deckList.Add(deck);
            }
        }

        public IList<Deck> DeckList
        {
            get
            { 
                return deckList;  
            }
        }

      
        private Card getCard(CardType type)
        {
            Card topCard = null;
            bool isFound = false;
            foreach (var deck in deckList)
            {
                if (isFound)
                    break;
                foreach (var card in deck.CardList)
                {
                    if (!card.IsDealt && card.CardType == type)
                    {
                        topCard = card;
                        topCard.IsDealt = true;
                        isFound = true;
                        break;
                    }
                }
            }

            if (topCard == null)
                throw new ArgumentNullException("the card is null", "the card is null");
            return topCard;
        }

        public Card GetALow()
        {
            return getCard(CardType.Numeric);
        }

        public Card GetAnAce()
        {
            return getCard(CardType.Ace);
        }

        public Card GetAFace()
        {
            return getCard(CardType.Jack);
        }

        public Card SplitTester(Card prevCard)
        {
            Card topCard = null;
            bool isFound = false;
            foreach (var deck in deckList)
            {
                if (isFound)
                    break;
                foreach (var card in deck.CardList)
                {
                    if (!card.IsDealt && card.Index == prevCard.Index )
                    {
                        topCard = card;
                        topCard.IsDealt = true;
                        isFound = true;
                        break;
                    }
                }
            }

            if (topCard == null)
                throw new ArgumentNullException("the card is null", "the card is null");
            return topCard;
        }


        public Card NextCard
        {
            get
            {
                Card topCard = null;
                bool isFound = false;
           
           
                foreach (var deck in deckList)
                {
                    if (isFound)
                        break;
                    foreach (var card in deck.CardList)
                    {
                        if (!card.IsDealt)
                        {
                            topCard = card;
                            topCard.IsDealt = true;
                            isFound = true;
                            break;
                        }
                    }
                }

                if (topCard == null)
                    throw new ArgumentNullException("the card is null", "the card is null");
                return topCard;
            }
        }

        public List<Card> DealtCards
        {
            get
            {
                var list = new List<Card>();
                this.deckList.ForEach(i => i.CardList.ForEach(card => { if(card.IsDealt) list.Add(card); }));
                return list;
            }
        }

        public List<Card> UndealtCards
        {
            get
            {
                var list = new List<Card>();
                this.deckList.ForEach(i => i.CardList.ForEach(card => { if (!card.IsDealt) list.Add(card); }));
                return list;
            }
        }
    }
}
