using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yu_gi_oh
{
    public partial class Duel : Form
    {
        //Login l = new Login();

        /**
         * Starting position: 512, 649
         * Size: 82, 112
         * 
         */

        private static Point currentPosition = new(412, 609);
        private static readonly Size defaultSize = new(122, 152);
        private static readonly double hoverCoefficient = 1.5;
        private static readonly Size hoverSize = new((int)(122 * hoverCoefficient), (int)(152 * hoverCoefficient));

        private static readonly int xOffset = 100;
        private static readonly List<PictureBox> hand = new();

        private static int zIndex;
        private static readonly int hoverHeight = 80;
        public Duel()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            PictureBox card = CreateCard();
            hand.Add(card);
            Controls.Add(card);
            card.Anchor = AnchorStyles.Bottom;
            card.BringToFront();
           
        }

        private PictureBox CreateCard()
        {
            PictureBox card = new();
            currentPosition.Offset(xOffset, 0);
            card.Location = currentPosition;
            card.Size = defaultSize;
            card.SizeMode = PictureBoxSizeMode.StretchImage;
            card.Image = Middleware.Controllers.YGOController.GetRandomImage();
            // Add mouse enter event
            card.MouseEnter += Card_MouseEnter;
            // Add mouse leave event
            card.MouseLeave += Card_MouseLeave;
            return card;
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            PictureBox card = sender as PictureBox;
            Controls.SetChildIndex(card, zIndex);
            card.Location = new Point(card.Location.X, card.Location.Y + hoverHeight);
            card.Size = defaultSize;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            PictureBox card = (sender as PictureBox);
            zIndex = Controls.GetChildIndex(card);

            card.Size = hoverSize;
            card.Location = new Point(card.Location.X, card.Location.Y - hoverHeight);
            card.BringToFront();
        }

        private void DestroyCard()
        {
            if (hand.Count == 0) return;

            PictureBox card = hand.Last();
            Controls.Remove(card);
            hand.Remove(card);
            card.Image.Dispose();
            card.Dispose();
            currentPosition.Offset(-xOffset, 0);
        }

        private void btnDP_Click(object sender, EventArgs e)
        {
            Draw();
        }

        private void btnEP_Click(object sender, EventArgs e)
        {
            DestroyCard();
        }

        
    }
}
