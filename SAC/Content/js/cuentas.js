var formAgregarCuentas = $('#cuentasForm');

formAgregarCuentas.Validador({
    plazo: { required: [true, 'Debe Ingresar los dias de plazo'], number: [true, 'Dias de plazo debe ser un numero'] },
    total: { required: [true, 'Debe Un total'], number: [true, 'Total debe ser un numero'] }
});



function EditarCuenta(cuentaId) {
    var selectUsuarios = $('#usuarios');
    $('#tcuentas_an_' + cuentaId).html(selectUsuarios.clone());
    $('#tcuentas_va_' + cuentaId).html(selectUsuarios.clone());
    $('#tcuentas_ce_' + cuentaId).html(selectUsuarios.clone());
    $('#tdias_' + cuentaId).attr('oldval', $('#tdias_' + cuentaId).html());
    $('#tdias_' + cuentaId).html("<input type='text' value='" + $('#tdias_' + cuentaId).html() + "' >");
    $('#tcuentas_total_' + cuentaId).attr('oldval', $('#tcuentas_total_' + cuentaId).html());
    $('#tcuentas_total_' + cuentaId).html("<input type='text' value='" + $('#tcuentas_total_' + cuentaId).html() + "' >");
    $('#tcuentas_an_' + cuentaId).find('select').val($('#tcuentas_an_' + cuentaId).attr('uid'));
    $('#tcuentas_va_' + cuentaId).find('select').val($('#tcuentas_va_' + cuentaId).attr('uid'));
    $('#tcuentas_ce_' + cuentaId).find('select').val($('#tcuentas_ce_' + cuentaId).attr('uid'));
    $('#btnEditarCA_' + cuentaId).html("Guardar");
    $('#btnEditarCA_' + cuentaId).attr("onclick", "GuardarCuenta('" + cuentaId + "')");
    $('#btnEliminarCA_' + cuentaId).html("Cancelar");
    $('#btnEliminarCA_' + cuentaId).attr("onclick", "CancelarEdicionCuenta('" + cuentaId + "')");

}

function GuardarCuenta(cuentaId) {
    var analista = $('#tcuentas_an_' + cuentaId).find('select').val();
    var validador = $('#tcuentas_va_' + cuentaId).find('select').val();
    var certificador = $('#tcuentas_ce_' + cuentaId).find('select').val();
    var diasPlazo = $('#tdias_' + cuentaId).find('input').val();
    var total = $('#tcuentas_total_' + cuentaId).find('input').val();
    var yyyy = $('#yyyy').val();
    var mm = $('#mm').val();

    swal({
        title: "Asignación de Cuentas",
        text: "Desea Modificar la Cuenta?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('EditarCuentaActiva',
                {
                    cuentaId: cuentaId,
                    analista: analista,
                    validador: validador,
                    certificador: certificador,
                    diasPlazo: diasPlazo,
                    total: total,
                    yyyy: yyyy,
                    mm: mm
                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Asignación de Cuentas",
                            text: "Cuenta Editada",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            $('#tcuentas_an_' + cuentaId).attr('uid', analista);
                            $('#tcuentas_va_' + cuentaId).attr('uid', validador);
                            $('#tcuentas_ce_' + cuentaId).attr('uid', certificador);
                            $('#tcuentas_an_' + cuentaId).html($('#tcuentas_an_' + cuentaId).find('option:selected').html());
                            $('#tcuentas_va_' + cuentaId).html($('#tcuentas_va_' + cuentaId).find('option:selected').html());
                            $('#tcuentas_ce_' + cuentaId).html($('#tcuentas_ce_' + cuentaId).find('option:selected').html());
                            $('#tdias_' + cuentaId).html(diasPlazo);
                            $('#tcuentas_total_' + cuentaId).html(total);

                            $('#btnEditarCA_' + cuentaId).html("Editar");
                            $('#btnEditarCA_' + cuentaId).attr("onclick", "EditarCuenta('" + cuentaId + "')");
                            //location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Empresas",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });

        }
    });
}

function CancelarEdicionCuenta(cuentaId) {
    $('#tcuentas_an_' + cuentaId).find('select').val($('#tcuentas_an_' + cuentaId).attr('uid'));
    $('#tcuentas_va_' + cuentaId).find('select').val($('#tcuentas_va_' + cuentaId).attr('uid'));
    $('#tcuentas_ce_' + cuentaId).find('select').val($('#tcuentas_ce_' + cuentaId).attr('uid'));
    $('#tcuentas_an_' + cuentaId).html($('#tcuentas_an_' + cuentaId).find('option:selected').html());
    $('#tcuentas_va_' + cuentaId).html($('#tcuentas_va_' + cuentaId).find('option:selected').html());
    $('#tcuentas_ce_' + cuentaId).html($('#tcuentas_ce_' + cuentaId).find('option:selected').html());

    $('#tdias_' + cuentaId).html($('#tdias_' + cuentaId).attr('oldval'));
    $('#tdias_' + cuentaId).removeAttr('oldval');
    $('#tcuentas_total_' + cuentaId).html($('#tcuentas_total_' + cuentaId).attr('oldval'));
    $('#tcuentas_total_' + cuentaId).removeAttr('oldval');


    $('#btnEditarCA_' + cuentaId).html("Editar");
    $('#btnEditarCA_' + cuentaId).attr("onclick", "EditarCuenta('" + cuentaId + "')");
    $('#btnEliminarCA_' + cuentaId).html("Eliminar");
    $('#btnEliminarCA_' + cuentaId).attr("onclick", "");
}

