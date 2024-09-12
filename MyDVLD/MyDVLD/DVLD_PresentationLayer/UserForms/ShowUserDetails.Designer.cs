namespace DVLD_PresentationLayer
{
    partial class ShowUserDetails
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
            this.ucShowUserDetailsCtrl1 = new DVLD_PresentationLayer.ucShowUserDetailsCtrl();
            this.SuspendLayout();
            // 
            // ucShowUserDetailsCtrl1
            // 
            this.ucShowUserDetailsCtrl1.Location = new System.Drawing.Point(12, 12);
            this.ucShowUserDetailsCtrl1.Name = "ucShowUserDetailsCtrl1";
            this.ucShowUserDetailsCtrl1.Size = new System.Drawing.Size(1018, 556);
            this.ucShowUserDetailsCtrl1.TabIndex = 0;
            // 
            // ShowUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 663);
            this.Controls.Add(this.ucShowUserDetailsCtrl1);
            this.Name = "ShowUserDetails";
            this.Text = "ShowUserInfo";
            this.Load += new System.EventHandler(this.ShowUserDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucShowUserDetailsCtrl ucShowUserDetailsCtrl1;
    }
}