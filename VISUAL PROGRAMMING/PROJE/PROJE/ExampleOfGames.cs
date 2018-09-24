using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmexampleofgames : Form
    {
        public frmexampleofgames()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "TRIANGLE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\triangle.gif");
            else if (comboBox1.Text == "SQUARE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\square.gif");
            else if (comboBox1.Text == "RECTANGULAR") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\rectangle.png");
            else if (comboBox1.Text == "CIRCLE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\circle.gif");
            else if (comboBox1.Text == "PENTAGON") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\pentagon.jpg");
            else if (comboBox1.Text == "HEXAGON") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\hexagon.jpg");
            else if (comboBox1.Text == "CUBE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\cube.jpg");
            else if (comboBox1.Text == "CYLINDER") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\cylinder.gif");
            else if (comboBox1.Text == "PYRAMID") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\pyramid.jpg");
            else if (comboBox1.Text == "TETRAHEDRAL") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\tetrahedral.jpg");
            else if (comboBox1.Text == "STAR") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\star.jpg");
            else if (comboBox1.Text == "ELLIPSE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\ellipse.jpg");
            else if (comboBox1.Text == "CONE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\cone.png");
            else if (comboBox1.Text == "SPHERE") pictureBox1.Image = Image.FromFile("C:\\Game Picture\\sphere.jpg");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
         
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            frmgames frm = new frmgames();
            frm.Show();
        }
    }
}
