(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormConsulta4G.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblPost_Consulta4G: $('#lblPost_Consulta4G', $element)
        });

    };


    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.setConsulta4G();
        },
        setConsulta4G: function () {
            var objService = Session.DATASERVICE;
            var controls = this.getControls();
           
            controls.lblPost_Consulta4G.text(objService.Consulta4G);
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };


    $.fn.FormConsulta4G = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormConsulta4G'),
                options = $.extend({}, $.fn.FormConsulta4G.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormConsulta4G', data);
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

    $.fn.FormConsulta4G.defaults = {
    }

    $('#FormConsulta4G', $('.modal:last')).FormConsulta4G();

})(jQuery);