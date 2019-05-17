let global = {
    amount: 1
}

$(() => {
    let amountField = $("#amount");

    amountField.change((event) => {
        $(".amount").val(amountField.val());

        let subtotals = $(".subtotal");
        let total = 0;

        for (let i = 0; i < subtotals.length; i++) {
            let subtotal = $("#txtSubtotal" + i);

            subtotal.val(subtotal.val() / global.amount * amountField.val());
            total += subtotal.val() * 1;
        }

        $("#totalPrice").val(total);

        global.amount = amountField.val();
    });
    
    let checkboxes = $(".checkboxes");

    checkboxes.change((event) => {
        let total = 0;

        for (let i = 0; i < checkboxes.length; i++) {
            let checkbox = $("#Content_" + i + "__Business");
            let basePrice;

            if (checkbox.is(":checked")) {
                basePrice = parseInt($("#txtBusiness" + i).val());
            } else {
                basePrice = parseInt($("#txtEconomic" + i).val());
            }

            $("#txtSubtotal" + i).val(basePrice * global.amount);

            total += basePrice * global.amount;
        }

        $("#totalPrice").val(total);
    });
});