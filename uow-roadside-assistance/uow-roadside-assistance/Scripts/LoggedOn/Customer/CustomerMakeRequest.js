window.onload = function () {
    CustomerService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Customer') {
                window.history.back();
            }
            else {

                var refer = window.location.href;
                refer = refer.replace('MakeRequest', 'CustomerHomepage');
                //alert(document.referrer);
                if (document.referrer != refer) {
                    window.history.back();
                }
                //CustomerService.getSessionRequest(function (res) {
                //    var sessionRequest = JSON.parse(res);
                //    if (sessionRequest != null) {
                //        alert('You have not completed your previouse request!!!');
                //        window.location.href = './BrowseAvailable.aspx';
                //    }
                //});
            }
        }
    });
}

var map, searchAddressText, defaultBounds, autocomplete;

var options;

var curLocMarker;

var nearByCircle;

var contractorMarkers = [];

var contractorInfos = [];



function initMap() {
    // initialize map
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -34.414321, lng: 150.884085},
        zoom: 8
    });

    // initialize autocomplete search box
    defaultBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(-28.15702, 159.1054441),
        new google.maps.LatLng(-37.5052801, 140.9992793)
    );

    options = {
        bounds: defaultBounds
    };

    // Create the search box and link it to the UI element.
    searchAddressText = document.getElementById('searchAddressText');
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    autocomplete = new google.maps.places.Autocomplete(searchAddressText, options);

    // initialize current location
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var curPos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            curLocMarker = new google.maps.Marker({
                position: curPos,
                map: map,
                title: 'Your location'
            });

            var infowindow = new google.maps.InfoWindow({
                content: 'Your location'
            });

            curLocMarker.addListener('click', function () {
                infowindow.open(map, curLocMarker);
            });

            nearByCircle = new google.maps.Circle({
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35,
                map: map,
                center: curPos,
                radius: 50000
            });

            nearByCircle.setVisible(false);

        }, errorCallBack, { timeout: 10000, enableHighAccuracy: true });
    };

    CustomerService.getAllContractorsAddresses(function (res) {
        var contractorAddresses = JSON.parse(res);
        for (var i = 0; i < contractorAddresses.length; i++) {

            var address = {
                userID: contractorAddresses[i].UserID,
                latitude: parseFloat(contractorAddresses[i].Latitude),
                longitude: parseFloat(contractorAddresses[i].Longitude)
            }
            contractorInfos.push(address);

            var newMarker = new google.maps.Marker({
                position: {
                    lat: address.latitude, lng: address.longitude
                },
                map: map,
                icon: {
                    url: "http://maps.google.com/mapfiles/ms/icons/green-dot.png"
                }
            });
            contractorMarkers.push(newMarker);
        }
    });

}

function errorCallBack() {
    alert('ERROR!!!');
}

