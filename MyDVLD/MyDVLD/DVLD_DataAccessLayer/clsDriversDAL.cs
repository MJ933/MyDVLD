using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccessLayer // Replace with your namespace
{
    public class clsDriversDAL
    {
        public static DataTable GetAllDriversData()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
select distinct d.DriverID as 'Driver ID', d.PersonID as 'Person ID',p.NationalNo as 'National No.',
p.FirstName + '' +p.SecondName + ' ' + p.ThirdName + ' '+ p.LastName as 'Full Name',
CreatedDate as 'Date',l.IsActive as 'Active Licenses'
from Drivers d
inner join People p on p.PersonID = d.PersonID
inner join Licenses l on l.DriverID = d.DriverID
;";

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
                    Console.WriteLine("Error retrieving drivers data: " + ex.Message);
                }
            }
            return dataTable;
        }

        // In clsDriversDAL.cs
        public static bool GetDriverByPersonID(int personID, ref int driverID, ref int createdByUserId, ref DateTime createdDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", personID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            driverID = (int)reader["DriverID"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            createdDate = (DateTime)reader["CreatedDate"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving driver by PersonID: " + ex.Message);
                }
            }
            return false;
        }
        public static bool GetDriverByDriverID(int driverID, ref int personID, ref int createdByUserId, ref DateTime createdDate)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personID = (int)reader["PersonID"];
                            createdByUserId = (int)reader["CreatedByUserID"];
                            createdDate = (DateTime)reader["CreatedDate"];
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error retrieving driver: " + ex.Message);
                }
            }
            return false;
        }

        // You can add other methods to get drivers by different criteria (e.g., PersonID)

        public static int AddNewDriver(int personID, int createdByUserId)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                VALUES (@PersonID, @CreatedByUserID, GETDATE()); 
                SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PersonID", personID);
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
                    Console.WriteLine("Error adding new driver: " + ex.Message);
                }
            }
            return -1;
        }

        public static bool UpdateDriver(int driverID, int personID, int createdByUserId)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                UPDATE Drivers SET
                    PersonID = @PersonID,
                    CreatedByUserID = @CreatedByUserID
                WHERE DriverID = @DriverID;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@PersonID", personID);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserId);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error updating driver: " + ex.Message);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool DeleteDriver(int driverID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log exception (optional)
                    Console.WriteLine("Error deleting driver: " + ex.Message);
                }
            }
            return rowsAffected > 0;
        }
    }
}