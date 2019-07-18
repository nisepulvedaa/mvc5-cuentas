var form = $('#analisisForm');
var formEditar = $('#analisisEditarForm');

form.Validador({
    nombreArchivo: { required: [true, 'Debe Ingresar el Nombre  del archivo'] },
    montoArchivo: { required: [true, 'Debe Ingresar un monto del archivo'], number: [true, 'El Monto del archivo debe ser un numero'] },
    comentarioArchivo: { required: [true, 'Debe Ingresar un comentario '] }
});

formEditar.Validador({
    nombreArchivo: { required: [true, 'Debe Ingresar el Nombre  del archivo'] },
    montoArchivo: { required: [true, 'Debe Ingresar un monto del archivo'], number: [true, 'El Monto del archivo debe ser un numero'] },
    comentarioArchivo: { required: [true, 'Debe Ingresar un comentario '] }
});



//form.validate();
//if (form.valid()) {


function agregarArchivo() {
    //$("#analisisForm").css("display", "none");
    form.validate().resetForm();
    $('#modalArchivos').LimpiarModal();
    $('#btn_delete').click();
    $('#fupload_btn_cancel').click();
    $('#modalArchivos').modal('show');

}

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
            //  $("#analisisForm").css("display", "block");
            btns.animate({
                opacity: 1
            }, 600, function () {

            });
            //$('#fileupload').find('.fupload_botones').css('display', 'block');
            $('#button-modal_cargarArchivo').removeClass('blue');
            $('#button-modal_cargarArchivo').addClass('disabled');

        });
        $('#fileupload').FileUploadLimit3(function () {
            $('#button-modal_cargarArchivo').removeClass('disabled');
            $('#button-modal_cargarArchivo').addClass('blue');



        });
    });
}



