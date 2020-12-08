document.addEventListener("click", eventHandler);

let table = document.getElementsByTagName("table")[0];
let thead = document.getElementsByTagName("thead")[0];
let customerSpan = document.getElementById("customerSpan");

table.style.fontSize = "0.9vw";
thead.style.textAlign = "center";
customerSpan.style.fontWeight = "bold";

let tableBody = table.getElementsByTagName("tbody").item(0);
let howManyRows = tableBody.rows.length;

console.log(howManyRows);

for (let i = 0; i < howManyRows; i++) {
    let thisTrElem = tableBody.rows[i];
    let thisTdElem = thisTrElem.cells[3];
    console.log(thisTdElem)

    let thisTextNode = thisTdElem.childNodes.item(0);

    console.log(thisTdElem, "td element");
    console.log(thisTextNode);

    if (thisTextNode.textContent) {
        console.log(thisTextNode.textContent);

        let inputDate = thisTextNode.textContent;
        let dateChanged = new Date(inputDate)
        let updatedDate = date.getDate() + " " + ((date.getMonth() + 1) < 10 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1)) + " " + date.getFullYear();
        console.log(updatedDate);

        thisTdElem.innerHTML = updatedDate;
    }
}

function eventHandler(e) {
    let buttonTag = (e.target).tagName;
    let buttonText = (e.target).innerHTML;
    let number = e.target.name;
    let trList = document.getElementsByTagName("tr");
    let idList = [];

    for (let i = 1; i < trList.length; i++) {
        idList.push(trList[i].id);
    }

    if (buttonTag === "A" && buttonText === "Select") {

        for (let i = 0; i < idList.length; i++) {
            if (parseInt(idList[i]) !== parseInt(number)) {
                document.getElementById(`${parseInt(idList[i])}`).remove();
            }
            else {

                document.getElementById(`${parseInt(idList[i])}`).lastElementChild.innerHTML = '<td> <button type="submit" class="btn btn-primary btn-block" id="buttonSale">Sale</button> </td>'
            }
        }

        let thElement = document.getElementsByTagName("th");
        for (let i = 0; i < thElement.length; i++) {
            thElement[i].classList.remove("sorting");
            thElement[i].classList.remove("sorting_asc");
        }
        document.getElementById("dataTable_paginate").style.display = "none";
        document.getElementById("dataTable_info").style.display = "none";
        document.getElementById("dataTable_length").style.display = "none";
        document.getElementById("dataTable_filter").style.display = "none";
    }


    let inputNumber = document.getElementById("productSold").addEventListener("keyup", eventHandlerKeyup);
    let availableProducts = document.getElementById("productsAvailable").textContent;

    function validateInput(numberOfProducts) {
        const reg = /^([0-9]+)$/;
        return reg.test(numberOfProducts);
    }

    function eventHandlerKeyup() {

        if (!(validateInput(this.value) && parseInt(this.value) <= parseInt(availableProducts)) && trList.length <= 2) {
            document.getElementById("buttonSale").style.display = "none";
        }
        else {
            document.getElementById("buttonSale").style.display = "block";
        }
    }
    eventHandlerKeyup(inputNumber)
}
