using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public class DealerHand : Hand
    {

        public event GameEvents.OnCardReceived onCardReceived;
        public event GameEvents.OnDealerWinHand onDealerWinHand;
        public event GameEvents.OnDealerLoseHand onDealerLoseHand;
        public event GameEvents.OnDealerBlackjack onDealerBlackjack;
        public event GameEvents.OnDealerBust onDealerBust;
        public event GameEvents.OnPushHand onPushHand;


        public DealerHand(Dealer dealer, GameController gameController)
            : base(gameController)
        {
            this.controller = gameController;
        }

        public void Win()
        {
            onDealerWinHand?.Invoke(this);
        }

        public void Lost()
        {
            onDealerLoseHand?.Invoke(this);
        }

        public void Push()
        {
            this.Result = Result.Push;
            onPushHand?.Invoke(this);
        }

        public void Blackjack()
        {
            this.Result = Result.Blackjack;
            var args = new OnCardReceivedEventArgs(this, null);
            onDealerBlackjack?.Invoke(this, args);
        }


        public override bool CheckIsBust()
        {

            if (CurrentScore > 21)
            {
                this.Result = Result.Bust;
                var args = new OnCardReceivedEventArgs(this, null);
                onDealerBust?.Invoke(this, args);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
