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
    public partial class ucInternationalAppInfo : UserControl
    {
        public static int DriverID { get; set; }
        public static int LicenseID { get; set; }

        public static event EventHandler DriverIDChanged;
        public ucInternationalAppInfo()
        {

            InitializeComponent();
            DriverIDChanged += OnDriverIDChanged;
            if (LicenseID != 0)
                LoadAppInfo();
        }


        private void OnDriverIDChanged(object sender, EventArgs e)
        {
            if (LicenseID != 0)
                LoadAppInfo();
        }

        public static void RaiseDriverIDChanged()
        {
            DriverIDChanged?.Invoke(null, EventArgs.Empty);
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ucInternationalAppInfo_Load(object sender, EventArgs e)
        {

        }

        private void LoadAppInfo()
        {
            clsInternationalLicensesBL ILicense = clsInternationalLicensesBL.FindInternationalLicenseByLDLicenseID(LicenseID);
            //clsDriversBL driver1 = clsDriversBL.FindDriverByDriverID(DriverID);

            if (ILicense != null)
            {
                clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByApplicationID(ILicense.ApplicationID);
                lblILAppID.Text = ILicense.ApplicationID.ToString();
                lblAppDate.Text = app1.ApplicationDate.ToString();
                lblIssueDate.Text = ILicense.IssueDate.ToString();
                lblFees.Text = (app1.ApplicationFees).ToString();
                lblILLicenseID.Text = ILicense.InternationalLicenseID.ToString();
                lblLocalLicenseID.Text = LicenseID.ToString();
                lblExpirationDate.Text = ILicense.ExpirationDate.ToString();
                lblCreateBy.Text = ILicense.CreatedByUserID.ToString();
            }
            else
            {
                ClearField();
            }
        }

        private void ClearField()
        {
            lblILAppID.Text = string.Empty;
            lblAppDate.Text = string.Empty;
            lblIssueDate.Text = string.Empty;
            lblFees.Text = string.Empty;
            lblILLicenseID.Text = string.Empty;
            lblLocalLicenseID.Text = string.Empty;
            lblExpirationDate.Text = string.Empty;
            lblCreateBy.Text = string.Empty;
        }
    }
}
