using DVLD_BusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class ucDriverLicenseInfo : UserControl
    {
        // Inside ucDriverLicenseInfo.cs
        public static int AppID { get; set; }

        public static event EventHandler AppIDChanged; // Declare the event here

        public ucDriverLicenseInfo()
        {
            InitializeComponent();
            // Subscribe to the AppIDChanged event in the constructor
            AppIDChanged += OnAppIDChanged; // Correctly subscribe here
        }

        protected void OnAppIDChanged(object sender, EventArgs e)
        {
            if (AppID != 0)
                loadInfo();
        }

        // Method to raise the AppIDChanged event
        public static void RaiseAppIDChanged()
        {
            AppIDChanged?.Invoke(null, EventArgs.Empty);
        }

        // ... rest of your code ...

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ucDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            if (AppID != 0)
                loadInfo();
        }

        //public void loadInfo()
        //{
        //    clsLicensesBL license1 = clsLicensesBL.FindLicenseByApplicationID(LDLAppID);
        //    clsApplicationsBL app1 = clsApplicationsBL.FindLDLApplicationByApplicationID(LDLAppID);
        //    clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(app1.PersonID);
        //    string FullName = Person1.FirstName + " " + Person1.SecondName + " " + Person1.ThirdName + " " + Person1.LastName;

        //    if (license1 != null)
        //    {
        //        lblClass.Text = clsLicenseClassesBL.FindLicenseClassNameByLicenseClassID(license1.LicenseClass).ClassName;
        //        lblName.Text = FullName;
        //        lblLicenseID.Text = license1.LicenseID.ToString();
        //        lblNationalNo.Text = Person1.NationalNo;
        //        if (Person1.Gender == 0)
        //            lblGendor.Text = "Male";
        //        else lblGendor.Text = "Female";
        //        lblIssueDate.Text = license1.IssueDate.ToString();
        //        lblIssueReason.Text = license1.IssueReason.ToString();
        //        lblNotes.Text = license1.Notes;
        //        if (license1.IsActive)
        //            lblIsActive.Text = "Yes";
        //        else lblIsActive.Text = "No";
        //        lblDateOfBirth.Text = Person1.DateOfBirth.ToString();
        //        lblDriverID.Text = license1.DriverID.ToString();
        //        lblExpirationDate.Text = license1.ExpirationDate.ToString();
        //        if (clsDetainedLicensesBL.DoesDetainedLicenseExistByLicenseID(license1.LicenseID))
        //            lblIsDetained.Text = "Yes";
        //        else lblIsDetained.Text = "No";
        //    }
        //}


        // Inside ucDriverLicenseInfo.cs - loadInfo() method
        public void loadInfo()
        {
            clsLicensesBL license1 = clsLicensesBL.FindLicenseByApplicationID(AppID);
            if (license1 != null)
            {
                clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByApplicationID(AppID);
                if (app1 != null)
                {
                    clsPeopleBL Person1 = clsPeopleBL.FindPersonByID(app1.PersonID);
                    if (Person1 != null)
                    {
                        string FullName = Person1.FirstName + " " + Person1.SecondName + " " + Person1.ThirdName + " " + Person1.LastName;

                        lblClass.Text = clsLicenseClassesBL.FindLicenseClassNameByLicenseClassID(license1.LicenseClass).ClassName;
                        lblName.Text = FullName;
                        lblLicenseID.Text = license1.LicenseID.ToString();
                        lblNationalNo.Text = Person1.NationalNo;
                        if (Person1.Gender == 0)
                            lblGendor.Text = "Male";
                        else lblGendor.Text = "Female";
                        lblIssueDate.Text = license1.IssueDate.ToString();
                        lblIssueReason.Text = clsLicensesBL.GetIssueReason(license1.IssueReason);
                        lblNotes.Text = license1.Notes;
                        if (license1.IsActive)
                            lblIsActive.Text = "Yes";
                        else lblIsActive.Text = "No";
                        lblDateOfBirth.Text = Person1.DateOfBirth.ToString();
                        lblDriverID.Text = license1.DriverID.ToString();
                        lblExpirationDate.Text = license1.ExpirationDate.ToString();
                        
                        if (clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(license1.LicenseID).IsReleased)
                            lblIsDetained.Text = "No";
                        else lblIsDetained.Text = "Yes";
                    }
                    else
                    {
                        // Handle the case where Person1 is null (e.g., show an error message)
                        MessageBox.Show("Person not found for this application.");
                    }
                }
                else
                {
                    // Handle the case where app1 is null
                    MessageBox.Show("Application not found.");
                }
            }
            else
            {
                // Handle the case where license1 is null
                MessageBox.Show("License not found for this application LDLicenseID.");
            }
        }
    }
}