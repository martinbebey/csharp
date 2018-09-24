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
    public partial class frmgames : Form
    {
        public frmgames()
        {
            InitializeComponent();
        }

        private void btnguessnumber_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmguessnumber frm = new frmguessnumber();
            frm.ShowDialog(this);
        }

        private void btnchoose_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmchooseoperation frm = new frmchooseoperation();
            frm.ShowDialog(this);
        }

        private void btnshapes_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmexampleofgames frm = new frmexampleofgames();
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
