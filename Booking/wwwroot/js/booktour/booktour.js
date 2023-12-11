function plus() {
    var preValue = document.getElementById("counter").value;
    document.getElementById("counter").value = parseInt(preValue) + 1;
}

function minus() {
    var preValue = document.getElementById("counter").value;
    if (parseInt(preValue) != 0) {
        document.getElementById("counter").value = parseInt(preValue) - 1;
    }
}

