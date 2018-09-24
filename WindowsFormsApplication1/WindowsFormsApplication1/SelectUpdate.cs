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
    public partial class SelectUpdate : Form
    {
        public SelectUpdate()
        {
            InitializeComponent();
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            UpdateStudent studentUpdateForm = new UpdateStudent();
            studentUpdateForm.Show();
        }

        private void addStaffButton_Click(object sender, EventArgs e)
        {
            StaffUpdate staffUpdateForm = new StaffUpdate();
            staffUpdateForm.Show();
        }

        private void addCourseButton_Click(object sender, EventArgs e)
        {
            CourseUpdate courseUpdateForm = new CourseUpdate();
            courseUpdateForm.Show();
        }

        private void addDepartmentButton_Click(object sender, EventArgs e)
        {
            DepartmentUpdate departmentUpdateForm = new DepartmentUpdate();
            departmentUpdateForm.Show();
            this.Hide();
        }
    }
}
