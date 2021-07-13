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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(tbUsername.Text != "" && mtbPassword.Text != "" && mtbPassword2.Text != "")
            {
                DialogResult = DialogResult.OK;
            }        
        }

        private void tbUsername_Validating(object sender, CancelEventArgs e)
        {
            if(tbUsername.Text == "")
            {
                ep2.SetError(tbUsername, "Username needed");
                e.Cancel = true;
            }
            else
            {
                ep2.SetError(tbUsername, null);
                e.Cancel = false;
            }
        }

        private void mtbPassword_Validating(object sender, CancelEventArgs e)
        {
            if(mtbPassword.Text == "")
            {
                ep2.SetError(mtbPassword, "Password needed");
                e.Cancel = true;
            }
            else
            {
                ep2.SetError(mtbPassword, null);
                e.Cancel = false;
            }
        }

        private void mtbPassword2_Validating(object sender, CancelEventArgs e)
        {
            if(mtbPassword2.Text == "")
            {
                ep2.SetError(mtbPassword2, "You need to confirm the password");
                e.Cancel = true;
            }
            else if(!mtbPassword.Text.Equals(mtbPassword2.Text))
            {
                ep2.SetError(mtbPassword2, " Passwords dont match");
                e.Cancel = true;
            }
                
            else
            {
                ep2.SetError(mtbPassword2, null);
                e.Cancel = false;
            }
        }
    }
}
