using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Blackjack.Core;

namespace BlackJackWinform
{
    public partial class HandControl : UserControl
    {
        protected readonly List<PictureBox> pictureBoxList;
        protected readonly GameController Controller;
        protected readonly BlackJackForm BlackJackForm;

        public HandControl(Hand hand, GameController controller, BlackJackForm blackjackForm)
        {
            InitializeComponent();
 
            this.pictureBoxList = new List<PictureBox>();
            this.Controller = controller;
            this.BlackJackForm = blackjackForm;
        }

        public void AddCard(Card card, bool isFaceUp)
        {
            var pictureBox = new PictureBox();
            //pictureBox.Height = 106;
            //pictureBox.Width = 72;


            pictureBox.Height = 138;
            pictureBox.Width = 94;

            Point p = new Point();
            if (pictureBoxList.Count > 0)
            {
                p.X = pictureBoxList[pictureBoxList.Count - 1].Location.X + 18;
                p.Y = pictureBoxList[pictureBoxList.Count - 1].Location.Y + 18;
            }
            pictureBox.Location = p;
            pictureBox.Tag = card;
            pictureBoxList.Add(pictureBox);
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
            this.pictureBoxList.Clear();
            pnlHand.Controls.Clear();
        }
    }
}