function FiltrarCuentas() {
    var empresa = $('#selectEmpresa').val();
    var rubro = $('#selectRubro').val();
    var grupo = $('#selectGrupo').val();

    $('#cuentasHolder').find('.chkcuenta').each(function () {
        if (!$(this).is(':checked')) {
            var holder = $('#cuentaHolder_' + $(this).attr('cid'));
            var ocultar = false;

            if (empresa != -1) {
                if (empresa != $(this).attr('eid')) {
                    ocultar = true;
                }
            }
            if (rubro != -1) {
                if (rubro != $(this).attr('rid')) {
                    ocultar = true;
                }
            }
            if (grupo != -1) {
                if (grupo != $(this).attr('gid')) {
                    ocultar = true;
                }
            }

            if (ocultar) {
                holder.css('display', 'none');
            } else {
                holder.css('display', 'inline-block');
            }
        }
    });
}

function agregarCuentas() {
    var cuentas = $('#cuentasHolder').find('.chkcuenta:checkbox:checked');
    var analista = $('#analista').val();
    var validador = $('#validador').val();
    var certificador = $('#certificador').val();
    var diasPlazo = $('#plazo').val();
    var total = $('#total').val();
    var yyyy = $('#yyyy').val();
    var mm = $('#mm').val();

    formAgregarCuentas.validate();
    if (cuentas.length > 0) {
        if (formAgregarCuentas.valid()) {
            swal({
                title: "Agregar Cuentas",
                text: "Desea Agregar las Cuentas seleccionadas para este mes?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    var cuentasId = [];
                    cuentas.each(function () {
                        cuentasId.push($(this).attr('cid'));
                    });

                    $.post('AgregarCuentasActivas',
                        {
                            cuentasId: cuentasId,
                            analista: analista,
                            validador: validador,
                            certificador: certificador,
                            diasPlazo: diasPlazo,
                            total: total,
                            yyyy: yyyy,
                            mm: mm
                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Agregar Cuentas",
                                    text: "Cuentas Agregadas",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
                                });

                            } else {
                                Metronic.unblockUI();
                                swal({
                                    title: "Agregar Cuentas",
                                    text: rdata["message"],
                                    type: "error",
                                    confirmButtonText: "Continuar"
                                });
                            }


                        });

                }
            });
        }
    } else {
        swal({
            title: "Agregar Cuentas",
            text: "Debe seleccionar por lo menos una cuenta",
            type: "error",
            confirmButtonText: "Continuar"
        });
    }
}


function CuentasSeleccionarTodo() {
    $('#cuentasHolder').find('.chkcuenta:visible').each(function () {
        $(this).prop('checked', true);
    });

}

function CuentasLimpiarSeleccion() {
    $('#cuentasHolder').find('.chkcuenta').each(function () {
        $(this).prop('checked', false);
    });
    FiltrarCuentas();
}

function LimpiarModalAgregarCuentas() {
    $('#selectEmpresa').val(-1);
    $('#selectRubro').val(-1);
    $('#selectGrupo').val(-1);
    CuentasLimpiarSeleccion();
    $('#analista').find("option:first").prop("selected", true);
    $('#validador').find("option:first").prop("selected", true);
    $('#certificador').find("option:first").prop("selected", true);
    $('#plazo').val(30);
    $('#fechaAgregarCuentas').html($('#mm').find('option:selected').html() + " " + $('#yyyy').val());
}

function CargarCuentasPorEmpresa() {
    var empresa = $('#ReprocesarEmpresa').val();
    var cuentas = $('#ReprocesarCuenta');
    cuentas.html('<option value="">Seleccione</option>');
    cuentas.find('option:first').prop("selected", true);
    if (empresa.length != '') {
        $.post('ObtenerCuentasPorEmpresa',
                        {
                            empresaId: empresa
                        },
                        function (rdata) {
                            cuentas.append(rdata['resultados']);
                        });
    }
}

function RecargarTabla() {
    var yyyy = $('#yyyy').val();
    var mm = $('#mm').val();
    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
    var table = $('#cuentasActivasTable').DataTable();
    table.ajax.url('AsignacionTable?año=' + yyyy + '&mes=' + mm).load(function (ajax) {

        $.post('RecargarCuentasNoActivas', { año: yyyy, mes: mm }, function (rdata) {
            $('#cuentasHolder').html(rdata['data']);
            Metronic.unblockUI();
        });
        
    });
    
}

