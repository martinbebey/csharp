namespace WindowsFormsApplication1
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.studentSearchButton = new System.Windows.Forms.Button();
            this.searchCourseButton = new System.Windows.Forms.Button();
            this.searchDepartmentButton = new System.Windows.Forms.Button();
            this.searchStaffButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(138, 12);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(343, 20);
            this.searchBox.TabIndex = 0;
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(30, 15);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(102, 13);
            this.searchLabel.TabIndex = 1;
            this.searchLabel.Text = "Enter Search Term: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "OR Select Below";
            // 
            // studentSearchButton
            // 
            this.studentSearchButton.Location = new System.Drawing.Point(138, 74);
            this.studentSearchButton.Name = "studentSearchButton";
            this.studentSearchButton.Size = new System.Drawing.Size(343, 23);
            this.studentSearchButton.TabIndex = 3;
            this.studentSearchButton.Text = "Search Student";
            this.studentSearchButton.UseVisualStyleBackColor = true;
            // 
            // searchCourseButton
            // 
            this.searchCourseButton.Location = new System.Drawing.Point(138, 116);
            this.searchCourseButton.Name = "searchCourseButton";
            this.searchCourseButton.Size = new System.Drawing.Size(343, 23);
            this.searchCourseButton.TabIndex = 4;
            this.searchCourseButton.Text = "Search Course";
            this.searchCourseButton.UseVisualStyleBackColor = true;
            // 
            // searchDepartmentButton
            // 
            this.searchDepartmentButton.Location = new System.Drawing.Point(138, 159);
            this.searchDepartmentButton.Name = "searchDepartmentButton";
            this.searchDepartmentButton.Size = new System.Drawing.Size(343, 23);
            this.searchDepartmentButton.TabIndex = 5;
            this.searchDepartmentButton.Text = "Search Department";
            this.searchDepartmentButton.UseVisualStyleBackColor = true;
            // 
            // searchStaffButton
            // 
            this.searchStaffButton.Location = new System.Drawing.Point(138, 203);
            this.searchStaffButton.Name = "searchStaffButton";
            this.searchStaffButton.Size = new System.Drawing.Size(343, 23);
            this.searchStaffButton.TabIndex = 6;
            this.searchStaffButton.Text = "Search Staff";
            this.searchStaffButton.UseVisualStyleBackColor = true;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 261);
            this.Controls.Add(this.searchStaffButton);
            this.Controls.Add(this.searchDepartmentButton);
            this.Controls.Add(this.searchCourseButton);
            this.Controls.Add(this.studentSearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.searchBox);
            this.Name = "SearchForm";
            this.Text = "SearchForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button studentSearchButton;
        private System.Windows.Forms.Button searchCourseButton;
        private System.Windows.Forms.Button searchDepartmentButton;
        private System.Windows.Forms.Button searchStaffButton;
    }
}