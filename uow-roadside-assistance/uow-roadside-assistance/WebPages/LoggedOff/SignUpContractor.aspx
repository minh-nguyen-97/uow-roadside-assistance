<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpContractor.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOff.SignUpContractor_2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%-- title --%>
    <title>Contractor Sign Up</title>

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
    <script type="text/javascript" src="../../Scripts/SignUp/SignUpContractor.js"></script>
    <script type="text/javascript" src="../../Scripts/OnLoadLoggedOff.js"></script>

    <%-- Page CSS --%>
    <link rel="stylesheet" href="../../Css/LoggedOff/SignUp/SignUpContractor.css" />

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
    <div class="container-fluid" style="padding-top: 1.5cm; ">
        <div class="row justify-content-md-center">
            <div class="col-sm-8">
                <div class="card" style="border:none;">
                    <div class="card-body">
                        <div class="card-header" style="border:none;">
                            <div class="container">
                                <div class="row">
                                    <div class="col d-flex justify-content-end"><img src="../../Images/official_logo.gif" width="250" height="250" /></div>
                                    <div class="col">
                                        <div style="margin-top: 10%;">
                                            <span id="contractorSignUpLogoText">Daedalus Contractor</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div>
                            <div id="accordion">

                                <%-- FIRST TAB --%>
                                <h3 style="height:1.5cm; vertical-align:middle;font-size: 0.5cm;"><a href="#">Account Details</a></h3>
                                <div>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col">
                                                <div class="input-group input-group-lg">
                                                    <input id="FirstName" type="text" class="form-control" placeholder="First Name" />
                                                </div>
                                                <span id="FirstNameErrMess" class="ErrorMessage"></span>
                                                <br /><br />

                                                <div class="input-group input-group-lg">
                                                    <input id="Username" type="text" class="form-control" placeholder="Username" />
                                                </div>
                                                <span id="UsernameErrMess" class="ErrorMessage"></span>
                                                
                                                <br /><br />
                                                <div class="input-group input-group-lg">
                                                    <input id="Email" type="email" class="form-control" placeholder="Email address" />
                                                </div>
                                                <span id="EmailErrMess" class="ErrorMessage"></span>
                                                <br />
                                            </div>
                                            <div class="col">

                                                <div class="input-group input-group-lg">
                                                    <input id="LastName" type="text" class="form-control" placeholder="Last Name" />
                                                </div>
                                                <span id="LastNameErrMess" class="ErrorMessage"></span>
                                                <br /><br />

                                                <div class="input-group input-group-lg">
                                                    <input id="Password" type="password" class="form-control" placeholder="Password" />
                                                </div>
                                                <span id="PasswordErrMess" class="ErrorMessage"></span>
                                                
                                                <br /><br />
                                                <div class="input-group input-group-lg">
                                                    <input id="ConfirmedPassword" type="password" class="form-control" placeholder="Confirm Password" />
                                                </div>
                                                <span id="ConfirmedPasswordErrMess" class="ErrorMessage"></span>
                                                <br />

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col"></div>
                                            <div class="col"></div>
                                            <div class="col"></div>
                                            <div class="col">
                                                <button id="next1" class="btn btn-outline-primary signUpButton" > NEXT </button>
                                            </div>
                                        </div>
                                    </div>                         
                                </div>

                                <%-- SECOND TAB --%>
                                <h3 style="height:1.5cm; vertical-align:middle;font-size: 0.5cm;" onclick="validateFirstFields()" class="ui-state-disabled"><a href="#">Credit Card Details</a></h3>
                                <div>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col">

                                                <div class="input-group input-group-lg">
                                                    <input id="AccountName" type="text" class="form-control" placeholder="Account Name" />
                                                </div>
                                                <span id="AccountNameErrMess" class="ErrorMessage"></span>

                                                <br /><br />
                                                <div class="input-group input-group-lg">
                                                    <input id="BSB" type="text" class="form-control" placeholder="BSB (e.g. 123456)" />
                                                </div>
                                                <span id="BSBErrMess" class="ErrorMessage"></span>
                                                <br />

                                            </div>
                                            <div class="col">

                                                <div class="input-group input-group-lg">
                                                    <input id="AccountNumber" type="text" class="form-control" placeholder="Account Number (e.g. 123456789)" />
                                                </div>
                                                <span id="AccountNumberErrMess" class="ErrorMessage"></span>
  
                                            </div>
                                        </div>

                                        <br />

                                        <div class="row">
                                            <div class="col">
                                                <button class="btn btn-outline-secondary signUpButton previous"> PREVIOUS </button>
                                            </div>
                                            <div class="col"></div>
                                            <div class="col"></div>
                                            <div class="col">
                                                <button id="signUpContractorSubmit" class="btn btn-success signUpButton"> SIGN UP </button>
                                            </div>
                                        </div>
                                    </div>   
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>


