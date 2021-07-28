using Middleware.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using yu_gi_oh.Threading;

namespace yu_gi_oh
{
    public partial class Deckbuilder : Form
    {
        // Saving
        public Deck deck { get; set; } = new();

        // Paging
        public int CurrentPage { get; set; } = 0;
        public int MaxPage { get; set; } = int.MaxValue;

        // Filter
        public CardDto filterCard = new();

        // Sources
        public BindingList<CardDto> cards = new();
        public BindingList<CardDto> deckCards = new();

        // DGV construction
        public readonly Dictionary<string, string> dgvNaming = new() { { "img", "" }, { "name", "Name" }, { "type", "Type" }, { "atk", "ATK" }, { "def", "DEF" } };

        public Deckbuilder()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            ConstructDGV(dgv1, cards);
            ConstructDGV(dgvDeck, deckCards);
            btnNewDeck.Enabled = false;
            loadingPB.Visible = true;
            LoadDataTable(Middleware.Models.Meta.Direction.FORWARDS);
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;
            lblnumberofcardsinDeck.Text = "0";
            lblcurr.Text = CurrentPage.ToString();
           
        }

        public void ConstructDGV(DataGridView dgv, BindingList<CardDto> source)
        {
            // Base configurations
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = source;
            dgv.AutoGenerateColumns = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.RowTemplate.Height = 100;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Disable useless columns
            dgv.Columns["id"].Visible = false;
            dgv.Columns["cardId"].Visible = false;
            dgv.Columns["description"].Visible = false;
            dgv.Columns["subType"].Visible = false;

            int idx = 0;
            foreach (KeyValuePair<string, string> item in dgvNaming)
            {
                DataGridViewColumn col = dgv.Columns[item.Key];
                col.HeaderText = item.Value;
                col.DisplayIndex = idx++;
                col.ReadOnly = true;

                // Set images as Stretch
                if (item.Key == "img")
                {
                    (col as DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Stretch;
                    col.Width = 60;
                }
            }
        }

        private void LoadDataTable(Middleware.Models.Meta.Direction direction)
        {
            CurrentPage += (int)direction;
            if (CurrentPage < 1) CurrentPage = 1;

            Middleware.Controllers.CardController.GetAllCardDtosShortAsync(filterCard, CurrentPage).ContinueWith(t =>
            {
                Invoker.SafeInvoke(this, () => CleanCards(), false);

                Middleware.Models.Meta.PageResponse<CardDto> page = t.Result;
                MaxPage = page.totalPages;
              
                foreach (CardDto card in page.content)
                {
                    card.img = Middleware.Controllers.YGOController.GetImage(card.cardId);
                    Invoker.SafeInvoke(this, () => cards.Add(card), false);
                }
                Invoker.SafeInvoke(this, () => lblmax.Text = "/ " + MaxPage.ToString(), false);
                Invoker.SafeInvoke(this, () => loadingPB.Visible = false, false);
      
            });
            
        }

        private void CleanCards()
        {
            foreach (CardDto card in cards)
            {
                if (card.img != null) card.img.Dispose();
            }
            cards.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form = new();
            form.ShowDialog();
            this.Close();
        }

        private void btnAddtoDeck_Click(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0) btnNewDeck.Enabled = true;

            if (dgv1.RowCount >= 0)
            {
                foreach (DataGridViewRow row in dgv1.SelectedRows)
                {
                    CardDto card = cards[row.Index].Clone();
                    if (IsMoreThan3(card))
                    {
                        MessageBox.Show("You can't add more than 3 instances of that card!", "ERROR");
                        return;
                    }
                    else if (MaxCards())
                    {
                        MessageBox.Show("The maximum number of cards for a deck is 30 !", "ERROR");
                        return;
                    }
                    deckCards.Add(card);
                    lblnumberofcardsinDeck.Text = deckCards.Count.ToString();
                }
            }
        }

        private bool IsMoreThan3(CardDto c)
        {
            return deckCards.Count((card) => card.id == c.id) >= 3;
        }

        private bool MaxCards()
        {
            return deckCards.Count >= 30;
        }

