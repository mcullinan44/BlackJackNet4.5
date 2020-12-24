using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public abstract class Hand
    {
        protected GameController controller;

        public event GameEvents.OnCardReceived onCardReceived;
        public event GameEvents.OnBlackjack onBlackjack;
        public event GameEvents.OnBust onBust;

        private readonly List<Card> cardList;
        private Bet currentBet;
       // public readonly IPlayer player;
        private Result result;

        public Hand(GameController controller)
        {
            cardList = new List<Card>();


            result = new Result();
        }

        public void ReceiveCard(Card card, bool isDeal)
        {
            cardList.Add(card);
            var args = new OnCardReceivedEventArgs(this, card);
            if(onCardReceived != null)
                 onCardReceived(this, args);
            var score = this.CurrentScore;
            if (score == 21 && isDeal)
            {
                IsBlackjack = true;
                IsFinalized = true;
                if(onBlackjack != null)
                    onBlackjack(this, args);
            }
            else if (score > 21)
            {
                IsBust = true;
                IsFinalized = true;
                
                if(onBust != null)
                    onBust(this, args);
            }
        }

        public bool IsFinalized { get; set;  }

        public bool IsBlackjack  { get; set; }

        public bool IsBust { get; set; }

        public List<Card> Cards
        {

            get
            {
                return cardList;
            }
        }
        
       public int CardCount
        {
            get
            {
                return cardList.Count;
            }
        }

       public int CurrentScore
       {
           get
           {
               int runningTotal = 0;
               cardList.ForEach((i) =>
               {
                   runningTotal += i.Value;
               });

               if(runningTotal > 21)
               {
                   foreach (var c in cardList)
                   {
                       if (c.CardType == CardType.Ace)
                       {
                           runningTotal -= 10;
                       }

                       if (runningTotal < 21)
                           break;
                   }
               }

               return runningTotal;
           }
       }

       public bool IsActive
       {
           get;
           set; 
       }

        public Result Result
       {

           get { return result; }
           set { result = value; }
       }
    }
}
