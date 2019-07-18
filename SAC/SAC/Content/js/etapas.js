
function mostrarEstado(estado) {
    var colorRight = '#80bfff';
    var colorWrong = '#ff4d4d';
    $('#estadoCuenta1').css('color', colorRight);
    $('#estadoCuenta1').css('border-color', colorRight);
    if (estado >= 2) {
        $('#estadoCuenta2').css('color', colorRight);
        $('#estadoCuenta3').css('color', colorRight);
    }
    if (estado >= 3) {
        $('#estadoCuenta3').css('color', colorRight);
        $('#estadoCuenta3').css('border-color', colorRight);
        $('#estadoCuenta4').css('color', colorRight);
    }
    if (estado == 5) {
        $('#estadoCuenta5').css('color', colorRight);
        $('#estadoCuenta5').css('border-color', colorRight);
    }
    switch (estado) {
        case 1:
            $('#estadoCuenta2').css('color', colorWrong);
            $('#estadoCuenta3').css('color', colorWrong);
            $('#estadoCuenta3').css('border-color', colorWrong);
            $('#estadoCuenta2').find('span').each(function () {
                $(this).removeClass('fa-chevron-right');
                $(this).addClass('fa-chevron-left');
            });
            break;
        case 4:
            $('#estadoCuenta3').css('color', colorRight);
            $('#estadoCuenta4').css('color', colorWrong);
            $('#estadoCuenta5').css('color', colorWrong);
            $('#estadoCuenta5').css('border-color', colorWrong);
            $('#estadoCuenta4').find('span').each(function () {
                $(this).removeClass('fa-chevron-right');
                $(this).addClass('fa-chevron-left');
            });
            break;
    }
}
