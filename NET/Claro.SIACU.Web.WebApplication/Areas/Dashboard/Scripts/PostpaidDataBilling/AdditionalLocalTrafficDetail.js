(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormAdditionalLocalTrafficDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblTimProAdditionalLocalTrafficDetail: $('#tblTimProAdditionalLocalTrafficDetail', $element)
            , tblTimMaxAdditionalLocalTrafficDetail: $('#tblTimMaxAdditionalLocalTrafficDetail', $element)
            , LoadingPrepaid1: $('#LoadingPrepaid1', $element)
            , LoadingPrepaid2: $('#LoadingPrepaid2', $element)
            , hidMsisdnTP: $('#hidMsisdnTP', $element)
            , hidTypeCallTP: $('#hidTypeCallTP', $element)
            , hidImportAllTP: $('#hidImportAllTP', $element)
            , hidMsisdnTM: $('#hidMsisdnTM', $element)
            , hidTypeCallTM: $('#hidTypeCallTM', $element)
            , hidImportAllTM: $('#hidImportAllTM', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getAdditionalLocalTraffic();
        },
        getDataAdditionalLocalTraffic: function myfunction(responseTimPro, responseTimMax) {
            var that = this,
                controls = that.getControls();

            that.tableTimProAdditionalLocalTrafficDetail = controls.tblTimProAdditionalLocalTrafficDetail.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": responseTimPro,
                "destroy": true,
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
            controls.LoadingPrepaid1.hide();
            that.tableTimMaxAdditionalLocalTrafficDetail = controls.tblTimMaxAdditionalLocalTrafficDetail.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select":"single",
                "data": responseTimMax,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": "  ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                   { "data": "Msisdn" },
                   { "data": "OnNet" },
                   { "data": "OffNet" },
                   { "data": "OffOnNetOffNet" },
                   { "data": "Amount" }
                ], columnDefs: [{
                    "targets": [0, 1, 2, 3],
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });
            controls.LoadingPrepaid2.hide();
        },
        getAdditionalLocalTraffic: function myfunction() {
            var that = this,
                controls = that.getControls();
         
            controls.LoadingPrepaid1.html('');
            controls.LoadingPrepaid2.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblTimProAdditionalLocalTrafficDetail.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            controls.tblTimMaxAdditionalLocalTrafficDetail.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingAdditionalLocalTrafficDetail',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: window.opener.$("#hidInvoiceNumber").val() },
                complete: function () {
                    that.AdditionalLocalTraffic_click();
                },
                success: function (result) {

                    that.getDataAdditionalLocalTraffic(result.data.listTimProLocalTrafficDetail, result.data.listTimMaxLocalTrafficDetail);

                },

                error: function (msger) {
                    controls.tblTimProAdditionalLocalTrafficDetail.find('tbody').html('');
                    $.app.error({
                        id: 'LoadingPrepaid1', message: msger
                    });
                    controls.tblTimMaxAdditionalLocalTrafficDetail.find('tbody').html('');
                    $.app.error({
                        id: 'LoadingPrepaid2', message: msger
                    });
                }
            });

        },
        AdditionalLocalTraffic_click: function () {
            var that = this,
            controls = that.getControls();
            controls.tblTimProAdditionalLocalTrafficDetail.find('tbody').find('a').addEvent(that, 'click', that.getTimProAdditionalLocalTrafficDetail);
            controls.tblTimMaxAdditionalLocalTrafficDetail.find('tbody').find('a').addEvent(that, 'click', that.getTimMaxAdditionalLocalTrafficDetail);
        },
        getTimProAdditionalLocalTrafficDetail: function (send, args) {

            var that = this,
                 controls = that.getControls();
            var dataRowTP = that.tableTimProAdditionalLocalTrafficDetail.row($(send).parents('td')).data();
            var importAllTP = "";
            var typeCallTP = $(send).parents('td').index();

            if (typeCallTP != 0) {
                importAllTP = $(send).parents('td').text();
            } else {
                importAllTP = dataRowTP.Amount;
            }
            controls.hidMsisdnTP.val(dataRowTP.Msisdn);
            controls.hidTypeCallTP.val(typeCallTP);
            controls.hidImportAllTP.val(importAllTP);

            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIM Pro",
                url: '~/Dashboard/PostpaidDataBilling/BillingTimProAdditionalLocalTrafficDetail',
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
        getTimMaxAdditionalLocalTrafficDetail: function (send, args) {
            var that = this,
              controls = that.getControls();

            var dataRowTM = that.tableTimMaxAdditionalLocalTrafficDetail.row($(send).parents('td')).data();
            var typeCallTM = $(send).parents('td').index();
            var importAllTM = "";

            if (typeCallTM != 0) {
                importAllTM = $(send).parents('td').text();
            } else {
                importAllTM = dataRowTM.Amount;
            }

            controls.hidMsisdnTM.val(dataRowTM.Msisdn);
            controls.hidTypeCallTM.val(typeCallTM);
            controls.hidImportAllTM.val(importAllTM);

            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIM Max",
                url: '~/Dashboard/PostpaidDataBilling/BillingTimMaxAdditionalLocalTrafficDetail',
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
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.FormAdditionalLocalTrafficDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormAdditionalLocalTrafficDetail'),
                options = $.extend({}, $.fn.FormAdditionalLocalTrafficDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormAdditionalLocalTrafficDetail', data);
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

    $.fn.FormAdditionalLocalTrafficDetail.defaults = {
    }

    $('#FormAdditionalLocalTrafficDetail', $('.modal:last')).FormAdditionalLocalTrafficDetail();

})(jQuery);
