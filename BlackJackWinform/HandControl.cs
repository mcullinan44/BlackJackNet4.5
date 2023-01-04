using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Blackjack.Core;
using Blackjack.Core.Entities;

namespace BlackJackWinform
{
    public partial class HandControl : UserControl
    {
        protected readonly List<PictureBox> PictureBoxList;
        protected readonly GameController Controller;
        protected readonly BlackJackForm BlackJackForm;

        //protected readonly Hand Hand;
        public HandControl(GameController controller, BlackJackForm blackjackForm)
        {
            InitializeComponent();
            PictureBoxList = new List<PictureBox>();
            Controller = controller;
            BlackJackForm = blackjackForm;
            //Hand = hand;
            btnDoubleDown.Enabled = true;
            btnSplit.Enabled = false;
            btnHit.Enabled = true;
            btnStand.Enabled = true;
        }

        public void AddCard(Card card, bool isFaceUp)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Height = 138;
            pictureBox.Width = 94;
            Point p = new Point();
            if (PictureBoxList.Count > 0)
            {
                p.X = PictureBoxList[PictureBoxList.Count - 1].Location.X + 18;
                p.Y = PictureBoxList[PictureBoxList.Count - 1].Location.Y + 18;
            }

            pictureBox.Location = p;
            pictureBox.Tag = card;
            PictureBoxList.Add(pictureBox);
            pnlHand.Controls.Add(pictureBox);
            pictureBox.BringToFront();
            var image = isFaceUp ? ImageHelper.GetFaceImageForCard(card) : ImageHelper.GetBackImage();
            Size size = new Size(94, 138);
            pictureBox.Image = ResizeImage(image, size);
        }

        public static Image ResizeImage(Image img, Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gr.DrawImage(img, new Rectangle(Point.Empty, size));
            }
            return bmp;
        }

        public void AddCard(Card card)
        {
            AddCard(card, true);
        }

        public void EndGame()
        {
            this.PictureBoxList.Clear();
            pnlHand.Controls.Clear();
        }
    }
}
