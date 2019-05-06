using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Address
    {
        protected int _userID;
        public int UserID
        {
            get { return _userID; }
        }

        protected String _latitude;
        public String Latitude
        {
            get { return _latitude; }
        }

        protected String _longitude;
        public String Longitude
        {
            get { return _longitude; }
        }

        protected String _userType;
        public String UserType
        {
            get { return _userType; }
        }

        public Address(int userID, String latitude, String longitude, String userType)
        {
            _userID = userID;
            _latitude = latitude;
            _longitude = longitude;
            _userType = userType;
        }
        
    }
}