var baseUrl = $('base').attr('href');
let idCE;

var _sourceCorrienteExitAction =
{
    dataType: "json",
    dataFields: [
        { name: 'Id_CorrienteExit', type: 'int' },
        { name: 'Tap', type: 'int' },
        { name: 'A_0', type: 'double' },
        { name: 'B_0', type: 'double' },
        { name: 'C_0', type: 'double' },
        { name: 'PorcientoDesviacion', type: 'double' },
    ],
    id: 'id',
    pagenum: 3,
    pagesize: 20,
    url: baseUrl + "Sub_MttoTFuerza/ObtenerListaCorrienteExit",
    data: { idMTF: idMttoTF },
    type: 'GET',
    pager: function (pagenum, pagesize, oldpagenum) {
        // callback called when a page or page size is changed.
    },
    //updaterow: function (rowid, newdata, commit) {
    //    $.ajax({
    //        cache: false,
    //        dataType: 'json',
    //        url: baseUrl + "Sub_MttoTFuerza/ActualizarCorrienteExit",
    //        data: { datosFilaCorrienteExit: newdata, idMTF: idMttoTF },
    //        type: "POST",
    //        success: function (data, status, xhr) {
    //            $("#TablaCorrienteExit").jqxGrid('updatebounddata');
    //            // insert command is executed.
    //            App.alert({
    //                container: "#bootstrap_alerts_CorrienteExit", // alerts parent container(by default placed after the page breadcrumbs)
    //                place: "prepend", // append or prepent in container
    //                type: "success",  // alert's type
    //                message: "Se actualizaron los datos correctamente.",  // alert's message
    //                close: 1, // make alert closable
    //                reset: 1, // close all previouse alerts first
    //                focus: 1, // auto scroll to the alert after shown
    //                closeInSeconds: 2,//, // auto close after defined seconds
    //                icon: "fa fa-success" // put icon before the message
    //            });

    //        },
    //        error: function (jqXHR, textStatus, errorThrown) {
    //            commit(false);
    //            App.alert({
    //                container: "#bootstrap_alerts_CorrienteExit", // alerts parent container(by default placed after the page breadcrumbs)
    //                place: "prepend", // append or prepent in container
    //                type: "danger",  // alert's type
    //                message: "No se pudieron actualizar los datos.",  // alert's message
    //                close: 1, // make alert closable
    //                reset: 1, // close all previouse alerts first
    //                focus: 1, // auto scroll to the alert after shown
    //                closeInSeconds: 5,//, // auto close after defined seconds
    //                icon: "fa fa-danger" // put icon before the message
    //            });
    //        }
    //    });
    //},
    deleterow: function (rowid, newdata, commit) {
        $.ajax({
            dataType: 'json',
            cache: false,
            url: baseUrl + "Sub_MttoTFuerza/EliminarCorrienteExit",
            type: "POST",
            data: {
                idCE: idCE
            },

            success: function (data, status, xhr) {
                // delete command is executed.
                $("#TablaCorrienteExit").jqxGrid('updatebounddata');

                App.alert({
                    container: "#bootstrap_alerts_CorrienteExit", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "success",  // alert's type
                    message: "La fila se eliminó correctamente.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-success" // put icon before the message
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                App.alert({
                    container: "#bootstrap_alerts_CorrienteExit", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "danger",  // alert's type
                    message: "La fila no ha sido eliminada.",  // alert's message
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



var _columnsCorrienteExitAction = [
    { text: 'idCE', dataField: 'Id_CorrienteExit', hidden: true },
    { text: 'Tap', columntype: 'textbox', filtertype: 'number', dataField: 'Tap', align: 'center', width: '17%', editable: false },
    { text: 'A-0', columntype: 'textbox', filtertype: 'number', dataField: 'A_0', align: 'center', width: '17%', editable: false },
    { text: 'B-0', columntype: 'textbox', filtertype: 'number', dataField: 'B_0', align: 'center', width: '17%', editable: false },
    { text: 'C-0', columntype: 'textbox', filtertype: 'number', dataField: 'C_0', align: 'center', width: '17%', editable: false },
    { text: '% Desviacion', columntype: 'textbox', filtertype: 'number', dataField: 'PorcientoDesviacion', align: 'center', width: '17%', editable: false },
    {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '15%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaCorrienteExit").jqxGrid('getrowid', editrow);
            swal({
                title: '¿Está seguro de eliminar esta fila?',
                text: '¡Los valores de la Tangente delta se eliminarán y no se podrán recuperar!',
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
                    $("#TablaCorrienteExit").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }
];

inicializarCorrienteExit("#TablaCorrienteExit", _sourceCorrienteExitAction, _columnsCorrienteExitAction);

function inicializarCorrienteExit(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '76%',
            height: 400,
            theme: 'energyblue',
            source: _source,
            sortable: true,
            editable: true,
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

$("#TablaCorrienteExit").on('rowselect', function (event) {

    datosCorrienteExit = $("#TablaCorrienteExit").jqxGrid('getrowdata', event.args.rowindex);
    idCE = (datosCorrienteExit.Id_CorrienteExit);

});