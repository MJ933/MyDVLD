namespace DVLD_PresentationLayer.ApplicationForms
{
    partial class NewLocalDrivingLicenseApplication
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
            this.ucNewLocalDrivingLicenseApplicationCtrl1 = new DVLD_PresentationLayer.ApplicationForms.ucNewLocalDrivingLicenseApplicationCtrl();
            this.SuspendLayout();
            // 
            // ucNewLocalDrivingLicenseApplicationCtrl1
            // 
            this.ucNewLocalDrivingLicenseApplicationCtrl1.Location = new System.Drawing.Point(13, 14);
            this.ucNewLocalDrivingLicenseApplicationCtrl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucNewLocalDrivingLicenseApplicationCtrl1.Name = "ucNewLocalDrivingLicenseApplicationCtrl1";
            this.ucNewLocalDrivingLicenseApplicationCtrl1.Size = new System.Drawing.Size(1079, 654);
            this.ucNewLocalDrivingLicenseApplicationCtrl1.TabIndex = 0;
            this.ucNewLocalDrivingLicenseApplicationCtrl1.Load += new System.EventHandler(this.ucNewLocalDrivingLicenseApplicationCtrl1_Load);
            // 
            // NewLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 668);
            this.Controls.Add(this.ucNewLocalDrivingLicenseApplicationCtrl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NewLocalDrivingLicenseApplication";
            this.Text = "NewLocalDrivingLicenseApplication";
            this.ResumeLayout(false);

        }

        #endregion

        private ucNewLocalDrivingLicenseApplicationCtrl ucNewLocalDrivingLicenseApplicationCtrl1;
    }
}