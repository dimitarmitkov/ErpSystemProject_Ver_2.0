let button = document.getElementById("submitButton");

button.addEventListener("click", (e) => {
    let customerInput = document.getElementById("allCustomer").value;
    let productInput = document.getElementById("allProduct").value;
    let afntyForgery = document.getElementById("antyForgeryForm");
    let transfer = { customer: customerInput, product: productInput }

    var antyForgeryToken = afntyForgery.firstChild.value;

    const uri = '/api/SalesApi';

    fetch(uri, {
        method: 'POST',
        headers: {
            "X-CSRF-TOKEN": antyForgeryToken,
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(transfer)
    })
        .then(response => response.json())
        .then(() => { this.getData() })
        .catch(error => console.error('Unable to add item.', error));
});

function getData() {
    fetch("/SalesRest/SaleRestReturn")
        .then(response => response.json())
        .then(jsonDataInput => {
            let tableElement = document.getElementById("tableBody");
            let first = Object.entries(jsonDataInput);
            var options = {
                year: "numeric",
                month: "2-digit",
                day: "2-digit"
            };

            let listOfSales = first[0][1];
            for (let i = 0; i < listOfSales.length; i++) {
                tableElement.innerHTML += `<tr>
                                <td>${listOfSales[i].customer}</td>
                                <td>${listOfSales[i].product}</td>
                                <td>${listOfSales[i].productMesure}</td>
                                <td>${listOfSales[i].numberOfSoldProducts}</td>
                                <td>${(new Date(listOfSales[i].saleDate)).toLocaleString("en", options)}</td>
                                <td>${(listOfSales[i].singleProudctSalePrice).toLocaleString('en-US', { minimumFractionDigits: 2 })}</td>
                                <td>${(listOfSales[i].totalSalePrice).toLocaleString('en-US', { minimumFractionDigits: 2 })}</td>
                            </tr>`
            }

        })
        .catch(err => console.log(err))
}
