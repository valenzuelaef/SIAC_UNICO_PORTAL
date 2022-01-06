(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPospaidPlanHistory.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbPost_PlanHistory: $('#tbPost_PlanHistory', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var controls = this.getControls();

            controls.tbPost_PlanHistory.DataTable({
                "info": false,
                "scrollY": "200px",
                "scrollCollapse": true,
                "select": "single",
                "paging": false,
                "destroy": true,
                "searching": false,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columnDefs: [
                { type: "date", targets: 0 }
                ],
                "order": [0, 'desc']
            });

        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.FormPospaidPlanHistory = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPospaidPlanHistory'),
                options = $.extend({}, $.fn.FormPospaidPlanHistory.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPospaidPlanHistory', data);
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

    $.fn.FormPospaidPlanHistory.defaults = {
    }

    $('#PospaidPlanHistory', $('.modal:last')).FormPospaidPlanHistory();

})(jQuery);