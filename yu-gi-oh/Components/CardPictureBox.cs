using Middleware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace yu_gi_oh.Components
{
    public partial class CardPictureBox : PictureBox
    {

        public static readonly Size defaultSize = new(122, 152);

        protected static readonly Size fieldSize = new(82, 112);

        protected CardDto _card { get; set; } = new();

        public CardDto Card
        {
            get { return _card; }
            set
            {
                _card = value;
                Image = value.img;
            }
        }

        public CardPictureBox()
        {
            InitializeComponent();
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = fieldSize;
        }

        public void SetInHand(Point position)
        {
            Location = position;
            Size = defaultSize;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override string ToString()
        {
            return string.Format("{0}", Card.name);
        }
    }
}
