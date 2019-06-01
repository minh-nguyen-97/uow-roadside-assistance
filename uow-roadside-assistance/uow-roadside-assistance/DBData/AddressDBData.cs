using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class AddressDBData
    {
        public static ArrayList getAddressByUserType(String userType)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.ADDRESS WHERE userType = @userType";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userType", userType);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int userID = Convert.ToInt32(reader["userID"]);
                String latitude = Convert.ToString(reader["latitude"]).TrimEnd();
                String longitude = Convert.ToString(reader["longitude"]).TrimEnd();

                Address address = new Address(userID, latitude, longitude, userType);

                res.Add(address);
            }

            conn.Close();

            return res;
        }

        public static Address getAddressByUserID(int userID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.ADDRESS WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = cmd.ExecuteReader();

            Address res = null;
            if (reader.Read())
            {
                String latitude = Convert.ToString(reader["latitude"]).TrimEnd();
                String longitude = Convert.ToString(reader["longitude"]).TrimEnd();
                String userType = Convert.ToString(reader["userType"]).TrimEnd();

                res = new Address(userID, latitude, longitude, userType);
                
            }

            conn.Close();

            return res;
        }
    }
}