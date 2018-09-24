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
    public partial class frmnewmember : Form
    {
        public frmnewmember()
        {
            InitializeComponent();
        }

        

        private void btnsignin_Click(object sender, EventArgs e)
        {
            
            DateTime birtdate = dtdatetime.Value;
            string birthdatestring = birtdate.ToString("dd.MM.yyyy");
            if (rbfemale.Checked == true)
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO USERINFORMATION ([NAMES], [SURNAMES], [USERNAME], [PASSWORD], [REENTERPASSWORD], [BIRTHDATE], [GENDER]) VALUES (?,?,?,?,?,?,?)", oleDbConn);

                cmd.Parameters.Add("@NAMES", OleDbType.Char, 255).Value = txtname.Text;
                cmd.Parameters.Add("@SURNAMES", OleDbType.Char, 255).Value = txtsurname.Text;
                cmd.Parameters.Add("@USERNAME", OleDbType.Char, 255).Value = txtuser.Text;
                cmd.Parameters.Add("@PASSWORD", OleDbType.VarChar, 255).Value = txtpword.Text;
                cmd.Parameters.Add("@REENTERPASSWORD", OleDbType.VarChar, 255).Value = txtpwordagain.Text;
                cmd.Parameters.Add("@BIRTDATE", OleDbType.Char, 255).Value = birthdatestring;
                cmd.Parameters.Add("@GENDER", OleDbType.Char, 255).Value = rbfemale.Text;

                

                

                int count = cmd.ExecuteNonQuery();
                oleDbConn.Close();
                MessageBox.Show("Application is completed !");
                this.Close();
            }
            if (rbmale.Checked == true)
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO USERINFORMATION ([NAMES], [SURNAMES], [USERNAME], [PASSWORD], [REENTERPASSWORD], [BIRTHDATE], [GENDER]) VALUES (?,?,?,?,?,?,?)", oleDbConn);
                
                cmd.Parameters.Add(":NAMES", OleDbType.Char, 255).Value = txtname.Text;
                cmd.Parameters.Add(":SURNAMES", OleDbType.Char, 255).Value = txtsurname.Text;
                cmd.Parameters.Add(":USERNAME", OleDbType.Char, 255).Value = txtuser.Text;
                cmd.Parameters.Add(":PASSWORD", OleDbType.VarChar,255).Value = txtpword.Text;
                cmd.Parameters.Add(":RE-ENTERPASSWORD", OleDbType.VarChar,255).Value = txtpwordagain.Text;
                cmd.Parameters.Add(":BIRTDATE", OleDbType.Char,255).Value =birthdatestring;
                cmd.Parameters.Add(":GENDER", OleDbType.Char, 255).Value = rbmale.Text;



                int count = cmd.ExecuteNonQuery();
                oleDbConn.Close();
                MessageBox.Show("Application is completed !");
                this.Close();
                
            }

        }
    }
}
