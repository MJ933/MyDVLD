namespace DVLD_PresentationLayer
{
    partial class LoginForm
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
            this.ucLoginFormCtrl1 = new DVLD_PresentationLayer.ucLoginFormCtrl();
            this.SuspendLayout();
            // 
            // ucLoginFormCtrl1
            // 
            this.ucLoginFormCtrl1.Location = new System.Drawing.Point(76, 51);
            this.ucLoginFormCtrl1.Name = "ucLoginFormCtrl1";
            this.ucLoginFormCtrl1.Size = new System.Drawing.Size(618, 306);
            this.ucLoginFormCtrl1.TabIndex = 0;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 479);
            this.Controls.Add(this.ucLoginFormCtrl1);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ucLoginFormCtrl ucLoginFormCtrl1;
    }
}