$(document).keydown(function (event) {
    var keyCode = event.keyCode;

    if (keyCode >= 37 && keyCode <= 40) {
        var gameData = $('#gameFieldNumbers').html();
        var regex = /<td [a-zA-Z-/=/":; 0-9]+>([0-9]*)<\/td>/g;
        var matches = gameData.match(regex);

        for (var i = 0; i < matches.length; i++) {
            keyCode += "*";
            let start = matches[i].indexOf('>');
            matches[i] = matches[i].substr(start + 1);
            let end = matches[i].indexOf('<');
            matches[i] = matches[i].substr(0, end);
            if (matches[i].length === 0) {
                keyCode += "0";
            }

            keyCode += matches[i];
        }

        $('#gameFieldNumbers').load("/Games/ReadKey?keyCode=" + keyCode);
    }
});

function startNewGame() {
    $('#gameFieldNumbers').load("/Games/ReadKey")
};