using DVLD_BusinessLayer;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace DVLD_PresentationLayer.ApplicationForms
{
    public partial class ScheduleVisionTest : Form
    {
        public static bool NotUpdate = true;
        public static int appointmentID { get; set; }
        public static int ID { get; set; }
        public bool isLocked = true;
        public static bool Exist { get; set; }
        public static int TestTypeID { get; set; }
        private bool Result = true;
        public int CurrentTest { get; set; }
        public ScheduleVisionTest()
        {
            CurrentTest = LocalDrivingLicenseApplications.CurrentTest;
            TestTypeID = CurrentTest;
            Exist = false;
            Result = TakeTest.Result;

            InitializeComponent();
            if (CurrentTest == 2)
                pictureBox1.Image = Image.FromFile(@"D:\DVLD_Icons\notes.png");

            else if (CurrentTest == 3)
                pictureBox1.Image = Image.FromFile(@"D:\DVLD_Icons\slippery.png");
            if (ID == 0)
            {
                MessageBox.Show("LDLicenseID is not set correctly.");
            }
            else
            {
                usScheduleTestCtrl.ID = ID;
            }
        }

        private void ScheduleVisionTest_Load(object sender, EventArgs e)
        {
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            isLocked = clsAppointmentsBL.IsAppointmentLocked(appointmentID);
            Exist = clsAppointmentsBL.CheckAppointmentByTestTypeIDAndLDLAppID(TestTypeID, ID);
            if ((NotUpdate && !Exist))
            {
                AddAppointment();
            }
            else if (NotUpdate && Exist)
            {

                if (isLocked)
                {
                    if (!Result)
                        AddAppointment();
                    else MessageBox.Show("You are Already Passed The Test!");
                }
                else MessageBox.Show("Person Already Have an active appointment for this test, You can not add new Appointment!");
            }
            else if (NotUpdate == false && Exist)
            {
                clsAppointmentsBL appointment1 = clsAppointmentsBL.FindAppointmentByID(appointmentID);
                if (!appointment1.IsLocked)
                {
                    UpdateAppointment();
                }
                else
                {
                    MessageBox.Show("The Appointment is Locked!");
                }
            }

        }
        public void AddAppointment()

        {
            clsAppointmentsBL appointment1 = new clsAppointmentsBL();
            appointment1.TestTypeID = TestTypeID;
            appointment1.LocalDrivingLicenseApplicationID = ID;
            appointment1.AppointmentDate = ucScheduleTestCtrl1.SelectedDate;
            appointment1.PaidFees = Convert.ToDecimal(ucScheduleTestCtrl1.Fees);
            appointment1.CreatedByUserID = clsGlobalSettings.User.UserID;
            appointment1.IsLocked = false;
            if (appointment1.Save())
            {
                MessageBox.Show("The Appointment has Save successfully");
                //NotUpdate = false;
            }
            else
            {
                MessageBox.Show("The Appointment has NOT Save successfully");
            }
        }
        public void UpdateAppointment()
        {
            clsAppointmentsBL appointment1 = clsAppointmentsBL.FindAppointmentByID(appointmentID);
            if (appointment1 != null)
            {
                appointment1.TestTypeID = CurrentTest;
                appointment1.LocalDrivingLicenseApplicationID = ID;
                appointment1.AppointmentDate = ucScheduleTestCtrl1.SelectedDate;
                appointment1.PaidFees = Convert.ToDecimal(ucScheduleTestCtrl1.Fees);
                appointment1.CreatedByUserID = clsGlobalSettings.User.UserID;
                //appointment1.IsLocked = false;
                if (appointment1.Save())
                {
                    MessageBox.Show("The Appointment has Updated successfully");
                    NotUpdate = true;
                }
                else
                {
                    MessageBox.Show("The Appointment has NOT Updated successfully");
                }
            }
            else
            {
                MessageBox.Show("The Appointment has NOT Added successfully with Unknown reason!!!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucScheduleTestCtrl1_Load(object sender, EventArgs e)
        {
        }


    }
}