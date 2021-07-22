﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Middleware.Models;
using Middleware.Models.Meta;

namespace yu_gi_oh
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            //_ = Middleware.Controllers.YGOController.DownloadAllImages(new string[] { "86198326", "24140059", "32207100" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            //if (tbUsername.Text != "" && mtbPassword.Text != "")
            //{
            //    HttpClient c = new HttpClient();
            //    c.BaseAddress = new Uri("/* TODO: api call to server */");
            //    HttpResponseMessage response = c.PostAsJsonAsync(/* TODO: api call to server */"insert URL here", tbUsername.Text);

            //await Middleware.Controllers.YGOController.PreLoadCache();
            this.Hide();
            MainMenu form = new MainMenu();
            form.ShowDialog();
            //}

            //Task.Factory.StartNew( async () => 
            //{
            //    CardDto card = new CardDto();
            //    card.name = "Dark";
            //    PageResponse<CardDto> res = await Middleware.Controllers.CardController.GetAllCardDtosShortAsync(card, 1, "atk");
            //});
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            
            if(tbUsername.Text == "")
            {
                ep1.SetError(tbUsername, "Username is needed");
                e.Cancel = true;
            }
            else
            {
                ep1.SetError(tbUsername, null);
                e.Cancel = false;
            }
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if(mtbPassword.Text == "")
            {
                ep1.SetError(mtbPassword, "Password is needed");
                e.Cancel = true;
            }
            else
            {
                ep1.SetError(mtbPassword, null);
                e.Cancel = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.ShowDialog();
        }
    }
}
