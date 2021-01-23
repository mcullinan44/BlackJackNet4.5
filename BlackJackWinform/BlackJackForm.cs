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
            this.controller = new GameController(6, 10);


            controller.onBankrollChange += Instance_OnBankrollChange;
            controller.onShowAllCards += Instance_OnShowAllCards;
            controller.onGameEnd += controller_onGameEnd;
            controller.MinimumBet = Defaults.MinimumBet;
            controller.NumberOfDecks = Defaults.NumberOfDecks;


            controller.AddPlayer("matt", Defaults.Bankroll);
            controller.ShuffleAll();


            var collection = new ShoeRemainingCollection(controller);
            collection.ShoeRemainingBindingList.ListChanged += ShoeRemainingBindingList_ListChanged;
            tbBet.Text = controller.MinimumBet.ToString("c0");
            lblBankroll.Text = controller.PlayerList[0].PlayerbankRoll.ToString("c0");


            this.shoeRemainingBindingSource = new BindingSource();
            this.shoeRemainingBindingSource.DataSource = collection.ShoeRemainingBindingList;
           // this.dgvShoeRemaining.DataSource = this.shoeRemainingBindingSource;

            var countStrategyCollection = new CardCountingMethodCollection(controller);
            this.cardCountingStrategyBindingSource = new BindingSource();
            this.cardCountingStrategyBindingSource.DataSource = countStrategyCollection.CardCountingStrategyBindingList;
            //this.dgvCountingScenarios.DataSource = this.cardCountingStrategyBindingSource;
            //pnlAction.Visible = false;
        }

        void ShoeRemainingBindingList_ListChanged(object sender, ListChangedEventArgs e)
        {
         


        }

        #region User Events

        private void btnHit_Click(object sender, EventArgs e)
        {
            controller.Hit(controller.ActivePlayer.ActiveHand);
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
           
        }


        private PlayerHandControl ActivePlayerHandControl
        {
            get
            {
                return playerHandControlList.Find(i => i.PlayerHand == controller.ActivePlayer.ActiveHand);
            }

        }

        private void btnDoubleDown_Click(object sender, EventArgs e)
        {

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
     
            if (controller.Shoe.UndealtCards.Count < 10)
                controller.ShuffleAll();

            controller.StartNewHand();



            dealerLayoutPanel.Controls.Clear();
            dealerHandControl = new DealerHandControl(controller.Dealer.Hand, controller, this);


            controller.Dealer.Hand.onDealerBlackjack += controller_onGameEnd;
            controller.Dealer.Hand.onDealerBlackjack += Instance_OnShowAllCards;
            controller.Dealer.Hand.onDealerBust += controller_onGameEnd;
            controller.Dealer.Hand.onDealerBust += Instance_OnShowAllCards;


            dealerLayoutPanel.Controls.Add(dealerHandControl);
            foreach(var control in playerHandControlList)
            {
                control.EndGame();
            }
            layout.Controls.Clear();


            controller.IncreaseBet(controller.PlayerList[0].ActiveHand.CurrentBet, double.Parse(tbBet.Text));

            var playerHandControl = new PlayerHandControl(controller.ActivePlayer.ActiveHand, controller,this);



            playerHandControlList.Add(playerHandControl);
            layout.Controls.Add(playerHandControl);
            playerHandControl.btnDoubleDown.Enabled = true;
            playerHandControl.btnHit.Enabled = true;
            playerHandControl.btnStand.Enabled = true;
            
            controller.Deal();
   
           // pnlAction.Visible = true;
        }

        private void Hand_onDealerBlackjack(object sender, OnCardReceivedEventArgs args)
        {
            
        }

        public void SplitHand(PlayerHandControl sourceHandControl)
        {

            //take a card from the source hand
            Card cardToMoveToNewHand = sourceHandControl.TakeLastCard();


            //create a new hand for the active player
            PlayerHand newHand = controller.AddHandToPlayer(controller.ActivePlayer, State.NotYetPlayed);
            newHand.State = State.NotYetPlayed;
            //add the card that was removed from the source to the new hand
            var newPlayerHandControl = new PlayerHandControl(newHand, controller, this);

            controller.GivePlayerACard(newHand, cardToMoveToNewHand);

            newPlayerHandControl.Visible = true;
            playerHandControlList.Add(newPlayerHandControl);
            layout.Controls.Add(newPlayerHandControl);

            controller.IncreaseBet(newHand.CurrentBet, double.Parse(tbBet.Text));
            
            newPlayerHandControl.btnDoubleDown.Enabled = false;
            newPlayerHandControl.btnHit.Enabled = false;
            newPlayerHandControl.btnStand.Enabled = false;
            newPlayerHandControl.btnSplit.Enabled = false;

     




            //deal a new card to the old hand
            //controller.GivePlayerACard(sourceHandControl.Hand);

            //PlayerHand ph = new PlayerHand(controller.ActivePlayer, controller);
            //controller.ActivePlayer.CurrentHands.Add(ph);





        }



        void ActiveHand_onBust(object sender, OnCardReceivedEventArgs args)
        {
            var newHand = playerHandControlList.Find(i => i.PlayerHand == controller.ActivePlayer.ActiveHand);
            if (newHand != null)
            {
                newHand.IsActive = true;
            }
        }

        //void controller_OnActiveHandChanged(object sender, EventArgs e)
        //{
        //    if (controller.ActivePlayer.ActiveHand.Cards.Count == 2)
        //    {
        //        if (controller.ActivePlayer.ActiveHand.Cards[0].CardType == controller.ActivePlayer.ActiveHand.Cards[1].CardType
        //            && controller.ActivePlayer.ActiveHand.Cards[0].Value == controller.ActivePlayer.ActiveHand.Cards[1].Value)
        //        {
        //            btnSplit.Enabled = true;
        //        }

        //        btnDoubleDown.Enabled = true;
        //        btnSplit.Enabled = false;
        //    }
        //    else
        //    {
        //        btnDoubleDown.Enabled = false;
        //        btnSplit.Enabled = false;
        //    }
        //    var activeHand = (PlayerHand)controller.ActivePlayer.ActiveHand;
        //    btnDoubleDown.Enabled = true;
        //    var activeControl = playerHandControlList.Find(i => i.Hand.IsActive);
        //    activeControl.IsActive = true;
        //}

        #endregion

        #region Game Events

        void controller_onGameEnd(object sender, EventArgs e)
        {
            tbBet.Enabled = true;
            btnBet.Enabled = true;
            btnBet.Visible = true;


        }


        void Instance_OnShowAllCards(object sender, EventArgs e)
        {
            dealerHandControl.ShowAllCards();
        }

        void Instance_OnBankrollChange(object sender, OnBankrollChangedEventArgs e)
        {
            lblBankroll.Text = e.Player.PlayerbankRoll.ToString("c0");
        }

        #endregion
    }
}
