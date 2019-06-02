using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Customer
{
    public class CustomerCompletedTransaction
    {
        private String _contractorFullName;
        public String ContractorFullName
        {
            get { return _contractorFullName; }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
        }

        private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
        }

        public String TransactionDateToString
        {
            get { return _transactionDate.ToString("yyyy/MM/dd HH:mm:ss"); }
        }

        private int _transactionID;
        public int TransactionID
        {
            get { return _transactionID; }
        }

        public CustomerCompletedTransaction(String contractorFullName, double cost, DateTime transactionDate, int transactionID)
        {
            _contractorFullName = contractorFullName;
            _cost = cost;
            _transactionDate = transactionDate;
            _transactionID = transactionID;
        }
    }
}