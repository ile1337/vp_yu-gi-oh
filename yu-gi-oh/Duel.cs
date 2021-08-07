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
        private static int ctr = 0;
        private ListBox monsterActions = new ListBox();
        private ListBox spellActions = new ListBox();
        private ListBox trapActions = new ListBox();
        public CardPictureBox c;
        public bool monsterField1 = true;
        public bool monsterField2 = false;
        public bool monsterField3 = false;
        

        public Duel()
        {
            InitializeComponent();
            setMonsterActions();
            setSpellActions();
            setTrapActions();
            tbATK.Visible = false;
            tbDEF.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            pbP1.Maximum = int.Parse(tbP1LifePoints.Text);
            pbP1.Value = int.Parse(tbP1LifePoints.Text);
            pbP2.Maximum = int.Parse(tbP2LifePoints.Text);
            pbP2.Value = int.Parse(tbP2LifePoints.Text);
        }

        public void setMonsterActions()
        {
            monsterActions.Items.Add("Summon in Attack");
            monsterActions.Items.Add("Summon in Defense");
            monsterActions.Items.Add("Summon Face Down");
            monsterActions.Items.Add("Send to Graveyard");
            monsterActions.Items.Add("Send to Deck");
            monsterActions.BorderStyle = BorderStyle.Fixed3D;
            monsterActions.BackColor = Color.MediumPurple;
            monsterActions.Size = new Size(150, 125);
            monsterActions.Font = new Font(monsterActions.Font, FontStyle.Bold);
        }

        public void setSpellActions()
        {
            spellActions.Items.Add("Activate");
            spellActions.Items.Add("Send to Graveyard");
            spellActions.Items.Add("Set");
            spellActions.Items.Add("Send to Deck");
            spellActions.BorderStyle = BorderStyle.Fixed3D;
            spellActions.BackColor = Color.MediumPurple;
            spellActions.Size = new Size(150, 125);
            spellActions.Font = new Font(spellActions.Font, FontStyle.Bold);
        }

        public void setTrapActions()
        {
            trapActions.Items.Add("Send to Graveyard");
            trapActions.Items.Add("Set");
            trapActions.Items.Add("Send to Deck");
            trapActions.BorderStyle = BorderStyle.Fixed3D;
            trapActions.BackColor = Color.MediumPurple;
            trapActions.Size = new Size(150, 125);
            trapActions.Font = new Font(spellActions.Font, FontStyle.Bold);
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
            Controls.Remove(monsterActions);
            Controls.Remove(spellActions);
            Controls.Remove(trapActions);
            card.MouseEnter += Card_MouseEnter;
            card.MouseLeave += Card_MouseLeave;
            card.Click += Card_Click;
            return card;
        }
        private void ReadCard(CardDto card)
        {
            cardDescription.Text = card.description;
            cardImg.BackgroundImage = card.img;
            if (card.type.Equals("MONSTER"))
            {
                tbATK.Text = card.atk.ToString();
                tbDEF.Text = card.def.ToString();
                tbATK.Visible = true;
                tbDEF.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                tbATK.Visible = false;
                tbDEF.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            CardPictureBox card = sender as CardPictureBox;
            c = card;
            if (card.Card.type.Equals("MONSTER"))
            {
                monsterActions.Location = new Point(card.Location.X, card.Location.Y + hoverHeight - 100);
                Controls.Remove(spellActions);
                Controls.Remove(trapActions);
                Controls.Add(monsterActions);
                monsterActions.BringToFront();
                monsterActions.ClearSelected();
                monsterActions.DoubleClick += monsterActions_DoubleClick;
            }
            else if (card.Card.subType.Equals("SPELL"))
            {
                spellActions.Location = new Point(card.Location.X, card.Location.Y + hoverHeight - 100);
                Controls.Remove(monsterActions);
                Controls.Remove(trapActions);
                Controls.Add(spellActions);
                spellActions.BringToFront();
                spellActions.ClearSelected();
            }
            else
            {
                trapActions.Location = new Point(card.Location.X, card.Location.Y + hoverHeight - 100);
                Controls.Remove(monsterActions);
                Controls.Remove(spellActions);
                Controls.Add(trapActions);
                trapActions.BringToFront();
                trapActions.ClearSelected();
            }
            
        }

        private void monsterActions_DoubleClick(object sender, EventArgs e)
        {
            monsterActions_Click(c);
            Invalidate();
        }

        private void monsterActions_Click(CardPictureBox card)
        {
            //pictureBox10.BackgroundImage.Dispose();
            //pictureBox11.BackgroundImage.Dispose();
            //pictureBox12.BackgroundImage.Dispose();
            if (monsterActions.SelectedIndex != -1)
            {
                var item = monsterActions.Items[monsterActions.SelectedIndex].ToString();
                if (item.Equals("Summon in Attack"))
                {
                    if (monsterField1)
                    {
                        ChangePictureBoxBackgroundImage(pictureBox10, card.Card.img);
                        monsterField1 = false;
                        monsterField2 = true;
                    }
                    else if(monsterField2)
                    {
                        ChangePictureBoxBackgroundImage(pictureBox11, card.Card.img);
                        monsterField2 = false;
                        monsterField3 = true;
                    }
                    else if(monsterField3)
                    {
                        ChangePictureBoxBackgroundImage(pictureBox12, card.Card.img);
                        monsterField3 = false;
                    }
                }
            }
        }

        private void ChangePictureBoxBackgroundImage(PictureBox p ,Image image)
        {
            //p.BackgroundImage.Dispose();//dispose the old image.

            p.BackgroundImage = image;
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
            ++ctr;
            if (ctr >= 4)
            {
                btnDP.Enabled = false;
            }
            else
            {
                btnDP.Enabled = true;
            }
            if (ctr < 4 && ctr > 0)
            {
                btnDP.Enabled = true;
                btnEP.Enabled = true;
            }
            Draw();
        }

        private void btnEP_Click(object sender, EventArgs e)
        {
            --ctr;
            if (ctr <= 0)
            {
                btnEP.Enabled = false;
            }
            else
            {
                btnEP.Enabled = true;
            }
            if (ctr < 4 && ctr > 0)
            {
                btnDP.Enabled = true;
                btnEP.Enabled = true;
            }

            Controls.Remove(monsterActions);
            Controls.Remove(spellActions);
            Controls.Remove(trapActions);
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

        private void btnAddition_Click(object sender, EventArgs e)
        {
            int addition_value = (int)nudCalculate.Value;
            int newLifePoints = int.Parse(tbP1LifePoints.Text);
            newLifePoints += addition_value;
            if (newLifePoints <= nudCalculate.Maximum)
            {
                tbP1LifePoints.Text = newLifePoints.ToString();
                pbP1.Value = newLifePoints;
            }
            else
            {
                MessageBox.Show("The Maximum Life Points is 8000!", "Maximum Life Points Error");
                return;
            }
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            int subtraction_value = (int)nudCalculate.Value;
            int newLifePoints = int.Parse(tbP1LifePoints.Text);
            newLifePoints -= subtraction_value;
            if(newLifePoints > 0)
            {
                tbP1LifePoints.Text = newLifePoints.ToString();
                pbP1.Value = newLifePoints;
            }
            else
            {
                if (MessageBox.Show("Do you want to play again?", "Game Over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Duel duel = new Duel();
                    hand.Clear();
                    duel.ShowDialog();
                    this.Close();
                }
                else return;
            }
            
        }
    }
}
