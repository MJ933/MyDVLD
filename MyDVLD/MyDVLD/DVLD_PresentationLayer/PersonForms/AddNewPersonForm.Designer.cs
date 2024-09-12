namespace DVLD_PresentationLayer
{
    partial class AddNewPersonForm
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
            this.ucAddNewPersonCtrl1 = new DVLD_PresentationLayer.ucAddNewPersonCtrl();
            this.SuspendLayout();
            // 
            // ucAddNewPersonCtrl1
            // 
            this.ucAddNewPersonCtrl1.Location = new System.Drawing.Point(12, 12);
            this.ucAddNewPersonCtrl1.Name = "ucAddNewPersonCtrl1";
            this.ucAddNewPersonCtrl1.Size = new System.Drawing.Size(1180, 589);
            this.ucAddNewPersonCtrl1.TabIndex = 0;
            // 
            // AddNewPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 622);
            this.Controls.Add(this.ucAddNewPersonCtrl1);
            this.Name = "AddNewPerson";
            this.Text = "AddNewPerson";
            this.ResumeLayout(false);

        }

        #endregion

        private ucAddNewPersonCtrl ucAddNewPersonCtrl1;
    }
}