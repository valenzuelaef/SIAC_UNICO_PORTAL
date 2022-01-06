(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.MasiveInstantFail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblInsMasiveFail: $('#tblInsMasiveFail', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.createTableMasiveInstantFail();
        },
        createTableMasiveInstantFail: function () {
            this.getControls().tblInsMasiveFail.DataTable({
                "scrollY": "250px",
                "scrollCollapse": true,
                "destroy": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "info": false,
                "data": Session.ListMassiveInstantFail,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoempty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }, "columns": [
                        { "data": "Number" },
                        { "data": "Description" }
                ]
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.MasiveInstantFail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('MasiveInstantFail'),
                options = $.extend({}, $.fn.MasiveInstantFail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('MasiveInstantFail', data);
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

    $.fn.MasiveInstantFail.defaults = {
    }

    $('#ContentMassiveInstantFail', $('.modal:last')).MasiveInstantFail();

})(jQuery);