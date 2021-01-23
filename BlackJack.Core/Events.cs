using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public static class GameEvents
    {
        public delegate void OnBankrollChange(object sender, OnBankrollChangedEventArgs e);

        public delegate void OnShowAllCards(object sender, EventArgs e);
        public delegate void OnActiveHandChanged(object sender, EventArgs e);
        public delegate void OnCardReceived(object sender, OnCardReceivedEventArgs args);
        
        public delegate void OnDealerCardReceived(object sender, OnCardReceivedEventArgs args);

        public delegate void OnBust(object sender, OnCardReceivedEventArgs args);
        public delegate void OnBlackjack(object sender, OnCardReceivedEventArgs args);
        public delegate void OnWinHand(Hand hand);
        public delegate void OnLoseHand(Hand hand);


        public delegate void OnDealerBust(object sender, OnCardReceivedEventArgs args);
        public delegate void OnDealerBlackjack(object sender, OnCardReceivedEventArgs args);
        public delegate void OnDealerWinHand(Hand hand);
        public delegate void OnDealerLoseHand(Hand hand);

        public delegate void OnPushHand(Hand hand);
        


        public delegate void OnGameEnd(object sender, EventArgs e);
        public delegate void OnCardRemoved(object sender, OnCardReceivedEventArgs args);
        public delegate void OnBetChanged(object sender, OnBetChangedEventArgs args);
        public delegate void OnShuffle(object sender, EventArgs e);
    }
    
    public class OnCardReceivedEventArgs : EventArgs
    {
        private readonly Hand hand;
        private readonly Card card;

        public OnCardReceivedEventArgs(Hand hand, Card card)
        {
            this.hand = hand;
            this.card = card;
        }

        public Hand Hand
        {
            get { return hand; }
        }

        public Card Card
        {
            get { return card; }
        }
    }

    public class OnBankrollChangedEventArgs: EventArgs
    {
        public OnBankrollChangedEventArgs(Player player)
        {
            this.Player = player;
        }
        public Player Player { get; private set; }
    }


    public class OnBetChangedEventArgs : EventArgs
    {
        public OnBetChangedEventArgs(Bet bet)
        {
            this.Bet = bet;
        }

        

        public Bet Bet { get; private set;}
    }
}
