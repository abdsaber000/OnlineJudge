const VredictEls = document.querySelectorAll(".vredict");

VredictEls.forEach(VredictEl => {
    const statusStr = VredictEl.innerText;
    console.log(statusStr);
    if (statusStr === "Accepted") {
        VredictEl.style.color = 'green';
    } else if (statusStr === "In queue") {
        VredictEl.style.color = 'gray';
    } else {
        VredictEl.style.color = 'red';
    }
})
