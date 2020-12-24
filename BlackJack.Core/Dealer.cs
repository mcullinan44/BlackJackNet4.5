using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Dealer
    {
        private DealerHand dealerHand;

        public string Name { get; set; }

        public Dealer() {
          
   
        }

        public DealerHand ActiveHand
        {
            get
            {
                return this.dealerHand;
            }
            set { this.dealerHand = value; }
        }

        public int Position
        {

            get
            {
                return 0;
            }
        }
    
    }
}
