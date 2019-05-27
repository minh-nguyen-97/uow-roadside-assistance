using System;
using System.Collections;
using System.Collections.Generic;
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
using uow_roadside_assistance.Helper.Contractor;

namespace uow_roadside_assistance.WebPages.LoggedOn.Contractor
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContractorService
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
        public void UpdateContractorProfile(String email, String accountName, String accountNumber, String bsb)
        {
            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);
            int userID = curContractor.UserID;

            UserDBData.updateUserEmailByID(userID, email);

            int BSB = Convert.ToInt32(bsb);
            ContractorDBData.updateContractor(userID, accountName, accountNumber, BSB);

            HttpContext.Current.Session["New"] = ContractorDBData.getContractorByID(userID);
        }

        [OperationContract]
        public Boolean UpdateContractorPassword(String oldPass, String newPass)
        {
            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);

            if (curContractor.Password.Equals(oldPass))
            {
                int userID = curContractor.UserID;
                UserDBData.updateUserPasswordByID(userID, newPass);
                HttpContext.Current.Session["New"] = ContractorDBData.getContractorByID(userID);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Available Customer

        [OperationContract]
        public String getRequestedCustomers()
        {
            ArrayList result = new ArrayList();

            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);

            Address address = AddressDBData.getAddressByUserID(curContractor.UserID);
            String conLat = address.Latitude;
            String conLng = address.Longitude;

            ArrayList responses = ResponseDBData.getResponseListByContractorID(curContractor.UserID);
            foreach (Response response in responses)
            {
                int requestID = response.RequestID;
                String responseStatus = response.ResponseStatus;

                Request req = RequestDBData.getRequestByRequestID(response.RequestID);
                String cusLat = req.CustomerLatitude;
                String cusLng = req.CustomerLongitude;

                Classes.Customer customer = CustomerDBData.getCustomerByID(req.CustomerID);
                String fullName = customer.FullName;

                Helper.Contractor.NewWork requestedCustomer = new NewWork(fullName, cusLat, cusLng, conLat, conLng, responseStatus, requestID);

                result.Add(requestedCustomer);
            }

            return new JavaScriptSerializer().Serialize(result);
        }


        [OperationContract]
        public void declineRequest(int requestID)
        {
            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);
            ResponseDBData.declineResponse(requestID, curContractor.UserID);
        }

        [OperationContract]
        public void acceptRequest(int requestID)
        {
            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);
            ResponseDBData.acceptResponse(requestID, curContractor.UserID);
        }

        [OperationContract]
        public String getRequestDetails(int requestID)
        {
            Request req = RequestDBData.getRequestByRequestID(requestID);

            return new JavaScriptSerializer().Serialize(req);
        }

        [OperationContract]
        public String getCarDetailsOfCustomer(int customerID)
        {
            Classes.Customer customer = CustomerDBData.getCustomerByID(customerID);

            return new JavaScriptSerializer().Serialize(customer);
        }

        // Incomplete Transactions
        [OperationContract]
        public String getUnfinishedTransactions()
        {
            ArrayList result = new ArrayList();

            Classes.Contractor curContractor = (Classes.Contractor)(HttpContext.Current.Session["New"]);

            Address address = AddressDBData.getAddressByUserID(curContractor.UserID);
            String conLat = address.Latitude;
            String conLng = address.Longitude;
            
            ArrayList unfinishedTransactions = TransactionDBData.GetUnfinishedContractorTransactions(curContractor.UserID);
            foreach(Transaction transaction in unfinishedTransactions)
            {
                int transactionID = transaction.TransactionID;
                
                String cusLat = transaction.CustomerLatitude;
                String cusLng = transaction.CustomerLongitude;

                Classes.Customer customer = CustomerDBData.getCustomerByID(transaction.CustomerID);
                String fullName = customer.FullName;

                Helper.Contractor.IncompleteWork incompleteWork = new IncompleteWork(fullName, cusLat, cusLng, conLat, conLng, transactionID);

                result.Add(incompleteWork);
            }

            return new JavaScriptSerializer().Serialize(result);
        }
       

        [OperationContract]
        public void finishedContractor(int transactionID)
        {
            TransactionDBData.contractorFinishedTransaction(transactionID);
        }
        
        [OperationContract]
        public String getTransactionDetails(int transactionID)
        {
            Transaction transaction = TransactionDBData.GetTransactionByID(transactionID);

            return new JavaScriptSerializer().Serialize(transaction);
        }
    }

}
