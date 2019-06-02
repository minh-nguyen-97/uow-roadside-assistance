<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSubscription.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Customer.ManageSubscription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Customer Manage Subscription</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/CustomerNavBar.css" />
    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/ManageSubscription.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/OnLoadCustomer.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/ManageSubscription.js"></script>
</head>
<body>

    <%-- Service --%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
               <asp:ServiceReference Path="~/WebPages/LoggedOn/Customer/CustomerService.svc" />
            </Services>
        </asp:ScriptManager>
    </form>

    <%-- Navigation Bar --%>
    <div id="navigationBar" class="container-fluid">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div id="navbarNavDropdown" class="navbar-collapse collapse">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <a class="navbar-brand" href="CustomerHomepage.aspx">
                            <img src="../../../Images/official_logo.gif" width="60" height="60"/><span id="logoText">Daedalus Customer</span>
                        </a>
                    </li>
                </ul>
                <ul class="navbar-nav">
                        
                    <li class="nav-item dropdown">
                        <a id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <span id="UserNameLabel"></span>
                            <i class="fas fa-user-circle fa-2x"></i>
                        </a>
                        <div class="dropdown-menu dropdown-menu-right text-right" aria-labelledby="dropdownMenuButton">
                            <a class="dropdown-item" href="CustomerHomepage.aspx">Dashboard</a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="CustomerProfile.aspx">Profile</a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="CustomerPastTransactions.aspx">Past transactions</a>
                            <div class="dropdown-divider"></div>
                            <a id="logOutLink" class="dropdown-item">Logout <i class='fas fa-sign-out-alt'></i></a> 
                        </div>
                    </li>
                </ul>
            </div>
        </nav>
    </div>

    <%-- Page Content --%>
    <div class="container" style="padding-top: 1.5cm;">
        <div id="subsHeader">
            <img src="../../../Images/official_logo.gif" style="border: whitesmoke solid 3px; border-radius: 5cm;" width="200" height="200" />
            <br /><br />
            <h2>Get started with a Daedalus subscription that works for you</h2>
        </div>

        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <%-- Pay As You Go Subscription --%>
                    <div class="card">
                        <div id="freeHeader" class="card-header">
                            Pay-as-you-go
                        </div>

                        <div id="freeBody" class="card-body">
                            <div class="price">
                                <span class="priceNumber">$0</span>
                            </div>

                            <div class="container-fluid benefits">
                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-check-circle"></i>
                                    </div>
                                    <div class="col">
                                        No charge for subscription
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-times-circle"></i>
                                    </div>
                                    <div class="col">
                                        Payment for every transaction is much more expensive for frequent users
                                    </div>
                                </div>

                            </div>

                            <div id="freeSubInfo" class="subInfo">
                                <div class="alert alert-primary text-center">
                                    CURRENT SUBSCRIPTION
                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <%-- MONTHLY SUBCRIPTION --%>
                <div class="col">
                    <div class="card">
                        <div id="monthlyHeader" class="card-header">
                            Premium (Monthly)
                        </div>

                        <div id="monthlyBody" class="card-body">
                            <div class="price">
                                <span class="priceNumber">$50</span>/mo
                            </div>

                            <div class="container-fluid benefits">
                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-check-circle"></i>
                                    </div>
                                    <div class="col">
                                        No fee for every transaction
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-check-circle"></i>
                                    </div>
                                    <div class="col">
                                        Best plan for short-term subscribers
                                    </div>
                                </div>

                            </div>

                            <div class="subInfo">

                                <div id="notSubscribeMonthly" class="notSubscribe">
                                    <button id="monthlySubButton" class="btn btn-outline-warning" data-toggle='modal' data-target='#PaymentModalCenter' data-subtype="monthly">
                                        SUBSCRIBE
                                    </button>
                                </div>

                                <div id="subscribedMonthly" class="subscribed">
                                    <div class="alert alert-primary text-center">
                                        CURRENT SUBSCRIPTION
                                    </div>

                                    <button id="cancelMonthlyButton" class="btn btn-danger" data-toggle='modal' data-target='#CancelModalCenter' data-subtype="monthly">
                                        CANCEL SUBSCRIPTION

                                    </button>

                                    <div id="monthlyExpiresAlert" class="alert alert-dark text-center">
                                        Expires on <span id="monthlyExpDate">13/12/2019</span>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                </div>

                <%-- YEARLY SUBSCRIPTION --%>
                <div class="col">
                    <div class="card">
                        <div id="yearlyHeader" class="card-header">
                            Premium (Yearly)
                        </div>

                        <div id="yearlyBody" class="card-body">
                            <div class="price">
                                <span class="priceNumber">$250</span>/yr
                            </div>

                            <div class="container-fluid benefits">
                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-check-circle"></i>
                                    </div>
                                    <div class="col">
                                        No fee for every transaction
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-2 text-center">
                                        <i class="fas fa-check-circle"></i>
                                    </div>
                                    <div class="col">
                                        Save up to 60% in comparison to monthly plan
                                    </div>
                                </div>

                            </div>

                            <div class="subInfo">

                                <div id="notSubscribeYearly" class="notSubscribe">
                                    <button id="yearlySubButton" class="btn btn-outline-warning" data-toggle='modal' data-target='#PaymentModalCenter' data-subtype="yearly">
                                        SUBSCRIBE
                                    </button>
                                </div>

                                <div id="subscribedYearly" class="subscribed">
                                    <div class="alert alert-primary text-center">
                                        CURRENT SUBSCRIPTION
                                    </div>

                                    <button id="cancelYearlyButton" class="btn btn-danger"  data-toggle='modal' data-target='#CancelModalCenter' data-subtype="yearly">
                                        CANCEL SUBSCRIPTION

                                    </button>
                                    <div id="yearlyExpiresAlert" class="alert alert-dark text-center">
                                        Expires on <span id="yearlyExpDate">13/12/2019</span>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>

    <br /><br />

    <%-- Modals --%>

        <!-- Payment Modal -->
    <div class="modal fade" id="PaymentModalCenter" tabindex="-1" role="dialog" aria-labelledby="PaymentModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="PaymentModalCenterTitle"><strong>Confirm Subscription Payment</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid" style="text-align:center">
                YOU ARE ABOUT TO PAY: <br /><br />
                <strong style="font-size: 30px">$</strong>
                <strong id="confirmFee" style="font-size: 30px"></strong> <br /><br />
                <strong>FOR <span id="subType"></span> SUBSCRIPTION</strong> <br /><br />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary mr-auto" data-dismiss="modal">Cancel</button>
            <button id="subAcceptButton" type="button" class="btn btn-primary acceptButton">Accept</button>
          </div>
        </div>
      </div>
    </div>

    <%-- Cancel Modal --%>

    <div class="modal fade" id="CancelModalCenter" tabindex="-1" role="dialog" aria-labelledby="CancelModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="CancelModalCenterTitle"><strong>Confirm Subscription Cancellation</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid" style="text-align:center">
                <br />
                <strong>YOU ARE ABOUT TO CANCEL YOUR <span id="cancelSubType"></span> SUBSCRIPTION</strong> <br /><br />
                <strong>However, your subscription is still valid until [<span id="cancelExpDate" style="color: blue"></span>]</strong> <br /> <br />
                <strong style="color:red">Are you sure you want to cancel this subscription ? </strong><br /><br />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary mr-auto" data-dismiss="modal">No Cancellation</button>
            <button id="cancelSubAcceptButton" type="button" class="btn btn-danger confirmCancelButton">Confirm Cancellation</button>
          </div>
        </div>
      </div>
    </div>

</body>
</html>

