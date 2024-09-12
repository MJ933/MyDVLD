namespace DVLD_PresentationLayer
{
    partial class ChangeUserPassword
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
            this.ucChangeUserPasswordCtrl1 = new DVLD_PresentationLayer.ucChangeUserPasswordCtrl();
            this.SuspendLayout();
            // 
            // ucChangeUserPasswordCtrl1
            // 
            this.ucChangeUserPasswordCtrl1.Location = new System.Drawing.Point(12, 12);
            this.ucChangeUserPasswordCtrl1.Name = "ucChangeUserPasswordCtrl1";
            this.ucChangeUserPasswordCtrl1.Size = new System.Drawing.Size(1065, 638);
            this.ucChangeUserPasswordCtrl1.TabIndex = 0;
            // 
            // ChangeUserPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 653);
            this.Controls.Add(this.ucChangeUserPasswordCtrl1);
            this.Name = "ChangeUserPassword";
            this.Text = "ChangeUserPassword";
            this.ResumeLayout(false);

        }

        #endregion

        private ucChangeUserPasswordCtrl ucChangeUserPasswordCtrl1;
    }
}