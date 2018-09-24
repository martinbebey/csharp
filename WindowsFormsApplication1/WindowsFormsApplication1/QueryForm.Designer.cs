namespace WindowsFormsApplication1
{
    partial class QueryForm
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
            this.queryBoxLabel = new System.Windows.Forms.Label();
            this.queryBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // queryBoxLabel
            // 
            this.queryBoxLabel.AutoSize = true;
            this.queryBoxLabel.Location = new System.Drawing.Point(12, 9);
            this.queryBoxLabel.Name = "queryBoxLabel";
            this.queryBoxLabel.Size = new System.Drawing.Size(73, 13);
            this.queryBoxLabel.TabIndex = 0;
            this.queryBoxLabel.Text = "Insert Query : ";
            // 
            // queryBox
            // 
            this.queryBox.Location = new System.Drawing.Point(91, 12);
            this.queryBox.Name = "queryBox";
            this.queryBox.Size = new System.Drawing.Size(606, 20);
            this.queryBox.TabIndex = 1;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(245, 56);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(279, 23);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 103);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.queryBox);
            this.Controls.Add(this.queryBoxLabel);
            this.Name = "QueryForm";
            this.Text = "QueryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label queryBoxLabel;
        private System.Windows.Forms.TextBox queryBox;
        private System.Windows.Forms.Button submitButton;
    }
}