using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uow_roadside_assistance.Helper.Customer
{
    public class ReviewDetails
    {
        private String _customerFullName;
        public String CustomerFullName
        {
            get { return _customerFullName; }
        }

        private String _reviewDesc;
        public String ReviewDesc
        {
            get { return _reviewDesc; }
        }

        public ReviewDetails(String customerFullName, String reviewDesc)
        {
            _customerFullName = customerFullName;
            _reviewDesc = reviewDesc;
        }
    }
}