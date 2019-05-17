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

        private Boolean _tyreProblem;
        public Boolean TyreProblem
        {
            get { return _tyreProblem; }
        }

        private Boolean _carBatteryProblem;
        public Boolean CarBatteryProblem
        {
            get { return _carBatteryProblem; }
        }

        private Boolean _engineProblem;
        public Boolean EngineProblem
        {
            get { return _engineProblem; }
        }

        private Boolean _generalProblem;
        public Boolean GeneralProblem
        {
            get { return _generalProblem; }
        }

        private String _problemDescription;
        public String ProblemDescription
        {
            get { return _problemDescription; }
        }

        private String _customerLatitude;
        public String CustomerLatitude
        {
            get { return _customerLatitude; }
        }

        private String _customerLongitude;
        public String CustomerLongitude
        {
            get { return _customerLongitude; }
        }

        private DateTime _transactionDate;
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
        }

        public Transaction(int transactionID, int contractorID, int customerID, double cost, Boolean contractorFinished, Boolean customerFinished, Boolean tyreProblem, Boolean carBatterProblem, Boolean engineProblem, Boolean generalProblem, String problemDescription, String customerLatitude, String customerLongitude, DateTime transactionDate)
        {
            _transactionID = transactionID;
            _contractorID = contractorID;
            _customerID = customerID;
            _cost = cost;
            _contractorFinished = contractorFinished;
            _customerFinished = customerFinished;

            _tyreProblem = tyreProblem;
            _carBatteryProblem = carBatterProblem;
            _engineProblem = engineProblem;
            _generalProblem = generalProblem;

            _problemDescription = problemDescription;
            _customerLatitude = customerLatitude;
            _customerLongitude = customerLongitude;
            _transactionDate = transactionDate;
        }
    }
}