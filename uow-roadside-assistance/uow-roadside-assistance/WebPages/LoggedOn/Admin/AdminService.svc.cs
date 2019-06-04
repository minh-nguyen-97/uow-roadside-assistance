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

namespace uow_roadside_assistance.WebPages.LoggedOn.Admin
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AdminService
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

        // All Transactions
        [OperationContract]
        public String getAllTransactions()
        {
            ArrayList result = TransactionDBData.GetCompletedTransactions();
            return new JavaScriptSerializer().Serialize(result);
        }

        // Account Statistics
        [OperationContract] 
        public Boolean IsExist(int userID)
        {
            User user = UserDBData.getUserByID(userID);
            if (user != null)
            {
                if (user.UserType.Equals("Admin"))
                {
                    return false;
                }
                return true;
            }

            return false;

        }

        //
        [OperationContract]
        public String GetUser(int userID)
        {
            User user = UserDBData.getUserByID(userID);

            if (user.UserType.Equals("Contractor"))
            {
                user = ContractorDBData.getContractorByID(userID);
            }
            else
            {
                user = CustomerDBData.getCustomerByID(userID);
            }

            return new JavaScriptSerializer().Serialize(user);
        }

        // Statistics
        [OperationContract]
        public String GetRatingsStats(int userID)
        {
            User user = UserDBData.getUserByID(userID);

            ArrayList reviews;
            if (user.UserType.Equals("Contractor"))
            {
                reviews = ReviewDBData.getReviewsByContractorID(userID);
            }
            else
            {
                reviews = ReviewDBData.getReviewsByCustomerID(userID);
            }

            ArrayList result = new ArrayList();

            result.Add(reviews.Count);
            int goodRating = 0;
            foreach(Review review in reviews)
            {
                if (review.Rating > 3)
                {
                    goodRating++;
                }
            }

            result.Add(goodRating);

            return new JavaScriptSerializer().Serialize(result);
        }

        [OperationContract]
        public ArrayList GetCompletedTransactions(int userID)
        {
            User user = UserDBData.getUserByID(userID);

            ArrayList transactions;
            if (user.UserType.Equals("Contractor"))
            {
                transactions = TransactionDBData.GetCompletedTransactionsByContractorID(userID);
            }
            else
            {
                transactions = TransactionDBData.GetCompletedTransactionsByCustomerID(userID);
            }

            return transactions;
        }

        [OperationContract]
        public String GetProblemsStats(int userID)
        {
            ArrayList transactions = GetCompletedTransactions(userID);

            int tyre = 0;
            int battery = 0;
            int engine = 0;
            int general = 0;

            foreach (Transaction transaction in transactions)
            {
                if (transaction.TyreProblem)
                    tyre++;

                if (transaction.CarBatteryProblem)
                    battery++;

                if (transaction.EngineProblem)
                    engine++;

                if (transaction.GeneralProblem)
                    general++;
            }

            ArrayList res = new ArrayList();
            res.Add(tyre);
            res.Add(battery);
            res.Add(engine);
            res.Add(general);

            return new JavaScriptSerializer().Serialize(res);
        }

        [OperationContract]
        public String GetTransactionsStats(int userID)
        {
            return new JavaScriptSerializer().Serialize(GetCompletedTransactions(userID));
        }


        // Appeal Review
        [OperationContract]
        public String getReviewAndRating(int transactionID)
        {
            Review review = ReviewDBData.getReviewsByTransactionID(transactionID);

            return new JavaScriptSerializer().Serialize(review);
        }

        [OperationContract]
        public String getAppealedReviews()
        {
            ArrayList appealedReviews = ReviewDBData.getAppealedReviews();
            return new JavaScriptSerializer().Serialize(appealedReviews);
        }

        [OperationContract]
        public void rejectAppeal(int transactionID)
        {
            ReviewDBData.updateAppealAndReason(transactionID, false, "");
        }

        [OperationContract]
        public void acceptAppeal(int transactionID)
        {
            ReviewDBData.deleteReviewByTransactionID(transactionID);
        }

        // Homepage Report
        [OperationContract]
        public String GetRatingsStatsReport()
        {
            ArrayList res = new ArrayList();

            ArrayList completedTransactions = TransactionDBData.GetCompletedTransactions();
            res.Add(completedTransactions.Count);

            res.Add(ReviewDBData.getTotalNumberOfReviews());
            res.Add(ReviewDBData.getNumberOfGoodRatings());

            return new JavaScriptSerializer().Serialize(res);
        }

        [OperationContract]
        public String GetAllRatings()
        {
            ArrayList allReviews = ReviewDBData.getAllReviews();
            return new JavaScriptSerializer().Serialize(allReviews);
        }

    }
}
