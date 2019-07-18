var form = $('#loginForm');

form.Validador({
    pwd: { required: [true, 'Debe Ingresar un password'] },
    pwdConfirm: { required: [true, 'Debe reingresar el password'], equalTo: ['#pwd', 'Los password deben coincidir'] },
});


function cambiarPassword() {
    if (form.valid()) {


        var pwd = $('#pwd').val();

        swal({
            title: "Cambio de Password",
            text: "Confirmar Cambio de Password",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Cambiar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('CambiarPassword',
                    {
                        pwd: pwd,
                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Cambio de Password",
                                text: "Password Actualizado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                window.location = "/Dashboard/";
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Cambio de Password",
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

