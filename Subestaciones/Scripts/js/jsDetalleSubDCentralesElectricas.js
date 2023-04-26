var baseUrl = $('base').attr('href');

var _sourceTablaCentrales =
    {
        dataType: "json",
        dataFields: [
            { name: 'CentroTransformacion', type: 'string' }, //subestacion
            { name: 'Nombre', type: 'string' },
            { name: 'Codigo', type: 'string' }, //codigo de la central
            { name: 'Tipo', type: 'string' },
            { name: 'Calle', type: 'string' },
            { name: 'Numero', type: 'int' },
            { name: 'Entrecalle1', type: 'string' },
            { name: 'Entrecalle2', type: 'string' },
            { name: 'BarrioPueblo', type: 'string' },
      
        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "SubestacionesD/ObtenerListaCentralesElectricas",
        data: { codSub: codSub },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
    };



var _columnsTablaCentrales = [
   
    { text: 'Codigo', columntype: 'textbox', filtertype: 'input', dataField: 'Codigo', align: 'left', width: '5%' },
    { text: 'Nombre', columntype: 'textbox', filtertype: 'input', dataField: 'Nombre', align: 'left', width: '15%' },
    { text: 'Tipo', columntype: 'textbox', filtertype: 'input', dataField: 'Tipo', align: 'left', width: '10%' },
    { text: 'Calle', columntype: 'textbox', filtertype: 'input', dataField: 'Calle', align: 'left', width: '10%' },
    { text: 'Número', columntype: 'textbox', filtertype: 'number', dataField: 'Numero', align: 'left', width: '5%' },
    { text: 'EntreCalle1', columntype: 'textbox', filtertype: 'input', dataField: 'EntreCalle1', align: 'left', width: '20%' },
    { text: 'EntreCalle2', columntype: 'textbox', filtertype: 'input', dataField: 'EntreCalle2', align: 'left', width: '20%' },
    { text: 'BarrioPueblo', columntype: 'textbox', filtertype: 'input', dataField: 'BarrioPueblo', align: 'left', width: '15%' },
];

inicializarTablaBloques("#TablaCentralesE", _sourceTablaCentrales, _columnsTablaCentrales);

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



