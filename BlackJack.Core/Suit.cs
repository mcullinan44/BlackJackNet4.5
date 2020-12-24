using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public enum Suit
    {
        Hearts = 1,
        Diamonds,
        Clubs,
        Spades,
        None
    }

    public enum CardType
    {
        Numeric = 1,
        Jack,
        Queen,
        King,
        Ace
    }
}
