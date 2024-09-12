using DVLD_DataAccessLayer; // Replace with your DataAccessLayer namespace
using System;
using System.Data;

namespace DVLD_BusinessLayer // Replace with your BusinessLayer namespace
{
    public class clsDriversBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        // You might have dependencies on other BL classes (e.g., clsPeopleBL)
        // private clsPeopleBL _person;

        public clsDriversBL()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.MinValue;
            // Initialize dependencies if needed
            // this._person = new clsPeopleBL();
        }

        public clsDriversBL(int driverID, int personID, int createdByUserId, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserId;
            this.CreatedDate = createdDate;
            this.Mode = enMode.Update;
        }

        public static clsDriversBL FindDriverByDriverID(int driverID)
        {
            int personID = -1;
            int createdByUserId = -1;
            DateTime createdDate = DateTime.MinValue;

            if (clsDriversDAL.GetDriverByDriverID(driverID, ref personID, ref createdByUserId, ref createdDate))
            {
                return new clsDriversBL(driverID, personID, createdByUserId, createdDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriversBL FindDriverByPersonID(int personID)
        {
            int driverID = -1;
            int createdByUserId = -1;
            DateTime createdDate = DateTime.MinValue;

            if (clsDriversDAL.GetDriverByPersonID(personID, ref driverID, ref createdByUserId, ref createdDate))
            {
                return new clsDriversBL(driverID, personID, createdByUserId, createdDate);
            }
            else
            {
                return null;
            }
        }
        // Add other methods to find drivers by different criteria (e.g., PersonID)

        private bool _AddNewDriver()
        {
            this.DriverID = clsDriversDAL.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return this.DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            return clsDriversDAL.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewDriver())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateDriver();
                default:
                    return false;
            }
        }

        public static DataTable GetAllDriversData()
        {
            return clsDriversDAL.GetAllDriversData();
        }

        public static bool DeleteDriver(int driverID)
        {
            return clsDriversDAL.DeleteDriver(driverID);
        }
    }
}