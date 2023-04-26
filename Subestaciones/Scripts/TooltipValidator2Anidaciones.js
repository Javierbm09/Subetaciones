$("span.field-validation-error").each(function () {
    var id = $(this).attr("data-valmsg-for");
    var idc = "#" + id;
    $(idc).parent().parent().addClass("has-error");
    $(idc).attr("data-toggle", "tooltip");
    $(idc).attr("data-placement", "auto top");
    $(idc).attr("title", $(this).html());
    $('[data-toggle="tooltip"]').tooltip();

});