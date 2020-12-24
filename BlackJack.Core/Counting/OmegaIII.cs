using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core.Counting
{
    public class OmegaIII : BaseCardCountingStrategy
    {
        public OmegaIII(Shoe shoe) : base(shoe) { }

        public override string StrategyName
        {
            get { return "Omega III"; }
        }
        public override int GetValueForCard(Card card)
        {
            int value = 0;
            switch (card.Index)
            {
                case 2:
                case 3:
                    value = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    value = 2;
                    break;
                case 7:
                    value = 1;
                    break;
                case 8:
                    value = 0;
                    break;
                case 9:
                    value = -1;
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                    value = -2;
                    break;
                case 1:
                    value = 0;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }
    }

}
