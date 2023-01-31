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

async function downloadRosterFile(fileName, contentStreamReference) {
    let arrayBuffer = await contentStreamReference.arrayBuffer();
    let blob = new Blob([arrayBuffer]);
    let url = URL.createObjectURL(blob);
    let anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

function openFileBrowser() {
    document.getElementById("loadArmyListInput").click();
}