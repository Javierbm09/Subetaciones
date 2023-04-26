$(function () {
    $("#boton_insertar_breaker").on('click', adiciontarBreakerPorBaja);
    $(document).on('click','.deleteRow', eliminarBreakerPorBaja);
});


function adiciontarBreakerPorBaja() {
    var template = $("#template_breakerPorBaja").html();

    $("#divSub_DesconectivoSubestacion").append(template);

}

function eliminarBreakerPorBaja() {
    $(this).parent().parent().parent().remove();
}
