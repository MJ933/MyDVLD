// ignore spelling: dvld

using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class clsCountriesBL
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        private clsCountriesBL(int ID, string CountryName)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
        }


        public static clsCountriesBL FindByID(int ID)
        {
            string countryName = "";
            if (clsCountriesDAL.GetCountryByID(ID, ref countryName))
            {
                return new clsCountriesBL(ID, countryName);
            }
            else return null;

        }
        public static clsCountriesBL FindByName(string countryName)
        {
            int ID = -1;
            if (clsCountriesDAL.GetCountryByName(ref ID, countryName))
            {
                return new clsCountriesBL(ID, countryName);
            }
            else return null;

        }

        public static DataTable GetCountriesList()
        {
            return clsCountriesDAL.GetCountriesList();
        }


    }
}
