using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using yu_gi_oh.Components;
using yu_gi_oh.Components.Actions;

namespace yu_gi_oh
{
    public partial class Duel : Form
    {
        /**
         * Starting position: 512, 649
         * Size: 82, 112
         */

        private readonly Random random = new();

        // Position properties/constants
        private static Point currentPosition = new(312, 609);
        private static readonly int xOffset = 100;

        // Hover properties/constants
        private static readonly double hoverCoefficient = 1.5;
        private static readonly Size hoverSize = new((int)(122 * hoverCoefficient), (int)(152 * hoverCoefficient));
        private static readonly int hoverHeight = 80;

        // Depth
        private static int zIndex;

        // Logic properties/constants
        public static  List<CardPictureBox> hand = new();
        public List<CardDto> graveyardCards = new();
        public List<CardDto> deck = new();
        private static int currentPhase;
        private CardPictureBox SelectedCard;
        public CardPictureBox SelectedCardOnField;

        // Action ListBoxes
        private ListBox monsterActions = new();
        private ListBox spellActions = new();
        private ListBox trapActions = new();
        private ListBox monsterActionsField = new();
        private ListBox spellActionsField = new();
        private ListBox trapActionsField = new();

        // Field properties/constants
        private List<CardPictureBox> monsterFields = new();
        private List<CardPictureBox> spellFields = new();

        public Duel()
        {
            InitializeComponent();
            CreateListBoxes();
            InitializeProperties();
            monsterFields = new List<CardPictureBox> { cardPictureBox1, cardPictureBox2, cardPictureBox3 };
            spellFields = new List<CardPictureBox> { cardPictureBox4, cardPictureBox5, cardPictureBox6 };
            btnDP.Enabled = false;
            MessageBox.Show("At the start of the game please select a deck!", "Warning!");
            this.Refresh();
        }

        private void InitializeProperties()
        {
            currentPosition = new(312, 609);
            hand.Clear();
            zIndex = 0;
            deck.Clear();
            graveyardCards.Clear();
            currentPhase = 0;
            SelectedCard = null;
            SelectedCardOnField = null;
            monsterFields.Clear();
            spellFields.Clear();
            UpdateGraveyardLabel(graveyardCards);
            this.Refresh();
        }


        // ListBox creation
        public void CreateListBoxes()
        {
            CreateListBox<MonsterActions, Hand>(monsterActions, monsterActions_Click);
            CreateListBox<SpellActions, Hand>(spellActions, spellActions_Click);
            CreateListBox<TrapActions, Hand>(trapActions, trapActions_Click);
            CreateListBox<MonsterActions, Field>(monsterActionsField, monsterActionsField_Click);
            CreateListBox<SpellActions, Field>(spellActionsField, spellActionsField_Click);
            CreateListBox<TrapActions, Field>(trapActionsField, trapActionsField_Click);
        }


        public void CreateListBox<T, K>(ListBox actions, EventHandler handler) where T : struct, Enum, IConvertible
        {
            actions.DataSource = ActionUtilities.FilterLabelActions<T, K>();
            actions.BorderStyle = BorderStyle.Fixed3D;
            actions.BackColor = Color.MediumPurple;
            actions.Size = new Size(150, 125);
            actions.Font = new Font(actions.Font, FontStyle.Bold);
            if (handler != null) actions.Click += handler;
        }


        // Logic
        public void Draw()
        {
            CardPictureBox card = DrawCard();
            card.Anchor = AnchorStyles.Bottom;
            hand.Add(card);
            Controls.Add(card);
            card.BringToFront();
            UpdateDeckLabel(deck);
        }

        private void AdminDefeat_Click(object sender, EventArgs e) => NewGame();


        // Card logic
        private CardPictureBox DrawCard()
        {
            int idx = random.Next(0, deck.Count);
            CardDto dto = deck[idx];
            currentPosition.Offset(xOffset, 0);

            CardPictureBox card = new() 
            {
                Card = dto
            };
            card.SetInHand(currentPosition);

            ClearListBoxes();
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            card.Click += Card_Click;
            deck.RemoveAt(idx);
            return card;
        }


        private CardPictureBox GetFreeField(List<CardPictureBox> fields) => (
            from field in fields
            where field.isFree
            select field
            ).FirstOrDefault();

