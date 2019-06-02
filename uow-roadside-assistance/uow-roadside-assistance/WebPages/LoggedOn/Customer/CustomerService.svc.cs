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
using uow_roadside_assistance.Helper.Customer;

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
                else if (curUser.UserType.Equals("Contractor"))
                {
                    curUser = (uow_roadside_assistance.Classes.Contractor)(HttpContext.Current.Session["New"]);
                }
                else
                {
                    curUser = (uow_roadside_assistance.Classes.User)(HttpContext.Current.Session["New"]);
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
        public String getAvailableContractors()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);
            if (RequestDBData.IsExist(customer.UserID))
            {
                Request req = RequestDBData.getRequestByCustomerID(customer.UserID);
                String cusLat = req.CustomerLatitude;
                String cusLng = req.CustomerLongitude;

                ArrayList result = new ArrayList();

                ArrayList responses = ResponseDBData.getResponseList(req.RequestID);

                foreach(Response response in responses)
                {
                    int contractorID = response.ContractorID;

                    Classes.Contractor contractor = ContractorDBData.getContractorByID(contractorID);
                    String contractorFullName = contractor.FullName;
                    
                    Address contractorAddress = AddressDBData.getAddressByUserID(contractorID);
                    String conLat = contractorAddress.Latitude;
                    String conLng = contractorAddress.Longitude;

                    String responseStatus = response.ResponseStatus;

                    double averageRating = ReviewDBData.getAverageRatingByContractorID(contractorID);

                    Helper.Customer.AvailableContractor availableContractor
                        = new AvailableContractor(contractorID, contractorFullName, conLat, conLng, cusLat, cusLng, responseStatus, averageRating);

                    result.Add(availableContractor);
                }

                return new JavaScriptSerializer().Serialize(result);
            }
            return null;
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

                Helper.Customer.ReviewDetails extractedData = new ReviewDetails(customerFullName, reviewDesc);

                result.Add(extractedData);
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        /* Membership */

        // Monthly
        [OperationContract]
        public void subscribeMonthly()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            CustomerDBData.updateMemberStatus(customer.UserID, "monthly");

            DateTime expDate = DateTime.Now.AddMonths(1);
            CustomerDBData.updateSubscriptionExpireDate(customer.UserID, expDate);

            CustomerDBData.updateSubscriptionCancellation(customer.UserID, false);

            HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(customer.UserID);
        }

        [OperationContract]
        public void cancelMonthly()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            CustomerDBData.updateSubscriptionCancellation(customer.UserID, true);

            HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(customer.UserID);
        }


        // Yearly
        [OperationContract]
        public void subscribeYearly()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            CustomerDBData.updateMemberStatus(customer.UserID, "yearly");

            DateTime expDate = DateTime.Now.AddYears(1);
            CustomerDBData.updateSubscriptionExpireDate(customer.UserID, expDate);

            CustomerDBData.updateSubscriptionCancellation(customer.UserID, false);

            HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(customer.UserID);
        }

        [OperationContract]
        public void cancelYearly()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            CustomerDBData.updateSubscriptionCancellation(customer.UserID, true);

            HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(customer.UserID);
        }

        /* Past Transactions */
        [OperationContract]
        public String getAllPastCompletedTransactions()
        {
            Classes.Customer customer = (Classes.Customer)(HttpContext.Current.Session["New"]);

            ArrayList transactions = TransactionDBData.GetCompletedTransactionsByCustomerID(customer.UserID);

            ArrayList result = new ArrayList();

            foreach(Transaction transaction in transactions)
            {
                Classes.Contractor contractor = ContractorDBData.getContractorByID(transaction.ContractorID);

                Helper.Customer.CustomerCompletedTransaction completedTransaction = new CustomerCompletedTransaction(contractor.FullName, transaction.Cost, transaction.TransactionDate, transaction.TransactionID);

                result.Add(completedTransaction);
            }

            return new JavaScriptSerializer().Serialize(result);
        }

        [OperationContract]
        public String getReviewAndRating(int transactionID)
        {
            Review review = ReviewDBData.getReviewsByTransactionID(transactionID);

            return new JavaScriptSerializer().Serialize(review);
        }

        [OperationContract]
        public void updateReviewAndRating(int transactionID, String reviewDesc, double rating)
        {
            ReviewDBData.updateReviewAndRating(transactionID, reviewDesc, rating);
        }

    }
}
