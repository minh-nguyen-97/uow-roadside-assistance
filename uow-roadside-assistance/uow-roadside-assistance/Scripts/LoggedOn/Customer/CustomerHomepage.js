function redirectToRightPlace() {
    CustomerService.getTheRightRedirect(function (res) {
        if (res == 'InProgressTransaction.aspx') {
            alert('You have not completed your previouse transaction!!!');
        } else if (res == 'BrowseAvailable.aspx') {
            alert('You have not completed your previouse request!!!');
        }

        window.location.href = res;
    });
}