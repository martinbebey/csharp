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
    public partial class frmchooseoperation : Form
    {
        public frmchooseoperation()
        {
            InitializeComponent();
        }

        int count;

        private void btnsubtraction_Click(object sender, EventArgs e)
        {
            txt1.Text = "-";
            if (txt2.Visible == true) { txt2.Text = "-"; }
            if (txt3.Visible == true) { txt3.Text = "-"; }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            txt1.Text = "+";
            if (txt2.Visible == true) { txt2.Text = "+"; }
            if (txt3.Visible == true) { txt3.Text = "+"; }
        }

        private void btnmult_Click(object sender, EventArgs e)
        {
            txt1.Text = "*";
            if (txt2.Visible == true) { txt2.Text = "*"; }
            if (txt3.Visible == true) { txt3.Text = "*"; }
        }

        private void btndiv_Click(object sender, EventArgs e)
        {
            txt1.Text = "/";
            if (txt2.Visible == true) { txt2.Text = "/"; }
            if (txt3.Visible == true) { txt3.Text = "/"; }
            
        }
        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (txt1.Text == "-")
            {
                label18.Text = "TRUE";
                txt2.Text = " ";
                label19.Text = " ";
            }
            else
            {
                label18.Text = "FALSE";
            }
            
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            if (txt2.Text == "+")
            {
                label19.Text = "TRUE";

            }
            else
            {
                label19.Text = "FALSE";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            count++;
            if (count == 1)
            {
                groupBox1.Visible = false;
                label18.Visible = false;
                groupBox2.Visible = true;
                label19.Visible = true;
            }
            if (count == 2) {

                groupBox2.Visible = false;
                label19.Visible = true;
                groupBox3.Visible = true;
            }
	
            
	
                
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txt3.Text == "/")
            {
                label20.Text = "TRUE";

            }
            else
            {
                label20.Text = "FALSE";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmgames frm = new frmgames();
            frm.Show();
        }

        }
    }

