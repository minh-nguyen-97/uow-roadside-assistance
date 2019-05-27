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
            }
        }
    });
}