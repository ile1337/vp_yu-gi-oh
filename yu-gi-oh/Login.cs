using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Middleware.Models;
using Middleware.Models.Meta;

namespace yu_gi_oh
{
    public partial class Login : Form
    {
        Thread loadImages = new Thread(new ThreadStart(Middleware.Controllers.YGOController.DownloadAllImages));
        public Login()
        {
            InitializeComponent();
            loadImages.IsBackground = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {

            OAuth oauth = await Middleware.Controllers.AccountController.GetLoginToken(tbUsername.Text, mtbPassword.Text);
            Middleware.Controllers.HttpClientBuilder.SetToken(oauth.access_token);

            this.Hide();
            MainMenu form = new MainMenu();
            loadImages.Start();
            await Middleware.Controllers.YGOController.PreLoadCache();
            form.ShowDialog();
            loadImages.Join();
            this.Close();
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
