namespace DVLD_PresentationLayer.ApplicationForms
{
    partial class DetainedLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetainedLicense));
            this.GbFind = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LlblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnDetain = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.ucAppInfoForDetainLicense1 = new DVLD_PresentationLayer.ApplicationForms.ucAppInfoForDetainLicense();
            this.ucDriverLicenseInfo1 = new DVLD_PresentationLayer.ApplicationForms.ucDriverLicenseInfo();
            this.GbFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbFind
            // 
            this.GbFind.Controls.Add(this.btnFind);
            this.GbFind.Controls.Add(this.txtFind);
            this.GbFind.Controls.Add(this.label1);
            this.GbFind.Location = new System.Drawing.Point(12, 50);
            this.GbFind.Name = "GbFind";
            this.GbFind.Size = new System.Drawing.Size(665, 90);
            this.GbFind.TabIndex = 38;
            this.GbFind.TabStop = false;
            this.GbFind.Text = "Filter";
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFind.BackgroundImage")));
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFind.Location = new System.Drawing.Point(596, 22);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 54);
            this.btnFind.TabIndex = 10;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Location = new System.Drawing.Point(142, 29);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(429, 35);
            this.txtFind.TabIndex = 8;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "LDLicenseID:";
            // 
            // LlblShowLicenseInfo
            // 
            this.LlblShowLicenseInfo.AutoSize = true;
            this.LlblShowLicenseInfo.Enabled = false;
            this.LlblShowLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LlblShowLicenseInfo.Location = new System.Drawing.Point(256, 618);
            this.LlblShowLicenseInfo.Name = "LlblShowLicenseInfo";
            this.LlblShowLicenseInfo.Size = new System.Drawing.Size(172, 25);
            this.LlblShowLicenseInfo.TabIndex = 43;
            this.LlblShowLicenseInfo.TabStop = true;
            this.LlblShowLicenseInfo.Text = "Show License Info";
            this.LlblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblShowLicenseInfo_LinkClicked);
            // 
            // btnDetain
            // 
            this.btnDetain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.Location = new System.Drawing.Point(1000, 605);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(122, 53);
            this.btnDetain.TabIndex = 41;
            this.btnDetain.Text = "Detain";
            this.btnDetain.UseVisualStyleBackColor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(835, 605);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 53);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(7, 618);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(200, 25);
            this.lblShowLicenseHistory.TabIndex = 45;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show License History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(428, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 37);
            this.label2.TabIndex = 40;
            this.label2.Text = "Detain License ";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Location = new System.Drawing.Point(200, 555);
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(154, 26);
            this.txtFineFees.TabIndex = 47;
            // 
            // ucAppInfoForDetainLicense1
            // 
            this.ucAppInfoForDetainLicense1.Location = new System.Drawing.Point(12, 440);
            this.ucAppInfoForDetainLicense1.Name = "ucAppInfoForDetainLicense1";
            this.ucAppInfoForDetainLicense1.Size = new System.Drawing.Size(1114, 159);
            this.ucAppInfoForDetainLicense1.TabIndex = 46;
            this.ucAppInfoForDetainLicense1.Load += new System.EventHandler(this.ucAppInfoForDetainLicense1_Load);
            // 
            // ucDriverLicenseInfo1
            // 
            this.ucDriverLicenseInfo1.Location = new System.Drawing.Point(12, 146);
            this.ucDriverLicenseInfo1.Name = "ucDriverLicenseInfo1";
            this.ucDriverLicenseInfo1.Size = new System.Drawing.Size(1120, 292);
            this.ucDriverLicenseInfo1.TabIndex = 44;
            // 
            // DetainedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 667);
            this.Controls.Add(this.txtFineFees);
            this.Controls.Add(this.ucAppInfoForDetainLicense1);
            this.Controls.Add(this.GbFind);
            this.Controls.Add(this.LlblShowLicenseInfo);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucDriverLicenseInfo1);
            this.Name = "DetainedLicense";
            this.Text = "DetainedLicense";
            this.Load += new System.EventHandler(this.DetainedLicense_Load);
            this.GbFind.ResumeLayout(false);
            this.GbFind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GbFind;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LlblShowLicenseInfo;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private System.Windows.Forms.Label label2;
        private ucDriverLicenseInfo ucDriverLicenseInfo1;
        private ucAppInfoForDetainLicense ucAppInfoForDetainLicense1;
        private System.Windows.Forms.TextBox txtFineFees;
    }
}