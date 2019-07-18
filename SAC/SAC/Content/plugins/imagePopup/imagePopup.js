

$.fn.imagePopup = function (data) {
    var imagen = $(this);
    var width = data['width'];
    var height = data['height'];

    imagen.css('width', width);
    imagen.css('height', height);

    if ($('#imagePopupWindow').length == 0) {
        $('body').append('<div id="imagePopupWindow"><img ></div>');
    }
    var popup = $('#imagePopupWindow');
    

    popup.css('position', 'absolute');
    popup.css('z-index', '1000');
    popup.css('display', 'none');
    popup.css('border', 'solid 1px #000');

    imagen.on('mouseover', function () {
        popup.empty();
        popup.css('display', 'block');
        var clone = imagen.clone();

        clone.css('width', 'auto');
        clone.css('height', 'auto');

        clone.appendTo(popup);
        
        var x = imagen.offset().left;
        var y = imagen.offset().top;
        var w = imagen.outerWidth();

        var pw = popup.outerWidth();
        var ph = popup.outerHeight();

        popup.css('left', (x + (w/2) - (pw/2)) + 'px');
        popup.css('top', (y - ph - 10) + 'px');

    });

    imagen.on('mouseout', function () {
        popup.empty();
        popup.css('display', 'none');
    });

}