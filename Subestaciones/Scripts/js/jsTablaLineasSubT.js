var baseUrl = $('base').attr('href');

var _sourceTablaLineas =
    {
        dataType: "json",
        dataFields: [
            { name: 'SubestacionTransmicion', type: 'string' },
            { name: 'Codigolinea', type: 'string' },
            { name: 'DesconectivoA', type: 'string' },
            { name: 'DesconectivoB', type: 'string' },
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "SubestacionesTransmisions/ObtenerListaLineas",
        data: { codSub: codSub },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
       
    };



var _columnsTablaLineas = [
    { text: 'codSub', dataField: 'SubestacionTransmicion', hidden: true },
    { text: 'Circuito', columntype: 'textbox', filtertype: 'input', dataField: 'Codigolinea', align: 'left', width: '60%' },
    { text: 'Desconectivo A', columntype: 'textbox', filtertype: 'input', dataField: 'DesconectivoA', align: 'left', width: '20%' },
    { text: 'Desconectivo B', columntype: 'textbox', filtertype: 'input', dataField: 'DesconectivoB', align: 'left', width: '20%' },

   

];

inicializarTablaLineas("#Lineas", _sourceTablaLineas, _columnsTablaLineas);

function inicializarTablaLineas(_idTabla, _source, columns) {
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
