$(() => {
    $("#submit").click((event) => {
        event.preventDefault();

        let isValid = true;

        let i = 0;
        while (i < $("input").length - 1 && isValid) {
            if ($("#txtName" + i).val().localeCompare("") === 0)
                isValid = false;

            i++;
        }

        if (isValid)
            $("#userForm").trigger("submit");
        else
            $("#error").text("Make sure you fill in all names.");
    });
});