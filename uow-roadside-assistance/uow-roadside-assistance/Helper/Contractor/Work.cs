using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Contractor
{
    public class Work
    {
        private String _fullName;
        public String FullName
        {
            get { return _fullName; }
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

        public Work(String fullName, String cusLat, String cusLng, String conLat, String conLng)
        {
            _fullName = fullName;
            _cusLat = cusLat;
            _cusLng = cusLng;
            _conLat = conLat;
            _conLng = conLng;
        }
    }
}