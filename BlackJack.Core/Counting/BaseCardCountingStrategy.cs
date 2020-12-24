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
    public abstract class BaseCardCountingStrategy : INotifyPropertyChanged
    {
        private readonly Shoe shoe;
        private int _currentCount;
        public abstract string StrategyName { get; }
        public BaseCardCountingStrategy(Shoe shoe)
        {
            this.shoe = shoe;
        }

        public abstract int GetValueForCard(Card card);

        public int CurrentCount
        {
            get
            {
                return _currentCount;
            }
            set
            {
                if (value != this._currentCount)
                {
                    this._currentCount = value;
                    NotifyPropertyChanged("CurrentCount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
