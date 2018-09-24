namespace WindowsFormsApplication1
{
    partial class frmcategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcategory));
            this.label1 = new System.Windows.Forms.Label();
            this.grbcategory = new System.Windows.Forms.GroupBox();
            this.rbcube = new System.Windows.Forms.RadioButton();
            this.rbgames = new System.Windows.Forms.RadioButton();
            this.rboperations = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grbcategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(61, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please choose a category!!!!";
            // 
            // grbcategory
            // 
            this.grbcategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grbcategory.Controls.Add(this.rbcube);
            this.grbcategory.Controls.Add(this.rbgames);
            this.grbcategory.Controls.Add(this.rboperations);
            this.grbcategory.Font = new System.Drawing.Font("Lucida Calligraphy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbcategory.Location = new System.Drawing.Point(205, 43);
            this.grbcategory.Name = "grbcategory";
            this.grbcategory.Size = new System.Drawing.Size(142, 184);
            this.grbcategory.TabIndex = 2;
            this.grbcategory.TabStop = false;
            this.grbcategory.Text = "CATEGORIES";
            // 
            // rbcube
            // 
            this.rbcube.AutoSize = true;
            this.rbcube.Font = new System.Drawing.Font("Lucida Calligraphy", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbcube.Location = new System.Drawing.Point(2, 141);
            this.rbcube.Name = "rbcube";
            this.rbcube.Size = new System.Drawing.Size(146, 36);
            this.rbcube.TabIndex = 2;
            this.rbcube.Text = "INTELLIGENCE \r\nCUBE";
            this.rbcube.UseVisualStyleBackColor = true;
            this.rbcube.CheckedChanged += new System.EventHandler(this.rbcube_CheckedChanged);
            // 
            // rbgames
            // 
            this.rbgames.AutoSize = true;
            this.rbgames.Font = new System.Drawing.Font("Lucida Calligraphy", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbgames.Location = new System.Drawing.Point(2, 91);
            this.rbgames.Name = "rbgames";
            this.rbgames.Size = new System.Drawing.Size(82, 20);
            this.rbgames.TabIndex = 1;
            this.rbgames.Text = "GAMES";
            this.rbgames.UseVisualStyleBackColor = true;
            this.rbgames.CheckedChanged += new System.EventHandler(this.rbgames_CheckedChanged);
            // 
            // rboperations
            // 
            this.rboperations.AutoSize = true;
            this.rboperations.Font = new System.Drawing.Font("Lucida Calligraphy", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rboperations.Location = new System.Drawing.Point(0, 36);
            this.rboperations.Name = "rboperations";
            this.rboperations.Size = new System.Drawing.Size(147, 34);
            this.rboperations.TabIndex = 0;
            this.rboperations.Text = "ARITHMETICAL \r\nOPERATIONS";
            this.rboperations.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rboperations.UseVisualStyleBackColor = true;
            this.rboperations.CheckedChanged += new System.EventHandler(this.rboperations_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.gri_back;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(327, 245);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmcategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(386, 276);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grbcategory);
            this.Controls.Add(this.label1);
            this.Name = "frmcategory";
            this.Text = "Category";
            this.grbcategory.ResumeLayout(false);
            this.grbcategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbcategory;
        private System.Windows.Forms.RadioButton rbcube;
        private System.Windows.Forms.RadioButton rbgames;
        private System.Windows.Forms.RadioButton rboperations;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}