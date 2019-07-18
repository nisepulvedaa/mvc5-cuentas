var form = $('#empresasForm');
    var formRubros = $('#rubrosForm');
    var formGrupos = $('#gruposForm');
    var formCuentas = $('#cuentasForm');
    var formUsuarios = $('#usuariosForm');

    form.Validador({
        rutEmpresa: { required: [true, 'Debe Ingresar el Rut de la empresa'] },
        razonSocialEmpresa: { required: [true, 'Debe Ingresar la Razon Social de la empresa'] },
        giroEmpresa: { required: [true, 'Debe Ingresar el Giro de la empresa'] },
        EmpresaLogoFile_b64: { required: [true, 'Debe Ingresar la Imagen  de la empresa'] }
    });

    formRubros.Validador({
        nombreRubro: { required: [true, 'Debe Ingresar el Nombre del Rubro'] },
        descripcionRubro: { required: [true, 'Debe Ingresar la descripcion del Rubro'] }
    });

    formGrupos.Validador({
        nombreGrupo: { required: [true, 'Debe Ingresar el Nombre del Grupo'] },
        descripcionGrupo: { required: [true, 'Debe Ingresar la descripcion del Grupo'] }
    });

    formCuentas.Validador({
        nroCuenta: { required: [true, 'Debe Ingresar el Numero de la Cuenta'] },
        nombreCuenta: { required: [true, 'Debe Ingresar el Nombre de la Cuenta'] },
        empresaCuenta: { required: [true, 'Debe Seleccionar una Empresa'] },
        empresaRubro: { required: [true, 'Debe Seleccionar un Rubro'] },
        empresaGrupo: { required: [true, 'Debe Seleccionar un Grupo'] },
        descripcionCuenta: { required: [true, 'Debe ingresar una Descripci&oacute;n'] }
    });

    formUsuarios.Validador({
        usuarioRut: { required: [true, 'Debe Ingresar el Rut del Usuario'], rut: [true, 'Rut debe estar en formato xx.xxx.xxx-x'] },
        usuarioNombre: { required: [true, 'Debe Ingresar el Nombre de Usuario'] },
        empresaUsuario: { required: [true, 'Debe Seleccionar la Empresa del Usuario'] },
        usuarioEmail: { required: [true, 'Debe Ingresar el email del Usuario'], email: [true, 'debe Ingresar un formato de email valido'] },
        perfilUsuario: { required: [true, 'Debe Seleccionar un Perfil de Usuario'] }
    });




    function nuevaEmpresa() {
        $('#modalEmpresas').LimpiarModal();
        $('#modal-title').html("Ingresar Empresas");
        $('#button-modal').attr('onclick', 'ingresarEmpresas()');
        $('#button-modal').html("Ingresar Empresas");
        $('#previewimg').attr('src', '');
        form.validate().resetForm();
    }


    function ingresarEmpresas() {

        form.validate();
        if (form.valid()) {

            var rut = $('#rutEmpresa').val();
            var razonSocial = $('#razonSocialEmpresa').val();
            var giro = $('#giroEmpresa').val();
            var logo = $('#EmpresaLogoFile_b64').val();


            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('ingresarEmpresa',
                {
                    rut: rut,
                    razonSocial: razonSocial,
                    giro: giro,
                    logo: logo

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Empresas",
                            text: "Empresa Agregada",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
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
    }

    function obtenerEmpresa(empresaId) {

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $('#modalEmpresas').LimpiarModal();
        form.validate().resetForm();
        $.post('obtenerEmpresa',
                  {
                      id: empresaId

                  },
                  function (rdata) {

                      $('#rutEmpresa').val(rdata["empresaRut"])
                      $('#razonSocialEmpresa').val(rdata["empresaRazonSocial"]);
                      $('#giroEmpresa').val(rdata["empresaGiro"]);
                      $('#EmpresaLogoFile_b64').val(rdata["empresaLogo"]);
                      $('#previewimg').attr("src", rdata["empresaLogo"]);
                      $('#modalEmpresas').modal("show");
                      $('#modal-title').html("Editar Empresas");
                      $('#button-modal').attr('onclick', 'editarEmpresa()');
                      $('#button-modal').html("Guardar Empresa");
                      $('#empresaId').val(empresaId);
                      Metronic.unblockUI();




                  });
    }

    function editarEmpresa() {


        form.validate();
        if (form.valid()) {

            var rut = $('#rutEmpresa').val();
            var razonSocial = $('#razonSocialEmpresa').val();
            var giro = $('#giroEmpresa').val();
            var logo = $('#EmpresaLogoFile_b64').val();
            var empresaId = $('#empresaId').val();


            swal({
                title: "Mantenedor de Empresas",
                text: "Desea grabar modificaciones?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    $.post('editarEmpresa',
                        {
                            empresaId: empresaId,
                            rut: rut,
                            razonSocial: razonSocial,
                            giro: giro,
                            logo: logo

                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Empresas",
                                    text: "Empresa Editada",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
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

    }

    function eliminarEmpresa(empresaId, empresaRazonSocial) {


        swal({
            title: "Mantenedor de Empresas",
            text: 'Realmente desea Eliminar  la Empresa "' + empresaRazonSocial + '"? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('eliminarEmpresa',
                    {
                        empresaId: empresaId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Empresas",
                                text: "Empresa Eliminada",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
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


    function nuevoRubro() {
        $('#modalRubros').LimpiarModal();
        $('#modal-title').html("Ingresar Rubro");
        $('#button-modal').attr('onclick', 'ingresarRubros()');
        $('#button-modal').html("Ingresar Rubro");
        formRubros.validate().resetForm();
    }

    function ingresarRubros() {

        formRubros.validate();
        if (formRubros.valid()) {

            var rubroNombre = $('#nombreRubro').val();
            var rubroDescripcion = $('#descripcionRubro').val();



            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('ingresarRubro',
                {
                    rubroNombre: rubroNombre,
                    rubroDescripcion: rubroDescripcion

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Rubros",
                            text: "Rubro Agregado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Rubros",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });



        }
    }


    function obtenerRubro(rubroId) {

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $('#modalRubros').LimpiarModal();
        formRubros.validate().resetForm();
        $.post('obtenerRubro',
                  {
                      id: rubroId

                  },
                  function (rdata) {

                      $('#nombreRubro').val(rdata["rubroNombre"])
                      $('#descripcionRubro').val(rdata["rubroDescripcion"]);
                      $('#modalRubros').modal("show");
                      $('#modal-title').html("Editar Rubros");
                      $('#button-modal').attr('onclick', 'editarRubro()');
                      $('#button-modal').html("Guardar Rubro");
                      $('#rubroId').val(rubroId);
                      Metronic.unblockUI();




                  });
    }



    function editarRubro() {


        formRubros.validate();
        if (formRubros.valid()) {

            var rubroNombre = $('#nombreRubro').val();
            var rubroDescripcion = $('#descripcionRubro').val();
            var rubroId = $('#rubroId').val();


            swal({
                title: "Mantenedor de Rubros",
                text: "Desea grabar modificaciones?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    $.post('editarRubro',
                        {
                            rubroId: rubroId,
                            rubroNombre: rubroNombre,
                            rubroDescripcion: rubroDescripcion


                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Rubros",
                                    text: "Rubro Editado",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
                                });

                            } else {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Rubros",
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


    function eliminarRubro(rubroId, rubroNombre) {


        swal({
            title: "Mantenedor de Rubros",
            text: 'Realmente desea Eliminar  El Rubro"' + rubroNombre + '"? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('eliminarRubro',
                    {
                        rubroId: rubroId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Rubros",
                                text: "Rubro Eliminado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Rubros",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });
    }


    function nuevoGrupo() {
        $('#modalGrupos').LimpiarModal();
        $('#modal-title').html("Ingresar Grupo");
        $('#button-modal').attr('onclick', 'ingresarGrupos()');
        $('#button-modal').html("Ingresar Grupo");
        formGrupos.validate().resetForm();
    }

    function ingresarGrupos() {

        formGrupos.validate();
        if (formGrupos.valid()) {

            var grupoNombre = $('#nombreGrupo').val();
            var grupoDescripcion = $('#descripcionGrupo').val();



            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('ingresarGrupo',
                {
                    grupoNombre: grupoNombre,
                    grupoDescripcion: grupoDescripcion

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Grupos",
                            text: "Grupo Agregado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Grupos",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });






        }
    }

    function obtenerGrupo(grupoId) {

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $('#modalGrupos').LimpiarModal();
        formGrupos.validate().resetForm();

        $.post('obtenerGrupo',
                  {
                      id: grupoId

                  },
                  function (rdata) {

                      $('#nombreGrupo').val(rdata["grupoNombre"])
                      $('#descripcionGrupo').val(rdata["grupoDescripcion"]);
                      $('#modalGrupos').modal("show");
                      $('#modal-title').html("Editar Grupos");
                      $('#button-modal').attr('onclick', 'editarGrupo()');
                      $('#button-modal').html("Guardar Grupo");
                      $('#grupoId').val(grupoId);
                      Metronic.unblockUI();




                  });
    }


    function editarGrupo() {

        formGrupos.validate();
        if (formGrupos.valid()) {

            var grupoNombre = $('#nombreGrupo').val();
            var grupoDescripcion = $('#descripcionGrupo').val();
            var grupoId = $('#grupoId').val();


            swal({
                title: "Mantenedor de Grupos",
                text: "Desea grabar modificaciones?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    $.post('editarGrupo',
                        {
                            grupoId: grupoId,
                            grupoNombre: grupoNombre,
                            grupoDescripcion: grupoDescripcion


                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Grupos",
                                    text: "Grupo Editado",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
                                });

                            } else {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Grupos",
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


    function eliminarGrupo(grupoId, grupoNombre) {


        swal({
            title: "Mantenedor de Grupos",
            text: 'Realmente desea Eliminar  El Grupo"' + grupoNombre + '"? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('eliminarGrupo',
                    {
                        grupoId: grupoId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Grupos",
                                text: "Grupo Eliminado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Grupos",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });
    }


    function nuevaCuenta() {
        $('#modalCuentas').LimpiarModal();
        $('#modal-title').html("Ingresar Cuenta");
        $('#button-modal').attr('onclick', 'ingresarCuentas()');
        $('#button-modal').html("Ingresar Cuenta");
        formCuentas.validate().resetForm();
    }

    function ingresarCuentas() {

        formCuentas.validate();
        if (formCuentas.valid()) {

            var nroCuenta = $('#nroCuenta').val();
            var nombreCuenta = $('#nombreCuenta').val();
            var empresaCuenta = $('#empresaCuenta').val();
            var empresaRubro = $('#empresaRubro').val();
            var empresaGrupo = $('#empresaGrupo').val();
            var descripcionCuenta = $('#descripcionCuenta').val();



            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('ingresarCuenta',
                {
                    nroCuenta: nroCuenta,
                    nombreCuenta: nombreCuenta,
                    empresaCuenta: empresaCuenta,
                    empresaRubro: empresaRubro,
                    empresaGrupo: empresaGrupo,
                    descripcionCuenta: descripcionCuenta

                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Cuentas",
                            text: "Cuenta Agregada",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

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
    }

    function obtenerCuenta(cuentaId) {

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $('#modalCuentas').LimpiarModal();
        formCuentas.validate().resetForm();

        $.post('obtenerCuenta',
                  {
                      id: cuentaId

                  },
                  function (rdata) {

                      $('#nroCuenta').val(rdata["cuentaNumero"])
                      $('#nombreCuenta').val(rdata["cuentaNombre"]);
                      $('#empresaCuenta').val(rdata["cuentaEmpresa"]).trigger('change');
                      $('#empresaRubro').val(rdata["cuentaRubro"]).trigger('change');
                      $('#empresaGrupo').val(rdata["cuentaGrupo"]).trigger('change');
                      $('#descripcionCuenta').val(rdata["cuentaDescripcion"]);

                      $('#modalCuentas').modal("show");
                      $('#modal-title').html("Editar Cuenta");
                      $('#button-modal').attr('onclick', 'editarCuenta()');
                      $('#button-modal').html("Guardar Cuenta");

                      $('#cuentaId').val(cuentaId);
                      Metronic.unblockUI();




                  });
    }


    function editarCuenta() {


        formCuentas.validate();
        if (formCuentas.valid()) {

            var nroCuenta = $('#nroCuenta').val();
            var nombreCuenta = $('#nombreCuenta').val();
            var empresaCuenta = $('#empresaCuenta').val();
            var empresaRubro = $('#empresaRubro').val();
            var empresaGrupo = $('#empresaGrupo').val();
            var descripcionCuenta = $('#descripcionCuenta').val();
            var cuentaId = $('#cuentaId').val();


            swal({
                title: "Mantenedor de Cuentas",
                text: "Desea grabar modificaciones?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    $.post('editarCuenta',
                        {
                            cuentaId: cuentaId,
                            nroCuenta: nroCuenta,
                            nombreCuenta: nombreCuenta,
                            empresaCuenta: empresaCuenta,
                            empresaRubro: empresaRubro,
                            empresaGrupo: empresaGrupo,
                            descripcionCuenta: descripcionCuenta


                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Cuentas",
                                    text: "Cuenta Editada",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
                                });

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


    }


    function eliminarCuenta(cuentaId, cuentaNombre) {


        swal({
            title: "Mantenedor de Cuentas",
            text: 'Realmente desea Eliminar  La Cuenta"' + cuentaNombre + '"? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('eliminarCuenta',
                    {
                        cuentaId: cuentaId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Cuentas",
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

    function nuevoUsuario() {
        $('#modalUsuarios').LimpiarModal();
        $('#modal-title').html("Ingresar Usuario");
        $('#button-modal').attr('onclick', 'ingresarUsuarios()');
        $('#button-modal').html("Ingresar Usuario");
        formUsuarios.validate().resetForm();
    }



    function ingresarUsuarios() {

        formUsuarios.validate();
        if (formUsuarios.valid()) {

            var usuarioRut = $('#usuarioRut').val();
            var usuarioNombre = $('#usuarioNombre').val();
            var empresaUsuario = $('#empresaUsuario').val();
            var usuarioEmail = $('#usuarioEmail').val();
            var perfilUsuario = $('#perfilUsuario').val();


            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('ingresarUsuario',
                {
                    usuarioNombre: usuarioNombre,
                    empresaUsuario: empresaUsuario,
                    usuarioEmail: usuarioEmail,
                    perfilUsuario: perfilUsuario,
                    usuarioRut: usuarioRut


                },
                function (rdata) {
                    if (rdata["response"] == "success") {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Usuarios",
                            text: "Usuario Agregado",
                            type: "success",
                            confirmButtonColor: "#DD6B55",
                            confirmButtonText: "Continuar",
                        }, function () {
                            location.reload();
                        });

                    } else {
                        Metronic.unblockUI();
                        swal({
                            title: "Mantenedor de Usuarios",
                            text: rdata["message"],
                            type: "error",
                            confirmButtonText: "Continuar"
                        });
                    }


                });





        }
    }



    function obtenerUsuario(usuarioId) {

        Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
        $('#modalUsuarios').LimpiarModal();
        formUsuarios.validate().resetForm();

        $.post('obtenerUsuario',
                  {
                      id: usuarioId

                  },
                  function (rdata) {

                      $('#usuarioRut').val(rdata["usuarioRut"]);
                      $('#usuarioNombre').val(rdata["usuarioNombre"]);
                      $('#empresaUsuario').val(rdata["usuarioEmpresaId"]).trigger('change');
                      $('#usuarioEmail').val(rdata["usuarioEmail"]);
                      $('#perfilUsuario').val(rdata["usuarioPerfil"]).trigger('change');

                      $('#modalUsuarios').modal("show");
                      $('#modal-title').html("Editar Usuario");
                      $('#button-modal').attr('onclick', 'editarUsuario()');
                      $('#button-modal').html("Guardar Usuario");

                      $('#usuarioId').val(usuarioId);
                      Metronic.unblockUI();




                  });
    }


    function editarUsuario() {

        formUsuarios.validate();
        if (formUsuarios.valid()) {

            var usuarioRut = $('#usuarioRut').val();
            var usuarioNombre = $('#usuarioNombre').val();
            var empresaUsuario = $('#empresaUsuario').val();
            var usuarioEmail = $('#usuarioEmail').val();
            var perfilUsuario = $('#perfilUsuario').val();
            var usuarioId = $('#usuarioId').val();


            swal({
                title: "Mantenedor de Usuarios",
                text: "Desea grabar modificaciones?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Continuar",
                cancelButtonText: "Cancelar",
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                    $.post('editarUsuario',
                        {

                            usuarioId: usuarioId,
                            empresaUsuario: empresaUsuario,
                            usuarioNombre: usuarioNombre,
                            usuarioEmail: usuarioEmail,
                            perfilUsuario: perfilUsuario,
                            usuarioRut: usuarioRut


                        },
                        function (rdata) {
                            if (rdata["response"] == "success") {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Usuarios",
                                    text: "Usuario",
                                    type: "success",
                                    confirmButtonColor: "#DD6B55",
                                    confirmButtonText: "Continuar",
                                }, function () {
                                    location.reload();
                                });

                            } else {
                                Metronic.unblockUI();
                                swal({
                                    title: "Mantenedor de Usuarios",
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



    function eliminarUsuario(usuarioId, usuarioNombre) {


        swal({
            title: "Mantenedor de Usuarios",
            text: 'Realmente desea Eliminar  El Usuario"' + usuarioNombre + '"? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('eliminarUsuario',
                    {
                        usuarioId: usuarioId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Usuarios",
                                text: "Usuario Eliminado",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Usuarios",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });
    }




    function editarClave(usuarioId, usuarioNombre) {


        swal({
            title: "Mantenedor de Usuarios",
            text: 'Realmente desea que   El Usuario"' + usuarioNombre + '" cambie su clave? ',
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Continuar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
                $.post('editarClave',
                    {
                        usuarioId: usuarioId

                    },
                    function (rdata) {
                        if (rdata["response"] == "success") {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Usuarios",
                                text: "Clave Reseteada",
                                type: "success",
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Continuar",
                            }, function () {
                                location.reload();
                            });

                        } else {
                            Metronic.unblockUI();
                            swal({
                                title: "Mantenedor de Usuarios",
                                text: rdata["message"],
                                type: "error",
                                confirmButtonText: "Continuar"
                            });
                        }


                    });

            }
        });
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
                $('#button-modal_close').attr('onclick', 'location.reload();');
            });
        });
    }

    function cargarCuentasMasiva() {
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
                                $('#button-modal_cargarcuentas').attr('onclick', 'reprocesarCuentasMasivas()');
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




    function ingresarCuentasMasivas() {

        $("#tableCargaMasiva").css("display", "none");
        $('#button-modal_cargarcuentas').removeClass('blue');
        $('#button-modal_cargarcuentas').addClass('disabled');
        $('#btn_delete').click();
        $('#fupload_btn_cancel').click();
        $('#modalCuentaMasiva').modal('show');


    }

    function reprocesarCuentasMasivas() {

        var data = [];
        var error = false;

        $('#tableCargaMasiva').find('tr.danger').each(function () {


            var tr = $(this);
            var tds = tr.find('td');
            var cuenta = "";
            var fila = "";
            var nombre = "";
            var empresa = "";
            var rubro = "";
            var grupo = "";
            var descripcion = "";

            fila = tds.eq(0).html();
            cuenta = tds.eq(1).html();
            nombre = tds.eq(2).html();
            descripcion = tds.eq(6).html();

            if (tds.eq(3).find('select').length == 0) {
                empresa = tds.eq(3).html();
            }
            else {
                if (tds.eq(3).find('select').val() === "") {
                    error = true;
                }
                empresa = tds.eq(3).find('select').find('option:selected').html();
            }
            if (tds.eq(4).find('select').length === 0) {
                console.log("select lenght 0");
                rubro = tds.eq(4).html();
            }
            else {
                if (tds.eq(4).find('select').val() === "") {
                    error = true;
                }
                rubro = tds.eq(4).find('select').find('option:selected').html();
            }

            if (tds.eq(5).find('select').length == 0) {
                grupo = tds.eq(5).html();
            }
            else {
                if (tds.eq(5).find('select').val() === "") {
                    error = true;
                }
                grupo = tds.eq(5).find('select').find('option:selected').html();
            }
            var d = [fila, cuenta, nombre, empresa, rubro, grupo, descripcion];
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

            Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> cargando...</div>', boxed: true });
            $.post('reprocesarExcel',
                   {
                       data: data
                   },
                   function (rdata) {

                       var result = rdata["resultados"]["Data"];

                       $.each(result, function (index, value) {
                           var fila = value["fila"];
                           var resultado = value["resultado"];
                           var tr = $('#tableCargaMasiva').find('tr#' + fila);
                           tr.removeClass("danger");
                           var tds = tr.find('td');

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
                               tds.eq(7).html("INSERTADA  <i class='fa fa-plus-circle' style='color:#4f4' ></i>");

                           }
                           else {

                               tds.eq(7).html("ACTUALIZADA <i class='fa fa-check-circle' style='color:#4f4' ></i>");

                           }

                       });
                       $('#button-modal_cargarcuentas').removeClass('blue');
                       $('#button-modal_cargarcuentas').addClass('disabled');
                       $('#button-modal_close').attr('onclick', 'location.reload()');

                       Metronic.unblockUI();

                   });
        }

    }
