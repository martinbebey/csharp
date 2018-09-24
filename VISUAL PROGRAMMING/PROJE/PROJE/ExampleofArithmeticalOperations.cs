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
    public partial class frmlevels : Form
    {
        public frmlevels()
        {
            InitializeComponent();
            txtresult.Visible = false;
            
            
        }
        
        
        private void LoadData()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            int rnd = r.Next(0, 9);
            try
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT NUMBER1,OPERATION,NUMBER2,RESULT FROM OperationLevel1 WHERE ID=?", oleDbConn);
                cmd.Parameters.Add(":ID", OleDbType.Char, 255).Value = rnd;
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            textresult.Text = "";
            LoadData();

        }

        private void btnfinish_Click(object sender, EventArgs e)
        {
            this.Close();
            frmarithmeticaloperations frm = new frmarithmeticaloperations();
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtresult.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtresult.Visible = false;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            textresult.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            textresult.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            textresult.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            textresult.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            textresult.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            textresult.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            textresult.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            textresult.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            textresult.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            textresult.Text  += "9";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LoadData();
        }

       
    }
}
