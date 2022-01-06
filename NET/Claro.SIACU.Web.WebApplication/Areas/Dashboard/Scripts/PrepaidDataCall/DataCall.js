
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.DataCallPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            , btnVisualizeCall: $('#btnVisualizeCall', $element)
            , txtCallDateFrom: $('#txtCallDateFrom', $element)
            , txtCallDateTo: $('#txtCallDateTo', $element)
            , tblDataCall: $('#tblDataCall', $element)
            , btnExcel: $('#btnExcel', $element)
            , btnPrintCall: $('#btnPrintCall', $element)
            , cboEventType: $('#cboEventType', $element)
            , btnInLine: $('#btnInLine', $element)
            , hidCallTelephoneCustomer: $('#hidCallTelephoneCustomer', $element)
            , containerDateRange: $('#containerDateRange', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnPrintCall.addEvent(that, 'click', that.btnPrintCall_click);
            controls.btnExcel.addEvent(that, 'click', that.btnExcel_click);
            controls.btnVisualizeCall.addEvent(that, 'click', that.btnVisualizeCall_click);
            controls.btnInLine.addEvent(that, 'click', that.btnInLine_click);


            var fDateToDay = new Date(), fDatePrevious = new Date();
            fDateToDay = $.app.date.format(fDateToDay, { format: 'dd/mm/yy' });
            fDatePrevious = $.app.date.addMonth(fDatePrevious, -2, { format: 'dd/mm/yy' });

            controls.containerDateRange.datepicker({
                format: 'dd/mm/yyyy',
                startDate: fDatePrevious,
                endDate: fDateToDay,
                beforeShowDay: function (date) {
                    $.app.date.limitedDate(date);
                }
            });

            that.render();
        },
        render: function () {
            this.getEventType();
            this.getDataTable();

        },
        getEventType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataCall/GetTraffictType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.cboEventType
                        .populateDropDown({
                            dataSource: response.data,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },
        btnExcel_click: function () {
            var controls = this.getControls(),
                oContract = {
                    strIdSession: Session.IDSESSION,
                    Msisdn: Session.TELEPHONE,
                    StartDate: controls.txtCallDateFrom.val(),
                    EndDate: controls.txtCallDateTo.val(),
                    TrafficType: $('#cboEventType option:selected').val()
                };

            var fn_success=function () {
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PrepaidDataCall/GetExportExcelCall',
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }) + '?strPath=' + path + "&strNewfileName=ReporteDeLlamadas.xlsx";
                }
            });
            }; 
            if (Session.USERACCESS.optionPermissions.indexOf('SU_SIACA_EDLM') == -1) {
                ValidateUser('gConstkeyBuscarDetalleLLamadaAutorizada', fn_success, null, null, null);

            } else {
                fn_success();
            }



        },
        btnPrintCall_click: function () {
            var that = this,
           controls = that.getControls();
            var prm = '?strIdSession=' + Session.IDSESSION + '&strTelephoneCustomer=' + Session.TELEPHONE + '&strDateFrom=' + controls.txtCallDateFrom.val() + '&strDateTo=' + controls.txtCallDateTo.val()
            var view = $.app.getPageUrl({ url: '~/Dashboard/Format/PrintCall' + prm });
            var fn_success = function () {
            var ventimp = window.open(view, 'popimpr', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1250, height=650');
            ventimp.focus();

            $(ventimp.document).ready(function () {
                ventimp.window.focus();
                ventimp.document.close();
                ventimp.window.print();
            });
            };
            if (Session.USERACCESS.optionPermissions.indexOf('SU_SIACA_IDLM') == -1) {
                ValidateUser('gConstkeyImpresionDetalleLLamadaAutorizada', fn_success, null, null, null);

            } else {
                fn_success();
            }

        },

        btnVisualizeCall_click: function () {
            var controls = this.getControls();
            if (controls.txtCallDateFrom.val() == '' || controls.txtCallDateTo.val() == '') {
                showAlert('Error al ingresar rango de fechas', 'Error')
                        }
            else {
                modalConfirm('Se generará Tipificación por la información', '', function (result) {
                    if (result == true) {
                        $.window.open({
                            modal: false,
                            title: "CONSULTA DE LLAMADAS",
                            url: '~/Dashboard/PrepaidDataCall/VisualizeCall',
                            data: {
                                strIdSession: Session.IDSESSION,
                                strDateFrom: controls.txtCallDateFrom.val(),
                                strDateTo: controls.txtCallDateTo.val(),
                                TrafficType: $('#cboEventType option:selected').val()
                            },
                            width: 1020,
                            height: 600,
                            buttons: {
                                Exportar: {
                                    click: function (sender, args) {
                                        var modalJQuery = args.event.view.$;

                                        modalJQuery('#contenedorVisualizeCallDetail').VisualizeCallPrepaid('getExportExcelCall');

                                    }
                                },
                                Cancelar: {
                                    click: function (sender, args) {
                                        this.close();
                                    }
                                }
                            }
                        });
                        }
                    else {
                        return;
                    }
                });
                }
        },
        btnInLine_click: function () {
            var fn_success = function () {
            $.window.open({
                modal: false,
                title: "DETALLE DE LLAMADAS Y CONSULTAS EN LINEA",
                url: '~/Dashboard/PrepaidDataCall/InLineCall',
                width: 1020,
                height: 600
            }).maximize();
            };
            if (Session.USERACCESS.optionPermissions.indexOf('SU_SIACA_DLEL') == -1) {
                ValidateUser('gConstkeyDetalleLLamadaEnLinea', fn_success, null, null, null);

            } else {
                fn_success();
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        },
        getDataTable: function () {
            var controls = this.getControls();
            var headers = $('#headers').val(),
                splitHeaders = headers.split(',');
            var tbl = controls.tblDataCall.DataTable({
                "scrollX": true,
                "scrollY": "200px",
                "scrollCollapse": true,
                "info": false,
                "select": "single",
                "paging": false,
                "destroy": true,
                "ordering": false,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
            $.each(splitHeaders, function (index, value) {
                tbl.column(index).visible((/true/i).test(value))
            });
        },
        loadPrint: function () {
            window.focus();
            if (navigator.appName == 'Microsoft Internet Explorer') {
                window.print();
                window.close();
            }
        }


    };

    $.fn.DataCallPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('DataCallPrepaid'),
                options = $.extend({}, $.fn.DataCallPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('DataCallPrepaid', data);
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

    $.fn.DataCallPrepaid.defaults = {
    }

    $('#contenedorDataCall').DataCallPrepaid();

})(jQuery);