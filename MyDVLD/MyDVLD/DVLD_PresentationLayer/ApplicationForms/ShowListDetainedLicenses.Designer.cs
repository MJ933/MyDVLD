namespace DVLD_PresentationLayer.ApplicationForms
{
    partial class ShowListDetainedLicenses
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowListDetainedLicenses));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnAddNewDetainedLicense = new System.Windows.Forms.Button();
            this.lblRecords = new System.Windows.Forms.Label();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnReleaseLicense = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicenseApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicenseHistoryApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(562, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 83;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(429, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(401, 40);
            this.label9.TabIndex = 91;
            this.label9.Text = "List Detained Licenses";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1134, 574);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(122, 52);
            this.btnClose.TabIndex = 90;
            this.btnClose.Text = "🔒Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(140, 587);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(23, 25);
            this.lblResult.TabIndex = 89;
            this.lblResult.Text = "0";
            // 
            // btnAddNewDetainedLicense
            // 
            this.btnAddNewDetainedLicense.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddNewDetainedLicense.BackgroundImage")));
            this.btnAddNewDetainedLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNewDetainedLicense.Location = new System.Drawing.Point(1167, 158);
            this.btnAddNewDetainedLicense.Name = "btnAddNewDetainedLicense";
            this.btnAddNewDetainedLicense.Size = new System.Drawing.Size(88, 78);
            this.btnAddNewDetainedLicense.TabIndex = 84;
            this.btnAddNewDetainedLicense.UseVisualStyleBackColor = true;
            this.btnAddNewDetainedLicense.Click += new System.EventHandler(this.btnAddNewDetainedLicense_Click);
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(28, 587);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(106, 25);
            this.lblRecords.TabIndex = 88;
            this.lblRecords.Text = "# Records:";
            // 
            // cbFilters
            // 
            this.cbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Location = new System.Drawing.Point(142, 201);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(186, 37);
            this.cbFilters.TabIndex = 87;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(28, 204);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(107, 29);
            this.lblFilter.TabIndex = 86;
            this.lblFilter.Text = "Filter By:";
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(333, 202);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(190, 35);
            this.txtFilter.TabIndex = 85;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 244);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1226, 324);
            this.dataGridView1.TabIndex = 82;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // btnReleaseLicense
            // 
            this.btnReleaseLicense.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReleaseLicense.BackgroundImage")));
            this.btnReleaseLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReleaseLicense.Location = new System.Drawing.Point(1063, 160);
            this.btnReleaseLicense.Name = "btnReleaseLicense";
            this.btnReleaseLicense.Size = new System.Drawing.Size(88, 78);
            this.btnReleaseLicense.TabIndex = 92;
            this.btnReleaseLicense.UseVisualStyleBackColor = true;
            this.btnReleaseLicense.Click += new System.EventHandler(this.btnReleaseLicense_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PersonToolStripMenuItem,
            this.LicenseApplicationToolStripMenuItem,
            this.LicenseHistoryApplicationToolStripMenuItem,
            this.toolStripSeparator1,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(324, 195);
            // 
            // PersonToolStripMenuItem
            // 
            this.PersonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PersonToolStripMenuItem.Image")));
            this.PersonToolStripMenuItem.Name = "PersonToolStripMenuItem";
            this.PersonToolStripMenuItem.Size = new System.Drawing.Size(323, 38);
            this.PersonToolStripMenuItem.Text = "Show Person Details";
            this.PersonToolStripMenuItem.Click += new System.EventHandler(this.PersonToolStripMenuItem_Click);
            // 
            // LicenseApplicationToolStripMenuItem
            // 
            this.LicenseApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LicenseApplicationToolStripMenuItem.Image")));
            this.LicenseApplicationToolStripMenuItem.Name = "LicenseApplicationToolStripMenuItem";
            this.LicenseApplicationToolStripMenuItem.Size = new System.Drawing.Size(323, 38);
            this.LicenseApplicationToolStripMenuItem.Text = "Show License Details";
            this.LicenseApplicationToolStripMenuItem.Click += new System.EventHandler(this.LicenseApplicationToolStripMenuItem_Click);
            // 
            // LicenseHistoryApplicationToolStripMenuItem
            // 
            this.LicenseHistoryApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LicenseHistoryApplicationToolStripMenuItem.Image")));
            this.LicenseHistoryApplicationToolStripMenuItem.Name = "LicenseHistoryApplicationToolStripMenuItem";
            this.LicenseHistoryApplicationToolStripMenuItem.Size = new System.Drawing.Size(323, 38);
            this.LicenseHistoryApplicationToolStripMenuItem.Text = "Show Person License History";
            this.LicenseHistoryApplicationToolStripMenuItem.Click += new System.EventHandler(this.LicenseHistoryApplicationToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("releaseDetainedLicenseToolStripMenuItem.Image")));
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(323, 38);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(320, 6);
            // 
            // ShowListDetainedLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 653);
            this.Controls.Add(this.btnReleaseLicense);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnAddNewDetainedLicense);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ShowListDetainedLicenses";
            this.Text = "ListDetainedLicense";
            this.Load += new System.EventHandler(this.ShowListDetainedLicenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnAddNewDetainedLicense;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnReleaseLicense;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem PersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicenseApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicenseHistoryApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}