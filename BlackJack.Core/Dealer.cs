using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Dealer
    {
        public string Name { get; set; }

        public Dealer() {
          
   
        }

        public DealerHand Hand { get; set; }

        public int Position
        {

            get
            {
                return 0;
            }
        }
    
    }
}
