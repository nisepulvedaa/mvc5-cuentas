

function filtrar() {
    var años = null2n1($('#selectAño').val());
    var meses = null2n1($('#selectMes').val());
    var empresas = null2n1($('#selectEmpresa').val());
    var rubros = null2n1($('#selectRubro').val());
    var grupos = null2n1($('#selectGrupo').val());
    var cuentas = null2n1($('#selectCuenta').val());


    var table = $('#reportesTable').DataTable();
    table.ajax.url('Filtrar?annos=' + años + '&meses=' + meses + "&empresas=" + empresas + "&rubros=" + rubros + "&grupos=" + grupos + "&cuentas=" + cuentas).load(function (ajax) {

    });
}

function descargar() {
    var años = null2n1($('#selectAño').val());
    var meses = null2n1($('#selectMes').val());
    var empresas = null2n1($('#selectEmpresa').val());
    var rubros = null2n1($('#selectRubro').val());
    var grupos = null2n1($('#selectGrupo').val());
    var cuentas = null2n1($('#selectCuenta').val());

    window.open('Descargar?annos=' + años + '&meses=' + meses + "&empresas=" + empresas + "&rubros=" + rubros + "&grupos=" + grupos + "&cuentas=" + cuentas, '_blank');
}

function null2n1(val) {
    if (val == null) {
        return -1;
    } else {
        return val;
    }
}