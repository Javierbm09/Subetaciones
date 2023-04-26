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
    };



var _columnsTablaBloques = [
    { text: 'Id', dataField: 'Id_bloque', hidden: true },
    { text: 'Sub', dataField: 'Codigo', hidden: true },
    { text: 'Tipo bloque', columntype: 'textbox', filtertype: 'input', dataField: 'tipobloque', align: 'left', width: '14%' },
    { text: 'Tipo salida', columntype: 'textbox', filtertype: 'input', dataField: 'TipoSalida', align: 'left', width: '10%' },
    { text: 'Esquema por baja', columntype: 'textbox', filtertype: 'input', dataField: 'EsquemaPorBaja', align: 'left', width: '14%' },
    { text: 'Voltaje secundario', columntype: 'textbox', filtertype: 'number', dataField: 'VoltajeSecundario', align: 'left', width: '9%' },
    { text: 'Voltaje terciario', columntype: 'textbox', filtertype: 'number', dataField: 'VoltajeTerciario', align: 'left', width: '8%' },
    { text: 'Sector', columntype: 'textbox', filtertype: 'input', dataField: 'Sector', align: 'left', width: '20%' },
    { text: 'Cliente', columntype: 'textbox', filtertype: 'input', dataField: 'Cliente', align: 'left', width: '20%' },
    { text: 'Priorizado', columntype: 'checkbox', filtertype: 'input', dataField: 'Priorizado', align: 'left', width: '5%' },
];

inicializarTablaBloques("#TablaBloques1", _sourceTablaBloques, _columnsTablaBloques);

function inicializarTablaBloques(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '100%',
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



