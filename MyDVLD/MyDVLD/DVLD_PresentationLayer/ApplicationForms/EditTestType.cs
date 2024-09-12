using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class EditTestType : Form
    {
        public static int ID { get; set; }
        private clsTestTypeBL TestType0;

        public EditTestType()
        {
            InitializeComponent();
        }

        private void EditTestType_Load(object sender, EventArgs e)
        {
            GitTestTypeInfo(ID);
        }

        private void GitTestTypeInfo(int ID)
        {
            TestType0 = clsTestTypeBL.FindTestTypeByID(ID);
            if (TestType0 == null)
            {
                MessageBox.Show("Application Type with LDLicenseID " + ID + " not found.");
                return;
            }

            lblTestTypeID.Text = ID.ToString();
            txtFees.Text = TestType0.Fees.ToString();
            rtxtDescription.Text = TestType0.Description;
            txtTitle.Text = TestType0.Title.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestTypeBL TestType1 = TestType0;
            if (TestType1 != null)
            {
                lblTestTypeID.Text = TestType1.ID.ToString();
                TestType1.Title = txtTitle.Text;
                TestType1.Description = rtxtDescription.Text;
                TestType1.Fees = Convert.ToDecimal(txtFees.Text);
                if (TestType1.Save())
                {
                    MessageBox.Show("TestType Has Been Updated Successfully.");
                }
                else MessageBox.Show("TestType Has Not Updated Successfully.");
            }
            else MessageBox.Show("The Test Type is Empty!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
