<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOff.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%-- title --%>
    <title>Homepage</title>

    <%-- jquery and jquery ui --%>
    <link rel="stylesheet" href="../../Css/LoggedOff/NavBar.css" />
    
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <%-- Bootstrap 4 --%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../Scripts/OnLoadLoggedOff.js"></script>

    <%-- Page CSS --%>
    <link rel="stylesheet" href="../../Css/LoggedOff/SlideShow.css" />

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
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="2"></li>
            <li data-target="#myCarousel" data-slide-to="3"></li>
            <li data-target="#myCarousel" data-slide-to="4"></li>
            <li data-target="#myCarousel" data-slide-to="5"></li>
            <li data-target="#myCarousel" data-slide-to="6"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
            <div class="carousel-item active">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/first.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/second.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/third.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/forth.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/fifth.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/sixth.jpg" class="imgHomeLogo"/>
                </div>
            </div>
            <div class="carousel-item">
                <div class ="homelogo">
                    <img src="../../Images/slide_show/seventh.jpg" class="imgHomeLogo"/>
                </div>
            </div>

        </div>

        <!-- Left and right controls -->
        <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>

</body>
</html>

