using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer // Replace with your namespace
{
    public class clsInternationalLicensesDAL
    {
        public static DataTable GetAllInternationalLicensesData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) // Replace clsDataAccessSettings with your connection string class
            {
                string query = @"select InternationalLicenseID as 'Int.License ID', ApplicationID as 'Application ID'
                                , DriverID as 'Driver ID', IssuedUsingLocalLicenseID as 'L.License ID', IssueDate as 'Issue Date'
                                , ExpirationDate as 'Expiration Date', IsActive as 'Is Active'
                                from InternationalLicenses;";
                string query2 = @"
                                SELECT 
                                    InternationalLicenseID,
                                    ApplicationID,
                                    DriverID,
                                    IssuedUsingLocalLicenseID,
                                    IssueDate,
                                    ExpirationDate,
                                    IsActive,
                                    CreatedByUserID
                                FROM InternationalLicenses;";

                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving InternationalLicenses data: " + ex.Message);
                }
            }
            return dataTable;
        }


        public static DataTable GetInternationalLicensesDataByLDLicense(int IssuedUsingLocalLicenseID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) // Replace clsDataAccessSettings with your connection string class
            {
                string query = @"select InternationalLicenseID as 'Int.License ID', ApplicationID as 'Application ID'
                                , IssuedUsingLocalLicenseID as 'L.License ID', IssueDate as 'Issue Date'
                                , ExpirationDate as 'Expiration Date', IsActive as 'Is Active'
                                from InternationalLicenses
                                where IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID
                                ;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving InternationalLicenses data: " + ex.Message);
                }
            }
            return dataTable;
        }


        public static bool GetInternationalLicenseByInternationalLicenseID
            (int internationalLicenseID, ref int applicationID, ref int driverID, ref int issuedUsingLocalLicenseID,
            ref DateTime issueDate, ref DateTime expirationDate, ref bool isActive, ref int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationID = (int)reader["ApplicationID"];
                            driverID = (int)reader["DriverID"];
                            issuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                            issueDate = (DateTime)reader["IssueDate"];
                            expirationDate = (DateTime)reader["ExpirationDate"];
                            isActive = (bool)reader["IsActive"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving InternationalLicense: " + ex.Message);
                }
            }
            return false;
        }

        public static bool GetInternationalLicenseByLDLicenseID
            (ref int internationalLicenseID, ref int applicationID, ref int driverID, int issuedUsingLocalLicenseID,
            ref DateTime issueDate, ref DateTime expirationDate, ref bool isActive, ref int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalLicenseID = (int)reader["InternationalLicenseID"];
                            applicationID = (int)reader["ApplicationID"];
                            driverID = (int)reader["DriverID"];
                            //issuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                            issueDate = (DateTime)reader["IssueDate"];
                            expirationDate = (DateTime)reader["ExpirationDate"];
                            isActive = (bool)reader["IsActive"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving InternationalLicense: " + ex.Message);
                }
            }
            return false;
        }


        public static bool GetInternationalLicenseByDriverID(int driverID, ref int internationalLicenseID, ref int applicationID,
        ref int issuedUsingLocalLicenseID, ref DateTime issueDate, ref DateTime expirationDate, ref bool isActive, ref int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM InternationalLicenses WHERE DriverID = @DriverID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            internationalLicenseID = (int)reader["InternationalLicenseID"];
                            applicationID = (int)reader["ApplicationID"];
                            issuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                            issueDate = (DateTime)reader["IssueDate"];
                            expirationDate = (DateTime)reader["ExpirationDate"];
                            isActive = (bool)reader["IsActive"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving InternationalLicense by DriverID: " + ex.Message);
                }
            }
            return false;
        }

        public static bool DoesInternationalLicenseExistByDriverID(int driverID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM InternationalLicenses WHERE DriverID = @DriverID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error checking for existing InternationalLicense: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool DoesInternationalLicenseExistByLDLicense(int IssuedUsingLocalLicenseID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM InternationalLicenses WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error checking for existing InternationalLicense: " + ex.Message);
                    return false;
                }
            }
        }


        public static int AddNewInternationalLicense(int applicationID, int driverID, int issuedUsingLocalLicenseID,
                                                        DateTime issueDate, DateTime expirationDate, bool isActive,
                                                        int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO InternationalLicenses (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserId);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        return insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error adding new InternationalLicense: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool UpdateInternationalLicense(int internationalLicenseID, int applicationID, int driverID,
                                                       int issuedUsingLocalLicenseID, DateTime issueDate,
                                                       DateTime expirationDate, bool isActive, int createdByUserId)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE InternationalLicenses SET
                    ApplicationID = @ApplicationID,
                    DriverID = @DriverID,
                    IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                    IssueDate = @IssueDate,
                    ExpirationDate = @ExpirationDate,
                    IsActive = @IsActive,
                    CreatedByUserID = @CreatedByUserID 
                WHERE InternationalLicenseID = @InternationalLicenseID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserId);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating InternationalLicense: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteInternationalLicense(int internationalLicenseID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@InternationalLicenseID", internationalLicenseID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting InternationalLicense: " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }
    }
}