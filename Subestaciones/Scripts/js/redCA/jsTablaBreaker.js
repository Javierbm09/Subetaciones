var baseUrl = $('base').attr('href');

var _sourceTablaBreakers =
{
    dataType: "json",
    dataFields: [
        { name: 'CodigoDesconectivo', type: 'string' },
        { name: 'TensionNominal', type: 'double' },
        { name: 'CorrienteNominal', type: 'double' }
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
    { text: 'Código', columntype: 'textbox', filtertype: 'input', dataField: 'CodigoDesconectivo', align: 'left', width: '10%' },
    { text: 'Tensión Nominal', columntype: 'textbox', filtertype: 'input', dataField: 'TensionNominal', align: 'left', width: '10%' },
    { text: 'Corriente Nominal', columntype: 'textbox', filtertype: 'input', dataField: 'CorrienteNominal', align: 'left', width: '10%' },
    {
        text: '', datafield: 'Edit', columntype: 'button', width: '10%', id: 'editarBt', cellsrenderer: function () {
            return "Editar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.
            editrow = row;
            var offset = $("#Breakers").offset();
            $("#popEditaBreaker").jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });

            // get the clicked row's data and initialize the input fields.
            var datos = $("#Breakers").jqxGrid('getrowdata', editrow);
            $("#_CodigoBreaker").val(datos.CodigoDesconectivo);
            $("#_tension").val(datos.TensionNominal);
            $("#_corriente").val(datos.CorrienteNominal);
            // show the popup window.
            $("#popEditaBreaker").jqxWindow('open');
        }
    }
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
            editable: true,
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
