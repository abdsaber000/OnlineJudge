const remainingTimeEl = document.querySelector(".remaining-time");

const startDateEl = document.querySelector(".start-date");

const endDateEl = document.querySelector(".end-date");

console.log(startDateEl.innerHTML);


let startDate = new Date(startDateEl.innerHTML);
let endDate = new Date(endDateEl.innerHTML);
let currentDate = Date.now();
if (currentDate < startDate) {
    let remainingDate = startDate - currentDate;
    remainingTimeEl.innerHTML = `Before Start : ${convertTime(remainingDate)}`;
} else if (currentDate >= startDate && currentDate <= endDate) {
    remainingTimeEl.innerHTML = "Contest Started !!";
} else {
    remainingTimeEl.innerHTML = "Contest Ended !!";
}


function convertTime(timeInSeconds) {
    timeInSeconds = Math.floor(timeInSeconds / 1000);
    let hours = Math.floor(timeInSeconds / (60 * 60));
    timeInSeconds %= 60 * 60;
    let minutes = Math.floor(timeInSeconds / 60);
    timeInSeconds %= 60;
    return hours.toString() + ":" + minutes.toString() + ":" + timeInSeconds.toString();
}