<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHomepage.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Admin.AdminHomepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Admin Homepage</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Admin/AdminHomepage.css" />

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
                <a class="nav-link active navBarElement" href="AdminHomepage.aspx">
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
                <a class="nav-link navBarElement" href="ManageContractors.aspx">
                    <div class="selectedLi bg-dark">&nbsp;</div>
                    <div class="container">
                        <div class="row">
                            <div class="col-2">
                                <i class="fas fa-users-cog"></i>
                            </div>
                            <div class="col">
                                Manage Contractors
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="nav-item">
                <a class="nav-link navBarElement" href="AccountStatistics.aspx">
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
                <a  id="logOut" class="nav-link navBarElement" href="#">
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
        
        <div class="container">
            <div class="row">
                <div class="col">
                    <div class="card">
                        <div class="card-header">
                            <span id="6MonthsTransactions"></span> transactions over last 6 months
                        </div>
                        <div class="card-body">
                            <div class="chart-container" style="position: relative; height:400px; width:400px; margin-left:50px">
                                <canvas id="transactionBarChart" width="300" height="300"></canvas>
                            </div>
                        </div>
                    </div>

                    <br /> <br />
                    <div class="card">
                        <div class="card-header">
                            Completed Transactions
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item">
                                <span class="infoLabel">Total Completed Transactions</span>
                                <span id="TotalTransactions" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Rated Transactions</span>
                                <span id="RatedTransactions" class="infoValue"></span>
                            </li>
                            <li class="list-group-item">
                                <span class="infoLabel">Good Rating (greater than 3 <i class="fas fa-star" style="color: yellowgreen"></i> )</span>
                                <span id="GoodRating" class="infoValue"></span>
                            </li>
                        </ul>
                        
                        <div class="card-body" >
                            <div class="chart-container" style="position: relative; height:400px; width:400px; margin-left:50px">
                                <canvas id="ratingPieChart" width="300" height="300"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="card">
                        <div class="card-header">
                            Average ratings over last 6 months
                        </div>
                        <div class="card-body" >
                            <div class="chart-container" style="position: relative; height:450px; width:450px; margin-top: 2cm; margin-bottom: 2cm; margin-left: 1cm">
                                <canvas id="avgRatingLineChart" width="300" height="300"></canvas>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="card">
                        <div class="card-header">
                            Transaction costs over last 6 months
                        </div>
                        <div class="card-body" >
                            <div class="chart-container" style="position: relative; height:450px; width:450px; margin-left: 10px">
                                <canvas id="costHorizonalBarChart" width="300" height="300"></canvas>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- End of Page Content --%>
    </div>

    <br /><br /><br />

    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminHomepage.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminHomepageStats/TransactionsBarChart.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminHomepageStats/RatingLineChart.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminHomepageStats/RatingPieChart.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminHomepageStats/CostHorizontalBarChart.js"></script>

</body>
</html>



