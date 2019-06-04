window.onload = function () {
    AdminService.getUserFromSession(function (session) {
        if (session == null) {
            window.location.href = '../../LoggedOff/Home.aspx'
        } else {
            var curUser = JSON.parse(session);
            if (curUser.UserType != 'Admin') {
                window.history.back();
            }
            else {
                // do stuff

                loadAllInformation();
            }
        }
    });
}

function loadAllInformation() {
    loadTransactionsBarChart();
    loadAvgRatingsLineChart();
    loadRatingPieChart();
}

function loadTransactionsBarChart() {
    AdminService.getAllTransactions(function (result) {
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

        plotTransactionsBarChart([jan, feb, mar, apr, may, jun]);
    })
}

function loadAvgRatingsLineChart() {
    AdminService.GetAllRatings(function (result) {
        var res = JSON.parse(result);

        var jan = 0;
        var feb = 0;
        var mar = 0;
        var apr = 0;
        var may = 0;
        var jun = 0;

        var janRating = 0;
        var febRating = 0;
        var marRating = 0;
        var aprRating = 0;
        var mayRating = 0;
        var junRating = 0;

        for (var i = 0; i < res.length; i++) {
            if (res[i].ReviewDateToString.includes('2019/01')) {
                jan++;
                janRating += res[i].Rating;
            }
            if (res[i].ReviewDateToString.includes('2019/02')) {
                feb++;
                febRating += res[i].Rating;
            }
            if (res[i].ReviewDateToString.includes('2019/03')) {
                mar++;
                marRating += res[i].Rating;
            }
            if (res[i].ReviewDateToString.includes('2019/04')) {
                apr++;
                aprRating += res[i].Rating;
            }
            if (res[i].ReviewDateToString.includes('2019/05')) {
                may++;
                mayRating += res[i].Rating;
            }
            if (res[i].ReviewDateToString.includes('2019/06')) {
                jun++;
                junRating += res[i].Rating;
            }
        }

        janRating = janRating / jan;
        febRating = febRating / feb;
        marRating = marRating / mar;
        aprRating = aprRating / apr;
        mayRating = mayRating / may;
        junRating = junRating / jun;
        
        
        plotAvgRatingLineChart([janRating, febRating, marRating, aprRating, mayRating, junRating]);
    });
}

function loadRatingPieChart() {
    AdminService.GetRatingsStatsReport(function (result) {
        var res = JSON.parse(result);

        var totalTransactions = res[0];
        var ratedTransactions = res[1];
        var goodRatings = res[2];

        $('#TotalTransactions').text(totalTransactions);
        $('#RatedTransactions').text(ratedTransactions);
        $('#GoodRating').text(goodRatings);

        plotRatingPieChart(totalTransactions, ratedTransactions, goodRatings);

    });
}