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

                AdminService.getAppealedReviews(function (result) {
                    var res = JSON.parse(result);

                    for (var i = 0; i < res.length; i++) {
                        addRow(res[i].TransactionID, res[i].ContractorID, res[i].CustomerID);
                    }
                });
            }
        }
    });
}


function addRow(transactionID, contractorID, customerID) {

    var tr = document.createElement('tr');
    tr.setAttribute("id", "rowFor" + transactionID);

    var rowContent =
        "<td>" + transactionID + "</td>" +
        "<td><a href='AccountStatistics.aspx?uid=" + contractorID + "'>" + contractorID + "</a></td>" +
        "<td><a href='AccountStatistics.aspx?uid=" + customerID + "'>" + customerID + "</a></td>" +
        "<td>" +
        "<button class='btn btn-outline-primary' style='width:100%' data-toggle='modal' data-target='#AppealModalCenter' data-transaction='" + transactionID + "'>" +
        "View Details" +
        "</button>" +
        "</td>" +
        "<td>" +
        "<button class='btn btn-danger' onclick='rejectAppeal(" + transactionID + ")'>Reject Appeal</button>&nbsp;" +
        "<button class='btn btn-success' onclick='acceptAppeal(" + transactionID + ")'>Accept & Delete Review</button>"+
        "</td>";

    tr.innerHTML = rowContent;

    document.getElementById('allAppealedReviewsTable').appendChild(tr);
}

function rejectAppeal(transactionID) {
    AdminService.rejectAppeal(transactionID);
    deleteRow(transactionID);
}

function acceptAppeal(transactionID) {
    AdminService.acceptAppeal(transactionID);
    deleteRow(transactionID);
}

function deleteRow(transactionID) {
    var tr = document.getElementById('rowFor' + transactionID);
    document.getElementById('allAppealedReviewsTable').removeChild(tr);
}


$(document).ready(function () {

    // Appeal Modal
    $('#AppealModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var transactionID = button.data('transaction') // Extract info from data-* attributes
    

        AdminService.getReviewAndRating(transactionID, function (result) {
            var res = JSON.parse(result);

            for (var i = 1; i <= 5; i++) {
                var starID = '#star' + i;
                if ($(starID).hasClass('fas')) {
                    $(starID).removeClass('fas');
                    $(starID).addClass("far");
                }
            }

            for (var i = 1; i <= res.Rating; i++) {
                var starID = '#star' + i;
                $(starID).removeClass('far');
                $(starID).addClass("fas");
            }

            $('#reviewDesc').val(res.ReviewDesc);
            $('#appealReason').val(res.Reason);

        });
    });

    // Filter Modal
    $('#FilterModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var filterType = button.data('filtertype') // Extract info from data-* attributes

        console.log(filterType);

        $('#filterType').text(filterType);
    });

    $('#searchForID').click(function (e) {

        var ID = $('#filterID').val();

        var filterable = false;

        if ($('#filterType').text() == 'Transaction') {
            filterable = filterable || searchIDInColumn(0, ID);
        }

        if ($('#filterType').text() == 'Contractor') {
            filterable = filterable || searchIDInColumn(1, ID);
        }

        if ($('#filterType').text() == 'Customer') {
            filterable = filterable || searchIDInColumn(2, ID);
        }

        if (filterable)
            $('#FilterModalCenter').modal('toggle');
    });


    $('#clearFilterButton').click(function (e) {
        var table = document.getElementById('allAppealedReviewsTable').childNodes;

        for (var i = 1; i < table.length; i++) {
            table[i].style.display = 'table-row';
        }
    });
});

function searchIDInColumn(col, ID) {
    if (/^\d+$/.test(ID) == false) {
        alert('The ID must be an integer !!!');
        return false;
    }

    var table = document.getElementById('allAppealedReviewsTable').childNodes;

    for (var i = 1; i < table.length; i++) {
        if (table[i].style.display != 'none') {
            var idInTable = table[i].childNodes[col];

            if (idInTable.textContent == ID) {
                table[i].style.display = 'table-row';
            } else {
                table[i].style.display = 'none';
            }
        }
    }

    return true;
}