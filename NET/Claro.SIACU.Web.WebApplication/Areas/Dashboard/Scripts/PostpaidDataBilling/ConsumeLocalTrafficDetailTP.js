(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormConsumeLocalTrafficDetailTP.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblLocalTrafficDetailTP: $('#tblLocalTrafficDetailTP', $element)
            , tblConsumeLocalTrafficDetailTP: $('#tblConsumeLocalTrafficDetailTP', $element)
            , LoadingPostpaidConsumeLocalTrafficDetailTP: $('#LoadingPostpaidConsumeLocalTrafficDetailTP', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdnTPConsume: $('#hidMsisdnTPConsume', $element)
            , hidTypeCallTPConsume: $('#hidTypeCallTPConsume', $element)
            , costTotMinuTPConsume: $('#costTotMinuTPConsume', $element)
            , hidImportAllTPConsume: $('#hidImportAllTPConsume', $element)
            , minuFactTP: $('#minuFactTP', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getConsumeLocalTrafficDetailTP();
        },
        getDataConsumeLocalTrafficDetailTP: function myfunction(ConsumeLocalTrafficDetailTP) {
            var that = this,
                controls = that.getControls();

            controls.tblConsumeLocalTrafficDetailTP.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": ConsumeLocalTrafficDetailTP,
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
            controls.LoadingPostpaidConsumeLocalTrafficDetailTP.hide();
        },
        getConsumeLocalTrafficDetailTP: function myfunction() {
            var that = this,
                controls = that.getControls();


            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetConsumeLocalTrafficDetailTP',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.hidInvoiceNumber.val(), strMsisdn: controls.hidMsisdnTPConsume.val(), strTypeCallTPCol: controls.hidTypeCallTPConsume.val() },
                success: function (result) {

                    controls.costTotMinuTPConsume.html(controls.hidImportAllTPConsume.val());
                    controls.minuFactTP.html(result.data.ConsumeLocalTrafficQuantityCallTP);

                    that.getDataConsumeLocalTrafficDetailTP(result.data.listConsumeLocalTrafficDetailCallTP);

                },

                error: function (msger) {
                    $.app.error({ id: 'FormConsumeLocalTrafficDetailTP', message: msger });
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

    $.fn.FormConsumeLocalTrafficDetailTP = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormConsumeLocalTrafficDetailTP'),
                options = $.extend({}, $.fn.FormConsumeLocalTrafficDetailTP.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormConsumeLocalTrafficDetailTP', data);
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

    $.fn.FormConsumeLocalTrafficDetailTP.defaults = {
    }

    $('#FormConsumeLocalTrafficDetailTP', $('.modal:last')).FormConsumeLocalTrafficDetailTP();

})(jQuery);

