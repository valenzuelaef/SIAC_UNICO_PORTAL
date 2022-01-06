(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidServiceBSCS.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCommercialService: $('#lblCommercialService', $element)
            , tblServiceBSCS: $('#tblServiceBSCS', $element)
            , contenedorServices: $('#contenedorServices', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },
        render: function () {
            this.createTableServicesBSCS();
        },
        createTableServicesBSCS: function () {
            var that = this,
                controls = that.getControls(),
                objServicesBSCS = window.opener.$('#contenedorServices').PostPaidServices('getObjServicesBSCS');
            
            if (objServicesBSCS.data != null) {
                controls.tblServiceBSCS.DataTable({
                    scrollY: 300
                    , scrollCollapse: true
                    , paging: false
                    , info: false
                    , select : 'single'
                    , destroy: true
                    , searching: false
                    , data: objServicesBSCS.data.BSCSServices
                    , columns: [
                        { "data": "Service" },
                        { "data": "Package" },
                        { "data": "State" }
                    ]
                    , language: {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidServiceBSCS = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidServiceBSCS'),
                options = $.extend({}, $.fn.PostPaidServiceBSCS.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidServiceBSCS', data);
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

    $.fn.PostPaidServiceBSCS.defaults = {
    }

    $('#contenedorServicesBSCS', $('.modal:last')).PostPaidServiceBSCS();

})(jQuery);