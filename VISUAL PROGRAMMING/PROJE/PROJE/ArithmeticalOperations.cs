using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmarithmeticaloperations : Form
    {
        public frmarithmeticaloperations()
        {
            InitializeComponent();
        }

        private void btnlevel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmlevels frm = new frmlevels();
            frm.ShowDialog(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmcategory frm = new frmcategory();
            frm.Show();
            
        }

        private void btnquiz_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmquiz frm = new frmquiz();
            frm.ShowDialog(this);
            
        }

        private void btnlevel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmlevel2 frm = new frmlevel2();
            frm.ShowDialog(this);


        }

        private void checklvl1_CheckedChanged(object sender, EventArgs e)
        {
            btnlevel2.Enabled = true;
        }

       
    }
}
