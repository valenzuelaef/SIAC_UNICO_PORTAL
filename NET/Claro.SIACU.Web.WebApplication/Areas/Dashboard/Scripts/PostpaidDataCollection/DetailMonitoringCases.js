(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidDetailMonitoringCases.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            tblDetailMonitoringCases: $('#tblDetailMonitoringCases', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getLoadDataTable();
            this.numberCase_click();
        },
        numberCase_click: function () {
            var controls = this.getControls();
            controls.tblDetailMonitoringCases.find('tbody').find('a').addEvent(this, 'click', this.trackingDetail);
        },
        createDialogTrackingDetail: function () {
            var that = this;

            $.window.open({
                modal: false,
                minimize: false,
                title: "Gestión Seguimiento de Casos",
                url: '~/Dashboard/PostpaidDataCollection/TrackingDetail',
                width: 1024,
                height: 650,
                buttons: {
                    "Cierre y Reapertura": {
                        click: function (sender, args) {
                            that.createDialogClosuresAndReclosures();
                        }
                    },
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        createDialogClosuresAndReclosures: function () {
            $.window.open({
                modal: true,
                minimize: false,
                title: "Visualización de Cierres y Reaperturas",
                url: '~/Dashboard/PostpaidDataCollection/ClosuresAndReclosures',
                width: 1024,
                height: 650,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        trackingDetail: function (send, args) {
            var that = this,
                controls = that.getControls();

            args.event.preventDefault();

            that.objRow = controls.tblDetailMonitoringCases.DataTable().row($(send).parents('tr')).data();
            that.createDialogTrackingDetail();
        },
        getLoadDataTable: function () {
            this.getControls().tblDetailMonitoringCases.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollX: true,
                scrollY: 230,
                scrollCollapse: true,
                order: [[2, 'asc']],
                destroy: true,
                columnDefs: [
                    { visible: false, targets: 0 },
                    {
                        targets: 1,
                        render: function (data, type, full, meta) {
                            return '<a href="#">' + full[1] + '</a>';
                        }
                    },
                    { type: "date", targets: 2 }
                ],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        getObjRow: function () {
            return this.objRow;
        },
        strTitleMessage: "Consulta Deuda Número Documento",
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };
    $.fn.FormPostpaidDetailMonitoringCases = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getObjRow'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidDetailMonitoringCases'),
                options = $.extend({}, $.fn.FormPostpaidDetailMonitoringCases.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidDetailMonitoringCases', data);
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
    $.fn.FormPostpaidDetailMonitoringCases.defaults = {};
    $('#PostpaidDetailMonitoringCases', $('.modal:last')).FormPostpaidDetailMonitoringCases();
})(jQuery);
