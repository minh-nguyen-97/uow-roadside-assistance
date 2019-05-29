
var transactionsLineCtx = document.getElementById('transactionLineChart').getContext('2d');
var transactionsLineChart = null;


function plotTransactionsLineChart(dataValues) {

    if (transactionsLineChart == null) {
        transactionsLineChart = new Chart(transactionsLineCtx, {
            // The type of chart we want to create
            type: 'line',

            // The data for our dataset
            data: {
                labels: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN'],
                datasets: [{
                    borderColor: 'rgb(255, 99, 132)',
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

        return;
    }

    transactionsLineChart.data.datasets = [{
        borderColor: 'rgb(255, 99, 132)',
        data: dataValues
    }];

    transactionsLineChart.update();
}
