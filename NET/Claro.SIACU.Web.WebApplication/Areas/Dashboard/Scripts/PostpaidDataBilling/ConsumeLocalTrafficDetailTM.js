(function ($, undefined) {

    'use strict';
    
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormConsumeLocalTrafficDetailTM.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblLocalTrafficDetailTM: $('#tblConsumeLocalTrafficDetailTM', $element)
            , LoadingPostpaidConsumeLocalTrafficDetailTM: $('#LoadingPostpaidConsumeLocalTrafficDetailTM', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdnTMConsume: $('#hidMsisdnTMConsume', $element)
            , hidTypeCallTMConsume: $('#hidTypeCallTMConsume', $element)
            , costTotMinuTMConsume: $('#costTotMinuTMConsume', $element)
            , hidImportAllTMConsume: $('#hidImportAllTMConsume', $element)
            , minuFactTM: $('#minuFactTM', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getConsumeLocalTrafficDetailTM();
        },
        getDataConsumeLocalTrafficDetailTM: function myfunction(ConsumeLocalTrafficDetailTM) {
            var that = this,
                controls = that.getControls();

            controls.tblConsumeLocalTrafficDetailTM.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": ConsumeLocalTrafficDetailTM,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "CALLNUMBER" },
                    { "data": "CALLDESTINATION" },
                    { "data": "CALLDATE" },
                    { "data": "CALLTIME" },
                    { "data": "CALLTIMEFIN" },
                    { "data": "CALLDURATIONMIN" },
                    { "data": "CALLTOTAL" },
                ]

            });
            controls.LoadingPostpaidConsumeLocalTrafficDetailTM.hide();
        },
        getConsumeLocalTrafficDetailTM: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetConsumeLocalTrafficDetailTM',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.hidInvoiceNumber.val(), strMsisdn: controls.hidMsisdnTMConsume.val(), strTypeCallTMCol: controls.hidTypeCallTMConsume.val() },
                success: function (result) {

                    controls.costTotMinuTMConsume.html(controls.hidImportAllTMConsume.val());
                    controls.minuFactTM.html(result.data.ConsumeLocalTrafficQuantityCallTM);
                    that.getDataConsumeLocalTrafficDetailTM(result.data.listConsumeLocalTrafficDetailCallTM);
                },

                error: function (msger) {
                    $.app.error({ id: 'FormConsumeLocalTrafficDetailTM', message: msger });
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

    $.fn.FormConsumeLocalTrafficDetailTM = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormConsumeLocalTrafficDetailTM'),
                options = $.extend({}, $.fn.FormConsumeLocalTrafficDetailTM.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormConsumeLocalTrafficDetailTM', data);
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

    $.fn.FormConsumeLocalTrafficDetailTM.defaults = {
    }

    $('#FormConsumeLocalTrafficDetailTM', $('.modal:last')).FormConsumeLocalTrafficDetailTM();

})(jQuery);