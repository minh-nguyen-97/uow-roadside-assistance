using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Contractor
{
    public class ContractorCompletedTransaction
    {
        private String _customerFullName;
        public String CustomerFullName
        {
            get { return _customerFullName; }
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

        public ContractorCompletedTransaction(String customerFullName, double cost, DateTime transactionDate, int transactionID)
        {
            _customerFullName = customerFullName;
            _cost = cost;
            _transactionDate = transactionDate;
            _transactionID = transactionID;
        }
    }
}