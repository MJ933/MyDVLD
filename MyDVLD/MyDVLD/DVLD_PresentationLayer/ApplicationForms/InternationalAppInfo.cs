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
    public partial class InternationalAppInfo : Form
    {
        public static int LDLicenseID { get; set; }
        public InternationalAppInfo()
        {
            ucInternationalAppInfo.LicenseID = LDLicenseID;
            InitializeComponent();
        }

        private void InternationalAppInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
