document.addEventListener("click", eventHandler)
let customerSelectInput = document.getElementById("customerName");
let discountField = document.getElementById("hasCustomerDiscount");

function eventHandler(e) {
    if (e.target.id === "submitBtn") {
        let s = document.getElementsByName("CustomerCombined.CustomerName")[0];
        let text = s.options[s.selectedIndex].text;
        let value = s.options[s.selectedIndex].value;

        console.log(discountField.value);
        console.log(text);
        console.log(value);

    }
}