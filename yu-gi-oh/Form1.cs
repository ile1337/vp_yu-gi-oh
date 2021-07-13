using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if(tbUsername.Text != "" && mtbPassword.Text != "")
            {
                this.Hide();
                MainMenu form = new MainMenu();
                form.ShowDialog();
            }                       
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
