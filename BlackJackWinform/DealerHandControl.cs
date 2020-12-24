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
    public partial class DealerHandControl : HandControl
    {
        private readonly DealerHand Hand;
        
        public DealerHandControl(DealerHand hand, GameController controller,BlackJackForm form): base(hand, controller,form)
        {
            this.Hand = hand;
            hand.onCardReceived += dealer_onCardReceived;
            lblOutcome.Text = string.Empty;
            lblOutcome.Visible = false;
            hand.onBust += dealer_onBust;
            hand.onBlackjack += dealer_onBlackjack;
            hand.onWinHand += hand_onWinHand;
            hand.onLoseHand += hand_onLoseHand;
            hand.onPushHand += hand_onPushHand;
        }

        void hand_onPushHand(Hand hand)
        {
            lblOutcome.Text = "Push";
        }

        void hand_onLoseHand(Hand hand)
        {
            lblOutcome.Text = hand.CurrentScore.ToString();
            lblOutcome.Visible = true;
        }

        void hand_onWinHand(Hand hand)
        {
            lblOutcome.Text = hand.CurrentScore.ToString();
            lblOutcome.Visible = true;
        }

        void dealer_onBust(object sender, OnCardReceivedEventArgs args)
        {
            lblOutcome.Text = "Bust with " + args.Hand.CurrentScore;
            lblOutcome.Visible = true;
        }

        void dealer_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            lblOutcome.Text = "Blackjack";
            lblOutcome.Visible = true;
        }

        void dealer_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            try
            {
                if (args.Hand.CardCount == 2)
                {
                    this.AddCard(args.Card, false);
                }
                else
                {
                    this.AddCard(args.Card);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public void ShowAllCards()
        {
            this.pictureBoxList.Clear();
            pnlHand.Controls.Clear();
            foreach (var card in Hand.Cards)
            {
                AddCard(card, true);
            }
        }

    }
}
