namespace DVLD_PresentationLayer
{
    partial class UpdatePerosn
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
            this.ucUpdatePersonCtrl1 = new DVLD_PresentationLayer.ucUpdatePersonCtrl();
            this.SuspendLayout();
            // 
            // ucUpdatePersonCtrl1
            // 
            this.ucUpdatePersonCtrl1.Location = new System.Drawing.Point(12, 12);
            this.ucUpdatePersonCtrl1.Name = "ucUpdatePersonCtrl1";
            this.ucUpdatePersonCtrl1.Size = new System.Drawing.Size(1085, 604);
            this.ucUpdatePersonCtrl1.TabIndex = 0;
            // 
            // UpdatePerosn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 640);
            this.Controls.Add(this.ucUpdatePersonCtrl1);
            this.Name = "UpdatePerosn";
            this.Text = "UpdatePerosn";
            this.Load += new System.EventHandler(this.UpdatePerosn_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucUpdatePersonCtrl ucUpdatePersonCtrl1;
    }
}