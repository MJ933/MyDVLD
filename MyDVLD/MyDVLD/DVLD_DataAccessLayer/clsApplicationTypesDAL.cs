using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationTypesDAL
    {
        public static bool GetApplicationTypeByID(int ID, ref string Title, ref decimal Fees)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("ApplicationTypeID", ID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Fees = (int)reader["ApplicationFees"];
                            Fees = reader.GetDecimal(reader.GetOrdinal("ApplicationFees"));

                            Title = reader["ApplicationTypeTitle"].ToString();
                            return true;

                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving user: " + ex.Message);
                }
            }
            return false;
        }


        public static DataTable GetAllApplicationTypesData()
        {
            DataTable dataTable1 = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT * from ApplicationTypes;";

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

        public static bool UpdateApplicationType(int ID, string Title, decimal Fees)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE ApplicationTypes SET
                    ApplicationTypeTitle = @ApplicationTypeTitle,
                    ApplicationFees = @ApplicationFees
                WHERE ApplicationTypeID = @ApplicationTypeID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationTypeID", ID);
                command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
                command.Parameters.AddWithValue("@ApplicationFees", Fees);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating user: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }

        public static bool UpdateApplicationTypeIDByLDLAppID(int LocalDrivingLicenseApplicationID, int ApplicationTypeID)
        {
            int RowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE Applications SET
                    ApplicationTypeID = @ApplicationTypeID
                WHERE ApplicationID IN (
                    SELECT ApplicationID
                    FROM LocalDrivingLicenseApplications
                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                );";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                try
                {
                    connection.Open();
                    RowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating application type ID: " + ex.Message);
                    return false;
                }
            }
            return RowsAffected > 0;
        }


        public static DataTable GetAllData()
        {
            DataTable dataTable1 = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"select ApplicationTypeID as 'ID', ApplicationTypeTitle as 'Title'
                                ,ApplicationFees as 'Fees' from ApplicationTypes ";

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
    }
}