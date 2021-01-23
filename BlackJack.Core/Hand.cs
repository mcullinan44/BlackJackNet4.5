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

        public Hand(GameController controller)
        {
            Cards = new List<Card>();
            Result = new Result();
            
            State = State.NotYetPlayed;

            this.controller = controller;
        }



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

                //treat Aces as 1 if current score is over 21
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

        public abstract bool CheckIsBust();
  
 
        public State State { get; set; }

        public Result Result { get; set; }
    }
}
