﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class StaffUpdate : Form
    {
        public StaffUpdate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int win = int.Parse(textBox1.Text);

            StringBuilder stringBuilder = new StringBuilder();
            string queryResult = "", password = "Changem3", connStr = "server=dbase.cs.wmich.edu;uid=mbdbuser;password=" + password + ";database=mbdb;";
            MySqlConnection connection = new MySqlConnection(connStr);
            connection.Open();

            string query = "DELETE FROM staff WHERE WIN like '" + win + "';";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            try
            {
               cmd.ExecuteNonQuery();
               MessageBox.Show("Update Successful.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

            catch (Exception ex) // catching any exceptions
            {

            }

            connection.Close();
            this.Hide();
        }
    }
}

