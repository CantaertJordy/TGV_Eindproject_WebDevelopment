$(() => {
    let dateInput = $("#dateOfDeparture");

    let today = new Date();
    console.log(today);
    let finalDate = today.getDate() + 14;
    console.log(finalDate);

    dateInput.attr("min", today);
    dateInput.attr("max", finalDate);
})