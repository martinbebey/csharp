using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using WindowsFormsApplication1; 


namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        private bool signedIn = false;
        public Login()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void login_Click(object sender, EventArgs e)
        {
            MD5 md5 = MD5.Create();
            Button loginButton = (Button)sender;
            StringBuilder stringBuilder = new StringBuilder();
            signedIn = false;
            //string password = "Changem3", connStr = "server=dbase.cs.wmich.edu;uid=mbdbuser;password=" + password + ";database=mbdb;";
            string password = "martin6030", connStr = "server=mb.comuvrtek8ne.us-west-2.rds.amazonaws.com;uid=martin;password=" + password + ";database=martindb;";

            string username = usernameBox.Text, userPass = passwordBox.Text, queryResult = "", md5Hash = GetMd5Hash(userPass);
            MySqlConnection connection = new MySqlConnection(connStr); 

            
            string query = "SELECT * FROM users WHERE username LIKE '" + username + "' AND password LIKE '" + md5Hash + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open(); 

            try
            {
                
                if (query.Split(' ')[1] != "COUNT(*)")//dealing with multiple results
                {
                    MySqlDataReader rdr = cmd.ExecuteReader();//the db reader
           
                    while (rdr.Read())
                    {
                        if (rdr.FieldCount > 1)//more than  1 column in result
                        {
                            stringBuilder.AppendFormat("{0,-14}:  {1,10:N0}", rdr[0], rdr[1]);
                        }

                        else// only 1 column in result (to prevent out of range exception)
                        {
                            stringBuilder.AppendFormat("{0,-14}", rdr[0]);
                        }

                        queryResult = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }

                    rdr.Close();//closing the reader
                }

                else//dealing with single value result
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int number = Convert.ToInt32(result);
                        
                    }
                }
            }

            catch (Exception ex) // catching any exceptions
            {
                Console.WriteLine("ERROR on {0}, QUERY not done", ex);
            }

            if(queryResult != "")
            {
                signedIn = true;
                MessageBox.Show("Logged in as " + username, "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Admin adminForm = new Admin();
                adminForm.Show();
            }

            else
            {
                MessageBox.Show("Login failed. Username and/or password is invalid. Please try again.", "Login Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public string GetMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {           
            AcceptButton = loginButton;
        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {
            AcceptButton = loginButton;
        }
    }
}
