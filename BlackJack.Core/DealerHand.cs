using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public class DealerHand : Hand
    {
        public event GameEvents.OnPushHand onPushHand;
        public event GameEvents.OnWinHand onWinHand;
        public event GameEvents.OnLoseHand onLoseHand;
        
        public DealerHand(Dealer dealer, GameController gameController)
            : base(gameController)
        {
            this.controller = gameController;
            this.onBust += DealerHand_onBust;
            this.onBlackjack += DealerHand_onBlackjack;
        }

        void DealerHand_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            controller.PlayerOne.CurrentHands.ForEach(i =>
            {
                ((PlayerHand)i).Result = Core.Result.DealerBlackjack;
                ((PlayerHand)i).LoseHand();
            }
          );
        }

        void DealerHand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            controller.PlayerOne.CurrentHands.ForEach(i =>
                {
                    if (!i.IsBust && !i.IsBlackjack)
                    {
                       // controller.PlayerbankRoll += i.CurrentBet.Amount * 2;
                        ((Hand)i).Result = Core.Result.Win;

                        ((PlayerHand)i).WinHand();
                    }
                }
               );
        }

        public void PushHand()
        {
            onPushHand(this);
        }

        public void WinHand()
        {
            onWinHand(this);
        }

        public void LoseHand()
        {
            onLoseHand(this);
        }
    }
}
