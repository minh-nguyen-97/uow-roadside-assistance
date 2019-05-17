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
                loadIncompleteTransaction();
            }
        }
    });
}

var ContractorLatitude = null;
var ContractorLongitude = null;

function loadIncompleteTransaction() {
    ContractorService.getUnfinishedTransactions(function (result) {
        var res = JSON.parse(result);
        for (var i = 0; i < res.length; i++) {
            var transaction = res[i];
            var transactionID = transaction.TransactionID;

            if (ContractorLatitude == null)
                ContractorLatitude = transaction.ConLat;

            if (ContractorLongitude == null)
                ContractorLongitude = transaction.ConLng;

            var name = transaction.FullName;
            var distance = distanceInKiloMeters(transaction.CusLat, transaction.CusLng, transaction.ConLat, transaction.ConLng);
            distance = Math.round(distance * 100) / 100

            var fee = Math.round(distance * 50 * 100) / 100;
            
            addRow(name, fee, distance, transactionID);
        }
    })
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

function addRow(fullName, fee, distance, transactionID) {
    var tr = document.createElement('tr');
    tr.setAttribute('id', 'rowFor' + transactionID);

    tr.innerHTML =
        "   <th scope = 'row'>" + fullName + "</th >" +
        "   <td>$" + fee + "</td>" +
        "   <td>" + distance + "</td>" +
    "   <td>" +
    "       <button class='btn btn-outline-primary' data-toggle='modal' data-target='#ModalCenter' data-whatever='" + transactionID + "'>View Details</button> " +
        "   </td>" +
        "   <td>" +
        "       <button onclick='finishedContractor(" + transactionID + ")' class='btn btn-success' > Completed Work</button > " +
        "    </td>";

    document.getElementById('incompleteCustomersTable').appendChild(tr);
}

function finishedContractor(transactionID) {
    var tr = document.getElementById('rowFor' + transactionID);
    document.getElementById('incompleteCustomersTable').removeChild(tr);
    ContractorService.finishedContractor(transactionID);
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
        var transactionID = button.data('whatever') // Extract info from data-* attributes

        ContractorService.getTransactionDetails(transactionID, function (result) {
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