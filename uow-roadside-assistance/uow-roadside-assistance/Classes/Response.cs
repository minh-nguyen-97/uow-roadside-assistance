using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Classes
{
    public class Response
    {
        private int _responseID;
        public int ResponseID
        {
            get { return _responseID; }
        }

        private int _requestID;
        public int RequestID
        {
            get { return _requestID; }
        }

        private int _contractorID;
        public int ContractorID
        {
            get { return _contractorID; }
        }

        private String _responseStatus;
        public String ResponseStatus
        {
            get { return _responseStatus; }
        }

        public Response(int responseID, int requestID, int contractorID, String responseStatus)
        {
            _responseID = responseID;
            _requestID = requestID;
            _contractorID = contractorID;
            _responseStatus = responseStatus;
        }
    }
}