function isChecked(elemId) {
    let element = document.getElementById(elemId);
    //alert(elemId + " " + element.checked);
    return element.checked;
}

function changeCheck(elemId, checkVal) {
    let element = document.getElementById(elemId);
    //alert(elemId +" "+element.checked);
    element.checked = checkVal;
}
