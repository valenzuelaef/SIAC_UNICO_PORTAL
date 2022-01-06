(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidDetailVisualizeCall.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblDetailVisualizeCall: $('#tblDetailVisualizeCall', $element)
           , rbRowInstant: $('input:radio[name=rbRowInstant]', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {

            var that = this,
                controls = this.getControls();
            controls.rbRowInstant.addEvent(that, 'click', that.rbRowInstant_click);
            that.render();
        },
        render: function () {
            this.getDataTable();
        },


        rbRowInstant_click: function () {
        },

        getDataTable: function () {
            var controls = this.getControls();

            controls.tblDetailVisualizeCall.DataTable({
                "scrollX": true,
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "info" : false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
          
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.PrepaidDetailVisualizeCall = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidDetailVisualizeCall'),
                options = $.extend({}, $.fn.PrepaidDetailVisualizeCall.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidDetailVisualizeCall', data);
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

    $.fn.PrepaidDetailVisualizeCall.defaults = {
    }

})(jQuery);

$(document).ready(function () {

    $('#contenedorDetailVisualizeCall').PrepaidDetailVisualizeCall();

});

