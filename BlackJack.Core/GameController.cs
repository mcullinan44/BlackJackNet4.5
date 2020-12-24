using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class GameController
    {
        public event GameEvents.OnBankrollChange onBankrollchange;
        public event GameEvents.OnShowAllCards onShowAllCards;
        public event GameEvents.OnActiveHandChanged onActiveHandChanged;
        public event GameEvents.OnGameEnd onGameEnd;
        public event GameEvents.OnCardReceived onCardReceived;
        public event GameEvents.OnShuffle onShuffle;

        private readonly Player player;
        private readonly Dealer dealer;
        private readonly int minimumBet;
        private double bankroll = 0;

        public GameController()
        {
            this.player = new Player(1);
            this.dealer = new Dealer();  
        }

        void ActiveHand_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            if (onCardReceived != null)
                onCardReceived(sender, args);
        }

        #region Properties

        public Player PlayerOne
        {
            get { return player; }
        }

        public Dealer Dealer
        {
            get { return dealer; }
        }

        public int NumberOfDecks { get; set; }

        public int MinimumBet { get;  set;  }

        public Shoe Shoe
        {
            get;
            set;
        }

        public double PlayerbankRoll
        {
            get { return bankroll; }
            set
            {
                bankroll = value;
                onBankrollchange(this, null);
            }
        }

        #endregion

        #region Methods

        public void ShuffleAll()
        {
            foreach (var deck in Shoe.DeckList)
            {
                deck.Shuffle();
            }
            if (onShuffle != null)
                onShuffle(this, null);
        }

        public void StartNewSession(int numberOfDecks, int minimumBet)
        {
            this.Shoe = new Shoe(numberOfDecks);
            var dealer = new Dealer();
            var player = new Player(1);
        }

        public void StartNewHand()
        {
            PlayerOne.CurrentHands.Clear();
            
            DealerHand dh = new DealerHand(dealer, this);
            dh.IsActive = true;
            dealer.ActiveHand = dh;
            PlayerHand ph = new PlayerHand(player, this);
            ph.IsActive = true;
            player.CurrentHands.Add(ph);

            this.player.ActiveHand.onCardReceived += ActiveHand_onCardReceived;
            this.dealer.ActiveHand.onCardReceived += ActiveHand_onCardReceived;
        }

        public void Deal()
        {
            bool isPlayersCard = true;
            for (var i = 0; i < 4; i++)
            {
                Card card = null;
                if(isPlayersCard)
                {
                    card = this.Shoe.NextCard;
                    
                    //if(player.ActiveHand.Cards.Count == 0)
                    //{
                    //    card = this.Shoe.GetAnAce();
                    //}
                    //else
                    //{
                    //    card = this.Shoe.SplitTester(this.Shoe.GetAnAce());
                    //}
                    
                    
                    //  var card = player.ActiveHand.Cards.Count == 1 ? this.Shoe.SplitTester(player.ActiveHand.Cards[0]) : this.Shoe.NextCard;

                    //testing



                    player.ActiveHand.ReceiveCard(card, true);
                    isPlayersCard = false;
                }
                else
                {
                    card = this.Shoe.NextCard;
                    dealer.ActiveHand.ReceiveCard(card, true);
                    isPlayersCard = true;
                }
            }
        }

        public PlayerHand CreateNewHandForSplit()
        {
            var hand = PlayerOne.CurrentHands.Find(i => i.IsActive);
            var newHand = new PlayerHand(PlayerOne, this);
            PlayerOne.CurrentHands.Add(newHand);
            Bet bet = new Bet(newHand, this) { ChipList = hand.CurrentBet.ChipList, Amount = hand.CurrentBet.Amount };
            newHand.CurrentBet = bet;

            return newHand;
        }

        public void Hit()
        {
           var card = Shoe.NextCard;
           PlayerOne.ActiveHand.ReceiveCard(card, false);
        }

        public void Stand()
        {
            PlayerOne.ActiveHand.IsFinalized = true;
        }

        public void DoubleDown()
        {
            PlayerOne.ActiveHand.CurrentBet.Amount *= 2;
            PlayerOne.ActiveHand.ReceiveCard(Shoe.NextCard, false);
            PlayerOne.ActiveHand.IsFinalized = true;
        }

        public Hand ActivateNextHand()
        {
            var currentHand = PlayerOne.ActiveHand;
            var nextHand = PlayerOne.CurrentHands.LastOrDefault(v => !v.IsFinalized);
            if (nextHand != null)
            {
                currentHand.IsActive = false;
                nextHand.IsActive = true;
                onActiveHandChanged(this, null);
            }
            else if(PlayerOne.CurrentHands.Find(v => !v.IsBlackjack) != null 
                && PlayerOne.CurrentHands.Find(v => !v.IsBust) != null)
            {
                
                playForDealer();
                if(!Dealer.ActiveHand.IsBust)
                    calculateScore();
                onGameEnd(this, null);
            }
            else {
                onGameEnd(this, null);
            }
            return nextHand;
        }

        #endregion

        #region PrivateMethods

        private void playForDealer()
        {
            onShowAllCards(this, null);
            Dealer dealer = (Dealer)this.Dealer;
            while (!Dealer.ActiveHand.IsBust && Dealer.ActiveHand.CurrentScore <= 16)
            {
                var card = Shoe.NextCard;
                dealer.ActiveHand.ReceiveCard(card, false);
            }
        }

        private void calculateScore()
        {
            var dealerScore = Dealer.ActiveHand.CurrentScore;
            foreach (PlayerHand i in PlayerOne.CurrentHands)
            {
                if (!i.IsBust && !i.IsBlackjack)
                {
                    if (i.CurrentScore > dealerScore)
                    {
                        i.WinHand();
                        ((DealerHand)Dealer.ActiveHand).LoseHand();
                        
                    }
                    else if (i.CurrentScore == dealerScore)
                    {
                        i.PushHand();
                        ((DealerHand)Dealer.ActiveHand).PushHand();
                    }
                    else
                    {
                        i.LoseHand();
                        ((DealerHand)Dealer.ActiveHand).WinHand();
                    }
                }
            }
        }


        #endregion

    }
}
