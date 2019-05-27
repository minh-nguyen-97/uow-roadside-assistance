$(document).ready(function () {
    $('#searchButton').click(function (e) {

        var userID = $('#UserID').val();

        if (/^\d+$/.test(userID) == false) {
            $('#UserIDErrMess').text('User ID is an integer')
        } else {

            AdminService.IsExist(userID, function (res) {
                if (res) {
                    $('.card').css('visibility', 'visible');
                    loadCards(userID);
                    $('#UserIDErrMess').text('');
                } else {
                    $('#UserIDErrMess').text('User ID does not exist')
                }
            });
        }
    });
});

function loadCards(userID) {

    loadProfile(userID);

    loadDoughnutRating(userID);

    loadProblemsBarChart(userID);

    loadTransactionsLineChart(userID);
}

function loadTransactionsLineChart(userID) {
    AdminService.GetTransactionsStats(userID, function (result) {
        var res = JSON.parse(result);

        var jan = 0;
        var feb = 0;
        var mar = 0;
        var apr = 0;
        var may = 0;
        var jun = 0;

        for (var i = 0; i < res.length; i++) {
            if (res[i].TransactionDateToString.includes('2019/01'))
                jan++;
            if (res[i].TransactionDateToString.includes('2019/02'))
                feb++;
            if (res[i].TransactionDateToString.includes('2019/03'))
                mar++;
            if (res[i].TransactionDateToString.includes('2019/04'))
                apr++;
            if (res[i].TransactionDateToString.includes('2019/05'))
                may++;
            if (res[i].TransactionDateToString.includes('2019/06'))
                jun++;
        }

        var total = jan + feb + mar + apr + may + jun;
        $('#6MonthsTransactions').text(total);

        plotTransactionsLineChart([jan, feb, mar, apr, may, jun]);
    })
}

function loadProblemsBarChart(userID) {
    AdminService.GetProblemsStats(userID, function (result) {
        var res = JSON.parse(result);

        var tyre = res[0];
        var battery = res[1];
        var engine = res[2];
        var general = res[3];

        $('#TyreProblem').text(tyre);
        $('#CarBatteryProblem').text(battery);
        $('#EngineProblem').text(engine);
        $('#GeneralProblem').text(general);

        plotProblemsBarChart([tyre, battery, engine, general]);
    });
}

function loadDoughnutRating(userID) {
    AdminService.GetRatingsStats(userID, function (result) {
        var res = JSON.parse(result);
        var totalTransactions = res[0];
        var goodRating = res[1];

        $('#TotalTransactions').text(totalTransactions);
        $('#GoodRating').text(goodRating);

        plotDoughnutRating([goodRating, totalTransactions - goodRating]);
    });
}

function loadProfile(userID) {
    AdminService.GetUser(userID, function (result) {

        var res = JSON.parse(result);

        $('#UserType').text(res.UserType);
        $('#FullName').text(res.FullName);
        $('#Username').text(res.UserName);
        $('#Email').text(res.Email);

        if (res.UserType == 'Contractor') {
            $('#contractorDetails').css('display', '');
            $('#customerDetails').css('display', 'none');

            $('#AccountName').text(res.AccountName);
            $('#AccountNumber').text(res.AccountNumber);
            $('#BSB').text(res.BSB);
        } else {
            $('#contractorDetails').css('display', 'none');
            $('#customerDetails').css('display', '');

            $('#RegNo').text(res.RegNo);
            $('#Make').text(res.Make);
            $('#Model').text(res.Model);
            $('#Color').text(res.Color);

            $('#CardHolder').text(res.CardHolder);
            $('#CardNumber').text(res.CardNo);
            $('#ExpiryDate').text(res.ExpiryDate);
            $('#CVV').text(res.CVV);
        }
    });
}