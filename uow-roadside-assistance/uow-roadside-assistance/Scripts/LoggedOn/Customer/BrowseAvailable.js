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
                CustomerService.getSessionRequest(function (res) {
                    var sessionRequest = JSON.parse(res);
                    if (sessionRequest == null) {
                        window.history.back();
                    } else {
                        $('#UserNameLabel').text('Welcome, ' + curUser.FullName);
                        loadAvailableContractors();
                    }
                });
            }
        }
    });
}

function loadAvailableContractors() {
    CustomerService.getContratorsResponses(function (res) {
        var responses = JSON.parse(res);
        alert('YO');
        for (var i = 0; i < responses.length; i++) {
            console.log(responses[i]);
            var table = document.getElementById('availableContractorsTable');

        }
    });
}

$(document).ready(function () {

    $('#CancelRequestButton').click(function (e) {
        CustomerService.cancelRequest();
        window.location.href = './MakeRequest.aspx';
    });
});