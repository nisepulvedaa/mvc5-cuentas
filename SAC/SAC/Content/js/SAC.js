var datatables_options = {
    "language": {
        "decimal": "",
        "emptyTable": "Sin Datos",
        "info": "Mostrando _START_ a _END_ de _TOTAL_ filas",
        "infoEmpty": "Mostrando 0 a 0 de 0 filas",
        "infoFiltered": "(Filtrado de _MAX_ filas totales)",
        "infoPostFix": "",
        "thousands": ",",
        "lengthMenu": "Monstrando _MENU_ filas",
        "loadingRecords": "Cargando...",
        "processing": "Procesando...",
        "search": "Búsqueda Rápida:",
        "zeroRecords": "No se encontraron filas",
        "paginate": {
            "first": "Primero",
            "last": "Último",
            "next": "Siguiente",
            "previous": "Anterior"
        },
        "aria": {
            "sortAscending": ": Activar para ordenar Ascendentemente",
            "sortDescending": ": Activar para ordenar Descendentemente"
        }
    }
};
var summernote_options = {
    height: 300,
    toolbar: [
    ['style', ['style']],
    ['font', ['bold', 'italic', 'underline', 'clear']],
    ['fontname', ['fontname']],
    ['color', ['color']],
    ['para', ['ul', 'ol', 'paragraph']]
    
    
    ]
};

$.fn.LimpiarModal = function () {
    var modal = $(this);
    modal.find('input, textarea').each(function () {
        $(this).val('');
    });
    modal.find('select').each(function () {
        $(this).val("option:first").trigger('change');
    });
    modal.find("label.error").hide();
    modal.find(".has-error").removeClass("has-error");
};

$.validator.addMethod('rut', function (value) {
    return /^(\d{1,2}\.\d{3}\.\d{3}-[0-9kK]{1})$/.test(value);
}, 'Rut debe estar en formato xx.xxx.xxx-x');

$.fn.Validador = function (data) {
    var form = $(this);

    var validarData = {
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        ignore: "",
        messages: {
            
        },
        rules: {
            
        },

        invalidHandler: function (event, validator) { //display error alert on form submit
            
            //Metronic.scrollTo(error1, -200);
        },

        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        unhighlight: function (element) { // revert the change done by hightlight
            $(element)
                .closest('.form-group').removeClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label
                .closest('.form-group').removeClass('has-error'); // set success class to the control group
        },

        submitHandler: function (form) {
            
        }
    };


    $.each(data, function (key, val) {
        var input = key;
        validarData['messages'][input] = {};
        validarData['rules'][input] = {};
        $.each(val, function (k, v) {
            validarData['messages'][input][k] = v[1];
            validarData['rules'][input][k] = v[0];
        });
    });


    form.validate(validarData);

};


$.fn.FileUploadLimit1 = function (func) {
    var form = $(this);
    fileuploadcheckfileuploaded(form, func);
}


$.fn.FileUploadLimit2 = function (func) {
    var form = $(this);
    fileuploadcheckfiledeloaded(form, func);
}

$.fn.FileUploadLimit3 = function (func) {
    var form = $(this);
    fileuploadcheckfileready(form, func);
}

function fileuploadcheckfiledeloaded(form, func) {
    var table = form.find('.fupload_tbody_files');
    var filas = table.find('tr');
    if (filas.length > 0) {
        setTimeout(function () { fileuploadcheckfiledeloaded(form, func); }, 500);
    } else {
        func();
    }
}

function fileuploadcheckfileuploaded(form, func) {
    var table = form.find('.fupload_tbody_files');
    var filas = table.find('tr');
    if (filas.length > 0) {
        func();
    } else {
        setTimeout(function () { fileuploadcheckfileuploaded(form, func); }, 500);
    }
}


function fileuploadcheckfileready(form, func) {
    var table = form.find('.fupload_tbody_files');
    var filas = table.find('button');
    console.log(filas.length);
    if (filas.length == 1) {
        func();
    } else {
        setTimeout(function () { fileuploadcheckfileready(form, func); }, 500);
    }
}

$.fn.SNtexto = function () {
    return $(this).code().replace(/\&nbsp;/g, " ").replace(/<\/?[^>]+(>|$)/g, "").trim();
};
