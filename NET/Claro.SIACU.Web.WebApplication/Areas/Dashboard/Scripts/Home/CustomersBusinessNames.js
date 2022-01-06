
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , ddlTipoBusqueda: $('#ddlTipoBusqueda', $element)
            , btnClear: $('#btnLimpiar', $element)
            , btnSearchByReason: $('#btnBuscarPorRazon', $element)
            , divMessageClientList: $('#dvMensajeListaCliente', $element)
            , divMessageSelect: $('#dvMensajeSeleccion', $element)
            , divVistaParcialNombres: $('#dvistaParcialNombres', $element)
            , txtCriteriaValue: $("#txtCriteriaValue", $element)
            , txtSearchName: $('#NombreBusq', $element)
            , txtSearchLastName: $('#ApellidoBusq', $element)
            , tblDatosCliente: $('#tblDatosCliente', $element)
        })
    };

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnClear.addEvent(that, 'click', that.btnClear_click);
            controls.btnSearchByReason.addEvent(that, 'click', that.btnSearchByReason_click);
            controls.txtSearchName.on('keypress', that, that.enterValidationSearch);
            that.render();
        },
        render: function () {

            var that = this,
                controls = this.getControls();
              
            var strBus= window.opener.$("#txtCriteriaValue").val();
            controls.txtSearchName.val(strBus);

            if (strBus != "") {
                var stUrlLogo =  "/Images/loading2.gif";

                controls.divVistaParcialNombres.find('table tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
                that.customerList(strBus, "");
            }

            that.resizeContent();
        },
        applicativeRoute: window.location.href,

        btnClear_click: function () {

            var controls = this.getControls();

            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');
            controls.txtSearchName.val('');
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

            var stUrlLogo =  "/Images/loading2.gif";
            controls.divVistaParcialNombres.find('table tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            this.customerList(gNombre, gApellido);
        },

        resizeContent: function () {

            $(window).off('resize').on('resize', function () {
                $('div.dataTables_scrollBody').css('max-height', ($(window).outerHeight() - ($('div.modal-header').outerHeight() + $('div.modal-footer').outerHeight() + 230)) + 'px');
            });
        },

        createTableClientData: function (result) {
            this.getControls().tblDatosCliente.DataTable({
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "info": false,
                "select": "single",
                "destroy": true,
                "searching": false,
                "data": result.data.listDataCustomerModel,
                "columns": [
                    { "data": null },
                    { "data": "Names" },
                    { "data": "DocumentType" },
                    { "data": "DocumentIdentity" }
                ],
                "columnDefs": [
                    {
                        targets: 0,
                        orderable: false,
                        className: 'select-radio',
                        defaultContent: ""
                    }
                ],
                drawCallback: function (settings) {
                    $(this).DataTable().row(0).select();
                },
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
        },
        customerList: function (name, lastName) {

            var that = this,
                controls = that.getControls(),
                strUrlCustomer = '~/Dashboard/Home/CustomerList',
                oCustomer = {};

            oCustomer.strSearch = name == undefined ? '' : $.trim(name).toUpperCase();
            oCustomer.strLastName = lastName == undefined ? '' : $.trim(lastName).toUpperCase();
            oCustomer.StrIdSession = Session.IDSESSION;
            
            $.app.ajax({
                type: 'POST',
                url: strUrlCustomer,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                cache: true,
                data: JSON.stringify(oCustomer),
                success: function (result) {
                    that.createTableClientData(result);
                },
                error: function (ex) {
                    controls.divVistaParcialNombres.find('table tbody').html('');
                    $.app.error({ id: 'dvMensajeSeleccion', message: ex });
                }
            });

        },

        enterValidationSearch: function (event) {
            var strBus = this.value;
            var key = event.which || event.keyCode;
            switch (key) {
                default:
                    break;
                case 13:
                    event.data.customerList(strBus, "");
                    event.preventDefault();
                    return false;
                    break;
            }
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
            allowedMethods = ['getControls'];

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

    $('#contenedor-customerBusinessNames', $('.modal:last')).form();

})(jQuery);
