using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Contractor
{
    public class NewWork : Work
    {
        private int _requestID;
        public int RequestID
        {
            get { return _requestID; }
        }

        private String _responseStatus;
        public String ResponseStatus
        {
            get { return _responseStatus; }
        }

        public NewWork(String fullName, String cusLat, String cusLng, String conLat, String conLng, String responseStatus, int requestID) : base(fullName, cusLat, cusLng, conLat, conLng)
        {
            _requestID = requestID;
            _responseStatus = responseStatus;
        }
    }
}