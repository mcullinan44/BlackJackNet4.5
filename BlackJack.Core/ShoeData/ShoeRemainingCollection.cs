using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Core;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Blackjack.Core.ShoeData
{
    public class ShoeRemainingCollection
    {
        private readonly BindingList<ShoeRemaining> shoeRemainingBindingList;
        public ShoeRemainingCollection(GameController controller)
        {
            this.shoeRemainingBindingList = new BindingList<ShoeRemaining>();
            shoeRemainingBindingList.Add(new ShoeRemaining(controller.Shoe, Suit.Clubs));
            shoeRemainingBindingList.Add(new ShoeRemaining(controller.Shoe, Suit.Diamonds));
            shoeRemainingBindingList.Add(new ShoeRemaining(controller.Shoe, Suit.Hearts));
            shoeRemainingBindingList.Add(new ShoeRemaining(controller.Shoe, Suit.Spades));
            foreach (var item in ShoeRemainingBindingList)
            {
                item.Calculate();
            }
            controller.onCardReceived += controller_onCardReceived;
            controller.onShuffle += controller_onShuffle;
        }

        void controller_onShuffle(object sender, EventArgs e)
        {
            foreach (var item in ShoeRemainingBindingList)
            {
                item.Calculate();
            }
        }

        public BindingList<ShoeRemaining> ShoeRemainingBindingList
        {
            get { return this.shoeRemainingBindingList; }

        }

        void controller_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            ShoeRemainingBindingList.First(i => i.Suit == args.Card.CardSuit).Calculate();
        }
    }
}
