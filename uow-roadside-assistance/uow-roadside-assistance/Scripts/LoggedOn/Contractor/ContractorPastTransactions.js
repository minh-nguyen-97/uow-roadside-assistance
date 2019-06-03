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

                loadPastTransactions();
            }
        }
    });
}

function loadPastTransactions() {
    ContractorService.getAllPastCompletedTransactions(function (result) {
        var res = JSON.parse(result);

        for (var i = 0; i < res.length; i++) {
            addTransactionRow(res[i].CustomerFullName, res[i].Cost, res[i].TransactionDateToString, res[i].TransactionID)
        }
    });
}

function addTransactionRow(customerFullName, cost, transactionDate, transactionID) {
    //console.log(contractorFullName + ' ' + cost + ' ' + transactionDate + ' ' + transactionID);

    var tr = document.createElement('tr');

    var rowContent =
        "<th scope = 'row'>" + customerFullName + "</th>" +
        "<td>$" + cost + "</td>" +
        "<td>" + transactionDate + "</td>" +
        "<td>" +
        "<button class='btn btn-outline-primary' data-toggle='modal' data-target='#ModalCenter' data-transaction='" + transactionID + "'>" +
        "View Review & Rating" +
        "</button >" +
        "</td > ";

    tr.innerHTML = rowContent;

    document.getElementById('pastCompletedTransactionTable').appendChild(tr);
}

$(document).ready(function () {
    
    $('#ModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var transactionID = button.data('transaction') // Extract info from data-* attributes
        

        ContractorService.getReviewAndRating(transactionID, function (result) {
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

        });

    });


});