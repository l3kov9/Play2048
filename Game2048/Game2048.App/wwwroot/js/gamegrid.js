$(document).keydown(function (event) {
    var keyCode = event.keyCode;
    let gameOverMessage = $('#gameOverMessage').html();

    if (gameOverMessage.length === 0) {
        if (keyCode >= 37 && keyCode <= 40) {
            let direction;
            switch (keyCode) {
                case 37: direction = "left";
                    break;
                case 38: direction = "up";
                    break;
                case 39: direction = "right";
                    break;
                case 40: direction = "down";
                    break;
                default:
                    break;
            }

            $.ajax({
                url: "Games/Index/" + direction,
                type: "post",
                data: $("#gameForm").serialize(),
                success: function (result) {
                    $("#partial").html(result);
                }
            });
        }
    }
});

function saveGame() {
    let username = $('#username').val();

    $.ajax({
        url: "Games/SaveGame?username=" + username,
        type: "post",
        success: function (result) {
            $("#highScorePartial").html(result);
        }
    });
}

function startNewGame() {
    $.ajax({
        url: "Games/Index/",
        type: "post",
        data: $("#gameForm").serialize(),
        success: function (result) {
            $("#partial").html(result);
        }
    });
}