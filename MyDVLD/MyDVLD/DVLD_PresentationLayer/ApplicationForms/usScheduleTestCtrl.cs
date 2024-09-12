using DVLD_BusinessLayer;
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
    public partial class usScheduleTestCtrl : UserControl
    {
        public static int ID { get; set; }
        public int CurrentTest { get; set; }
        public usScheduleTestCtrl()
        {
            InitializeComponent();
            CurrentTest = LocalDrivingLicenseApplications.CurrentTest;
        }

        public DateTime SelectedDate
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }
        public string Fees { get { return lblFees.Text; } }


        private void usScheduleTestCtrl_Load(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                GetApplicationInfo();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        void GetApplicationInfo()
        {

            clsApplicationsBL app1 = clsApplicationsBL.FindApplicationByLDLApplicationID(ID);
            lblLDLAppID.Text = ID.ToString();
            lblDClass.Text = clsLicenseClassesBL.FindLicenseClassNameByLicenseClassID(app1.LicenseClassID).ClassName.ToString();
            clsPeopleBL p1 = clsPeopleBL.FindPersonByID(app1.PersonID);
            string fullName = p1.FirstName + " " + p1.SecondName + "" + p1.ThirdName + " " + p1.LastName;
            lblName.Text = fullName;
            lblTrial.Text = clsAppointmentsBL.GetTrails(ID).ToString();
            lblFees.Text = app1.ApplicationFees.ToString();
            if (CurrentTest != 0)
            {
                if (clsTestsBL.HasTestResult(Convert.ToInt16(lblLDLAppID.Text), CurrentTest))
                {
                    if (!clsTestsBL.GetTestResult(Convert.ToInt16(lblLDLAppID.Text), CurrentTest))
                    {
                        RetakeTestSecreen(app1.ApplicationFees);
                        clsApplicationTypesBL.UpdateApplicationTypeIDByLDLAppID(ID, 8);
                    }
                }
            }
            else MessageBox.Show("The Test Type Is Unknown!");
        }
        void RetakeTestSecreen(decimal fees)
        {
            groupBox2.Enabled = true;
            lblRAppFees.Text = (5).ToString();
            lblTotalFees.Text = (Convert.ToInt16(fees + 5)).ToString();
            lblTitle.Text = "Schedule Retake Test";
        }
    }
}
