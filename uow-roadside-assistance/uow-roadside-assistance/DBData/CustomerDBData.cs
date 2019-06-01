using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class CustomerDBData
    {
        public static Customer getCustomerByID(int userID)
        {
            User getUser = UserDBData.getUserByID(userID);
            String username = getUser.UserName;
            String email = getUser.Email;
            String password = getUser.Password;
            String userType = getUser.UserType;
            String fullName = getUser.FullName;

            SqlConnection conn = Helper.Connection.connectionString;
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.CUSTOMERS WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = cmd.ExecuteReader();

            Customer res = null;
            if (reader.Read())
            {
                String regNo = Convert.ToString(reader["regNo"]).TrimEnd();
                String make = Convert.ToString(reader["make"]).TrimEnd();
                String model = Convert.ToString(reader["model"]).TrimEnd();
                String color = Convert.ToString(reader["color"]).TrimEnd();
                String cardHolder = Convert.ToString(reader["cardHolder"]).TrimEnd();
                String cardNo = Convert.ToString(reader["cardNo"]).TrimEnd();
                int expMonth = Convert.ToInt32(reader["expMonth"]);
                int expYear = Convert.ToInt32(reader["expYear"]);
                int CVV = Convert.ToInt32(reader["CVV"]);

                res = new Customer(userID, username, email, password, userType, fullName, regNo, make, model, color, cardHolder, cardNo, expMonth, expYear, CVV);
            }

            conn.Close();

            return res;

        }

        public static void insertNewCustomer(int userID, String regNo, String make, String model, String color, String cardHolder, String cardNo, int expMonth, int expYear, int CVV)
        {
            SqlConnection conn = Helper.Connection.connectionString;
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
            SqlConnection conn = Helper.Connection.connectionString;
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