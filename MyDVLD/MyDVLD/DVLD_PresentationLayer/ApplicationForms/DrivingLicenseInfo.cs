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
    public partial class DrivingLicenseInfo : Form
    {
        public static int ID { get; set; }
        public DrivingLicenseInfo()
        {
            ucDriverLicenseInfo.AppID = ID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
