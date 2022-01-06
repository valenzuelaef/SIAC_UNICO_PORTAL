(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormInternationalRoamingDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblInternationalRoamingDetail: $('#tblInternationalRoamingDetail', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = that.getControls();

            controls.tblInternationalRoamingDetail.DataTable({
                "info": false,
                "scrollY": "400px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "destroy": true,
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

            that.render();
        },
        render: function () {
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.FormInternationalRoamingDetail = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormInternationalRoamingDetail'),
                options = $.extend({}, $.fn.FormInternationalRoamingDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormInternationalRoamingDetail', data);
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

    $.fn.FormInternationalRoamingDetail.defaults = {
    }

    $('#FormInternationalRoamingDetail', $('.modal:last')).FormInternationalRoamingDetail();

})(jQuery);
