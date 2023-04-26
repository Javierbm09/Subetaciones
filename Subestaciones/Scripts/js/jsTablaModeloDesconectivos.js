var baseUrl = $('base').attr('href');
var idT;
var _sourceTablaModelosDesc =
    {
        dataType: "json",
        dataFields: [
            { name: 'Id_Nomenclador', type: 'int' },
            { name: 'id_EAdministrativa_Prov', type: 'int' },
            { name: 'Id_Administrativa', type: 'int' },
            { name: 'NumAccion', type: 'int' },
            { name: 'tipoDesc', type: 'int' },
            { name: 'Id_ModeloDesconectivo', type: 'int' },
            { name: 'Id_Fabricante', type: 'int' },
            { name: 'Id_MedioExtinsion', type: 'int' },
            { name: 'Id_PresionGas', type: 'int' },
            { name: 'Id_Bil', type: 'int' },
            { name: 'Id_ApertCable', type: 'int' },
            { name: 'Id_Aislamiento', type: 'int' },
            { name: 'Id_Corriente', type: 'int' },
            { name: 'Id_Cortocircuito', type: 'int' },
            { name: 'Id_Tension', type: 'int' },
            { name: 'Id_SecuenciaOperacion', type: 'int' },
            { name: 'Id_MedioExtinsion', type: 'int' },
            { name: 'modelo', type: 'string' },
            { name: 'Fabricante', type: 'string' },
            { name: 'tension', type: 'string' },
            { name: 'corrienteN', type: 'string' },
            { name: 'corrienteCorto', type: 'string' },
            { name: 'corrienteA', type: 'string' },
            { name: 'Bil', type: 'string' },
            { name: 'Tanque', type: 'int' },
            { name: 'SecOpe', type: 'string' },
            { name: 'ExtArco', type: 'string' },
            { name: 'Aislamiento', type: 'string' },
            { name: 'presion', type: 'string' },
            { name: 'pesoGas', type: 'double' },
            { name: 'pesoInt', type: 'double' },
            { name: 'pesoGab', type: 'double' },
            { name: 'pesoTotal', type: 'double' },

        ],
        id: 'id',
        pagenum: 3,
        pagesize: 20,
        url: baseUrl + "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/ObtenerListaModelos",
        data: { tipoDesc: idT },
        type: 'GET',
        pager: function (pagenum, pagesize, oldpagenum) {
            // callback called when a page or page size is changed.
        },
        updaterow: function (rowid, newdata, commit) {
            console.log(rowid);
            $.ajax({
                cache: false,
                dataType: 'json',
                url: baseUrl + "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/ActualizarModelo",
                data: newdata,
                type: "POST",
                success: function (data, status, xhr) {
                    $("#TablaModelos").jqxGrid('updatebounddata');

                    // insert command is executed.
                    App.alert({
                        container: "#bootstrap_alerts_Modelo", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "Se editó correctamente el modelo.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-success" // put icon before the message
                    });

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus);
                    console.log(jqXHR);
                    console.log(errorThrown);

                    commit(false);
                    App.alert({
                        container: "#bootstrap_alerts_Modelo", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "No se pudo editar el devanado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-danger" // put icon before the message
                    });
                }
            });
        },
        deleterow: function (rowid, newdata, commit) {
            $.ajax({
                dataType: 'json',
                cache: false,
                url: baseUrl + "Inst_Nomenclador_Desconectivos_DatosEspecificos_Comunicacion/EliminarModelo",
                type: "POST",
                data: {
                    id_modelo: rowid

                },

                success: function (data, status, xhr) {
                    // delete command is executed.
                    $("#TablaModelos").jqxGrid('updatebounddata');

                    App.alert({
                        container: "#bootstrap_alerts_Modelo", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "success",  // alert's type
                        message: "El modelo fue eliminado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-success" // put icon before the message
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    App.alert({
                        container: "#bootstrap_alerts_Modelo", // alerts parent container(by default placed after the page breadcrumbs)
                        place: "prepend", // append or prepent in container
                        type: "danger",  // alert's type
                        message: "El modelo no ha sido eliminado.",  // alert's message
                        close: 1, // make alert closable
                        reset: 1, // close all previouse alerts first
                        focus: 1, // auto scroll to the alert after shown
                        closeInSeconds: 5,//, // auto close after defined seconds
                        icon: "fa fa-danger" // put icon before the message
                    });
                }
            });
        },
    };



var _columnsTablaModelosDesc = [
    { text: 'id_modelo', dataField: 'Id_Nomenclador', hidden: true },
    { text: 'EA', dataField: 'Id_Administrativa', hidden: true},
    { text: 'EAProv', dataField: 'id_EAdministrativa_Prov', hidden: true },
    { text: 'numA', dataField: 'NumAccion', hidden: true },
    { text: 'Modelo', columntype: 'textbox', filtertype: 'input', dataField: 'modelo', align: 'left', width: '20%' },
    { text: 'Fabricante', columntype: 'textbox', filtertype: 'input', dataField: 'Fabricante', align: 'left', width: '20%' },
    { text: 'Tensión N', columntype: 'textbox', filtertype: 'input', dataField: 'tension', align: 'left', width: '10%' },
    { text: 'I Nominal', columntype: 'textbox', filtertype: 'input', dataField: 'corrienteN', align: 'left', width: '10%' },
    { text: 'I Cortocircuito', columntype: 'textbox', filtertype: 'input', dataField: 'corrienteCorto', align: 'left', width: '10%' },
    { text: 'I Apertura', columntype: 'textbox', filtertype: 'input', dataField: 'corrienteA', align: 'left', width: '10%' },
    { text: 'Bil', columntype: 'textbox', filtertype: 'input', dataField: 'Bil', align: 'left', width: '5%' },
    { text: 'Tanque', columntype: 'textbox', filtertype: 'input', dataField: 'Tanque', align: 'left', width: '10%' }, 
    { text: 'Secuencia Operaciones', columntype: 'textbox', filtertype: 'input', dataField: 'SecOpe', align: 'left', width: '20%' },
    { text: 'Ext Arco', columntype: 'textbox', filtertype: 'input', dataField: 'ExtArco', align: 'left', width: '10%' },
    { text: 'Aislamiento', columntype: 'textbox', filtertype: 'input', dataField: 'Aislamiento', align: 'left', width: '10%' },
    { text: 'Presión Gas', columntype: 'textbox', filtertype: 'input', dataField: 'presion', align: 'left', width: '10%' }, 
    { text: 'Peso Gas', columntype: 'textbox', filtertype: 'input', dataField: 'pesoGas', align: 'left', width: '10%' },
    { text: 'Peso Int', columntype: 'textbox', filtertype: 'input', dataField: 'pesoInt', align: 'left', width: '10%' },
    { text: 'Peso Gab', columntype: 'textbox', filtertype: 'input', dataField: 'pesoGab', align: 'left', width: '10%' },
    { text: 'Peso Total', columntype: 'textbox', filtertype: 'input', dataField: 'pesoTotal', align: 'left', width: '10%' },
    {
        text: '', datafield: 'Edit', columntype: 'button', width: '15%', cellsrenderer: function () {
            return "Editar";
        },
        buttonclick: function (row) {
            // open the popup window when the user clicks a button.
            editrow = row;
            var offset = $("#TablaModelos").offset();
            //$("#popEditaModelos").jqxWindow({ position: { x: parseInt(offset.left) + 10, y: parseInt(offset.top) + 10 } });
            $("#popEditaModelos").jqxWindow({ position: 'absolute', zIndex: '100'});

            // get the clicked row's data and initialize the input fields.
            var datos = $("#TablaModelos").jqxGrid('getrowdata', editrow);
            $("#_editaId_NomModelo").val(datos.Id_Nomenclador);
            $("#_editaEA").val(datos.Id_Administrativa);
            $("#_editaId_EAdministrativaProv").val(datos.id_EAdministrativa_Prov);
            $("#_editaNumAccion").val(datos.NumAccion);
            $("#_editaDescripcion").val(datos.tipoDesc);
            $("#_EditarModelo option[value=" + datos.Id_ModeloDesconectivo + "]").attr('selected', true);
            $("#_EditarFabricante option[value=" + datos.Id_Fabricante + "]").attr('selected', true);
            $("#_EditarTension option[value=" + datos.Id_Tension + "]").attr('selected', true);
            $("#_EditarCorriente option[value=" + datos.Id_Corriente + "]").attr('selected', true);
            $("#_editarBil option[value=" + datos.Id_Bil + "]").attr('selected', true);
            $("#_editarICortoCircuito option[value=" + datos.Id_Cortocircuito + "]").attr('selected', true);
            $("#_editarTanque option[value" + datos.Tanque + "]").attr('selected', true);
            $("#_editarIAperturaCable option[value=" + datos.Id_ApertCable + "]").attr('selected', true);
            $("#_editarPresionGas option[value=" + datos.Id_PresionGas + "]").attr('selected', true);
            $("#_editarAisl option[value=" + datos.Id_Aislamiento + "]").attr('selected', true);
            $("#_editarSecuenciaOp option[value=" + datos.Id_SecuenciaOperacion + "]").attr('selected', true);
            $("#_editarExtArco option[value=" + datos.Id_MedioExtinsion + "]").attr('selected', true);

            $("#_EditarPGab").val(datos.pesoGab);
            $("#_EditarPInt").val(datos.pesoInt);
            $("#_EditarPGas").val(datos.pesoGas);
            $("#_EditarTotal").val(datos.pesoTotal);
            // show the popup window.
            $("#popEditaModelos").jqxWindow('open');
        }
    },
 {
        text: '', datafield: 'addButtonColumn', columntype: 'button', width: '10%', cellsrenderer: function () {
            return "Eliminar";
        }, buttonclick: function (row) {
            // open the popup window when the user clicks a button.            
            editrow = row;
            filaSeleccionada = $("#TablaModelos").jqxGrid('getrowid', editrow);
            console.log(filaSeleccionada);
            swal({
                title: '¿Está seguro de eliminar el modelo seleccionado?',
                text: '¡Los valores del modelo se eliminaran y no se podrán recuperar!',
                type: 'info',
                allowOutsideClick: 'allow-outside-click',
                showConfirmButton: 'true',
                showCancelButton: 'true',
                confirmButtonClass: 'btn-success',
                cancelButtonClass: 'btn-default',

                closeOnConfirm: 'false',
                closeOnCancel: 'false',
                confirmButtonText: '¡Sí, eliminar!',
                cancelButtonText: '¡No, cancelar!',

            }, function (isConfirm) {
                if (isConfirm) {
                    $("#TablaModelos").jqxGrid('deleterow', filaSeleccionada);

                } else {
                    swal("Cancelado", "Los acción de eliminar ha sido cancelada.", "error")
                }
            });
        }
    }
];
inicializarTablaModelosDesc("#TablaModelos", _sourceTablaModelosDesc, _columnsTablaModelosDesc);


function inicializarTablaModelosDesc(_idTabla, _source, columns) {
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