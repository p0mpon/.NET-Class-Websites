const nInput = document.getElementById("nInput");
const generateButton = document.getElementById("generateButton");
const tableDiv = document.getElementById("tableDiv");
const errorDiv = document.getElementById("errorMessage");

function checkN(n) {
    if (!Number.isInteger(n)) {
        return "Liczba musi być całkowita.";
    }
    if (n < 5 || n > 20) {
        return "Liczba musi być w zakresie [5, 20]"
    }
    return null;
}

function generateRandomNumbers(n, min = 1, max = 99) {
    const numbers = [];

    for (let i = min; i <= max; i++) {
        numbers.push(i);
    }

    for (let i = 0; i < n; i++) {
        const j = Math.floor(Math.random() * (max - min + 1));
        [numbers[i], numbers[j]] = [numbers[j], numbers[i]];
    }

    return numbers.slice(0, n);
}

function generateTable(n, numbers) {
    const mul_arr = new Array(n);

    for (let i = 0; i < n; i++) {
        mul_arr[i] = new Array(n);
        
        for (let j = 0; j < n; j++) {
            mul_arr[i][j] = numbers[i] * numbers[j];
        }
    }

    return mul_arr;
}

function cleanTable() {
    while (tableDiv.firstChild) {
        tableDiv.removeChild(tableDiv.firstChild);
    }
}

function makeHtmlTable(n, numbers, products_array) {
    cleanTable();

    //create table
    const table = document.createElement("table");

    const headerRow = document.createElement("tr");

    const topLeft = document.createElement("th");
    topLeft.textContent = "";
    headerRow.appendChild(topLeft);

    //every drawn(?) number
    numbers.forEach(num => {
        const th = document.createElement("th");
        th.textContent = num;
        headerRow.appendChild(th);
    });

    table.appendChild(headerRow);

    //the actual products
    for (let i = 0; i < n; i++) {
        const row = document.createElement("tr");
        const th = document.createElement("th");
        th.textContent = numbers[i];
        row.appendChild(th);

        for (let j = 0; j < n; j++) {
            const td = document.createElement("td");

            //check if even or odd
            const product = products_array[i][j];
            if (product % 2 == 0) {
                td.className = "even";
            }
            else {
                td.className = "odd";
            }

            td.textContent = product;
            row.appendChild(td);
        }

        table.appendChild(row);
    };

    tableDiv.appendChild(table);
}

function generate() {
    const n = Number(nInput.value);
    const error = checkN(n);

    if (error) {
        errorDiv.textContent = error;
        cleanTable();
    }
    else {
        errorDiv.textContent = "";
        const drawnNumbers = generateRandomNumbers(n);
        const products = generateTable(n, drawnNumbers);
        makeHtmlTable(n, drawnNumbers, products);
    }
}

generateButton.addEventListener("click", generate);
nInput.addEventListener("keydown", e => { if(e.key === 'Enter') generate(); })