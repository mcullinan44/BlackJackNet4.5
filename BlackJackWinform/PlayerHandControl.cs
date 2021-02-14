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

  

            playerHand.onBetChanged += PlayerHand_onBetChanged;

            playerHand.onCardReceived += hand_onCardReceived;
            playerHand.onBust += hand_onBust;
            playerHand.onBlackjack += hand_onBlackjack;
            playerHand.onWinHand += hand_onWinHand;
            playerHand.onLoseHand += hand_onLoseHand;
            playerHand.onPushHand += hand_onPushHand;

            playerHand.onActivate += PlayerHand_onActivate;
            


            controller.onGameEnd += Controller_onGameEnd;


            this.IsPlaying = playerHand.State == State.Playing;
        }

        private void PlayerHand_onBetChanged(object sender, OnBetChangedEventArgs args)
        {
            lblBet.Text = (args.Bet.Amount * 1).ToString("c0");
        }

        private void PlayerHand_onActivate(object sender, EventArgs e)
        {

            ActivateButtons();

        }


        public void ActivateButtons()
        {

            btnHit.Enabled = true;
            btnStand.Enabled = true;
            //can't split more than two times.
            if(Controller.ActivePlayer.CurrentHands.Count == 3)
            {
                btnSplit.Enabled = false;
            }


            if (PlayerHand.Cards.Count == 2)
            {
                if (PlayerHand.Cards[0].CardType == PlayerHand.Cards[1].CardType
                    && PlayerHand.Cards[0].Value == PlayerHand.Cards[1].Value)
                {
                    this.btnSplit.Enabled = true;
                }
                
                
                this.btnDoubleDown.Enabled = true;

            }
            else
            {
                
                ///overried for testing (enable these)
                this.btnDoubleDown.Enabled = this.btnSplit.Enabled = false;
            }


        }

        public void DeactivateButtons()
        {
            btnHit.Enabled = btnSplit.Enabled = btnDoubleDown.Enabled = btnStand.Enabled = false;
        }


        private void Controller_onGameEnd(object sender, EventArgs e)
        {
            btnDoubleDown.Enabled = false;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnSplit.Enabled = false;
        }

  

        public Card TakeLastCard()
        {
           

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
            this.IsPlaying = false;
        }

        void hand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            lblWinning.Text = "-" + ((PlayerHand)args.Hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Bust with " + args.Hand.CurrentScore;
            this.IsPlaying = false;
        }

        void hand_onPushHand(Hand hand)
        {
            lblWinning.Text = "$0";
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Push";
            this.IsPlaying = false;
        }

        void hand_onLoseHand(Hand hand)
        {
            lblWinning.Text = "- " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Lose with " + hand.CurrentScore;
            this.IsPlaying = false;
        }

        void hand_onWinHand(Hand hand)
        {
            lblWinning.Text = "+ " + ((PlayerHand)hand).CurrentBet.Amount.ToString("c0");
            lblWinning.Visible = lblOutcome.Visible = true;
            lblOutcome.Text = "Win with " + hand.CurrentScore;
            this.IsPlaying = false;
        }

        void hand_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            AddCard(args.Card);

            if(PlayerHand.State == State.Playing)
            {
                ActivateButtons();
            }
        }

        public bool IsPlaying
        {
            get { return this.lblActive.Visible; }
            set
            {
                if(value)
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
             
                }
                else
                {
                    DeactivateButtons();
                }
                
                this.lblActive.Visible = value;

                
            }
        }
    }
}
