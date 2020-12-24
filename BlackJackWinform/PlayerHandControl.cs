using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blackjack.Core;

namespace BlackJackWinform
{
    public partial class PlayerHandControl : HandControl
    {
        public readonly PlayerHand Hand;
        
        public PlayerHandControl(PlayerHand hand, int bet, GameController controller, BlackJackForm blackjackForm)
            : base(hand,controller,blackjackForm)
        {
            InitializeComponent();
            this.Hand = hand;
            hand.CurrentBet.onBetChanged += CurrentBet_onBetChanged;
            hand.onCardReceived +=hand_onCardReceived;
            lblBet.Visible = true;
            lblBet.Text = bet.ToString("c0");
            hand.CurrentBet.Amount = bet;
            hand.onBust += hand_onBust;
            hand.onBlackjack += hand_onBlackjack;
            hand.onWinHand += hand_onWinHand;
            hand.onLoseHand += hand_onLoseHand;
            hand.onPushHand += hand_onPushHand;
            this.IsActive = true;
        }

        public void Split(PlayerHand newHand)
        {
            this.IsActive = false;
            //Take card from old hand
            newHand.ReceiveCard(this.Hand.Cards[1], true);
            this.pictureBoxList.Remove(pictureBoxList.Find(i => i.Tag == this.Hand.Cards[1]));
            this.Hand.Cards.RemoveAt(1);
            pnlHand.Controls.RemoveAt(0);
            //"Old" hand gets new card
            this.Hand.ReceiveCard(Controller.Shoe.NextCard, true);
            //New hand gets second card
            newHand.ReceiveCard(Controller.Shoe.NextCard, true);
        }

        void hand_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "+ " + this.Hand.CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Blackjack!";
            this.IsActive = false;
        }

        void hand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "-" + ((PlayerHand)args.Hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Bust with " + args.Hand.CurrentScore;
            this.IsActive = false;
        }

        void hand_onPushHand(Hand hand)
        {
            lblWinning.Text = "$0";
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Push";
            this.IsActive = false;
        }

        void hand_onLoseHand(Hand hand)
        {
            lblWinning.Text = "- " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Lose with " + hand.CurrentScore;
            this.IsActive = false;
        }

        void hand_onWinHand(Hand hand)
        {
            lblWinning.Text = "+ " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Win with " + hand.CurrentScore;
            this.IsActive = false;
        }

        void hand_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            AddCard(args.Card);
            if (args.Hand.Cards.Count == 2)
            {
                if (args.Hand.Cards[0].CardType == args.Hand.Cards[1].CardType
                    && args.Hand.Cards[0].Value == args.Hand.Cards[1].Value)
                {
                    this.BlackJackForm.btnSplit.Enabled = true;
                }
                this.BlackJackForm.btnDoubleDown.Enabled = true;
            }
            else
            {
                this.BlackJackForm.btnDoubleDown.Enabled = this.BlackJackForm.btnSplit.Enabled = false;
            }
        }

        void CurrentBet_onBetChanged(object sender, OnBetChangedEventArgs args)
        {
            lblBet.Text = args.NewBet.Amount.ToString("c0");
        }

        public bool IsActive
        {
            get { return this.lblActive.Visible; }
            set
            {
                this.lblActive.Visible = value;
            }
        }
    }
}
