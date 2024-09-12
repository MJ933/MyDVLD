using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsTestTypeBL
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Fees { get; set; }

        clsTestTypeBL(int ID, string Title, string description, decimal Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = description;
            this.Fees = Fees;
        }

        public static clsTestTypeBL FindTestTypeByID(int ID)
        {
            string Title = string.Empty;
            decimal Fees = -1;
            string Description = string.Empty;

            if (clsTestTypesDAL.GetTestTypeByID(ID, ref Title, ref Description, ref Fees))
            {
                return new clsTestTypeBL(ID, Title, Description, Fees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllData()
        {
            return clsTestTypesDAL.GetAllData();
        }

        private bool _UpdateApplicationType()
        {
            return clsTestTypesDAL.UpdateTestType(this.ID, this.Title, this.Description, this.Fees);
        }

        public bool Save()
        {
            return this._UpdateApplicationType();
        }
    }


}
