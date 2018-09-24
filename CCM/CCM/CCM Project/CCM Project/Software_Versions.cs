using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCM_Project
{
    public partial class Software_Versions : Form
    {
        public Software_Versions()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homescreen Hs_open = new Homescreen();
            Hs_open.Show();
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

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Ls_open = new Form1();
            Ls_open.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map_Versions Mv_open = new Map_Versions();
            Mv_open.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
