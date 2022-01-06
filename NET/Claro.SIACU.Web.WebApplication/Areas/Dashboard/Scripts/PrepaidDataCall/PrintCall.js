(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.PrepaidPrintCall.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , headers: $('#headers', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.ViewColumns();
           
        },        
        ViewColumns: function () {

            
            var that = this;
            var headers = $('#headers').val(), splitHeaders;
            splitHeaders = headers.split(',');
            $.each(splitHeaders, function (index, value) {               
                if ((/true/i).test(value) == false) {
                    that.hideTableTR(index)
                } else if ((/true/i).test(value) == true) {
                    that.showTableTR(index)
                } 
            });
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
        hideTableTR: function (num) {
            $('td:nth-child(' + num + '),th:nth-child(' + num + ')').hide();
        },
        showTableTR: function (num) {
            $('td:nth-child(' + num + '),th:nth-child(' + num + ')').show();
        },
        showAllTableTR: function () {
            for (var i = 1; i <= 34; i++) {
                $('td:nth-child(' + i + '),th:nth-child(' + i + ')').show();
                i++;
            }
        }
    };

    $.fn.PrepaidPrintCall = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidPrintCall'),
                options = $.extend({}, $.fn.PrepaidPrintCall.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidPrintCall', data);
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

    $.fn.PrepaidPrintCall.defaults = {
    }

    $('#ContenedorCall').PrepaidPrintCall();

})(jQuery);