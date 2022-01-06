(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormDebtDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblDebtDetail: $('#tblDebtDetail', $element)
            , LoadingPrepaid: $('#LoadingPrepaid', $element)
            , DocumentNumber: $('#DocumentNumber', $element)
            , DebtQuantity: $('#DebtQuantity', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getDebtDetail();
        },
        getDataDebtDetail: function myfunction(response) {
            var controls = this.getControls();
            controls.tblDebtDetail.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": response,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }, "columns": [
                        { "data": "Number" },
                        { "data": "CustomerCode" },
                        { "data": "CustomerType" },
                        { "data": "CustomerName" },
                        { "data": "CustomerLastname" },
                        { "data": "CustomerRepresentative" },
                        { "data": "CustomerDebt" }
                ]
            });
            controls.LoadingPrepaid.hide();
        },
        getDebtDetail: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetBillingDebtDetail',
                data: { strIdSession: Session.IDSESSION, strDocumentNumber: Session.DATACUSTOMER.DocumentNumber },
                complete: function () {

                },
                success: function (result) {
                    controls.DocumentNumber.html(result.data.DocumentNumber);
                    controls.DebtQuantity.html(result.data.DebtQuantity);
                    that.getDataDebtDetail(result.data.listApadeceDebt);
                },
                error: function (msger) {

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

    $.fn.FormDebtDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormDebtDetail'),
                options = $.extend({}, $.fn.FormDebtDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormDebtDetail', data);
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

    $.fn.FormDebtDetail.defaults = {
    }

    $('#FormDebtDetail', $('.modal:last')).FormDebtDetail();

})(jQuery);
