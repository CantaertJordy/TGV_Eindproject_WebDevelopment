const Succes = () => {
    $("#loading").hide();
}

const Failure = () => {
    $("#loading").hide();

    error("Something went wrong while retrieving the information.");
}

const error = (message) => {
    $("#errorMessage").text(message);
}

$(() => {
    $("#dateOfDeparture").datepicker({
        numberOfMonths: 1,
        showButtonPanel: true,
        dateFormat: "dd-mm-yy",
        minDate: "+0d",
        maxDate: "+2w"
    });

    $("#calculate").click((event) => {
        event.preventDefault();

        let departureId = $("#departureId option:selected").val();
        let destinationId = $("#destinationId option:selected").val();
        let dateOfDepartureString = $("#dateOfDeparture").val();

        if (isNaN(departureId)) {
            error("Please select a point of departure.");
        } else if (isNaN(destinationId)) {
            error("Please select a destination.");
        } else if (departureId === destinationId) {
            error("Please choose a different departure and destination.");
        } else if (dateOfDepartureString.localeCompare("") === 0) {
            error("Please select a date of departure.");
        } else {
            error("");
            $("#searchForm").trigger("submit");
        }
    });
})