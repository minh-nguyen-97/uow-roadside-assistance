window.onload = function (e) {
    LoggedOffService.getUserFromSession(function (session) {
        if (session != null) {
            var curUser = JSON.parse(session);
            if (curUser.UserType == 'Customer') {
                window.location.href = '../LoggedOn/Customer/CustomerHomepage.aspx';
            } else {
                window.location.href = '../LoggedOn/Contractor/ContractorHomepage.aspx';
            }
            alert("You are already logged in !!!");
        }
    });
};