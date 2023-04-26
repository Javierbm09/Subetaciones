var baseUrl = $('base').attr('href');

var _sourceTablaTransformadores =
    {
        dataType: "json",
        dataFields: [
            { name: 'Codigo', type: 'string' },
            { name: 'Seccionalizador', type: 'string' }
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "Sub_RedCorrienteAlterna/CargarTablaBancosTransformadores",
        data: { codigoSubestacion: codSub },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },

    };



var _columnsTablaTransformadores = [
    { text: 'Código', columntype: 'textbox', filtertype: 'input', dataField: 'Codigo', align: 'left', width: '50%' },
    { text: 'Seccionalizador', columntype: 'textbox', filtertype: 'input', dataField: 'Seccionalizador', align: 'left', width: '50%' }
];

inicializarTablaBancoTransf("#tablaBancosTransformadores", _sourceTablaTransformadores, _columnsTablaTransformadores);

function inicializarTablaBancoTransf(_idTabla, _source, columns) {
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

$("#tablaBancosTransformadores").on('rowselect', function (event) {
    filaSeleccionada = $("#tablaBancosTransformadores").jqxGrid('getrowid', event.args.rowindex);
    tabla = $("#tablaBancosTransformadores").jqxGrid('getrowdata', event.args.rowindex);
    codigoBanco = (tabla.Codigo);
    inicializarTablaTransf("#tablaTransformadoresDelBanco", _sourceTablaTransformadores, _columnsTablaTransformadores);
});