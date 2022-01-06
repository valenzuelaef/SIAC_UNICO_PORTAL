
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountCreditLimit.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , tblCreditLimit: $('#tblCreditLimit', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            var td = controls.tblCreditLimit.find("tr").find('td');
            var strNroPac = td.eq(5).text();
            

            controls.tblCreditLimit.find("tr").find('td:eq(12)').each(function () {
                var $this = $(this);
                $(this).find('a').on("click", function () { that.SearchDetailCreditLimit($this) });

                if (strNroPac == "") {
                    $(this).find('a').off("click");
                    $(this).find('a').css("color", "gray");
                }

            });


            that.render();
        },
        SearchDetailCreditLimit: function (sender) {
            var td = sender.parent().find('td');
            var strNroPac = td.eq(5).text();
            $.window.open({
                modal: true,
                title: "Detalle de pagos a cuenta",
                url: '~/Dashboard/PostpaidDataAccount/AccountDetailCreditLimit',
                data: { strIdSession: Session.IDSESSION, strNroPac: strNroPac, strAccount: Session.DATACUSTOMER.Account, strDocumentNumber: Session.DATACUSTOMER.DocumentNumber, strDocumentType: Session.DATACUSTOMER.DocumentType },
                width: 1024,
                height: 600,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
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
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'

    };

    $.fn.formAccountCreditLimit = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountCreditLimit'),
                options = $.extend({}, $.fn.formAccountCreditLimit.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountCreditLimit', data);
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

    $.fn.formAccountCreditLimit.defaults = {
    }

    $('#ContentPostpaidAccountCreditLimit', $('.modal:last')).formAccountCreditLimit();

})(jQuery);
