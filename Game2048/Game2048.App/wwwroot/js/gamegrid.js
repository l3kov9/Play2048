﻿$(document).keydown(function (event) {
    var keyCode = event.keyCode;

    if (keyCode >= 37 && keyCode <= 40) {
        let arrowInput = '<input type="text" value=' + keyCode + ' name="arrowKey" hidden />';
        $('#arrowInput').html(arrowInput);

        $('#gameForm').submit();
    }
})