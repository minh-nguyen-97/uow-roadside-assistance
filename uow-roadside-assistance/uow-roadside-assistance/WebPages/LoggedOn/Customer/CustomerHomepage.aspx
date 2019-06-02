<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerHomepage.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Customer.CustomerHomepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Customer Homepage</title>
    
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

    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/CustomerHomepage.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/OnLoadCustomer.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerHomepage.js"></script>
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
                            <a class="dropdown-item" href="#">Past transactions</a>
                            <div class="dropdown-divider"></div>
                            <a id="logOutLink" class="dropdown-item">Logout <i class='fas fa-sign-out-alt'></i></a> 
                        </div>
                    </li>
                </ul>
            </div>
        </nav>
    </div>

    <%-- Page Content --%>
    <br />
    <br />
    <table style="width: 80%;" border="0" id="topTable">
        <tbody>
            <tr>
                <td id="update-details-image" style="background-color:#172547; cursor:pointer " onclick="window.location.href = './CustomerProfile.aspx'">
                    <span class="icon"><i class='fas fa-user-edit' style='font-size:55px; color:lightgray;'></i></span>
                </td>
                <td style="width:5%; height:50px;"></td>
                <td id="manage-subscription-image" style=" background-color:#172547; cursor:pointer " onclick="window.location.href = './ManageSubscription.aspx'">
                <span class="icon"><i class="fas fa-toolbox" style='font-size:55px; color:lightgray;'></i></span>
                </td>
            </tr>
                <tr></tr>
            <tr>
                <td id="update-details-text" style="cursor:pointer" onclick="window.location.href = './CustomerProfile.aspx'"> 
                    <h4>Update My Details</h4>
                </td>
                <td style="width:5%"></td>
                <td id="manage-subscription-text"> 
                    <h4>Manage Subscription</h4>
                </td>
            </tr>
        </tbody>
    </table>
    <table id="middleTable" style="width: 60%; text-align: center" border="0">
        <tr>
            <td id="request-service-image" style="cursor:pointer" onclick="redirectToRightPlace()">
            <span class="icon"><i class="fas fa-tools" style="font-size:55px; color:lightgray;"></i></span>
            </td>
        </tr>
        <tr>
            <td id="request-service-text" onclick="redirectToRightPlace()" style="width:70%; text-align: center; background-color:lightgray;cursor:pointer">
                <h4>Request Service</h4> 
            </td>
        </tr>
    </table>

</body>
</html>

