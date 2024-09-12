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
    public partial class IssueDrivingLicense : Form
    {
        public static int ID { get; set; }

        public IssueDrivingLicense()
        {
            if (ID != 0)
                ucLDLAppInfoCtrl.ID = ID;
            else MessageBox.Show("The id is not found!");
            InitializeComponent();
        }


        private void AddNewLicense(clsApplicationsBL App1)
        {


            //create a new function to update the application status 
            // create a new function to check if you already save the license so the user cannot click save twice



            clsLicensesBL license1 = new clsLicensesBL();
            license1.DriverID = clsDriversBL.FindDriverByPersonID(App1.PersonID).DriverID;
            license1.ApplicationID = App1.ApplicationID;
            license1.LicenseClass = App1.LicenseClassID;
            license1.IssueDate = DateTime.Today;
            license1.ExpirationDate = license1.IssueDate.AddYears(10);
            license1.Notes = richTextBox1.Text;
            license1.PaidFees = App1.ApplicationFees;
            license1.IsActive = true;
            license1.IssueReason = 1;
            license1.CreatedByUserID = clsGlobalSettings.User.UserID;
            if (license1.Save())
            {
                MessageBox.Show("The License is Issued Successfully");
                clsApplicationsBL.UpdateApplicationStatus(App1.ApplicationID, 3);
            }
            else
            {
                MessageBox.Show("The License is NOT Issued Successfully");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByLDLApplicationID(ID);
            int applicationID = App1.ApplicationID;

            if (clsLicensesBL.DoesLicenseExistForApplication(applicationID))
            {
                MessageBox.Show("A license already exists for this application.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                clsDriversBL driver1 = clsDriversBL.FindDriverByPersonID(App1.PersonID);
                if (driver1 == null)
                {
                    driver1 = new clsDriversBL();
                    driver1.PersonID = App1.PersonID;
                    driver1.CreatedByUserID = clsGlobalSettings.User.UserID;
                    if (driver1.Save())
                    {
                        AddNewLicense(App1);
                    }
                    else MessageBox.Show("We had a Problem in adding new driver");
                }

                else
                {
                    AddNewLicense(App1);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
