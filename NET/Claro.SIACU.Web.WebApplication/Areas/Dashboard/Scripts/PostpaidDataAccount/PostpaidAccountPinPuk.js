
(function ($) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountPinPuk.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblPinPuk: $('#tblPinPuk', $element)
            , dtPinPuk : null
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.desingtblPinPuk();
            that.render();
        },
        desingtblPinPuk: function () {
            var that = this,
            controls = that.getControls();
            controls.dtPinPuk = controls.tblPinPuk.DataTable({
                info: false
                , scrollY: "300px"
                , select: "single"
                , paging: false
                , searching: false
                , destroy: true
                , order: [[2,'asc']]
                ,scrollX: true
                ,sScrollXInner: "100%"
                ,autoWidth: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
            });
        },
        render: function () {
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

        GetExportPinPuk: function () {
            var strUrlModal = '~/Dashboard/PostpaidDataAccount/AccountExportPinPuk';
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.srtAccount = Session.DATACUSTOMER.Account;
            oContract.strBusinessName = Session.DATACUSTOMER.BusinessName;
            oContract.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=PinPuk.xlsx";
                }
            });
        },


        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'
    };




    $.fn.formAccountPinPuk = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportPinPuk'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountPinPuk'),
                options = $.extend({}, $.fn.formAccountPinPuk.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountPinPuk', data);
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

    $.fn.formAccountPinPuk.defaults = {
    }

    $('#ContentPostpaidAccountPinPuk', $('.modal:last')).formAccountPinPuk();

})(jQuery);