function cargarArchivo() {

    form.validate();
    if (form.valid()) {




        var nombreArchivo = $('#nombreArchivo').val();
        var montoArchivo = $('#montoArchivo').val();
        var comentarioArchivo = $('#comentarioArchivo').val();
        var cuentaId = $('#cuentaId').val();
        var cuentaFecha = $('#cuentaFecha').val();
        var nombreFile = $('#fileupload').find('.fupload_file_name').html();


        swal({
            title: "Agregar Archivos",
            text: "Desea Agregar  El Archivo?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('../ingresarArchivo',
                    {
                        nombreFile: nombreFile
                         , nombreArchivo: nombreArchivo
                         , montoArchivo: montoArchivo
                         , comentarioArchivo: comentarioArchivo
                         , cuentaId: cuentaId
                         , cuentaFecha: cuentaFecha

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Agregar Archivo",
                                text: "Archivo Agregado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Agregar Archivo",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}

function obtenerArchivo(archivoId, versionId) {

    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalArchivosEditar').LimpiarModal();
    formEditar.validate().resetForm();
    $.post('../obtenerArchivo',
              {
                  id: archivoId,
                  version: versionId

              },
              function (rdata) {

                  $('#archivoId').val(rdata["archivoId"]);
                  $('#archivoVersion').val(rdata["archivoVersion"]);
                  $('#nombreEditarArchivo').val(rdata["archivoNombre"]);
                  $('#montoEditarArchivo').val(rdata["archivoMonto"]);
                  $('#comentarioEditarArchivo').val(rdata["archivoComentario"]);
                  $('#modalEditarArchivos').modal("show");
                  Metronic.unblockUI();
                  //$('#rutEmpresa').val(rdata["empresaRut"])
                  //$('#razonSocialEmpresa').val(rdata["empresaRazonSocial"]);
                  //$('#giroEmpresa').val(rdata["empresaGiro"]);
                  //$('#EmpresaLogoFile_b64').val(rdata["empresaLogo"]);
                  //$('#previewimg').attr("src", rdata["empresaLogo"]);
                  //$('#modalEmpresas').modal("show");
                  //$('#modal-title').html("Editar Empresas");
                  //$('#button-modal').attr('onclick', 'editarEmpresa()');
                  //$('#button-modal').html("Editar Empresas");
                  //$('#empresaId').val(empresaId);
                  //Metronic.unblockUI();




              });

}



function EditarArchivo() {

    var archivoId = $('#archivoId').val();
    var archivoVersion = $('#archivoVersion').val();
    var nombreArchivo = $('#nombreEditarArchivo').val();
    var montoArchivo = $('#montoEditarArchivo').val();
    var comentarioArchivo = $('#comentarioEditarArchivo').val()


    swal({
        title: "Analisis  de Cuentas",
        text: "Desea Editar el Archivo?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('../editarArchivo',
                {
                    archivoId: archivoId,
                    archivoVersion: archivoVersion,
                    nombreArchivo: nombreArchivo,
                    montoArchivo: montoArchivo,
                    comentarioArchivo: comentarioArchivo


                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Analisis  de Cuentas",
                            text: "Archivo Editado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Analisis  de Cuentas",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}



function eliminarArchivo(archivoId, archivoVersion, archivoNombre) {


    swal({
        title: "Analisis  de Cuentas",
        text: 'Realmente desea Eliminar  el Archivo "' + archivoNombre + '"? ',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('../eliminarArchivo',
                {
                    archivoId: archivoId,
                    version: archivoVersion

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Analisis  de Cuentas",
                            text: "Archivo Eliminado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Analisis  de Cuentas",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}

function copiarArchivo(archivoId, version) {

    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    $('#modalArchivos').LimpiarModal();
    form.validate().resetForm();
    $.post('../obtenerArchivo',
              {
                  id: archivoId,
                  version: version
              },
              function (rdata) {

                  $('#archivoId').val(rdata["archivoId"]);
                  $('#archivoVersion').val(rdata["archivoVersion"]);
                  $('#nombreArchivo').val(rdata["archivoNombre"]);
                  $('#montoArchivo').val(rdata["archivoMonto"]);
                  $('#comentarioArchivo').val(rdata["archivoComentario"]);
                  $('#modalArchivos').modal("show");
                  $('#button-modal_cargarArchivo').attr('onclick', 'cargarCopiaArchivo()');
                  Metronic.unblockUI();




              });

}


function cargarCopiaArchivo() {


    form.validate();
    if (form.valid()) {

        var nombreArchivo = $('#nombreArchivo').val();
        var montoArchivo = $('#montoArchivo').val();
        var comentarioArchivo = $('#comentarioArchivo').val();
        var archivoId = $('#archivoId').val();
        var archivoVersion = $('#archivoVersion').val();
        var nombreFile = $('#fileupload').find('.fupload_file_name').html();
        var cuentaId = $('#cuentaId').val();
        var cuentaFecha = $('#cuentaFecha').val();

        swal({
            title: "Agregar Version",
            text: "Desea Agregar Esta Version?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('../ingresarVersion',
                    {
                        archivoId: archivoId
                        , archivoVersion: archivoVersion
                        , nombreArchivo: nombreArchivo
                        , montoArchivo: montoArchivo
                        , comentarioArchivo: comentarioArchivo
                        , nombreFile: nombreFile
                        , cuentaId: cuentaId
                        , cuentaFecha: cuentaFecha
                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Agregar Version",
                                text: "Version Agregada",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Agregar Version",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });

    }

}

function prepararEnviar() {
    $('#modalEnviar').modal("show");
}

function Enviar() {
    var code = $('#snComentario').code();
    var cuentaId = $('#cuentaId').val();
    var cuentaFecha = $('#cuentaFecha').val();

    if ($('#snComentario').SNtexto().length > 5) {
        swal({
            title: "Finalizar Cuenta",
            text: "Desea Finalizar esta cuenta?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('../FinalizarCuenta',
                    {
                        comentario: code,
                        cuentaId: cuentaId,
                        cuentaFecha: cuentaFecha
                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Finalizar Cuenta",
                                text: "Cuenta Finalizar",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.href = '../Index';
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Finalizar Cuenta",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }
                    });
            }
        });
    } else {
        swal({
            title: "Analisis  de Cuentas",
            text: "El comentario debe ser mayor a 5 caracteres",
            type: "error",
            confirmButtonText: "Continuar"
        });
    }
}


function prepararRetroceder() {
    $('#modalRetroceder').modal("show");
}

function EnviarAnalisis() {
    var code = $('#snComentarioR').code();
    var cuentaId = $('#cuentaId').val();
    var cuentaFecha = $('#cuentaFecha').val();

    if ($('#snComentarioR').SNtexto().length > 5) {
        swal({
            title: "Enviar a Validación",
            text: "Desea Enviar esta cuenta a Validación?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('../EnviarCertificacion',
                    {
                        comentario: code,
                        cuentaId: cuentaId,
                        cuentaFecha: cuentaFecha
                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Enviar a Certificacion",
                                text: "Cuenta Enviada a Certificacion",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.href = '../Index';
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Enviar a Certificacion",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }
                    });
            }
        });
    } else {
        swal({
            title: "Analisis  de Cuentas",
            text: "El comentario debe ser mayor a 5 caracteres",
            type: "error",
            confirmButtonText: "Continuar"
        });
    }
}


function descargar(archivoId, version) {
    window.open('/Archivo/Descargar?archivoId=' + archivoId + '&version=' + version, '_blank');
}


function ExpandirLogs() {
    var logs = $('#loglist');
    logs.css('opacity', 0);
    logs.css('display', 'block');
    var alturaObjetivo = logs.css('height');
    logs.css('height', '0px');

    $('.loglistexpand').removeClass('fa-plus');
    $('.loglistexpand').addClass('fa-minus');
    $('.loglistexpand').attr('onclick', 'ContraerLogs()');

    logs.animate({
        height: alturaObjetivo
    }, 1000, function () {
        logs.animate({
            opacity: 1
        }, 1000, function () {

        });
    });

}

function ContraerLogs() {
    var logs = $('#loglist');

    $('.loglistexpand').removeClass('fa-minus');
    $('.loglistexpand').addClass('fa-plus');
    $('.loglistexpand').attr('onclick', 'ExpandirLogs()');

    logs.animate({
        opacity: 0
    }, 1000, function () {
        logs.animate({
            height: '0px'
        }, 1000, function () {
            logs.css('opacity', 0);
            logs.css('display', 'none');
            logs.css('height', 'auto');
        });
    });
}





