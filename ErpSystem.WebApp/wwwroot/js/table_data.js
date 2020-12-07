function computeTableColumnTotal(tableId, colNumber) {

    let result = 0;
    let tableElem = window.document.getElementById(tableId);
    let tableBody = tableElem.getElementsByTagName("tbody").item(0);
    let i;
    let howManyRows = tableBody.rows.length;

    for (i = 0; i < (howManyRows); i++) {
        let thisTrElem = tableBody.rows[i];
        let thisTdElem = thisTrElem.cells[colNumber];
        let thisTextNode = thisTdElem.childNodes.item(0);

        //thisTextNode = thisTextNode.replace(',', '');

        // try to convert text to numeric

        let thisNumber = parseFloat(thisTextNode.data.replaceAll(',', ''));

        // if you didn't get back the value NaN (i.e. not a number), add into result


        if (!isNaN(thisNumber))
            result += thisNumber;
    } // end for

    return result;
}

let totalSales = computeTableColumnTotal("dataTable", 6);
document.getElementById("totalSales").innerHTML = parseFloat(totalSales).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
