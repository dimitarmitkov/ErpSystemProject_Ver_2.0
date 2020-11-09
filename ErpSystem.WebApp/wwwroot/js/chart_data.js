document.addEventListener("click", eventHandler);
let startDateField = document.getElementById("inputStartDate");
let endDateField = document.getElementById("inputEndDate");

let arrFromHtml = document.getElementById("jsArray").innerText;

let arrFromJson = JSON.parse(arrFromHtml);

let myJsArray = Object.En

console.log(arrFromJson);


function eventHandler(e) {

    if (e.target.id === "submit") {

        let startDate = new Date(startDateField.value);
        let endDate = new Date(endDateField.value);
        let daysOfYear = [];
        let dataList = [];


        for (var d = startDate; d <= endDate; d.setDate(d.getDate() + 1)) {
            daysOfYear.push(`${new Date(d).getDate()}/${new Date(d).getMonth() + 1}`);
        }

        var ctx = document.getElementById('myBarChart');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                //labels: [1, 2, 3, 4, 5, 6, 5, 3, 4, 2, 1, 2, 5, 7, 9, 2, 1, 3, 5]
                labels: myJsArray,
                datasets: [{
                    label: 'sales',
                    data: dataList,

                    backgroundColor:
                        'rgba(255, 99, 132, 0.2)',
                    borderColor:
                        'rgba(255, 99, 132, 1)',

                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }
}

