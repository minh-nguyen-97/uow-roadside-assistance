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
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
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

            return res;
        }
    }
}