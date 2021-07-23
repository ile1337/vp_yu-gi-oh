using Middleware.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yu_gi_oh
{
    public partial class Deckbuilder : Form
    {
        public Deck deck { get; set; } = new Deck();
        public Deckbuilder()
        {
            InitializeComponent();
            lbAllCards.AllowDrop = true;
            lbDeckCards.AllowDrop = true;
            init();

        }
        public void init()
        {
            lbAllCards.Items.Clear();
            Task<Middleware.Models.Meta.PageResponse<Middleware.Models.CardDto>> tmp = Middleware.Controllers.CardController.GetAllCardDtosShortAsync(new Middleware.Models.CardDto(), 1);
            foreach (CardDto t in tmp.Result.content)
            {
                lbAllCards.Items.Add(t);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form = new MainMenu();
            form.ShowDialog();
        }

        private void btnAddtoDeck_Click(object sender, EventArgs e)
        {
            if (lbAllCards.SelectedIndex != 0)
            {
                if (isMorethan3(lbAllCards.SelectedItem as CardDto))
                {
                    MessageBox.Show("You can't add more than 3 instances of that card!", "ERROR");
                    return;
                }
                if (maxCards())
                {
                    MessageBox.Show("The maximum number of cards for a deck is 30 !", "ERROR");
                    return;
                }
                lbDeckCards.Items.Add(lbAllCards.SelectedItem as CardDto);
            }
        }
        private bool isMorethan3(CardDto c)
        {
            int ctr = 0;
            if (lbDeckCards.Items.Count > 0)
            {
                foreach (CardDto card in lbDeckCards.Items)
                {
                    if (card == c)
                    {
                        ctr++;
                    }
                }

            }
            if (ctr >= 3) return true;
            else return false;
        }
        private bool maxCards()
        {
            return lbDeckCards.Items.Count > 30;
        }

        private void btnRemoveFromDeck_Click(object sender, EventArgs e)
        {
            if (lbDeckCards.SelectedIndex != -1)
            {
                lbDeckCards.Items.Remove(lbDeckCards.SelectedItem);
            }
        }

        private void btnSaveDeck_Click(object sender, EventArgs e)
        {
            if (lbDeckCards.Items.Count > 0)
            {

                foreach (CardDto card in lbDeckCards.Items)
                {
                    deck.cards.Add(card);
                }

                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    saveAsync(sfd.FileName);
                }
                else
                {
                    MessageBox.Show("You need to enter a name for the new deck !", "ERROR");
                    return;
                }

            }
        }
        private async void saveAsync(string s)
        {
            using (FileStream fs = new FileStream(s, FileMode.Create))
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(fs, deck);
            }
        }

        private void btnNewDeck_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure you want to remove all cards from your deck ?", "Are you sure ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lbDeckCards.Items.Clear();
            }
            else
            {
                return;
            }

        }

        private void btnOpenDeck_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lbDeckCards.Items.Clear();
                openAsync(ofd.FileName);
            }
        }
        private async void openAsync(string s)
        {
            using (FileStream fs = new FileStream(s, FileMode.Open))
            {
                Deck deck = await System.Text.Json.JsonSerializer.DeserializeAsync<Deck>(fs);
                foreach (CardDto card in deck.cards)
                {
                    lbDeckCards.Items.Add(card);
                }

            }
        }
    }

}
