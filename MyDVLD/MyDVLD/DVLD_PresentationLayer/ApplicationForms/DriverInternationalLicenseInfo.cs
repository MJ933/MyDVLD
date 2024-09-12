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
    public partial class DriverInternationalLicenseInfo : Form
    {
        public static int LDLicenseID { get; set; }
        public DriverInternationalLicenseInfo()
        {
            if (LDLicenseID != 0)
            {
                ucDriverInternationalLicenseInfo.LDLicenseID = LDLicenseID;
            }
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
