function CheckMoblieNumber10Digit() {
    debugger;
    var regx = /^[0][1-9]\d{9}$|^[1-9]\d{9}$/g;
    var number = document.getElementById("PhoneNumber").value;

    if (regx.test(number)) {
        document.getElementById("para1").innerHTML = "found number";
    }
    else {
        document.getElementById("para1").innerHTML = "number not found";
    }
}
function CheckForString() {
    debugger;
    var regx = /^[a-zA-Z ]+$/g;
    var stringtotest = document.getElementById("TextBox1").value;
    if (regx.test(stringtotest)) {
        document.getElementById("para1").innerHTML = "perfect string";
    }
    else {
        document.getElementById("para1").innerHTML = "not a perfect string";
    }

}