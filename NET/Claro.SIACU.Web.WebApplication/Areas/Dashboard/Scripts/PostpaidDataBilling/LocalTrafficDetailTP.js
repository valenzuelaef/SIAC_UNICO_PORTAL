(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormLocalTrafficDetailTP.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblLocalTrafficDetailTP: $('#tblLocalTrafficDetailTP', $element)
            , LoadingPostpaidLocalTrafficDetailTP: $('#LoadingPostpaidLocalTrafficDetailTP', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdnTP: $('#hidMsisdnTP', $element)
            , hidTypeCallTP: $('#hidTypeCallTP', $element)
            , minuFactTP: $('#minuFactTP', $element)
            , costTotMinuTP: $('#costTotMinuTP', $element)
            , hidImportAllTP: $('#hidImportAllTP', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {

            var that = this;

            that.getLocalTrafficDetailTP();


        },

        getDataLocalTrafficDetailTP: function myfunction(LocalTrafficDetailTP) {
            var that = this,
                controls = that.getControls();
            controls.tblLocalTrafficDetailTP.find("tbody").html("");
            controls.tblLocalTrafficDetailTP.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": LocalTrafficDetailTP,
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
            controls.LoadingPostpaidLocalTrafficDetailTP.hide();
        },
        getLocalTrafficDetailTP: function myfunction() {
            var that = this,
                controls = that.getControls();
            controls.LoadingPostpaidLocalTrafficDetailTP.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblLocalTrafficDetailTP.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetLocalTrafficDetailTP',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.hidInvoiceNumber.val(), strMsisdn: controls.hidMsisdnTP.val(), strTypeCallTPCol: controls.hidTypeCallTP.val() },
                success: function (result) {

                    controls.minuFactTP.html(result.data.LocalTrafficQuantityCallTP);
                    controls.costTotMinuTP.html(controls.hidImportAllTP.val());
                    that.getDataLocalTrafficDetailTP(result.data.listLocalTrafficDetailCallTP);

                },

                error: function (msger) {
                    $.app.error({ id: 'LoadingPostpaidLocalTrafficDetailTP', message: msger });
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

    $.fn.FormLocalTrafficDetailTP = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormLocalTrafficDetailTP'),
                options = $.extend({}, $.fn.FormLocalTrafficDetailTP.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormLocalTrafficDetailTP', data);
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

    $.fn.FormLocalTrafficDetailTP.defaults = {
    }

    $('#FormLocalTrafficDetailTP', $('.modal:last')).FormLocalTrafficDetailTP();

})(jQuery);
