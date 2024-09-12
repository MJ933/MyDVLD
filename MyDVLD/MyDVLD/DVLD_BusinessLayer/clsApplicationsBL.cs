using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class clsApplicationsBL
    {
        public enum enMode
        { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int LicenseClassID { get; set; }
        public decimal ApplicationFees { get; set; }
        public string CreatedBy { get; set; }
        public int PersonID { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }

        public clsApplicationsBL()
        {
            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.MinValue;
            this.LicenseClassID = 0;
            this.ApplicationFees = 0;
            this.CreatedBy = string.Empty;
            this.PersonID = 0;
            this.ApplicationTypeID = 0;
            this.ApplicationStatus = 0;
            this.LastStatusDate = DateTime.MinValue;
        }

        public clsApplicationsBL(int LDLApplicationID, DateTime applicationDate, int licenseClassID, decimal applicationFees,
            string createdBy, int personID, int applicationTypeID, byte applicationStatus, DateTime lastStatusDate)
        {
            this.ApplicationID = LDLApplicationID;
            this.ApplicationDate = applicationDate;
            this.LicenseClassID = licenseClassID;
            this.ApplicationFees = applicationFees;
            this.CreatedBy = createdBy;
            this.PersonID = personID;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.Mode = enMode.Update;
        }
        public static clsApplicationsBL FindLDLApplicationByApplicationID(int LDLApplicationID)
        {
            DateTime ApplicationDate = DateTime.MinValue;
            int LicenseClassID = 0;
            decimal ApplicationFees = 0;
            string CreatedBy = string.Empty;
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;

            if (clsApplicationsDAL.GetLDLApplicationByApplicationID(LDLApplicationID, ref ApplicationDate,
                ref LicenseClassID, ref ApplicationFees, ref CreatedBy, ref ApplicantPersonID, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate))
                return new clsApplicationsBL(LDLApplicationID, ApplicationDate, LicenseClassID,
                    ApplicationFees, CreatedBy, ApplicantPersonID, ApplicationTypeID, ApplicationStatus, LastStatusDate);
            else return null;
        }

        public static clsApplicationsBL FindApplicationByApplicationID(int ApplicationID)
        {
            DateTime ApplicationDate = DateTime.MinValue;
            decimal ApplicationFees = 0;
            string CreatedBy = string.Empty;
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;

            if (clsApplicationsDAL.GetApplicationByApplicationID(ApplicationID, ref ApplicationDate,
                ref ApplicationFees, ref CreatedBy, ref ApplicantPersonID, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate))
                return new clsApplicationsBL
                {
                    ApplicationID = ApplicationID,
                    ApplicationDate = ApplicationDate,
                    ApplicationFees = ApplicationFees,
                    CreatedBy = CreatedBy,
                    PersonID = ApplicantPersonID,
                    ApplicationTypeID = ApplicationTypeID,
                    ApplicationStatus = ApplicationStatus,
                    LastStatusDate = LastStatusDate,
                    Mode = enMode.Update // Set mode to Update since we found an existing application
                };
            else
                return null;
        }

        public static clsApplicationsBL FindLDLAppByPersonID(int PersonID)
        {
            int LDLApplicationID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int LicenseClassID = 0;
            decimal ApplicationFees = 0;
            string CreatedBy = string.Empty;
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;

            if (clsApplicationsDAL.GetLDLApplicationByPersonID(PersonID, ref LDLApplicationID, ref ApplicationDate,
                ref LicenseClassID, ref ApplicationFees, ref CreatedBy, ref ApplicantPersonID, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate))
                return new clsApplicationsBL(LDLApplicationID, ApplicationDate, LicenseClassID, ApplicationFees,
                    CreatedBy, ApplicantPersonID, ApplicationTypeID, ApplicationStatus, LastStatusDate);
            else return null;
        }

        public static clsApplicationsBL FindApplicationByLDLApplicationID(int LDLApplicationID)
        {
            int ApplicationID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int LicenseClassID = 0;
            decimal ApplicationFees = 0;
            string CreatedBy = string.Empty;
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;

            if (clsApplicationsDAL.GetApplicationByLocalDrivingLicenseApplicationID(LDLApplicationID, ref ApplicationID, ref ApplicationDate,
                ref LicenseClassID, ref ApplicationFees, ref CreatedBy, ref ApplicantPersonID, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate))
                return new clsApplicationsBL(ApplicationID, ApplicationDate, LicenseClassID, ApplicationFees,
                    CreatedBy, ApplicantPersonID, ApplicationTypeID, ApplicationStatus, LastStatusDate);
            else return null;
        }

        private bool _AddNewLDLApplication()
        {
            this.ApplicationID = clsApplicationsDAL.AddNewLDLApplication(this.CreatedBy, this.ApplicationDate,
                this.ApplicationFees, this.LicenseClassID, this.PersonID, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate);
            return this.ApplicationID != -1;
        }

        private bool _UpdateLDLApplication()
        {
            return clsApplicationsDAL.UpdateLDLApplication(this.ApplicationID, this.CreatedBy, this.ApplicationDate,
                this.ApplicationFees, this.LicenseClassID, this.PersonID, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate);
        }

        public bool CancelApplicationStatus()
        {
            return clsApplicationsDAL.CancelApplicationStatus(this.ApplicationID, 2, DateTime.Today);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewLDLApplication())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateLDLApplication();
                default:
                    return false;
            }
        }

        public static bool NotHaveActiveApplicationOfSameLicenseClass(int LicenseClassID, int ApplicantPersonID)
        {
            return clsApplicationsDAL.NotHaveActiveApplicationOfSameLicenseClass(LicenseClassID, ApplicantPersonID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDAL.DeleteApplication(ApplicationID);
        }
        public static bool DeleteLocalApplication(int AppID)
        {
            return clsApplicationsDAL.DeleteLDLApplicationByID(AppID);
        }


        public static int GetPassedTests(int AppID)
        {
            return clsApplicationsDAL.GetPassedTests(AppID);
        }
        public static DataTable GetAllData()
        {
            return clsApplicationsDAL.GetAllData();
        }


        public static bool UpdateApplicationStatus(int applicationID, int newStatus)
        {
            return clsApplicationsDAL.UpdateApplicationStatus(applicationID, newStatus);
        }


        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDAL.AddNewApplication(this.CreatedBy, this.ApplicationDate,
                this.ApplicationFees, this.PersonID, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate);
            return this.ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsDAL.UpdateApplication(this.ApplicationID, this.CreatedBy, this.ApplicationDate,
                this.ApplicationFees, this.PersonID, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate);
        }

        public static clsApplicationsBL FindApplicationByApplicationID1(int ApplicationID)
        {
            DateTime ApplicationDate = DateTime.MinValue;
            decimal ApplicationFees = 0;
            string CreatedBy = string.Empty;
            int ApplicantPersonID = 0;
            int ApplicationTypeID = 0;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;

            if (clsApplicationsDAL.GetApplicationByApplicationID(ApplicationID, ref ApplicationDate,
                ref ApplicationFees, ref CreatedBy, ref ApplicantPersonID, ref ApplicationTypeID,
                ref ApplicationStatus, ref LastStatusDate))
                return new clsApplicationsBL
                {
                    ApplicationID = ApplicationID,
                    ApplicationDate = ApplicationDate,
                    ApplicationFees = ApplicationFees,
                    CreatedBy = CreatedBy,
                    PersonID = ApplicantPersonID,
                    ApplicationTypeID = ApplicationTypeID,
                    ApplicationStatus = ApplicationStatus,
                    LastStatusDate = LastStatusDate,
                    Mode = enMode.Update
                };
            else
                return null;
        }

        public static bool DeleteApplication1(int ApplicationID)
        {
            return clsApplicationsDAL.DeleteApplication(ApplicationID);
        }
        public bool SaveApp()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewApplication())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateApplication();
                default:
                    return false;
            }
        }

    }
}