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
    public partial class frmcategory : Form
    {
        public frmcategory()
        {
            InitializeComponent();
        }

        private void rboperations_CheckedChanged(object sender, EventArgs e)
        {
            
            if(rboperations.Checked) 
            {
                
                Open();
            }
            
        }

        private void rbgames_CheckedChanged(object sender, EventArgs e)
        {
            if (rbgames.Checked)
               
                Open();
           
        }

        private void rbcube_CheckedChanged(object sender, EventArgs e)
        {
            if(rbcube.Checked)
                
                Open(); 
        }
        private void Open()
        {
            
            
            if(rboperations.Checked)
            {
                this.Hide();
                
                frmarithmeticaloperations frm=new frmarithmeticaloperations();
                frm.ShowDialog(this);
                
                
            }
            if(rbgames.Checked)
            {
                this.Hide();
                frmgames frm =new frmgames();
                frm.ShowDialog(this);
            }
             if(rbcube.Checked)
            {
                this.Hide();
                frmintelligencecube frm=new frmintelligencecube();
                frm.ShowDialog(this);
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmlogin login = new frmlogin();
            login.Show();
        }

        

    }
}
