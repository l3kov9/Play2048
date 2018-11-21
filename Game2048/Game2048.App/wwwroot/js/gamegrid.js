$(document).keydown(function (event) {
    var keyCode = event.keyCode;
    let gameOverMessage = $('#gameOverMessage').html();

    if (gameOverMessage.length === 0) {
        if (keyCode >= 37 && keyCode <= 40) {

            let arrowInput = '<input type="text" value=' + keyCode + ' name="arrowKey" hidden />';
            $('#arrowInput').html(arrowInput);

            $.ajax({
                url: "Games/Index",
                type: "post",
                data: $("#gameForm").serialize(),
                success: function (result) {
                    $("#partial").html(result);
                }
            });
        }
    }
});