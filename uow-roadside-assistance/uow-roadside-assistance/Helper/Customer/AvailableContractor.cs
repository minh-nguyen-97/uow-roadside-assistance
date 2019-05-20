using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Customer
{
    public class AvailableContractor
    {
        private String _contractorFullName;
        public String ContractorFullName
        {
            get { return _contractorFullName; }
        }

        private int _contractorID;
        public int ContractorID
        {
            get { return _contractorID; }
        }

        private String _conLat;
        public String ConLat
        {
            get { return _conLat; }
        }

        private String _conLng;
        public String ConLng
        {
            get { return _conLng; }
        }

        private String _cusLat;
        public String CusLat
        {
            get { return _cusLat; }
        }

        private String _cusLng;
        public String CusLng
        {
            get { return _cusLng; }
        }

        private String _responsesStatus;
        public String ResponseStatus
        {
            get { return _responsesStatus; }
        }

        private double _averageRating;
        public double AverageRating
        {
            get { return _averageRating; }
        }

        public AvailableContractor(int contractorID, String contractorFullName, String conLat, String conLng, String cusLat, String cusLng, String responseStatus, double averageRating)
        {
            _contractorID = contractorID;
            _contractorFullName = contractorFullName;
            _conLat = conLat;
            _conLng = conLng;
            _cusLat = cusLat;
            _cusLng = cusLng;
            _responsesStatus = responseStatus;
            _averageRating = averageRating;
        }
    }
}