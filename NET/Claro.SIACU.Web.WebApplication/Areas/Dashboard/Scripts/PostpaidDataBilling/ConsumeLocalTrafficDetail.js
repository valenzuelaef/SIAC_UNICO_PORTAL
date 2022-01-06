(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormConsumeLocalTrafficDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblTimProConsumeLocalTrafficDetail: $('#tblTimProConsumeLocalTrafficDetail', $element)
            , tblTimMaxConsumeLocalTrafficDetail: $('#tblTimMaxConsumeLocalTrafficDetail', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdnTPConsume: $('#hidMsisdnTPConsume', $element)
            , hidTypeCallTPConsume: $('#hidTypeCallTPConsume', $element)
            , hidImportAllTPConsume: $('#hidImportAllTPConsume', $element)
            , hidMsisdnTMConsume: $('#hidMsisdnTMConsume', $element)
            , hidTypeCallTMConsume: $('#hidTypeCallTMConsume', $element)
            , hidImportAllTMConsume: $('#hidImportAllTMConsume', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.getConsumeLocalTraffic();

            that.render();
        },
        getDataConsumeLocalTraffic: function myfunction(responseTimPro, responseTimMax) {
            var that = this,
                controls = that.getControls();

            that.tableTimProConsumeLocalTrafficDetail = controls.tblTimProConsumeLocalTrafficDetail.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": responseTimPro,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "Msisdn" },
                    { "data": "Rtp" },
                    { "data": "OnNet" },
                    { "data": "OffNetToTelephone" },
                    { "data": "OffNetToCellphone" },
                    { "data": "Amount" }
                ], columnDefs: [{
                    "targets": [0, 1, 2, 3, 4],
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });

            that.tableTimMaxConsumeLocalTrafficDetail = controls.tblTimMaxConsumeLocalTrafficDetail.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": responseTimMax,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                   { "data": "Msisdn" },
                   { "data": "OnNet" },
                   { "data": "OffNetToTelephone" },
                   { "data": "OffNetToCellphone" },
                   { "data": "Amount" }
                ], columnDefs: [{
                    "targets": [0, 1, 2, 3],
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });

        },
        getConsumeLocalTraffic: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingConsumeLocalTrafficDetail',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.hidInvoiceNumber.val() },
                complete: function () {
                    that.ConsumeLocalTraffic_click();
                },
                success: function (result) {

                    that.getDataConsumeLocalTraffic(result.data.listTimProLocalTrafficDetail, result.data.listTimMaxLocalTrafficDetail);

                },

                error: function (msger) {
                    $.app.error({ id: 'FormConsumeLocalTrafficDetail', message: msger });
                }
            });

        },
        ConsumeLocalTraffic_click: function () {
            var that = this,
            controls = that.getControls();
            controls.tblTimProConsumeLocalTrafficDetail.find('tbody').find('a').addEvent(that, 'click', that.getTimProConsumeLocalTrafficDetail);
            controls.tblTimMaxConsumeLocalTrafficDetail.find('tbody').find('a').addEvent(that, 'click', that.getTimMaxConsumeLocalTrafficDetail);
        },
        getTimProConsumeLocalTrafficDetail: function (send, args) {

            var that = this,
               controls = that.getControls();
            var dataRowTPConsume = that.tableTimProConsumeLocalTrafficDetail.row($(send).parents('td')).data();
            var typeCallConsume = $(send).parents('td').index();
            var importAllTPConsume;

            if (typeCallConsume !== 0) {
                importAllTPConsume = $(send).parents('td').text();
            } else {
                importAllTPConsume = dataRowTPConsume.Amount;
            }
            controls.hidMsisdnTPConsume.val(dataRowTPConsume.Msisdn);
            contro.hidTypeCallTPConsume.val(typeCallConsume);
            controls.hidImportAllTPConsume.val(importAllTPConsume);

            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIM Pro Consumo",
                url: '~/Dashboard/PostpaidDataBilling/BillingTimProConsumeLocalTrafficDetail',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });


        },
        getTimMaxConsumeLocalTrafficDetail: function (send, args) {
            var that = this,
              controls = that.getControls();

            var dataRowTMConsume = that.tableTimMaxConsumeLocalTrafficDetail.row($(send).parents('td')).data();
            var typeCallTMConsume = $(send).parents('td').index();

            var importAllTMConsume;

            if (typeCallTMConsume !== 0) {
                importAllTMConsume = $(send).parents('td').text();
            } else {
                importAllTMConsume = dataRowTMConsume.Amount;
            }
            controls.hidMsisdnTMConsume.val(dataRowTMConsume.Msisdn);
            controls.hidTypeCallTMConsume.val(typeCallTMConsume);
            controls.hidImportAllTMConsume.val(importAllTMConsume);

            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIM Max Consumo",
                url: '~/Dashboard/PostpaidDataBilling/BillingTimMaxConsumeLocalTrafficDetail',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.FormConsumeLocalTrafficDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormConsumeLocalTrafficDetail'),
                options = $.extend({}, $.fn.FormConsumeLocalTrafficDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormConsumeLocalTrafficDetail', data);
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

    $.fn.FormConsumeLocalTrafficDetail.defaults = {
    }

    $('#FormConsumeLocalTrafficDetail', $('.modal:last')).FormConsumeLocalTrafficDetail();

})(jQuery);