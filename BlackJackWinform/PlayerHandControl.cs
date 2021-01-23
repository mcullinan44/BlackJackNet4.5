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
        public readonly PlayerHand PlayerHand;
        
        public PlayerHandControl(PlayerHand playerHand, GameController controller, BlackJackForm blackjackForm)
            : base(playerHand, controller,blackjackForm)
        {
            InitializeComponent();
            this.PlayerHand = playerHand;
            
            lblBet.Visible = true;
            lblBet.Text = playerHand.CurrentBet.Amount.ToString("c0");

            controller.onBetChanged += Controller_onBetChanged;




            playerHand.onCardReceived += hand_onCardReceived;


            playerHand.onBust += hand_onBust;
            playerHand.onBlackjack += hand_onBlackjack;
            playerHand.onWinHand += hand_onWinHand;
            playerHand.onLoseHand += hand_onLoseHand;
            playerHand.onPushHand += hand_onPushHand;



            controller.onGameEnd += Controller_onGameEnd;


            this.IsActive = playerHand.IsActive;
        }


        private void Controller_onGameEnd(object sender, EventArgs e)
        {
            btnDoubleDown.Enabled = false;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnSplit.Enabled = false;
        }

        private void Controller_onBetChanged(object sender, OnBetChangedEventArgs args)
        {
            lblBet.Text = args.Bet.Amount.ToString("c0");
        }

        public Card TakeLastCard()
        {
            this.IsActive = false;

            //get the last card from this hand
            var result = this.PlayerHand.Cards[1];


            this.PlayerHand.Cards.Remove(result);

         
            //Take card from old hand
            //newHand.ReceiveCard(this.Hand.Cards[1], true);
            
            this.pictureBoxList.Remove(pictureBoxList.Find(i => i.Tag == result));
            
            pnlHand.Controls.RemoveAt(0);


            return result;
        }

        void hand_onBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "+ " + this.PlayerHand.CurrentBet.Amount.ToString("c0");
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
                    this.btnSplit.Enabled = true;
                }
                this.btnDoubleDown.Enabled = true;
            }
            else
            {
                this.btnDoubleDown.Enabled = this.btnSplit.Enabled = true;
            }
        }

        public bool IsActive
        {
            get { return this.lblActive.Visible; }
            set
            {
                if(value)
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
                    this.pnlHand.BackColor = Color.Yellow;
                }
                
                this.lblActive.Visible = value;
            }
        }
    }
}
