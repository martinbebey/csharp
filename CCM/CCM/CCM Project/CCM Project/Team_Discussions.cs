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
    public partial class Team_Discussions : Form
    {
        public Team_Discussions()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Ls_open = new Form1();
            Ls_open.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homescreen Hs_open = new Homescreen();
            Hs_open.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Current_Assigments Ca_open = new Current_Assigments();
            Ca_open.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map_Versions Mv_open = new Map_Versions();
            Mv_open.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Software_Versions Sv_open = new Software_Versions();
            Sv_open.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.230.22.35:7001/exlogin.jsp");
        }
    }
}
