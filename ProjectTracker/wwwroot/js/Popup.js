////showmodelpopup = function () {

////    window.window.open('/project/popup', "windowpopup", 'width=400px,height=400px');
////}

showmodelpopup = function (url, title, w, h) {


    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open('/project/popup', title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}


NoteList =function (url, title, w, h) {


    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open('/Project/Notes', title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

Attachments = function (url, title, w, h) {


    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open('/Project/Attachment', title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

//NoteList = function () {
//    window.window.open('/Project/Notes', "WindowPopup", 'width=600px,height=400px');
//}

ShowFilePopUp = function () {
    window.window.open('/Project/Attachment', "WindowPopup", 'width=400px,height=400px');
}

function openfileDialog() {
    $("#fileLoader").click(); 
}

function submitCreate() {
    $("#btnsub").click(); 
}

function buttonSubmit() {
    $("#btnupload").click();
}





                   