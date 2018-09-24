using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UpdateStudent : Form
    {
        public UpdateStudent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int win, age, phone, gpa, accountBalance, coursesEnrolled;
            string name, major, address, gender, level;

            win = int.Parse(textBox1.Text);
            age = int.Parse(textBox4.Text);
            phone = int.Parse(textBox5.Text);
            gpa = int.Parse(textBox7.Text);
            accountBalance = int.Parse(textBox10.Text);
            coursesEnrolled = int.Parse(textBox11.Text);
            name = textBox2.Text;
            major = textBox3.Text;
            address = textBox6.Text;
            gender = textBox8.Text;
            level = textBox9.Text;

            StringBuilder stringBuilder = new StringBuilder();
            string queryResult = "", password = "Changem3", connStr = "server=dbase.cs.wmich.edu;uid=mbdbuser;password=" + password + ";database=mbdb;";
            MySqlConnection connection = new MySqlConnection(connStr);
            connection.Open();

            string query = "INSERT INTO students VALUES ('" + win + "', '" + name +"', '"+ major +"', '" + age + "', '" + phone + "', '" + address + "', '" + gpa + "', '" + gender + "', '" + level + "', '" + accountBalance + "', '"+ coursesEnrolled + "');";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            


            try
            {

               cmd.ExecuteNonQuery();

               MessageBox.Show("UPDATE SUCCESSFUL!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

            catch (Exception ex) // catching any exceptions
            {

            }

            //if (queryResult != "")
            //{

            //    this.Hide();
            //    Admin adminForm = new Admin();
            //    adminForm.Show();
            //}

            //else
            //{
            //    MessageBox.Show("Login failed. Username and/or password is invalid. Please try again.", "Login Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            connection.Close();
            this.Hide();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
