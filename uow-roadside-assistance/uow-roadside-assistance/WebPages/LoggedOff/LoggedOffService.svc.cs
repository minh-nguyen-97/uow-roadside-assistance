using System;
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

namespace uow_roadside_assistance.WebPages.LoggedOff
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoggedOffService
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
        public Boolean createNewCustomer(String username, String email, String password, String fullName, String regNo, String make, String model, String color, String cardHolder, String cardNo, String expDate, String cvv)
        {
            if (UserDBData.IsExist(username))
                return false;
            UserDBData.insertNewUser(username, email, password, "Customer", fullName);

            int userID = UserDBData.getUserIDFromName(username);

            int CVV = Convert.ToInt32(cvv);
            String[] mmyy = expDate.Split('/');
            int expMonth = Convert.ToInt32(mmyy[0]);
            int expYear = Convert.ToInt32(mmyy[1]);

            CustomerDBData.insertNewCustomer(userID, regNo, make, model, color, cardHolder, cardNo, expMonth, expYear, CVV);

            return true;
        }

        //
        [OperationContract]
        public String createNewContractor(String username, String email, String password, String fullName, String accountName, String accountNumber, String bsb)
        {
            if (UserDBData.IsExist(username))
            {
                return "Existed";
            }

            UserDBData.insertNewUser(username, email, password, "Contractor", fullName);

            int userID = UserDBData.getUserIDFromName(username);
            int BSB = Convert.ToInt32(bsb);

            ContractorDBData.insertNewContractor(userID, accountName, accountNumber, BSB);

            Boolean addressForNewContractor = AddressDBData.findContractorAddress(userID);

            if (addressForNewContractor == false)
            {
                UserDBData.deleteUserByUserID(userID);
                ContractorDBData.deleteContractorByUserID(userID);
                return "Overlimit";
            }

            return "OK";
        }

        //
        [OperationContract]
        public Boolean logIn(String username, String password)
        {
            Boolean check = UserDBData.IsExist(username, password);
            if (check)
            {
                setSession(username);
            }

            return check;
        }

        [OperationContract]
        public void setSession(String username)
        {
            User curUser = UserDBData.getUserByName(username);
            if (curUser.UserType.Equals("Customer"))
            {
                HttpContext.Current.Session["New"] = CustomerDBData.getCustomerByID(curUser.UserID);
            }
            else if (curUser.UserType.Equals("Contractor"))
            {
                HttpContext.Current.Session["New"] = ContractorDBData.getContractorByID(curUser.UserID);
            } else
            {
                HttpContext.Current.Session["New"] = UserDBData.getUserByID(curUser.UserID);
            }
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
                    curUser = (Customer)(HttpContext.Current.Session["New"]);
                } 
                else if (curUser.UserType.Equals("Contractor"))
                {
                    curUser = (Contractor)(HttpContext.Current.Session["New"]);
                }
                else // admin
                {
                    curUser = (User)(HttpContext.Current.Session["New"]);
                }
                return new JavaScriptSerializer().Serialize(curUser);
            }
        }
    }
}
