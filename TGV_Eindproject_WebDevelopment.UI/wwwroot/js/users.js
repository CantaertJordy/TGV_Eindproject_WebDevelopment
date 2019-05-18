$(() => {
    $("#submit").click((event) => {
        let isValid = true;

        let i = 0;
        while (i < $(".userInput").length && isValid) {
            if ($("#txtName" + i).val().localeCompare("") === 0)
                isValid = false;

            i++;
        }

        if (!isValid) {
            $("#error").text("Make sure you fill in all names.");
            event.preventDefault();
        }
    });
});