using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class frmlogin : Form
    {

        public frmlogin()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT USERNAME,PASSWORD FROM USERINFORMATION WHERE USERNAME=? AND PASSWORD=?", oleDbConn);
                cmd.Parameters.Add(":USERNAME", OleDbType.Char, 255).Value = txtusername.Text;
                cmd.Parameters.Add(":PASSWORD", OleDbType.Char, 255).Value = txtpassword.Text;
                OleDbDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    this.Hide();
                    rdr.Close();
                    oleDbConn.Close();

                    frmcategory frm = new frmcategory();
                    frm.ShowDialog(this);

                    
                }
                else 
                {
                    rdr.Close();
                    oleDbConn.Close();
                    MessageBox.Show(this, "Enter appropriate password or user name!", "Attention",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata:" + ex.ToString());
            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            LoadData();
            
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linklblsign_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmnewmember frm = new frmnewmember();
            frm.ShowDialog(this);
            LoadData();
           
        }

   


       

        

       
      
    }
}
