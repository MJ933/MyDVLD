using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsLicenseClassesBL
    {
        public int LicenseClassID;
        public string ClassName;
        public string ClassDescription;
        public byte MinimumAllowedAge;
        public byte DefaultValidityLength;
        public decimal ClassFees;

        public clsLicenseClassesBL()
        {
            this.LicenseClassID = -1;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
        }

        public clsLicenseClassesBL(int LicenseClassID, string ClassName, string ClassDescription,
             byte MinimumAllowedAge, byte DefaultValidityLength, decimal ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }


        public static clsLicenseClassesBL FinLicenseClassIDByClassName(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassesDAL.GetLicenseClassIDByLicenseClassName(ref LicenseClassID, ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClassesBL(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else return null;
        }

        public static clsLicenseClassesBL FindLicenseClassNameByLicenseClassID(int LicenseClassID)
        {
            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            decimal ClassFees = 0;

            if (clsLicenseClassesDAL.GetLicenseClassNameByLicenseClassID(LicenseClassID, ref ClassName, ref ClassDescription,
                ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClassesBL(LicenseClassID, ClassName, ClassDescription,
                    MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else return null;
        }


        public static DataTable GetAllData()
        {
            return clsLicenseClassesDAL.GetAllData();
        }
    }
}
