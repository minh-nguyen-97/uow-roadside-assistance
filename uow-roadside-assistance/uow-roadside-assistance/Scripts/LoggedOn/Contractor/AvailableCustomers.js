window.onload = function () {
    ContractorService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Contractor') {
                window.history.back();
            }
            else {
                $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                loadAvailableCustomers();
            }
        }
    });
}

function loadAvailableCustomers() {
    ContractorService.getRequestedIds(function (res) {
        var requests = JSON.parse(res);

        for (var i = 0; i < requests.length; i++) {
            var requestID = requests[i].RequestID;
            var responseStatus = requests[i].ResponseStatus;

            ContractorService.extractCustomerData(requestID, responseStatus, function (result) {
                var res = JSON.parse(result);

                var fullName = res[0];

                var cusLat = res[1];
                var cusLng = res[2];
                var conLat = res[3];
                var conLng = res[4];
                var distance = distanceInKiloMeters(cusLat, cusLng, conLat, conLng);
                distance = Math.round(distance * 100) / 100

                var fee = Math.round(distance * 50 * 100) / 100;
                

                var responseStatus = res[5];
                var requestID = res[6];

                addCustomerRow(fullName, fee, distance, responseStatus, requestID);
            });
        }
    })
}

function addCustomerRow(fullName, fee, distance, responseStatus, requestID) {
    if (responseStatus == 'Busy')
        return;

    var tr = document.createElement('tr');
    tr.setAttribute("id", "rowFor" + requestID);

    var rowContent =
        "   <th scope = 'row'>" + fullName+"</th> " +
        "   <td>$" + fee + "</td>" +
        "   <td>" + distance+"</td> " +
        "   <td>" +
        "   <button class='btn btn-outline-primary' data-toggle='modal' data-target='#ModalCenter' data-whatever='" + requestID + "'> View Details</button> " +
        "   </td>";

    if (responseStatus == 'Waiting') {

        rowContent +=
            "<td>" +
            "   <button id='declineButton" + requestID + "' class='btn btn-danger responseButton declineButton' onclick='deleteRow(" + requestID + ")' type='button'>Busy</button>&nbsp;" +
            "   <button id='acceptButton" + requestID + "' class='btn btn-success responseButton acceptButton' onclick='acceptRow(" + requestID + ")' type='button'>Accept</button>" +
            "</td>";

    } else if (responseStatus == 'Accepted') {
        rowContent +=
            "<td>" +
            "    <button class='btn btn-warning statusButton' type='button' disabled>" +
            "        <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>" +
            "        Waiting for customer agreement..." +
            "    </button>" +
            "</td>";
    }

    tr.innerHTML = rowContent;

    document.getElementById('availableCustomersTable').appendChild(tr);
}

function distanceInKiloMeters(lat1, lon1, lat2, lon2) {
    if ((lat1 == lat2) && (lon1 == lon2)) {
        return 0;
    }
    else {
        var radlat1 = Math.PI * lat1 / 180;
        var radlat2 = Math.PI * lat2 / 180;
        var theta = lon1 - lon2;
        var radtheta = Math.PI * theta / 180;
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        if (dist > 1) {
            dist = 1;
        }
        dist = Math.acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;
        dist = dist * 1609.344 / 1000;
        return dist;
    }
}


function deleteRow(requestID) {
    var tr = document.getElementById('rowFor' + requestID);
    document.getElementById('availableCustomersTable').removeChild(tr);
    ContractorService.declineRequest(requestID);
};

function acceptRow(requestID) {
    var tr = document.getElementById('rowFor' + requestID);
    tr.lastChild.innerHTML =
        "    <button class='btn btn-warning statusButton' type='button' disabled>" +
        "        <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>" +
        "        Waiting for customer agreement..." +
        "    </button>";
    ContractorService.acceptRequest(requestID);
}

$(document).ready(function () {
    $('#ModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var requestID = button.data('whatever') // Extract info from data-* attributes

        ContractorService.getDetailsForCustomer(requestID, function (result) {
            var res = JSON.parse(result);

            var tyreProblem = res[0];
            var carBatteryProblem = res[1];
            var engineProblem = res[2];
            var generalProblem = res[3];
            var problemDescription = res[4];

            $('#TyreCheckBox').prop('checked', tyreProblem);
            $('#CarBatteryCheckBox').prop('checked', carBatteryProblem);
            $('#EngineCheckBox').prop('checked', engineProblem);
            $('#GeneralCheckBox').prop('checked', generalProblem);

            $('#Description').val(problemDescription);
        });
        
    });
});