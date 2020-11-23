document.addEventListener("click", clickHandler)
let h3Element = document.getElementsByTagName("h3");
let spanElement = document.getElementsByTagName("span");


for (let j = 0; j < h3Element.length; j++) {
    h3Element[j].style.fontSize = "1vw";
}

for (let k = 0; k < spanElement.length; k++) {
    spanElement[k].style.fontWeight = "bold";
}

function clickHandler(e) {
    console.log(e.target.tagName);
    console.log(e.target.id);

    if (e.target.tagName === "BUTTON") {
        let formElementToHide = document.getElementById(`form_${e.target.id}`);

        formElementToHide.style.display = "none";
    }
}