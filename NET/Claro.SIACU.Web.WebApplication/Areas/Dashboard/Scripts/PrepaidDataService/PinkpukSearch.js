(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidPinPukSearch.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblPinPukSearch: $('#tblPinPukSearch', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },
        render: function () {
            this.getDataTable();
        },

        getDataTable: function () {
            var controls = this.getControls();

            controls.tblPinPukSearch.DataTable({
                "scrollY": "200px",
                "info": false,
                "select": "single",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": "",
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

    $.fn.PrepaidPinPukSearch = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidPinPukSearch'),
                options = $.extend({}, $.fn.PrepaidPinPukSearch.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidPinPukSearch', data);
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

    $.fn.PrepaidPinPukSearch.defaults = {
    }

    $('#containerPinPukSearch', $('.modal:last')).PrepaidPinPukSearch();

})(jQuery);

