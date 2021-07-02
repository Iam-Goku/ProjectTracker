ShowModelPopUp = function (){
    window.window.open('/Project/Popup', "WindowPopup", 'width=400px,height=400px');
}

function openfileDialog() {
    $("#fileLoader").click();
}

function buttonSubmit() {
    $("#btnupload").click();
}


//function Edit(s){
//    location.href = "@Url.Action("Edit","Project",new{id="ProjectID"})".replace("ProjectID", s.tostring());
//}