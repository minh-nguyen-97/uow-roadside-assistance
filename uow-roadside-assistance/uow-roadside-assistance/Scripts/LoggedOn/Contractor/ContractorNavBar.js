$(document).ready(function () {
    $('#logOutLink').click(function (e) {
        ContractorService.logOut();
        window.location.href = '../../LoggedOff/Home.aspx';
    })
});