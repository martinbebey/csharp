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

namespace CCM_Project
{
    public partial class CreateAccount : Form
    {
        private int id, count;
        private string name, email, password, rePassword, department;

        private void pwdBox_TextChanged(object sender, EventArgs e)
        {
            password = pwdBox.Text;
        }

        private void rePwdBox_TextChanged(object sender, EventArgs e)
        {
            rePassword = rePwdBox.Text;
        }

        private void depBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            department = depBox.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;
            myConnectionString = "server=localhost;user id=root;pwd=root;database=ccm_test;";

            //1st we try to see if this id already has an account. userID MUST be UNIQUE
            string query = "SELECT * FROM employee WHERE id = '" + id + "'";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);                
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    count++;
                }

                if (count == 1)
                {
                    MessageBox.Show("This user already has an account. If you forgot your password please contact the system admin.");
                    count = 0;
                    conn.Close();
                }

                else
                {
                    query = "INSERT INTO Employee (`id`, `name`, `email`, `pw`) VALUES ('" + id + "', '" + name + "', '" + email + "', MD5('" + password + "'))";

                    if (password != rePassword)
                    {
                        MessageBox.Show("Passwords don't match");
                    }

                    else
                    {
                        try
                        {
                            cmd = new MySqlCommand(query, conn);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Account created successfully");
                            conn.Close();
                            this.Close();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

                  

        }

        private void emailBox_TextChanged(object sender, EventArgs e)
        {
            email = emailBox.Text;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            name = nameBox.Text;
        }

        public CreateAccount()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            id = Convert.ToInt32(textBox1.Text);
        }
    }
}
