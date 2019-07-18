function upload_step1() {
    $('#fileupload').FileUploadLimit1(function () {
        var btns = $('#fileupload').find('.fupload_botones');

        btns.animate({
            height: 0
        }, 600, function () {
            btns.css('display', 'none');
        });

        $('#fileupload').FileUploadLimit2(function () {
            var btns = $('#fileupload').find('.fupload_botones');
            btns.css('height', 'auto');
            btns.css('opacity', 0);
            btns.css('display', 'block');
            btns.animate({
                opacity: 1
            }, 600, function () {

            });
            //$('#fileupload').find('.fupload_botones').css('display', 'block');
            $('#button-modal_cargarcuentas').removeClass('blue');
            $('#button-modal_cargarcuentas').addClass('disabled');
        });
        $('#fileupload').FileUploadLimit3(function () {
            $('#button-modal_cargarcuentas').removeClass('disabled');
            $('#button-modal_cargarcuentas').addClass('blue');
        });
    });
}



function cargarAsignacioonCuentasMasiva() {
    var nombre = $('#fileupload').find('.fupload_file_name').html();


    swal({
        title: "Mantenedor de Cuentas",
        text: "Desea Procesar el Archivo '" + nombre + "'?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: true
    }, function (isConfirm) {
        if (isConfirm) {
            $('#button-modal_close').attr('onclick', 'location.reload();');

            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('procesarExcel',
                {
                    archivo: nombre
                },
                function (rdata) {
                    if (rdata["response"] == "success") {

                        $("#tableCargaMasiva").find("tbody").html(rdata["tabla"]);
                        $("#tableCargaMasiva").css("display", "");
                        if (rdata["errores"]) {
                            $('#button-modal_cargarcuentas').html("Reprocesar Archivo");
                            $('#button-modal_cargarcuentas').attr('onclick', 'reprocesarCuentasAsignacionMasivas()');
                        }
                        else {
                            $('#button-modal_cargarcuentas').removeClass('blue');
                            $('#button-modal_cargarcuentas').addClass('disabled');
                        }
                        Metronic.unblockUI();

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Cuentas",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });

}


function reprocesarCuentasAsignacionMasivas() {

    var data = [];
    var error = false;

    $('#tableCargaMasiva').find('tr.danger').each(function () {


        var tr = $(this);
        var tds = tr.find('td');

        var empresa = "";
        var cuenta = "";
        var total = "";
        var analista = "";
        var validador = "";
        var certificador = "";
        var fecha = "";
        var dias = "";

        if (tds.eq(0).find('select').length == 0) {
            empresa = tds.eq(0).html();
        }
        else {
            if (tds.eq(0).find('select').val() === "") {
                error = true;
            }
            empresa = tds.eq(0).find('select').find('option:selected').html();
        }
        //cuenta = tds.eq(1).html();

        if (tds.eq(1).find('select').length == 0) {
            cuenta = tds.eq(1).html();
        }
        else {
            if (tds.eq(1).find('select').val() === "") {
                error = true;
            }
            cuenta = tds.eq(1).find('select').find('option:selected').html();
        }


        total = tds.eq(2).html();

        if (tds.eq(3).find('select').length == 0) {
            analista = tds.eq(3).html();
        }
        else {
            if (tds.eq(3).find('select').val() === "") {
                error = true;
            }
            analista = tds.eq(3).find('select').find('option:selected').html();
        }

        if (tds.eq(4).find('select').length == 0) {
            validador = tds.eq(4).html();
        }
        else {
            if (tds.eq(4).find('select').val() === "") {
                error = true;
            }
            validador = tds.eq(4).find('select').find('option:selected').html();
        }

        if (tds.eq(5).find('select').length == 0) {
            certificador = tds.eq(5).html();
        }
        else {
            if (tds.eq(5).find('select').val() === "") {
                error = true;
            }
            certificador = tds.eq(5).find('select').find('option:selected').html();
        }
        dias = tds.eq(6).html();
        fecha = tds.eq(7).html();
        id = tr.prop("id");

        var d = [empresa, cuenta, total, analista, validador, certificador, dias, fecha, id];
        data.push(d);
    });

    if (error) {

        swal({
            title: "Mantenedor de Cuentas",
            text: "Por favor revise la asignacion de datos",
            type: "error",
            confirmButtonText: "Continuar"
        });


    } else {
        var yyyy = $('#yyyy').val();
        var mm = $('#mm').val();

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $.post('reprocesarExcel',
               {
                   data: data,
                   yyyy: yyyy,
                   mm: mm
               },
               function (rdata) {

                   var result = rdata["resultados"]["Data"];

                   $.each(result, function (index, value) {
                       var fila = value["fila"];
                       var resultado = value["resultado"];
                       var tr = $('#tableCargaMasiva').find('tr#' + fila);
                       tr.removeClass("danger");
                       var tds = tr.find('td');

                       if (tds.eq(0).find('select').length == 1) {
                           tds.eq(0).html(tds.eq(0).find('select').find('option:selected').html());
                       }
                       if (tds.eq(1).find('select').length == 1) {
                           tds.eq(1).html(tds.eq(1).find('select').find('option:selected').html());
                       }
                       if (tds.eq(3).find('select').length == 1) {
                           tds.eq(3).html(tds.eq(3).find('select').find('option:selected').html());
                       }
                       if (tds.eq(4).find('select').length == 1) {
                           tds.eq(4).html(tds.eq(4).find('select').find('option:selected').html());
                       }
                       if (tds.eq(5).find('select').length == 1) {
                           tds.eq(5).html(tds.eq(5).find('select').find('option:selected').html());
                       }

                       if (resultado === "True") {
                           tds.eq(8).html("INSERTADA  <i class='fa fa-plus-circle' style='color:#4f4' ></i>");

                       }
                       else {

                           tds.eq(8).html("ACTUALIZADA <i class='fa fa-check-circle' style='color:#4f4' ></i>");

                       }

                   });
                   $('#button-modal_cargarcuentas').removeClass('blue');
                   $('#button-modal_cargarcuentas').addClass('disabled');
                   $('#button-modal_close').attr('onclick', 'location.reload()');

                   Metronic.unblockUI();

               });
    }

}


function ingresarCuentasMasivas() {
    $("#tableCargaMasiva").css("display", "none");
    $('#button-modal_cargarcuentas').removeClass('blue');
    $('#button-modal_cargarcuentas').addClass('disabled');
    $('#btn_delete').click();
    $('#fupload_btn_cancel').click();
    $('#modalAsignacionMasiva').modal('show');


}