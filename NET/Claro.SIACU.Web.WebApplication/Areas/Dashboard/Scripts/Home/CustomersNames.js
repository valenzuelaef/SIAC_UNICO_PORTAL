
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , ddlTipoBusqueda: $('#ddlTipoBusqueda', $element)
            , btnClear: $('#btnLimpiar', $element)
            , btnSearchByName: $('#btnBuscarPorNombre', $element)
            , divMessageClientList: $('#dvMensajeListaCliente', $element)
            , divMessageSelect: $('#dvMensajeSeleccion', $element)
            , divVistaParcialNombres: $('#dvistaParcialNombres', $element)
            , txtCriteriaValue: $("#txtCriteriaValue", $element)
            , txtSearchName: $('#NombreBusq', $element)
            , txtSearchLastName: $('#ApellidoBusq', $element)
            , hdnNameLength: $('#hdnNameLength', $element)
            , hdnLastNameLength: $('#hdnLastNameLength', $element)
            , tblDatosCliente: $('#tblDatosCliente', $element)
        })
    };

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnClear.addEvent(that, 'click', that.btnClear_click);
            controls.btnSearchByName.addEvent(that, 'click', that.btnSearchByName_click);

            that.render();
        },
        render: function () {

            var that = this;
            var controls = this.getControls();

            $.onlyLettersSpaces(controls.txtSearchName);
            $.onlyLettersSpaces(controls.txtSearchLastName);
            controls.txtSearchName.focus();
            controls.txtSearchName.keyup(function (e) {

                    if (e.keyCode == 13)

                        that.btnSearchByName_click();

            });

            controls.txtSearchLastName.keyup(function (e) {

                if (e.keyCode == 13)

                    that.btnSearchByName_click();

            });
        },
        applicativeRoute: window.location.href,

        btnClear_click: function () {

            var controls = this.getControls();

            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');
            controls.txtSearchName.val('');
            controls.txtSearchLastName.val('');

            this.customerList();

        },

        btnSearchByName_click: function () {

            var controls = this.getControls();
            var gtipo = "6";
            var that = this;
            controls.txtCriteriaValue.val('');
            controls.divMessageClientList.html('');
            controls.divMessageSelect.html('');

            var gNombre = controls.txtSearchName.val(),
                gApellido = "";

            gApellido = controls.txtSearchLastName.val();
            controls.ddlTipoBusqueda.val(gtipo);

            //Inicio Validando campos necesarios
            if ((gNombre == "" && gApellido == "") && gtipo == "6") {

                showAlert('Debe ingresar los campos Nombres y Apellidos.', this.strTitleMessage);
                return;
            } else if (gtipo == "6" && !that.ValidateLength()) {

                showAlert('Longitud mínima para la busqueda. ' +
                                                  'Nombres : ' + controls.hdnNameLength.val() + ' caracteres. ' +
                                                  'Apellidos : ' + controls.hdnLastNameLength.val() + ' caracteres.', this.strTitleMessage);
                return;
            }

            //Fin Validando campos necesarios

            var stUrlLogo =  "/Images/loading2.gif";
            controls.divVistaParcialNombres.find('table tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            this.customerList(gNombre, gApellido);

        },
        createTableClientData: function (result) {
            this.getControls().tblDatosCliente.DataTable({
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "searching": false,
                "select": "single",
                "info": false,
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
                "drawCallback": function (settings) {
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
                    that.createTableClientData(result);
                },
                error: function (ex) {
                    controls.divVistaParcialNombres.find('table tbody').html('');
                    $.app.error({ id: 'dvMensajeSeleccion', message: ex });
                }
            });
        },
        ValidateLength: function () {
            var controls = this.getControls();
            var hdnNameLength = controls.hdnNameLength.val();
            var hdnLastNameLength = controls.hdnLastNameLength.val();
            var txtSearchNameLength = controls.txtSearchName.val().length;
            var txtSearchLastNameLength = controls.txtSearchLastName.val().length;
            if (hdnNameLength <= txtSearchNameLength && hdnLastNameLength <= txtSearchLastNameLength) {
                return true;
            }
            return false;
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

    $.fn.form.defaults = {}

    $('#contenedor-customerNames', $('.modal:last')).form();
})(jQuery);