var baseUrl = $('base').attr('href');

var _sourceTablaInstrumentos =
{
    dataType: "json",
    dataFields: [
        { name: 'Instrumento', type: 'string' },
        { name: 'ModeloTipo', type: 'string' },
    ],
    id: 'id',
    pagenum: 3,
    pagesize: 20,
    url: baseUrl + "Sub_MttoBateriasEstacionarias/ObtenerListaInstrumentos",
    data: { idMBE: idMBE, idBateria: idB, redEA: ea_red},
    type: 'GET',
    pager: function (pagenum, pagesize, oldpagenum) {
        // callback called when a page or page size is changed.
    },
    deleterow: function (rowid, newdata, commit) {
        $.ajax({
            dataType: 'json',
            cache: false,
            url: baseUrl + "Sub_MttoBateriasEstacionarias/EliminarInstrumento",
            type: "POST",
            data: {
                instrumento: instr, idMBE: idMBE, idBateria: idB, redEA: ea_red
            },
            success: function (data, status, xhr) {
                // delete command is executed.
                $("#TablaInstrumentosBateriasEstacionarias").jqxGrid('updatebounddata');
                App.alert({
                    container: "#bootstrap_alerts_instrumentos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "success",  // alert's type
                    message: "El instrumento fue eliminado.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-success" // put icon before the message
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                App.alert({
                    container: "#bootstrap_alerts_instrumentos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "danger",  // alert's type
                    message: "El instrumento no ha sido eliminado.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-danger" // put icon before the message
                });
            }
        });
    },
};



var _columnsTablaInstrumentos = [
    { text: 'Instrumento', columntype: 'textbox', filtertype: 'input', dataField: 'Instrumento', align: 'center', width: '40%' },
    { text: 'Modelo o tipo', columntype: 'textbox', filtertype: 'input', dataField: 'ModeloTipo', align: 'center', width: '40%' },
    {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '20%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaInstrumentosBateriasEstacionarias").jqxGrid('getrowid', editrow);
            console.log(filaSeleccionada);
            swal({
                title: '¿Está seguro de eliminar el instrumento seleccionado?',
                text: '¡Los valores de instrumento y modelo se eliminarán y no se podrán recuperar!',
                type: 'info',
                allowOutsideClick: 'allow-outside-click',
                showConfirmButton: 'true',
                showCancelButton: 'true',
                confirmButtonClass: 'btn-success',
                cancelButtonClass: 'btn-default',

                closeOnConfirm: 'false',
                closeOnCancel: 'false',
                confirmButtonText: '¡Sí, eliminar!',
                cancelButtonText: '¡No, cancelar!',

            }, function (isConfirm) {
                if (isConfirm) {
                    $("#TablaInstrumentosBateriasEstacionarias").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }

];

inicializarTablaInstrumento("#TablaInstrumentosBateriasEstacionarias", _sourceTablaInstrumentos, _columnsTablaInstrumentos);

function inicializarTablaInstrumento(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '40.5%',
            height: 400,
            theme: 'energyblue',
            source: _source,
            sortable: true,
            //editable: true,
            autoheight: true,
            filterable: true,
            pageable: true,
            localization: getLocalization('es'),

            rendertoolbar: function (statusbar) {
                var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                statusbar.append(container);
            },
            columns: columns,
        });
}

$("#TablaInstrumentosBateriasEstacionarias").on('rowselect', function (event) {
    instrumentoSeleccionado = $("#TablaInstrumentosBateriasEstacionarias").jqxGrid('getrowdata', event.args.rowindex);
    instr = (instrumentoSeleccionado.Instrumento);
});