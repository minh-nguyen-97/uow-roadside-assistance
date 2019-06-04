function plotAvgRatingLineChart(dataValues) {

    for (var i = 0; i < dataValues.length; i++) {
        dataValues[i] = dataValues[i].toFixed(2);
    }

    var avgRatingLineCtx = document.getElementById('avgRatingLineChart').getContext('2d');

    var avgRatingLineChart = new Chart(avgRatingLineCtx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN'],
            datasets: [{
                borderColor: 'rgba(54, 162, 235, 1)',
                backgroundColor: 'rgba(54, 162, 235, 0.2)',
                data: dataValues
            }]
        },

        // Configuration options go here
        options: {
            legend: {
                display: false
            }
        }
    });

}
