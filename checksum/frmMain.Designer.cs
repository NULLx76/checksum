namespace checksum
{
    partial class frmMain
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
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.lblChecksum1 = new System.Windows.Forms.Label();
            this.lblChecksum2 = new System.Windows.Forms.Label();
            this.tbChecksum1 = new System.Windows.Forms.TextBox();
            this.tbChecksum2 = new System.Windows.Forms.TextBox();
            this.pbCheck = new System.Windows.Forms.PictureBox();
            this.btnFile1 = new System.Windows.Forms.Button();
            this.btnFile2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Location = new System.Drawing.Point(64, 12);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(223, 21);
            this.cmbMethod.TabIndex = 0;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(12, 15);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(46, 13);
            this.lblMethod.TabIndex = 1;
            this.lblMethod.Text = "Method:";
            // 
            // lblChecksum1
            // 
            this.lblChecksum1.AutoSize = true;
            this.lblChecksum1.Location = new System.Drawing.Point(12, 51);
            this.lblChecksum1.Name = "lblChecksum1";
            this.lblChecksum1.Size = new System.Drawing.Size(69, 13);
            this.lblChecksum1.TabIndex = 2;
            this.lblChecksum1.Text = "Checksum 1:";
            // 
            // lblChecksum2
            // 
            this.lblChecksum2.AutoSize = true;
            this.lblChecksum2.Location = new System.Drawing.Point(12, 77);
            this.lblChecksum2.Name = "lblChecksum2";
            this.lblChecksum2.Size = new System.Drawing.Size(69, 13);
            this.lblChecksum2.TabIndex = 3;
            this.lblChecksum2.Text = "Checksum 2:";
            // 
            // tbChecksum1
            // 
            this.tbChecksum1.Location = new System.Drawing.Point(87, 48);
            this.tbChecksum1.Name = "tbChecksum1";
            this.tbChecksum1.Size = new System.Drawing.Size(200, 20);
            this.tbChecksum1.TabIndex = 4;
            // 
            // tbChecksum2
            // 
            this.tbChecksum2.Location = new System.Drawing.Point(87, 74);
            this.tbChecksum2.Name = "tbChecksum2";
            this.tbChecksum2.Size = new System.Drawing.Size(200, 20);
            this.tbChecksum2.TabIndex = 5;
            // 
            // pbCheck
            // 
            this.pbCheck.Location = new System.Drawing.Point(292, 76);
            this.pbCheck.Name = "pbCheck";
            this.pbCheck.Size = new System.Drawing.Size(16, 16);
            this.pbCheck.TabIndex = 6;
            this.pbCheck.TabStop = false;
            // 
            // btnFile1
            // 
            this.btnFile1.Location = new System.Drawing.Point(314, 43);
            this.btnFile1.Name = "btnFile1";
            this.btnFile1.Size = new System.Drawing.Size(72, 23);
            this.btnFile1.TabIndex = 8;
            this.btnFile1.Text = "From File...";
            this.btnFile1.UseVisualStyleBackColor = true;
            // 
            // btnFile2
            // 
            this.btnFile2.Location = new System.Drawing.Point(314, 72);
            this.btnFile2.Name = "btnFile2";
            this.btnFile2.Size = new System.Drawing.Size(72, 23);
            this.btnFile2.TabIndex = 9;
            this.btnFile2.Text = "From File...";
            this.btnFile2.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 107);
            this.Controls.Add(this.btnFile2);
            this.Controls.Add(this.btnFile1);
            this.Controls.Add(this.pbCheck);
            this.Controls.Add(this.tbChecksum2);
            this.Controls.Add(this.tbChecksum1);
            this.Controls.Add(this.lblChecksum2);
            this.Controls.Add(this.lblChecksum1);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.cmbMethod);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Checksum";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label lblChecksum1;
        private System.Windows.Forms.Label lblChecksum2;
        private System.Windows.Forms.TextBox tbChecksum1;
        private System.Windows.Forms.TextBox tbChecksum2;
        private System.Windows.Forms.PictureBox pbCheck;
        private System.Windows.Forms.Button btnFile1;
        private System.Windows.Forms.Button btnFile2;
    }
}

