(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountPinPukConstancy.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , tblPinPuk: $('#tblPinPukConstancy', $element)
        });
    }

    Form.prototype = {  
        constructor: Form,
        init: function () {
            var that = this;
            that.desingtblPinPuk();
            that.render();
        },
        ImprimirPinPuk: function () {
            window.print();          
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

    $.fn.formAccountPinPukConstancy = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountPinPukConstancy'),
                options = $.extend({}, $.fn.formAccountPinPukConstancy.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountPinPukConstancy', data);
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

    $.fn.formAccountPinPukConstancy.defaults = {
    }

    $('#ContentPostpaidAccountPinPukConstancy', $('.modal:last')).formAccountPinPukConstancy();

})(jQuery);
