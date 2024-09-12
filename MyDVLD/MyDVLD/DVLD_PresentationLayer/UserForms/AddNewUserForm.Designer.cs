namespace DVLD_PresentationLayer
{
    partial class AddNewUserForm
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
            this.ucAddNewUserFormCtrl1 = new DVLD_PresentationLayer.ucAddNewUserFormCtrl();
            this.SuspendLayout();
            // 
            // ucAddNewUserFormCtrl1
            // 
            this.ucAddNewUserFormCtrl1.Location = new System.Drawing.Point(1, 1);
            this.ucAddNewUserFormCtrl1.Name = "ucAddNewUserFormCtrl1";
            this.ucAddNewUserFormCtrl1.Size = new System.Drawing.Size(1083, 657);
            this.ucAddNewUserFormCtrl1.TabIndex = 0;
            // 
            // AddNewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 687);
            this.Controls.Add(this.ucAddNewUserFormCtrl1);
            this.Name = "AddNewUserForm";
            this.Text = "AddNewUserForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ucAddNewUserFormCtrl ucAddNewUserFormCtrl1;
    }
}