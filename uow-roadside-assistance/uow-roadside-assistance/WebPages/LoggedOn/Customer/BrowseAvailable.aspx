<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseAvailable.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Customer.BrowseAvailable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Customer Browse Contractors</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Customer/BrowseAvailable.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/CustomerNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/OnLoadCustomer.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Customer/BrowseAvailable.js"></script>
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
    <div class="container">
        <br /><br />
        <div class="headerDetails">
            <h2>
                <span class="underlinedText" style="text-align:center">
                    Requested Contractors
                </span>
                <span class="icon"><i class="fas fa-list-ul" style='font-size:36px'></i></span>
            </h2>
        </div>

        <br /><br />
        <table class="table table-striped">
            <thead>
                <tr>
                    <th scope="col" class="sortable">Contractor <i class="fas fa-sort"></i></th>
                    <th scope="col" class="sortable">Consultation Fee <i class="fas fa-sort"></i></th>
                    <th scope="col" class="sortable">Distance (KM) <i class="fas fa-sort"></i></th>
                    <th scope="col" class="sortable">Rating <i class="fas fa-sort"></i></th>
                    <th scope="col">Reviews </th>
                    <th id="statusFilter" scope="col" data-toggle='modal' data-target='#FilterModalCenter'>
                        Status 
                        <i class="fas fa-filter" style="color: red"></i>
                        <i class="fas fa-filter" style="color: yellow"></i>
                        <i class="fas fa-filter" style="color: green"></i>
                    </th>
                </tr>
            </thead>
            <tbody id="availableContractorsTable">


<%--                <tr>
                    <th scope='row'>Mark Otto</th>
                    <td>$100</td>
                    <td>500</td>
                    <td>5 <i class='fas fa-star' style='color: greenyellow'></i></td>
                    <td><button class='btn btn-outline-primary'>View Reviews</button></td>
                    <td>
                        <button class='btn btn-warning statusButton' type='button' disabled>
                          <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>
                          Waiting...
                        </button>
                    </td>
                </tr>
                <tr>
                    <th scope='row'>Fernando Torres</th>
                    <td>$90</td>
                    <td>900</td>
                    <td>4 <i class='fas fa-star' style='color:greenyellow'></i></td>
                    <td><button class='btn btn-outline-primary'>View Reviews</button></td>
                    <td>    
                        <!-- Button trigger modal -->
                        <button type='button' class='btn btn-danger statusButton' disabled>
                          Busy
                        </button>
                    </td>
                </tr>
                <tr>
                    <th scope='row'>Jacob Thotton</th>
                    <td>$80</td>
                    <td>1000</td>
                    <td>4.5 <i class='fas fa-star' style='color:greenyellow'></i></td>
                    <td><button class='btn btn-outline-primary'>View Reviews</button></td>
                    <td>    
                        <!-- Button trigger modal -->
                        <button type='button' class='btn btn-success statusButton' data-toggle='modal' data-target='#ModalCenter'>
                          Accepted
                        </button>
                    </td>
                </tr>--%>
            </tbody>
        </table>

        <div style="text-align:center">
            <button id="CancelRequestButton" class="btn btn-danger" style="width:30%; height:1.5cm">Cancel Request</button>
        </div>
    </div>

    <!-- Payment Modal -->
    <div class="modal fade" id="ModalCenter" tabindex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="ModalCenterTitle"><strong>Confirm Payment</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid" style="text-align:center">
                YOU ARE ABOUT TO PAY: <br /><br />
                [<strong id="confirmContractorName"></strong>] <br />
                <strong style="font-size: 18px">$</strong>
                <strong id="confirmFee" style="font-size: 18px"></strong> <br /><br />
                <strong>To come and assess your problem!!!</strong> <br /><br />
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Decline</button>
            <button type="button" class="btn btn-primary submitButton">Accept</button>
          </div>
        </div>
      </div>
    </div>

    <%-- Review Modal --%>
    <div class="modal fade" id="ReviewModalCenter" tabindex="-1" role="dialog" aria-labelledby="ReviewModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="ReviewModalCenterTitle"><strong>Reviews</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div id="reviews" class="container">
                <div class='row'>
                    <div class='col-1'>
                        <i class='fas fa-user-circle fa-2x'></i>
                    </div>
                    <div class='col'>
                        <span style='color: steelblue; font-size: 20px'>Customer Name</span>
                    </div>
                </div>
                <div class='row'>
                    <div class='col-1'>
                    </div>
                    <div class='col' style='background-color: whitesmoke; border-radius: 10px; padding: 10px;'>
                        Review goes here
                    </div>
                </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <%-- Filter Modal --%>
    <div class="modal fade" id="FilterModalCenter" tabindex="-1" role="dialog" aria-labelledby="FilterModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="FilterModalCenterTitle"><strong>Filter Status</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
             <div class="container">
                 <br />
                 <div class="row">
                     <div class="col col-lg-3 text-right">
                         <input id="BusyCheckBox" type="checkbox" checked/>
                     </div>
                     <div class="col">
                         <button type='button' class='btn btn-danger statusButton' disabled>Busy</button>
                     </div>
                 </div>
                 <br />
                 <div class="row">
                     <div class="col col-lg-3 text-right">
                         <input id="WaitingCheckBox" type="checkbox" checked />
                     </div>
                     <div class="col">
                         <button class='btn btn-warning statusButton' type='button' disabled>
                              <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>
                              Waiting...
                         </button> 
                     </div>
                 </div>

                 <br />
                 <div class="row">
                     <div class="col col-lg-3 text-right">
                         <input id="AcceptedCheckBox" type="checkbox" checked/>
                     </div>
                     <div class="col">
                         <button type='button' class='btn btn-success statusButton'>
                            Accepted
                        </button>
                     </div>
                 </div>
                 <br />
             </div>
          </div>
            
          <div class="modal-footer">
            <button id="applyFilterButton" type="button" class="btn btn-outline-primary mx-auto" style="width: 25%; height: 1.3cm"  data-dismiss="modal">APPLY</button>
          </div>

        </div>
      </div>
    </div>


</body>
</html>
