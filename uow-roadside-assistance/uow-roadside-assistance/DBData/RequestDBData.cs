using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class RequestDBData
    {
        public static Request getRequestByCustomerID(int customerID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getRequestQuery = "SELECT * FROM dbo.REQUESTS WHERE customerID = @customerID";
            SqlCommand cmd = new SqlCommand(getRequestQuery, conn);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            SqlDataReader reader = cmd.ExecuteReader();

            Request res = null;
            if (reader.Read())
            {
                int requestID = Convert.ToInt32(reader["requestID"]);

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                String requestStatus = Convert.ToString(reader["requestStatus"]).TrimEnd();

                DateTime requestDate = Convert.ToDateTime(reader["requestDate"]);

                res = new Request(requestID, customerID, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, requestStatus, requestDate);
            }

            conn.Close();

            return res;
        }


        public static Request getRequestByRequestID(int requestID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getRequestQuery = "SELECT * FROM dbo.REQUESTS WHERE requestID = @requestID";
            SqlCommand cmd = new SqlCommand(getRequestQuery, conn);
            cmd.Parameters.AddWithValue("@requestID", requestID);
            SqlDataReader reader = cmd.ExecuteReader();

            Request res = null;
            if (reader.Read())
            {
                int customerID = Convert.ToInt32(reader["customerID"]);

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                String requestStatus = Convert.ToString(reader["requestStatus"]).TrimEnd();

                DateTime requestDate = Convert.ToDateTime(reader["requestDate"]);

                res = new Request(requestID, customerID, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, requestStatus, requestDate);
            }

            conn.Close();

            return res;
        }

        //
        public static Boolean IsExist(int customerID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getRequestQuery = "SELECT * FROM dbo.REQUESTS WHERE customerID = @customerID";
            SqlCommand cmd = new SqlCommand(getRequestQuery, conn);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            SqlDataReader reader = cmd.ExecuteReader();

            Boolean check = reader.HasRows;

            conn.Close();

            return check;
        }

        //
        public static void insertNewRequest(int customerID, Boolean tyreProblem, Boolean carBatteryProblem, Boolean engineProblem, Boolean generalProblem, String problemDescription, String customerLatitude, String customerLongitude)
        {
            String requestStatus = "Waiting";
            DateTime currentDate = DateTime.Now;
            String requestDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Insert query for REQUESTS
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();
            String insertRequestQuery = "INSERT INTO dbo.REQUESTS(customerID, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, requestStatus, requestDate)" +
                                    "VALUES (@customerID, @tyreProblem, @carBatteryProblem, @engineProblem, @generalProblem, @problemDescription, @customerLatitude, @customerLongitude, @requestStatus, @requestDate)";
            SqlCommand cmd = new SqlCommand(insertRequestQuery, conn);

            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@tyreProblem", tyreProblem);
            cmd.Parameters.AddWithValue("@carBatteryProblem", carBatteryProblem);
            cmd.Parameters.AddWithValue("@engineProblem", engineProblem);
            cmd.Parameters.AddWithValue("@generalProblem", generalProblem);

            cmd.Parameters.AddWithValue("@problemDescription", problemDescription);
            cmd.Parameters.AddWithValue("@customerLatitude", customerLatitude);
            cmd.Parameters.AddWithValue("@customerLongitude", customerLongitude);
            cmd.Parameters.AddWithValue("@requestStatus", requestStatus);
            cmd.Parameters.AddWithValue("@requestDate", requestDate);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        //

        public static void deleteRequest(int requestID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String deletetRequestQuery = "DELETE FROM dbo.REQUESTS WHERE requestID = @requestID";
            SqlCommand cmd = new SqlCommand(deletetRequestQuery, conn);
            cmd.Parameters.AddWithValue("@requestID", requestID);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}