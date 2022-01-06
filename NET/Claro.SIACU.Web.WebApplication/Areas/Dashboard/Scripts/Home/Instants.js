(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormInstants.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , DueNumberDocumentlg: $('#DueNumberDocumentlg', $element)

        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
          
            that.getLoad();
        },

        getLoad: function () {

            if ($('#hdnCantReg').val() != "") {
                $('#spidnotify').html('');
                $('#btnInstant').prop('title', 'Se tiene ' + $('#hdnCantReg').val() + ' Instantaneas');
                $('#spidnotify').html($('#hdnCantReg').val());
            } else {
                $('#spidnotify').html('');
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };
    $.fn.FormInstants = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormInstants'),
                options = $.extend({}, $.fn.FormInstants.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormInstants', data);
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



    $.fn.FormInstants.defaults = {
    }

    $('#FormInstants', $('.modal:last')).FormInstants();

})(jQuery);



