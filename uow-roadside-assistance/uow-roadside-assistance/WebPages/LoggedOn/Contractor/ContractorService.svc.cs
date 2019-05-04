using System;
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
                else
                {
                    curUser = (uow_roadside_assistance.Classes.Contractor)(HttpContext.Current.Session["New"]);
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
            Classes.Contractor curCust = (Classes.Contractor)(HttpContext.Current.Session["New"]);

            if (curCust.Password.Equals(oldPass))
            {
                int userID = curCust.UserID;
                UserDBData.updateUserPasswordByID(userID, newPass);
                HttpContext.Current.Session["New"] = ContractorDBData.getContractorByID(userID);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