$(document).ready(function () {

    $('.problemType').click(function (e) {
        var problemID = $(this).attr('id');
        problemID = problemID.replace('Problem', '');
        var problemCheckBoxID = problemID + 'CheckBox';
        var checkbox = $('#' + problemCheckBoxID);
        checkbox.prop("checked", !checkbox.prop("checked"));
    });

    $('.problemLabel').click(function (e) {
        var problemID = $(this).attr('id');
        problemID = problemID.replace('Label', '');
        var problemCheckBoxID = problemID + 'CheckBox';
        var checkbox = $('#' + problemCheckBoxID);
        checkbox.prop("checked", !checkbox.prop("checked"));
    });
    
    $('#nearBySearchButton').click(function (e) {
        if ($('#nearByRadius').val() == '') {
            $('#nearByRadiusErrMess').text('Please enter radius that you want to search !!');
            return;
        } else {
            $('#nearByRadiusErrMess').text('');
        }


        var radius = $('#nearByRadius').val();
        
        if (/^\d*\.?\d*$/.test(radius) == false) {
            $('#nearByRadiusErrMess').text('Please enter a valid radius, which is a real positive number !!');
            return;
        } else {
            $('#nearByRadiusErrMess').text('');
        }

        var unit = $('#nearByUnit').val();
        if (unit == 'Kilometers')
            radius = radius * 1000;

        nearByCircle.setRadius(parseFloat(radius));
        nearByCircle.setCenter(curLocMarker.getPosition());
        nearByCircle.setVisible(true);
        
        for (var i = 0; i < contractorMarkers.length; i++) {
            var lat1 = curLocMarker.getPosition().toJSON().lat;
            var lon1 = curLocMarker.getPosition().toJSON().lng;
            var lat2 = contractorMarkers[i].getPosition().toJSON().lat;
            var lon2 = contractorMarkers[i].getPosition().toJSON().lng;

            if (distanceInMeters(lat1, lon1, lat2, lon2) <= radius) {
                contractorMarkers[i].setVisible(true);
            } else {
                contractorMarkers[i].setVisible(false);
            }
        }
        
    });

    $('#searchAddressButton').click(function (e) {

        if ($('#searchAddressText').val() == '') {
            $('#searchAddressErrMess').text('Please choose your location!!!');
            return;
        } else {
            $('#searchAddressErrMess').text('');
        }

        var place = autocomplete.getPlace();
        
        if (place == undefined || !place || !place.geometry ) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            $('#searchAddressErrMess').text('No details available for entered place !!!');
            return;
        } else {
            $('#searchAddressErrMess').text('');
        }

        curLocMarker.setVisible(false);
        nearByCircle.setVisible(false);

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
        }
        curLocMarker.setPosition(place.geometry.location);
        console.log(place.geometry.location.toJSON());
        curLocMarker.setVisible(true);

        for (var i = 0; i < contractorMarkers.length; i++)
        {
            contractorMarkers[i].setVisible(true);
        }
    });

    //CustomerService.getAllContractorsAddresses(function (res) {
    //    var contractorAddresses = JSON.parse(res);
    //    for (var i = 0; i < contractorAddresses.length; i++) {

    //        var address = {
    //            userID: contractorAddresses[i].UserID,
    //            latitude: parseFloat(contractorAddresses[i].Latitude),
    //            longitude: parseFloat(contractorAddresses[i].Longitude)
    //        }
    //        contractorInfos.push(address);

    //        var newMarker = new google.maps.Marker({
    //            position: {
    //                lat: address.latitude, lng: address.longitude
    //            },
    //            map: map,
    //            icon: {
    //                url: "http://maps.google.com/mapfiles/ms/icons/green-dot.png"
    //            }
    //        });
    //        contractorMarkers.push(newMarker);
    //    }
    //});

    $('#RequestButton').click(function (e) {
        var checkbox = $('#TyreCheckBox').prop('checked');
        checkbox = checkbox || $('#CarBatteryCheckBox').prop('checked');
        checkbox = checkbox || $('#EngineCheckBox').prop('checked');
        checkbox = checkbox || $('#GeneralCheckBox').prop('checked');

        if (!checkbox) {
            alert('Please choose at least 1 type of problem that you have !!!');
            return;
        }

        var description = $('#Description').val();
        if (description.length == 0) {
            alert('Please enter description for your problem');
            return;
        }

        if (description.length > 1000) {
            alert('Please enter a short description (no more than 1000 characters)');
            return;
        }

        // service
        var tyreProblem = $('#TyreCheckBox').prop('checked');
        var carBatteryProblem = $('#CarBatteryCheckBox').prop('checked');
        var engineProblem = $('#EngineCheckBox').prop('checked');
        var generalProblem = $('#GeneralCheckBox').prop('checked');

        var problemDescription = $('#Description').val();
        var customerLatitude = curLocMarker.getPosition().toJSON().lat;
        var customerLongitude = curLocMarker.getPosition().toJSON().lng;

        var contractorIDs = [];
        for (var i = 0; i < contractorMarkers.length; i++) {
            if (contractorMarkers[i].getVisible()) {
                contractorIDs.push(contractorInfos[i].userID);
            }
        }

        CustomerService.MakeRequest(tyreProblem, carBatteryProblem, engineProblem, generalProblem, problemDescription, customerLatitude, customerLongitude, contractorIDs)

        window.location.href = './BrowseAvailable.aspx';

    });

});

function distanceInMeters(lat1, lon1, lat2, lon2) {
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
        dist = dist * 1609.344;
        return dist;
    }
}