using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Core
{
    public sealed class Player
    {
        private List<PlayerHand> currentHands;
        private readonly int position;

        public Player(int position)
        {
            currentHands = new List<PlayerHand>();
            this.position = position;
        }

        public string Name { get; set; }

        public List<PlayerHand> CurrentHands
        {
            get
            {
                return currentHands;
            }
            set
            {
                currentHands = value;
            }
        }

        public int Position { get { return position; } }

        public PlayerHand ActiveHand
        {
            get { return this.CurrentHands.Find(i => i.IsActive); }
        }
    }
}
