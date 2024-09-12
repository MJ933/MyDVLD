using DVLD_PresentationLayer.ApplicationForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class DriverLicenseInfo : Form
    {
        public static int AppID { get; set; }
        public DriverLicenseInfo()
        {
            ucDriverLicenseInfo.AppID = AppID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
