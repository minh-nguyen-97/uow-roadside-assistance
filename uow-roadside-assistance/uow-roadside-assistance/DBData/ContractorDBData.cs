using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.DBData
{
    public class ContractorDBData
    {
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

    }
}