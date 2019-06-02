<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerPastTransactions.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Customer.CustomerPastTransactions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Customer Master</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/CustomerPastTransactions.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/OnLoadCustomer.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerPastTransactions.js"></script>

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
    <div class="container">
        <br /><br />
        <div class="headerDetails">
            <h2>
                <span class="underlinedText" style="text-align:center">
                    Past Completed Transactions
                </span>
                <span class="icon"><i class="fas fa-list-ul" style='font-size:36px'></i></span>
            </h2>
        </div>

        <br /><br />
        <table class="table table-hover">
            <thead class="thead-light">
                <tr>
                    <th scope="col" style="width: 25%" class="sortable">Contractor <i class="fas fa-sort"></i></th>
                    <th scope="col" style="width: 25%" class="sortable">Cost <i class="fas fa-sort"></i></th>
                    <th scope="col" style="width: 25%" class="sortable">Transaction Date Time <i class="fas fa-sort"></i></th>
                    <th scope="col" style="width: 25%" >Review & Rating </th>
                </tr>
            </thead>
            <tbody id="pastCompletedTransactionTable">
                <%--<tr>
                    <th scope='row'>Mark Otto</th>
                    <td>$100</td>
                    <td>500</td>
                    <td><button class='btn btn-outline-primary'>View / Modify</button></td>
                </tr>
                <tr>
                    <th scope='row'>Fernando Torres</th>
                    <td>$90</td>
                    <td>900</td>
                    <td><button class='btn btn-outline-primary'>View / Modify</button></td>
                </tr>--%>

            </tbody>
        </table>

    </div>

    <%-- Rating Modal --%>

    <div class="modal fade" id="ModalCenter" tabindex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="ModalCenterTitle"><strong>Review and Rating</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
              <br />
            <div id="ratingDiv" class="container-fluid" style="text-align:center">
                <i id="star1" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star2" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star3" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star4" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star5" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <div>
                    <span id="ratingErrMess" style="color:red; display:inline-block; font-size:20px"></span>
                </div>
            </div>
              <br /><br />
            <div class="container-fluid" style="text-align:center">
                <textarea id="reviewDesc" class="form-control" rows="9" placeholder="You can let us know how our service was in details (required)"></textarea>
                <div>
                    <span id="reviewErrMess" style="color:red; display:inline-block; font-size:20px"></span>
                </div>
                <br />
            </div>
          </div>
          <div class="modal-footer">
            <button id="submitReviewButton" type="button" class="btn btn-success mx-auto submitButton">SUBMIT</button>
          </div>
        </div>
      </div>
    </div>

</body>
</html>

