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
    public partial class ucReleaceDetianInfo : UserControl
    {
        public static int OldAppID { get; set; }
        public static int OldLicenseID { get; set; }
        public static int NewAppID { get; set; }
        public static int NewLicenseID { get; set; }
        public static int AppTypeID { get; set; }

        public static int FineFees { get; set; }

        public static event EventHandler OldAppIdChanged;

        public ucReleaceDetianInfo()
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
            clsLicensesBL LDLicense = clsLicensesBL.FindLicenseByLicenseID(OldLicenseID);
            OldAppID = LDLicense.ApplicationID;
            if (LDLicense == null)
            {
                ClearLabelsIfAppIsNull();
                return;
            }
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByApplicationID(OldAppID);
            if (App1 == null)
            {
                ClearLabelsIfAppIsNull();
                return;
            }

            clsDetainedLicensesBL DetainedLicense1 = clsDetainedLicensesBL.FindDetainedLicenseByLicenseID(OldLicenseID);
            ClearLabelsIfAppIsNull();

            if (DetainedLicense1 != null)
            {
                lblDetainedID.Text = DetainedLicense1.DetainID.ToString();
                lblDetainedDate.Text = DateTime.Today.ToString();
                int AppFees = Convert.ToInt16(clsApplicationTypesBL.FindApplicationTypeByID(5).Fees);
                lblAppFees.Text = AppFees.ToString();
                lblTotalFees.Text = (AppFees + DetainedLicense1.FineFees).ToString();
                lblLicenseID.Text = DetainedLicense1.LicenseID.ToString();
                lblFineFees.Text = DetainedLicense1.FineFees.ToString();
                lblAppID.Text = App1.ApplicationID.ToString();


            }
            else
                lblLicenseID.Text = OldLicenseID.ToString();
            lblCreateBy.Text = clsUsersBL.FindUserByPersonID(clsGlobalSettings.User.PersonID).UserName;

            //}

        }
        private void ClearLabelsIfAppIsNull()
        {
            lblDetainedID.Text = "???";
            lblDetainedDate.Text = "???";
            lblAppFees.Text = "???";
            lblTotalFees.Text = "???";
            lblLicenseID.Text = "???";
            lblFineFees.Text = "???";
            lblAppID.Text = "???";
            lblCreateBy.Text = "???";
            lblLicenseID.Text = "???";
        }


        private void ucReleaceDetianInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
