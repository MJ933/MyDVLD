using DVLD_DataAccessLayer; // Replace with your DataAccessLayer namespace
using System;
using System.Data;

namespace DVLD_BusinessLayer // Replace with your BusinessLayer namespace
{
    public class clsDetainedLicensesBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainedLicensesBL()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.MinValue;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        public clsDetainedLicensesBL(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserId,
                                     bool isReleased, DateTime releaseDate, int releasedByUserId, int releaseApplicationID)
        {
            this.DetainID = detainID;
            this.LicenseID = licenseID;
            this.DetainDate = detainDate;
            this.FineFees = fineFees;
            this.CreatedByUserID = createdByUserId;
            this.IsReleased = isReleased;
            this.ReleaseDate = releaseDate;
            this.ReleasedByUserID = releasedByUserId;
            this.ReleaseApplicationID = releaseApplicationID;
            this.Mode = enMode.Update;
        }

        public static clsDetainedLicensesBL FindDetainedLicenseByDetainID(int detainID)
        {
            int licenseID = -1;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserId = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserId = -1;
            int releaseApplicationID = -1;

            if (clsDetainedLicensesDAL.GetDetainedLicenseByDetainID(detainID, ref licenseID, ref detainDate, ref fineFees,
                                                       ref createdByUserId, ref isReleased, ref releaseDate,
                                                       ref releasedByUserId, ref releaseApplicationID))
            {
                return new clsDetainedLicensesBL(detainID, licenseID, detainDate, fineFees, createdByUserId, isReleased,
                                                 releaseDate, releasedByUserId, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicensesBL FindDetainedLicenseByLicenseID(int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.MinValue;
            decimal fineFees = 0;
            int createdByUserId = -1;
            bool isReleased = false;
            DateTime releaseDate = DateTime.MinValue;
            int releasedByUserId = -1;
            int releaseApplicationID = -1;

            if (clsDetainedLicensesDAL.GetDetainedLicenseByLicenseID(licenseID, ref detainID, ref detainDate, ref fineFees,
                                                       ref createdByUserId, ref isReleased, ref releaseDate,
                                                       ref releasedByUserId, ref releaseApplicationID))
            {
                return new clsDetainedLicensesBL(detainID, licenseID, detainDate, fineFees, createdByUserId, isReleased,
                                                 releaseDate, releasedByUserId, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static bool DoesDetainedLicenseExistByLicenseID(int licenseID)
        {
            return clsDetainedLicensesDAL.DoesDetainedLicenseExistByLicenseID(licenseID);
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDAL.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return this.DetainID != -1;
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesDAL.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID,
                                                this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewDetainedLicense())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateDetainedLicense();
                default:
                    return false;
            }
        }

        public static DataTable GetAllDetainedLicensesData()
        {
            return clsDetainedLicensesDAL.GetAllDetainedLicensesData();
        }

        public static bool DeleteDetainedLicense(int detainID)
        {
            return clsDetainedLicensesDAL.DeleteDetainedLicense(detainID);
        }

        public static bool ReleaseDetainedLicense(int detainID, DateTime releaseDate, int releasedByUserId, int releaseApplicationID)
        {
            return clsDetainedLicensesDAL.ReleaseDetainedLicense(detainID, releaseDate, releasedByUserId, releaseApplicationID);
        }
    }
}