using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Bet
    {
        private double amount = 0;
        

        public Bet(PlayerHand hand)
        {
            this.Hand = hand;
        }


        public PlayerHand Hand { get; set; }

        public double Amount
        {
            get { return amount; }
            set
            {
                var oldAmount = Amount;
                amount = value;
            }
        }
    }
}
