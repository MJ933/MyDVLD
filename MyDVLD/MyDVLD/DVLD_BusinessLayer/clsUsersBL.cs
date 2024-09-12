using DVLD_DataAccessLayer;
using System;
using System.Data;

namespace DVLD_BusinessLayer
{
    public class clsUsersBL
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode { get; set; } = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        private clsPeopleBL _person;

        public clsUsersBL()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;
            this._person = new clsPeopleBL();
        }

        public clsUsersBL(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.Mode = enMode.Update;
        }

        public static clsUsersBL FindUserByUserID(int userID)
        {
            int personID = -1;
            string userName = string.Empty;
            string password = string.Empty;
            bool isActive = false;

            // Assuming you have a method in clsUsersDAL to get user details by ID
            if (clsUsersDAL.GetUserByUserID(userID, ref personID, ref userName, ref password, ref isActive))
            {
                return new clsUsersBL(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUsersBL FindUserByPersonID(int personID)
        {
            int userID = -1;
            string userName = string.Empty;
            string password = string.Empty;
            bool isActive = false;

            // Assuming you have a method in clsUsersDAL to get user details by ID
            if (clsUsersDAL.GetUserByPersonID(ref userID, personID, ref userName, ref password, ref isActive))
            {
                return new clsUsersBL(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }


        public static clsUsersBL FindUserByUserName(string userName)
        {
            int userID = -1;
            int personID = -1;
            string password = string.Empty;
            bool isActive = false;

            // Assuming you have a method in clsUsersDAL to get user details by ID
            if (clsUsersDAL.GetUserByUserName(ref userID, ref personID, userName, ref password, ref isActive))
            {
                return new clsUsersBL(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }


        private bool _AddNewUser()
        {

            if (this.PersonID != -1)
            {
                this.UserID = clsUsersDAL.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
                return this.UserID != -1;
            }
            return false;
        }

        private bool _UpdateUser()
        {
            return clsUsersDAL.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public bool Save()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewUser())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return this._UpdateUser();
                default:
                    return false;
            }
        }

        public static DataTable GetAllData()
        {
            return clsUsersDAL.GetAllData();
        }

        public static bool DeleteUser(int userID)
        {
            return clsUsersDAL.DeleteUser(userID);
        }

    }
}