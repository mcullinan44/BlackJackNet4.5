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
        private double bankroll = 0;



        public Player(int position)
        {
            currentHands = new List<PlayerHand>();
            this.position = position;
        }

        public bool IsActive { get; set; }

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

        public double PlayerbankRoll
        {
            get { return bankroll; }
            set
            {
                bankroll = value;
       
            }
        }

        public int Position { get { return position; } }

        public PlayerHand ActiveHand
        {
            get { return this.CurrentHands.Find(i => i.State == State.Playing); }
        }
    }
}
