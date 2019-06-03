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

    ContractorService.getReviewAndRating(transactionID, function (result) {
        var tr = document.createElement('tr');
        tr.id = 'rowFor' + transactionID;

        var rowContent =
            "<th scope = 'row'>" + customerFullName + "</th>" +
            "<td>$" + cost + "</td>" +
            "<td>" + transactionDate + "</td>";

        var res = JSON.parse(result);

        if (res == null) {
            rowContent +=
                "<td>" +
                "<button type='button' class='btn btn-secondary' disabled style='width:100%'>" +
                "Review for this transaction is deleted" +
                "</button>" +
                "</td>";
        } else {

            if (res.Appeal == false) {
                rowContent +=
                    "<td>" +
                    "<button class='btn btn-outline-primary' style='width:49%' data-toggle='modal' data-target='#ModalCenter' data-transaction='" + transactionID + "'>" +
                    "View Review & Rating" +
                    "</button> &nbsp;" +
                    "<button class='btn btn-outline-danger' style='width:49%' data-toggle='modal' data-target='#AppealModalCenter'  data-transaction='" + transactionID + "'> " +
                    "Appeal" +
                    "</button> " +
                    "</td>";
            } else {
                rowContent +=
                    "<td>" +
                    "    <button class='btn btn-warning statusButton' type='button' disabled style='width:100%'>" +
                    "        <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>" +
                    "        Being Re-assessed..." +
                    "    </button>" +
                    "</td>"
            }
        }

        tr.innerHTML = rowContent;

        document.getElementById('pastCompletedTransactionTable').appendChild(tr);

        sortByColumn(2);

    });
   
}

function convertToRightInfo(value, columnNum) {
    if (columnNum == 1) {
        value = value.replace('$', '');
        value = parseFloat(value);
    }

    return value;

}

var sortingAscOrder = [true, true, false]

function sortByColumn(columnNum) {
    var table = document.getElementById('pastCompletedTransactionTable').childNodes;

    for (var i = 1; i < table.length - 1; i++) {

        if (table[i].style.display != 'none') {

            var valuei = table[i].childNodes[columnNum].textContent;
            valuei = convertToRightInfo(valuei, columnNum);

            //console.log(valuei);

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

$(document).ready(function () {

    // sort column

    $('.sortable').click(function (e) {
        var columnName = $(this).text();
        var columnNum = 0;

        if (columnName == 'Customer ') {
            columnNum = 0;
        }

        if (columnName == 'Cost ') {
            columnNum = 1;
        }

        if (columnName == 'Transaction Date Time ') {
            columnNum = 2;
        }

        sortingAscOrder[columnNum] = !sortingAscOrder[columnNum];

        sortByColumn(columnNum);
    });


    //Review and rating
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

    // Appeal
    $('#AppealModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var transactionID = button.data('transaction') // Extract info from data-* attributes

        var modal = $(this);

        modal.find('.submitAppealButton').attr('id', 'submitAppealButton' + transactionID);

    });

    $('.submitAppealButton').click(function (e) {
        var transactionID = $(this).attr('id').replace('submitAppealButton', '')
        var reason = $('#appealReason').val();
        if (reason == '') {
            $('#appealReasonErrMess').text('Appeal Reason is required !!!');
        } else {
            $('#appealReasonErrMess').text('');
            ContractorService.appealReview(transactionID, reason);

            var appealedRow = document.getElementById('rowFor' + transactionID);
            appealedRow.childNodes[3].innerHTML =
                "    <button class='btn btn-warning statusButton' type='button' disabled style='width:100%'>" +
                "        <span class='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>" +
                "        Being Re-assessed..." +
                "    </button>";

            $('#AppealModalCenter').modal('toggle');
        }
    });


});