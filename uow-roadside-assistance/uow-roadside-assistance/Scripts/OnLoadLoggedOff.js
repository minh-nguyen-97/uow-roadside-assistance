window.onload = function (e) {
    LoggedOffService.getUserFromSession(function (session) {
        if (session != null) {
            var curUser = JSON.parse(session);
            if (curUser.UserType == 'Customer') {
                window.location.href = '../LoggedOn/Customer/CustomerHomepage.aspx';
            } else if (curUser.UserType == 'Contractor') {
                window.location.href = '../LoggedOn/Contractor/ContractorHomepage.aspx';
            } else {
                window.location.href = '../LoggedOn/Admin/AdminHomepage.aspx';
            }
            alert("You are already logged in !!!");
        }
    });
};