let currentTime = document.getElementById("time");
let currentDate = document.getElementById("date");
let i = 0;
currentDate.style.color = "white";
currentTime.style.color = "white";
currentDate.style.fontWeight = "bold";
currentTime.style.fontWeight = "bold";

function startTime() {
    let today = new Date();
    let h = today.getHours();
    let m = today.getMinutes();
    let s = today.getSeconds();

    m = checkTime(m);
    s = checkTime(s);
    currentTime.innerHTML = h + ":" + m + ":" + s;
    let t = setTimeout(startTime, 500);

    if (+h === 0) {
        showCurrentDate();
    }
}

function checkTime(i) {
    if (i < 10) {
        i = "0" + i
    }
    return i;
}

startTime();

function showCurrentDate() {
    currentDate.innerHTML = new Date().toDateString();
}

showCurrentDate();

function time() {
    setTimeout(() => {

        image.style.opacity = `${1 - (parseFloat(i).toFixed(2))}`;
        i += 0.01;

        if (parseFloat(i).toFixed(2) < 1) {
            time();
        } else if (parseFloat(i.toFixed(2)) === 1) {
            image.style.display = "none";
            arrow.style.display = "inline-block";
            image.style.opacity = "1";
            i = 0;
        }
    }, 20);
}