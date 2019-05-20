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
                var refer1 = refer.replace('BrowseAvailable', 'MakeRequest');
                var refer2 = refer.replace('BrowseAvailable', 'CustomerHomepage');

                if (document.referrer != refer1 && document.referrer != refer2) {
                    window.history.back();
                } else {
                    $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                    loadAvailableContractors();
                }
            }
        }
    });
}

function loadAvailableContractors() {

    CustomerService.getAvailableContractors(function (availableContractors) {
        var result = JSON.parse(availableContractors);

        for (var i = 0; i < result.length; i++) {
            var res = result[i];

            var distance = distanceInKiloMeters(res.ConLat, res.ConLng, res.CusLat, res.CusLng);
            distance = Math.round(distance * 100) / 100
            
            var fee = Math.round(distance * 50 * 100) / 100;

            addRow(res.ContractorFullName, fee, distance, res.AverageRating.toFixed(1), res.ResponseStatus, res.ContractorID);
        }
    });

}

function addRow(fullName, fee, distance, rating, status, contractorID) {
    // Create new row
    var tr = document.createElement('tr');

    var rowContent = "<th scope='row'>" + fullName + "</th>" +
        "<td>$" + fee + "</td >" +
        "<td>" + distance + "</td>" +
        "<td>" + rating + " <i class='fas fa-star' style = 'color: greenyellow'></i ></td > " +
        "<td><button class='btn btn-outline-primary' data-toggle='modal' data-target='#ReviewModalCenter' data-whatever = '" + contractorID +"' >View Reviews</button></td>";

    if (status == 'Waiting') {
        rowContent +=
            "<td>" +
            "    <button class='btn btn-warning statusButton' type='button' disabled>" +
            "        <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>" +
            "        Waiting..." +
            "    </button>" +
            "</td>";
    } else if (status == 'Busy') {
        rowContent +=
            "<td>" +
            "<button type='button' class='btn btn-danger statusButton' disabled>" +
            "    Busy" +
            "</button>" +
            "</td >";
    } else {
        rowContent +=
            "<td>" +
            "    <button type='button' class='btn btn-success statusButton' data-toggle='modal' data-target='#ModalCenter' data-whatever='" + fullName + "@" + fee + "@"+ contractorID +"'>" +
            "        Accepted" +
            "    </button>" +
            "</td >";
    }

    tr.innerHTML = rowContent;

    document.getElementById('availableContractorsTable').appendChild(tr);
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

$(document).ready(function () {

    // Filter status
    $('#applyFilterButton').click(function () {

        var statuses = [];
        if ($('#BusyCheckBox').prop('checked')) {
            statuses.push('Busy')
        }

        if ($('#WaitingCheckBox').prop('checked')) {
            statuses.push('Waiting')
        }

        if ($('#AcceptedCheckBox').prop('checked')) {
            statuses.push('Accepted')
        }

        console.log(statuses);

        var table = document.getElementById('availableContractorsTable').childNodes;

        for (var i = 1; i < table.length; i++) {
            var rowStatus = table[i].childNodes[6].textContent;

            var display = false;
            for (var j = 0; j < statuses.length; j++) {
                if (rowStatus.includes(statuses[j])) {
                    display = true;
                    break;
                }
            }

            console.log(i + ' ' + display);

            if (display) {
                table[i].style.display = 'table-row';
            } else {
                table[i].style.display = 'none';
            }
        }
    });

    // Sorting columns
    var sortingAscOrder = [true, true, true, true]

    $('.sortable').click(function (e) {
        var columnName = $(this).text();
        var columnNum = 0;

        if (columnName == 'Contractor ') {
            columnNum = 0;
        }

        if (columnName == 'Consultation Fee ') {
            columnNum = 1;
        }

        if (columnName == 'Distance (KM) ') {
            columnNum = 2;
        }

        if (columnName == 'Rating ') {
            columnNum = 3;
        }

        sortingAscOrder[columnNum] = !sortingAscOrder[columnNum];

        sortByColumn(columnNum);
    });

    function sortByColumn(columnNum) {
        var table = document.getElementById('availableContractorsTable').childNodes;

        for (var i = 1; i < table.length - 1; i++) {

            if (table[i].style.display != 'none') {

                var valuei = table[i].childNodes[columnNum].textContent;
                valuei = convertToRightInfo(valuei, columnNum);

                console.log(valuei);

                var bestIdx = i;
                var valueBestIdx = valuei;

                for (var j = i + 1; j < table.length; j++) {

                    if (table[j].style.display != 'none') {
                        var valuej = table[j].childNodes[columnNum].textContent;
                        valuej = convertToRightInfo(valuej, columnNum);

                        if (sortingAscOrder[columnNum]) {
                            if (valueBestIdx > valuej) {
                                bestIdx = j;
                                valueBestIdx = valuej;
                            }
                        } else {
                            if (valueBestIdx < valuej) {
                                bestIdx = j;
                                valueBestIdx = valuej;
                            }
                        }

                    }

                }

                var tmp = table[i].innerHTML;
                table[i].innerHTML = table[bestIdx].innerHTML;
                table[bestIdx].innerHTML = tmp;
            }

        }
    }

    function convertToRightInfo(value, columnNum) {
        if (columnNum == 1) {
            value = value.replace('$', '');
            value = parseFloat(value);
        }

        if (columnNum == 2 || columnNum == 3) {
            value = parseFloat(value);
        }

        return value;
        
    }

    $('#CancelRequestButton').click(function (e) {
        CustomerService.cancelRequest();
        window.location.href = './CustomerHomepage.aspx';
    });

    $('#ModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var contractorData = button.data('whatever') // Extract info from data-* attributes
        var datas = contractorData.split('@');
        var fullName = datas[0];
        var fee = datas[1];
        var contractorID = datas[2];

        var modal = $(this)
        modal.find('#confirmContractorName').text(fullName);
        modal.find('#confirmFee').text(fee);
        modal.find('.submitButton').attr('id', 'submitButton' + contractorID);
    });

    $('.submitButton').click(function (e) {
        var contractorID = $(this).attr('id');
        contractorID = contractorID.replace('submitButton', '');
        contractorID = parseInt(contractorID);

        var cost = $('#confirmFee').text();
        cost = parseFloat(cost);

        CustomerService.acceptPayment(contractorID, cost);
        window.location.href = './InProgressTransaction.aspx';
    });

    $('#ReviewModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var contractorID = button.data('whatever') // Extract info from data-* attributes

        var modal = $(this)
        modal.find('#reviews').html('');
        CustomerService.getReviews(contractorID, function (result) {
            var res = JSON.parse(result);

            for (var i = 0; i < res.length; i++) {
                var firstRow = 
                    "<div class='row'>" +
                        "<div class='col-1'>"+
                            "<i class='fas fa-user-circle fa-2x'></i>" +
                        "</div>" +
                        "<div class='col'>" +
                            "<span style='color: steelblue; font-size: 20px'>" + res[i].CustomerFullName + "</span>" +
                        "</div>" +
                    "</div>";

                var secondRow = 
                    "<div class='row'>" +
                        "<div class='col-1'>" +
                        "</div>" +
                        "<div class='col' style='background-color: whitesmoke; border-radius: 10px; padding: 10px;'>" +
                            res[i].ReviewDesc +
                        "</div>" +
                    "</div> <br />";

                modal.find('#reviews').append(firstRow);
                modal.find('#reviews').append(secondRow);
            }

        });
    });
});