        private void btnRemoveFromDeck_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvDeck.SelectedRows)
                deckCards.RemoveAt(row.Index);
            lblnumberofcardsinDeck.Text = deckCards.Count.ToString();
        }

        private void btnSaveDeck_Click(object sender, EventArgs e)
        {
            if (dgvDeck.Rows.Count <= 0) return;

            deck.cards = deckCards.ToList();
            CallFileExplorer(new SaveFileDialog(), (dialog) => SaveAsync(dialog.FileName));
        }

        private async void SaveAsync(string s)
        {
            using FileStream fs = new(s, FileMode.Create);
            await System.Text.Json.JsonSerializer.SerializeAsync(fs, deck);
        }

        private void btnNewDeck_Click(object sender, EventArgs e)
        {
            if (deckCards.Count > 0)
            {
                btnNewDeck.Enabled = true;
                if (MessageBox.Show("Are You sure you want to remove all cards from your deck ?", "Are you sure ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btnNewDeck.Enabled = false;
                    lblnumberofcardsinDeck.Text = "0";
                    deckCards.Clear();
                }
                else return;
            }
        }

        private static void CallFileExplorer(FileDialog dialog, Action<FileDialog> action)
        {
            dialog.AddExtension = true;
            dialog.DefaultExt = Configuration.YGO_DEFAULT_EXTENSION;
            dialog.Filter = Configuration.YGO_FILTER_EXTENSION;

            if (dialog.ShowDialog() == DialogResult.OK) action.Invoke(dialog);
        }


        private void btnOpenDeck_Click(object sender, EventArgs e)
        {
            btnNewDeck.Enabled = true;
            CallFileExplorer(new OpenFileDialog(), (dialog) =>
            {
                deckCards.Clear();
                OpenAsync(dialog.FileName);
            });
        }

        private async void OpenAsync(string s)
        {
            using FileStream fs = new(s, FileMode.Open);

            Deck deck = await System.Text.Json.JsonSerializer.DeserializeAsync<Deck>(fs);
            foreach (CardDto card in deck.cards)
            {
                card.img = Middleware.Controllers.YGOController.GetImage(card.cardId);
                deckCards.Add(card);
            }
            lblnumberofcardsinDeck.Text = deckCards.Count.ToString();
        }

        private void Deckbuilder_Load(object sender, EventArgs e)
        {

        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            loadingPB.Visible = true;
            LoadDataTable(Middleware.Models.Meta.Direction.FORWARDS);
            btnPreviousPage.Enabled = true;

            if (CurrentPage >= MaxPage) btnNextPage.Enabled = false;
            if (CurrentPage <= 1) btnPreviousPage.Enabled = false;
            lblcurr.Text = CurrentPage.ToString();
        }



        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            loadingPB.Visible = true;
            LoadDataTable(Middleware.Models.Meta.Direction.BACKWARDS);
            btnNextPage.Enabled = true;

            if (CurrentPage <= 1) btnPreviousPage.Enabled = false;
            if (CurrentPage >= MaxPage) btnNextPage.Enabled = false;
            lblcurr.Text = CurrentPage.ToString();

        }

        private void ReadCard(CardDto card)
        {
            rtbDescription.Text = card.description;
            pbCardImage.BackgroundImage = card.img;
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            ReadCard(cards[e.RowIndex]);
        }
        private void dgvDeck_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            ReadCard(deckCards[e.RowIndex]);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (CardDto card in cards)
            {
                card.img = Middleware.Controllers.YGOController.GetImage(card.cardId);
            }
            foreach (CardDto card in deckCards)
            {
                card.img = Middleware.Controllers.YGOController.GetImage(card.cardId);
            }
            refresh();
        }
        private void refresh()
        {
           
            dgvDeck.Refresh();
            dgv1.Refresh();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            filterCard = new()
            {
                name = tbCardName.Text == "" ? null : tbCardName.Text,
                atk = (int)nudATK.Value == 0 ? null : (int)nudATK.Value,
                type = cbCardType.Text == "" ? null : cbCardType.Text,
                def = (int)nudDEF.Value == 0 ? null : (int)nudDEF.Value
            };
            LoadDataTable(Middleware.Models.Meta.Direction.NONE);
            btnPreviousPage.Enabled = false;
            btnNextPage.Enabled = true;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            filterCard = new();

            tbCardName.ResetText();
            cbCardType.ResetText();

            nudATK.Value = 0;
            nudDEF.Value = 0;
        }

    }
}