$(document).ready(function () {
    $('#signInSubmit').click( function(e) {
        var username = $('#Username').val();
        var password = $('#Password').val();
        LoggedOffService.logIn(username, password, onLogIn);
    });

    function onLogIn(res) {
        if (res) {
            var username = $('#Username').val();
            LoggedOffService.setSession(username);
            LoggedOffService.getUserTypeFromSession(function (userType) {
                if (userType == 'Customer')
                    window.location.href = '../LoggedOn/Customer/CustomerHomepage.aspx';
                else
                    window.location.href = '../LoggedOn/Contractor/ContractorHomepage.aspx';
            })
            
        }
        else {
            alert('Username or password is wrong!!!')
        }
    }
});