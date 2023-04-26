var baseUrl = $('base').attr('href');

var _sourceTablaCircuitosBaja =
    {
        dataType: "json",
        dataFields: [
            { name: 'SubAlimentadora', type: 'string' }, //subestacion
            { name: 'DesconectivoPrincipal', type: 'string' },
            { name: 'CodigoCircuito', type: 'string' }, //codigo del circuito

        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "SubestacionesD/ObtenerListaCircuitosBaja",
        data: { codSub: codSub },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
    };



var _columnsTablaCircuitoBaja = [

    { text: 'Código circuito', columntype: 'textbox', filtertype: 'input', dataField: 'CodigoCircuito', align: 'left', width: '50%' },
    { text: 'Desconectivo principal', columntype: 'textbox', filtertype: 'input', dataField: 'DesconectivoPrincipal', align: 'left', width: '50%' },
];

inicializarTablaBloques("#TablaCircuitoBaja", _sourceTablaCircuitosBaja, _columnsTablaCircuitoBaja);

function inicializarTablaBloques(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '50%',
            height: 350,
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



