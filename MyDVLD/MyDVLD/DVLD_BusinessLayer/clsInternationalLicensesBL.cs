using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer // Replace with your BusinessLayer namespace
{
    public class clsInternationalLicensesBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInternationalLicensesBL()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.IsActive = false;
            this.CreatedByUserID = -1;
        }

        public clsInternationalLicensesBL(int internationalLicenseID, int applicationID, int driverID,
                                         int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate,
                                         bool isActive, int createdByUserId)
        {
            this.InternationalLicenseID = internationalLicenseID;
            this.ApplicationID = applicationID;
            this.DriverID = driverID;
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserId;
            this.Mode = enMode.Update;
        }

        public static clsInternationalLicensesBL FindInternationalLicenseByInternationalLicenseID(int internationalLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            bool isActive = false;
            int createdByUserId = -1;

            if (clsInternationalLicensesDAL.GetInternationalLicenseByInternationalLicenseID(internationalLicenseID,
                ref applicationID, ref driverID, ref issuedUsingLocalLicenseID, ref issueDate, ref expirationDate,
                ref isActive, ref createdByUserId))
            {
                return new clsInternationalLicensesBL(internationalLicenseID, applicationID, driverID,
                                                     issuedUsingLocalLicenseID, issueDate, expirationDate,
                                                     isActive, createdByUserId);
            }
            else
            {
                return null;
            }
        }
        public static clsInternationalLicensesBL FindInternationalLicenseByLDLicenseID(int issuedUsingLocalLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int internationalLicenseID = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            bool isActive = false;
            int createdByUserId = -1;

            if (clsInternationalLicensesDAL.GetInternationalLicenseByLDLicenseID(ref internationalLicenseID,
                ref applicationID, ref driverID, issuedUsingLocalLicenseID, ref issueDate, ref expirationDate,
                ref isActive, ref createdByUserId))
            {
                return new clsInternationalLicensesBL(internationalLicenseID, applicationID, driverID,
                                                     issuedUsingLocalLicenseID, issueDate, expirationDate,
                                                     isActive, createdByUserId);
            }
            else
            {
                return null;
            }
        }

        public static clsInternationalLicensesBL FindInternationalLicenseByDriverID(int driverID)
        {
            int internationalLicenseID = -1;
            int applicationID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            bool isActive = false;
            int createdByUserId = -1;

            if (clsInternationalLicensesDAL.GetInternationalLicenseByDriverID(driverID,
                                                                               ref internationalLicenseID,
                                                                               ref applicationID,
                                                                               ref issuedUsingLocalLicenseID,
                                                                               ref issueDate,
                                                                               ref expirationDate,
                                                                               ref isActive,
                                                                               ref createdByUserId))
            {
                return new clsInternationalLicensesBL(internationalLicenseID, applicationID, driverID,
                                                     issuedUsingLocalLicenseID, issueDate, expirationDate,
                                                     isActive, createdByUserId);
            }
            else
            {
                return null;
            }
        }

        public static bool DoesInternationalLicenseExistByDriverID(int driverID)
        {
            return clsInternationalLicensesDAL.DoesInternationalLicenseExistByDriverID(driverID);
        }


        public static bool DoesInternationalLicenseExistByLDLicense(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalLicensesDAL.DoesInternationalLicenseExistByLDLicense(IssuedUsingLocalLicenseID);
        }


        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesDAL.AddNewInternationalLicense(this.ApplicationID,
                                                                                            this.DriverID,
                                                                                            this.IssuedUsingLocalLicenseID,
                                                                                            this.IssueDate,
                                                                                            this.ExpirationDate,
                                                                                            this.IsActive,
                                                                                            this.CreatedByUserID);
            return this.InternationalLicenseID != -1;
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicensesDAL.UpdateInternationalLicense(this.InternationalLicenseID,
                                                                         this.ApplicationID,
                                                                         this.DriverID,
                                                                         this.IssuedUsingLocalLicenseID,
                                                                         this.IssueDate,
                                                                         this.ExpirationDate,
                                                                         this.IsActive,
                                                                         this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewInternationalLicense())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateInternationalLicense();
                default:
                    return false;
            }
        }

        public static DataTable GetAllInternationalLicensesData()
        {
            return clsInternationalLicensesDAL.GetAllInternationalLicensesData();
        }

        public static DataTable GetInternationalLicensesDataByLDLicense(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalLicensesDAL.GetInternationalLicensesDataByLDLicense(IssuedUsingLocalLicenseID);
        }

        public static bool DeleteInternationalLicense(int internationalLicenseID)
        {
            return clsInternationalLicensesDAL.DeleteInternationalLicense(internationalLicenseID);
        }
    }
}