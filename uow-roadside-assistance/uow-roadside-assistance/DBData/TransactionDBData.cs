using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class TransactionDBData
    {

        //
        public static Transaction GetUnfinishedCustomerTransaction(int customerID)
        {
            Boolean customerFinished = false;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE customerID = @customerID and customerFinished = @customerFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@customerFinished", customerFinished);
            SqlDataReader reader = cmd.ExecuteReader();

            Transaction res = null;
            if (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                int contractorID = Convert.ToInt32(reader["contractorID"]);
                // customerID
                double cost = Convert.ToDouble(reader["cost"]);
                Boolean contractorFinished = Convert.ToBoolean(reader["contractorFinished"]);
                // customerFinished

                res = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished);
            }

            conn.Close();

            return res;
        }

        //
        public static Transaction GetUnfinishedContractorTransaction(int contractorID)
        {
            Boolean contractorFinished = false;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE contractorID = @contractorID and contractorFinished = @contractorFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerID", contractorID);
            cmd.Parameters.AddWithValue("@customerFinished", contractorFinished);
            SqlDataReader reader = cmd.ExecuteReader();

            Transaction res = null;
            if (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                // contractorID
                int customerID = Convert.ToInt32(reader["customerID"]);
                double cost = Convert.ToDouble(reader["cost"]);
                // customerFinished
                Boolean customerFinished = Convert.ToBoolean(reader["customerFinished"]);

                res = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished);
            }

            conn.Close();

            return res;
        }


        //
        public static Boolean IsExistUnfinishedCustomerTransaction(int customerID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE customerID = @customerID and customerFinished = @customerFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@customerFinished", false);
            SqlDataReader reader = cmd.ExecuteReader();

            Boolean check = reader.HasRows;

            conn.Close();

            return check;
        }

        public static Boolean IsExistUnfinishedContractorTransaction(int contractorID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE contractorID = @contractorID and contractorFinished = @contractorFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            cmd.Parameters.AddWithValue("@contractorFinished", false);
            SqlDataReader reader = cmd.ExecuteReader();

            Boolean check = reader.HasRows;

            conn.Close();

            return check;
        }

        //
        public static void insertNewTransaction(int contractorID, int customerID, double cost)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String insertQuery = "INSERT INTO dbo.TRANSACTIONS(contractorID, customerID, cost, contractorFinished, customerFinished)" +
                                    "VALUES (@contractorID, @customerID, @cost, @contractorFinished, @customerFinished)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);

            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@contractorFinished", false);
            cmd.Parameters.AddWithValue("@customerFinished", false);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // 
        public static void customerFinishedTransaction(int transactionID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String updateQuery =    "UPDATE dbo.TRANSACTIONS " +
                                    "SET customerFinished = 1 " +
                                    "WHERE transactionID = @transactionID";
            SqlCommand cmd = new SqlCommand(updateQuery, conn);

            cmd.Parameters.AddWithValue("@transactionID", transactionID);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public static void contractorFinishedTransaction(int transactionID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String updateQuery = "UPDATE dbo.TRANSACTIONS " +
                                    "SET contractorFinished = 1 " +
                                    "WHERE transactionID = @transactionID";
            SqlCommand cmd = new SqlCommand(updateQuery, conn);

            cmd.Parameters.AddWithValue("@transactionID", transactionID);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}