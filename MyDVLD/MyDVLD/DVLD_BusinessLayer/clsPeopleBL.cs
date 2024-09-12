using DVLD_DataAccessLayer;
using System;
using System.Data;
using System.Net;
using System.Security.Policy;

namespace DVLD_BusinessLayer
{
    public class clsPeopleBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsPeopleBL()
        {
            this.ID = -1;
            this.NationalNo = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.MinValue;
            this.Gender = 0;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityCountryID = -1;
            this.ImagePath = string.Empty;
            this.Mode = enMode.AddNew;
        }

        private clsPeopleBL(int id, string nationalNo, string firstName, string secondName, string thirdName, string lastName,
            DateTime dateOfBirth, byte gender, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            this.ID = id;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            this.Mode = enMode.Update;
        }

        public static clsPeopleBL FindPersonByID(int ID)
        {
            string nationalNo = string.Empty;
            string firstName = string.Empty;
            string secondName = string.Empty;
            string thirdName = string.Empty;
            string lastName = string.Empty;
            DateTime dateOfBirth = DateTime.MinValue;
            byte gender = 0;
            string address = string.Empty;
            string phone = string.Empty;
            string email = string.Empty;
            int nationalityCountryID = 0;
            string imagePath = string.Empty;

            if (clsPeopleDAL.GetPersonByID(ID, ref nationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender,
                ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath))
            {
                return new clsPeopleBL(ID, nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address,
                phone, email, nationalityCountryID, imagePath);
            }
            else
            {
                return null;
            }
        }
        public static clsPeopleBL FindPersonByNationalNo(string NationalNo)
        {
            int id = -1;
            string firstName = string.Empty;
            string secondName = string.Empty;
            string thirdName = string.Empty;
            string lastName = string.Empty;
            DateTime dateOfBirth = DateTime.MinValue;
            byte gender = 0;
            string address = string.Empty;
            string phone = string.Empty;
            string email = string.Empty;
            int nationalityCountryID = 0;
            string imagePath = string.Empty;

            if (clsPeopleDAL.GetPersonByNationalNo(ref id, NationalNo, ref firstName, ref secondName, ref thirdName, ref lastName, ref dateOfBirth, ref gender,
                ref address, ref phone, ref email, ref nationalityCountryID, ref imagePath))
            {
                return new clsPeopleBL(id, NationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address,
                phone, email, nationalityCountryID, imagePath);
            }
            else
            {
                return null;
            }
        }

        public static bool checkPersonNationalNo(string NationalNo)
        {
            return clsPeopleDAL.CheckPersonNationalNo(NationalNo);
        }
        private bool _AddNewPerson()
        {
            this.ID = clsPeopleDAL.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return this.ID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPeopleDAL.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewPerson())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdatePerson();
                default:
                    return false;
            }
        }

        public static DataTable GetAllData()
        {
            return clsPeopleDAL.GetAllData();
        }

        public static bool DeletePerson(int ID)
        {
            return clsPeopleDAL.DeletePerson(ID);
        }
    }
}
