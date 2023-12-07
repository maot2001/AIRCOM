// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function hide() {
    document.getElementById('add').style.display = 'none';
}

function show() {
    document.getElementById('add').style.display = 'block';
}

function swap(id, ElBoleano, tipo="block") {
    if (ElBoleano) {
        document.getElementById(id).style.display = 'none';
        document.getElementById("a" + id).style.display = tipo;
    }
    else {
        document.getElementById(id).style.display = tipo;
        document.getElementById("a" + id).style.display = 'none';
    }
}

function filter() {
    var list = document.querySelectorAll(".ofertas > div");
    for (var i = 0; i < list.length; i++) {
        if (list[i].id.includes(document.getElementById("search").inputMode.toString(), 0)) {
            list[i].style.display = "block";
        }
        else {
            list[i].style.display = "none";
        }
    }
}

function sortTable(n,type) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("myTable");
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;
        for (i = 2; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            if (dir == "asc") {
                if ((type=="str" && x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) || (type=="int" && parseFloat(x.innerHTML) > parseFloat(y.innerHTML))) {
                    shouldSwitch= true;
                    break;
                }
            }
            else if (dir == "desc") {
                if ((type=="str" && x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) || (type=="int" && parseFloat(x.innerHTML) < parseFloat(y.innerHTML))) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount ++;
        }
        else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}