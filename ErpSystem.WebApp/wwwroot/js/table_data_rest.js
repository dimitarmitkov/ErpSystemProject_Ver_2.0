let button = document.getElementById("submitButton");

button.addEventListener("click", (e) => {
    let customerInput = document.getElementById("allCustomer").value;
    let productInput = document.getElementById("allProduct").value;
    let afntyForgery = document.getElementById("antyForgeryForm");
    let transfer = { customer: customerInput, product: productInput }

    var antyForgeryToken = afntyForgery.firstChild.value;

    console.log(antyForgeryToken);

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
        .catch(error => console.error('Unable to add item.', error));
});

function getData() {
    fetch("https://localhost:35811/SalesApi/SaleRest")
        .then(response => response.json())
        .then(jsonDataInput => {
            console.log(jsonDataInput)
        })
        .catch(err => console.log(err))
}

getData();
