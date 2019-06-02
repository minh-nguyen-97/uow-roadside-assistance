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
    //console.log(contractorFullName + ' ' + cost + ' ' + transactionDate + ' ' + transactionID);

    var tr = document.createElement('tr');

    var rowContent =
        "<th scope = 'row'>" + contractorFullName +"</th>" + 
        "<td>$" + cost + "</td>" +
        "<td>" + transactionDate + "</td>" +
        "<td>" +
        "<button class='btn btn-outline-primary' data-toggle='modal' data-target='#ModalCenter' data-transaction='" + transactionID + "'>" +
                "View / Modify" +
            "</button >" +
        "</td > ";

    tr.innerHTML = rowContent;

    document.getElementById('pastCompletedTransactionTable').appendChild(tr);
}

$(document).ready(function () {

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