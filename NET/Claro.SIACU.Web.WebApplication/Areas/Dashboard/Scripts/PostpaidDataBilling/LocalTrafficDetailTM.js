(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormLocalTrafficDetailTM.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblLocalTrafficDetailTM: $('#tblLocalTrafficDetailTM', $element)
            , LoadingPostpaidLocalTrafficDetailTM: $('#LoadingPostpaidLocalTrafficDetailTM', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdnTM: $('#hidMsisdnTM', $element)
            , hidTypeCallTM: $('#hidTypeCallTM', $element)
            , costTotMinuTM: $('#costTotMinuTM', $element)
            , hidImportAllTM: $('#hidImportAllTM', $element)
            , minuFactTM: $('#minuFactTM', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {

            var that = this;

            that.getLocalTrafficDetailTM();

        },

        getDataLocalTrafficDetailTM: function myfunction(LocalTrafficDetailTM) {
            var that = this,
                controls = that.getControls();
            controls.tblLocalTrafficDetailTM.find("tbody").html("");
            controls.tblLocalTrafficDetailTM.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": LocalTrafficDetailTM,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "CALLAMBITO" },
                    { "data": "CALLNUMBER" },
                    { "data": "CALLDESTINATION" },
                    { "data": "CALLDATE" },
                    { "data": "CALLTIME" },
                    { "data": "CALLTIME" },
                    { "data": "CALLDURATIONMIN" },
                    { "data": "CALLTOTAL" },
                ],
                "columnDefs": [
                    { "bVisible": false, "aTargets": [0] },
                ]

            });
            controls.LoadingPostpaidLocalTrafficDetailTM.hide();
        },
        getLocalTrafficDetailTM: function myfunction() {
            var that = this,
                controls = that.getControls();
            controls.LoadingPostpaidLocalTrafficDetailTM.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblLocalTrafficDetailTM.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetLocalTrafficDetailTM',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: window.opener.window.opener.$("#hidInvoiceNumber").val(), strMsisdn: window.opener.$("#hidMsisdnTM").val(),strTypeCallTMCol:window.opener.$("#hidTypeCallTM").val() },
                success: function (result) {
                    controls.costTotMinuTM.html(window.opener.$("#hidImportAllTM").val());
                    controls.minuFactTM.html(result.data.LocalTrafficQuantityCallTM);
                    that.getDataLocalTrafficDetailTM(result.data.listLocalTrafficDetailCallTM);
                },

                error: function (msger) {
                    $.app.error({ id: 'LoadingPostpaidLocalTrafficDetailTM', message: msger });
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

    $.fn.FormLocalTrafficDetailTM = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormLocalTrafficDetailTM'),
                options = $.extend({}, $.fn.FormLocalTrafficDetailTM.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormLocalTrafficDetailTM', data);
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

    $.fn.FormLocalTrafficDetailTM.defaults = {
    }

    $('#FormLocalTrafficDetailTM', $('.modal:last')).FormLocalTrafficDetailTM();

})(jQuery);