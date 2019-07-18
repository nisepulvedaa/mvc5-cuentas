
function EliminarCuenta(cuentaId, cuenta, yyyy, mm) {
       

    swal({
        title: "Eliminar Cuenta",
        text: "Desea Eliminar la Cuenta Activa '" + cuenta + "'?: Esto NO SE PUEDE DESHACER",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: false
    }, function (isConfirm) {
        if (isConfirm) {
            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });

            $.post('../../Cuentas/Eliminar',
                {
                    cuentaId: cuentaId,
                    yyyy: yyyy,
                    mm: mm
                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Eliminar Cuenta",
                            text: "Cuenta Eliminada",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Eliminar Cuenta",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }
                });
        }
    });


}
