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

        public Hand(GameController controller)
        {
            Cards = new List<Card>();
            Result = new Result();
        }

        public void ReceiveCard(Card card, bool isDeal)
        {
            Cards.Add(card);
            var args = new OnCardReceivedEventArgs(this, card);
            onCardReceived?.Invoke(this, args);
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

                onBust?.Invoke(this, args);
            }
        }

        public bool IsFinalized { get; set;  }

        public bool IsBlackjack  { get; set; }

        public bool IsBust { get; set; }

        public List<Card> Cards { get; }

        public int CardCount
        {
            get
            {
                return Cards.Count;
            }
        }

       public int CurrentScore
       {
           get
           {
               int runningTotal = 0;
               Cards.ForEach((i) =>
               {
                   runningTotal += i.Value;
               });

               if(runningTotal > 21)
               {
                   foreach (var c in Cards)
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

        public Result Result { get; set; }
    }
}
