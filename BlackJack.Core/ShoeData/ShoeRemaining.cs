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
    public class ShoeRemaining : INotifyPropertyChanged
    {
        private readonly Shoe shoe;
        public Suit Suit { get; set; }
        private int _twoThroughNineCount;
        private int _tenThroughKingCount;
        private int _acesCount;

        public ShoeRemaining(Shoe shoe, Suit suit)
        {
            this.shoe = shoe;
            this.Suit = suit;
        }

        public void Calculate()
        {
            this.TwoThroughNineCount = shoe.UndealtCards.Count(i => { return i.Index <= 9 && i.Index > 1 && i.CardSuit == this.Suit; });
            this.TenThroughKingCount = shoe.UndealtCards.Count(i => { return i.Index > 9 && i.Index <= 13 && i.CardSuit == this.Suit; });
            this.AcesCount = shoe.UndealtCards.Count(i => { return i.Index == 1 && i.CardSuit == this.Suit; });
        }

        public int TwoThroughNineCount
        {
            get { return this._twoThroughNineCount; }
            set
            {
                if (value != this._twoThroughNineCount)
                {
                    this._twoThroughNineCount = value;
                    NotifyPropertyChanged("TwoThroughNineCount");
                }
            }
        }

        public int TenThroughKingCount
        {

            get { return this._tenThroughKingCount; }
            set
            {
                if (value != this._tenThroughKingCount)
                {
                    this._tenThroughKingCount = value;
                    NotifyPropertyChanged("TenThroughKingCount");
                }
            }
        }

        public int AcesCount
        {
            get { return this._acesCount; }
            set
            {
                if (value != this._acesCount)
                {
                    this._acesCount = value;
                    NotifyPropertyChanged("AcesCount");
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
