using DVLD_BusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class EditApplicationTypeForm : Form
    {
        public static int ID { get; set; }
        private clsApplicationTypesBL applicationType0;

        public EditApplicationTypeForm()
        {
            InitializeComponent();
        }

        private void EditApplicationTypeForm_Load(object sender, EventArgs e)
        {
            GitApplicationType(ID);
        }

        private void GitApplicationType(int ID)
        {
            applicationType0 = clsApplicationTypesBL.FindApplicationTypeByID(ID);
            if (applicationType0 == null)
            {
                MessageBox.Show("Application Type with LDLicenseID " + ID + " not found.");
                return;
            }

            lblApplicationTypeID.Text = ID.ToString();
            txtFees.Text = applicationType0.Fees.ToString();
            txtTitle.Text = applicationType0.Title.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplicationTypesBL ApplicationType1 = applicationType0;
            if (ApplicationType1 != null)
            {
                lblApplicationTypeID.Text = ApplicationType1.ID.ToString();
                ApplicationType1.Title = txtTitle.Text;
                ApplicationType1.Fees = Convert.ToDecimal(txtFees.Text);
                if (ApplicationType1.Save())
                {
                    MessageBox.Show("ApplicationType Has Been Updated Successfully.");
                }
                else MessageBox.Show("ApplicationType Has Not Updated Successfully.");
            }
            else MessageBox.Show("The Application Type is Empty!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}