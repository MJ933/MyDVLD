using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class clsTestsBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTestsBL()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = string.Empty;
            this.CreatedByUserID = -1;
            this.Mode = enMode.AddNew;
        }

        private clsTestsBL(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID)
        {
            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;
            this.Mode = enMode.Update;
        }

        public static clsTestsBL FindTestByID(int testID)
        {
            int testAppointmentID = -1;
            bool testResult = false;
            string notes = string.Empty;
            int createdByUserID = -1;

            if (clsTestsDAL.GetTestByID(testID, ref testAppointmentID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTestsBL(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsTestsBL FindTestByAppointmentID(int testAppointmentID)
        {
            int testID = -1;
            bool testResult = false;
            string notes = string.Empty;
            int createdByUserID = -1;

            if (clsTestsDAL.GetTestByAppointmentID(testAppointmentID, ref testID, ref testResult, ref notes, ref createdByUserID))
            {
                return new clsTestsBL(testID, testAppointmentID, testResult, notes, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestsDAL.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            return this.TestID != -1;
        }

        private bool _UpdateTest()
        {
            return clsTestsDAL.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewTest())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateTest();
                default:
                    return false;
            }
        }

        public static DataTable GetAllTests()
        {
            return clsTestsDAL.GetAllTests();
        }

        public static bool DeleteTest(int testID)
        {
            return clsTestsDAL.DeleteTest(testID);
        }

        public static bool HasTestResult(int LDLAppID, int TestTypeID)
        {
            return clsTestsDAL.HasTestResult(LDLAppID, TestTypeID);
        }

        public static bool GetTestResult(int LDLAppID, int TestTypeID)
        {
            return clsTestsDAL.GetTestResult(LDLAppID, TestTypeID);
        }
    }
}