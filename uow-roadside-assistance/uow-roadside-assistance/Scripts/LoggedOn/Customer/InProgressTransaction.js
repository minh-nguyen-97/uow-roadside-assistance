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
                var refer1 = refer.replace('InProgressTransaction', 'BrowseAvailable');
                var refer2 = refer.replace('InProgressTransaction', 'CustomerHomepage');

                if (document.referrer != refer1 && document.referrer != refer2) {
                    window.history.back();
                } else {
                    $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                    hideOrShowAppropriateNotification();
                }
            }
        }
    });
}

function hideOrShowAppropriateNotification() {
    CustomerService.getCustomerUnfinishedTransaction(function (res) {
        var transaction = JSON.parse(res);
        if (transaction.ContractorFinished) {
            $('#unfinished').hide();
            $('finished').show();
        } else {
            $('#unfinished').show();
            $('#finished').hide();
        }
    });
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

    $('#submitButton').click(function (e) {

        var rating = -1;
        for (var i = 5; i >= 1; i--) {
            var starID = '#star' + i;
            if ($(starID).hasClass('fas')) {
                rating = i;
                break;
            } 
        }

        if (rating < 0) {
            $('#ratingErrMess').text('Rating is required !!!');
        } else {
            $('#ratingErrMess').text('');

            var reviewDesc = $('#reviewDesc').val();

            CustomerService.reviewAndRating(reviewDesc, rating);
            CustomerService.customerFinishedTransaction();
            window.location.href = 'CustomerHomepage.aspx';
        }
    });


});