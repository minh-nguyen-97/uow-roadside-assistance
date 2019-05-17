<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractorIncompleteWork.aspx.cs" Inherits="uow_roadside_assistance.WebPages.LoggedOn.Contractor.ContractorIncompleteWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <%--title--%>
    <title>Contractor Master</title>
    
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
    <link rel="stylesheet" href="../../../Css/LoggedOn/Contractor/ContractorNavBar.css" />
    <link rel="stylesheet" href="../../../Css/LoggedOn/Contractor/ContractorIncompleteWork.css" />

    <%--Nav Bar Scripts--%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Contractor/ContractorNavBar.js"></script>

    <%-- Page Scripts --%>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Contractor/OnLoadContractor.js"></script>
    <script type="text/javascript" src="../../../Scripts/LoggedOn/Contractor/ContractorIncompleteWork.js"></script>

</head>
<body>

    <%-- Service --%>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
               <asp:ServiceReference Path="~/WebPages/LoggedOn/Contractor/ContractorService.svc" />
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
                            <img src="../../../Images/official_logo.gif" width="60" height="60"/><span id="logoText">Daedalus Contractor</span>
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
                            <a class="dropdown-item" href="ContractorHomepage.aspx">Dashboard</a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="ContractorProfile.aspx">Profile</a>
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
                    Incomplete Transactions
                </span>
                <span class="icon"><i class="fas fa-list-ul" style='font-size:36px'></i></span>
            </h2>
        </div>

        <br /><br />
        <table class="table table-striped">
            <thead>
                <tr>
                    <th scope="col">Customer</th>
                    <th scope="col">Fee</th>
                    <th scope="col">Distance (KM)</th>
                    <th scope="col">Details</th>
                    <th scope="col" style="width:33%">Confirm Complete?</th>
                </tr>
            </thead>
            <tbody id="incompleteCustomersTable">
                <%--<tr>
                    <th scope='row'>Mark Otto</th>
                    <td>$100</td>
                    <td>5</td>
                    <td>
                        <button class='btn btn-outline-primary' data-toggle='modal' data-target='#ModalCenter'>View Details</button>
                    </td>
                    <td>
                        <button id='completeButton' class='btn btn-success'>Completed Work</button>
                    </td>
                </tr>--%>
                <%--<tr>
                    <th scope='row'>Mark Otto</th>
                    <td>$100</td>
                    <td>
                        
                    </td>
                </tr>--%>
            </tbody>
        </table>

    </div>

    <!-- Modal -->
    <div class="modal fade " id="ModalCenter" tabindex="-1" role="dialog" aria-labelledby="ModalCenterTitle" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="ModalCenterTitle"><strong>Problem Details</strong></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="container-fluid" style="text-align:center">
                
                 <div class="container-fluid">
                     <div class="headerDetails">
                         <h3><i>Car Details: </i></h3>
                     </div>
                     <br /><br />
                    <div class="row">
                        <div class="col">
                            <div class="container">
                                <div class="row">
                                    <div class="col col-lg-4 text-right">Make: </div>
                                    <div class="col text-left">
                                        <input id="Make" type="text" class="form-control" disabled/>
                                        <span id="MakeErrMess" class="ErrorMessage"></span>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col col-lg-4 text-right">Model:</div>
                                    <div class="col text-left">
                                        <input id="Model" type="text" class="form-control" disabled/>
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
                                        <input id="Color" type="text" class="form-control" disabled/>
                                        <span id="ColorErrMess" class="ErrorMessage"></span>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col col-lg-4 text-right">Registration:</div>
                                    <div class="col text-left">
                                        <input id="RegNo" type="text" class="form-control" disabled/>
                                        <span id="RegNoErrMess" class="ErrorMessage"></span>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>

                     <br /><br />
                    <div class="headerDetails">
                        <h3><i>Problem Type: </i></h3>
                    </div>
                    <br />
                    <%-- Row 1 --%>
                    <div class="row">
                        <div class="col">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-4 text-right" >
                                        <input id="TyreCheckBox" type="checkbox" disabled/>
                                    </div>
                                    <div class="col">
                                        <img id="TyreProblem" class="problemType" src="../../../Images/Customer/ProblemType/tyre.png"/>
                                        <br />
                                        <span id="TyreLabel" class="problemLabel">Tyre</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-4 text-right" >
                                        <input id="CarBatteryCheckBox" type="checkbox" disabled/>
                                    </div>
                                    <div class="col">
                                        <img id="CarBatteryProblem" class="problemType" src="../../../Images/Customer/ProblemType/carBattery.png"/>
                                        <br />
                                        <span id="CarBatteryLabel" class="problemLabel">Car Battery</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            
                    <br /><br />

                    <%-- Row 2 --%>
                    <div class="row">
                        <div class="col">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-4 text-right" >
                                        <input id="EngineCheckBox" type="checkbox" disabled/>
                                    </div>
                                    <div class="col">
                                        <img id="EngineProblem" class="problemType" src="../../../Images/Customer/ProblemType/engine.png"/>
                                        <br />
                                        <span id="EngineLabel" class="problemLabel">Engine</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="container">
                                <div class="row">
                                    <div class="col-lg-4 text-right" >
                                        <input id="GeneralCheckBox" type="checkbox" disabled/>
                                    </div>
                                    <div class="col">
                                        <img id="GeneralProblem" class="problemType" src="../../../Images/Customer/ProblemType/general.png"/>
                                        <br />
                                        <span id="GeneralLabel" class="problemLabel">General</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br /> <br />

                    <%-- Description --%>
                    <div class="headerDetails">
                        <h3><i>Description: </i></h3>
                    </div>

                    <br />
                    <div>
                        <textarea id="Description" class="form-control" rows="10" placeholder="Please write a short description of your problem" disabled></textarea>
                    </div>

                     <br /><br />
                    <%-- Location --%>
                    <div class="headerDetails">
                        <h3><i>Location: </i></h3>
                    </div>
                     <br /> 
                     <div class="row">
                         <div class="col">
                             <div class="location-map" id="location-map">
                                <div style="width: 600px; height: 400px;" id="map_canvas"></div>
                             </div>
                         </div>
                     </div>

                </div>

            </div>
              <br />
          </div>
        </div>
      </div>
    </div>


    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBbzUiNGypGLKksqci8ZJpNTrJ-JNqAFJA"></script>

</body>
</html>



