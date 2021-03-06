(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPospaidHistoricoSIM.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbPost_HistoricoSIM: $('#tbHistoricoSIM', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var controls = this.getControls();

            controls.tbPost_HistoricoSIM.DataTable({
                "info": false,
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "select": 'single',
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


    $.fn.FormPospaidHistoricoSIM = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPospaidHistoricoSIM'),
                options = $.extend({}, $.fn.FormPospaidHistoricoSIM.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPospaidHistoricoSIM', data);
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

    $.fn.FormPospaidHistoricoSIM.defaults = {
    }

    $('#PostpaidHistoricoSIMList', $('.modal:last')).FormPospaidHistoricoSIM();

})(jQuery);