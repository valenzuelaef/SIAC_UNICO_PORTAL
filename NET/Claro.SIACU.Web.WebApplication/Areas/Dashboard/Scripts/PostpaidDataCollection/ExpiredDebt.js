(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormExpiredDebt.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblExpiredDebt: $('#tblExpiredDebt', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            var that = this;
            that.gettblExpiredDebt();
        },
        gettblExpiredDebt: function () {
            var that = this;

            var is = Session.IDSESSION;
            var strplataforma = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            var customerid = Session.DATACUSTOMER.CustomerID

            if (strplataforma == "TOBE") {
                customerid = Session.DATACUSTOMER.csIdPub;
            }

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostpaidDataCollection/ListExpiredDebt',
                data: { strIdSession: is, strCustomerId: customerid },
                
                success: function (response) {
                    that.createTableExpiredDebt(response);
                },
                error: function () { },
            });


        },

        createTableExpiredDebt: function (response) {
            var that = this,
            controls = that.getControls();

            that.tblExpiredDebt = controls.tblExpiredDebt.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , select: 'single'
                , data: response
                , scrollY: 300
                , scrollCollapse: true
                , columns: [
                    { "data": "Document" },
                    { "data": "DateIssue" },
                    { "data": "DateDue" },
                    { "data": "ImportPending" },
                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    },
                ]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen registros",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
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
    $.fn.FormExpiredDebt = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormExpiredDebt'),
                options = $.extend({}, $.fn.FormExpiredDebt.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormExpiredDebt', data);
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

    $('#ExpiredDebt').FormExpiredDebt();

})(jQuery);

