var baseUrl = $('base').attr('href');

var _sourceTablaCircuitos =
{
    dataType: "json",
    dataFields: [
        { name: 'Subestacion', type: 'string' },
        { name: 'Circuito', type: 'string' },
        { name: 'Seccionalizador', type: 'string' },
    ],
    id: 'id',
    pagenum: 3,
    pagesize: 20,
    url: baseUrl + "SubestacionesD/ObtenerListaCircuitosAlta",
    data: { codSub: codSub },
    type: 'GET',
    pager: function (pagenum, pagesize, oldpagenum) {
        // callback called when a page or page size is changed.
    },
    updaterow: function (rowid, newdata, commit) {
        console.log(rowid);
        $.ajax({
            cache: false,
            dataType: 'json',
            url: baseUrl + "SubestacionesD/ActualizarCircuito",
            data: newdata,
            type: "POST",
            success: function (data, status, xhr) {
                $("#TablaCircuitosAlta1").jqxGrid('updatebounddata');

                // insert command is executed.
                App.alert({
                    container: "#bootstrap_alerts_Circuitos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "success",  // alert's type
                    message: "Se editó correctamente el circuito.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-success" // put icon before the message
                });

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus);
                console.log(jqXHR);
                console.log(errorThrown);

                commit(false);
                App.alert({
                    container: "#bootstrap_alerts_Circuitos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "danger",  // alert's type
                    message: "No se pudo editar el circuito.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-danger" // put icon before the message
                });
            }
        });
    },
    deleterow: function (rowid, newdata, commit) {
        $.ajax({
            dataType: 'json',
            cache: false,
            url: baseUrl + "SubestacionesD/EliminarCircuito",
            type: "POST",
            data: {
                sub: codSub, circuito: codCircuito

            },

            success: function (data, status, xhr) {
                // delete command is executed.
                $("#TablaCircuitosAlta1").jqxGrid('updatebounddata');

                App.alert({
                    container: "#bootstrap_alerts_Circuitos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "success",  // alert's type
                    message: "El circuito fue eliminado.",  // alert's message
                    close: 1, // make alert closable
                    reset: 1, // close all previouse alerts first
                    focus: 1, // auto scroll to the alert after shown
                    closeInSeconds: 5,//, // auto close after defined seconds
                    icon: "fa fa-success" // put icon before the message
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                App.alert({
                    container: "#bootstrap_alerts_Circuitos", // alerts parent container(by default placed after the page breadcrumbs)
                    place: "prepend", // append or prepent in container
                    type: "danger",  // alert's type
                    message: "El circuito no ha sido eliminado.",  // alert's message
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



var _columnsTablaCircuitos = [
    { text: 'codSub', dataField: 'Subestacion', hidden: true },
    { text: 'Circuito', columntype: 'textbox', filtertype: 'input', dataField: 'Circuito', align: 'left', width: '60%' },
    { text: 'Seccionalizador', columntype: 'textbox', filtertype: 'number', dataField: 'Seccionalizador', align: 'left', width: '20%' },

    {
        text: '', datafield: 'Edit', columntype: 'button', width: '10%', id: 'editarBt', cellsrenderer: function () {
            return "Editar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.
            editrow = row;
            var offset = $("#TablaCircuitosAlta1").offset();
            $("#popEditaCircuito").jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });

            // get the clicked row's data and initialize the input fields.
            var datos = $("#TablaCircuitosAlta1").jqxGrid('getrowdata', editrow);
            $("#sub").val(datos.Subestacion);
            $("#IdCircuito").val(datos.Circuito);
            $("#IdSecc").val(datos.Seccionalizador);


            // show the popup window.
            $("#popEditaCircuito").jqxWindow('open');
        }
    },
    {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '10%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaCircuitosAlta1").jqxGrid('getrowid', editrow);
            console.log(filaSeleccionada);
            swal({
                title: '¿Está seguro de eliminar el circuito seleccionado?',
                text: '¡Los valores del circuito y seccionalizador se eliminaran y no se podrán recuperar!',
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
                    $("#TablaCircuitosAlta1").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }

];

inicializarTablaCircuito("#TablaCircuitosAlta1", _sourceTablaCircuitos, _columnsTablaCircuitos);

function inicializarTablaCircuito(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '40%',
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

$("#TablaCircuitosAlta1").on('rowselect', function (event) {

    circuitoSeleccionado = $("#TablaCircuitosAlta1").jqxGrid('getrowdata', event.args.rowindex);
    codSub = (circuitoSeleccionado.Subestacion);
    codCircuito = (circuitoSeleccionado.Circuito);


});