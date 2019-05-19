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
    public class ReviewDBData
    {
        public static double getAverageRatingByContractorID(int contractorID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT AVG(rating) AS averageRating FROM dbo.REVIEWS WHERE contractorID = @contractorID;";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            SqlDataReader reader = cmd.ExecuteReader();

            double averageRating = 0;
            if (reader.Read())
            {
                averageRating = Convert.ToDouble(reader["averageRating"]);
            }

            conn.Close();

            return averageRating;
        }

        public static ArrayList getReviewsByContractorID(int contractorID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.REVIEWS WHERE contractorID = @contractorID ORDER BY reviewDate DESC";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList res = new ArrayList();
            while (reader.Read())
            {
                int reviewID = Convert.ToInt32(reader["reviewID"]);
                String reviewDesc = Convert.ToString(reader["reviewDesc"]).TrimEnd();
                double rating = Convert.ToInt32(reader["rating"]);
                int transactionID = Convert.ToInt32(reader["transactionID"]);
                int customerID = Convert.ToInt32(reader["customerID"]);
                // contractorID
                DateTime reviewDate = Convert.ToDateTime(reader["reviewDate"]);

                Review review = new Review(reviewID, reviewDesc, rating, transactionID, customerID, contractorID, reviewDate);

                res.Add(review);
            }

            conn.Close();

            return res;
        }

        //
        public static void insertNewReview(String reviewDesc, double rating, int transactionID)
        {
            Transaction transaction = TransactionDBData.GetTransactionByID(transactionID);
            int customerID = transaction.CustomerID;
            int contractorID = transaction.ContractorID;

            DateTime currentDate = DateTime.Now;
            String reviewDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String insertQuery = "INSERT INTO dbo.REVIEWS(reviewDesc, rating, transactionID, customerID, contractorID, reviewDate) " +
                                    "VALUES (@reviewDesc, @rating, @transactionID, @customerID, @contractorID, @reviewDate)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@reviewDesc", reviewDesc);
            cmd.Parameters.AddWithValue("@rating", rating);
            cmd.Parameters.AddWithValue("@transactionID", transactionID);
            cmd.Parameters.AddWithValue("@customerID", customerID);
            cmd.Parameters.AddWithValue("@contractorID", contractorID);
            cmd.Parameters.AddWithValue("@reviewDate", reviewDate);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
        
    }
}