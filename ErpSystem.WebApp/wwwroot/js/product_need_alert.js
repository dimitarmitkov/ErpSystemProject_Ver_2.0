


fetch("https://localhost:35811/Sales/OrderNeeded")
    .then(response => response.json())
    .then(result => console.log(result))
    .catch(err => console.log(err))