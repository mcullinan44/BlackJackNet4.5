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


namespace Blackjack.Core.Counting
{
    public class CardCountingMethodCollection
    {
        private readonly BindingList<BaseCardCountingStrategy> cardCountingStrategyBindingList;
        private readonly GameController controller;

        public CardCountingMethodCollection(GameController controller)
        {
            this.cardCountingStrategyBindingList = new BindingList<BaseCardCountingStrategy>();
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiLo, controller.Shoe));
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiOp1, controller.Shoe));
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.HiOpt2, controller.Shoe));
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.KO, controller.Shoe));
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.Omega3, controller.Shoe));
            cardCountingStrategyBindingList.Add(CardCountingMethodFactory.GetStrategy(CardCountingStrategy.ZenCount, controller.Shoe));
            this.controller = controller;
            this.controller.onCardReceived += controller_onCardReceived;
        }

        void controller_onCardReceived(object sender, OnCardReceivedEventArgs args)
        {
            foreach (var item in cardCountingStrategyBindingList)
            {
                item.CurrentCount += item.GetValueForCard(args.Card);
            }
        }

        public BindingList<BaseCardCountingStrategy> CardCountingStrategyBindingList
        {
            get { return this.cardCountingStrategyBindingList; }
        }
    }
}
