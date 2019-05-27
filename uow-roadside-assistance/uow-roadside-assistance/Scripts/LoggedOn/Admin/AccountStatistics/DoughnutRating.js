
function plotDoughnutRating(dataValues) {
    doughnutChart.data.datasets = [{
        data: dataValues,
        backgroundColor: ['mediumseagreen', 'orangered']
    }]

    doughnutChart.update();
}

var doughnutCtx = document.getElementById('ratingDoughnutChart');
var doughnutData = {
    datasets: [{
        data: [80,27],
        backgroundColor: ['mediumseagreen', 'orangered']
    }],

    // These labels appear in the legend and in the tooltips when hovering different arcs
    labels: [
        'Good Rating',
        'Bad Rating'
    ]
};


var doughnutChart = new Chart(doughnutCtx, {
    // The type of chart we want to create
    type: 'doughnut',

    // The data for our dataset
    data: doughnutData,

    // Configuration options go here
    options: {
        responsive: true,
        legend: {
            position: 'bottom',
        },
        animation: {
            animateScale: true,
            animateRotate: true
        },
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var dataset = data.datasets[tooltipItem.datasetIndex];
                    var total = dataset.data.reduce(function (previousValue, currentValue, currentIndex, array) {
                        return previousValue + currentValue;
                    });
                    var currentValue = dataset.data[tooltipItem.index];
                    var percentage = Math.floor(((currentValue / total) * 100) + 0.5);
                    return percentage + "%";
                }
            }
        }
    }

});

