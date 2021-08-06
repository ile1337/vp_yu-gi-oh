using Middleware.Models;
using System.Drawing;
using System.Windows.Forms;

namespace yu_gi_oh.Components
{
    class CardPictureBox : PictureBox
    {
        public static readonly Size defaultSize = new(122, 152);

        public readonly CardDto Card;

        public CardPictureBox(CardDto dto, Point position) : base()
        {
            Card = dto;
            Image = Card.img;
            Location = position;
            Size = defaultSize;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
