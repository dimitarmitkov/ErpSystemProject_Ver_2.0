function chart() {

    fetch("/Warehouses/JsonChartBoxes")
        .then(response => response.json())
        .then(jsonDataInput => {

            let labelsArray = Object.keys(jsonDataInput);
            let dataArray = Object.values(jsonDataInput);

            var ctx = document.getElementById("myPieChartBoxes");
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labelsArray,
                    datasets: [{
                        data: dataArray,
                        backgroundColor: ['rgb(245, 52 , 58)', 'rgb(28, 129, 235)'],
                    }],
                },
            });
        })
        .catch(err => console.log(err))

    fetch("/Warehouses/JsonChartPallets")
        .then(response => response.json())
        .then(jsonDataInput => {

            let labelsArray = Object.keys(jsonDataInput);
            let dataArray = Object.values(jsonDataInput);

            var ctx = document.getElementById("myPieChartPallets");
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: labelsArray,
                    datasets: [{
                        data: dataArray,
                        backgroundColor: ['rgb(245, 52 , 58)', 'rgb(28, 129, 235)'],
                    }],
                },
            });
        })
        .catch(err => console.log(err))
}

chart();