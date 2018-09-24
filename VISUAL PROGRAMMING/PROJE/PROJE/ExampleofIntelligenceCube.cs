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
    public partial class frmexampleofintelligence : Form
    {
        public frmexampleofintelligence()
        {
            InitializeComponent();
        }
        int sayac = 1;
        int count;
        int score;

        private void LoadData()
        {
            
            try
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Question,Answer FROM IntelligenceCube WHERE ID=?", oleDbConn);
                cmd.Parameters.Add(":ID", OleDbType.Char, 255).Value = sayac++;
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string number1 = rdr["Question"].ToString();
                    string number2 = rdr["Answer"].ToString();


                    textquestion.Text = number1;
                    txtanswer.Text = number2;
                }
                rdr.Close();
                oleDbConn.Close();
            }
            catch
            {
                MessageBox.Show("error");

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timeroyun.Start();
            LoadData();


        }

        private void timeroyun_Tick(object sender, EventArgs e)
        {

            if (progressBar.Value == progressBar.Minimum)
            {
                timeroyun.Stop();
                MessageBox.Show("Time is up! Your score is : "+score ,"SCOREBOARD", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            else
            {
                progressBar.Value = progressBar.Value - 1;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            frmcategory frm = new frmcategory();
            frm.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtres.Text == txtanswer.Text) 
            {
                count += 1;
                score = count * 10;
                lblScore.Text = score.ToString();
            }
            txtres.Text = "";
            LoadData();

        }
        private void Data()
        {
            oleDbConn.Open();
            OleDbCommand cmd = new OleDbCommand("INSERT INTO SCOREINFORMATION ([NAMES],[SURNAMES],[SCORE]) VALUES (?,?,?)", oleDbConn);
            cmd.Parameters.Add(":NAMES", OleDbType.Char, 25).Value = txtName.Text;
            cmd.Parameters.Add(":SURNAMES", OleDbType.Char, 25).Value = txtSurname.Text;
            cmd.Parameters.Add(":SCORE", OleDbType.Char, 25).Value = lblScore.Text;
            int count = cmd.ExecuteNonQuery();
            oleDbConn.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" && txtSurname.Text == "" )
            {
                MessageBox.Show("Please fill empty boxes");
                return;
            }

            Data();
            grpInfo.Visible = false;
            MessageBox.Show("Your score is saved","",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            grpInfo.Visible = true;
        }
        private void ShowScores()
        {
            try
            {
                oleDbConn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT NAMES,SURNAMES,SCORE FROM SCOREINFORMATION", oleDbConn);
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string name = rdr["NAMES"].ToString();
                    string sname = rdr["SURNAMES"].ToString();
                    string score = rdr["SCORE"].ToString();
                    
                    ListViewItem lvi = new ListViewItem(new string[] { name, sname, score });
                    lvData.Items.Add(lvi);
                }
                rdr.Close();
                oleDbConn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
                
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lvData.Visible = true;
            ShowScores();
        }
    }
}

