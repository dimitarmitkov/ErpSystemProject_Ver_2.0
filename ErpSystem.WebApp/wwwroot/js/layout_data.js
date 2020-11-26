document.addEventListener("click", eventHandler);
let buttonLogout = document.getElementById("buttonLogout");


function eventHandler(e) {
    console.log(e.target.id);

    if (e.target.id === "buttonLogout") {
        window.location.href = "/Users/Login"
    }
}