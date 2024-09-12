using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsApplicationTypesBL
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Fees { get; set; }

        clsApplicationTypesBL(int ID, string Title, decimal Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static clsApplicationTypesBL FindApplicationTypeByID(int ID)
        {
            string Title = string.Empty;
            decimal Fees = -1;

            // Assuming you have a method in clsUsersDAL to get user details by ID
            if (clsApplicationTypesDAL.GetApplicationTypeByID(ID, ref Title, ref Fees))
            {
                return new clsApplicationTypesBL(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllData()
        {
            return clsApplicationTypesDAL.GetAllData();
        }

        public static DataTable GetAllApplicationTypesData()
        {
            return clsApplicationTypesDAL.GetAllApplicationTypesData();
        }
        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesDAL.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }
        public static bool UpdateApplicationTypeIDByLDLAppID(int LocalDrivingLicenseApplicationID, int ApplicationTypeID)
        {
            return clsApplicationTypesDAL.UpdateApplicationTypeIDByLDLAppID(LocalDrivingLicenseApplicationID, ApplicationTypeID);
        }
        public bool Save()
        {
            return this._UpdateApplicationType();
        }


    }


}
