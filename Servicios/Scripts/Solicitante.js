$(document).ready(function () {
    jQuery(function ($) {
        $("#cedula").mask("999-9999999-9");
    });
    $('#cedula').change(function () {
        var ced = $('#cedula').val().replace(/-+/g, '');
        ced = ced.replace()
        if ( ced.length== 11) {
            var serviceURL = 'NombreCompleto/?id=' +ced;
            $.ajax({
                type: "GET",
                url: serviceURL,
                data: param = "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successFunc,
                error: successFunc
            });

            function successFunc(data, status) {

                $('#NombreCompleto').val(data.responseText);
            }

            function errorFunc(data) {
                $('#NombreCompleto').val(data.ResponseText);
            }
        }
    });
    $('#submit').click(function () {
        $('#cedula').val($('#cedula').val().replace(/-+/g, ''));
    });
    $('#createLink').click(function () {
        $('#usuarioLog').hide();
        $('#claveLog').hide();
    });
});
