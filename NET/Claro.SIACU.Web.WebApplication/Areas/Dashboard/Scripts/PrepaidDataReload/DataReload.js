
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.DataReloadPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

          , btnVisualizeReload: $('#btnVisualizeReload', $element)
          , txtReloadDateFrom: $('#txtReloadDateFrom', $element)
          , txtReloadDateTo: $('#txtReloadDateTo', $element)
          , tblDataReload: $('#tblDataReload', $element)
          , btnRechargeReload: $('#btnRechargeReload', $element)
          , txtReloadNumberLot: $('#txtReloadNumberLot', $element)
          , btnConsultReload: $('#btnConsultReload', $element)
          , btnChangeStateReload: $('#btnChangeStateReload', $element)
          , btnCopyRow: $('#btnCopyRow', $element)
          , containerDateRange: $('#containerDateRange', $element)
          , btnExcelReload: $('#btnExcelReload', $element)
          , btnInLineRecharge: $('#btnInLineRecharge', $element)
          , ddlTipoMov: $('#ddlTipoMov', $element)
          , ddlCredDev: $('#ddlCredDev', $element)
          , btnConsumptionDataPacket: $('#btnConsumptionDataPacket', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnVisualizeReload.addEvent(that, 'click', that.btnVisualizeReload_click);
            controls.btnRechargeReload.addEvent(that, 'click', that.btnRechargeReload_click);
            controls.btnConsultReload.addEvent(that, 'click', that.btnConsultReload_click);
            controls.btnChangeStateReload.addEvent(that, 'click', that.btnChangeStateReload_click);
            controls.btnExcelReload.addEvent(that, 'click', that.btnExcelReload_click);
            controls.btnInLineRecharge.addEvent(that, 'click', that.btnInLineRecharge_click);
            controls.btnConsumptionDataPacket.addEvent(that, 'click', that.btnConsumptionDataPacket_click);
            controls.btnCopyRow.addEvent(that, 'click', that.btnCopyRow_click);

            that.render();
        },
        render: function () {
            this.getMovementType();
            this.getCredDevType();
            this.dataCustomerReload();
            this.getDataTable();
        },

        getMovementType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/Prepaid/GetMovementType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.ddlTipoMov
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },

        getCredDevType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/Prepaid/GetCredDevType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.ddlCredDev
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },
        btnCopyRow_click: function () {
            var fn = function (str) {
                var $temp = $("<textarea>");
                $("body").append($temp);
                $temp.val(str).select();
                document.execCommand("copy");
                $temp.remove();
            };
            var strRow = '';
            $('.SelectCopy').each(function (i, e) {

                if ($(e).prop('checked') == true) {
                    var td = $(e).parent().parent().find('td');
                    td.splice(0, 1);
                    var str = "";
                    for (var n = 0; n <= td.length - 1; n++) {
                        str = str + " " + $(td[n]).html();

                    }
                    if (str != '') {
                        strRow = strRow + str + '\n';
                    }
                   
                };
            });

            if (strRow != '') {
                fn(strRow);
            }

        },
        btnExcelReload_click: function () {
            var controls = this.getControls(),
                strUrlResult = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }),
                oContract = {
                    strIdSession: Session.IDSESSION,
                    strTelephoneCustomer: $('#hidReloadTelephoneCustomer').val(),
                    strDateFrom: controls.txtReloadDateFrom.val(),
                    strDateTo: controls.txtReloadDateTo.val()
                };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PrepaidDataReload/GetExportExcelReload',
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ReporteDeRecargas.xlsx";
                }
            });
        },
        btnVisualizeReload_click: function () {
            var controls = this.getControls();

            $.window.open({
                modal: false,
                title: "CONSULTA DE RECARGAS",
                url: '~/Dashboard/PrepaidDataReload/VisualizeReload',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: $('#hidReloadTelephoneCustomer').val(), strDateFrom: controls.txtReloadDateFrom.val(), strDateTo: controls.txtReloadDateTo.val(), strMovementType: controls.ddlTipoMov.find('option:selected').val(), strcreditoDebito: controls.ddlCredDev.find('option:selected').val() },
                width: 1120,
                height: 600,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnRechargeReload_click: function () {
            var that = this,
                  contacto = Session.DATACUSTOMER.FullName;
            that.setNumLote();
            Session.OPTIONREDIRECT = 'SU_SIACA_RT'
            Session.TIPO = '3'
            Session.CONTACTO = contacto
            $.redirect.GetParamsData(Session.OPTIONREDIRECT, "PREPAGO");
        },
        btnConsultReload_click: function () {
            var that = this,
                contacto = Session.DATACUSTOMER.FullName;
            that.setNumLote();
            Session.OPTIONREDIRECT = 'SU_SIACA_RT'
            Session.TIPO = '1'
            Session.CONTACTO = contacto
            $.redirect.GetParamsData(Session.OPTIONREDIRECT, "PREPAGO");
        },
        btnChangeStateReload_click: function () {
            var that = this,
                  contacto = Session.DATACUSTOMER.FullName;
            that.setNumLote();
            Session.OPTIONREDIRECT = 'SU_SIACA_RT'
            Session.TIPO = '2'
            Session.CONTACTO = contacto
            $.redirect.GetParamsData(Session.OPTIONREDIRECT, "PREPAGO");
        },
        btnInLineRecharge_click: function () {
            var fn_success = function () {
            $.window.open({
                modal: false,
                title: "DETALLE DE RECARGAS EN LINEA",
                url: '~/Dashboard/PrepaidDataReload/OnlineReload',
                width: 1020,
                height: 600
            }).maximize();
            };
            if (Session.USERACCESS.optionPermissions.indexOf('SU_SIACA_DREL') == -1) {
                ValidateUser('gConstkeyDetalleRecargasEnLinea', fn_success, null, null, null);

            } else {
                fn_success();
            }

           
        },
        btnConsumptionDataPacket_click: function () {
            $.window.open({
                modal: false,
                title: "CONSULTA PAQUETE DE DATOS",
                url: '~/Dashboard/PrepaidDataReload/ConsumptionDataPacket',
                width: 1020,
                height: 600,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#containerConsumptionDataPacket').ConsumptionDataPacket('getExcelConsumptionDataPacket');
                        }
                    }

                }
            }).maximize();
        },
        setNumLote: function () {
            var controls = this.getControls();
            Session.NUMLOTE = controls.txtReloadNumberLot.val();
        },
        dataCustomerReload: function () {

            var controls = this.getControls();

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
            var tbl = controls.tblDataReload.DataTable({
                "scrollX": true,
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "info": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                order: [0, 'desc']
            });

            var headers = $('#headers').val();
            var splitHeaders = headers.split(',');
            if (splitHeaders != null) {
                for (var i = 0; i <= splitHeaders.length - 1; i++) {
                    tbl.column(i).visible((/true/i).test(splitHeaders[i]));

                }
            }

        }

    };

    $.fn.DataReloadPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('DataReloadPrepaid'),
                options = $.extend({}, $.fn.DataReloadPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('DataReloadPrepaid', data);
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

    $.fn.DataReloadPrepaid.defaults = {
    }

    $('#contenedorDataReload').DataReloadPrepaid();

})(jQuery);