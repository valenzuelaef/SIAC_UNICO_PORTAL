(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidDetailVisualizeReaload.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblDetailVisualizeReload: $('#tblDetailVisualizeReload', $element)

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

            var tbl = controls.tblDetailVisualizeReload.DataTable({
                "scrollX": true,
                "scrollY": "200px",
                "scrollCollapse": true,
                "info": false,
                "select": "single",
                "paging": false,
                "destroy": true,
                "ordering": true,
                "searching": false,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                order: [0, 'desc']
            });
            var headers = $('#headers').val();
            var splitHeaders = headers.split(',');
            $.each(splitHeaders, function (index, value) {
                tbl.column(index).visible((/true/i).test(value));
            });

        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.PrepaidDetailVisualizeReaload = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidDetailVisualizeReaload'),
                options = $.extend({}, $.fn.PrepaidDetailVisualizeReaload.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidDetailVisualizeReaload', data);
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

    $.fn.PrepaidDetailVisualizeReaload.defaults = {
    }

    $('#contenedorDetailVisualizeReload', $('.modal:last')).PrepaidDetailVisualizeReaload();

})(jQuery);