        public void PutCardInHand(CardDto dto)
        {
            currentPosition.Offset(xOffset, 0);

            CardPictureBox card = new()
            {
                Card = dto
            };
            card.SetInHand(currentPosition);

            ClearListBoxes();
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            card.Click += Card_Click;
            card.Anchor = AnchorStyles.Bottom;
            hand.Add(card);
            Controls.Add(card);
            card.BringToFront(); 
        }

        private void ReadCard(CardDto card)
        {
            if (card == null || card.id == null) return;

            cardDescription.Text = card.description;
            cardImg.BackgroundImage = card.img;
            bool isMonster = card.type.Equals("MONSTER");
            tbATK.Text = card.atk.ToString();
            tbDEF.Text = card.def.ToString();
            tbATK.Visible = isMonster;
            tbDEF.Visible = isMonster;
            textBox1.Visible = isMonster;
            textBox2.Visible = isMonster;
        }

        private void DestroyCard(CardPictureBox card)
        {
            Controls.Remove(card);
            RepositionCards(card);
            hand.Remove(card);
            card.Dispose();
            currentPosition.Offset(-xOffset, 0);
        }

        private void DestroyCardFromField()
        {
            SelectedCardOnField.Image = null;
            SelectedCardOnField.Card = null;
            SelectedCardOnField.isFree = true;
        }


        // ListBox logic
        private void ClearListBoxes()
        {
            Controls.Remove(monsterActions);
            Controls.Remove(spellActions);
            Controls.Remove(trapActions);
            Controls.Remove(monsterActionsField);
            Controls.Remove(spellActionsField);
            Controls.Remove(trapActionsField);
        }

        private void CreateAction(ListBox action, CardPictureBox card, bool isField)
        {
            int x = card.Location.X + (isField ? p1Zone.Location.X : 0);
            int y = card.Location.Y + (isField ? p1Zone.Location.Y : 0);
            int offset = isField ? -120 : hoverHeight - 100;
            action.Location = new Point(x, y + offset);
            Controls.Add(action);
            action.BringToFront();
            action.ClearSelected();
        }

        // ListBox creation
        private void Card_Click(object sender, EventArgs e)
        {
            SelectedCard = sender as CardPictureBox;

            ListBox selectedActions;
            if (SelectedCard.Card.type.Equals("MONSTER")) selectedActions = monsterActions;
            else if (SelectedCard.Card.subType.Equals("SPELL")) selectedActions = spellActions;
            else selectedActions = trapActions;

            if (Controls.Contains(selectedActions))
            {
                ClearListBoxes();
                return;
            }

            ClearListBoxes();
            CreateAction(selectedActions, SelectedCard, false);
        }

        private void CardField_Click(object sender, EventArgs e)
        {
            SelectedCardOnField = sender as CardPictureBox;
            if (SelectedCardOnField == null || SelectedCardOnField.Card == null || SelectedCardOnField.Card.id == null) return;

            ListBox selectedActions;
            if (SelectedCardOnField.Card.type.Equals("MONSTER")) selectedActions = monsterActionsField;
            else if (SelectedCardOnField.Card.subType.Equals("SPELL")) selectedActions = spellActionsField;
            else selectedActions = trapActionsField;
            
            if (Controls.Contains(selectedActions))
            {
                ClearListBoxes();
                return;
            }

            ClearListBoxes();
            CreateAction(selectedActions, SelectedCardOnField, true);
        }

        // ListBox actions
        private void monsterActions_Click(object sender, EventArgs e)
        {
            CardPictureBox field = GetFreeField(monsterFields);
            CardPictureBox card = SelectedCard;
            if (monsterActions.SelectedIndex == -1) return;
            MonsterActions item = monsterActions.Items[monsterActions.SelectedIndex].ToString().ToAction<MonsterActions>();
            switch (item)
            {
                case MonsterActions.SUMMON_ATTACK:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = monsterFields.IndexOf(field);
                    ChangePictureBoxImageAtk(field, card.Card);
                    field.isFree = false;
                    break;
                case MonsterActions.SUMMON_DEFENSE:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = monsterFields.IndexOf(field);
                    ChangePictureBoxImageDef(field, card.Card);
                    field.isFree = false;
                    break;
                case MonsterActions.SUMMON_FACE_DOWN:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = monsterFields.IndexOf(field);
                    ChangePictureAndDrawCardbackMonster(field, card.Card);
                    field.isFree = false;
                    break;

                case MonsterActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    break;
                case MonsterActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    break;
            }

            DestroyCard(card);
            ClearListBoxes();
        }

