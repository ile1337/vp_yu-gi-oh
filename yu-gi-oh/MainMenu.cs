﻿using System;
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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();        
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Lobby form = new Lobby();
            //form.ShowDialog();
            this.BackgroundImage = Middleware.Controllers.YGOController.GetImage("86198326");
            Middleware.Controllers.YGOController.Clear();
        }

        private void btnDeckBuild_Click(object sender, EventArgs e)
        {
            this.Hide();
            Deckbuilder deckbuilderForm = new Deckbuilder();
            deckbuilderForm.ShowDialog();
        }
    }
}
