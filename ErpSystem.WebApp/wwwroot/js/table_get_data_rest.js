function getData() {
    fetch("/api/SalesApi")
        .then(response => response.json())
        .then(jsonDataInput => {
            console.log(jsonDataInput)
        })
        .catch(err => console.log(err))
}

getData();