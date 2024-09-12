namespace DVLD_PresentationLayer.ApplicationForms
{
    partial class InternationalAppInfo
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
            this.ucInternationalAppInfo1 = new DVLD_PresentationLayer.ApplicationForms.ucInternationalAppInfo();
            this.SuspendLayout();
            // 
            // ucInternationalAppInfo1
            // 
            this.ucInternationalAppInfo1.Location = new System.Drawing.Point(12, 1);
            this.ucInternationalAppInfo1.Name = "ucInternationalAppInfo1";
            this.ucInternationalAppInfo1.Size = new System.Drawing.Size(1120, 194);
            this.ucInternationalAppInfo1.TabIndex = 0;
            // 
            // InternationalAppInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 199);
            this.Controls.Add(this.ucInternationalAppInfo1);
            this.Name = "InternationalAppInfo";
            this.Text = "InternationalAppInfo";
            this.Load += new System.EventHandler(this.InternationalAppInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucInternationalAppInfo ucInternationalAppInfo1;
    }
}