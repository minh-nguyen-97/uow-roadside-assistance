using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.DBData
{
    public class CustomerDBData
    {
        public static void insertNewCustomer(int userID, String regNo, String make, String model, String color, String cardHolder, String cardNo, int expMonth, int expYear, int CVV)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String insertQuery = "INSERT INTO dbo.CUSTOMERS(userID, regNo, make, model, color, cardHolder, cardNo, expMonth, expYear, CVV)" +
                                    "VALUES (@userID, @regNo, @make, @model, @color, @cardHolder, @cardNo, @expMonth, @expYear, @CVV)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@regNo", regNo);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@color", color);
            cmd.Parameters.AddWithValue("@cardHolder", cardHolder);
            cmd.Parameters.AddWithValue("@cardNo", cardNo);
            cmd.Parameters.AddWithValue("@expMonth", expMonth);
            cmd.Parameters.AddWithValue("@expYear", expYear);
            cmd.Parameters.AddWithValue("@CVV", CVV);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public static void updateCustomer(int userID, String regNo, String make, String model, String color, String cardHolder, String cardNo, int expMonth, int expYear, int CVV)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String updateCustomerQuery = "UPDATE dbo.CUSTOMERS " +
                                     "SET regNo = @regNo, make = @make, model = @model, color = @color, cardHolder = @cardHolder, cardNo = @cardNo, expMonth = @expMonth, expYear = @expYear, CVV =  @CVV " +
                                     "WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(updateCustomerQuery, conn);
            cmd.Parameters.AddWithValue("@regNo", regNo);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@color", color);
            cmd.Parameters.AddWithValue("@cardHolder", cardHolder);
            cmd.Parameters.AddWithValue("@cardNo", cardNo);
            cmd.Parameters.AddWithValue("@expMonth", expMonth);
            cmd.Parameters.AddWithValue("@expYear", expYear);
            cmd.Parameters.AddWithValue("@CVV", CVV);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}