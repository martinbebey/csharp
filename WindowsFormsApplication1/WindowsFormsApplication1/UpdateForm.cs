using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            SelectUpdate selectUpdate = new SelectUpdate();
            selectUpdate.Show();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            SelectUpdate selectUpdate = new SelectUpdate();
            selectUpdate.Show();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            SelectUpdate selectUpdate = new SelectUpdate();
            selectUpdate.Show();
        }
    }
}
