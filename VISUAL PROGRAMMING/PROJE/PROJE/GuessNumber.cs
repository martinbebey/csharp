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
    public partial class frmguessnumber : Form
    {
        int num;
        int count;
        public frmguessnumber()
        {
            InitializeComponent();

        }
       


        private void btnguess_Click(object sender, EventArgs e)
        {

                
                if (num < Convert.ToInt32(txtguess.Text))
                {
                    lblguess.Text = "Please, Enter a smaller number!";
                    count++;
                }
                else if (num > Convert.ToInt32(txtguess.Text))
                {
                    lblguess.Text = "Please, Enter a bigger number!";
                    count++;
                }
                else 
                {
                    pictureBox1.Visible = false;
                    lblguess.Visible = false;
                    btnwon.Visible = true;
                    lblstart.Visible = false;
                    lblanswer.Visible = false;
                    lblkeep.Visible = false;
                }
                txtguess.Text = "";


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random r = new Random();
            num = r.Next(100);
            lblanswer.Visible = true;
            lblkeep.Visible = true;
           
        }

        private void btnwon_Click(object sender, EventArgs e)
        {
            btnwon.Visible = false;
            pictureBox1.Visible = true;
            lblstart.Visible = true;
            lblguess.Visible = true;
            lblguess.Text = "";
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmgames frm = new frmgames();
            frm.Show();
        }
    }
}
