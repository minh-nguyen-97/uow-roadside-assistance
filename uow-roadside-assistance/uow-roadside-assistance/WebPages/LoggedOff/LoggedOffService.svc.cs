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
        public Boolean createNewCustomer(String username, String email, String password, String regNo, String make, String model, String color, String cardHolder, String cardNo, String expDate, String cvv)
        {
            if (UserDBData.IsExist(username))
                return false;
            UserDBData.insertNewUser(username, email, password, "Customer");

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
        public Boolean createNewContractor(String username, String email, String password, String accountName, String accountNumber, String bsb)
        {
            if (UserDBData.IsExist(username))
            {
                return false;
            }

            UserDBData.insertNewUser(username, email, password, "Contractor");

            int userID = UserDBData.getUserIDFromName(username);
            int BSB = Convert.ToInt32(bsb);

            ContractorDBData.insertNewContractor(userID, accountName, accountNumber, BSB);

            return true;
        }

        //
        [OperationContract]
        public Boolean logIn(String username, String password)
        {
            return UserDBData.IsExist(username, password);
        }

        [OperationContract]
        public void setSession(String username)
        {
            HttpContext.Current.Session["New"] = username;
        }

        [OperationContract]
        public String getSession()
        {
            return (String) HttpContext.Current.Session["New"];
        }

        [OperationContract]
        public String getUserTypeFromSession()
        {
            String username = (String)HttpContext.Current.Session["New"];
            User user = UserDBData.getUserByName(username);
            return user.UserType;
        }
    }
}
