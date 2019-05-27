$(document).ready(function () {
    if ($('#verticalNavBar .active .selectedLi').hasClass('bg-dark')) {
        $('#verticalNavBar .active .selectedLi').removeClass('bg-dark');
        $('#verticalNavBar .active .selectedLi').addClass('bg-success');
    };

    $('#logOut').click(function (e) {
        AdminService.logOut();
        window.location.href = '../../LoggedOff/Home.aspx';
    });
})