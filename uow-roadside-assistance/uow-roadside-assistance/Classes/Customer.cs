using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Customer : User
    {
        private String _regNo;
        public String RegNo
        {
            get { return _regNo; }
        }

        private String _make;
        public String Make
        {
            get { return _make; }
        }

        private String _model;
        public String Model
        {
            get { return _model; }
        }

        private String _color;
        public String Color
        {
            get { return _color; }
        }

        private String _cardHolder;
        public String CardHolder
        {
            get { return _cardHolder; }
        }

        private String _cardNo;
        public String CardNo
        {
            get { return _cardNo; }
        }

        private int _expMonth;
        public int ExpMonth
        {
            get { return _expMonth; }
        }

        private int _expYear;
        public int ExpYear
        {
            get { return _expYear; }
        }

        public String ExpiryDate
        {
            get { return full(_expMonth) + "/" + full(_expYear); }
        }

        private String full(int exp)
        {
            String res = exp.ToString();
            if (res.Length == 1)
                res = "0" + res;
            return res;
        }

        private int _CVV;
        public int CVV
        {
            get { return _CVV; }
        }

        private String _memberStatus;
        public String MemberStatus
        {
            get { return _memberStatus; }
        }

        private DateTime _subExpDate;
        public DateTime SubExpDate
        {
            get { return _subExpDate; }
        }

        public String SubExpDateToString
        {
            get { return _subExpDate.ToString("yyyy/MM/dd"); }
        }

        private Boolean _subCancelled;
        public Boolean SubCancelled
        {
            get { return _subCancelled; }
        }

        public Customer(int userID, String username, String email, String password, String userType, String fullName, String regNo, String make, String model, String color, String cardHolder, String cardNo, int expMonth, int expYear, int CVV) : base(userID, username, email, password, userType, fullName)
        {
            _regNo = regNo;
            _make = make;
            _model = model;
            _color = color;
            _cardHolder = cardHolder;
            _cardNo = cardNo;
            _expMonth = expMonth;
            _expYear = expYear;
            _CVV = CVV;
        }

        public Customer(int userID, String username, String email, String password, String userType, String fullName, String regNo, String make, String model, String color, String cardHolder, String cardNo, int expMonth, int expYear, int CVV, String memberStatus, DateTime subExpDate, Boolean subCancelled) : base(userID, username, email, password, userType, fullName)
        {
            _regNo = regNo;
            _make = make;
            _model = model;
            _color = color;
            _cardHolder = cardHolder;
            _cardNo = cardNo;
            _expMonth = expMonth;
            _expYear = expYear;
            _CVV = CVV;
            _memberStatus = memberStatus;
            _subExpDate = subExpDate;
            _subCancelled = subCancelled;
        }
    }
}