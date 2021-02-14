using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class GameController
    {
        public event GameEvents.OnBankrollChange onBankrollChange;
        public event GameEvents.OnShowAllCards onShowAllCards;
        public event GameEvents.OnGameEnd onGameEnd;
        public event GameEvents.OnDealerCardReceived onDealerCardReceived;
        public event GameEvents.OnShuffle onShuffle;


        public GameController(int numberOfDecks, int minimumBet)
        {
            this.PlayerList = new List<Player>();
            this.Shoe = new Shoe(numberOfDecks);
            this.Dealer = new Dealer();  
        }


        #region Properties


        public void AddPlayer(string name, double bankRoll)
        {
            var newPlayer = new Player(1);
            newPlayer.Name = name;
            newPlayer.PlayerbankRoll = bankRoll;
            PlayerList.Add(newPlayer);
        }


        public List<Player> PlayerList { get; }

        public Player ActivePlayer
        {
            get { return PlayerList.Where(i => i.IsActive).FirstOrDefault();  }
        }

        public Dealer Dealer { get; }

        public int NumberOfDecks { get; set; }

        public int MinimumBet { get;  set;  }

        public Shoe Shoe
        {
            get;
            set;
        }


        #endregion

        #region Methods

        public void ShuffleAll()
        {
            foreach (var deck in Shoe.DeckList)
            {
                deck.Shuffle();
            }
            onShuffle?.Invoke(this, null);
        }

        public void StartNewHand()
        {
            DealerHand dh = new DealerHand(Dealer, this);
            Dealer.Hand = dh;
            foreach(var player in PlayerList)
            {
                player.CurrentHands.Clear();

                AddHandToPlayer(player, State.Playing);

            }

            PlayerList[0].IsActive = true;
        }

        public PlayerHand AddHandToPlayer(Player player, State state)
        {
            var result = new PlayerHand(player, this);
            result.State = state;
            player.CurrentHands.Add(result);
            return result;
        }

        private void dealARoundOfCards()
        {

            //deal second card to each player
            foreach (var player in PlayerList)
            {
                GivePlayerNextCardInShoe(player.ActiveHand, false);
        
            }


            //then to the dealer
            giveDealerACard();
        }

        public void Deal()
        {
            var dealerBlackJack = false;
       
            dealARoundOfCards();
       

            dealARoundOfCards();


            //check if the dealer has blackjack.
            if (Dealer.Hand.CurrentScore == 21)
            {
                dealerBlackJack = true;
            }

            //check if any players have blackjack
            foreach (var player in PlayerList)
            {
                if(player.ActiveHand.CurrentScore == 21)
                {
                    
                    //if both the dealer and the player have blackjack, then PUSH.
                    if(dealerBlackJack)
                    {
                        player.ActiveHand.Push();

                    }
                    //Otherwise, the player wins 1.5x his original bet
                    else
                    {
                        player.ActiveHand.Blackjack();
                        

                        //TODO: Need an onPotchange event to denote the house giving the extra half

                        adjustPlayerBankRoll(player, player.ActiveHand.CurrentBet.Amount * 2.5);
                    }
                }
                else //player does not have blackjack, but the dealer does. The player loses.
                {
                    if(dealerBlackJack)
                    {
                        //give dealer blackjack
                        Dealer.Hand.Blackjack();


                        //player loses
                        player.ActiveHand.Lost();
                    }
                }
            }

            var moreToPlay = PlayerList.Any(i => i.CurrentHands.Any(x => x.Result == Result.Undetermined));
            if(!moreToPlay)
            {
                onGameEnd?.Invoke(this, null);
            }
        }


        private void adjustPlayerBankRoll(Player player, double amount)
        {
            player.PlayerbankRoll += amount;
            var bargs = new OnBankrollChangedEventArgs(player);
            onBankrollChange?.Invoke(this, bargs);
        }


        public void GivePlayerNextCardInShoe(PlayerHand playerHand, bool checkBlackJackImmediately)
        {
            Card card = this.Shoe.NextCard;

            playerHand.AddCard(card);


            if(checkBlackJackImmediately)
            {
                if(playerHand.State == State.Playing && playerHand.CurrentScore == 21)
                {
                    playerHand.Blackjack();
                    FinishHand(playerHand.Player);
                }
            }
        }

        public void GivePlayerACard(PlayerHand playerHand, Card card)
        {
            playerHand.AddCard(card);
        }


        private void giveDealerACard()
        {
            Card card = this.Shoe.NextCard;
            Dealer.Hand.Cards.Add(card);
            var args = new OnCardReceivedEventArgs(Dealer.Hand, card);
            onDealerCardReceived?.Invoke(this, args);

        }

        

        public void IncreaseBet(PlayerHand hand, double amountToIncrease)
        {
            hand.IncreaseBet(amountToIncrease);

            hand.Player.PlayerbankRoll -= amountToIncrease;
            var bargs = new OnBankrollChangedEventArgs(hand.Player);
            onBankrollChange?.Invoke(this, bargs);
        }


        public void FinishHand(Player player)
        {
            var nextHand = player.CurrentHands.FirstOrDefault(i => i.State == State.NotYetPlayed);
            //move to the next player if there are no more unresolved hands.
            if (nextHand == null)
            {
                player.IsActive = false;
                
                //if there are hands that have not busted or blackjacked
                var nextPlayer = PlayerList.FirstOrDefault(i => i.CurrentHands.Any(x => x.State == State.NotYetPlayed ));
                var everythingIsbusted = !PlayerList.Any(i => i.CurrentHands.Any(x => x.Result != Result.Bust));


                //If all hands have busted, end the game and don't play for the dealer
                if (everythingIsbusted)
                {
                    //do nothing
                    onGameEnd?.Invoke(this, null);
                }
                else if (nextPlayer == null)
                {
                    onShowAllCards(this, null);

                    while (!Dealer.Hand.CheckIsBust() && Dealer.Hand.CurrentScore <= 16)
                    {
                        giveDealerACard();
                    }

                    calculateScore();

                    onGameEnd(this, null);
                }
                else
                {
                    //move to the next player
                    nextPlayer.IsActive = true;
                    nextPlayer.CurrentHands[0].State = State.Playing;
                }
            }
            else 
            {
                
                nextHand.State = State.Playing;
            }
        }

        #endregion

        #region PrivateMethods

        private void calculateScore()
        {
            //check if dealer has blackJack
            var dealerScore = Dealer.Hand.CurrentScore;
            foreach(var player in PlayerList)
            {
                foreach (PlayerHand i in player.CurrentHands)
                {
                    if (i.State == State.Stand || i.State == State.Doubled)
                    {
                        if (i.CurrentScore > dealerScore || Dealer.Hand.CheckIsBust())
                        {
                            adjustPlayerBankRoll(player, i.CurrentBet.Amount * 2);
                            i.Win();

                            //TODO: fix, this raises a second event.
                            if(Dealer.Hand.CheckIsBust())
                            {
                                
                            }
                            else
                            {

                                Dealer.Hand.Lost();
                                
                            }
                            
                        }
                        else if (i.CurrentScore == dealerScore)
                        {
                            adjustPlayerBankRoll(player, i.CurrentBet.Amount);
                            i.Push();
                            Dealer.Hand.Push(); 
                        }
                        else
                        {
                            i.Lost();
                            Dealer.Hand.Win();
                        }
                    }
                }
            } 
        }
        #endregion

    }
}
