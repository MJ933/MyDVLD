using DVLD_DataAccessLayer; // Replace with your DataAccessLayer namespace
using System;
using System.Data;

namespace DVLD_BusinessLayer // Replace with your BusinessLayer namespace
{
    public class clsLicensesBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        // You might have dependencies on other BL classes, similar to clsPeopleBL in the example
        // private clsDriversBL _driver; 

        public clsLicensesBL()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;

            // Initialize dependencies if needed
            // this._driver = new clsDriversBL();
        }

        public clsLicensesBL(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate,
                             DateTime expirationDate, string notes, decimal paidFees, bool isActive, byte issueReason,
                             int createdByUserId)
        {
            this.LicenseID = licenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.LicenseClass = licenseClass;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;
            this.IssueReason = issueReason;
            this.CreatedByUserID = createdByUserId;
            this.Mode = enMode.Update;
        }

        public static clsLicensesBL FindLicenseByLicenseID(int licenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserId = -1;

            if (clsLicensesDAL.GetLicenseByLicenseID(licenseID, ref applicationID, ref driverID, ref licenseClass,
                                                    ref issueDate, ref expirationDate, ref notes, ref paidFees,
                                                    ref isActive, ref issueReason, ref createdByUserId))
            {
                return new clsLicensesBL(licenseID, applicationID, driverID, licenseClass, issueDate, expirationDate,
                                         notes, paidFees, isActive, issueReason, createdByUserId);
            }
            else
            {
                return null;
            }
        }

        // Add other methods to find licenses by different criteria (e.g., DriverID, ApplicationID)
        public static clsLicensesBL FindLicenseByApplicationID(int applicationID)
        {
            int licenseID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            decimal paidFees = 0;
            bool isActive = false;
            byte issueReason = 0;
            int createdByUserId = -1;

            if (clsLicensesDAL.GetLicenseByApplicationID(applicationID, ref licenseID, ref driverID, ref licenseClass,
                                                    ref issueDate, ref expirationDate, ref notes, ref paidFees,
                                                    ref isActive, ref issueReason, ref createdByUserId))
            {
                return new clsLicensesBL(licenseID, applicationID, driverID, licenseClass, issueDate, expirationDate,
                                         notes, paidFees, isActive, issueReason, createdByUserId);
            }
            else
            {
                return null;
            }
        }

        public static bool DoesLicenseExistForApplication(int applicationID)
        {
            // You'll need a method in clsLicensesDAL to check for existing licenses by ApplicationID
            return clsLicensesDAL.DoesLicenseExistForApplication(applicationID);
        }

        private bool _AddNewLicense()
        {
            // Add validation logic if needed before adding
            int DriverId = -1;
            this.LicenseID = clsLicensesDAL.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
                                                         this.IssueDate, this.ExpirationDate, this.Notes,
                                                         this.PaidFees, this.IsActive, this.IssueReason,
                                                         this.CreatedByUserID);
            return this.LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            // Add validation logic if needed before updating 

            return clsLicensesDAL.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                                                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                                                this.IsActive, this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewLicense())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateLicense();
                default:
                    return false;
            }
        }

        public static DataTable GetAllLicensesData()
        {
            return clsLicensesDAL.GetAllLicensesData();
        }

        public static bool DeleteLicense(int licenseID)
        {
            return clsLicensesDAL.DeleteLicense(licenseID);
        }

        public static DataTable GetLocalLicenseHistoryByPersonID(int applicantPersonID)
        {
            return clsLicensesDAL.GetLocalLicenseHistoryByPersonID(applicantPersonID);
        }

        public static bool DeactivateLicense(int licenseID)
        {
            return clsLicensesDAL.DeactivateLicense(licenseID);
        }

        public static string GetIssueReason(int Reason)
        {
            if (Reason == 1)
                return "First Time";
            else if (Reason == 2)
                return "ReNew";
            else if (Reason == 3)
                return "Replacement for Damaged";
            else if (Reason == 4)
                return "Replacement for Lost";
            else return "Unknown";
        }

        public static bool HasActiveLicenseOfClass(int driverID, int licenseClass)
        {
            return clsLicensesDAL.HasActiveLicenseOfClass(driverID, licenseClass);
        }
    }
}