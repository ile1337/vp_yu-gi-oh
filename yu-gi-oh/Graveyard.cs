using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Middleware.Models;

namespace yu_gi_oh
{
    public partial class Graveyard : Form
    {
        public BindingList<CardDto> graveList = new();
        public List<CardDto> deck = new();
        public Duel duel;
        public Graveyard(List<CardDto> grave,List<CardDto> d,Duel dl)
        {
            InitializeComponent();
            graveList = new BindingList<CardDto>(grave);
            deck = d;
            duel = dl;
            lbGraveyardCards.DataSource = graveList;
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
            duel.UpdateGraveyardLabel(graveList.ToList());
            duel.UpdateDeckLabel(deck);
            this.DialogResult = DialogResult.OK;
        }

        private void btnToHand_Click(object sender, EventArgs e)
        {
            if (lbGraveyardCards.SelectedIndex == -1) return;
            if (duel.CurrentHandCount() >= 4)
            {
                MessageBox.Show("You can have only 4 cards in hand!", "Cards in Hand");
                return;
            }
            int i = lbGraveyardCards.SelectedIndex;
            duel.PutCardInHand(graveList[i]);
            RemoveFromGraveyard(i);
        }

        private void btnToDeck_Click(object sender, EventArgs e)
        {
            if (lbGraveyardCards.SelectedIndex == -1) return;
            int i = lbGraveyardCards.SelectedIndex;
            deck.Add(graveList[i]);
            RemoveFromGraveyard(i);
        }

        private void RemoveFromGraveyard(int i)
        {
            graveList.RemoveAt(i);
            duel.UpdateGraveyardLabel(graveList.ToList());
            duel.UpdateDeckLabel(deck);
            duel.Refresh();
        }
    }
}
