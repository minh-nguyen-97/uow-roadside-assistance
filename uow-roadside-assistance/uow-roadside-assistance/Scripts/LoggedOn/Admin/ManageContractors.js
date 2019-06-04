window.onload = function () {
    AdminService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Admin') {
                window.history.back();
            }
            else {
                // do stuff

                loadAllContractors();

            }
        }
    });
}

function loadAllContractors() {
    AdminService.GetAllContractorUsers(function (result) {
        var res = JSON.parse(result);

        for (var i = 0; i < res.length; i++) {
            addContractorRow(res[i].UserID, res[i].FullName, res[i].UserName, res[i].Email);
        }
    })
}

function addContractorRow(contractorID, fullName, username, email) {
    var tr = document.createElement('tr');
    tr.setAttribute("id", "rowFor" + contractorID);

    var rowContent =
        "<td><a href='AccountStatistics.aspx?uid=" + contractorID + "'>" + contractorID + "</a></td>" +
        "<td>" + fullName + "</td>" +
        "<td>" + username + "</td>" +
        "<td>" + email + "</td>" +
        "<td>" +
        "<button class='btn btn-danger' data-contractor='" + contractorID +"' data-toggle='modal' data-target='#DeleteModalCenter'> Delete</button> " +
        "</td>";

    tr.innerHTML = rowContent;

    document.getElementById('allContractorsTable').appendChild(tr);
}

function deleteRow(contractorID) {
    var tr = document.getElementById('rowFor' + contractorID);
    document.getElementById('allContractorsTable').removeChild(tr);
}


$(document).ready(function () {

    // Appeal Modal
    $('#DeleteModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var contractorID = button.data('contractor') // Extract info from data-* attributes

        $('#confirmContractorID').text('[' + contractorID + ']');

        $('#confirmDeleteButton').click(function (e) {
            AdminService.deleteContractor(contractorID);

            deleteRow(contractorID);

            $('#DeleteModalCenter').modal('toggle');
        });

    });
})