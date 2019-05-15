$(() => {
    $("#dateOfDeparture").datepicker({
        numberOfMonths: 1,
        showButtonPanel: true,
        dateFormat: "dd-mm-yy",
        minDate: "+0d",
        maxDate: "+2w"
    })
})