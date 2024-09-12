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
    public partial class DetainedLicense : Form
    {
        public static int CurrentLicenseID { get; set; }

        public DetainedLicense()
        {
            InitializeComponent();
        }

        private void DetainedLicense_Load(object sender, EventArgs e)
        {

        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            LlblShowLicenseInfo.Enabled = false;
            if (!string.IsNullOrEmpty(txtFind.Text))
            {
                CurrentLicenseID = Convert.ToInt32(txtFind.Text);
                if (CurrentLicenseID != 0)
                {
                    clsDetainedLicensesBL DLicense = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(CurrentLicenseID);
                    clsLicensesBL License = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
                    if (License != null)
                    {
                        //if (!clsLicensesBL.HasActiveLicenseOfClass(License1.DriverID, License1.LicenseClass))
                        //else MessageBox.Show("Your  Already Has an Active License of the Same Class!");
                        if (DLicense != null)
                        {
                            if (!DLicense.IsReleased)
                            {
                                MessageBox.Show("Your License Is Already Detained!");
                                return;
                            }
                        }
                        LoadDrivingLicenseInfo();

                    }
                    else MessageBox.Show($"There is No License With ID = {CurrentLicenseID}");
                }
                else MessageBox.Show($"Please enter a valid License ID! {CurrentLicenseID}");
            }
            else MessageBox.Show($"Please enter a valid License ID! {CurrentLicenseID}");
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            clsLicensesBL Licenses1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(Licenses1.ApplicationID);
            if (Licenses1 != null)
            {
                if (App1 != null)
                {

                    clsDetainedLicensesBL DLicense = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(CurrentLicenseID);

                    clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
                    if (License1.IsActive)
                    {
                        //if (!clsLicensesBL.HasActiveLicenseOfClass(License1.DriverID, License1.LicenseClass))
                        if (DLicense != null)
                        {
                            if (!DLicense.IsReleased)
                            {
                                MessageBox.Show("Your License Is Already Detained!");
                                return;
                            }
                        }
                        DetainLicense();
                        //else MessageBox.Show("Your  Already Has an Active License of the Same Class!");

                    }
                    else MessageBox.Show("Your License is NOT Active!");

                }

            }

        }

        private void LoadDrivingLicenseInfo()
        {
            clsLicensesBL Llicense = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            if (Llicense != null)
            {
                // Update the static LDLAppID
                ucDriverLicenseInfo.AppID = Llicense.ApplicationID;
                // Raise the event to signal LDLAppID change
                ucDriverLicenseInfo.RaiseAppIDChanged();

                //if (Llicense.ExpirationDate <= DateTime.Today)
                //{
                //ucAppInfoForDetainLicense.OldAppID = ucDriverLicenseInfo.AppID;
                ucAppInfoForDetainLicense.OldLicenseID = CurrentLicenseID;
                ucAppInfoForDetainLicense.RaiseOldAppIdChanged();
                LlblShowLicenseInfo.Enabled = true;

                //}
                //else
                //{
                //    MessageBox.Show("The Application Is Not Expired!, You Cannot Replaced The License!");
                //    LlblShowLicenseInfo.Enabled = false;
                //}

            }
            else MessageBox.Show($"There is no License with LDLicenseID {CurrentLicenseID}");
        }


        private void AddnewApp()
        {
            clsLicensesBL Licenses1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(Licenses1.ApplicationID);
            if (Licenses1 != null)
            {
                if (App1 != null)
                {
                    //clsApplicationsBL app2 = new clsApplicationsBL();
                    //app2.PersonID = App1.PersonID;
                    //app2.ApplicationDate = DateTime.Today;
                    //app2.ApplicationTypeID = 2;
                    //app2.ApplicationStatus = 3;
                    //app2.LastStatusDate = DateTime.Today;
                    //app2.ApplicationFees = clsApplicationTypesBL.FindApplicationTypeByID(2).Fees;
                    //app2.CreatedBy = clsGlobalSettings.User.UserID.ToString();
                    //if (app2.SaveApp())
                    //{
                    //DetainLicense();
                    //}
                    //else MessageBox.Show("The Replaced Application has NOT Issued!");
                }
            }
        }

        private void DetainLicense()
        {
            //clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsDetainedLicensesBL DLicense = new clsDetainedLicensesBL();
            DLicense.LicenseID = CurrentLicenseID;
            DLicense.DetainDate = DateTime.Today;
            DLicense.FineFees = Convert.ToInt32(txtFind.Text);
            DLicense.IsReleased = false;
            DLicense.CreatedByUserID = clsGlobalSettings.User.UserID;

            if (DLicense.Save())
            {

                MessageBox.Show("The License Has been Detained Successfully");
                //clsLicensesBL.DeactivateLicense(CurrentLicenseID);
                btnDetain.Enabled = false;
                //GbFind.Enabled = false;
                txtFineFees.Visible = false;
                //ucAppInfoForDetainLicense.OldAppID = CurrentLicenseID;
                ucAppInfoForDetainLicense.OldLicenseID = DLicense.LicenseID;
                ucAppInfoForDetainLicense.RaiseOldAppIdChanged();
                LlblShowLicenseInfo.Enabled = true;

                CurrentLicenseID = DLicense.LicenseID;
            }
            else
                MessageBox.Show("The License Has NOT been Detained!");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            LicenseHistory.LDLAppID = License1.ApplicationID;
            LicenseHistory.LDLicenseID = CurrentLicenseID;
            Form frm = new LicenseHistory();
            frm.ShowDialog();
        }

        private void LlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            DriverLicenseInfo.AppID = License1.ApplicationID;
            //LicenseHistory.LDLicenseID = CurrentLicenseID;
            Form frm = new DriverLicenseInfo();
            frm.ShowDialog();
        }

        private void ucAppInfoForDetainLicense1_Load(object sender, EventArgs e)
        {

        }
    }
}
