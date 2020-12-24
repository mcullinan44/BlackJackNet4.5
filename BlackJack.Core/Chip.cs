using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Chip
    {
        private ChipDenomination denomination;
        
        public Chip(ChipDenomination chipDenomination)
        {
            denomination = chipDenomination;
        }

        public ChipDenomination Value
        {
            get
            {
                return denomination;
            }
        }
    }

    public enum ChipDenomination
    {
        One = 1,
        Five = 5,
        Ten = 10,
        TwentyFive = 25,
        Fifty = 50,
        OneHundred = 100
    }
}
