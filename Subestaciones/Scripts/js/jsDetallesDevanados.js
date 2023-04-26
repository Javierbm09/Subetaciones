﻿var baseUrl = $('base').attr('href');

var _sourceTablaTransformadoresDevanados =
    {
        dataType: "json",
        dataFields: [
            { name: 'Nro_Dev', type: 'short' },
            { name: 'Nro_TC', type: 'string' },
            { name: 'Capacidad', type: 'short' },
            { name: 'Clase_Precision', type: 'string' },
            { name: 'Designacion', type: 'string' },

        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "ES_TransformadorCorriente/ObtenerListaDevanados",
        data: { tc: TC },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
        updaterow: function (rowid, newdata, commit) {
            console.log(rowid);
            $.ajax({
                cache: false,
                dataType: 'json',
                url: baseUrl + "ES_TransformadorCorriente/ActualizarDevanado",
                data: newdata,
                type: "POST",
                success: function (data, status, xhr) {
                    $("#TablaDevanado").jqxGrid('updatebounddata');

                    // insert command is executed.
                    App.alert({
                        container: "#bootstrap_alerts_Devanados", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "Se editó correctamente el devanado.",  // alert's message
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
                        container: "#bootstrap_alerts_Devanados", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "No se pudo editar el devanado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-danger" // put icon before the message
                    });
                }
            });
        }

    };



var _columnsTablaDevanados = [
    { text: 'TC', dataField: 'Nro_TC', hidden: true },
    { text: 'Número', columntype: 'textbox', filtertype: 'input', dataField: 'Nro_Dev', align: 'left', width: '10%' },
    { text: 'Designación', columntype: 'textbox', filtertype: 'input', dataField: 'Designacion', align: 'left', width: '30%' },
    { text: 'CLase precisión', columntype: 'textbox', filtertype: 'input', dataField: 'Clase_Precision', align: 'left', width: '40%' },
    { text: 'Capacidad (VA)', columntype: 'textbox', filtertype: 'input', dataField: 'Capacidad', align: 'left', width: '20%' },
   
];

inicializarTablaTransfroamdores("#TablaDevanado", _sourceTablaTransformadoresDevanados, _columnsTablaDevanados);

function inicializarTablaTransfroamdores(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '50%',
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