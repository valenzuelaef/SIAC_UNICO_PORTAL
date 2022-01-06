
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.VisualizeCallPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element


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


        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        },


    };

    $.fn.VisualizeCallPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('VisualizeCallPrepaid'),
                options = $.extend({}, $.fn.VisualizeCallPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('VisualizeCallPrepaid', data);
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

    $.fn.VisualizeCallPrepaid.defaults = {
    }

})(jQuery);

$(document).ready(function () {

    $('#contenedorMessageInstant').VisualizeCallPrepaid();


});



















