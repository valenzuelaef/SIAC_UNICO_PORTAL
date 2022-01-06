
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            ddlTipoBusqueda: $('#ddlTipoBusqueda')
            , btnSelect: $('#btnSeleccionar')
            , btnClear: $('#btnLimpiar')
            , btnSearchByName: $('#btnBuscarPorNombre')
            , btnSearchByReason: $('#btnBuscarPorRazon')
            , divMessageClientList: $('#dvMensajeListaCliente')
            , divMessageSelect: $('#dvMensajeSeleccion')
            , divVistaParcialNombres: $('#dvistaParcialNombres')
            , txtCriteriaValue: $("#txtCriteriaValue")
            , txtSearchName: $('#NombreBusq')

            , txtSearchLastName: $('#ApellidoBusq')

        })
    };

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSelect.addEvent(that, 'click', that.btnSelect_click);
            controls.btnClear.addEvent(that, 'click', that.btnClear_click);
            controls.btnSearchByName.addEvent(that, 'click', that.btnSearchByName_click);
            controls.btnSearchByReason.addEvent(that, 'click', that.btnSearchByReason_click);

            that.render();
        },
        render: function () {

            var that = this;
            var strBus = that.getControls().txtSearchName.val();

            that.customerList(strBus, "");

        },
        applicativeRoute: window.location.href,
        btnSelect_click: function () {



            var fila = $(":input[name=optionsRadios]:checked").val();

            if (fila == "" || fila == undefined || fila == null) {
                showAlert('Debe seleccionar un cliente.', this.strTitleMessage);
                return;
            }

            var res = fila.split("|");

            var DocumentType = res[0].trim();
            var DocumentNumber = res[1].trim();

            //INI ccv
            var data = { strSearchType: '1', strSearchValue: DocumentType + '|' + DocumentNumber, strIdSession: Session.IDSESSION };
            var strUrl = '~/Dashboard/Home/CustomerValidate';
            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: strUrl,
                data: data,
                success: function (result) {
                    if (result.data != null) {
                        Session.CUSTOMERPRODUCT = result.data;
                        Session.CUSTOMERPRODUCT.isRedirecBussines = true;
                        Session.CUSTOMERPRODUCT.DocumentType = DocumentType;
                        Session.CUSTOMERPRODUCT.DocumentNumber = DocumentNumber;

                        $.window.open({
                            modal: true,
                            title: "CONSULTA PRODUCTOS",
                            url: '~/Dashboard/Home/Products',
                            data: {},
                            width: 900,
                            height: 600,
                            buttons: {
                                Cancelar: {
                                    click: function (sender, args) {
                                        this.close();
                                    }
                                }
                            }
                        });
                    }
                    else {
                        showAlert('Debe seleccionar un cliente.', this.strTitleMessage);
                        return;
                    }
                },
                error: function (msger) {

                }
            });
            //FIN ccv
        },
        btnClear_click: function () {

            var controls = this.getControls();

            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');

            this.customerList();

        },
        btnSearchByReason_click: function () {

            var controls = this.getControls();
            var gtipo = controls.ddlTipoBusqueda.val();

            controls.txtCriteriaValue.val('');
            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');

            var gNombre = controls.txtSearchName.val(), gApellido = "";

            controls.ddlTipoBusqueda.val("7");

            if (gNombre == "" && gtipo == "7") {
                showAlert('Debe ingresar el campo Razón Social.', this.strTitleMessage);
                return;
            }
            //Fin Validando campos necesarios

            var stUrlLogo = "/Images/loading2.gif";

            controls.divVistaParcialNombres.find('table tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            this.customerList(gNombre, gApellido);
        },
        btnSearchByName_click: function () {

            var controls = this.getControls();
            var gtipo = "6";

            controls.txtCriteriaValue.val('');
            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');

            var gNombre = controls.txtSearchName.val(), gApellido = "";

            gApellido = controls.txtSearchLastName.val();
            controls.ddlTipoBusqueda.val(gtipo);

            //Inicio Validando campos necesarios
            if ((gNombre == "" || gApellido == "") && gtipo == "6") {
                showAlert('Debe ingresar los campos Nombres y Apellidos.', this.strTitleMessage);
                return;
            }

            //Fin Validando campos necesarios

            var stUrlLogo = "/Images/loading2.gif";

            controls.divVistaParcialNombres.find('table tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            this.customerList(gNombre, gApellido);

        },
        customerList: function (name, lastName) {

            var controls = this.getControls(),
                strUrlCustomer = '~/Dashboard/Home/CustomerList',
                oCustomer = {};

            oCustomer.strSearch = name == undefined ? '' : name;
            oCustomer.strLastName = lastName == undefined ? '' : lastName;
            oCustomer.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                url: strUrlCustomer,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                cache: true,
                data: JSON.stringify(oCustomer),
                success: function (result) {

                    if (result.data != null) {
                        $('#tblDatosCliente').DataTable({
                            "scrollY": "180px",
                            "scrollCollapse": true,
                            "info": false,
                            "select": "single",
                            "paging": false,
                            "destroy": true,
                            "searching": false,
                            "data": result.data.listDataCustomerModel,
                            "language": {
                                "lengthMenu": "Display _MENU_ records per page",
                                "zeroRecords": "No existen datos",
                                "info": " ",
                                "infoEmpty": " ",
                                "infoFiltered": "(filtered from _MAX_ total records)"
                            },
                            "columns": [
                                { "data": null },
                                { "data": "Names" },
                                { "data": "DocumentType" },
                                { "data": "DocumentIdentity" }
                            ]
                            , "columnDefs": [{
                                targets: 0,
                                render: function (data, type, full, meta) {
                                    var $rb = $('<input>', {
                                        type: 'radio',
                                        value: full.DocumentType + '|' + full.DocumentIdentity + '|' + full.Names
                                    , name: 'optionsRadios',
                                        id: "optionsRadios1"
                                    });
                                    return $rb[0].outerHTML;
                                }
                            }]
                            , drawCallback: function (settings) {

                                $('input[name=optionsRadios]:first').attr('checked', true);
                            }
                        });
                    }

                },
                error: function (ex) {
                    showAlert('Error al cargar los datos.', this.strTitleMessage);
                    controls.divVistaParcialNombres.find('table tbody').html('');
                }
            });

        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta de Clientes",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('form'),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('form', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.form.defaults = {
    }

})(jQuery);

$(document).ready(function () {

    $('#contenedor-customer').form();

});