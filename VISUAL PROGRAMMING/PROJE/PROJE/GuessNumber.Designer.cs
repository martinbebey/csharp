namespace WindowsFormsApplication1
{
    partial class frmguessnumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmguessnumber));
            this.txtguess = new System.Windows.Forms.TextBox();
            this.lblkeep = new System.Windows.Forms.Label();
            this.lblenter = new System.Windows.Forms.Label();
            this.btnguess = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblguess = new System.Windows.Forms.Label();
            this.btnwon = new System.Windows.Forms.Button();
            this.lblanswer = new System.Windows.Forms.Label();
            this.lblstart = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtguess
            // 
            this.txtguess.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtguess.Location = new System.Drawing.Point(69, 205);
            this.txtguess.Multiline = true;
            this.txtguess.Name = "txtguess";
            this.txtguess.Size = new System.Drawing.Size(73, 33);
            this.txtguess.TabIndex = 0;
            // 
            // lblkeep
            // 
            this.lblkeep.AutoSize = true;
            this.lblkeep.BackColor = System.Drawing.Color.Transparent;
            this.lblkeep.Font = new System.Drawing.Font("Lucida Calligraphy", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblkeep.Location = new System.Drawing.Point(282, 149);
            this.lblkeep.Name = "lblkeep";
            this.lblkeep.Size = new System.Drawing.Size(185, 34);
            this.lblkeep.TabIndex = 1;
            this.lblkeep.Text = "I KEEP A NUMBER :)\r\nLET\'S GUESS!!!\r\n";
            this.lblkeep.Visible = false;
            // 
            // lblenter
            // 
            this.lblenter.AutoSize = true;
            this.lblenter.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblenter.Location = new System.Drawing.Point(133, 124);
            this.lblenter.Name = "lblenter";
            this.lblenter.Size = new System.Drawing.Size(0, 21);
            this.lblenter.TabIndex = 2;
            // 
            // btnguess
            // 
            this.btnguess.BackColor = System.Drawing.Color.Tomato;
            this.btnguess.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnguess.Location = new System.Drawing.Point(170, 286);
            this.btnguess.Name = "btnguess";
            this.btnguess.Size = new System.Drawing.Size(85, 34);
            this.btnguess.TabIndex = 3;
            this.btnguess.Text = "GUESS";
            this.btnguess.UseVisualStyleBackColor = false;
            this.btnguess.Click += new System.EventHandler(this.btnguess_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(167, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 21);
            this.label2.TabIndex = 4;
            // 
            // lblguess
            // 
            this.lblguess.AutoSize = true;
            this.lblguess.BackColor = System.Drawing.Color.Transparent;
            this.lblguess.Font = new System.Drawing.Font("Lucida Calligraphy", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblguess.Location = new System.Drawing.Point(54, 110);
            this.lblguess.Name = "lblguess";
            this.lblguess.Size = new System.Drawing.Size(0, 21);
            this.lblguess.TabIndex = 5;
            // 
            // btnwon
            // 
            this.btnwon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnwon.BackgroundImage")));
            this.btnwon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnwon.Location = new System.Drawing.Point(-7, -8);
            this.btnwon.Name = "btnwon";
            this.btnwon.Size = new System.Drawing.Size(533, 353);
            this.btnwon.TabIndex = 6;
            this.btnwon.UseVisualStyleBackColor = true;
            this.btnwon.Visible = false;
            this.btnwon.Click += new System.EventHandler(this.btnwon_Click);
            // 
            // lblanswer
            // 
            this.lblanswer.AutoSize = true;
            this.lblanswer.BackColor = System.Drawing.Color.Transparent;
            this.lblanswer.Font = new System.Drawing.Font("Lucida Calligraphy", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblanswer.Location = new System.Drawing.Point(0, 174);
            this.lblanswer.Name = "lblanswer";
            this.lblanswer.Size = new System.Drawing.Size(207, 16);
            this.lblanswer.TabIndex = 7;
            this.lblanswer.Text = "WRITE YOUR ANSWER =)";
            this.lblanswer.Visible = false;
            // 
            // lblstart
            // 
            this.lblstart.AutoSize = true;
            this.lblstart.BackColor = System.Drawing.Color.Transparent;
            this.lblstart.Font = new System.Drawing.Font("Lucida Calligraphy", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstart.LinkColor = System.Drawing.Color.SeaShell;
            this.lblstart.Location = new System.Drawing.Point(-2, 67);
            this.lblstart.Name = "lblstart";
            this.lblstart.Size = new System.Drawing.Size(97, 27);
            this.lblstart.TabIndex = 8;
            this.lblstart.TabStop = true;
            this.lblstart.Text = "START";
            this.lblstart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.mavi_back;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 34);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmguessnumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(523, 345);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblstart);
            this.Controls.Add(this.lblanswer);
            this.Controls.Add(this.btnwon);
            this.Controls.Add(this.lblguess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnguess);
            this.Controls.Add(this.lblenter);
            this.Controls.Add(this.lblkeep);
            this.Controls.Add(this.txtguess);
            this.Name = "frmguessnumber";
            this.Text = "GuessNumber";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtguess;
        private System.Windows.Forms.Label lblkeep;
        private System.Windows.Forms.Label lblenter;
        private System.Windows.Forms.Button btnguess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblguess;
        private System.Windows.Forms.Button btnwon;
        private System.Windows.Forms.Label lblanswer;
        private System.Windows.Forms.LinkLabel lblstart;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}