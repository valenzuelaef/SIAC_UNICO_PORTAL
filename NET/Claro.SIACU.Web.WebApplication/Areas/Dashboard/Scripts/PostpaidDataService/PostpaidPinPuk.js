
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPinPuk.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblPost_Pin1: $('#lblPost_Pin1', $element)
            , lblPost_Pin2: $('#lblPost_Pin2', $element)
            , lblPost_Puk1: $('#lblPost_Puk1', $element)
            , lblPost_Puk2: $('#lblPost_Puk2', $element)
        });

    };


    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.setPinPuk();
        },
        setPinPuk: function () {
            var objService = Session.DATASERVICE;
            var controls = this.getControls();
            controls.lblPost_Pin1.text(objService.PIN1);
            controls.lblPost_Pin2.text(objService.PIN2);
            controls.lblPost_Puk1.text(objService.PUK1);
            controls.lblPost_Puk2.text(objService.PUK2);
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };


    $.fn.FormPinPuk = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPinPuk'),
                options = $.extend({}, $.fn.FormPinPuk.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPinPuk', data);
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

    $.fn.FormPinPuk.defaults = {
    }

    $('#FormPinPuk', $('.modal:last')).FormPinPuk();

})(jQuery);