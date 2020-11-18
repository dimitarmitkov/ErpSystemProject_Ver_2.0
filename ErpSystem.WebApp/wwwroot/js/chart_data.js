document.addEventListener("click", eventHandler);
let startDateField = document.getElementById("inputStartDate");
let endDateField = document.getElementById("inputEndDate");
let arrFromHtml = document.getElementById("jsArray").innerText;

function eventHandler(e) {

    if (e.target.id === "submit") {

        fetch("https://localhost:35811/Charts/JsonChart")
            .then(response => response.json())
            .then(jsonDataInput => {

                let resultObjectWithDateAndAmountKeys = Object.keys(jsonDataInput).map(e => ({ date: e, amount: jsonDataInput[e] }));
                let startDate = new Date(startDateField.value);
                let endDate = new Date(endDateField.value);
                let dayAmount = {};
                let resulObjectOfDaysWithoutAmount = {};
                let daysWithoutAmountArray = [];

                // create result array of objects 
                for (var d = startDate; d <= endDate; d.setDate(d.getDate() + 1)) {
                    new Date(d).getDate() < 10 ?
                        dayAmount[`0${new Date(d).getDate()}-${new Date(d).getMonth() + 1}-${new Date(d).getFullYear()}`] = 0 :
                        dayAmount[`${new Date(d).getDate()}-${new Date(d).getMonth() + 1}-${new Date(d).getFullYear()}`] = 0;
                    daysWithoutAmountArray.push(Object.keys((dayAmount)));
                    dayAmount = {};
                }
                // end of create result array


                // add zero values to all array items 
                for (var i = 0; i < daysWithoutAmountArray.length; i++) {
                    resulObjectOfDaysWithoutAmount[daysWithoutAmountArray[i]] = 0;
                }

                let resultObjectDaysWithAmount = Object.keys(resulObjectOfDaysWithoutAmount).map(e => ({ date: e, amount: resulObjectOfDaysWithoutAmount[e] }));
                // end of add zero


                // combine two arrays with adding dates with amount into arra with zero amount
                function newArray(arr1, arr2) {
                    return arr2.reduce((result, obj2) => {
                        if (arr1.some(obj1 => obj1['date'] === obj2['date'])) {
                            return result;
                        }
                        return [...result, { ['date']: obj2['date'], 'amount': obj2['amount'] }];
                    }, arr1)
                }

                const resultArray = newArray(resultObjectWithDateAndAmountKeys, resultObjectDaysWithAmount);
                // end of array combination


                // create sortable array and sort it ascending
                let sortable = [];
                Object.entries(resultArray).map(x => sortable.push(x[1]));

                sortable = sortable.sort((a, b) => ((a.date) > (b.date)) ? 1 : (((b.date) < (a.date)) ? -1 : 0));
                // end of sortable array


                let labelsArray = [];
                let dataArray = [];

                for (let i = 0; i < sortable.length; i++) {
                    labelsArray.push(Object.values(sortable[i])[0]);
                    dataArray.push(Object.values(sortable[i])[1]);
                }

                var ctx = document.getElementById('myBarChart');
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: labelsArray,
                        datasets: [{
                            label: 'sales',
                            data: dataArray,

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

            })
            .catch(err => console.log(err))
    }
}

