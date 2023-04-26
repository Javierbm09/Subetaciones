var baseUrl = $('base').attr('href');

var _sourceTablaBreakers =
    {
        dataType: "json",
        dataFields: [
            { name: 'CodigoDesconectivo', type: 'string' },
            { name: 'TensionNominal', type: 'double' },
            { name: 'CorrienteNominal', type: 'double' },
            { name: 'CodigoSub', type: 'string' },
            { name: 'RedCA', type: 'short' }
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "Sub_RedCorrienteAlterna/CargarTablaBreakers",
        data: { codigoSubestacion: codSub, idRedCA: red },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },

    };



var _columnsTablaBreakers = [
    { text: 'codSub', dataField: 'CodigoSub', hidden: true },
    { text: 'Código', columntype: 'textbox', filtertype: 'input', dataField: 'CodigoDesconectivo', align: 'left', width: '10%' },
    { text: 'Tensión Nominal', columntype: 'textbox', filtertype: 'input', dataField: 'TensionNominal', align: 'left', width: '10%' },
    { text: 'Corriente Nominal', columntype: 'textbox', filtertype: 'input', dataField: 'CorrienteNominal', align: 'left', width: '10%' }
];

inicializarTablaBreaker("#Breakers", _sourceTablaBreakers, _columnsTablaBreakers);

function inicializarTablaBreaker(_idTabla, _source, columns) {
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
