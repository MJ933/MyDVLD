using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationsDAL
    {

        public static DataTable GetAllData()
        {
            DataTable dataTable1 = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT
    L.LocalDrivingLicenseApplicationID AS 'L.D.L.AppID',
    Lc.ClassName AS 'Driving Class',
    p.NationalNo AS 'National No',
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS 'Full Name',
    a.ApplicationDate AS 'Application Date',
    (SELECT COUNT(ta.TestTypeID)
     FROM TestAppointments ta
     INNER JOIN LocalDrivingLicenseApplications L2
     ON L2.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID
     INNER JOIN Tests t
     ON t.TestAppointmentID = ta.TestAppointmentID
     WHERE L2.LocalDrivingLicenseApplicationID = L.LocalDrivingLicenseApplicationID
     AND t.TestResult = 1) AS 'Passed Test',
    CASE
        WHEN a.ApplicationStatus = 1 THEN 'New'
        WHEN a.ApplicationStatus = 2 THEN 'Canceled'
        WHEN a.ApplicationStatus = 3 THEN 'Completed'
    END AS 'Status'
FROM
    LocalDrivingLicenseApplications L
INNER JOIN
    LicenseClasses Lc
    ON Lc.LicenseClassID = L.LicenseClassID
INNER JOIN
    Applications a
    ON a.ApplicationID = L.ApplicationID
INNER JOIN
    People p
    ON p.PersonID = a.ApplicantPersonID;";

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

        public static bool UpdateApplicationStatus(int applicationID, int newStatus)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE Applications 
                SET ApplicationStatus = @NewStatus
                WHERE ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@NewStatus", newStatus);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating application status: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool GetLDLApplicationByApplicationID(int ApplicationID, ref DateTime applicationDate,
                    ref int LicenseClassID, ref decimal applicationFees, ref string createdBy, ref int ApplicantPersonID
            , ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT " +
                    "p.ApplicationID, u.UserName, l.LicenseClassID, p.ApplicationDate, p.PaidFees, p.ApplicantPersonID, p.ApplicationTypeID, p.ApplicationStatus, p.LastStatusDate " +
                    "FROM Applications p " +
                    "INNER JOIN Users u ON u.UserID = p.CreatedByUserID " +
                    "INNER JOIN LocalDrivingLicenseApplications l2 ON p.ApplicationID = l2.ApplicationID " +
                    "INNER JOIN LicenseClasses l ON l2.LicenseClassID = l.LicenseClassID " +
                    "WHERE p.ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ApplicationID = (int)reader["ApplicationID"];
                            applicationDate = (DateTime)reader["ApplicationDate"];
                            LicenseClassID = (int)reader["LicenseClassID"];
                            applicationFees = (decimal)reader["PaidFees"];
                            createdBy = reader["UserName"].ToString();
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                }
            }
            return false;
        }
        public static bool GetApplicationByApplicationID(int ApplicationID, ref DateTime applicationDate,
            ref decimal applicationFees, ref string createdBy, ref int ApplicantPersonID,
            ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT " +
                    "a.ApplicationID, u.UserName, a.ApplicationDate, a.PaidFees, a.ApplicantPersonID, a.ApplicationTypeID, a.ApplicationStatus, a.LastStatusDate " +
                    "FROM Applications a " +
                    "INNER JOIN Users u ON u.UserID = a.CreatedByUserID " +
                    "WHERE a.ApplicationID = @ApplicationID;";


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ApplicationID = (int)reader["ApplicationID"];
                            applicationDate = (DateTime)reader["ApplicationDate"];
                            applicationFees = (decimal)reader["PaidFees"];
                            createdBy = reader["UserName"].ToString();
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                }
            }
            return false;
        }
        public static bool GetLDLApplicationByPersonID(int PersonID, ref int LDLApplicationID, ref DateTime applicationDate,
            ref int LicenseClassID, ref decimal applicationFees, ref string createdBy, ref int ApplicantPersonID,
            ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT " +
                    "p.ApplicationID, u.UserName, l.LicenseClassID, p.ApplicationDate, p.PaidFees, p.ApplicantPersonID, p.ApplicationTypeID, p.ApplicationStatus, p.LastStatusDate " +
                    "FROM Applications p " +
                    "INNER JOIN Users u ON u.UserID = p.CreatedByUserID " +
                    "INNER JOIN LocalDrivingLicenseApplications l2 ON p.ApplicationID = l2.ApplicationID " +
                    "INNER JOIN LicenseClasses l ON l2.LicenseClassID = l.LicenseClassID " +
                    "WHERE p.ApplicantPersonID = @ApplicantPersonID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LDLApplicationID = (int)reader["ApplicationID"];
                            applicationDate = (DateTime)reader["ApplicationDate"];
                            LicenseClassID = (int)reader["LicenseClassID"];
                            applicationFees = (decimal)reader["PaidFees"];
                            createdBy = reader["UserName"].ToString();
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                }
            }
            return false;
        }


        public static bool GetApplicationByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref DateTime applicationDate,
    ref int LicenseClassID, ref decimal applicationFees, ref string createdBy, ref int ApplicantPersonID,
    ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT " +
                    "a.ApplicationID, u.UserName, l.LicenseClassID, a.ApplicationDate, a.PaidFees, a.ApplicantPersonID, a.ApplicationTypeID, a.ApplicationStatus, a.LastStatusDate " +
                    "FROM Applications a " +
                    "INNER JOIN Users u ON u.UserID = a.CreatedByUserID " +
                    "INNER JOIN LocalDrivingLicenseApplications l2 ON a.ApplicationID = l2.ApplicationID " +
                    "INNER JOIN LicenseClasses l ON l2.LicenseClassID = l.LicenseClassID " +
                    "WHERE l2.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ApplicationID = (int)reader["ApplicationID"];
                            applicationDate = (DateTime)reader["ApplicationDate"];
                            LicenseClassID = (int)reader["LicenseClassID"];
                            applicationFees = (decimal)reader["PaidFees"];
                            createdBy = reader["UserName"].ToString();
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                }
            }
            return false;
        }


        public static bool NotHaveActiveApplicationOfSameLicenseClass(int LicenseClassID, int ApplicantPersonID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                                SELECT * 
                                FROM LocalDrivingLicenseApplications l 
                                INNER JOIN Applications a 
                                ON a.ApplicationID = l.ApplicationID 
                                WHERE l.LicenseClassID = @LicenseClassID 
                                AND a.ApplicantPersonID = @ApplicantPersonID
                                AND a.ApplicationStatus IN (1, 3)  -- Modified to check for status 1 or 3
                                ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // If a record is found, return false indicating there is an active application
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                    // Rethrow the exception or handle it as per your application's error handling strategy
                    //throw;
                }
            }

            // If no record is found, return true indicating there is no active application
            return true;
        }

        public static int AddNewLDLApplication(string CreatedBy, DateTime ApplicationDate,
            decimal PaidFees, int LicenseClassID, int ApplicantPersonID, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
                ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
                @ApplicationStatus, @LastStatusDate, @PaidFees, (SELECT UserID FROM Users WHERE UserName = @CreatedBy));
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        // Insert into LocalDrivingLicenseApplications table
                        string licenseQuery = @"
                        INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID)
                        VALUES (@ApplicationID, @LicenseClassID);";

                        SqlCommand licenseCommand = new SqlCommand(licenseQuery, connection);
                        licenseCommand.Parameters.AddWithValue("@ApplicationID", InsertedID);
                        licenseCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                        connection.Open();
                        licenseCommand.ExecuteNonQuery();
                        connection.Close();

                        return InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error adding new application: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool DeleteLDLApplicationByID(int ApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                // First, delete from the LocalDrivingLicenseApplications table
                string deleteLicenseQuery = @"
        DELETE FROM LocalDrivingLicenseApplications 
        WHERE ApplicationID = @ApplicationID;";

                SqlCommand deleteLicenseCommand = new SqlCommand(deleteLicenseQuery, connection);
                deleteLicenseCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    int rowsAffectedLicense = deleteLicenseCommand.ExecuteNonQuery();

                    // Then, delete from the Applications table
                    string deleteApplicationQuery = @"
            DELETE FROM Applications 
            WHERE ApplicationID = @ApplicationID;";

                    SqlCommand deleteApplicationCommand = new SqlCommand(deleteApplicationQuery, connection);
                    deleteApplicationCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                    int rowsAffectedApplication = deleteApplicationCommand.ExecuteNonQuery();
                    connection.Close();

                    // Return true if both deletions were successful
                    return rowsAffectedLicense > 0 && rowsAffectedApplication > 0;
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting application: " + ex.Message);
                }
            }
            return false;
        }


        public static bool UpdateLDLApplication(int ApplicationID, string CreatedBy,
    DateTime ApplicationDate, decimal PaidFees, int LicenseClassID, int ApplicantPersonID, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        UPDATE Applications SET
            CreatedByUserID = (SELECT UserID FROM Users WHERE UserName = @CreatedBy),
            ApplicationDate = @ApplicationDate,
            PaidFees = @PaidFees,
            ApplicantPersonID = @ApplicantPersonID,
            ApplicationTypeID = @ApplicationTypeID,
            ApplicationStatus = @ApplicationStatus,
            LastStatusDate = @LastStatusDate
        WHERE ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();

                    // Update LocalDrivingLicenseApplications table
                    string licenseQuery = @"
            UPDATE LocalDrivingLicenseApplications SET
                LicenseClassID = @LicenseClassID
            WHERE ApplicationID = @ApplicationID;";

                    SqlCommand licenseCommand = new SqlCommand(licenseQuery, connection);
                    licenseCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                    licenseCommand.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                    RowsAffected += licenseCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating application: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }

        public static bool CancelApplicationStatus(int ApplicationID, byte ApplicationStatus, DateTime LastStatusDate)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            UPDATE Applications SET
                ApplicationStatus = @ApplicationStatus,
                LastStatusDate = @LastStatusDate
            WHERE ApplicationID = @ApplicationID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating application: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting application: " + ex.Message);
                }
            }
            return RowsAffected > 0;
        }


        public static int GetPassedTests(int LDLAppID)
        {
            int PassedTests = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT COUNT(ta.TestTypeID) AS [Passed Tests] " +
                               "FROM TestAppointments ta " +
                               "INNER JOIN LocalDrivingLicenseApplications L2 " +
                               "ON L2.LocalDrivingLicenseApplicationID = ta.LocalDrivingLicenseApplicationID " +
                               "INNER JOIN Tests t " +
                               "ON t.TestAppointmentID = ta.TestAppointmentID " +
                               "WHERE L2.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " +
                               "AND t.TestResult = 1;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLAppID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PassedTests = (int)reader["Passed Tests"];
                            return PassedTests;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                    // Optionally rethrow the exception if needed
                    // throw;
                }
            }
            return PassedTests;
        }





        public static int AddNewApplication(string CreatedBy, DateTime ApplicationDate,
    decimal PaidFees, int ApplicantPersonID, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID,
            ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
            VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
            @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
            SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CreatedByUserID", CreatedBy);
                command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

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
                    Console.WriteLine("Error adding new application: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool GetApplicationByAppID(int ApplicationID, ref DateTime applicationDate,
            ref decimal applicationFees, ref string createdBy, ref int ApplicantPersonID,
            ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT " +
                    "a.ApplicationID, u.UserName, a.ApplicationDate, a.PaidFees, a.ApplicantPersonID, a.ApplicationTypeID, a.ApplicationStatus, a.LastStatusDate " +
                    "FROM Applications a " +
                    "INNER JOIN Users u ON u.UserID = a.CreatedByUserID " +
                    "WHERE a.ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ApplicationID = (int)reader["ApplicationID"];
                            applicationDate = (DateTime)reader["ApplicationDate"];
                            applicationFees = (decimal)reader["PaidFees"];
                            createdBy = reader["UserName"].ToString();
                            ApplicantPersonID = (int)reader["ApplicantPersonID"];
                            ApplicationTypeID = (int)reader["ApplicationTypeID"];
                            ApplicationStatus = (byte)reader["ApplicationStatus"];
                            LastStatusDate = (DateTime)reader["LastStatusDate"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving application: " + ex.Message);
                }
            }
            return false;
        }

        public static bool UpdateApplication(int ApplicationID, string CreatedBy,
            DateTime ApplicationDate, decimal PaidFees, int ApplicantPersonID, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
            UPDATE Applications SET
                CreatedByUserID = (SELECT UserID FROM Users WHERE UserName = @CreatedBy),
                ApplicationDate = @ApplicationDate,
                PaidFees = @PaidFees,
                ApplicantPersonID = @ApplicantPersonID,
                ApplicationTypeID = @ApplicationTypeID,
                ApplicationStatus = @ApplicationStatus,
                LastStatusDate = @LastStatusDate
            WHERE ApplicationID = @ApplicationID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                command.Parameters.AddWithValue("@PaidFees", PaidFees);
                command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating application: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }

        public static bool DeleteApplication1(int ApplicationID)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting application: " + ex.Message);
                }
            }
            return RowsAffected > 0;
        }

    }
}
