(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPrintTimServiceDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblPrintTimServiceDetail: $('#tblPrintTimServiceDetail', $element)
            , LoadingPrepaid: $('#LoadingPrepaid', $element)
            , hidMsisdn: $('#hidMsisdn', $element)
            , ColumnNro: $('#ColumnNro', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
              
            that.getTimServiceDetail();

            if (Session.DATACUSTOMER.Application != 'POSTPAID')          
            { that.changeCellPhoneWithCustomerId(); }

            that.render();
        },
        changeCellPhoneWithCustomerId:function(){
            var that = this,
            controls = that.getControls();
            controls.ColumnNro.text('Nro. Servicio');
        },

        tablePrintTimServiceDetail: null,

        getDataTimServiceDetail: function myfunction(response) {
            var that = this,
                controls = that.getControls();

            that.tablePrintTimServiceDetail = controls.tblPrintTimServiceDetail.DataTable({
                "info": false,
                "scrollY": "400px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": response,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "MSISDN" },
                    { "data": "RATEPLAN" },
                    { "data": "PERIODO" },
                    { "data": "AMOUNT" }
                ]
            });
            controls.LoadingPrepaid.hide();
        },
        SmsDetail_Click: function () {
            var that = this, controls = that.getControls();
           
            controls.tblPrintTimServiceDetail.find('tbody').find('a').addEvent(that, 'click', that.getSmsDetail);
        },
        setMSISDN: function (MSISDN) {
            this.MSISDN = MSISDN;
        },
        getMSISDN: function () {
            return this.MSISDN;
        },
        getSmsDetail: function (send, args) {
            var that = this;        
            window.opener.$('#hidInvoiceNumber').data('MSISDN', that.tablePrintTimServiceDetail.row($(send).parents('tr')).data().MSISDN);
            that.setMSISDN(that.tablePrintTimServiceDetail.row($(send).parents('tr')).data().MSISDN);
            args.event.preventDefault();
            $.window.open({
                modal: false,
                title: "LÍNEAS ASOCIADAS A LA CUENTA",
                url: '~/Dashboard/PostpaidDataBilling/BillingSMSDetails',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        getTimServiceDetail: function myfunction() {
            var that = this;
            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingPrintTimServiceDetail',
                data: { strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                complete: function () {
                    that.SmsDetail_Click();
                },
                success: function (result) {
                    
                    
                    that.getDataTimServiceDetail(result.data.listCallDetailTimService);
                },
                error: function (msger) {
                    $.app.error({
                        id: 'FormPrintTimServiceDetail',
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

    $.fn.FormPrintTimServiceDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getMSISDN'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPrintTimServiceDetail'),
                options = $.extend({}, $.fn.FormPrintTimServiceDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPrintTimServiceDetail', data);
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

    $.fn.FormPrintTimServiceDetail.defaults = {
    }

    $('#FormPrintTimServiceDetail', $('.modal:last')).FormPrintTimServiceDetail();

})(jQuery);