var baseUrl = $('base').attr('href');

var _sourceTablaTermometros =
    {
        dataType: "json",
        dataFields: [
            { name: 'Id_Termometro', type: 'int' },
            { name: 'Id_EAdministrativa', type: 'int' },
            { name: 'Id_Transformador', type: 'int' },
            { name: 'NumAccion', type: 'int' },
            { name: 'Numero', type: 'string' },
            { name: 'Tipo', type: 'string' },
            { name: 'Rango', type: 'number' },
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 10,
        url: baseUrl + "TransformadoresSubtransmisions/ObtenerListaTermometros",
        data: { idTransf: transf },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
      
    };



var _columnsTablaTermometros = [
    { text: 'transformador', dataField: 'Id_Termometro', hidden: true },
    { text: 'Id_EAdministrativa', dataField: 'Id_EAdministrativa', hidden: true },
    { text: 'Id_Transformador', dataField: 'Id_Transformador', hidden: true },
    { text: 'NumAccion', dataField: 'NumAccion', hidden: true },
    { text: 'Termómetro', columntype: 'textbox', filtertype: 'input', dataField: 'Numero', align: 'left', width: '40%' },
    { text: 'Tipo', columntype: 'textbox', filtertype: 'input', dataField: 'Tipo', align: 'left', width: '30%' },
    { text: 'Rango (°C)', columntype: 'textbox', filtertype: 'number', dataField: 'Rango', align: 'left', width: '30%' },
];

inicializarTablaTermometros("#TablaTermometro", _sourceTablaTermometros, _columnsTablaTermometros);

function inicializarTablaTermometros(_idTabla, _source, columns) {
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

