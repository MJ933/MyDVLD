using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer // Replace with your namespace
{
    public class clsLicensesDAL
    {
        public static DataTable GetAllLicensesData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString)) // Replace clsDataAccessSettings with your connection string class
            {
                string query = @"
                                SELECT 
                                    LicenseID,
                                    ApplicationID,
                                    DriverID,
                                    LicenseClass,
                                    IssueDate,
                                    ExpirationDate,
                                    Notes,
                                    PaidFees,
                                    IsActive,
                                    IssueReason,
                                    CreatedByUserID
                                FROM Licenses;";

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
                    Console.WriteLine("Error retrieving licenses data: " + ex.Message);
                }
            }
            return dataTable;
        }

        public static DataTable GetLocalLicenseHistoryByPersonID(int applicantPersonID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        SELECT 
            L.LicenseID AS 'Lic.ID', 
            a.ApplicationID AS 'App.ID',
            Lc.ClassName,
            L.IssueDate AS 'Issue Date',
            L.ExpirationDate AS 'Expiration Date', 
            L.IsActive AS 'Is Active' 
        FROM 
            Licenses L
        INNER JOIN 
            LicenseClasses Lc ON Lc.LicenseClassID = L.LicenseClass
        INNER JOIN 
            Applications a ON a.ApplicationID = L.ApplicationID
        WHERE 
            a.ApplicantPersonID = @ApplicantPersonID;
        ";

                string query2 = @"
        SELECT 
            L.LicenseID AS 'Lic.ID', 
            a.ApplicationID AS 'App.ID',
            Lc.ClassName,
            L.IssueDate AS 'Issue Date',
            L.ExpirationDate AS 'Expiration Date', 
            L.IsActive AS 'Is Active' 
        FROM 
            Licenses L
        INNER JOIN 
            LicenseClasses Lc ON Lc.LicenseClassID = L.LicenseClass
        INNER JOIN 
            Applications a ON a.ApplicationID = L.ApplicationID
        INNER JOIN 
            LocalDrivingLicenseApplications LDLa ON LDLa.ApplicationID = a.ApplicationID
        WHERE 
            a.ApplicantPersonID = @ApplicantPersonID;
        ";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", applicantPersonID);

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
                    Console.WriteLine("Error retrieving local license history: " + ex.Message);
                }
            }
            return dataTable;
        }

        public static bool GetLicenseByLicenseID(int licenseID, ref int applicationID, ref int driverID, ref int licenseClass,
                                               ref DateTime issueDate, ref DateTime expirationDate, ref string notes, ref decimal paidFees,
                                               ref bool isActive, ref byte issueReason, ref int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            applicationID = (int)reader["ApplicationID"];
                            driverID = (int)reader["DriverID"];
                            licenseClass = (int)reader["LicenseClass"];
                            issueDate = (DateTime)reader["IssueDate"];
                            expirationDate = (DateTime)reader["ExpirationDate"];
                            notes = reader["Notes"].ToString();
                            paidFees = (decimal)reader["PaidFees"];
                            isActive = (bool)reader["IsActive"];
                            issueReason = (byte)reader["IssueReason"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving license: " + ex.Message);
                }
            }
            return false;
        }

        public static bool GetLicenseByApplicationID(int applicationID, ref int licenseID, ref int driverID, ref int licenseClass,
                                                   ref DateTime issueDate, ref DateTime expirationDate, ref string notes, ref decimal paidFees,
                                                   ref bool isActive, ref byte issueReason, ref int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseID = (int)reader["LicenseID"];
                            driverID = (int)reader["DriverID"];
                            licenseClass = (int)reader["LicenseClass"];
                            issueDate = (DateTime)reader["IssueDate"];
                            expirationDate = (DateTime)reader["ExpirationDate"];
                            notes = reader["Notes"].ToString();
                            paidFees = (decimal)reader["PaidFees"];
                            isActive = (bool)reader["IsActive"];
                            issueReason = (byte)reader["IssueReason"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving license by ApplicationID: " + ex.Message);
                }
            }
            return false;
        }


        public static int AddNewLicense(int applicationID, int driverID, int licenseClass, DateTime issueDate,
                                            DateTime expirationDate, string notes, decimal paidFees, bool isActive,
                                            byte issueReason, int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID)
                VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@LicenseClass", licenseClass);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                if (string.IsNullOrEmpty(notes))
                {
                    command.Parameters.AddWithValue("@Notes", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Notes", notes);
                }
                //command.Parameters.AddWithValue("@Notes", notes);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@IssueReason", issueReason);
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
                    Console.WriteLine("Error adding new license: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool UpdateLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate,
                                        DateTime expirationDate, string notes, decimal paidFees, bool isActive,
                                        byte issueReason, int createdByUserId)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE Licenses SET
                    ApplicationID = @ApplicationID,
                    DriverID = @DriverID,
                    LicenseClass = @LicenseClass,
                    IssueDate = @IssueDate,
                    ExpirationDate = @ExpirationDate,
                    Notes = @Notes,
                    PaidFees = @PaidFees,
                    IsActive = @IsActive,
                    IssueReason = @IssueReason,
                    CreatedByUserID = @CreatedByUserID 
                WHERE LicenseID = @LicenseID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@LicenseClass", licenseClass);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@Notes", notes);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@IssueReason", issueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserId);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating license: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteLicense(int licenseID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting license: " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }


        public static bool DoesLicenseExistForApplication(int applicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM Licenses WHERE ApplicationID = @ApplicationID"; // Modified query
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Use ExecuteScalar() directly
                    return result != null; // Returns true if a license exists, false otherwise
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error checking for existing license: " + ex.Message);
                    return false; // Return false in case of an error
                }
            }
        }

        // Add this method to your clsLicensesDAL class
        public static bool DeactivateLicense(int licenseID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "UPDATE Licenses SET IsActive = 0 WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deactivating license: " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }


        public static bool HasActiveLicenseOfClass(int driverID, int licenseClass)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM Licenses WHERE DriverID = @DriverID AND LicenseClass = @LicenseClass AND IsActive = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@LicenseClass", licenseClass);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error checking for active license of class: " + ex.Message);
                    return false;
                }
            }
        }


    }
}