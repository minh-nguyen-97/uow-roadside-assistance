using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Contractor
{
    public class IncompleteWork : Work
    {
        private int _transactionID;
        public int TransactionID
        {
            get { return _transactionID; }
        }
        public IncompleteWork(String fullName, String cusLat, String cusLng, String conLat, String conLng, int transactionID) : base(fullName, cusLat, cusLng, conLat, conLng)
        {
            _transactionID = transactionID;
        }
    }
}