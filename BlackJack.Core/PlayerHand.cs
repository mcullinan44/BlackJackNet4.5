using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public class PlayerHand : Hand
    {

        public event GameEvents.OnCardReceived onCardReceived;
        public event GameEvents.OnCardRemoved onCardRemoved;
        public event GameEvents.OnWinHand onWinHand;
        public event GameEvents.OnLoseHand onLoseHand;
        public event GameEvents.OnPushHand onPushHand;
        public event GameEvents.OnBlackjack onBlackjack;
        public event GameEvents.OnBust onBust;
        public event GameEvents.OnBetChanged onBetChanged;
        public event GameEvents.OnActivate onActivate;



        private State _state = State.NotYetPlayed;


        public PlayerHand(Player player, GameController gameController)
            : base(gameController)
        {            
            this.controller = gameController;
            this.Player = player;

            CurrentBet = new Bet(this);


        }

        public State State
        {
            get { return this._state; }
            set {
                this._state = value;
                if(this._state == State.Playing)
                {
                    onActivate?.Invoke(this, null);
                }
            }
        }
        

        public void Win()
        {
            this.Result = Result.Win;
            onWinHand?.Invoke(this);
        }

        public void Lost()
        {
            this.Result = Result.Lost;
            onLoseHand?.Invoke(this);
        }

        public void Push()
        {
            this.Result = Result.Push;
            onPushHand?.Invoke(this);
        }

        public void Blackjack()
        {
            this.Result = Result.Blackjack;
            var args = new OnCardReceivedEventArgs(this, null);
            onBlackjack?.Invoke(this, args);
            
        }

        public Player Player { get; set; }

        public Bet CurrentBet { get; set; }

        public void IncreaseBet(double amountToIncrease)
        {
            CurrentBet.Amount += amountToIncrease;
            var args = new OnBetChangedEventArgs(CurrentBet);
            onBetChanged?.Invoke(this, args);

        }

        public void AddCard(Card card)
        {
            this.Cards.Add(card);
            var args = new OnCardReceivedEventArgs(this, card);
            onCardReceived?.Invoke(this, args);
        }

        public Card RemoveCard(int index)
        {
            var result = this.Cards[index];
            this.Cards.Remove(result);
            var args = new OnCardReceivedEventArgs(this, result);
            onCardReceived?.Invoke(this, args);


            return result;
        }


        public override bool CheckIsBust()
        {

            if (CurrentScore > 21)
            {
                this.Result = Result.Bust;
                var args = new OnCardReceivedEventArgs(this, null);
                onBust?.Invoke(this, args);
                return true;
            }
            else
            {
                return false;
            }

        }




    }
}
