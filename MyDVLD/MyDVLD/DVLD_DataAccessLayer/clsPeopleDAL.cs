using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.IO;
namespace DVLD_DataAccessLayer
{
    public class clsPeopleDAL
    {
        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM People WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                            NationalNo = reader["NationalNo"].ToString();
                            FirstName = reader["FirstName"].ToString();
                            SecondName = reader["SecondName"].ToString();
                            ThirdName = reader["ThirdName"].ToString();
                            LastName = reader["LastName"].ToString();
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gendor"];
                            Address = reader["Address"].ToString();
                            Phone = reader["Phone"].ToString();
                            Email = reader["Email"].ToString();
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath = reader["ImagePath"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving person: " + ex.Message);
                }
            }
            return IsFound;
        }

        public static bool GetPersonByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, ref DateTime DateOfBirth, ref byte Gender, ref string Address, ref string Phone, ref string Email,
           ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                            PersonID = (int)reader["PersonID"];
                            FirstName = reader["FirstName"].ToString();
                            SecondName = reader["SecondName"].ToString();
                            ThirdName = reader["ThirdName"].ToString();
                            LastName = reader["LastName"].ToString();
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gendor"];
                            Address = reader["Address"].ToString();
                            Phone = reader["Phone"].ToString();
                            Email = reader["Email"].ToString();
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath = reader["ImagePath"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving person: " + ex.Message);
                }
            }
            return IsFound;
        }

        public static bool CheckPersonNationalNo(string NationalNo)
        {

            bool IsFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IsFound = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving person: " + ex.Message);
                }
            }
            return IsFound;

        }

        public static DataTable GetAllData()
        {
            DataTable dataTable1 = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT 
                                 PersonID AS 'Person ID', 
                                 NationalNo AS 'National No', 
                                 FirstName AS 'First Name',
                                 SecondName AS 'Second Name', 
                                 ThirdName AS 'Third Name', 
                                 LastName AS 'Last Name',
                                 CASE 
                                     WHEN Gendor = 0 THEN 'Male'
                                     WHEN Gendor = 1 THEN 'Female'
                                 END AS 'Gender',
                                 DateOfBirth AS 'Date Of Birth',
                                 (SELECT CountryName 
                                  FROM Countries 
                                  WHERE People.NationalityCountryID = Countries.CountryID) AS Nationality,
                                 Phone, 
                                 Email
                             FROM 
                                 People;
                             ";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable1.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving data: " + ex.Message);
                }
            }

            return dataTable1;
        }
        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName, string ThirdName,
              string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email,
              int NationalityCountryID, string ImagePath)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone,
                                    Email, NationalityCountryID, ImagePath)
                VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone,
                        @Email, @NationalityCountryID, @ImagePath);
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Gendor", Gender);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath", ImagePath);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        return InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error adding new person: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, byte Gender, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE People SET
                    NationalNo = @NationalNo,
                    FirstName = @FirstName,
                    SecondName = @SecondName,
                    ThirdName = @ThirdName,
                    LastName = @LastName,
                    DateOfBirth = @DateOfBirth,
                    Gendor = @Gendor,
                    Address = @Address,
                    Phone = @Phone,
                    Email = @Email,
                    NationalityCountryID = @NationalityCountryID,
                    ImagePath = @ImagePath
                WHERE PersonID = @PersonID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@NationalNo", NationalNo);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@SecondName", SecondName);
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@Gendor", Gender);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath", ImagePath);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating person: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }

        public static bool DeletePerson(int ID)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM People WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", ID);
                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting person: " + ex.Message);
                }
            }
            return RowsAffected > 0;
        }
    }
}
