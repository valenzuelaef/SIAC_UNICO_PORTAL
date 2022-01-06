(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormLongDistanceDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblDetailLongDistance: $('#tblDetailLongDistance', $element)
            , LoadingPrepaid: $('#LoadingPrepaid', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber')
            , hidTypeCall: $('#hidTypeCall', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.getLongDistance();
            that.render();
        },
        getDataLongDistance: function myfunction(response) {
            var that = this,
                controls = that.getControls();          
            if (Session.DATACUSTOMER.Application === "HFC" || Session.DATACUSTOMER.Application === "LTE") {
               controls.tblDetailLongDistance.DataTable({
                    "info": false,
                    "scrollY": "350px",
                    "scrollCollapse": true,
                    "paging": false,
                    "searching": false,
                    "select" : "single",
                    "data": response,
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
                        { "data": "CallNumber" },
                        { "data": "CallDate" },
                        { "data": "CallTime" },
                        { "data": "CallDuration" },
                        { "data": "CallTotal" },
                        { "data": "CallOrigin" }
                    ]
                });
            }
            else if (Session.DATACUSTOMER.Application === "POSTPAID" && controls.hidTypeCall.val() === "National") {

                controls.tblDetailLongDistance.DataTable({
                    "info": false,
                    "scrollY": "350px",
                    "scrollCollapse": true,
                    "paging": false,
                    "searching": false,
                    "select": "single",
                    "data": response,
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
                        { "data": "CallBefore" },
                        { "data": "CallAfter" },
                        { "data": "CallDestination" },
                        { "data": "CallNumber" },
                        { "data": "CallDate" },
                        { "data": "CallTime" },
                        { "data": "CallDuration" },
                        { "data": "CallTotal" }
                    ]
                });

            }
            else {

                controls.tblDetailLongDistance.DataTable({
                    "info": false,
                    "scrollY": "350px",
                    "scrollCollapse": true,
                    "paging": false,
                    "searching": false,
                    "select" : "single",
                    "data": response,
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
                        { "data": "CallAfter" },
                        { "data": "CallDestination" },
                        { "data": "CallNumber" },
                        { "data": "CallDate" },
                        { "data": "CallTime" },
                        { "data": "CallDuration" },
                        { "data": "CallTotal" }
                    ]
                });
            }
            controls.LoadingPrepaid.hide();
        },
        getLongDistance: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingDetailLongDistance',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.hidInvoiceNumber.val(), strTypeCall: controls.hidTypeCall.val() },
                success: function (result) {

                    that.getDataLongDistance(result.data.listCallDetail);

                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormLongDistanceDetail',
                        message: msger
                    });

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

    $.fn.FormLongDistanceDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormLongDistanceDetail'),
                options = $.extend({}, $.fn.FormLongDistanceDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormLongDistanceDetail', data);
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

    $.fn.FormLongDistanceDetail.defaults = {
    }

    $('#FormLongDistanceDetail', $('.modal:last')).FormLongDistanceDetail();

})(jQuery);