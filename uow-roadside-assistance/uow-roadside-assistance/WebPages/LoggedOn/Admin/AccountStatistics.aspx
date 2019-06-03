<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountStatistics.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Admin.AccountStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Account Statistics</title>
    
     <%-- jquery and jquery ui --%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <%-- Bootstrap 4 --%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <%--Font Awesome--%>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    
    <%--Page CSS--%>
    <link rel="stylesheet" href="../../../Css/LoggedOn/Admin/AdminNavBar.css" />
    <link rel="stylesheet" href="../../../Css/LoggedOn/Admin/AccountStatistics.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/chart.js@2.8.0"></script>

    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/OnLoadAdmin.js"></script>
    


</head>
<body>

    <%-- Service --%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
               <asp:ServiceReference Path="~/WebPages/LoggedOn/Admin/AdminService.svc" />
            </Services>
        </asp:ScriptManager>
    </form>

    <%-- Navigation Bar --%>
    <div id="verticalNavBar" class="bg-dark">
        <ul class="nav flex-column">
            <br />
            <li class="nav-item">
                <a class="navbar-brand" href="AdminHomepage.aspx">
                    <div class="container">
                        <div class="row">
                            <div class="col">
                                <img src="../../../Images/official_logo.gif" width="60" height="60"/>
                            </div>
                            <div class="col" style="padding:0;">
                                <span id="logoText">Daedalus</span> <br />
                                <span id="adminText">ADMIN</span>
                            </div>
                        </div>
                    </div>
                    
                </a>
            </li>
            <br />
            <li class="nav-item">
                <a class="nav-link navBarElement" href="AdminHomepage.aspx">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-home"></i> 
                            </div>
                            <div class="col">
                                Dashboard
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link navBarElement" href="AllTransactions.aspx">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-credit-card"></i>
                            </div>
                            <div class="col">
                                All Transactions
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link navBarElement" href="#">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-users-cog"></i>
                            </div>
                            <div class="col">
                                Manage Accounts
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link active navBarElement" href="AccountStatistics.aspx">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="far fa-chart-bar"></i>
                            </div>
                            <div class="col">
                                Account Statistics
                            </div>
                        </div>
                    </div>
                </a>
            </li>

            <li class="nav-item">
                <a class="nav-link navBarElement" href="AppealReviews.aspx">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-star"></i>
                            </div>
                            <div class="col">
                                Appeal Reviews
                            </div>
                        </div>
                    </div>
                </a>
            </li>

            <li class="nav-item">
                <a id="logOut" class="nav-link navBarElement" href="#">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-sign-out-alt"></i>
                            </div>
                            <div class="col">
                                Logout
                            </div>
                        </div>
                    </div>
                </a>
            </li>
        </ul>
    </div>


    <div id="content">
        <%-- Page Content --%>
        
        <div class="container" style=" width: 50%">
            <div class="row">
                <div class="col text-right">
                    <span style="font-size:30px; font-weight:500; line-height: 30px">USER ID : </span>
                </div>
                <div class="col">
                    <input id="UserID" type="text" class="form-control"/>
                    <span id="UserIDErrMess" style="display:inline-block; color:red;"></span>
                </div>
                <div class="col">
                    <i id="searchButton" class="fas fa-search fa-2x"></i>
                </div>
            </div>
            
        </div>

        <br />
        <br />
        <div class="container">
            <div class="row">
                <div class="col-5">
                    <div class="card">
                        <div class="card-header">
                        User Profile
                        </div>
                        <div class="card-body">
                            <div class="container">
                                <div class="row">
                                    <div class="col-4">
                                        <i class="fas fa-user-circle fa-6x" style="color:cornflowerblue"></i> <br />
                                        <div id="UserType">Contractor</div>
                                    </div>
                                    <div class="col">
                                        <div id="FullName" class="userInfo">Full Name</div>
                                        <div id="Username" class="userInfo">Username</div>
                                        <div id="Email" class="userInfo">Email</div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <ul id="customerDetails" class="list-group list-group-flush">
                            <li class="list-group-item">
                                <span class="infoLabel">Registration</span>
                                <span id="RegNo" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Make</span>
                                <span id="Make" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Model</span>
                                <span id="Model" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Color</span>
                                <span id="Color" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Card Holder</span>
                                <span id="CardHolder" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Card Number</span>
                                <span id="CardNumber" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Expiry Date</span>
                                <span id="ExpiryDate" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">CVV</span>
                                <span id="CVV" class="infoValue"></span>
                            </li>
                        </ul>

                        <ul id="contractorDetails" class="list-group list-group-flush">
                            <li class="list-group-item">
                                <span class="infoLabel">Account Name</span>
                                <span id="AccountName" class="infoValue">Minh Nguyen</span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Account Number</span>
                                <span id="AccountNumber" class="infoValue">0213123123</span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">BSB</span>
                                <span id="BSB" class="infoValue">23123</span>
                            </li>
                        </ul>
                    </div>

                    <%-- Description --%>
                    <br /> <br />
                    <div class="card">
                        <div class="card-header">
                        Problems
                        </div>

                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <span class="infoLabel">Tyre Problem</span>
                                <span id="TyreProblem" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Car Battery Problem</span>
                                <span id="CarBatteryProblem" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Engine Problem</span>
                                <span id="EngineProblem" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">General Problem</span>
                                <span id="GeneralProblem" class="infoValue"></span>
                            </li>
                        </ul>

                        <div class="card-body" style="padding-top: 50px">
                            <div class="chart-container" style="position: relative; height:360px; width:360px">
                                <canvas id="problemsBarChart" width="200" height="200"></canvas>
                            </div>
                            <br /><br />
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card">
                        <div class="card-header">
                        Ratings
                        </div>

                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <span class="infoLabel">Completed Transactions</span>
                                <span id="TotalTransactions" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Good Rating (greater than 3 <i class="fas fa-star" style="color: yellowgreen"></i> )</span>
                                <span id="GoodRating" class="infoValue"></span>
                            </li>
                        </ul>
                        
                        <div class="card-body" >
                            <div class="chart-container" style="position: relative; height:300px; width:300px; margin-left: 150px">
                                <canvas id="ratingDoughnutChart" width="200" height="200"></canvas>
                            </div>
                        </div>
                    </div>

                    <br /> <br />
                    <div class="card">
                        <div class="card-header">
                        <span id="6MonthsTransactions"></span> transactions in the last 6 months
                        </div>
                        <div class="card-body">
                            <div class="chart-container" >
                                <canvas id="transactionLineChart" width="200" height="200"></canvas>
                            </div>
                        </div>
                    </div>
                    
                </div>

            </div>
            <br /><br />
        </div>
        <%-- End of Page Content --%>
    </div>

    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AccountStatistics/DoughnutRating.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AccountStatistics/ProblemBarChart.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AccountStatistics/TransactionLineChart.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AccountStatistics.js"></script>
    
</body>
</html>





