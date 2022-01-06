(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountAnnotation.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , cboTypeAnnotation: $('#cboTypeAnnotation', $element)
            , divAccountAnnotationList: $('#divAccountAnnotationList', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") controls.cboTypeAnnotation.val("OPEN");
            else controls.cboTypeAnnotation.val("Abierto");

            controls.cboTypeAnnotation.addEvent(that, 'change', that.cboTypeAnnotation_change);
            that.render();

        },
        cboTypeAnnotation_change: function () {
            var controls = this.getControls();
            var strCustomerId = Session.DATACUSTOMER.CustomerID;
            var strStatus = controls.cboTypeAnnotation.val();

            $.app.ajax({
                type: 'POST',
                dataType: 'html',
                url: '~/Dashboard/PostpaidDataAccount/AccountAnnotationList',
                cache: false,
                container: controls.divAccountAnnotationList,
                data: { strIdSession: Session.IDSESSION, strCustomerId: strCustomerId, strStatus: strStatus, strContract: Session.DATACUSTOMER.ContractID, strType: $('#hdiTypeAnnotation').val(), platform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, flagconvivencia: Session.DATACUSTOMER.flagConvivencia },
                error: function (ex) {
                    showAlert("Ocurrio un Error ,vuelva a intentarlo mas tarde");
                }
            });

        },
        render: function () {
            var that = this;
            that.cboTypeAnnotation_change();
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

    $.fn.formAccountAnnotation = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountAnnotation'),
                options = $.extend({}, $.fn.formAccountAnnotation.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountAnnotation', data);
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

    $.fn.formAccountAnnotation.defaults = {
    }

    $('#ContentAccountAnnotation', $('.modal:last')).formAccountAnnotation();

})(jQuery);
