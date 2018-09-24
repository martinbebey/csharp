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
    public partial class Current_Assigments : Form
    {
        public Current_Assigments()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homescreen Hs_open = new Homescreen();
            Hs_open.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 Ls_open = new Form1();
            Ls_open.Show();
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

        private void button10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://10.230.22.35:7001/exlogin.jsp");
        }

        private void Current_Assigments_Load(object sender, EventArgs e)
        {

        }
    }
}
