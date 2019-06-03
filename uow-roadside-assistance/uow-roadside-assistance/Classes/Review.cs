using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Review
    {
        private int _reviewID;
        public int ReviewID
        {
            get { return _reviewID; }
        }

        private String _reviewDesc;
        public String ReviewDesc
        {
            get { return _reviewDesc; }
        }

        private double _rating;
        public double Rating
        {
            get { return _rating; }
        }

        private int _transactionID;
        public int TransactionID
        {
            get { return _transactionID; }
        }

        private int _customerID;
        public int CustomerID
        {
            get { return _customerID; }
        }

        private int _contractorID;
        public int ContractorID
        {
            get { return _contractorID; }
        }

        private DateTime _reviewDate;
        public DateTime ReviewDate
        {
            get { return _reviewDate; }
        }

        private Boolean _appeal;
        public Boolean Appeal
        {
            get { return _appeal; }
        }

        private String _reason;
        public String Reason
        {
            get { return _reason; }
        }

        public Review(int reviewID, String reviewDesc, double rating, int transactionID, int customerID, int contractorID, DateTime reviewDate)
        {
            _reviewID = reviewID;
            _reviewDesc = reviewDesc;
            _rating = rating;
            _transactionID = transactionID;
            _customerID = customerID;
            _contractorID = contractorID;
            _reviewDate = reviewDate;
        }

        public Review(int reviewID, String reviewDesc, double rating, int transactionID, int customerID, int contractorID, DateTime reviewDate, Boolean appeal, String reason)
        {
            _reviewID = reviewID;
            _reviewDesc = reviewDesc;
            _rating = rating;
            _transactionID = transactionID;
            _customerID = customerID;
            _contractorID = contractorID;
            _reviewDate = reviewDate;
            _appeal = appeal;
            _reason = reason;
        }
    }
}