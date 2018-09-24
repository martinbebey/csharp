namespace WindowsFormsApplication1
{
    partial class frmgames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmgames));
            this.btnshapes = new System.Windows.Forms.Button();
            this.btnguessnumber = new System.Windows.Forms.Button();
            this.btnchoose = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnshapes
            // 
            this.btnshapes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnshapes.Font = new System.Drawing.Font("Lucida Calligraphy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshapes.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnshapes.Location = new System.Drawing.Point(172, 33);
            this.btnshapes.Name = "btnshapes";
            this.btnshapes.Size = new System.Drawing.Size(109, 61);
            this.btnshapes.TabIndex = 0;
            this.btnshapes.Text = "I\'AM LEARNING SHAPES";
            this.btnshapes.UseVisualStyleBackColor = false;
            this.btnshapes.Click += new System.EventHandler(this.btnshapes_Click);
            // 
            // btnguessnumber
            // 
            this.btnguessnumber.BackColor = System.Drawing.Color.LightYellow;
            this.btnguessnumber.Font = new System.Drawing.Font("Lucida Calligraphy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnguessnumber.ForeColor = System.Drawing.Color.OliveDrab;
            this.btnguessnumber.Location = new System.Drawing.Point(287, 80);
            this.btnguessnumber.Name = "btnguessnumber";
            this.btnguessnumber.Size = new System.Drawing.Size(106, 43);
            this.btnguessnumber.TabIndex = 1;
            this.btnguessnumber.Text = "GUESS NUMBER";
            this.btnguessnumber.UseVisualStyleBackColor = false;
            this.btnguessnumber.Click += new System.EventHandler(this.btnguessnumber_Click);
            // 
            // btnchoose
            // 
            this.btnchoose.BackColor = System.Drawing.Color.LightYellow;
            this.btnchoose.Font = new System.Drawing.Font("Lucida Calligraphy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnchoose.ForeColor = System.Drawing.Color.OliveDrab;
            this.btnchoose.Location = new System.Drawing.Point(48, 78);
            this.btnchoose.Name = "btnchoose";
            this.btnchoose.Size = new System.Drawing.Size(118, 45);
            this.btnchoose.TabIndex = 3;
            this.btnchoose.Text = "CHOOSE OPERATIONS";
            this.btnchoose.UseVisualStyleBackColor = false;
            this.btnchoose.Click += new System.EventHandler(this.btnchoose_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Transparent;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Font = new System.Drawing.Font("Lucida Calligraphy", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(3, 228);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(100, 31);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "BACK";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmgames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(449, 299);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnchoose);
            this.Controls.Add(this.btnguessnumber);
            this.Controls.Add(this.btnshapes);
            this.Name = "frmgames";
            this.Text = "Games";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnshapes;
        private System.Windows.Forms.Button btnguessnumber;
        private System.Windows.Forms.Button btnchoose;
        private System.Windows.Forms.LinkLabel linkLabel1;

    }
}