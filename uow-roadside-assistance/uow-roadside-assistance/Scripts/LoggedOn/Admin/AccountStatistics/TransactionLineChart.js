function plotTransactionsLineChart(dataValues) {
    transactionsLineChart.data.datasets = [{
        borderColor: 'rgb(255, 99, 132)',
        data: dataValues
    }];

    transactionsLineChart.update();
}

var transactionsLineCtx = document.getElementById('transactionLineChart').getContext('2d');
var transactionsLineChart = new Chart(transactionsLineCtx, {
    // The type of chart we want to create
    type: 'line',

    // The data for our dataset
    data: {
        labels: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN'],
        datasets: [{
            borderColor: 'rgb(255, 99, 132)',
            data: [5, 10, 5, 2, 20, 30]
        }]
    },

    // Configuration options go here
    options: {
        legend: {
            display: false
        }
    }
});