(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormFixedChargeBagDetailOne.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblFixedChargeDetailsTIMPro: $('#tblFixedChargeDetailsTIMPro', $element)
            , tblFixedChargeTimProBagDetailOne: $('#tblFixedChargeTimProBagDetailOne', $element)
            , LoadingPostpaidTimProBagDetailOne: $('#LoadingPostpaidTimProBagDetailOne', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.getFixedChargeTimProBagDetailOne();
           
            that.render();
        },
       
        getDataFixedChargeTimProBagDetailOne: function myfunction(TimProBagDetailOne) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeTimProBagDetailOne.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": TimProBagDetailOne,
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
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                ]
                , columnDefs: [{
                    "targets": 1,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTimProBagDetailOne.hide();
        },
        getFixedChargeTimProBagDetailOne: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeTimProBagDetailOne',
                data: { intGroupBox: '2', strInvoiceNumber: controls.hidInvoiceNumber.val(), strIdSession: Session.IDSESSION },
                success: function (result) {

                  
                    that.getDataFixedChargeTimProBagDetailOne(result.data.listFixedChargeTimProBagDetailOne);

                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormFixedChargeBagDetailOne',
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



    $.fn.FormFixedChargeBagDetailOne = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormFixedChargeDetails'),
                options = $.extend({}, $.fn.FormFixedChargeBagDetailOne.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormFixedChargeBagDetailOne', data);
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

    $.fn.FormFixedChargeBagDetailOne.defaults = {
    }

    $('#FormFixedChargeBagDetailOne', $('.modal:last')).FormFixedChargeBagDetailOne();

})(jQuery);