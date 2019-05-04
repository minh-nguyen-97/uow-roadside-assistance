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
            }
        }
    });
}