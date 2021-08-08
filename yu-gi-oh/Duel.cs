using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public static  Stack<CardPictureBox> graveyardToHand = new();
        public List<CardPictureBox> graveyardCards = new();
        public List<CardDto> deck = new();
        private static int currentPhase = 0;
        private CardPictureBox SelectedCard;

        // Action ListBoxes
        private ListBox monsterActions = new();
        private ListBox spellActions = new();
        private ListBox trapActions = new();

        // Field properties/constants
        private List<PictureBox> monsterFields = new();
        private int AvailableMonsterField = 0;

        public Duel()
        {
            InitializeComponent();
            CreateListBoxes();
            InitializeProperties();
            monsterFields = new List<PictureBox> { pictureBox10, pictureBox11, pictureBox12 };
            btnDP.Enabled = false;
            lblGraveYard.Text = graveyardCards.Count.ToString();
            lblGraveYard.Update();
            MessageBox.Show("At the start of the game please select a deck!", "Warning!");
            this.Refresh();
        }

        private void InitializeProperties()
        {
            currentPosition = new(312, 609);
            hand.Clear();
            zIndex = 0;
            deck.Clear();
            currentPhase = 0;
            SelectedCard = null;
            AvailableMonsterField = 0;
            monsterFields.Clear();
            lblGraveYard.Text = graveyardCards.Count.ToString();
            lblGraveYard.Refresh();
            this.Refresh();
        }


        // ListBox creation
        public void CreateListBoxes()
        {
            CreateListBox<MonsterActions, Hand>(monsterActions, monsterActions_Click);
            CreateListBox<SpellActions, Hand>(spellActions, null);
            CreateListBox<TrapActions, Hand>(trapActions, null);
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
            lbDeckCardsNum.Text = deck.Count.ToString();
        }

        // Card logic
        private CardPictureBox DrawCard()
        {
            int idx = random.Next(0, deck.Count);
            CardDto dto = deck[idx];
            currentPosition.Offset(xOffset, 0);
            CardPictureBox card = new(dto, currentPosition);
            ClearListBoxes();
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            card.Click += Card_Click;
            deck.RemoveAt(idx);
            return card;
        }

        public void PutCardInHand(int i)
        {
            currentPosition.Offset(xOffset, 0);
            CardPictureBox card = new CardPictureBox(graveyardToHand.Pop().Card,currentPosition);
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


        // ListBox logic
        private void ClearListBoxes()
        {
            Controls.Remove(monsterActions);
            Controls.Remove(spellActions);
            Controls.Remove(trapActions);
        }

        private void CreateAction(ListBox action, CardPictureBox card)
        {
            action.Location = new Point(card.Location.X, card.Location.Y + hoverHeight - 100);
            Controls.Add(action);
            action.BringToFront();
            action.ClearSelected();
        }

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
            CreateAction(selectedActions, SelectedCard);
        }

        // ListBox actions
        private void monsterActions_Click(object sender, EventArgs e)
        {
            CardPictureBox card = SelectedCard;
            if (monsterActions.SelectedIndex == -1) return;
            MonsterActions item = monsterActions.Items[monsterActions.SelectedIndex].ToString().ToAction<MonsterActions>();
            switch (item)
            {
                case MonsterActions.SUMMON_ATTACK:
                    if (AvailableMonsterField >= 3)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    ChangePictureBoxImageAtk(monsterFields[AvailableMonsterField++], card.Card.img);
                    DestroyCard(card);
                    break;
                case MonsterActions.SUMMON_DEFENSE:
                    if(AvailableMonsterField >= 3)
                    {
                        MessageBox.Show("No free fields!", "Fields error");
                        return;
                    }
                    ChangePictureBoxImageDef(monsterFields[AvailableMonsterField++], card.Card.img);
                    DestroyCard(card);
                    break;
                case MonsterActions.SEND_DECK:
                    deck.Add(card.Card);
                    lbDeckCardsNum.Text = deck.Count.ToString();
                    DestroyCard(card);
                    break;
                case MonsterActions.SEND_GRAVEYARD:
                    graveyardCards.Add(card);
                    lblGraveYard.Text = graveyardCards.Count.ToString();
                    DestroyCard(card);
                    break;

            }
            ClearListBoxes();
        }

        // Phases logic
        private void btnDP_Click(object sender, EventArgs e)
        {
           if(deck.Count <= 0)
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
            

            currentPhase++;
            // TODO: Uncomment for production, commented for Debug reasons
            // btnDP.Enabled = false;
            Draw();
            ClearListBoxes();
        }

        private void btnEP_Click(object sender, EventArgs e)
        {
            currentPhase = 0;
            btnDP.Enabled = true;
            ClearListBoxes();
        }


        // Card effects/events
        private void ChangePictureBoxImageAtk(PictureBox p, Image image)
        {
            p.Image = image;
            p.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ChangePictureBoxImageDef(PictureBox p, Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            p.Image = image;
            p.SizeMode = PictureBoxSizeMode.Zoom;
        }

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

        // Life points calculation
        private void btnAddition_Click(object sender, EventArgs e)
        {
            int newLifePoints = int.Parse(tbP1LifePoints.Text) + (int)nudCalculate.Value;
            if (newLifePoints > nudCalculate.Maximum)
            {
                MessageBox.Show("The Maximum Life Points is 8000!", "Maximum Life Points Error");
                return;
            }

            tbP1LifePoints.Text = newLifePoints.ToString();
            pbP1.Value = newLifePoints;
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            int newLifePoints = int.Parse(tbP1LifePoints.Text) - (int)nudCalculate.Value;
            if (newLifePoints <= 0)
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
            tbP1LifePoints.Text = newLifePoints.ToString();
            pbP1.Value = newLifePoints;

        }

        // Select Deck
        private void button1_Click(object sender, EventArgs e)
        {
            FileUtilities.CallFileExplorer(new OpenFileDialog(), (dialog) =>
            {
                deck.Clear();
                ReadDeckAsync(dialog.FileName);              
            });          
            btnDP.Enabled = true;
            lbDeckCardsNum.Text = deck.Count.ToString();
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
            lbDeckCardsNum.Text = deck.Count.ToString();
        }



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

        // Graveyard functionality
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            lblGraveYard.Text = graveyardCards.Count.ToString();
            Graveyard graveyard = new Graveyard(graveyardCards,deck,graveyardToHand,this);
            graveyard.ShowDialog();
        }
    }
}
