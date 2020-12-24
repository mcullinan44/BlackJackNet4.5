using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public class Stats
    {
        public Stats(PlayerHand playerHand)
        {
            this.PlayerHand = playerHand;
            this.Score = playerHand.CurrentScore;
            this.BetAmount = playerHand.CurrentBet.Amount;
            this.Result = Result.Playing;
            
        }
        public PlayerHand PlayerHand { get; set; }
        public int Score { get; set; }
        public double BetAmount { get; set;  }
        public Result Result { get; set; }
        public double Bankroll { get; set;  }
    }

    public enum Result
    {
        Playing,
        Blackjack,
        DealerBlackjack,
        Win,
        Push,
        Lost,
        Bust
    }


    public class ShoeStat
    {
        public string CardString { get; set; }

        public bool IsDealt { get; set; }

        public int DeckNumber { get; set; }

    }

}
