using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class User
    {
        protected int _userID;
        public int UserID
        {
            get { return _userID; }
        }

        protected String _userName;
        public String UserName
        {
            get { return _userName; }
        }

        protected String _email;
        public String Email
        {
            get { return _email; }
        }

        protected String _password;
        public String Password
        {
            get { return _password; }
        }

        protected String _userType;
        public String UserType
        {
            get { return _userType; }
        }

        protected String _fullName;
        public String FullName
        {
            get { return _fullName; }
        }

        public User(int userID, String username, String email, String password, String userType, String fullName)
        {
            _userID = userID;
            _userName = username;
            _email = email;
            _password = password;
            _userType = userType;
            _fullName = fullName;
        }
    }
}