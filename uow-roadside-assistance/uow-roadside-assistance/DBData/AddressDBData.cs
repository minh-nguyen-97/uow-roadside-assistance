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

        // find hardcode address for contractor
        public static int getNumberOfContractorAddresses()
        {

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String countAddressQuery = "SELECT COUNT(*) AS total FROM dbo.ADDRESS";
            SqlCommand cmd = new SqlCommand(countAddressQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            int res = 0;
            if (reader.Read())
            {
                res = Convert.ToInt32(reader["total"]);
            }

            conn.Close();

            return res;
        }

        public static int getOneUnassignedAddress()
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getOneUnassignedAddressQuery = "SELECT TOP 1 userID from ADDRESS WHERE userID NOT IN ( select userID from CONTRACTORS)";
            SqlCommand cmd = new SqlCommand(getOneUnassignedAddressQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            int unassignedAddressOldUserID = 0;
            if (reader.Read())
            {
                unassignedAddressOldUserID = Convert.ToInt32(reader["userID"]);
            }

            conn.Close();

            return unassignedAddressOldUserID;

            
        }


        public static Boolean findContractorAddress(int newContractorID)
        {
            int maxContractorLimit = getNumberOfContractorAddresses();

            int numOfContractors = ContractorDBData.getNumberOfContractors();
            int numOfContractorsBeforeInsertion = numOfContractors - 1;

            if (numOfContractorsBeforeInsertion + 1 > maxContractorLimit)
            {
                return false;
            }

            Address newContractorIDExistedInAddress = AddressDBData.getAddressByUserID(newContractorID);

            if (newContractorIDExistedInAddress == null)
            {
                int oldUserID = getOneUnassignedAddress();

                updateUserIDForNewContractor(newContractorID, oldUserID);
            }

            return true;
        }

        //
        public static void updateUserIDForNewContractor(int newContractorID, int oldUserID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String updateQuery = "UPDATE dbo.ADDRESS SET userID = @newContractorID WHERE userID = @oldUserID";
            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@newContractorID", newContractorID);
            cmd.Parameters.AddWithValue("@oldUserID", oldUserID);
            cmd.ExecuteNonQuery();

            conn.Close();
          
        }
    }
}