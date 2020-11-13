function computeTableColumnTotal(tableId, colNumber) {
    // find the table with id attribute tableId
    // return the total of the numerical elements in column colNumber
    // skip the top row (headers) and bottom row (where the total will go)

    let result = 0;

    let tableElem = window.document.getElementById(tableId);
    let tableBody = tableElem.getElementsByTagName("tbody").item(0);
    let i;
    let howManyRows = tableBody.rows.length;

    for (i = 0; i < (howManyRows); i++) {
        let thisTrElem = tableBody.rows[i];
        let thisTdElem = thisTrElem.cells[colNumber];
        let thisTextNode = thisTdElem.childNodes.item(0);



        // try to convert text to numeric 
        let thisNumber = parseFloat(thisTextNode.data);
        // if you didn't get back the value NaN (i.e. not a number), add into result

        if (!isNaN(thisNumber))
            result += thisNumber;
    } // end for

    return result;
}

let totalSales = computeTableColumnTotal("dataTable", 5);
document.getElementById("totalSales").innerHTML = parseFloat(totalSales).toFixed(2);