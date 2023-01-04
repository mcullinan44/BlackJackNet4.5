﻿using System.Collections.Generic;
using System.Linq;
using Blackjack.Core.Entities;

namespace Blackjack.Core
{
    public sealed class GameController
    {
        public event GameEvents.OnBankrollChange OnBankrollChange;
        public event GameEvents.OnShowAllCards OnShowAllCards;
        public event GameEvents.OnGameEnd OnGameEnd;
        public event GameEvents.OnDealerCardReceived OnDealerCardReceived;
        public event GameEvents.OnShuffle OnShuffle;
        public event GameEvents.OnTakeCardForSplit OnTakeCardForSplit;

        public GameController(int numberOfDecks)
        {
            this.PlayerList = new List<Player>();
            this.Shoe = new Shoe(numberOfDecks);
            this.Dealer = new Dealer();  
        }

        #region Properties

        public List<Player> PlayerList { get; }

        public Player ActivePlayer
        {
            get { return PlayerList.FirstOrDefault(i => i.IsActive);  }
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

        public void AddPlayer(string name, double bankRoll)
        {
            Player newPlayer = new Player(1)
            {
                Name = name,
                PlayerbankRoll = bankRoll
            };
            PlayerList.Add(newPlayer);
        }

        public void ShuffleAll()
        {
            foreach (var deck in Shoe.DeckList)
            {
                deck.Shuffle();
            }
            OnShuffle?.Invoke(this, null);
        }

        public void ResetBoard()
        {
            foreach(Player player in PlayerList)
            {
                player.CurrentHands.Clear();
            }
            PlayerList[0].IsActive = true;
        }

        public PlayerHand AddHandToPlayer(Player player, State state)
        {
            PlayerHand result = new PlayerHand(player, this)
            {
                State = state
            };
            player.CurrentHands.Add(result);
            return result;
        }

        private void DealARoundOfCards()
        {
            foreach (Player player in PlayerList)
            {
                GivePlayerNextCardInShoe(player.ActiveHand, false);
        
            }
            GiveDealerACard();
        }

        public void Deal()
        {
            bool dealerBlackJack = false;

            DealARoundOfCards();
            
            DealARoundOfCards();

            //check if the dealer has blackjack.
            if (Dealer.Hand.CurrentScore == 21)
            {
                dealerBlackJack = true;
            }

            //check if any players have blackjack
            foreach (Player player in PlayerList)
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

                        AdjustPlayerBankRoll(player, player.ActiveHand.CurrentBet.Amount * 2.5);
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

            bool moreToPlay = PlayerList.Any(i => i.CurrentHands.Any(x => x.Result == Result.Undetermined));
            if(!moreToPlay)
            {
                OnGameEnd?.Invoke(this, null);
            }
        }

        private void AdjustPlayerBankRoll(Player player, double amount)
        {
            player.PlayerbankRoll += amount;
            OnBankrollChangedEventArgs args = new OnBankrollChangedEventArgs(player);
            OnBankrollChange?.Invoke(this, args);
        }

        public void GivePlayerNextCardInShoe(PlayerHand playerHand, bool checkBlackJackImmediately)
        {
            Card card = Shoe.NextCard;

            playerHand.AddCard(card);

            if (!checkBlackJackImmediately) return;
            if (playerHand.State != State.Playing || playerHand.CurrentScore != 21) return;

            //blackjack
            playerHand.Blackjack();
            FinishHand(playerHand.Player);
        }

        public void SeedSplitHandWithNewCard(PlayerHand playerHand)
        {
            Card result = ActivePlayer.ActiveHand.Cards[1];
            OnCardRemovedForSplitEventArgs args = new OnCardRemovedForSplitEventArgs(result);
            OnTakeCardForSplit?.Invoke(this, args);
            playerHand.AddCard(result);
        }

        private void GiveDealerACard()
        {
            Card card = this.Shoe.NextCard;
            Dealer.Hand.Cards.Add(card);
            OnCardReceivedEventArgs args = new OnCardReceivedEventArgs(Dealer.Hand, card);
            OnDealerCardReceived?.Invoke(this, args);
        }

        public void IncreaseBet(PlayerHand hand, double amountToIncrease)
        {
            hand.IncreaseBet(amountToIncrease);
            hand.Player.PlayerbankRoll -= amountToIncrease;
            OnBankrollChangedEventArgs args = new OnBankrollChangedEventArgs(hand.Player);
            OnBankrollChange?.Invoke(this, args);
        }

        public void FinishHand(Player player)
        {
            PlayerHand nextHand = player.CurrentHands.FirstOrDefault(i => i.State == State.NotYetPlayed);
            //move to the next player if there are no more unresolved hands.
            if (nextHand == null)
            {
                player.IsActive = false;
                
                //if there are hands that have not busted or blackjacked
                Player nextPlayer = PlayerList.FirstOrDefault(i => i.CurrentHands.Any(x => x.State == State.NotYetPlayed ));
                bool everythingIsbusted = !PlayerList.Any(i => i.CurrentHands.Any(x => x.Result != Result.Bust));


                //If all hands have busted, end the game and don't play for the dealer
                if (everythingIsbusted)
                {
                    //do nothing
                    OnGameEnd?.Invoke(this, null);
                }
                else if (nextPlayer == null)
                {
                    OnShowAllCards(this, null);

                    while (!Dealer.Hand.CheckIsBust() && Dealer.Hand.CurrentScore <= 16)
                    {
                        GiveDealerACard();
                    }

                    CalculateScore();

                    OnGameEnd(this, null);
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

        private void CalculateScore()
        {
            //check if dealer has blackJack
            int dealerScore = Dealer.Hand.CurrentScore;
            foreach(var player in PlayerList)
            {
                foreach (PlayerHand i in player.CurrentHands)
                {
                    if (i.State == State.Stand || i.State == State.Doubled)
                    {
                        if (i.CurrentScore > dealerScore || Dealer.Hand.CheckIsBust())
                        {
                            AdjustPlayerBankRoll(player, i.CurrentBet.Amount * 2);
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
                            AdjustPlayerBankRoll(player, i.CurrentBet.Amount);
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
