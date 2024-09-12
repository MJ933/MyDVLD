using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class NewInternationalDrivingLicense : Form
    {
        public static int ID { get; set; }

        public NewInternationalDrivingLicense()
        {
            ID = 0;
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LlblShowLicenseInfo.Enabled = false;

            if (!string.IsNullOrEmpty(txtFind.Text))
            {
                ID = Convert.ToInt32(txtFind.Text);
                if (ID != 0)
                {
                    LoadDrivingLicenseInfo();
                }
                else MessageBox.Show($"Please enter a valid License LDLicenseID! {ID}");
            }
            else MessageBox.Show($"Please enter a valid License LDLicenseID! {ID}");
        }

        private void LoadDrivingLicenseInfo()
        {
            clsLicensesBL Llicense = clsLicensesBL.FindLicenseByLicenseID(ID);
            if (Llicense != null)
            {
                // Update the static LDLAppID
                ucDriverLicenseInfo.AppID = Llicense.ApplicationID;
                // Raise the event to signal LDLAppID change
                ucDriverLicenseInfo.RaiseAppIDChanged();
                if (clsInternationalLicensesBL.DoesInternationalLicenseExistByLDLicense(ID))
                {
                    //ucInternationalAppInfo.DriverID = Llicense.DriverID;
                    ucInternationalAppInfo.LicenseID = ID;
                    ucInternationalAppInfo.RaiseDriverIDChanged();
                    LlblShowLicenseInfo.Enabled = true;
                }
                else LlblShowLicenseInfo.Enabled = false;
            }
            else MessageBox.Show($"There is no License with LDLicenseID {ID}");
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicensesBL LDLicense = clsLicensesBL.FindLicenseByLicenseID(ID);

            //clsLicensesBL Llicense = clsLicensesBL.FindLicenseByLicenseID(LDLicenseID);
            clsInternationalLicensesBL ILicense = clsInternationalLicensesBL.FindInternationalLicenseByDriverID(LDLicense.DriverID);
            if (ILicense != null)
            {
                MessageBox.Show($"Person Already has an Active International License With LDLicenseID = {ILicense.InternationalLicenseID} and Local Driving License with LDLicenseID = {ID}");
                return;
            }
            else
            {
                AddNewInernationalApp();
            }
        }

        private void AddNewInernationalApp()
        {
            clsLicensesBL LDLicense = clsLicensesBL.FindLicenseByLicenseID(ID);
            clsApplicationsBL LDLAppID = clsApplicationsBL.FindApplicationByApplicationID(LDLicense.ApplicationID);

            if (LDLicense.IsActive)
            {
                if (LDLicense.LicenseClass >= 3)
                {
                    if (LDLicense.ExpirationDate >= DateTime.Today)
                    {
                        clsApplicationsBL App1 = new clsApplicationsBL();
                        App1.PersonID = LDLAppID.PersonID;
                        App1.ApplicationDate = DateTime.Today;
                        App1.ApplicationTypeID = 6;
                        App1.ApplicationStatus = 3;
                        App1.LastStatusDate = DateTime.Today;
                        App1.ApplicationFees = clsApplicationTypesBL.FindApplicationTypeByID(6).Fees;
                        App1.CreatedBy = clsGlobalSettings.User.UserID.ToString();
                        if (App1.SaveApp())
                        {
                            //NotAdded = false;
                            //lblDLApplicationID.Text = Application1.ApplicationID.ToString();
                            //MessageBox.Show("Application Has Been Added Successfully.");
                            AddNewInternationalLicense(App1);

                        }
                        else MessageBox.Show("Application Has Not Added Successfully.");
                    }
                    else MessageBox.Show("Your License is Expired,you Do not have a Valid License!");

                }
                else MessageBox.Show("Your License Class Is Below Class 3, Your License should be At least 3rd Class or above!");

            }
            else MessageBox.Show("Your License is NOT Active!");
        }
        private void AddNewInternationalLicense(clsApplicationsBL App1)
        {
            clsInternationalLicensesBL IlLicense = new clsInternationalLicensesBL();
            clsLicensesBL Llicense1 = clsLicensesBL.FindLicenseByLicenseID(ID);
            IlLicense.ApplicationID = App1.ApplicationID;
            IlLicense.DriverID = Llicense1.DriverID;
            IlLicense.IssuedUsingLocalLicenseID = Llicense1.LicenseID;
            IlLicense.IssueDate = DateTime.Now;
            IlLicense.ExpirationDate = DateTime.Now.AddYears(1);
            IlLicense.IsActive = true;
            IlLicense.CreatedByUserID = clsGlobalSettings.User.UserID;

            if (IlLicense.Save())
            {
                MessageBox.Show("International License has been Added Successfully -)");

                //ucInternationalAppInfo.DriverID = IlLicense.DriverID;
                ucInternationalAppInfo.LicenseID = ID;
                ucInternationalAppInfo.RaiseDriverIDChanged();
                LlblShowLicenseInfo.Enabled = true;
            }
            else MessageBox.Show("International License has  NOT been  Added -(");


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ID != 0)
            {
                DriverInternationalLicenseInfo.LDLicenseID = ID;
                Form frm = new DriverInternationalLicenseInfo();
                frm.ShowDialog();
            }
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicensesBL License = clsLicensesBL.FindLicenseByLicenseID(ID);
            LicenseHistory.LDLAppID = License.ApplicationID;
            LicenseHistory.LDLicenseID = ID;

            Form frm = new LicenseHistory();
            frm.ShowDialog();
        }
    }
}