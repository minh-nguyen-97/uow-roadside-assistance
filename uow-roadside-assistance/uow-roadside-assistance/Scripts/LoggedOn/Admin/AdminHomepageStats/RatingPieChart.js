// Rating Pie Chart
var pieCtx = document.getElementById('ratingPieChart');
var pieData = null;
var pieChart = null;

function plotRatingPieChart(totalTransactions, ratedTransactions, goodRatings) {

    var pieDataValues = [goodRatings, ratedTransactions - goodRatings, totalTransactions - ratedTransactions]//[totalTransactions, numberOfGoodRating];

    if (pieChart == null) {
        pieData = {
            datasets: [{
                data: pieDataValues,
                backgroundColor: ['mediumseagreen', 'orangered', 'gray']
            }],

            // These labels appear in the legend and in the tooltips when hovering different arcs
            labels: [
                'Good Rating',
                'Bad Rating',
                'Unrated'
            ]
        };

        pieChart = new Chart(pieCtx, {
            // The type of chart we want to create
            type: 'pie',

            // The data for our dataset
            data: pieData,

            // Configuration options go here
            options: {
                responsive: true,
                legend: {
                    position: 'bottom',
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                }
            }

        });

        return;
    }

    pieChart.data.datasets = [{
        data: pieDataValues,
        backgroundColor: ['mediumseagreen', 'orangered', 'gray']
    }]

    pieChart.update();

}
