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
     
    };



var _columnsTablaCircuitos = [
    { text: 'codSub', dataField: 'Subestacion', hidden: true },
    { text: 'Circuito', columntype: 'textbox', filtertype: 'input', dataField: 'Circuito', align: 'left', width: '60%' },
    { text: 'Seccionalizador', columntype: 'textbox', filtertype: 'number', dataField: 'Seccionalizador', align: 'left', width: '40%' },
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

