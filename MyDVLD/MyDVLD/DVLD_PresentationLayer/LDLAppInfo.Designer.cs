namespace DVLD_PresentationLayer
{
    partial class LDLAppInfo
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
            this.ucLDLAppInfoCtrl1 = new DVLD_PresentationLayer.ApplicationForms.ucLDLAppInfoCtrl();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucLDLAppInfoCtrl1
            // 
            this.ucLDLAppInfoCtrl1.Location = new System.Drawing.Point(12, 12);
            this.ucLDLAppInfoCtrl1.Name = "ucLDLAppInfoCtrl1";
            this.ucLDLAppInfoCtrl1.Size = new System.Drawing.Size(1042, 338);
            this.ucLDLAppInfoCtrl1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(932, 368);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 52);
            this.btnClose.TabIndex = 91;
            this.btnClose.Text = "🔒Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LDLAppInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 432);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucLDLAppInfoCtrl1);
            this.Name = "LDLAppInfo";
            this.Text = "LDLAppInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private ApplicationForms.ucLDLAppInfoCtrl ucLDLAppInfoCtrl1;
        private System.Windows.Forms.Button btnClose;
    }
}