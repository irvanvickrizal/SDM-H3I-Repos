function adjustBlurScreen() {
    var blur = document.getElementById('blur');
    var h = $(document).height() + "px";
    var w = $(document).width() + "px";
    blur.style.width = w;
    blur.style.height = h;
}

function blockColForm(idName) {
    $('#' + idName).addClass('disabledColumnForm');
}