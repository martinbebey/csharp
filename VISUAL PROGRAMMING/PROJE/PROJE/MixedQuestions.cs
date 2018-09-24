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
    public partial class frmquiz : Form
    {
        public frmquiz()
        {
            InitializeComponent();
        }

        int sayac = 1;

        private void LoadData()
        {
            try
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT NUMBER1,OPERATION,NUMBER2,RESULT FROM QUIZ WHERE ID=?", oleDbConn);
                cmd.Parameters.Add(":ID", OleDbType.Char, 255).Value = sayac++;
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string number1 = rdr["NUMBER1"].ToString();
                    string number2 = rdr["NUMBER2"].ToString();
                    string operation = rdr["OPERATION"].ToString();
                    string result = rdr["RESULT"].ToString();
                    txtnumber1.Text = number1;
                    txtoperation.Text = operation;
                    txtnumber2.Text = number2;
                    txtresult.Text = result;
                }
                rdr.Close();
                oleDbConn.Close();
            }
            catch
            {
                MessageBox.Show("error");

            }



        }
        

        private void buttonNext_Click(object sender, EventArgs e)
        {
            textresult.Text = " ";
            LoadData();
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtresult.Visible = true;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            txtresult.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmarithmeticaloperations frm = new frmarithmeticaloperations();
            frm.Show();
        }

        
    }
}
