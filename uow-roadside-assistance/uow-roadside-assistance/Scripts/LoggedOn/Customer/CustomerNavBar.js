$(document).ready(function () {
    $('#logOutLink').click(function (e) {
        CustomerService.logOut();
        window.location.href = '../../LoggedOff/Home.aspx';
    })
});