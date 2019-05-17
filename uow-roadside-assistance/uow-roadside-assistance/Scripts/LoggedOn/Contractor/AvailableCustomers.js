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

var ContractorLatitude = null;
var ContractorLongitude = null;

function loadAvailableCustomers() {

    ContractorService.getRequestedCustomers(function (res) {
        var requestedCustomers = JSON.parse(res);

        for (var i = 0; i < requestedCustomers.length; i++) {

            var req = requestedCustomers[i];

            if (ContractorLatitude == null)
                ContractorLatitude = req.ConLat;

            if (ContractorLongitude == null)
                ContractorLongitude = req.ConLng;

            var distance = distanceInKiloMeters(req.CusLat, req.CusLng, req.ConLat, req.ConLng);
            distance = Math.round(distance * 100) / 100

            var fee = Math.round(distance * 50 * 100) / 100;

            addCustomerRow(req.FullName, fee, distance, req.ResponseStatus, req.RequestID);
        }
    });

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

    var map = null;
    var conLatLng; 

    function initializeGMap(cusLat, cusLng) {

        var myOptions = {
            zoom: 12,
            zoomControl: true,
            center: conLatLng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

        // initialize contractor
        conLatLng = new google.maps.LatLng(ContractorLatitude, ContractorLongitude);
        var conMarker = new google.maps.Marker({
            position: conLatLng,
            map: map
        });

        var conInfowindow = new google.maps.InfoWindow({
            content: 'Your location'
        });

        conMarker.addListener('click', function () {
            conInfowindow.open(map, conMarker);
        });


        // initialize customer
        var cusLatLng = new google.maps.LatLng(cusLat, cusLng);
        var cusMarker = new google.maps.Marker({
            position: cusLatLng,
            map: map,
            icon: {
                url: "http://maps.google.com/mapfiles/ms/icons/green-dot.png"
            }
        });

        var cusInfowindow = new google.maps.InfoWindow({
            content: 'Your customer'
        });

        cusMarker.addListener('click', function () {
            cusInfowindow.open(map, cusMarker);
        });

        
    }

    //Re - init map before show modal
    $('#ModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var requestID = button.data('whatever') // Extract info from data-* attributes

        ContractorService.getRequestDetails(requestID, function (result) {
            var res = JSON.parse(result);

            ContractorService.getCarDetailsOfCustomer(res.CustomerID, function (customer) {
                var cus = JSON.parse(customer);

                $('#Make').val(cus.Make);
                $('#Model').val(cus.Model);
                $('#Color').val(cus.Color);
                $('#RegNo').val(cus.RegNo);
            });

            $('#TyreCheckBox').prop('checked', res.TyreProblem);
            $('#CarBatteryCheckBox').prop('checked', res.CarBatteryProblem);
            $('#EngineCheckBox').prop('checked', res.EngineProblem);
            $('#GeneralCheckBox').prop('checked', res.GeneralProblem);

            $('#Description').val(res.ProblemDescription);

            initializeGMap(res.CustomerLatitude, res.CustomerLongitude);
            $("#location-map").css("width", "100%");
            $("#map_canvas").css("width", "100%");
        });

    });

    // Trigger map resize event after modal shown
    $('#ModalCenter').on('shown.bs.modal', function () {
        google.maps.event.trigger(map, "resize");
        map.setCenter(conLatLng);
    });
});