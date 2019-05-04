<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerProfile.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Customer.CustomerProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Customer Profile</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/CustomerProfile.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/OnLoadCustomer.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerProfile.js"></script>
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
                        <a class="navbar-brand" href="#">
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
    <div class="container" style="border-bottom: 1px solid">
        <div class="row">
            <div class="col" style="border-right: 1px solid">
                <div class="headerDetails">
                    <h2><span class="underlinedText" style="text-align:center">Personal Details:</span><span class="icon"><i class='far fa-edit' style='font-size:36px'></i></span></h2>
                </div>
                <br /><br />
                <div class="container">
                    <div class="row">
                        <div class="col col-lg-4 text-right">Full Name: </div>
                        <div class="col text-left">
                            <input id="FullName" type="text" class="form-control" readonly/>
                            <span id="FullNameErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Username:</div>
                        <div class="col text-left">
                            <input id="Username" type="text" class="form-control" readonly/>
                            <span id="UsernameErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Email:</div>
                        <div class="col text-left">
                            <input id="Email" type="text" class="form-control" />
                            <span id="EmailErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right"></div>
                        <div class="col text-left">
                            <a href="CustomerChangePassword.aspx">Change Password?</a>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
            <div class="col">
                <div class="headerDetails">
                    <h2><span class="underlinedText">Payment Details:</span><span class="icon"><i class='far fa-edit' style='font-size:36px'></i></span></h2>
                </div>
                <br /><br />
                <div class="container">
                    <div class="row">
                        <div class="col col-lg-4 text-right">Card Holder:</div>
                        <div class="col text-left">
                            <input id="CardHolder" type="text" class="form-control" />
                            <span id="CardHolderErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Card Number: </div>
                        <div class="col text-left">
                            <input id="CardNo" type="text" class="form-control" />
                            <span id="CardNoErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Card Expiry:</div>
                        <div class="col text-left">
                             <input id="ExpiryDate" type="text" class="form-control" />
                             <span id="ExpiryDateErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">CVV:</div>
                        <div class="col text-left">
                            <input id="CVV" type="text" class="form-control" />
                            <span id="CVVErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <br />
        <div class="headerDetails">
            <h2><span class="underlinedText">Car Details:<span class="icon"><i class='far fa-edit' style='font-size:36px'></i></span></span></h2>
        </div>
        <br /><br />
        <div class="row">
            <div class="col">
                <div class="container">
                    <div class="row">
                        <div class="col col-lg-4 text-right">Make: </div>
                        <div class="col text-left">
                            <input id="Make" type="text" class="form-control" />
                            <span id="MakeErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Model:</div>
                        <div class="col text-left">
                            <input id="Model" type="text" class="form-control" />
                            <span id="ModelErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />

                </div>

            </div>
            <div class="col">
                <div class="container">
                    <div class="row">
                        <div class="col col-lg-4 text-right">Color: </div>
                        <div class="col text-left">
                            <input id="Color" type="text" class="form-control" />
                            <span id="ColorErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col col-lg-4 text-right">Registration:</div>
                        <div class="col text-left">
                             <input id="RegNo" type="text" class="form-control" />
                            <span id="RegNoErrMess" class="ErrorMessage"></span>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>

    <br />
    <div class="container" style="text-align:center">
        <button id="SaveChanges" class="btn btn-primary">Save Changes</button>
    </div>

    <br /> <br />
</body>
</html>

