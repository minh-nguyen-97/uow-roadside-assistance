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
    public class TransactionDBData
    {
        // 
        public static ArrayList GetCompletedTransactionsByContractorID(int contractorID)
        {
            Boolean customerFinished = true;
            Boolean contractorFinished = true;

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE customerFinished = @customerFinished and contractorFinished = @contractorFinished and contractorID = @contractorID";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerFinished", customerFinished);
            cmd.Parameters.AddWithValue("@contractorFinished", contractorFinished);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                // contractorID 
                int customerID = Convert.ToInt32(reader["customerID"]);
                double cost = Convert.ToDouble(reader["cost"]);
                // contractorFinished
                // customerFinished

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                Transaction transaction = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);

                res.Add(transaction);
            }

            conn.Close();

            return res;
        }

        // 
        public static ArrayList GetCompletedTransactionsByCustomerID(int customerID)
        {
            Boolean customerFinished = true;
            Boolean contractorFinished = true;

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE customerFinished = @customerFinished and contractorFinished = @contractorFinished and customerID = @customerID";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerFinished", customerFinished);
            cmd.Parameters.AddWithValue("@contractorFinished", contractorFinished);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                int contractorID = Convert.ToInt32(reader["contractorID"]);
                // customerID
                double cost = Convert.ToDouble(reader["cost"]);
                // contractorFinished
                // customerFinished

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                Transaction transaction = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);

                res.Add(transaction);
            }

            conn.Close();

            return res;
        }

        // 
        public static ArrayList GetCompletedTransactions()
        {
            Boolean customerFinished = true;
            Boolean contractorFinished = true;

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE customerFinished = @customerFinished and contractorFinished = @contractorFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@customerFinished", customerFinished);
            cmd.Parameters.AddWithValue("@contractorFinished", contractorFinished);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                int contractorID = Convert.ToInt32(reader["contractorID"]);
                int customerID = Convert.ToInt32(reader["customerID"]);
                double cost = Convert.ToDouble(reader["cost"]);
                // contractorFinished
                // customerFinished

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                Transaction transaction = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);

                res.Add(transaction);
            }

            conn.Close();

            return res;
        }

        //
        public static Transaction GetUnfinishedCustomerTransaction(int customerID)
        {
            Boolean customerFinished = false;

            SqlConnection conn = Helper.Connection.connectionString;
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

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                res = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);
            }

            conn.Close();

            return res;
        }

        //
        public static ArrayList GetUnfinishedContractorTransactions(int contractorID)
        {
            Boolean contractorFinished = false;

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE contractorID = @contractorID and contractorFinished = @contractorFinished";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            cmd.Parameters.AddWithValue("@contractorFinished", contractorFinished);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                // contractorID
                int customerID = Convert.ToInt32(reader["customerID"]);
                double cost = Convert.ToDouble(reader["cost"]);
                // contractorFinished
                Boolean customerFinished = Convert.ToBoolean(reader["customerFinished"]);

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                Transaction transaction = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);

                res.Add(transaction);
            }

            conn.Close();

            return res;
        }

        public static Transaction GetTransactionByID(int transactionID)
        {

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getTransactionQuery = "SELECT * FROM dbo.TRANSACTIONS WHERE transactionID = @transactionID ";
            SqlCommand cmd = new SqlCommand(getTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@transactionID", transactionID);
            SqlDataReader reader = cmd.ExecuteReader();

            Transaction res = null;
            if (reader.Read())
            {
                int contractorID = Convert.ToInt32(reader["contractorID"]);
                int customerID = Convert.ToInt32(reader["customerID"]);
                double cost = Convert.ToDouble(reader["cost"]);

                Boolean contractorFinished = Convert.ToBoolean(reader["contractorFinished"]);
                Boolean customerFinished = Convert.ToBoolean(reader["customerFinished"]);

                Boolean tyreProblem = Convert.ToBoolean(reader["tyreProblem"]);
                Boolean carBatteryProblem = Convert.ToBoolean(reader["carBatteryProblem"]);
                Boolean engineProblem = Convert.ToBoolean(reader["engineProblem"]);
                Boolean generalProblem = Convert.ToBoolean(reader["generalProblem"]);


                String problemDescription = Convert.ToString(reader["problemDescription"]).TrimEnd();
                String customerLatitude = Convert.ToString(reader["customerLatitude"]).TrimEnd();
                String customerLongitude = Convert.ToString(reader["customerLongitude"]).TrimEnd();
                DateTime transactionDate = Convert.ToDateTime(reader["transactionDate"]);

                res = new Transaction(transactionID, contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate);
                
            }

            conn.Close();

            return res;
        }



        //
        public static Boolean IsExistUnfinishedCustomerTransaction(int customerID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
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
            SqlConnection conn = Helper.Connection.connectionString;
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
        public static void insertNewTransaction(int contractorID, double cost, Request req)
        {
            DateTime currentDate = DateTime.Now;
            String transactionDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();
            String insertQuery = "INSERT INTO dbo.TRANSACTIONS(contractorID, customerID, cost, contractorFinished, customerFinished, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, transactionDate)" +
                                    "VALUES (@contractorID, @customerID, @cost, @contractorFinished, @customerFinished, @tyreProblem, @carBatteryProblem, @engineProblem, @generalProblem, @problemDescription, @customerLatitude, @customerLongitude, @transactionDate)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);

            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            cmd.Parameters.AddWithValue("@customerID", req.CustomerID);
            cmd.Parameters.AddWithValue("@cost", cost);
            cmd.Parameters.AddWithValue("@contractorFinished", false);
            cmd.Parameters.AddWithValue("@customerFinished", false);
            
            cmd.Parameters.AddWithValue("@tyreProblem", req.TyreProblem);
            cmd.Parameters.AddWithValue("@carBatteryProblem", req.CarBatteryProblem);
            cmd.Parameters.AddWithValue("@engineProblem", req.EngineProblem);
            cmd.Parameters.AddWithValue("@generalProblem", req.GeneralProblem);

            cmd.Parameters.AddWithValue("@problemDescription", req.ProblemDescription);
            cmd.Parameters.AddWithValue("@customerLatitude", req.CustomerLatitude);
            cmd.Parameters.AddWithValue("@customerLongitude", req.CustomerLongitude);
            cmd.Parameters.AddWithValue("@transactionDate", transactionDate);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // 
        public static void customerFinishedTransaction(int transactionID)
        {
            SqlConnection conn = Helper.Connection.connectionString;
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
            SqlConnection conn = Helper.Connection.connectionString;
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