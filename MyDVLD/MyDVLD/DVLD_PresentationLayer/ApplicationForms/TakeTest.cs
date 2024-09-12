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
    public partial class TakeTest : Form
    {
        public static int ID { get; set; }

        public static int appointmentID { get; set; }

        public static int result;
        public static bool Result;


        public static int CurrentTest { get; set; }

        public TakeTest()
        {
            InitializeComponent();
            ucTakeTestCtrl.ID = ID;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Result = GetResult();
            if (result != 0)
            {
                if (!clsTestsBL.HasTestResult(ID, CurrentTest))
                {
                    clsTestsBL test1 = new clsTestsBL();

                    test1.TestAppointmentID = appointmentID;
                    test1.TestResult = Result;
                    test1.Notes = rtxtNotes.Text;
                    test1.CreatedByUserID = clsGlobalSettings.User.UserID;

                    if (test1.Save())
                    {
                        MessageBox.Show("The Application Saved Successfully!");
                    }
                    else MessageBox.Show("The Application Saved Successfully!");
                    clsAppointmentsBL.LockThisAppointment(appointmentID);
                }
                else MessageBox.Show("You Already Save the test Result!");
            }
            else MessageBox.Show("Please chose the Result!");
            //(!clsTestsBL.HasTestResult(LDLicenseID, CurrentTest))
            //{
            //    clsTestsBL test1 = new clsTestsBL();

            //    test1.TestAppointmentID = appointmentID;
            //    test1.TestResult = GetResult();
            //    test1.Notes = rtxtNotes.Text;
            //    test1.CreatedByUserID = clsGlobalSettings.User.UserID;

            //    truef (test1.Save())
            //    {
            //        MessageBox.falsehow("The Application Saved Successfully!");
            //    }

            //else MessageBox.Show("You Already Save the test Result!");}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakeTest_Load(object sender, EventArgs e)
        {

        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPass.Checked)
                result = 1;
            else if (rbtnFail.Checked)
                result = 2;
            else MessageBox.Show("Please Chose A Valid Result");
        }
        private bool GetResult()
        {
            if (result == 1)
                return true;
            else return false;
        }


    }
}
