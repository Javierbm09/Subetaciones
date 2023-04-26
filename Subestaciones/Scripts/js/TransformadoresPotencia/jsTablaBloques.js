$("#TablaBloques").trigger('reloadTablaBloques');


var baseUrl = $('base').attr('href');
var codigo;





var _columnsTablaBloques = [
    { text: 'Id_bloque', dataField: 'Id_bloque', hidden: true },
    { text: 'Tipo bloque', columntype: 'textbox', filtertype: 'input', dataField: 'tipobloque', align: 'left', width: '20%' },
    { text: 'Esquema por baja', columntype: 'textbox', filtertype: 'input', dataField: 'EsquemaPorBaja', align: 'left', width: '20%' },
    { text: 'Sector cliente', columntype: 'textbox', filtertype: 'input', dataField: 'Sector', align: 'left', width: '20%' },
    { text: 'Cliente', columntype: 'textbox', filtertype: 'input', dataField: 'Cliente', align: 'left', width: '20%' },
    { text: 'Tensión terciario', columntype: 'textbox', filtertype: 'input', dataField: 'VoltajeTerciario', align: 'left', width: '20%' },
    { text: 'Tensión salida', columntype: 'textbox', filtertype: 'input', dataField: 'VoltajeSalida', align: 'left', width: '20%' },
    { text: 'Tipo salida', columntype: 'textbox', filtertype: 'input', dataField: 'TipoSalida', align: 'left', width: '20%' },

];



function inicializarTablaBloque(_idTabla, _source, columns) {
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
            selectionmode: 'singlerow',

            rendertoolbar: function (statusbar) {
                var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                statusbar.append(container);
            },
            columns: columns,
        });
}

$("#TablaBloques").on('rowselect', function (event) {

    bloqueSeleccionado = $("#TablaBloques").jqxGrid('getrowdata', event.args.rowindex);
    $("#bloqueSeleccionado").val(bloqueSeleccionado.tipobloque);
    $("#Bloque").val(bloqueSeleccionado.tipobloque);
    $("#Id_Bloque").val(bloqueSeleccionado.Id_bloque);
    $("#esquemaBloque").val(bloqueSeleccionado.EsquemaPorBaja);
    $("#sectorClienteBloque").val(bloqueSeleccionado.Sector);
    $("#clienteBloque").val(bloqueSeleccionado.Cliente);
    $("#tensionTerciarioBloque").val(bloqueSeleccionado.VoltajeTerciario);
    $("#tensionSalidaBloque").val(bloqueSeleccionado.VoltajeSalida);
    $("#tipoSalidaBloque").val(bloqueSeleccionado.TipoSalida);

});