        private void spellActions_Click(object sender, EventArgs e)
        {
            CardPictureBox field = GetFreeField(spellFields);
            CardPictureBox card = SelectedCard;
            if (spellActions.SelectedIndex == -1) return;
            SpellActions item = spellActions.Items[spellActions.SelectedIndex].ToString().ToAction<SpellActions>();
            switch (item)
            {
                case SpellActions.ACTIVATE:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = spellFields.IndexOf(field);
                    ChangePictureBoxImageAtk(field, card.Card);
                    field.isFree = false;
                    break;
                case SpellActions.SET:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = spellFields.IndexOf(field);
                    ChangePictureAndDrawCardback(field, card.Card);
                    field.isFree = false;
                    break;

                case SpellActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    break;
                case SpellActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    break;
            }

            DestroyCard(card);
            ClearListBoxes();
        }

        private void trapActions_Click(object sender, EventArgs e)
        {
            CardPictureBox field = GetFreeField(spellFields);
            CardPictureBox card = SelectedCard;
            if (trapActions.SelectedIndex == -1) return;
            TrapActions item = trapActions.Items[trapActions.SelectedIndex].ToString().ToAction<TrapActions>();
            switch (item)
            {
                case TrapActions.SET:
                    if (field == null)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    card.Card.position = spellFields.IndexOf(field);
                    ChangePictureAndDrawCardback(field, card.Card);
                    field.isFree = false;
                    break;

                case TrapActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    break;
                case TrapActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    break;
            }
            DestroyCard(card);
            ClearListBoxes();
        }

        private void monsterActionsField_Click(object sender,EventArgs e)
        {
            CardPictureBox card = SelectedCardOnField;
            if (monsterActionsField.SelectedIndex == -1) return;
            MonsterActions item = monsterActionsField.Items[monsterActionsField.SelectedIndex].ToString().ToAction<MonsterActions>();
            switch (item)
            {
                case MonsterActions.FLIP:
                    ChangeImageOnFieldMonsterFlip(monsterFields[card.Card.position], card.Card);
                    break;

                case MonsterActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    DestroyCardFromField();
                    break;
                case MonsterActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    DestroyCardFromField();
                    break;
            }
            ClearListBoxes();
        }

        private void spellActionsField_Click(object sender, EventArgs e)
        {
            CardPictureBox card = SelectedCardOnField;
            if (spellActionsField.SelectedIndex == -1) return;
            SpellActions item = spellActionsField.Items[spellActionsField.SelectedIndex].ToString().ToAction<SpellActions>();
            switch (item)
            {
                case SpellActions.ACTIVATE:
                    ChangePictureBoxImageAtk(spellFields[card.Card.position], card.Card);
                    break;

                case SpellActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    DestroyCardFromField();
                    break;
                case SpellActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    DestroyCardFromField();
                    break;
            }

            ClearListBoxes();
        }

        private void trapActionsField_Click(object sender, EventArgs e)
        {
            CardPictureBox card = SelectedCardOnField;
            if (trapActionsField.SelectedIndex == -1) return;
            TrapActions item = trapActionsField.Items[trapActionsField.SelectedIndex].ToString().ToAction<TrapActions>();
            switch (item)
            {
                case TrapActions.ACTIVATE:
                    ChangePictureBoxImageAtk(spellFields[card.Card.position], card.Card);
                    break;

                case TrapActions.SEND_DECK:
                    deck.Add(card.Card);
                    UpdateDeckLabel(deck);
                    DestroyCardFromField();
                    break;
                case TrapActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card.Card);
                    UpdateGraveyardLabel(graveyardCards);
                    DestroyCardFromField();
                    break;
            }

            ClearListBoxes();
        }

        // Phases logic
        private void btnDP_Click(object sender, EventArgs e)
        {
            if (deck.Count <= 0) NewGame();

            if (hand.Count >= 4)
            {
                MessageBox.Show("You can have only 4 cards in hand!", "Cards in Hand");
                return;
            }

            currentPhase++;
            btnDP.Enabled = false;
            Draw();
            ClearListBoxes();
        }

        private void btnEP_Click(object sender, EventArgs e)
        {
            currentPhase = 0;
            btnDP.Enabled = true;
            ClearListBoxes();
        }

