using Middleware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using yu_gi_oh.Components;

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
        private static readonly double hoverCoefficient = 1.5;
        private static readonly Size hoverSize = new((int)(122 * hoverCoefficient), (int)(152 * hoverCoefficient));

        private static readonly int xOffset = 100;
        private static readonly List<CardPictureBox> hand = new();

        private static int zIndex;
        private static readonly int hoverHeight = 80;

        // ONLY FOR TESTING UNTIL READ DECK IS IMPLEMENTED
        private List<CardDto> deck = MainMenu.deck;
        private readonly Random random = new Random();


        public Duel()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            CardPictureBox card = DrawCard();
            card.Anchor = AnchorStyles.Bottom;
            hand.Add(card);
            Controls.Add(card);
            card.BringToFront();
        }

        private CardPictureBox DrawCard()
        {
            CardDto dto = deck[random.Next(0, deck.Count)];
            
            currentPosition.Offset(xOffset, 0);
            CardPictureBox card = new(dto, currentPosition);
            
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            
            return card;
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            PictureBox card = sender as PictureBox;
            Controls.SetChildIndex(card, zIndex);
            card.Location = new Point(card.Location.X, card.Location.Y + hoverHeight);
            card.Size = CardPictureBox.defaultSize;
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

            CardPictureBox card = hand.Last();
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

        //Below function is for form flickering on resize or fullscreen
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }


    }
}
