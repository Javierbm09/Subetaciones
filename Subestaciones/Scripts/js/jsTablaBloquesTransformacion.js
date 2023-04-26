var baseUrl = $('base').attr('href');

var _sourceTablaBloques =
    {
        dataType: "json",
        dataFields: [
            { name: 'Codigo', type: 'string' },
            { name: 'Id_bloque', type: 'int' },
            { name: 'tipobloque', type: 'string' },
            { name: 'idEsquemaPorBaja', type: 'short' },
            { name: 'EsquemaPorBaja', type: 'short' },
            { name: 'idVoltajeSecundario', type: 'short' },
            { name: 'VoltajeSecundario', type: 'short' },
            { name: 'idVoltajeTerciario', type: 'short' },
            { name: 'VoltajeTerciario', type: 'short' },
            { name: 'TipoSalida', type: 'string' },
            { name: 'Priorizado', type: 'bool' },
            { name: 'Sector', type: 'string' },
            { name: 'idSector', type: 'string' },
            { name: 'Cliente', type: 'string' },
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "SubestacionesD/ObtenerListaBloques",
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
                url: baseUrl + "SubestacionesD/ActualizarBloque",
                data: newdata,
                type: "POST",
                success: function (data, status, xhr) {
                    $("#TablaBloques1").jqxGrid('updatebounddata');

                    // insert command is executed.
                    App.alert({
                        container: "#bootstrap_alerts_Bloques", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "Se editó correctamente el bloque de transformación.",  // alert's message
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
                        container: "#bootstrap_alerts_Bloques", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "No se pudo editar el bloque de transformación.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-danger" // put icon before the message
                    });
                }
            });
        },
        deleterow: function (rowid, commit) {
            $.ajax({
                dataType: 'json',
                cache: false,
                url: baseUrl + 'SubestacionesD/EliminarBloque',
                type: "POST",
                data: {
                    sub: codSub, bloque: codBloque 
                },
                success: function (data, status, xhr) {
                    $("#TablaBloques1").jqxGrid('updatebounddata');
             

                    // delete command is executed.
                    App.alert({
                        container: "#bootstrap_alerts_Bloques", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "El bloque de transformación fue eliminado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-success" // put icon before the message
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    App.alert({
                        container: "#bootstrap_alerts_Bloques", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "El bloque de transformación no ha sido eliminado.",  // alert's message
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



var _columnsTablaBloques = [
    { text: 'Id', dataField: 'Id_bloque', hidden: true },
    { text: 'Sub', dataField: 'Codigo', hidden: true },
    { text: 'Tipo bloque', columntype: 'textbox', filtertype: 'input', dataField: 'tipobloque', align: 'left', width: '9%' },
    { text: 'Tipo salida', columntype: 'textbox', filtertype: 'input', dataField: 'TipoSalida', align: 'left', width: '6%' },
    { text: 'Esquema por baja', columntype: 'textbox', filtertype: 'input', dataField: 'EsquemaPorBaja', align: 'left', width: '13%' },
    { text: 'Voltaje secundario', columntype: 'textbox', filtertype: 'number', dataField: 'VoltajeSecundario', align: 'left', width: '9%' },
    { text: 'Voltaje terciario', columntype: 'textbox', filtertype: 'number', dataField: 'VoltajeTerciario', align: 'left', width: '8%' },
    { text: 'Sector', columntype: 'textbox', filtertype: 'input', dataField: 'Sector', align: 'left', width: '20%' },
    { text: 'Cliente', columntype: 'textbox', filtertype: 'input', dataField: 'Cliente', align: 'left', width: '20%' },
    { text: 'Priorizado', columntype: 'checkbox', filtertype: 'input', dataField: 'Priorizado', align: 'left', width: '5%' },
    {
        text: '', datafield: 'Edit', columntype: 'button', width: '5%', cellsrenderer: function () {
            return "Editar";
        },
        buttonclick: function (row) {
            // open the popup window when the user clicks a button.
            editrow = row;
            var offset = $("#TablaBloques1").offset();
            $("#popEditaSecc").jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });

            // get the clicked row's data and initialize the input fields.
            var datos = $("#TablaBloques1").jqxGrid('getrowdata', editrow);
            $("#_sub").val(datos.Codigo);
            $("#_Id_bloque").val(datos.Id_bloque);
            $("#_EditaTipobloque").val(datos.tipobloque);
            $("#_EditaTipoSalida").val(datos.TipoSalida);
            $("#_EditaEsquemaPorBaja").val(datos.idEsquemaPorBaja);
            $("#_EditaVoltajeSecundario").val(datos.idVoltajeSecundario);
            $("#_EditaVoltajeTerciario").val(datos.idVoltajeTerciario);
            $("#_EditaSector").val(datos.idSector);
            $("#_editaCliente").val(datos.Cliente);
            $("#_editaPriorizado").val(datos.Priorizado);

            // show the popup window.
            $("#popEditaSecc").jqxWindow('open');
        }
    },
    {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '5%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaBloques1").jqxGrid('getrowid', editrow);
            swal({
                title: '¿Está seguro de eliminar el bloque de transformación seleccionado?',
                text: '¡Los valores de los bloques se eliminarán y no se podrán recuperar!',
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
                    $("#TablaBloques1").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }

];

inicializarTablaBloques("#TablaBloques1", _sourceTablaBloques, _columnsTablaBloques);

function inicializarTablaBloques(_idTabla, _source, columns) {
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
$("#TablaBloques1").on('rowselect', function (event) {

    bloqueSeleccionado = $("#TablaBloques1").jqxGrid('getrowdata', event.args.rowindex);
    codSub = (bloqueSeleccionado.Codigo);
    codBloque = (bloqueSeleccionado.Id_bloque);
    console.log(codSub);
    console.log(codBloque);

});


