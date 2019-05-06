using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Request
    {
        private int _requestID;
        public int RequestID
        {
            get { return _requestID; }
        }

        private int _customerID;
        public int CustomerID
        {
            get { return _customerID; }
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

        private String _requestStatus;
        public String RequestStatus
        {
            get { return _requestStatus; }
        }

        private DateTime _requestDate;
        public DateTime RequestDate
        {
            get { return _requestDate; }
        }

        public Request(int requestID, int customerID, Boolean tyreProblem, Boolean carBatterProblem, Boolean engineProblem, Boolean generalProblem, String problemDescription, String customerLatitude, String customerLongitude, String requestStatus, DateTime requestDate)
        {
            _requestID = requestID;
            _customerID = customerID;

            _tyreProblem = tyreProblem;
            _carBatteryProblem = carBatterProblem;
            _engineProblem = engineProblem;
            _generalProblem = generalProblem;

            _problemDescription = problemDescription;
            _customerLatitude = customerLatitude;
            _customerLongitude = customerLongitude;
            _requestStatus = requestStatus;
            _requestDate = requestDate;
        }
    }
}