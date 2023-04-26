var baseUrl = $('base').attr('href');

var _sourceTablaTermometros =
    {
        dataType: "json",
        dataFields: [
            { name: 'Id_Termometro', type: 'int' },
            { name: 'Id_EAdministrativa', type: 'int' },
            { name: 'Id_Transformador', type: 'int' },
            { name: 'NumAccion', type: 'int' },
            { name: 'Numero', type: 'string' },
            { name: 'Tipo', type: 'string' },
            { name: 'Rango', type: 'number' },
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 10,
        url: baseUrl + "TransformadoresSubtransmisions/ObtenerListaTermometros",
        data: { idTransf: transf },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
        updaterow: function (rowid, newdata, commit) {
            console.log(rowid);
            $.ajax({
                cache: false,
                dataType: 'json',
                url: baseUrl + "TransformadoresSubtransmisions/ActualizarTermometro",
                data: newdata,
                type: "POST",
                success: function (data, status, xhr) {
                    $("#TablaTermometro").jqxGrid('updatebounddata');

                    // insert command is executed.
                    App.alert({
                        container: "#bootstrap_alerts_Termometros", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "Se editó correctamente el termómetro.",  // alert's message
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
                        container: "#bootstrap_alerts_Termometros", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "No se pudo editar el termómetro.",  // alert's message
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
                url: baseUrl + "TransformadoresSubtransmisions/EliminarTermometro",
                type: "POST",
                data: {
                    EA: idEA, id_t: idT, id: IdTermometro, NumAccion: numA 

                },

                success: function (data, status, xhr) {
                    // delete command is executed.
                    $("#TablaTermometro").jqxGrid('updatebounddata');

                    App.alert({
                        container: "#bootstrap_alerts_Termometros", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "El termómetro fue eliminado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-success" // put icon before the message
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    App.alert({
                        container: "#bootstrap_alerts_Termometros", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "El termómetro no ha sido eliminado.",  // alert's message
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



var _columnsTablaTermometros = [
    { text: 'transformador', dataField: 'Id_Termometro', hidden: true },
    { text: 'Id_EAdministrativa', dataField: 'Id_EAdministrativa', hidden: true },
    { text: 'Id_Transformador', dataField: 'Id_Transformador', hidden: true },
    { text: 'NumAccion', dataField: 'NumAccion', hidden: true },
    { text: 'Termómetro', columntype: 'textbox', filtertype: 'input', dataField: 'Numero', align: 'left', width: '40%' },
    { text: 'Tipo', columntype: 'textbox', filtertype: 'input', dataField: 'Tipo', align: 'left', width: '20%' },
    { text: 'Rango (°C)', columntype: 'textbox', filtertype: 'number', dataField: 'Rango', align: 'left', width: '20%' },

    {
        text: '', datafield: 'Edit', columntype: 'button', width: '10%', id: 'editarBt', cellsrenderer: function () {
            return "Editar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.
            editrow = row;
            var offset = $("#TablaTermometro").offset();
            $("#popEditaTermo").jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });

            // get the clicked row's data and initialize the input fields.
            var datos = $("#TablaTermometro").jqxGrid('getrowdata', editrow);
            $("#_editaId_Termometro").val(datos.Id_Termometro);
            $("#_editaId_EAdministrativa").val(datos.Id_EAdministrativa);
            $("#_editaId_Transformador").val(datos.Id_Transformador);
            $("#_editaNumAccion").val(datos.NumAccion);
            $("#_editaTermometro").val(datos.Numero);
            $("#_editaTipo").val(datos.Tipo);
            $("#_editaRango").val(datos.Rango);


            // show the popup window.
            $("#popEditaTermo").jqxWindow('open');
        }
    },
    {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '10%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaTermometro").jqxGrid('getrowid', editrow);
            console.log(filaSeleccionada);
            swal({
                title: '¿Está seguro de eliminar el termómetro seleccionado?',
                text: '¡Los valores del termómetro se eliminaran y no se podrán recuperar!',
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
                    $("#TablaTermometro").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }

];

inicializarTablaTermometros("#TablaTermometro", _sourceTablaTermometros, _columnsTablaTermometros);

function inicializarTablaTermometros(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '100%',
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

$("#TablaTermometro").on('rowselect', function (event) {

    termom = $("#TablaTermometro").jqxGrid('getrowdata', event.args.rowindex);
    idEA = (termom.Id_EAdministrativa);
    numA = (termom.NumAccion);
    idT = (termom.Id_Transformador);
    IdTermometro = (termom.Id_Termometro);
    termometro = (termom.Numero);
    tipo = (termom.Tipo);
    rango = (termom.Rango);


});