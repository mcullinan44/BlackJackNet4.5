using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Blackjack.Core;

namespace BlackJackWinform
{
    public static class ImageHelper
    {
        private static System.Resources.ResourceManager resourceManager =
            new System.Resources.ResourceManager("BlackJackWinform.ImageResource", System.Reflection.Assembly.GetExecutingAssembly());
       // private static Bitmap cardImages = (Bitmap)resourceManager.GetObject("Cards.png");
        public const int CardWidth = 72;
        public const int CardHeight = 97;
        public const int CardWidthRect = 73;
        public const int CardHeightRect = 98;

        public static System.Drawing.Bitmap GetFaceImageForCard(Card card)
        {
            Bitmap cardImages = ImageResource.Cards;
                //Adjust the clipping of the cards image to reflect the current card
                int x = 0;
                int y = 0;

                //if (card..Visible)
                //{
                    //Define the card position in the cards image
                    if (card.Index <= 10)
                    {
                        x = (card.Index - 1) % 2;
                        y = (card.Index - 1) / 2;

                        switch (card.CardSuit)
                        {
                            case Suit.Spades:
                                x += 6;
                                break;
                            case Suit.Hearts:
                                x += 0;
                                break;
                            case Suit.Diamonds:
                                x += 2;
                                break;
                            case Suit.Clubs:
                                x += 4;
                                break;
                        }
                    }
                    else
                    {
                        int number = (card.Index - 11);
                        switch (card.CardSuit)
                        {
                            case Suit.Spades:
                                number += 6;
                                break;
                            case Suit.Hearts:
                                number += 9;
                                break;
                            case Suit.Diamonds:
                                number += 3;
                                break;
                            case Suit.Clubs:
                                number += 0;
                                break;
                        }

                        x = (number % 2) + 8;
                        y = number / 2;
                    }
      

             Rectangle rect = new Rectangle(x * CardWidthRect, y * CardHeightRect, CardWidth, CardHeight);



             Bitmap cropped = cardImages.Clone(rect, ImageResource.Cards.PixelFormat);
            return cropped;
        }

        public static Bitmap GetBackImage()
        {
            int x = 7;
            int y = 5;
            Bitmap cardImages = ImageResource.Cards;
            Rectangle rect = new Rectangle(x * CardWidthRect, y * CardHeightRect, CardWidth, CardHeight);
            Bitmap cropped = cardImages.Clone(rect, cardImages.PixelFormat);

            return cropped;
        }
    }
}
