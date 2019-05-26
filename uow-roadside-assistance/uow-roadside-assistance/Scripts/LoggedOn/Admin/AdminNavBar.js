$(document).ready(function () {
    if ($('#verticalNavBar .active .selectedLi').hasClass('bg-dark')) {
        $('#verticalNavBar .active .selectedLi').removeClass('bg-dark');
        $('#verticalNavBar .active .selectedLi').addClass('bg-success');
    };
})