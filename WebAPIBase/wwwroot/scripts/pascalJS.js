window.onload = function () {
    initPascal(10);
}

var initPascal = function (n) {

    var pascalDiv = document.getElementById('pascal');

    for (var sor = 0; sor < n; sor++) {

        var ujSorDiv = document.createElement('div');
        ujSorDiv.classList.add('sor');
        pascalDiv.appendChild(ujSorDiv);

        for (var oszlop = 0; oszlop <= sor; oszlop++) {
            var ujElemDiv = document.createElement('div');
            ujElemDiv.classList.add('elem');
            ujElemDiv.innerText =
                faktorialis(sor) / (faktorialis(oszlop) * faktorialis(sor - oszlop));
            ujSorDiv.appendChild(ujElemDiv);
        }
    }
}

var faktorialis = function (n) {
    er = 1;

    for (var i = 1; i <= n; i++) {
        er *= i;
    }

    return er;
}