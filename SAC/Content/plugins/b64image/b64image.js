
$.fn.b64image = function (data) {
    var input = $(this)[0];
    var inputId = $(this).prop('id');

    var showPreview = data['showPreview'];
    var preview = data['preview'];

    $(this).after('<input type="hidden" id="' + inputId + '_b64" name="' + inputId + '_b64" value="">');
    var inputh = '#' + inputId + '_b64';
    
    $(this).on("change", function () {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                if (showPreview) {
                    $(preview).attr('src', e.target.result);
                }
                var b64 = e.target.result.toString();
                //b64 = b64.substring(b64.indexOf(',') + 1);
                $(inputh).val(b64);
            }
            reader.readAsDataURL(input.files[0]);
        }
    });
};