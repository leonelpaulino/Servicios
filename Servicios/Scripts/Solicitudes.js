$(document).ready(function () {
    $('#remover').hide();
    $('#remover2').hide();
    $('#filemsg').hide();
    $('#adjuntar').change(function(){
        var input = $(this),
               label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        $('#nombreArchivo').val(label);
        $('#remover').show();
    });
    $('#remover').click(function () {
        $('#adjuntar').val('');
        $('#nombreArchivo').val('');
        $(this).hide();
    });
    $('#adjuntar').bind('change', function () {
        var fxt = $('#adjuntar').val().substring($('#adjuntar').val().lastIndexOf('.') + 1);
        if (this.files[0].size > 50000 || fxt != 'pdf' && fxt != 'docx') {
            $('#filemsg').show();
            $('#adjuntar').val('');
            $('#nombreArchivo').val('');
            $('#remover').hide();
        }
    });
    $('#adjuntar2').bind('change', function() {
        var fxt = $('#adjuntar2').val().substring($('#adjuntar').val().lastIndexOf('.') + 1);
        if (this.files[0].size > 50000 || fxt != 'pdf' && fxt != 'docx') {
            $('#filemsg').show();
            $('#adjuntar2').val('');
            $('#nombreArchivo2').val('');
            $('#remover2').hide();
        }

    });
    $('#adjuntar2').change(function () {
        var input = $(this),
               label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        $('#nombreArchivo2').val(label);
        $('#remover2').show();
    });
    $('#remover2').click(function () {
        $('#adjuntar2').val('');
        $('#nombreArchivo2').val('');
        $(this).hide();
    });
    $('#InServicio').change(function () {
        window.location.replace('http://'+window.location.hostname+':'+window.location.port+'/Solicitudes/create/?id=' + $('#InServicio').val());
    });
});