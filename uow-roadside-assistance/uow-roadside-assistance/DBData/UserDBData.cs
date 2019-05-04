using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using uow_roadside_assistance.Classes;

namespace uow_roadside_assistance.DBData
{
    public class UserDBData
    {
        // SELECT methods
        public static int getUserIDFromName(String username)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.USERS WHERE username = @username";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();

            int userID = -1;
            if (reader.Read())
            {
                userID = Convert.ToInt32(reader["userID"]);
            }

            return userID;
        }

        public static String getUserNameFromID(int userID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.USERS WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = cmd.ExecuteReader();

            String userName = null;
            if (reader.Read())
            {
                userName = Convert.ToString(reader["username"]).TrimEnd();
            }

            return userName;
        }

        public static User getUserByName(String username)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.USERS WHERE username = @username";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();

            User res = null;
            if (reader.Read())
            {
                int userID = Convert.ToInt32(reader["userID"]);
                String userName = username;
                String email = Convert.ToString(reader["email"]).TrimEnd();
                String password = Convert.ToString(reader["password"]).TrimEnd();
                String userType = Convert.ToString(reader["userType"]).TrimEnd();
                String fullName = Convert.ToString(reader["fullName"]).TrimEnd();

                res = new User(userID, userName, email, password, userType, fullName);
            }

            conn.Close();

            return res;
        }

        public static User getUserByID(int userID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserNameQuery = "SELECT * FROM dbo.USERS WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(getUserNameQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader reader = cmd.ExecuteReader();

            User res = null;
            if (reader.Read())
            {
                String userName = Convert.ToString(reader["username"]).TrimEnd();
                String email = Convert.ToString(reader["email"]).TrimEnd();
                String password = Convert.ToString(reader["password"]).TrimEnd();
                String userType = Convert.ToString(reader["userType"]).TrimEnd();
                String fullName = Convert.ToString(reader["fullName"]).TrimEnd();

                res = new User(userID, userName, email, password, userType, fullName);
            }

            conn.Close();

            return res;
        }

        // Check existence

        public static Boolean IsExist(String username, String password)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserByNameQuery = "SELECT * FROM dbo.USERS WHERE username = @username and password = @password";
            SqlCommand cmd = new SqlCommand(getUserByNameQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = cmd.ExecuteReader();

            Boolean check = reader.HasRows;

            conn.Close();

            return check;
        }

        public static Boolean IsExist(String username)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String getUserByNameQuery = "SELECT * FROM dbo.USERS WHERE username = @username";
            SqlCommand cmd = new SqlCommand(getUserByNameQuery, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();

            Boolean check = reader.HasRows;

            conn.Close();

            return check;
        }

        // INSERT

        public static void insertNewUser(String username, String email, String password, String userType, String fullName)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();
            String insertQuery = "INSERT INTO dbo.USERS(username, email, password, userType, fullName)" +
                                    "VALUES (@username, @email, @password, @userType, @fullName)";
            SqlCommand cmd = new SqlCommand(insertQuery, conn);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@userType", userType);
            cmd.Parameters.AddWithValue("@fullName", fullName);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // UPDATE

        public static void updateUserEmailByID(int userID, String email)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String updateUserQuery = "UPDATE dbo.USERS SET email = @email WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(updateUserQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void updateUserPasswordByID(int userID, String password)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["roadside-assistanceConnectionString"].ConnectionString);
            conn.Open();

            String updateUserQuery = "UPDATE dbo.USERS SET password = @password WHERE userID = @userID";
            SqlCommand cmd = new SqlCommand(updateUserQuery, conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}