<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuccessfulRegistration.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOff.SuccessfulRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%-- title --%>
    <title>Successful Registration</title>

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

    <%-- Page CSS --%>
    <script type="text/javascript">
        window.onload = function () {
            if (document.referrer == '') {
                window.location.href = './Home.aspx';
            }
        }
    </script>
</head>
<body>

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
        <div class="row justify-content-md-center">
            <div class="col-sm-8">
                <div class="card" style="background-color:whitesmoke; text-align:center;">
                    <div class="card-body" style="background-color:whitesmoke">
                        <div class="card-header" style="border:none; background-color:whitesmoke;">
                            <img src="../../Images/cropped_tick.gif" width="90" height="90"/> <br />
                            <span>Your registration is succesfull !!!</span> <br />
                        </div>
                        <div class="loginRegisBody">
                            <span>Thank you for registering in Daedalus</span> <br />
                            Please log in with your account <a href="SignIn.aspx">here</a> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>

