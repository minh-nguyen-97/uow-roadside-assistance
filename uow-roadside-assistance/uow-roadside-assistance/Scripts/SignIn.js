$(document).ready(function () {
    $('#signInSubmit').click( function(e) {
        var username = $('#Username').val();
        var password = $('#Password').val();
        LoggedOffService.logIn(username, password, onLogIn);
    });

    function onLogIn(res) {
        if (res) {

            LoggedOffService.getUserFromSession(function (session) {
                var curUser = JSON.parse(session);
                if (curUser.UserType == 'Customer') {
                    window.location.href = '../LoggedOn/Customer/CustomerHomepage.aspx';
                } else if (curUser.UserType == 'Contractor') {
                    window.location.href = '../LoggedOn/Contractor/ContractorHomepage.aspx';
                } else {
                    window.location.href = '../LoggedOn/Admin/AdminHomepage.aspx';
                }
            });
        }
        else {
            alert('Username or password is wrong!!!')
        }
    }
});