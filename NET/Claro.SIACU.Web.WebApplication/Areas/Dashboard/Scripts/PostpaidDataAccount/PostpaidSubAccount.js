(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formSubAccount.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , btnSearch: $('#btnSearch', $element)
            , ddlTipoBusqueda: $('#ddlTipoBusqueda', $element)
            , txtCriteriaValue: $('#txtCriteriaValue', $element)
            , btnCriteriaSearch: $('#btnCriteriaSearch', $element)
        });
    }

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this;
            that.render();
        },        
        render: function () {
        },

        load: function (AccountId) {
            var that = this,
            controls = that.getControls();

            controls.btnSearch.html("Cuenta");
            controls.ddlTipoBusqueda.val("2");
            controls.txtCriteriaValue.val(AccountId);
            controls.btnCriteriaSearch.click();
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

    $.fn.formSubAccount = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['load'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formSubAccount'),
                options = $.extend({}, $.fn.formSubAccount.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formSubAccount', data);
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

    $.fn.formSubAccount.defaults = {
    }

    $('#ContentSubAccount', $('.modal:last')).formSubAccount();

})(jQuery);