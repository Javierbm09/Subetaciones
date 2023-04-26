var baseUrl = $('base').attr('href');

var _sourceTablaTransformadores =
    {
        dataType: "json",
        dataFields: [
            { name: 'Codigo', type: 'string' },
            { name: 'Numemp', type: 'string' },
            { name: 'Capacidad', type: 'double' },
            { name: 'Fabricante', type: 'string' },
            { name: 'VoltajePrim', type: 'double' },
            { name: 'VoltajeSecun', type: 'double' },
            { name: 'Fase', type: 'string' },
            { name: 'EstadoOperativo', type: 'string' },
            { name: 'TapDejado', type: 'short' },
            { name: 'NumFase', type: 'short' }
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "Sub_RedCorrienteAlterna/CargarTablaTransformadores",
        data: { banco: function () { return codigoBanco; }  },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },

    };



var _columnsTablaTransformadores = [
    { text: 'codSub', dataField: 'Subestacion', hidden: true },
    { text: 'Código', columntype: 'textbox', filtertype: 'input', dataField: 'Codigo', align: 'left', width: '10%' },
    { text: 'Nro Empresa', columntype: 'textbox', filtertype: 'input', dataField: 'Numemp', align: 'left', width: '10%' },
    { text: 'Capacidad', columntype: 'textbox', filtertype: 'input', dataField: 'Capacidad', align: 'left', width: '10%' },
    { text: 'Fabricante', columntype: 'textbox', filtertype: 'input', dataField: 'Fabricante', align: 'left', width: '10%' },
    { text: 'Tensión Primaria', columntype: 'textbox', filtertype: 'input', dataField: 'VoltajePrim', align: 'left', width: '10%' },
    { text: 'Tensión Secundaria', columntype: 'textbox', filtertype: 'input', dataField: 'VoltajeSecun', align: 'left', width: '10%' },
    { text: 'Fase', columntype: 'textbox', filtertype: 'input', dataField: 'Fase', align: 'left', width: '10%' },
    { text: 'Estado Operativo', columntype: 'textbox', filtertype: 'input', dataField: 'EstadoOperativo', align: 'left', width: '10%' },
    { text: 'Tap dejado', columntype: 'textbox', filtertype: 'input', dataField: 'TapDejado', align: 'left', width: '10%' },
    { text: 'Nro Fase', columntype: 'textbox', filtertype: 'input', dataField: 'NumFase', align: 'left', width: '10%' }
    
];


function inicializarTablaTransf(_idTabla, _source, columns) {
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
