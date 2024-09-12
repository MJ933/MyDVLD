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
    public partial class ucLDLAppInfoCtrl : UserControl
    {
        public static int ID { get; set; }
        public ucLDLAppInfoCtrl()
        {
            InitializeComponent();
        }

        string GetClassName(int Id)
        {
            return clsLicenseClassesBL.FindLicenseClassNameByLicenseClassID(Id).ClassName;
        }

        void GetApplicationInfo()
        {
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByLDLApplicationID(ID);
            lblLDLAppID.Text = ID.ToString();
            lblAppliedForLicense.Text = GetClassName(App1.LicenseClassID);
            lblPassedTests.Text = clsApplicationsBL.GetPassedTests(ID).ToString() + "/3";

            lblID.Text = App1.ApplicationID.ToString();
            lblStatus.Text = App1.ApplicationStatus.ToString();
            lblFees.Text = App1.ApplicationFees.ToString();
            lblType.Text = clsApplicationTypesBL.FindApplicationTypeByID(App1.ApplicationTypeID).Title;
            clsPeopleBL person1 = clsPeopleBL.FindPersonByID(App1.PersonID);
            string FullName = person1.FirstName + " " + person1.SecondName + " " + person1.ThirdName + " " + person1.LastName;
            lblApplicant.Text = FullName;
            lblDate.Text = App1.ApplicationDate.ToString();
            lblStatusDate.Text = App1.LastStatusDate.ToString();
            lblCreatedBy.Text = App1.CreatedBy.ToString();

        }


        private void ucApplicationInfoCtrl_Load(object sender, EventArgs e)
        {
            if (ID != 0)
                GetApplicationInfo();
        }

        private void LlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsApplicationsBL App1 = clsApplicationsBL.FindApplicationByLDLApplicationID(ID);

            ShowPersonDetailsForm.ID = App1.PersonID;
            Form frm = new ShowPersonDetailsForm();
            frm.ShowDialog();
        }
    }
}
