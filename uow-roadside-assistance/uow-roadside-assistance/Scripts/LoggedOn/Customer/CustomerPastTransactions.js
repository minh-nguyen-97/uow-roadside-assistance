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
                $('#UserNameLabel').text('Welcome, ' + curUser.FullName);

                loadPastTransactions();
            }
        }
    });
}

function loadPastTransactions() {
    CustomerService.getAllPastCompletedTransactions(function (result) {
        var res = JSON.parse(result);

        for (var i = 0; i < res.length; i++) {
            addTransactionRow(res[i].ContractorFullName, res[i].Cost, res[i].TransactionDateToString, res[i].TransactionID)
        }
    });
}

function addTransactionRow(contractorFullName, cost, transactionDate, transactionID) {
    console.log(contractorFullName + ' ' + cost + ' ' + transactionDate + ' ' + transactionID);

    CustomerService.getReviewAndRating(transactionID, function (result) {

        var tr = document.createElement('tr');

        var rowContent =
            "<th scope = 'row'>" + contractorFullName + "</th>" +
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
                    "<button class='btn btn-outline-primary' style='width:100%' data-toggle='modal' data-target='#ModalCenter' data-transaction='" + transactionID + "'>" +
                    "View / Modify" +
                    "</button >" +
                    "</td > ";
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

$(document).ready(function () {

    // sort column

    $('.sortable').click(function (e) {
        var columnName = $(this).text();
        var columnNum = 0;

        if (columnName == 'Contractor ') {
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
    $(".star").on("mouseover", function () {
        var id = $(this).attr('id');
        id = id.replace('star', '');

        for (var i = 1; i <= id; i++) {
            var starID = '#star' + i;
            if (!$(starID).hasClass("fixed-rate")) {
                $(starID).removeClass('far');
                $(starID).addClass("fas");
            }
        }
    });

    $(".star").on("mouseout", function () {
        var id = $(this).attr('id');
        id = id.replace('star', '');

        for (var i = 1; i <= id; i++) {
            var starID = '#star' + i;
            if (!$(starID).hasClass("fixed-rate")) {
                $(starID).removeClass('fas');
                $(starID).addClass("far");
            }
        }
    });

    $(".star").on("click", function () {
        var id = $(this).attr('id');
        id = id.replace('star', '');
        id = parseInt(id);

        for (var i = 1; i <= id; i++) {
            var starID = '#star' + i;
            if (!$(starID).hasClass("fixed-rate")) {
                $(starID).addClass("fixed-rate");
                if ($(starID).hasClass('far')) {
                    $(starID).removeClass('far');
                    $(starID).addClass("fas");
                }
            }
        }

        for (var i = id + 1; i <= 5; i++) {
            var starID = '#star' + i;
            if ($(starID).hasClass("fixed-rate")) {
                $(starID).removeClass("fixed-rate");
                if ($(starID).hasClass('fas')) {
                    $(starID).removeClass('fas');
                    $(starID).addClass("far");
                }
            }
        }
    });

    $('.submitButton').click(function (e) {

        var rating = -1;
        for (var i = 5; i >= 1; i--) {
            var starID = '#star' + i;
            if ($(starID).hasClass('fas')) {
                rating = i;
                break;
            }
        }

        var review = $('#reviewDesc').val()

        if (rating < 0) {
            $('#ratingErrMess').text('Rating is required !!!');
        } else {
            $('#ratingErrMess').text('');
        }

        if (review == '') {
            $('#reviewErrMess').text('Review is required !!!');
        } else {
            $('#reviewErrMess').text('');
        }

        if (rating > 0 && review != '') {
            var reviewDesc = $('#reviewDesc').val();

            var transactionID = $(this).attr('id');
            transactionID = parseInt(transactionID.replace('submitReviewButton', ''));

            CustomerService.updateReviewAndRating(transactionID, reviewDesc, rating);
            $('#ModalCenter').modal('toggle');
        }
    });

    $('#ModalCenter').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var transactionID = button.data('transaction') // Extract info from data-* attributes

        var modal = $(this)

        modal.find('.submitButton').attr('id', 'submitReviewButton' + transactionID);

        CustomerService.getReviewAndRating(transactionID, function (result) {
            var res = JSON.parse(result);

            $('#star' + res.Rating).trigger('click');
            $('#reviewDesc').val(res.ReviewDesc);
            
        });

    });


});