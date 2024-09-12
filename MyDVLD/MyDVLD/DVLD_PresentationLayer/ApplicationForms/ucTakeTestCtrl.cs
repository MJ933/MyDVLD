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
    public partial class ucTakeTestCtrl : UserControl
    {
        public static int ID { get; set; }
        public ucTakeTestCtrl()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        void GetApplicationInfo()
        {
            if (ID != 0)
            {
                clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByLDLApplicationID(ID);
                lblLDLAppID.Text = ID.ToString();
                lblDClass.Text = clsLicenseClassesBL.FindLicenseClassNameByLicenseClassID(app1.LicenseClassID).ClassName.ToString();
                clsPeopleBL p1 = clsPeopleBL.FindPersonByID(app1.PersonID);
                string fullName = p1.FirstName + " " + p1.SecondName + "" + p1.ThirdName + " " + p1.LastName;
                lblName.Text = fullName;
                lblTrial.Text = clsAppointmentsBL.GetTrails(ID).ToString();
                lblDate.Text = app1.ApplicationDate.ToString();
                lblFees.Text = app1.ApplicationFees.ToString();
            }
        }

        private void ucTakeTestCtrl_Load(object sender, EventArgs e)
        {
            GetApplicationInfo();
        }
    }
}
