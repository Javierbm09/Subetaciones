var baseUrl = $('base').attr('href');

var _sourceTablaInstrumentos =
{
    dataType: "json",
    dataFields: [
        { name: 'Instrumento', type: 'string' },
        { name: 'ModeloTipo', type: 'string' },
    ],
    id: 'id',
    pagenum: 3,
    pagesize: 20,
    url: baseUrl + "Sub_MttoBateriasEstacionarias/ObtenerListaInstrumentos",
    data: { idMBE: idMBE, idBateria: idB, redEA: ea_red},
    type: 'GET',
    pager: function (pagenum, pagesize, oldpagenum) {
        // callback called when a page or page size is changed.
    },
};



var _columnsTablaInstrumentos = [
    { text: 'Instrumento', columntype: 'textbox', filtertype: 'input', dataField: 'Instrumento', align: 'center', width: '50%' },
    { text: 'Modelo o tipo', columntype: 'textbox', filtertype: 'input', dataField: 'ModeloTipo', align: 'center', width: '50%' }
];

inicializarTablaInstrumento("#TablaInstrumentosBateriasEstacionarias", _sourceTablaInstrumentos, _columnsTablaInstrumentos);

function inicializarTablaInstrumento(_idTabla, _source, columns) {
    $(_idTabla).jqxGrid(
        {
            width: '40.5%',
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

$("#TablaInstrumentosBateriasEstacionarias").on('rowselect', function (event) {
    instrumentoSeleccionado = $("#TablaInstrumentosBateriasEstacionarias").jqxGrid('getrowdata', event.args.rowindex);
    instr = (instrumentoSeleccionado.Instrumento);
});