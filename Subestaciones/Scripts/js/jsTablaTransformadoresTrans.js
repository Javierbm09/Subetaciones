var baseUrl = $('base').attr('href');

var _sourceTablaTransformadores =
    {
        dataType: "json",
        dataFields: [
            { name: 'Subestacion', type: 'string' },
            { name: 'Nombre', type: 'string' },
            { name: 'Numemp', type: 'string' },
            { name: 'Capacidad', type: 'short' },
            { name: 'Id_VoltajePrim', type: 'short' },
            { name: 'Id_Voltaje_Secun', type: 'short' },
            { name: 'VoltajeTerciario', type: 'short' },
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "SubestacionesTransmisions/ObtenerListaTransformadoresTrans",
        data: { codSub: codSub },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },

    };



var _columnsTablaTransformadores = [
    { text: 'codSub', dataField: 'Subestacion', hidden: true },
    { text: 'Nombre', columntype: 'textbox', filtertype: 'input', dataField: 'Nombre', align: 'left', width: '40%' },
    { text: 'Número Empresa', columntype: 'textbox', filtertype: 'input', dataField: 'Numemp', align: 'left', width: '15%' },
    { text: 'Capacidad', columntype: 'textbox', filtertype: 'input', dataField: 'Capacidad', align: 'left', width: '15%' },
    { text: 'Tensión Primaria', columntype: 'textbox', filtertype: 'input', dataField: 'Id_VoltajePrim', align: 'left', width: '10%' },
    { text: 'Tensión Secundaria', columntype: 'textbox', filtertype: 'input', dataField: 'Id_Voltaje_Secun', align: 'left', width: '10%' },
    { text: 'Tensión Terciaria', columntype: 'textbox', filtertype: 'input', dataField: 'VoltajeTerciario', align: 'left', width: '10%' },
];

inicializarTablaTransfroamdores("#TablaTransf", _sourceTablaTransformadores, _columnsTablaTransformadores);

function inicializarTablaTransfroamdores(_idTabla, _source, columns) {
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
