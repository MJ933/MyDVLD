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
    public partial class ucDriverInternationalLicenseInfo : UserControl
    {
        public static int LDLicenseID { get; set; }
        public ucDriverInternationalLicenseInfo()
        {
            InitializeComponent();
            LoadInfo();

        }

        private void LoadInfo()
        {
            clsInternationalLicensesBL IntLicense = clsInternationalLicensesBL.FindInternationalLicenseByLDLicenseID(LDLicenseID);

            if (IntLicense != null)
            {
                clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(IntLicense.ApplicationID);
                clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(App1.PersonID);
                string FullName = Person1.FirstName + " " + Person1.SecondName + " " + Person1.ThirdName + " " + Person1.LastName;

                lblName.Text = FullName;
                lblIntLicenseID.Text = IntLicense.InternationalLicenseID.ToString();
                lblLicenseID.Text = LDLicenseID.ToString();
                lblNationalNo.Text = Person1.NationalNo;
                if (Person1.Gender == 0)
                    lblGendor.Text = "Male";
                else lblGendor.Text = "Female";
                lblIssueDate.Text = IntLicense.IssueDate.ToString();
                lblAppID.Text = App1.ApplicationID.ToString();
                if (IntLicense.IsActive)
                    lblIsActive.Text = "Yes";
                else lblIsActive.Text = "No";
                lblDateOfBirth.Text = Person1.DateOfBirth.ToString();
                lblDriverID.Text = IntLicense.DriverID.ToString();
                lblExpirationDate.Text = IntLicense.ExpirationDate.ToString();
                pictureBox10.ImageLocation = Person1.ImagePath;
            }
            //else MessageBox.Show("There is No International License!");
        }

    }
}
