function computeTableColumnTotal(tableId, colNumber) {
    // find the table with id attribute tableId
    // return the total of the numerical elements in column colNumber
    // skip the top row (headers) and bottom row (where the total will go)

    let result = 0;

    let tableElem = window.document.getElementById(tableId);
    let tableBody = tableElem.getElementsByTagName("tbody").item(0);
    let i;
    let howManyRows = tableBody.rows.length;

    console.log(howManyRows);

    for (i = 0; i < (howManyRows); i++) {
        let thisTrElem = tableBody.rows[i];
        let thisTdElem = thisTrElem.children[colNumber - 1];
        let thisTextNode = thisTdElem.innerHTML;
        thisTextNode = thisTextNode.replaceAll(',', '');

        // try to convert text to numeric

        let thisNumber = parseFloat(thisTextNode);
        // if you didn't get back the value NaN (i.e. not a number), add into result


        if (!isNaN(thisNumber))
            result += thisNumber;
        console.log(result);
    } // end for

    return result;
}

let totalSales = computeTableColumnTotal("currentTable", 6);

let resultSales = parseFloat(totalSales).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
let vatResult = parseFloat(totalSales * 0.2).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
let totalResult = parseFloat(totalSales * 1.2).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

document.getElementById("totalSales").innerHTML = resultSales;
document.getElementById("vat").innerHTML = vatResult;
document.getElementById("total").innerHTML = totalResult;

let btn = document.getElementById("printBtn");
btn.onclick = function printInvoice() {



    let prtContent = document.getElementById("invoicePrint");

    let WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();

    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();



}