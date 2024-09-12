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
    public partial class ucAppInfoForDetainLicense : UserControl
    {
        public static int OldLicenseID { get; set; }


        public static event EventHandler OldAppIdChanged;

        public ucAppInfoForDetainLicense()
        {

            InitializeComponent();
            OldAppIdChanged += OnOldAppIdChanged;
        }
        private void OnOldAppIdChanged(object sender, EventArgs e)
        {
            if (OldLicenseID != 0)
                LoadNewLicense();
        }
        public static void RaiseOldAppIdChanged()
        {
            OldAppIdChanged?.Invoke(null, EventArgs.Empty);
        }

        private void LoadNewLicense()
        {
            //clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(OldAppID);
            //if (App1 == null)
            //{
            //    ClearLabelsIfAppIsNull();
            //    return;
            //}
            //clsLicensesBL LDLicense = clsLicensesBL.FindLicenseByApplicationID(OldAppID);
            //if (LDLicense == null)
            //{
            //    ClearLabelsIfAppIsNull();
            //    return;
            //}
            clsDetainedLicensesBL DetainedLicense1 = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(OldLicenseID);


            //    if (NewAppID != 0)
            //    {
            //        App1 = clsApplicationsBL.FindApplicationByApplicationID(NewAppID);
            //        if (App1 == null)
            //        {
            //            ClearLabelsIfAppIsNull();
            //            return;
            //        }
            //        LDLicense = clsLicensesBL.FindLicenseByApplicationID(NewAppID);
            //        if (LDLicense == null)
            //        {
            //            ClearLabelsIfAppIsNull();
            //            return;
            //        }
            ClearLabelsIfAppIsNull();

            if (DetainedLicense1 != null)
            {
                lblDetainedID.Text = DetainedLicense1.DetainID.ToString();
                txtFineFees.Text = DetainedLicense1.FineFees.ToString();
            }
            lblDetainedDate.Text = DateTime.Today.ToString();
            lblLicenseID.Text = OldLicenseID.ToString();
            lblCreateBy.Text = clsUsersBL.FindUserByPersonID(clsGlobalSettings.User.PersonID).UserName;

            //}

        }
        private void ClearLabelsIfAppIsNull()
        {
            lblDetainedID.Text = "???";
            lblLicenseID.Text = "???";
            lblDetainedDate.Text = "???";
            txtFineFees.Text = "???";
            lblCreateBy.Text = "???";
        }

    }
}
