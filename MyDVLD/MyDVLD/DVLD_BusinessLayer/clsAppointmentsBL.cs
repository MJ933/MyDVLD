using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class clsAppointmentsBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public clsAppointmentsBL()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.MinValue;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.Mode = enMode.AddNew;
        }

        private clsAppointmentsBL(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate,
            decimal paidFees, int createdByUserID, bool isLocked)
        {
            this.TestAppointmentID = testAppointmentID;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.IsLocked = isLocked;
            this.Mode = enMode.Update;
        }

        public static clsAppointmentsBL FindAppointmentByID(int testAppointmentID)
        {
            int testTypeID = -1;
            int localDrivingLicenseApplicationID = -1;
            DateTime appointmentDate = DateTime.MinValue;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;

            if (clsAppointmentsDAL.GetAppointmentByID(testAppointmentID, ref testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked))
            {
                return new clsAppointmentsBL(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate,
                    paidFees, createdByUserID, isLocked);
            }
            else
            {
                return null;
            }
        }

        public static clsAppointmentsBL FindAppointmentByTestTypeID(int testTypeID)
        {
            int testAppointmentID = -1;
            int localDrivingLicenseApplicationID = -1;
            DateTime appointmentDate = DateTime.MinValue;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;

            if (clsAppointmentsDAL.GetAppointmentByTestTypeID(ref testAppointmentID, testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked))
            {
                return new clsAppointmentsBL(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate,
                    paidFees, createdByUserID, isLocked);
            }
            else
            {
                return null;
            }
        }

        public static clsAppointmentsBL FindAppointmentByLDLAppID(int localDrivingLicenseApplicationID)
        {
            int testAppointmentID = -1;
            int testTypeID = -1;
            DateTime appointmentDate = DateTime.MinValue;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;

            if (clsAppointmentsDAL.GetAppointmentByLDLAppID(ref testAppointmentID, ref testTypeID, localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked))
            {
                return new clsAppointmentsBL(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate,
                    paidFees, createdByUserID, isLocked);
            }
            else
            {
                return null;
            }
        }


        public static bool CheckAppointmentByTestTypeIDAndLDLAppID(int testTypeID, int localDrivingLicenseApplicationID)
        {
            if (clsAppointmentsDAL.CheckAppointmentByTestTypeIDAndLDLAppID(testTypeID, localDrivingLicenseApplicationID))
                return true;
            else
                return false;
        }


        public static bool CheckAppointmentByTestTypeID(int testTypeID)
        {
            return clsAppointmentsDAL.CheckAppointmentByTestTypeID(testTypeID);
        }

        private bool _AddNewAppointment()
        {
            this.TestAppointmentID = clsAppointmentsDAL.AddNewAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked);
            return this.TestAppointmentID != -1;
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentsDAL.UpdateAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewAppointment())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateAppointment();
                default:
                    return false;
            }
        }

        public static DataTable GetAllAppointments(int LDLAppID)
        {
            return clsAppointmentsDAL.GetAllAppointments(LDLAppID);
        }
        public static DataTable GetAllAppointments(int LDLAppID, int TestTypeID)
        {
            return clsAppointmentsDAL.GetAllAppointments(LDLAppID, TestTypeID);
        }

        public static bool DeleteAppointment(int testAppointmentID)
        {
            return clsAppointmentsDAL.DeleteAppointment(testAppointmentID);
        }
        public static int GetTrails(int LDLAppID)
        {
            return clsAppointmentsDAL.GetTrails(LDLAppID);
        }


        public static bool LockThisAppointment(int testAppointmentID)
        {
            return clsAppointmentsDAL.LockThisAppointment(testAppointmentID);
        }

        public static bool IsAppointmentLocked(int testAppointmentID)
        {
            return clsAppointmentsDAL.IsAppointmentLocked(testAppointmentID);
        }
    }
}