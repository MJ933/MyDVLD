namespace DVLD_PresentationLayer
{
    partial class UpdateUserInfo
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
            this.ucUpdateUserCtrl1 = new DVLD_PresentationLayer.ucUpdateUserCtrl();
            this.SuspendLayout();
            // 
            // ucUpdateUserCtrl1
            // 
            this.ucUpdateUserCtrl1.Location = new System.Drawing.Point(1, -2);
            this.ucUpdateUserCtrl1.Name = "ucUpdateUserCtrl1";
            this.ucUpdateUserCtrl1.Size = new System.Drawing.Size(1057, 660);
            this.ucUpdateUserCtrl1.TabIndex = 0;
            // 
            // UpdateUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 657);
            this.Controls.Add(this.ucUpdateUserCtrl1);
            this.Name = "UpdateUserInfo";
            this.Text = "UpdateUserInfo";
            this.Load += new System.EventHandler(this.UpdateUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucUpdateUserCtrl ucUpdateUserCtrl1;
    }
}