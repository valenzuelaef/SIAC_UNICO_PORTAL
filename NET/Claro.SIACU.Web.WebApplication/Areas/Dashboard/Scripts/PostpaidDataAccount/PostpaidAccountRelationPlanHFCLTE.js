(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountRelationPlanHFCLTE.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , tblGSM: $('#tblGSM', $element)
            , tblClaroHogar: $('#tblClaroHogar', $element)
            , tblEquipos: $('#tblEquipos', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;


            that.render();
        },

        desingtblGSM: function () {
            var that = this,
            controls = that.getControls();
            controls.tblGSM.DataTable(
                {
                  info: false
                , scrollY: "150px"
                , select: "single"
                , scrollCollapse: true 
                , paging: false
                , searching: false
                , destroy: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                  , 'columnDefs': [{
                             'targets': 0,
                             'searchable': false,
                             'orderable': false,
                             'className': 'dt-body-center',
                             'render': function (data, type, full, meta) {
                                 return '<input type="checkbox" name="id[]" value="'
                                    + $('<div/>').text(data).html() + '">';
                             }
                   }]
                }
            );
        },

        desingtblClaroHogar: function () {
            var that = this,

            controls = that.getControls();
            controls.tblClaroHogar.DataTable({
                info: false
                , scrollY: "150px"
                , select: "single"
                , scrollCollapse: true
                , paging: false
                , searching: false
                , destroy: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                ,'columnDefs': [{
                    'targets': 0,
                    'searchable':false,
                    'orderable':false,
                    className: 'select-radio'
                }]
            });
        },

        desingtblEquipos: function () {
            var that = this,

            controls = that.getControls();
            controls.tblEquipos.DataTable({
                info: false
                , scrollY: "150px"
                , select: "single"
                , scrollCollapse: true
                , paging: false
                , searching: false
                , destroy: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                  , 'columnDefs': [{
                      'targets': 0,
                      'searchable': false,
                      'orderable': false,
                      'className': 'dt-body-center',
                      'render': function (data, type, full, meta) {
                          return '<input type="checkbox" name="id[]" value="'
                             + $('<div/>').text(data).html() + '">';
                      }
                  }]
            });
        },
        render: function () {
            var that = this;
            that.desingtblGSM();
            that.desingtblClaroHogar();
            that.desingtblEquipos();
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

    $.fn.formAccountRelationPlanHFCLTE = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('formAccountRelationPlanHFCLTE'),
                options = $.extend({}, $.fn.formAccountRelationPlanHFCLTE.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountRelationPlanHFCLTE', data);
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

    $.fn.formAccountRelationPlanHFCLTE.defaults = {
    }

    $('#ContentPostpaidAccountRelationPlanHFCLTE', $('.modal:last')).formAccountRelationPlanHFCLTE();

})(jQuery);
