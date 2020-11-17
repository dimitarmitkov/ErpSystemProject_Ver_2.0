﻿let table = document.getElementsByTagName("table")[0];
let thead = document.getElementsByTagName("thead")[0];
let customerSpan = document.getElementById("customerSpan");

table.style.fontSize = "0.9vw";
thead.style.textAlign = "center";
customerSpan.style.fontWeight = "bold";


document.addEventListener("click", eventHandler);


function eventHandler(e) {
    console.log(e.target.name);
    let buttonTag = (e.target).tagName;
    console.log(buttonTag);

    let number = e.target.name;

    console.log(number, "number");

    let trList = document.getElementsByTagName("tr");
    let idList = [];

    for (let i = 1; i < trList.length; i++) {
        idList.push(trList[i].id);
    }

    console.log(idList);


    if (buttonTag === "A") {
        for (let i = 0; i < idList.length; i++) {
            if (parseInt(idList[i]) !== parseInt(number)) {
                document.getElementById(`${parseInt(idList[i])}`).remove();
            }
            else {

                let button = document.createElement()
                document.getElementById(`${parseInt(idList[i])}`).lastElementChild.innerHTML = '<button type="submit" class="btn btn - primary btn - block">Sale</button>'
            }
        }
    }

}