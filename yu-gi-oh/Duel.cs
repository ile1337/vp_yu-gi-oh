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
    public partial class Duel : Form
    {
        Login l = new Login();
        
        public Duel()
        {
            InitializeComponent();
        }

        private void pictureBox20_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBox20_MouseLeave(object sender, EventArgs e)
        {
            l.Hide();
        }

        private void pictureBox20_MouseEnter(object sender, EventArgs e)
        {
            l.Show(this);
        }
    }
}
