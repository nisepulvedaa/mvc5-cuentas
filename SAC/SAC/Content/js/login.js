
$(document).ready(function () {
    $('#loginForm').unbind('submit');
    $('#loginForm').on('submit', function (event) {
        
        
        doLogin();
        event.stopImmediatePropagation();
        event.stopPropagation();
        event.preventDefault();

        //alert("staph");
        return false;
    });
});

function doLogin() {
    Metronic.blockUI({ message: '<div style="color:#BBB;"><i class="fa fa-cog fa-spin fa-3x fa-fw"></i><br> Cargando...</div>', boxed: true });
    $.post('Login', { user: $('#user').val(), pass: $('#pass').val() }, function (rdata) {
        if (rdata["response"] == "success") {
            //window.location = "/Dashboard/";
            loginForm.submit();
        } else {
            Metronic.unblockUI();
            //alert(rdata["message"]);
            swal({
                title: "Login",
                text: rdata["message"],
                type: "error",
                confirmButtonText: "Continuar"
            });
        }
    });
}

