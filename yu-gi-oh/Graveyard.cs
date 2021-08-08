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
using Middleware.Models;

namespace yu_gi_oh
{
    public partial class Graveyard : Form
    {
        public  List<CardPictureBox> graveList = new();
        public  List<CardDto> deck = new();
        public  Stack<CardPictureBox> hand = new();
        public Duel duel;
        public Graveyard(List<CardPictureBox> grave,List<CardDto> d,Stack<CardPictureBox> h,Duel dl)
        {
            InitializeComponent();
            graveList = grave;
            deck = d;
            hand = h;
            duel = dl;
            foreach (CardPictureBox c in graveList)
            {
                lbGraveyardCards.Items.Add(c.Card);
            }

        }

        private void Graveyard_Load(object sender, EventArgs e)
        {


        }

        private void lbGraveyardCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGraveyardCards.SelectedIndex == -1) return;
            Image image = (lbGraveyardCards.SelectedItem as CardDto).img;
            pbGrobishta.SizeMode = PictureBoxSizeMode.StretchImage;
            pbGrobishta.Image = image;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            duel.Refresh();
            duel.lblGraveYard.Text = graveList.Count.ToString();
            duel.lbDeckCardsNum.Text = deck.Count.ToString();
            this.DialogResult = DialogResult.OK;
            
        }



        private void btnToHand_Click(object sender, EventArgs e)
        {
            if (lbGraveyardCards.SelectedIndex == -1) return;
            int i = lbGraveyardCards.SelectedIndex;
            hand.Push(graveList[i]);
            CardPictureBox cd = graveList[i];
            duel.PutCardInHand(i);
            graveList.RemoveAt(i);
            lbGraveyardCards.Items.RemoveAt(lbGraveyardCards.SelectedIndex);
            duel.lblGraveYard.Text = graveList.Count.ToString();
            duel.lbDeckCardsNum.Text = deck.Count.ToString();
            duel.Refresh();
        }

        private void btnToDeck_Click(object sender, EventArgs e)
        {
            if (lbGraveyardCards.SelectedIndex == -1) return;
            int i = lbGraveyardCards.SelectedIndex;
            CardDto card = graveList[i].Card;
            deck.Add(card);
            graveList.RemoveAt(i);
            lbGraveyardCards.Items.RemoveAt(lbGraveyardCards.SelectedIndex);
            duel.lblGraveYard.Text = graveList.Count.ToString();
            duel.lbDeckCardsNum.Text = deck.Count.ToString();
            duel.Refresh();
        }
    }
}
