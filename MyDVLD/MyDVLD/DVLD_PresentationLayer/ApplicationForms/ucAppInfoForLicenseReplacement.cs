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
    public partial class ucAppInfoForLicenseReplacement : UserControl
    {

        public static int OldAppID { get; set; }
        public static int OldLicenseID { get; set; }
        public static int NewAppID { get; set; }
        public static int NewLicenseID { get; set; }

        public static int AppTypeID { get; set; }

        public static event EventHandler OldAppIdChanged;
        public ucAppInfoForLicenseReplacement()
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
        private void ucAppInfoForLicenseReplacement_Load(object sender, EventArgs e)
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
                lblLRAppID.Text = App1.ApplicationID.ToString();
                lblReplacementLicenseID.Text = LDLicense.LicenseID.ToString();
            }
            lblAppDate.Text = App1.ApplicationDate.ToString();
            lblAppFees.Text = App1.ApplicationFees.ToString();
            lblAppFees.Text = clsApplicationTypesBL.FindApplicationTypeByID(AppTypeID).Fees.ToString();
            lblOldLicenseID.Text = OldLicenseID.ToString();
            lblCreateBy.Text = clsUsersBL.FindUserByPersonID(clsGlobalSettings.User.PersonID).UserName;

        }
        private void ClearLabelsIfAppIsNull()
        {
            lblLRAppID.Text = "???";
            lblReplacementLicenseID.Text = "???";
            lblAppDate.Text = "???";
            lblAppFees.Text = "???";
            lblOldLicenseID.Text = "???";
            lblCreateBy.Text = "???";
        }
    }
}
