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
using System.Data.SqlClient;


namespace CCM_Project
{
    public partial class Homescreen : Form
    {
        private string text, date, description, duration;
        private char pad = ' ';
        private int spaces, padLeft, numberOfEvents = 0, i;
        private DateTime today = DateTime.Today;
        private DateTime eventDate, Date;
        //private bool firstLoad = true;
        private Label label;
        private MySqlDataAdapter adapter = new MySqlDataAdapter();
        private DataSet dataSet = new DataSet();

        public Homescreen()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Current_Assigments Ca_open = new Current_Assigments();
            Ca_open.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Team_Discussions Td_open = new Team_Discussions();
            Td_open.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Software_Versions Sv_open = new Software_Versions();
            Sv_open.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map_Versions Mv_open = new Map_Versions();
            Mv_open.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private string PadBoth(string str, int length)
        {
            spaces = length - str.Length;
            padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

        private void Homescreen_Load(object sender, EventArgs e)
        {
            //todaysDate = today.ToString("YYY.MM.DD");
            MySql.Data.MySqlClient.MySqlConnection conn, conn2, conn3;
            string myConnectionString;

            myConnectionString = "server=localhost;user id=root;pwd=root;database=ccm_test;";
            string query = "SELECT * FROM TeamEvents";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    date = (string)reader["date"];
                    eventDate = Convert.ToDateTime(date);

                    if (eventDate >= today)
                    {
                        ++numberOfEvents;
                        duration = (string)reader["duration"];
                        description = (string)reader["description"];
                        description = PadBoth(description, 174);
                        text = (date.PadLeft(11, pad) + description + duration.PadLeft(17, pad));
                        richTextBox1.AppendText(text);
                        richTextBox1.AppendText(Environment.NewLine);
                        richTextBox1.AppendText(Environment.NewLine);
                    }

                    else
                    {
                        conn2 = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                        conn2.Open();
                        query = "DELETE FROM teamEvents WHERE date like '" + date + "'";
                        cmd = new MySqlCommand(query, conn2);
                        cmd.ExecuteNonQuery();
                        conn2.Close();
                    }
                }

                reader.Close();
                conn.Close();
                
            }

            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);                
            }

            Date = today;

            label = new Label();
            label.Text = "Team";
            TeamScheduleTable.Controls.Add(label, 0, 0);

            label = new Label();
            label.Text = "Name";
            TeamScheduleTable.Controls.Add(label, 1, 0);

            label = new Label();
            label.Text = "Title";
            TeamScheduleTable.Controls.Add(label, 2, 0);

            label = new Label();
            label.Text = "Start Time";
            TeamScheduleTable.Controls.Add(label, 3, 0);

            label = new Label();
            label.Text = "Coordinator";
            TeamScheduleTable.Controls.Add(label, 0, 1);
                        
            label = new Label();
            label.Text = "Head of Team";
            TeamScheduleTable.Controls.Add(label, 0, 2);

            label = new Label();
            label.Text = "Automation";
            TeamScheduleTable.Controls.Add(label, 0, 3);

            label = new Label();
            label.Text = "Lamp";
            TeamScheduleTable.Controls.Add(label, 0, 4);

            label = new Label();
            label.Text = "KMC";
            TeamScheduleTable.Controls.Add(label, 0, 5);

            label = new Label();
            label.Text = "HMC";
            TeamScheduleTable.Controls.Add(label, 0, 6);

            for(i = 4; i < 9; ++i)
            {
                if((Date.DayOfWeek >= DayOfWeek.Monday) && (Date.DayOfWeek <= DayOfWeek.Friday))
                {
                    label = new Label();
                    label.Text = Date.ToString("dd") + "-" + Date.ToString("MMMM");
                    TeamScheduleTable.Controls.Add(label, i, 0);
                    Date = Date.AddDays(1);
                }

                else
                {
                    --i;
                    Date = Date.AddDays(1);
                }
            }

            query = "SELECT * FROM Employee";

            try
            {
                conn3 = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString);
                conn3.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn3);


                adapter.SelectCommand = cmd;
                adapter.Fill(dataSet);

                for (i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                {
                    //MessageBox.Show((string)dataSet.Tables[0].Rows[i].ItemArray[0]);

                    if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "Coordinator")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 1);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 1);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 1);
                    }

                    else if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "Head of Team")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 2);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 2);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 2);
                    }

                    else if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "Automation")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 3);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 3);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 3);
                    }

                    else if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "Lamp")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 4);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 4);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 4);
                    }

                    else if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "KMC")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 5);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 5);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 5);
                    }

                    else if ((string)dataSet.Tables[0].Rows[i].ItemArray[4] == "HMC")
                    {
                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[1];
                        TeamScheduleTable.Controls.Add(label, 1, 6);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[5];
                        TeamScheduleTable.Controls.Add(label, 2, 6);

                        label = new Label();
                        label.Text = (string)dataSet.Tables[0].Rows[i].ItemArray[6];
                        TeamScheduleTable.Controls.Add(label, 3, 6);
                    }
                }

                adapter.Dispose();
                cmd.Dispose();
                //MySqlDataReader reader = cmd.ExecuteReader 
                //reader.Close();
                conn3.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "https://uc.gmobis.com/Login_gmobis.aspx");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please log out and create an account via the log in window. For Assistance please contact the  Test & Development team or:\nMartin Bebey: mbebey@mobis-usa.com\ntwong@mobis-usa.com");
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void TeamScheduleTable_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To report an issue please contact the  Test & Development team or:\nMartin Bebey: mbebey@mobis-usa.com\ntwong@mobis-usa.com");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To request time off please talk to your mentor/supervisor/manager and update the timekeeping system.");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void teamEventsBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
