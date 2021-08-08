using Middleware.Models;
using System.Drawing;
using System.Windows.Forms;

namespace yu_gi_oh.Components
{
    public class CardPictureBox : PictureBox
    {
        public static readonly Size defaultSize = new(122, 152);

        public readonly CardDto Card;

        public CardPictureBox()
        {
        }

        public CardPictureBox(CardDto dto, Point position) : base()
        {
            Card = dto;
            Image = Card.img;
            Location = position;
            Size = defaultSize;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public override string ToString()
        {
            return string.Format("{0}", Card.name);
        }
    }
}
