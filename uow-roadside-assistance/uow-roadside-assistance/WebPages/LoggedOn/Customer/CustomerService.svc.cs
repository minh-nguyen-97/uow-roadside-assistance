using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using uow_roadside_assistance.Classes;
using uow_roadside_assistance.DBData;

namespace uow_roadside_assistance.WebPages.LoggedOn.Customer
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CustomerService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]
        [OperationContract]
        public void logOut()
        {
            HttpContext.Current.Session["New"] = null;
        }

        [OperationContract]
        public String getUserFromSession()
        {
            if (HttpContext.Current.Session["New"] == null)
                return null;
            else
            {
                User curUser = (User)HttpContext.Current.Session["New"];
                if (curUser.UserType.Equals("Customer"))
                {
                    curUser = (uow_roadside_assistance.Classes.Customer)(HttpContext.Current.Session["New"]);
                }
                else
                {
                    curUser = (uow_roadside_assistance.Classes.Contractor)(HttpContext.Current.Session["New"]);
                }
                return new JavaScriptSerializer().Serialize(curUser);
            }
        }

        [OperationContract]
        public void UpdateCustomerProfile(String email, String regNo, String make, String model, String color, String cardHolder, String cardNo, String expDate, String cvv)
        {
            uow_roadside_assistance.Classes.Customer curCust = (uow_roadside_assistance.Classes.Customer)(HttpContext.Current.Session["New"]);
            int userID = curCust.UserID;

            UserDBData.updateUserEmailByID(userID, email);

            int CVV = Convert.ToInt32(cvv);
            String[] mmyy = expDate.Split('/');
            int expMonth = Convert.ToInt32(mmyy[0]);
            int expYear = Convert.ToInt32(mmyy[1]);

            CustomerDBData.updateCustomer(userID, regNo, make, model, color, cardHolder, cardNo, expMonth, expYear, CVV);

            HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(userID);
        }

        [OperationContract]
        public Boolean UpdateCustomerPassword(String oldPass, String newPass)
        {
            uow_roadside_assistance.Classes.Customer curCust = (uow_roadside_assistance.Classes.Customer)(HttpContext.Current.Session["New"]);

            if (curCust.Password.Equals(oldPass))
            {
                int userID = curCust.UserID;
                UserDBData.updateUserPasswordByID(userID, newPass);
                HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(userID);
                return true;
            }
            else
            {
                return false;
            }
        }

        [OperationContract]
        public String getAllContractorsAddresses()
        {
            ArrayList contractorsAddresses = AddressDBData.getAddressByUserType("Contractor");
            return new JavaScriptSerializer().Serialize(contractorsAddresses);
        }

        [OperationContract]
        public void MakeRequest(Boolean tyreProblem, Boolean carBatteryProblem, Boolean engineProblem, Boolean generalProblem, String problemDescription, String customerLatitude, String customerLongitude, int[] contractorIDs)
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            int customerID = customer.UserID;

            RequestDBData.insertNewRequest(customerID, tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude);


            Request req = RequestDBData.getRequestByCustomerID(customerID);
            int requestID = req.RequestID;

            // Insert query for RESPONSES

            for (int i = 0; i < contractorIDs.Length; i++)
            {
                ResponseDBData.insertNewResponse(requestID, contractorIDs[i], "Waiting");
            }
        }


        [OperationContract]
        public String getSessionRequest()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (RequestDBData.IsExist(customer.UserID))
            {
                Request req = RequestDBData.getRequestByCustomerID(customer.UserID);
                return new JavaScriptSerializer().Serialize(req);
            }
            return null;
        }

        /* Browse Requested Contractors */
        [OperationContract]
        public void cancelRequest()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (RequestDBData.IsExist(customer.UserID))
            {
                Request req = RequestDBData.getRequestByCustomerID(customer.UserID);
                RequestDBData.deleteRequest(req.RequestID);
                ResponseDBData.deleteResponse(req.RequestID);
            }
        }

        [OperationContract]
        public String getContratorsResponses()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (RequestDBData.IsExist(customer.UserID))
            {
                Request req = RequestDBData.getRequestByCustomerID(customer.UserID);
                ArrayList responses = ResponseDBData.getResponseList(req.RequestID);
                return new JavaScriptSerializer().Serialize(responses);
            }
            return null;
        }

        [OperationContract]
        public String extractContractorData(int requestID, int contractorID, String responseStatus)
        {
            Classes.Contractor contractor = ContractorDBData.getContractorByID(contractorID);
            ArrayList res = new ArrayList();
            res.Add(contractor.FullName);

            Address contractorAddress = AddressDBData.getAddressByUserID(contractorID);
            res.Add(contractorAddress.Latitude);
            res.Add(contractorAddress.Longitude);

            Request req = RequestDBData.getRequestByRequestID(requestID);
            res.Add(req.CustomerLatitude);
            res.Add(req.CustomerLongitude);

            res.Add(responseStatus);
            res.Add(contractorID);

            double rating = ReviewDBData.getAverageRatingByContractorID(contractorID);
            res.Add(rating);

            return new JavaScriptSerializer().Serialize(res);
        }

        [OperationContract]
        public void acceptPayment(int contractorID, double cost)
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (RequestDBData.IsExist(customer.UserID))
            {
                Request req = RequestDBData.getRequestByCustomerID(customer.UserID);
                RequestDBData.deleteRequest(req.RequestID);
                ResponseDBData.deleteResponse(req.RequestID);

                TransactionDBData.insertNewTransaction(contractorID, cost, req);
            }
        }

        /* In Progress Transaction */

        [OperationContract]
        public String getCustomerUnfinishedTransaction()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (TransactionDBData.IsExistUnfinishedCustomerTransaction(customer.UserID))
            {
                Transaction transaction = TransactionDBData.GetUnfinishedCustomerTransaction(customer.UserID);
                return new JavaScriptSerializer().Serialize(transaction);
            }
            return null;
        }

        [OperationContract]
        public void customerFinishedTransaction()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (TransactionDBData.IsExistUnfinishedCustomerTransaction(customer.UserID))
            {
                Transaction transaction = TransactionDBData.GetUnfinishedCustomerTransaction(customer.UserID);
                TransactionDBData.customerFinishedTransaction(transaction.TransactionID);
            }
        }

        /* Redirect from Homepage */

        [OperationContract] 
        public String getTheRightRedirect()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            String URL = null;
            if (TransactionDBData.IsExistUnfinishedCustomerTransaction(customer.UserID))
            {
                URL = "InProgressTransaction.aspx";
            }
            else if (RequestDBData.IsExist(customer.UserID))
            {
                URL = "BrowseAvailable.aspx";
            }
            else
            {
                URL = "MakeRequest.aspx";
            }

            return URL;
        }

        /* Rating */
        [OperationContract]
        public void reviewAndRating(String reviewDesc, double rating)
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            Transaction transaction = TransactionDBData.GetUnfinishedCustomerTransaction(customer.UserID);
            ReviewDBData.insertNewReview(reviewDesc, rating, transaction.TransactionID);
        }

        /* get reviews */

        public class ReviewExtractedData
        {
            public String CustomerFullName;
            public String ReviewDesc;

            public ReviewExtractedData (String customerFullName, String reviewDesc)
            {
                CustomerFullName = customerFullName;
                ReviewDesc = reviewDesc;
            }


        }

        [OperationContract] 
        public String getReviews(int contractorID)
        {
            ArrayList reviews = ReviewDBData.getReviewsByContractorID(contractorID);

            ArrayList result = new ArrayList();

            foreach(Review review in reviews)
            {
                Classes.Customer customer = CustomerDBData.getCustomerByID(review.CustomerID);
                String customerFullName = customer.FullName;

                String reviewDesc = review.ReviewDesc;

                ReviewExtractedData extractedData = new ReviewExtractedData(customerFullName, reviewDesc);

                result.Add(extractedData);
            }

            return new JavaScriptSerializer().Serialize(result);
        }
        
    }
}
