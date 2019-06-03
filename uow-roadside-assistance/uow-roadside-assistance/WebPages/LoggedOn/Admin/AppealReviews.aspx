<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppealReviews.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Admin.AppealReviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Admin Assess Appeal</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Admin/AppealReviews.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AdminNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/OnLoadAdmin.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Admin/AppealReviews.js"></script>

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
                <a class="nav-link active navBarElement" href="AppealReviews.aspx">
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
        
        <div class="container-fluid" style="width: 90%;">
            <br /><br />
            <div class="headerDetails">
                <h2>
                    <span class="underlinedText" style="text-align:center">
                        Appealed Reviews
                    </span>
                    <span class="icon"><i class="fas fa-list-ul" style='font-size:36px'></i></span>
                </h2>
            </div>

            <br /><br />

            <table class="table table-hover table-bordered">
                <thead class="thead-dark">
                <tr>
                    <th scope="col" class="filterHeader" style="width:15%" data-toggle='modal' data-target='#FilterModalCenter' data-filtertype="Transaction">
                        Transaction ID <i class="fas fa-filter"></i>
                    </th>
                    <th scope="col" class="filterHeader" style="width:15%" data-toggle='modal' data-target='#FilterModalCenter' data-filtertype="Contractor">
                        Contractor ID <i class="fas fa-filter"></i>
                    </th>
                    <th scope="col" class="filterHeader" style="width:15%" data-toggle='modal' data-target='#FilterModalCenter' data-filtertype="Customer">
                        Customer ID <i class="fas fa-filter"></i>
                    </th>
                    <th scope="col" style="width:18%">Appealed Review</th>
                    <th scope="col" class="filterHeader" style="width:36%">
                        Appeal Assessment
                    </th>
                </tr>
                </thead>
                <tbody id="allAppealedReviewsTable">
                    <%--<tr>
                        <td>1</td>
                        <td>2</td>
                        <td>3</td>
                        <td>$3.5</td>
                        <td>2019-05-16 01:21:23.990</td>
                    </tr>--%>
                </tbody>
            </table>

        </div>


        <br />
        <div id="clearFilterDiv" class="container">
            <button id="clearFilterButton" class="btn btn-danger" >CLEAR FILTERS</button>
        </div>

        <br /><br />

        <%-- End of Page Content --%>
    </div>

        <!-- Filter By ID Modal -->
    <div class="modal fade" id="FilterModalCenter" tabindex="-1" role="dialog" aria-labelledby="FilterModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-body">
            <div class="container-fluid" style="text-align:center">
                <div class="row">
                    <div class="col-6 text-right">
                        <span id="filterType" class="filterName"></span>
                        <span class="filterName">ID:</span> 
                    </div>
                    <div class="col-3">
                        <input id="filterID" type="text" class="form-control"/> 
                    </div>
                    <div class="col text-left">
                        <i id="searchForID" class="fas fa-search fa-2x"></i>
                    </div>
                </div>
            </div>
          </div>
        </div>
      </div>
    </div>

     <%-- Appeal Modal --%>

    <div class="modal fade" id="AppealModalCenter" tabindex="-1" role="dialog" aria-labelledby="AppealModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="AppealModalCenterTitle"><strong>Review, Rating & Appeal Reason</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
              <br />
            <div id="ratingDiv" class="container-fluid" style="text-align:center">
                <h2><i>Review and Rating</i></h2>
                <br />
                <i id="star1" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star2" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star3" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star4" class="far fa-star fa-3x star" style="color: greenyellow"></i>
                <i id="star5" class="far fa-star fa-3x star" style="color: greenyellow"></i>
            </div>
              <br /><br />
            <div class="container-fluid" style="text-align:center">
                <textarea id="reviewDesc" class="form-control" rows="9" placeholder="You can let us know how our service was in details (required)" disabled></textarea>
                <br />
            </div>
              <br /><br />
              <div class="container-fluid"  style="text-align:center">
                  <h2><i>Appeal Reason</i></h2>
                  <br />
                  <textarea id="appealReason" class="form-control" rows="9" placeholder="Please enter your reason(s) for appealing this transaction review (required)" disabled></textarea>
              </div>
          </div>
            <br /><br />

        </div>
      </div>
    </div>

</body>
</html>
