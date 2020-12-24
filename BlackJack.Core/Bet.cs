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
        public event GameEvents.OnBetChanged onBetChanged;

        private List<Chip> chipList;
        private double amount = 0;
        PlayerHand hand;
        GameController controller;
        public Bet(PlayerHand hand, GameController gc)
        {
            chipList = new List<Chip>();

            this.controller = gc;
            this.hand = hand;
        }

        public List<Chip> ChipList
        {
            get { return chipList; }
            set { chipList = value; }
        }

        public double Amount
        {
            get { return amount; }
            set
            {
                var oldAmount = Amount;

                amount = value;
                controller.PlayerbankRoll -= (amount - oldAmount);

                if (onBetChanged != null)
                {
                    var b = new OnBetChangedEventArgs(hand, this);
                    onBetChanged(this, b);
                }
            }
        }
    }
}
