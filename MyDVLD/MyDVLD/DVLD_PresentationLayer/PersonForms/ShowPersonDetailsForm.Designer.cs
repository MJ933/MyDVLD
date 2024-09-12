namespace DVLD_PresentationLayer
{
    partial class ShowPersonDetailsForm
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
            this.ucShowPersonDetailsCtrl1 = new DVLD_PresentationLayer.ucShowPersonDetailsCtrl();
            this.SuspendLayout();
            // 
            // ucShowPersonDetailsCtrl1
            // 
            this.ucShowPersonDetailsCtrl1.Location = new System.Drawing.Point(13, 12);
            this.ucShowPersonDetailsCtrl1.Name = "ucShowPersonDetailsCtrl1";
            this.ucShowPersonDetailsCtrl1.Size = new System.Drawing.Size(1129, 513);
            this.ucShowPersonDetailsCtrl1.TabIndex = 0;
            // 
            // ShowPersonDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 453);
            this.Controls.Add(this.ucShowPersonDetailsCtrl1);
            this.Name = "ShowPersonDetailsForm";
            this.Text = "ShowPersonInfoForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ucShowPersonDetailsCtrl ucShowPersonDetailsCtrl1;
    }
}