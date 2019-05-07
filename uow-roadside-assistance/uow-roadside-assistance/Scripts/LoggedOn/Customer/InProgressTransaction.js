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
    $('#ratingButton').click(function (e) {
        CustomerService.customerFinishedTransaction();
        window.location.href = 'CustomerHomepage.aspx';
    });
});