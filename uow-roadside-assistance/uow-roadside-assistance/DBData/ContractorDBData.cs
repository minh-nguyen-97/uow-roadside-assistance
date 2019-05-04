using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class ContractorDBData
    {
        public static Contractor getContractorByID(int userID)
        {
            User getUser = UserDBData.getUserByID(userID);
            String username = getUser.UserName;
            String email = getUser.Email;
            String password = getUser.Password;
            String userType = getUser.UserType;
            String fullName = getUser.FullName;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.CONTRACTORS WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = cmd.ExecuteReader();

            Contractor res = null;
            if (reader.Read())
            {
                String accountName = Convert.ToString(reader["accountName"]).TrimEnd();
                String accountNumber = Convert.ToString(reader["accountNumber"]).TrimEnd();
                int BSB = Convert.ToInt32(reader["BSB"]);

                res = new Contractor(userID, username, email, password, userType, fullName, accountName, accountNumber, BSB);
            }

            conn.Close();

            return res;
        }

        // Insert 
        public static void insertNewContractor(int userID, String accountName, String accountNumber, int BSB)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String insertQuery = "INSERT INTO dbo.CONTRACTORS(userID, accountName, accountNumber, BSB) " +
                                    "VALUES (@userID, @accountName, @accountNumber, @BSB)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            cmd.Parameters.AddWithValue("@BSB", BSB);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public static void updateContractor(int userID, String accountName, String accountNumber, int BSB)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String updateQuery = "UPDATE dbo.CONTRACTORS " +
                                    "SET accountName = @accountName, accountNumber = @accountNumber, BSB = @BSB " +
                                    "WHERE userID = @userID";

            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            cmd.Parameters.AddWithValue("@BSB", BSB);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

    }
}