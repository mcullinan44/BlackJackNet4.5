using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Blackjack.Core;
using Blackjack.Core.Counting;
using Blackjack.Core.ShoeData;

namespace BlackJackWinform
{
    public partial class BlackJackForm : Form
    {
        private List<PlayerHandControl> playerHandControlList = new List<PlayerHandControl>();
        private DealerHandControl dealerHandControl;
        private readonly GameController controller;
        private readonly BindingList<ShoeRemaining> shoeRemainingBindingList;
        private readonly BindingSource shoeRemainingBindingSource;
        private readonly BindingList<BaseCardCountingStrategy> cardCountingStrategyBindingList;
        private readonly BindingSource cardCountingStrategyBindingSource;
        /*
         * 
         * KNOWN BUGS
         * -If Blackjack and player get blackjack, the player loses.
         * -Aces dealt as hit cards are not treated as 11s.
         */
        //Bug occurs when splitting into a blackjack
        public BlackJackForm()
        {
            InitializeComponent();
            this.controller = new GameController();
            controller.onBankrollchange += Instance_OnBankrollChange;
            controller.onShowAllCards += Instance_OnShowAllCards;
            controller.onGameEnd += controller_onGameEnd;
            controller.MinimumBet = Defaults.MinimumBet;
            controller.NumberOfDecks = Defaults.NumberOfDecks;
            controller.PlayerbankRoll = Defaults.Bankroll;
            controller.StartNewSession(6, 10);
            controller.ShuffleAll();
            var collection = new ShoeRemainingCollection(controller);
            collection.ShoeRemainingBindingList.ListChanged += ShoeRemainingBindingList_ListChanged;
            tbBet.Text = controller.MinimumBet.ToString("c0");
            this.shoeRemainingBindingSource = new BindingSource();
            this.shoeRemainingBindingSource.DataSource = collection.ShoeRemainingBindingList;
            this.dgvShoeRemaining.DataSource = this.shoeRemainingBindingSource;

            var countStrategyCollection = new CardCountingMethodCollection(controller);
            this.cardCountingStrategyBindingSource = new BindingSource();
            this.cardCountingStrategyBindingSource.DataSource = countStrategyCollection.CardCountingStrategyBindingList;
            this.dgvCountingScenarios.DataSource = this.cardCountingStrategyBindingSource;
            pnlAction.Visible = false;
        }

        void ShoeRemainingBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
         


        }


        #region User Events

        private void btnHit_Click(object sender, EventArgs e)
        {
            controller.Hit();
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            playerHandControlList.Find(i => i.Hand == controller.PlayerOne.ActiveHand).IsActive = false;
            controller.Stand();
            var hand = controller.ActivateNextHand();

            var newHand = playerHandControlList.Find(i => i.Hand == hand);
            if (newHand != null)
            {
                newHand.IsActive = true;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            var newhand = (PlayerHand)controller.CreateNewHandForSplit();
            var newHandControl = new PlayerHandControl(newhand, int.Parse(tbBet.Text), controller, this);
            newHandControl.IsActive = true;
            playerHandControlList.Add(newHandControl);
            layout.Controls.Add(newHandControl);
            var splittingHandControl = playerHandControlList.Find(i => i.Hand == controller.PlayerOne.ActiveHand);
            controller.PlayerOne.ActiveHand.IsActive = false;
            newhand.IsActive = true;
            splittingHandControl.Split(newhand);
        }

        private void btnDoubleDown_Click(object sender, EventArgs e)
        {
            playerHandControlList.Find(i => i.Hand == controller.PlayerOne.ActiveHand).IsActive = false;
            controller.DoubleDown();
            var hand = controller.ActivateNextHand();
            var newHand = playerHandControlList.Find(i => i.Hand == hand);
            if(newHand != null)
            {
                newHand.IsActive = true;
            }
        }

        private void newSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBet.Enabled = true;
            tbBet.Enabled = true;
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            Console.WriteLine("--------------------------------------");
            btnBet.Enabled = false;
            btnHit.Enabled = true;
            btnStand.Enabled = true;
            btnDoubleDown.Enabled = true;
            if (controller.Shoe.UndealtCards.Count < 10)
                controller.ShuffleAll();
            controller.StartNewHand();
            controller.Dealer.ActiveHand.onBust += controller_onGameEnd;
            controller.Dealer.ActiveHand.onBust += Instance_OnShowAllCards;
            controller.Dealer.ActiveHand.onBlackjack += controller_onGameEnd;
            controller.Dealer.ActiveHand.onBlackjack += Instance_OnShowAllCards;
            controller.onActiveHandChanged +=controller_OnActiveHandChanged;
            dealerLayoutPanel.Controls.Clear();
            dealerHandControl = new DealerHandControl(controller.Dealer.ActiveHand, controller, this);
            dealerLayoutPanel.Controls.Add(dealerHandControl);
            foreach(var control in playerHandControlList)
            {
                control.EndGame();
            }
            layout.Controls.Clear();
            var playerHandControl = new PlayerHandControl(controller.PlayerOne.ActiveHand, int.Parse(tbBet.Text), controller,this);
            playerHandControlList.Add(playerHandControl);
            layout.Controls.Add(playerHandControl);
            controller.Deal();
   
            pnlAction.Visible = true;
        }

        void ActiveHand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            var newHand = playerHandControlList.Find(i => i.Hand == controller.PlayerOne.ActiveHand);
            if (newHand != null)
            {
                newHand.IsActive = true;
            }
        }

        void controller_OnActiveHandChanged(object sender, EventArgs e)
        {
            if (controller.PlayerOne.ActiveHand.Cards.Count == 2)
            {
                if (controller.PlayerOne.ActiveHand.Cards[0].CardType == controller.PlayerOne.ActiveHand.Cards[1].CardType
                    && controller.PlayerOne.ActiveHand.Cards[0].Value == controller.PlayerOne.ActiveHand.Cards[1].Value)
                {
                    btnSplit.Enabled = true;
                }

                btnDoubleDown.Enabled = true;
                btnSplit.Enabled = false;
            }
            else
            {
                btnDoubleDown.Enabled = false;
                btnSplit.Enabled = false;
            }
            var activeHand = (PlayerHand)controller.PlayerOne.ActiveHand;
            btnDoubleDown.Enabled = true;
            var activeControl = playerHandControlList.Find(i => i.Hand.IsActive);
            activeControl.IsActive = true;
        }

        #endregion

        #region Game Events

        void controller_onGameEnd(object sender, EventArgs e)
        {
            tbBet.Enabled = true;
            btnBet.Enabled = true;
            btnBet.Visible = true;
            btnDoubleDown.Enabled = false;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnSplit.Enabled = false;
            pnlAction.Visible = false;
        }


        void Instance_OnShowAllCards(object sender, EventArgs e)
        {
            dealerHandControl.ShowAllCards();
        }

        void Instance_OnBankrollChange(object sender, EventArgs e)
        {
            lblBankroll.Text = controller.PlayerbankRoll.ToString("c0");
        }

        #endregion
    }
}
