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
    public partial class ucAppNewLicenseInfo : UserControl
    {
        public static int OldAppID { get; set; }
        public static int OldLicenseID { get; set; }
        public static int NewAppID { get; set; }
        public static int NewLicenseID { get; set; }
        public static event EventHandler OldAppIdChanged;
        public ucAppNewLicenseInfo()
        {
            InitializeComponent();
            OldAppIdChanged += OnOldAppIdChanged;
        }

        private void OnOldAppIdChanged(object sender, EventArgs e)
        {
            if (OldAppID != 0)
                LoadRenewAppInfo();
        }
        public static void RaiseOldAppIdChanged()
        {
            OldAppIdChanged?.Invoke(null, EventArgs.Empty);
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void ucAppNewLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void LoadRenewAppInfo()
        {
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(OldAppID);
            if (App1 == null)
            {
                ClearLabelsIfAppIsNull();
                return;
            }
            clsLicensesBL LDLicense = clsLicensesBL.FindLicenseByApplicationID(OldAppID);
            if (LDLicense == null)
            {
                ClearLabelsIfAppIsNull();
                return;
            }

            if (NewAppID != 0)
            {
                App1 = clsApplicationsBL.FindApplicationByApplicationID(NewAppID);
                if (App1 == null)
                {
                    ClearLabelsIfAppIsNull();
                    return;
                }
                LDLicense = clsLicensesBL.FindLicenseByApplicationID(NewAppID);
                if (LDLicense == null)
                {
                    ClearLabelsIfAppIsNull();
                    return;
                }
                lblRLAppID.Text = App1.ApplicationID.ToString();
                lblRenewedLicenseID.Text = LDLicense.LicenseID.ToString();

            }




            lblAppDate.Text = App1.ApplicationDate.ToString();
            lblIssueDate.Text = LDLicense.IssueDate.ToString();
            lblAppFees.Text = App1.ApplicationFees.ToString();
            lblLicenseFees.Text = LDLicense.PaidFees.ToString();
            rtxtNotes.Text = LDLicense.Notes.ToString();
            lblOldLicenseID.Text = OldLicenseID.ToString();
            lblExpirationDate.Text = LDLicense.ExpirationDate.ToString();
            lblCreateBy.Text = clsUsersBL.FindUserByPersonID(clsGlobalSettings.User.PersonID).UserName;
            lblTotalFees.Text = (LDLicense.PaidFees + App1.ApplicationFees).ToString();

        }
        private void ClearLabelsIfAppIsNull()
        {
            lblRLAppID.Text = "???";
            lblRenewedLicenseID.Text = "???";
            lblAppDate.Text = "???";
            lblIssueDate.Text = "???";
            lblAppFees.Text = "???";
            lblLicenseFees.Text = "???";
            rtxtNotes.Text = "???";
            lblOldLicenseID.Text = "???";
            lblExpirationDate.Text = "???";
            lblCreateBy.Text = "???";
            lblTotalFees.Text = "???";
        }
    }
}
