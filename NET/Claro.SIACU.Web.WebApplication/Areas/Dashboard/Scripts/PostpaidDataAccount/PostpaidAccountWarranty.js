(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountWarranty.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element
          , tblRenta: $('#tblRenta', $element)
          , tblDeposito: $('#tblDeposito', $element)
        });
       }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.getDataTableWarranty();
            that.render();
        },
        getDataTableWarranty: function () {
            var that = this;
            var controls = that.getControls();           

            controls.tblRenta.DataTable({
                "info": false,
                "scrollY": "150px",
                "select": "single",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                 "sScrollXInner": "100%",
                "autoWidth": true
                , "language":
                    {
                        "lengthMenu": "Display _MENU_ records per page",
                        "zeroRecords": "No existen datos",
                        "info": " ",
                        "infoEmpty": " ",
                        "infoFiltered": "(filtered from _MAX_ total records)"
                    }
            });

            controls.tblDeposito.DataTable({
                "info": false,
                "scrollY": "150px",
                "select": "single",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                 "sScrollXInner": "100%",
                "autoWidth": true
                , "language":
                    {
                        "lengthMenu": "Display _MENU_ records per page",
                        "zeroRecords": "No existen datos",
                        "info": " ",
                        "infoEmpty": " ",
                        "infoFiltered": "(filtered from _MAX_ total records)"
                    }
            });
        },      
        render: function () {
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'

    };

    $.fn.formAccountWarranty = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountWarranty'),
                options = $.extend({}, $.fn.formAccountWarranty.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountWarranty', data);
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

    $.fn.formAccountWarranty.defaults = {
    }

    $('#ContentAccountWarranty', $('.modal:last')).formAccountWarranty();

})(jQuery);