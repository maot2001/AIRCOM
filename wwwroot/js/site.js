// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function hide() {
    document.getElementById('add').style.display = 'none';
}

function show() {
    document.getElementById('add').style.display = 'block';
}

function swap(id, ElBoleano) {
    if (ElBoleano) {
        document.getElementById(id).style.display = 'none';
        document.getElementById("a" + id).style.display = 'block';
    }
    else {
        document.getElementById(id).style.display = 'block';
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