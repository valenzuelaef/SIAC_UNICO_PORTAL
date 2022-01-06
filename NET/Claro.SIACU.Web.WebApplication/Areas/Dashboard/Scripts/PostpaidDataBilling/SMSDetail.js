(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormSMSDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblSMSDetail: $('#tblSMSDetail', $element)
            , LoadingSMSDetail: $('#LoadingSMSDetail', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , hidMsisdn: $('#hidMsisdn', $element)
            , billedMessages: $('#billedMessages', $element)
            , errorSMSDetail: $('#errorSMSDetail', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getSMSDetails();
        },
        getDataSMSDetails: function myfunction(SMSDetail) {
            var that = this,
                controls = that.getControls();

            controls.tblSMSDetail.DataTable({
                info: false
                , paging: false
                , searching: false
                , select: 'single'
                , scrollY: 300
                , scrollCollapse: true
                , destroy: true
                ,scrollX: true
                ,sScrollXInner: "100%"
                ,autoWidth: true
                , data: SMSDetail
                , columns: [
                    { "data": "SMSNUMBER" },
                    { "data": "SMSDESTINATION" },
                    { "data": "SMSDATE" },
                    { "data": "SMSTIME" },
                    { "data": "SMSTOTAL" },
                ]
                , columnDefs: [
                    { type: "date", targets: 2 }
                ]
                , order: [[2, 'asc']]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        getSMSDetails: function myfunction() {
            var that = this,
                controls = that.getControls(),
                MSISDN = window.opener.window.opener.$('#hidInvoiceNumber').data('MSISDN');
            controls.errorSMSDetail.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblSMSDetail.find('tbody').html('<tr><td colspan="5" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetSMSDetail',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: window.opener.window.opener.$('#hidInvoiceNumber').val(), strMsisdn: MSISDN },
                success: function (result) {
                    controls.tblSMSDetail.find('tbody').html('');
                    controls.billedMessages.html(result.data.decCantidadSMS);
                    that.getDataSMSDetails(result.data.listCallDetailSMS);
                },

                error: function (msger) {
                    controls.tblSMSDetail.find('tbody').html('');
                    $.app.error({
                        id: 'errorSMSDetail',
                        message: msger,
                        click: function () {
                            that.getSMSDetails();
                        }
                    });
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

    $.fn.FormSMSDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormSMSDetail'),
                options = $.extend({}, $.fn.FormSMSDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormSMSDetail', data);
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

    $.fn.FormSMSDetail.defaults = {
    }

    $('#FormSMSDetail', $('.modal:last')).FormSMSDetail();

})(jQuery);