using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer // Replace with your namespace
{
    public class clsDetainedLicensesDAL
    {
        public static DataTable GetAllDetainedLicensesData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
select DetainID as 'D.ID',DL.LicenseID as 'L.ID' ,DetainDate as 'D.Date'
, IsReleased as 'Is Released',FineFees as 'Fine Fees',NationalNo as 'N.No'
,FirstName +' '+ SecondName+' '+ThirdName+' '+LastName
as 'Full Name', ReleaseDate as 'Release Date'
from DetainedLicenses DL
inner join Licenses L on L.LicenseID = DL.LicenseID
inner join Applications a on a.ApplicationID = L.ApplicationID
inner join People p on p.PersonID = a.ApplicantPersonID;";

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
                    Console.WriteLine("Error retrieving detained licenses data: " + ex.Message);
                }
            }
            return dataTable;
        }

        public static bool GetDetainedLicenseByDetainID(int detainID, ref int licenseID, ref DateTime detainDate, ref decimal fineFees,
                                                       ref int createdByUserId, ref bool isReleased, ref DateTime releaseDate,
                                                       ref int releasedByUserId, ref int releaseApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DetainID", detainID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            licenseID = (int)reader["LicenseID"];
                            detainDate = (DateTime)reader["DetainDate"];
                            fineFees = (decimal)reader["FineFees"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            isReleased = (bool)reader["IsReleased"];

                            // Handle nullable ReleaseDate, ReleasedByUserID, ReleaseApplicationID
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleaseDate")))
                                releaseDate = (DateTime)reader["ReleaseDate"];
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleasedByUserID")))
                                releasedByUserId = (int)reader["ReleasedByUserID"];
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")))
                                releaseApplicationID = (int)reader["ReleaseApplicationID"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving detained license: " + ex.Message);
                }
            }
            return false;
        }

        // Add other methods to get detained licenses by different criteria (e.g., LicenseID)

        public static bool GetDetainedLicenseByLicenseID(int licenseID, ref int detainID, ref DateTime detainDate, ref decimal fineFees,
                                                       ref int createdByUserId, ref bool isReleased, ref DateTime releaseDate,
                                                       ref int releasedByUserId, ref int releaseApplicationID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detainID = (int)reader["DetainID"];
                            detainDate = (DateTime)reader["DetainDate"];
                            fineFees = (decimal)reader["FineFees"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            isReleased = (bool)reader["IsReleased"];

                            // Handle nullable ReleaseDate, ReleasedByUserID, ReleaseApplicationID
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleaseDate")))
                                releaseDate = (DateTime)reader["ReleaseDate"];
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleasedByUserID")))
                                releasedByUserId = (int)reader["ReleasedByUserID"];
                            if (!reader.IsDBNull(reader.GetOrdinal("ReleaseApplicationID")))
                                releaseApplicationID = (int)reader["ReleaseApplicationID"];

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving detained license by LicenseID: " + ex.Message);
                }
            }
            return false;
        }

        public static bool DoesDetainedLicenseExistByLicenseID(int licenseID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null;
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error checking for detained license: " + ex.Message);
                    return false; // Return false in case of an error
                }
            }
        }

        public static int AddNewDetainedLicense(int licenseID, DateTime detainDate, decimal fineFees, int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO DetainedLicenses (LicenseID, DetainDate, FineFees, CreatedByUserID)
                VALUES (@LicenseID, @DetainDate, @FineFees, @CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                command.Parameters.AddWithValue("@DetainDate", detainDate);
                command.Parameters.AddWithValue("@FineFees", fineFees);
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
                    Console.WriteLine("Error adding new detained license: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool UpdateDetainedLicense(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserId,
                                                bool isReleased, DateTime releaseDate, int releasedByUserId, int releaseApplicationID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE DetainedLicenses SET
                    LicenseID = @LicenseID,
                    DetainDate = @DetainDate,
                    FineFees = @FineFees,
                    CreatedByUserID = @CreatedByUserID,
                    IsReleased = @IsReleased,
                    ReleaseDate = @ReleaseDate,
                    ReleasedByUserID = @ReleasedByUserID,
                    ReleaseApplicationID = @ReleaseApplicationID
                WHERE DetainID = @DetainID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DetainID", detainID);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                // ... (add parameters for other columns)

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating detained license: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteDetainedLicense(int detainID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM DetainedLicenses WHERE DetainID = @DetainID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DetainID", detainID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting detained license: " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }

        public static bool ReleaseDetainedLicense(int detainID, DateTime releaseDate, int releasedByUserId, int releaseApplicationID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
        UPDATE DetainedLicenses SET
            IsReleased = 1, 
            ReleaseDate = @ReleaseDate,
            ReleasedByUserID = @ReleasedByUserID,
            ReleaseApplicationID = @ReleaseApplicationID
        WHERE DetainID = @DetainID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DetainID", detainID);
                command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserId);
                command.Parameters.AddWithValue("@ReleaseApplicationID", releaseApplicationID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error releasing detained license: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }
    }
}