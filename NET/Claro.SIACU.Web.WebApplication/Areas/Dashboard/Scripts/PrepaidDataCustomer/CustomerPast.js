(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidCustomerPast.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblCustomerPast: $('#tblCustomerPast', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getDataTable();
        },

        getDataTable: function () {
            var controls = this.getControls();
   
            controls.tblCustomerPast.DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "select": {
                    "style" : "os",
                    "info" : false
                },
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columnDefs": [{
                    "orderable" : false,
                    "className" : 'select-radio',
                    "targets" : 0,
                },
                {
                    "targets": 1,
                    "visible" : false
                }
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

    $.fn.PrepaidCustomerPast = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidCustomerPast'),
                options = $.extend({}, $.fn.PrepaidCustomerPast.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidCustomerPast', data);
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

    $.fn.PrepaidCustomerPast.defaults = {
    }

    $('#contenedorCustomerPast', $('.modal:last')).PrepaidCustomerPast();

})(jQuery);