        // Card position effects (ATK, DEF, Flip, etc.)
        private void ChangePictureBoxImageAtk(CardPictureBox p, CardDto card)
        {
            p.Card = card;
            p.isDefense = false;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ChangePictureAndDrawCardback(CardPictureBox p, CardDto card)
        {
            p.Card = card;
            p.Image = Properties.Resources.wp2866512;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ChangePictureAndDrawCardbackMonster(CardPictureBox p, CardDto card)
        {
            Image cardback = Properties.Resources.wp2866512;
            cardback.RotateFlip(RotateFlipType.Rotate90FlipNone);

            p.Card = card;
            p.isDefense = true;
            p.Image = cardback;
            p.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ChangeImageOnFieldMonsterFlip(CardPictureBox p, CardDto card)
        {
            p.Card = card;

            if(!p.isDefense)
                p.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            p.isDefense = !p.isDefense;
            p.SizeMode = PictureBoxSizeMode.Zoom; 
        }

        private void ChangePictureBoxImageDef(CardPictureBox p, CardDto card)
        {
            p.Card = card;
            p.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            p.isDefense = true;
            p.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // Card effects/events
        private void Card_MouseLeave(object sender, EventArgs e)
        {
            CardPictureBox card = sender as CardPictureBox;
            Controls.SetChildIndex(card, zIndex);
            card.Location = new Point(card.Location.X, card.Location.Y + hoverHeight);
            card.Size = CardPictureBox.defaultSize;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            CardPictureBox card = (sender as CardPictureBox);
            ReadCard(card.Card);
            zIndex = Controls.GetChildIndex(card);
            card.Size = hoverSize;
            card.Location = new Point(card.Location.X, card.Location.Y - hoverHeight);
            card.BringToFront();
        }

        private static void RepositionCards(CardPictureBox card)
        {
            Point lastPosition = card.Location;
            for (int idx = hand.IndexOf(card) + 1; idx < hand.Count; idx++)
            {
                CardPictureBox currentCard = hand[idx];
                Point tmp = currentCard.Location;
                currentCard.Location = lastPosition;
                lastPosition = tmp;
            }
        }

        private void Field_MouseEnter(object sender, EventArgs e) => ReadCard((sender as CardPictureBox).Card);

        // Life points calculation
        private void btnAddition_Click(object sender, EventArgs e)
        {
            int newLifePoints = int.Parse(tbP1LifePoints.Text) + (int)nudCalculate.Value;
            if (newLifePoints > nudCalculate.Maximum)
            {
                MessageBox.Show("The Maximum Life Points is 4000!", "Maximum Life Points Error");
                return;
            }

            tbP1LifePoints.Text = newLifePoints.ToString();
            pbP1.Value = newLifePoints;
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            int newLifePoints = int.Parse(tbP1LifePoints.Text) - (int)nudCalculate.Value;
            if (newLifePoints <= 0) NewGame();
            tbP1LifePoints.Text = newLifePoints.ToString();
            pbP1.Value = newLifePoints;

        }

        private void NewGame()
        {
            
            if (MessageBox.Show("Do you want to play again?", "Game Over", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Duel duel = new();
                this.Hide();
                duel.ShowDialog();
                this.Close();
            }
            else
            {
                MainMenu menu = new();
                this.Hide();
                menu.ShowDialog();
                this.Close();
            }
        }

        // Select Deck
        private void SelectDeck_Click(object sender, EventArgs e)
        {
            FileUtilities.CallFileExplorer(new OpenFileDialog(), (dialog) =>
            {
                deck.Clear();
                ReadDeckAsync(dialog.FileName);              
            });          
            btnDP.Enabled = true;
            UpdateDeckLabel(deck);
            
          
        }

        private async void ReadDeckAsync(string s)
        {
            using FileStream fs = new(s, FileMode.Open);

            Deck d = await System.Text.Json.JsonSerializer.DeserializeAsync<Deck>(fs);
            foreach (CardDto card in d.cards)
            {
                card.img = Middleware.Controllers.YGOController.GetImage(card.cardId);
                deck.Add(card);
            }
            UpdateDeckLabel(deck);
            for (int i = 0; i < 4; ++i)
                Draw();
        }

        // Graveyard functionality
        private void Graveyard_Click(object sender, EventArgs e) => new Graveyard(graveyardCards, deck, this).ShowDialog();

        public void UpdateGraveyardLabel(List<CardDto> cards) => lbGraveyard.Text = cards.Count.ToString();

        public void UpdateDeckLabel(List<CardDto> cards) => lbDeckCardsNum.Text = cards.Count.ToString();

        public int CurrentHandCount() => hand.Count;

        
        //Below function is for form flickering (makes all animations look smoother)
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
