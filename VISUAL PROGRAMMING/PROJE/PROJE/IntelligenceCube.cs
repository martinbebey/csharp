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
    public partial class frmintelligencecube : Form
    {
        public frmintelligencecube()
        {
            InitializeComponent();
        }

        private void btnbegin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmexampleofintelligence frm = new frmexampleofintelligence();
            frm.ShowDialog(this);
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            frmcategory frm = new frmcategory();
            frm.Show();


        }
    }
}
