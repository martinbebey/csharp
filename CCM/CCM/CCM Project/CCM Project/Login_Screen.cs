/*CCM login
 this is the first window the users sees so they can login to the use the other functions
 It is also the driver window or main thread (program will be running until it is closed)


 
 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace CCM_Project
{
    public partial class Form1 : Form // the login form
    {
        private string userID;
        private string password;
        private int count; // counts the number of DB results that match userID and pwd pair

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //login button
        {
            MySql.Data.MySqlClient.MySqlConnection conn; //connection variable
            string myConnectionString; //string to connect to db

            myConnectionString = "server=localhost;user id=root;pwd=root;database=ccm_test;";

            //query to the db
            string query = "SELECT * FROM employee WHERE id = '" + userID + "' AND pw = MD5('" + password + "')";

            

            try
            {
                //open a new connection and sql command (query to execute)
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                //reader executes the command and is used to read the results
                MySqlDataReader reader = cmd.ExecuteReader();

                //count is incremented for every result read
                while (reader.Read())
                {
                    count++;
                }

                //the connection and the reader are closed
                reader.Close();
                conn.Close();

                if (count == 1)
                {
                    //this.Hide(); 
                    Homescreen Hs_open = new Homescreen();
                    Hs_open.Show();
                    count = 0;                    
                    //this.Close();
                }

                else
                {
                    MessageBox.Show("Wrong id or password please try again.");
                }
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Can not open connection! \n\n" + ex);
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            userID = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("For Assistance please contact the  Test & Development team or:\nMartin Bebey: mbebey@mobis-usa.com\ntwong@mobis-usa.com");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.Show();
        }
    }
}
