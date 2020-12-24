using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public class PlayerHand : Hand
    {
        public event GameEvents.OnPushHand onPushHand;
        public event GameEvents.OnWinHand onWinHand;
        public event GameEvents.OnLoseHand onLoseHand;
        private Bet currentBet;

        public PlayerHand(Player player, GameController gameController)
            : base(gameController)
        {
            this.onBlackjack += PlayerHand_onBlackjack;
            this.onBust += PlayerHand_onBust;
            this.controller = gameController;
            currentBet = new Bet(this, controller);
        }

        void PlayerHand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            base.Result = Result.Bust;
            controller.ActivateNextHand();
        }

        void PlayerHand_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            //this.CurrentBet.Amount = this.CurrentBet.Amount * 1.5;
            controller.PlayerbankRoll += this.CurrentBet.Amount * 2.5;
            if (!controller.Dealer.ActiveHand.IsBlackjack)
                base.Result = Result.Blackjack;

            args.Hand.IsFinalized = true;
            controller.ActivateNextHand();
        }

        public void PushHand()
        {
            controller.PlayerbankRoll += this.CurrentBet.Amount;
            base.Result = Result.Push;
            onPushHand(this);
        }

        public void WinHand()
        {
            controller.PlayerbankRoll += this.CurrentBet.Amount * 2;
            base.Result = Result.Win;
            onWinHand(this);
        }

        public Bet CurrentBet
        {
            get
            {
                return currentBet;
            }
            set
            {
                currentBet = value;
            }
        }

        public void LoseHand()
        {
            base.Result = Result.Lost;
            try
            {
                onLoseHand(this);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
