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
    public partial class ReplacementForLostLicense : Form
    {
        public static int CurrentLicenseID { get; set; }

        public ReplacementForLostLicense()
        {
            InitializeComponent();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReplacementForLostLicense_Load(object sender, EventArgs e)
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

                    clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
                    if (License1 != null)
                    {
                        if (License1.IsActive)
                        {

                            //if (!clsLicensesBL.HasActiveLicenseOfClass(License1.DriverID, License1.LicenseClass))
                            rbtnChanged();
                            LoadDrivingLicenseInfo();
                            //else MessageBox.Show("Your  Already Has an Active License of the Same Class!");
                        }

                        else MessageBox.Show("Your License is NOT Active!");
                    }
                    else MessageBox.Show($"There is No License With ID = {CurrentLicenseID}");
                }
                else MessageBox.Show($"Please enter a valid License ID! {CurrentLicenseID}");
            }
            else MessageBox.Show($"Please enter a valid License ID! {CurrentLicenseID}");

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
                ucAppInfoForLicenseReplacement.OldAppID = ucDriverLicenseInfo.AppID;
                ucAppInfoForLicenseReplacement.OldLicenseID = CurrentLicenseID;
                ucAppInfoForLicenseReplacement.RaiseOldAppIdChanged();
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

        private void btnIssue_Click(object sender, EventArgs e)
        {

            clsLicensesBL Licenses1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(Licenses1.ApplicationID);
            if (Licenses1 != null)
            {
                if (App1 != null)
                {


                    clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
                    if (License1.IsActive)
                    {
                        //if (!clsLicensesBL.HasActiveLicenseOfClass(License1.DriverID, License1.LicenseClass))
                        AddnewApp();
                        //else MessageBox.Show("Your  Already Has an Active License of the Same Class!");

                    }
                    else MessageBox.Show("Your License is NOT Active!");

                }
            }
        }

        private void AddnewApp()
        {
            clsLicensesBL Licenses1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(Licenses1.ApplicationID);
            if (Licenses1 != null)
            {
                if (App1 != null)
                {
                    clsApplicationsBL app2 = new clsApplicationsBL();
                    app2.PersonID = App1.PersonID;
                    app2.ApplicationDate = DateTime.Today;
                    app2.ApplicationTypeID = 2;
                    app2.ApplicationStatus = 3;
                    app2.LastStatusDate = DateTime.Today;
                    app2.ApplicationFees = clsApplicationTypesBL.FindApplicationTypeByID(2).Fees;
                    app2.CreatedBy = clsGlobalSettings.User.UserID.ToString();
                    if (app2.SaveApp())
                    {
                        AddnewLicense(app2.ApplicationID);
                    }
                    else MessageBox.Show("The Replaced Application has NOT Issued!");
                }
            }
        }

        private void AddnewLicense(int RenewAppID)
        {
            clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);
            clsLicensesBL License2 = new clsLicensesBL();
            License2.ApplicationID = RenewAppID;
            License2.DriverID = License1.DriverID;
            License2.LicenseClass = License1.LicenseClass;
            License2.IssueDate = DateTime.Today;
            License2.ExpirationDate = DateTime.Today.AddYears(10);
            License2.Notes = License1.Notes;
            License2.PaidFees = License1.PaidFees;
            License2.IsActive = true;
            License2.IssueReason = GetReasonID();
            License2.CreatedByUserID = clsGlobalSettings.User.UserID;
            if (License2.Save())
            {

                MessageBox.Show("The License Has been Replaced Successfully");
                clsLicensesBL.DeactivateLicense(CurrentLicenseID);
                btnIssue.Enabled = false;
                ucAppInfoForLicenseReplacement.NewAppID = RenewAppID;
                ucAppInfoForLicenseReplacement.NewLicenseID = License2.LicenseID;
                ucAppInfoForLicenseReplacement.RaiseOldAppIdChanged();
                LlblShowLicenseInfo.Enabled = true;
                CurrentLicenseID = License2.LicenseID;
            }
            else
                MessageBox.Show("The License Has NOT been Replaced!");
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
        private byte GetReasonID()
        {
            if (rbtnDamagedLicense.Checked == true)
                return 3;
            else return 4;

        }

        private void rbtnDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            rbtnChanged();
        }

        private void rbtnLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            rbtnChanged();
        }
        private void rbtnChanged()
        {
            if (rbtnDamagedLicense.Checked == true)
                ucAppInfoForLicenseReplacement.AppTypeID = 3;
            else
                ucAppInfoForLicenseReplacement.AppTypeID = 4;
            clsLicensesBL License1 = clsLicensesBL.FindLicenseByLicenseID(CurrentLicenseID);

            ucAppInfoForLicenseReplacement.NewAppID = License1.ApplicationID;
            ucAppInfoForLicenseReplacement.NewLicenseID = License1.LicenseID;
            ucAppInfoForLicenseReplacement.RaiseOldAppIdChanged();
        }

    }
}
