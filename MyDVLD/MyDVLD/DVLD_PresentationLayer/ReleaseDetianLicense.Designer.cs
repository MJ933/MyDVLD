namespace DVLD_PresentationLayer
{
    partial class ReleaseDetianLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseDetianLicense));
            this.GbFind = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucDriverLicenseInfo1 = new DVLD_PresentationLayer.ApplicationForms.ucDriverLicenseInfo();
            this.LlblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.ucReleaceDetianInfo1 = new DVLD_PresentationLayer.ApplicationForms.ucReleaceDetianInfo();
            this.GbFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbFind
            // 
            this.GbFind.Controls.Add(this.btnFind);
            this.GbFind.Controls.Add(this.txtFind);
            this.GbFind.Controls.Add(this.label1);
            this.GbFind.Location = new System.Drawing.Point(3, 36);
            this.GbFind.Name = "GbFind";
            this.GbFind.Size = new System.Drawing.Size(665, 90);
            this.GbFind.TabIndex = 48;
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
            // ucDriverLicenseInfo1
            // 
            this.ucDriverLicenseInfo1.Location = new System.Drawing.Point(3, 117);
            this.ucDriverLicenseInfo1.Name = "ucDriverLicenseInfo1";
            this.ucDriverLicenseInfo1.Size = new System.Drawing.Size(1120, 292);
            this.ucDriverLicenseInfo1.TabIndex = 53;
            // 
            // LlblShowLicenseInfo
            // 
            this.LlblShowLicenseInfo.AutoSize = true;
            this.LlblShowLicenseInfo.Enabled = false;
            this.LlblShowLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LlblShowLicenseInfo.Location = new System.Drawing.Point(247, 609);
            this.LlblShowLicenseInfo.Name = "LlblShowLicenseInfo";
            this.LlblShowLicenseInfo.Size = new System.Drawing.Size(172, 25);
            this.LlblShowLicenseInfo.TabIndex = 52;
            this.LlblShowLicenseInfo.TabStop = true;
            this.LlblShowLicenseInfo.Text = "Show License Info";
            this.LlblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LlblShowLicenseInfo_LinkClicked);
            // 
            // btnRelease
            // 
            this.btnRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.Location = new System.Drawing.Point(991, 596);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(122, 53);
            this.btnRelease.TabIndex = 50;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(826, 596);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 53);
            this.btnClose.TabIndex = 51;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(-2, 609);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(200, 25);
            this.lblShowLicenseHistory.TabIndex = 54;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show License History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(373, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(388, 37);
            this.label2.TabIndex = 49;
            this.label2.Text = "Release Detainrd License ";
            // 
            // ucReleaceDetianInfo1
            // 
            this.ucReleaceDetianInfo1.Location = new System.Drawing.Point(3, 408);
            this.ucReleaceDetianInfo1.Name = "ucReleaceDetianInfo1";
            this.ucReleaceDetianInfo1.Size = new System.Drawing.Size(1117, 182);
            this.ucReleaceDetianInfo1.TabIndex = 55;
            // 
            // ReleaseDetianLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 663);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucReleaceDetianInfo1);
            this.Controls.Add(this.GbFind);
            this.Controls.Add(this.ucDriverLicenseInfo1);
            this.Controls.Add(this.LlblShowLicenseInfo);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Name = "ReleaseDetianLicense";
            this.Text = "ReleaseDetianLicense";
            this.Load += new System.EventHandler(this.ReleaseDetianLicense_Load);
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
        private ApplicationForms.ucDriverLicenseInfo ucDriverLicenseInfo1;
        private System.Windows.Forms.LinkLabel LlblShowLicenseInfo;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private System.Windows.Forms.Label label2;
        private ApplicationForms.ucReleaceDetianInfo ucReleaceDetianInfo1;
    }
}