<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOff.SignIn_2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%-- title --%>
    <title>Sign In</title>

    <%-- jquery and jquery ui --%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <%-- Bootstrap 4 --%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <%-- Navigation Bar CSS --%>
     <link rel="stylesheet" href="../../Css/LoggedOff/NavBar.css" />

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../Scripts/SignIn.js"></script>
    <script type="text/javascript" src="../../Scripts/OnLoadLoggedOff.js"></script>

    <%-- Page CSS --%>
    <link rel="stylesheet" href="../../Css/LoggedOff/SignIn.css" />

</head>
<body>

    <%-- Service --%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/WebPages/LoggedOff/LoggedOffService.svc" />
            </Services>
        </asp:ScriptManager>
    </form>

     <%-- Navigation Bar --%>
    <div id="navigationBar" class="container-fluid">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <a class="navbar-brand" href="Home.aspx">
                <img src="../../Images/official_logo.gif" width="60" height="60"/><span id="logoText">Daedalus</span>
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown" aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div id="navbarNavDropdown" class="navbar-collapse collapse">
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item active">
                        <a class="nav-link" href="Home.aspx">Home <span class="sr-only">(current)</span></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Services</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">Pricing</a>
                    </li>

                </ul>
                <ul class="navbar-nav">
                    <li class="nav-item dropdown">
                        <div class="btn">
                            <a class="nav-link dropdown-toggle text-success" id="registerDropdown" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                Register
                            </a>
                            <div class="dropdown-menu" aria-labelledby="registerDropdown">
                                <a class="dropdown-item text-danger" href="SignUpCustomer.aspx">As a Customer</a>
                                <a class="dropdown-item text-info" href="SignUpContractor.aspx">As a Contractor</a>
                            </div>
                        </div>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="SignIn.aspx"><span class="btn btn-outline-primary">Login</span></a>
                    </li>
                </ul>
            </div>
        </nav>
    </div>

    <%-- Page Content --%>

    <div class="container-fluid" style="padding-top: 1.5cm;">
        <div class="row">
            <div class="col"></div>
            <div class="col">
                <div class="container" id="logInContainer">
                    <div class="card" >
                        <div class="card-body">
                            <div class="card-header" style="text-align:center; border:none;">
                                <img src="../../Images/official_logo.gif" width="200" height="200"/><br />
                                <span id="loginLogoText">Daedalus</span>
                            </div>
                            <div class="loginBody">
                                <br />
                                <div class="input-group input-group-lg">
                                    <input id="Username" type="text" class="form-control logInFieldButton" placeholder="Username" required />
                                </div>
                                <br />
                            
                                <div class="input-group input-group-lg">
                                    <input id="Password" type="password" class="form-control logInFieldButton" placeholder="Password" required />
                                </div>
                                <br />
                                    <button id="signInSubmit" class="btn btn-primary logInFieldButton">SIGN IN</button>
                                <br />
                                <br />
                                <br />
                                <h6 class="horizontalLine"><span>OR NEED AN ACCOUNT ?</span></h6>
                                <br />
                                <button class="btn btn-outline-danger signUpButtons" onclick="window.location.href='./SignUpCustomer.aspx'">SIGN UP AS A CUSTOMER</button>
                                <br />
                                <br />
                                <button class="btn btn-outline-info signUpButtons" onclick="window.location.href='./SignUpContractor.aspx'">SIGN UP AS A CONTRACTOR</button>
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col"></div>
        </div>
    </div>
    <br />
    <br />

</body>
</html>


