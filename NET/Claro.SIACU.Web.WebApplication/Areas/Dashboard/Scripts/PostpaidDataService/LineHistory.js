(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPospaidLineHistory.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbLineHistoryPost: $('#tbLineHistoryPost', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var controls = this.getControls();

            controls.tbLineHistoryPost.DataTable({
                "info": false,
                "scrollY": "200px",
                "scrollCollapse": true,
                "select": 'single',
                "paging": false,
                "destroy": true,
                "searching": false,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "bSort" : false,
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
        },

    };


    $.fn.FormPospaidLineHistory = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPospaidLineHistory'),
                options = $.extend({}, $.fn.FormPospaidLineHistory.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPospaidLineHistory', data);
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



    $.fn.FormPospaidLineHistory.defaults = {
    }

    $('#PospaidLineHistory', $('.modal:last')).FormPospaidLineHistory()

})(jQuery);