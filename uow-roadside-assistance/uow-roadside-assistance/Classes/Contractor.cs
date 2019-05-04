using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Contractor : User
    {
        private String _accountName;
        public String AccountName
        {
            get { return _accountName; }
        }

        private String _accountNumber;
        public String AccountNumber
        {
            get { return _accountNumber; }
        }

        private int _BSB;
        public int BSB
        {
            get { return _BSB; }
        }

        public Contractor(int userID, String username, String email, String password, String userType, String fullName, String accountName, String accountNumber, int bsb) : base(userID, username, email, password, userType, fullName)
        {
            _accountName = accountName;
            _accountNumber = accountNumber;
            _BSB = bsb;
        }
    }
}