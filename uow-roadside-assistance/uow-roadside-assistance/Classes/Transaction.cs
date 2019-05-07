using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Transaction
    {
        private int _transactionID;
        public int TransactionID
        {
            get { return _transactionID; }
        }

        private int _contractorID;
        public int ContractorID
        {
            get { return _contractorID; }
        }

        private int _customerID;
        public int CustomerID
        {
            get { return _customerID; }
        }

        private double _cost;
        public double Cost
        {
            get { return _cost; }
        }

        private Boolean _contractorFinished;
        public Boolean ContractorFinished
        {
            get { return _contractorFinished; }
        }

        private Boolean _customerFinished;
        public Boolean CustomerFinished
        {
            get { return _customerFinished; }
        }

        public Transaction(int transactionID, int contractorID, int customerID, double cost, Boolean contractorFinished, Boolean customerFinished)
        {
            _transactionID = transactionID;
            _contractorID = contractorID;
            _customerID = customerID;
            _cost = cost;
            _contractorFinished = contractorFinished;
            _customerFinished = customerFinished;
        }
    }
}