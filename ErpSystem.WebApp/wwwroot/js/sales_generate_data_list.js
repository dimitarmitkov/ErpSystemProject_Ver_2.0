﻿


//document.addEventListener("click", eventHandler);

//let table = document.getElementsByTagName("table")[0];
//let thead = document.getElementsByTagName("thead")[0];
//let customerSpan = document.getElementById("customerSpan");

//table.style.fontSize = "0.9vw";
//thead.style.textAlign = "center";
//customerSpan.style.fontWeight = "bold";

//function eventHandler(e) {
//    console.log(e.target.name);
//    let buttonTag = (e.target).tagName;
//    console.log(buttonTag);

//    let number = e.target.name;

//    console.log(number, "number");

//    let trList = document.getElementsByTagName("tr");
//    let idList = [];

//    console.log(trList.length)

//    for (let i = 1; i < trList.length; i++) {
//        idList.push(trList[i].id);
//    }

//    if (buttonTag === "A") {
//        for (let i = 0; i < idList.length; i++) {
//            if (parseInt(idList[i]) !== parseInt(number)) {
//                document.getElementById(`${parseInt(idList[i])}`).remove();
//            }
//            else {

//                document.getElementById(`${parseInt(idList[i])}`).lastElementChild.innerHTML = '<td><button type="submit" class="btn btn - primary btn - block" style = "color: white;background-color: rgb(0,92,255);">Sale</button></td>'
//            }
//        }
//    }

document.addEventListener("click", eventHandler);

function eventHandler(e) {

    let id = (e.target.id).match(/\d+/);

    console.log(id[0]);

    let inputNumber = document.getElementById(`productSold${id}`).addEventListener("keyup", eventHandlerKeyup);

    let availableProducts = document.getElementById(`productsAvailable${id}`).textContent;

    function validateInput(numberOfProducts) {
        const reg = /^([0-9]+)$/;
        return reg.test(numberOfProducts);
    }

    function eventHandlerKeyup() {

        if (!(validateInput(this.value) && parseInt(this.value) <= parseInt(availableProducts))) {
            document.getElementById(`buttonSale${id}`).style.display = "none";
        }
        else {
            document.getElementById(`buttonSale${id}`).style.display = "block";
        }
    }
    eventHandlerKeyup(inputNumber)
}

//}