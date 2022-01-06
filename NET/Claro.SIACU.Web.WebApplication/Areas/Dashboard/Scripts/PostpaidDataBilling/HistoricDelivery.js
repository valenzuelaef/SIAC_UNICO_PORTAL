    
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormHistoricDelivery.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblHitoricDelivery: $('#tblHitoricDelivery', $element)
            , tbDatosCliente2: $('#tbDatosCliente2',$element)
            , LoadingPrepaid: $('#LoadingPrepaid', $element)
            , lblPost_EquipmentsCustomerId: $('#lblPost_EquipmentsCustomerId', $element)
            , lblPost_EquipmentsCuenta: $('#lblPost_EquipmentsCuenta', $element)
            , lblPost_EquipmentsCliente: $('#lblPost_EquipmentsCliente', $element)
            , lblPost_EquipmentsContacto: $('#lblPost_EquipmentsContacto', $element)
        })
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },

        render: function () {
            var that = this;
            that.HistoricDelivery();
            that.setCustomerInformation();
        },

        setCustomerInformation: function () {
            var that = this,
                controls = that.getControls();

            if (Session.ORIGINTYPE === 'POSTPAID') {
                controls.tbDatosCliente2.hide();
            } else {
                controls.tbDatosCliente2.show();
            }
        },

        applicativeRoute: window.location.href,
        HistoricDelivery: function () {
            var that = this;
            var customerid;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === 'TOBE') {
                customerid = Session.DATACUSTOMER.csIdPub
            }
            else {
                customerid = Session.DATACUSTOMER.CustomerID
            }

            var data = { strIdSession: Session.IDSESSION, strCustomerID: customerid, flagPlataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT };

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingHistoricDelivery',
                data: data,
                success: function (result) {
                    that.setHistoricDelivery(result.data.listHisDelivery);
                    that.getHistoryInvoice(result.data.listHisDelivery);
                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormHistoricDelivery',
                        message: msger
                    });
                }
            });

        },
        setHistoricDelivery: function myfunction(data) {
            this.objHistoricDelivery = data;
        },
        getHistoricDelivery: function () {
            return this.objHistoricDelivery == null ? [] : this.objHistoricDelivery;
        },
        getHistoryInvoice: function myfuncion(listHisDelivery) {
            var that = this,
             controls = this.getControls();

            controls.tblHitoricDelivery.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "data": listHisDelivery,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "RECIBO" },
                    { "data": "FECEMISION" },
                    { "data": "TIPO" },
                    { "data": null },
                    { "data": "ESTADO" },
                    { "data": "MOTIVO" },
                    { "data": "FECENTREGA" }
                ],
                columnDefs: [{
                    "targets": 3,
                    "render": function (full, meta) {
                        var elementHtml;
                        if (full.Url != null && full.Url.trim() !== '') {
                            elementHtml = "<a style='cursor:pointer' href='" + full.Url + "'>" + full.COURIER + "</a>";
                        } else {
                            elementHtml= full.COURIER;
                        }
                        return elementHtml;
                    }
                }
                ]
            });
            controls.LoadingPrepaid.hide();

            that.setCustomer();
        },
        setCustomer: function () {
            var controls = this.getControls();
            var oCustomer = Session.DATACUSTOMER;
            controls.lblPost_EquipmentsCustomerId.text(oCustomer.CustomerID);
            controls.lblPost_EquipmentsCuenta.text(oCustomer.Account);
            controls.lblPost_EquipmentsCliente.text(oCustomer.BusinessName);
            controls.lblPost_EquipmentsContacto.text(oCustomer.FullName);

        },



        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta de Historial de Entrega ",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.FormHistoricDelivery = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getHistoricDelivery'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormHistoricDelivery'),
                options = $.extend({}, $.fn.FormHistoricDelivery.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormHistoricDelivery', data);
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

    $.fn.FormHistoricDelivery.defaults = {
    }

    $('#FormHistoricDelivery', $('.modal:last')).FormHistoricDelivery();

})(jQuery);