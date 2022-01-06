(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.contenedorVisualizeCallDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

          , divDetailVisualizeCall: $('#divDetailVisualizeCall', $element)
          , cboTrafficType: $('#cboTrafficType', $element)
          , divCallBag: $('#divCallBag', $element)
          , btnSearchDataCall: $('#btnSearchDataCall', $element)
          , btnClearDataCall: $('#btnClearDataCall', $element)
          , txtDetailCallDateFrom: $('#txtDetailCallDateFrom', $element)
          , txtDetailCallDateTo: $('#txtDetailCallDateTo', $element)
          , containerDateRange: $('#containerDateRange', $element)
          , tblDataVisualizeCall: $('#tblDataVisualizeCall', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSearchDataCall.addEvent(that, 'click', that.btnSearchDataCall_click);
            controls.btnClearDataCall.addEvent(that, 'click', that.btnClearDataCall_click);

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
            this.showDetailCall();
        },


        btnSearchDataCall_click: function () {
            var controls = this.getControls();

            if (controls.txtDetailCallDateFrom.val() == '' || controls.txtDetailCallDateTo.val() == '') {
                showAlert('Error al ingresar rango de fechas', 'Error')
            }
            else {
                this.showDetailCall();
            }
        },
        btnClearDataCall_click: function () {
            var controls = this.getControls();

            controls.txtDetailCallDateFrom.val('');
            controls.txtDetailCallDateTo.val('');
            controls.cboTrafficType.val('0');
            $('#tblDetailVisualizeCall').find('tbody').html("");
        },
        getExportExcelCall: function () {
            var controls = this.getControls(),
                objExportExcelCall = {
                    strIdSession: Session.IDSESSION,
                    Msisdn: Session.TELEPHONE,
                    StartDate: controls.txtDetailCallDateFrom.val(),
                    EndDate: controls.txtDetailCallDateTo.val(),
                    TrafficType: controls.cboTrafficType.find(":selected").val()
                };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PrepaidDataCall/GetExportExcelCall',
                data: JSON.stringify(objExportExcelCall),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }) + '?strPath=' + path + "&strNewfileName=ReporteDeLlamadas.xlsx";
                }
            });
        },
        showDetailCall: function () {

            var controls = this.getControls(),
                fDateToDay,
                fDatePrevious;

            if (controls.txtDetailCallDateFrom.val() == "" && controls.txtDetailCallDateTo.val() == "") {
                fDateToDay = new Date(), fDatePrevious = new Date();

                fDateToDay = $.app.date.format(fDateToDay, { format: 'dd/mm/yy' });
                fDatePrevious = $.app.date.addMonth(fDatePrevious, -2, { format: 'dd/mm/yy' });
            } else {
                fDatePrevious = controls.txtDetailCallDateFrom.val();
                fDateToDay = controls.txtDetailCallDateTo.val();
            }
            var data = {
                strIdSession: Session.IDSESSION,
                Msisdn: Session.TELEPHONE,
                StartDate: fDatePrevious,
                EndDate: fDateToDay,
                TrafficType: controls.cboTrafficType.find('option:selected').val(),
                isTFI: Session.DATASERVICE.IsTFI
            };


            $.app.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: '~/Dashboard/PrepaidDataCall/DetailVisualizeCall',
                data: JSON.stringify(data),
                success: function (result) {
                    var splitHeaders = result.headers.split(',');
                    
                    var tbl = controls.tblDataVisualizeCall.DataTable({
                        info: false,
                        paging: false,
                        scrollY: 300,
                        scrollX: true,
                        destroy: true,
                        scrollCollapse: true,
                        searching: false,
                        select: 'single',
                        data: result.data,
                        columns: [
                            { "data": "CallDateTime" },
                            { "data": "CallTelephoneDestination" },
                            { "data": "CallTypeTraffic" },
                            { "data": "CallDuration" },
                            { "data": "CallUptake" },
                            { "data": "CallBoughtPresented" },
                            { "data": "CallBalance" },
                            { "data": "CallBagId" },
                            { "data": "CallDescription" },
                            { "data": "CallPlan" },
                            { "data": "CallPromotion" },
                            { "data": "CallDestination" },
                            { "data": "CallOperator" },
                            { "data": "CallCobroGroup" },
                            { "data": "CallNetworkType" },
                            { "data": "CallImei" },
                            { "data": "CallRoaming" },
                            { "data": "CallTariffArea" }
                        ],
                        language: {
                            "lengthMenu": "Display _MENU_ records per page",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)"
                        }
                    });

                   
                    if (splitHeaders != null) {
                        for (var i = 0; i <= splitHeaders.length - 1; i++) {
                            tbl.column(i).visible((/true/i).test(splitHeaders[i]));
                            
                        }
                    }

                    var cantvoz = 0, cantsms = 0, cantmms = 0, cantdatos = 0.0, SegundosTotal = 0, SconsumoVoz = 0.0,
                    SconsumoIP = 0.0, SConsumoSMS = 0.0, SConsumoMMS = 0.0, SconsumoDatos = 0.0;
                    var dataCall = result.data;
                    var rows = 0;
                    if (dataCall != null) {
                        $.each(dataCall, function (index, value) {
                            rows += 1;
                            if (value.CallTypeTraffic == "VOZ") {
                                var duracionT, segundos;
                                duracionT = value.CallDuration;
                                segundos = duracionT.split(':');
                                SegundosTotal = parseInt(segundos[2]) + (parseInt(segundos[1]) * 60) + (parseInt(segundos[0]) * 60) * 60;
                                cantvoz = parseInt(cantvoz) + parseInt(SegundosTotal);
                                var ConsumoVoz = value.CallUptake.indexOf(".");
                                if (ConsumoVoz > 0) {
                                    SconsumoVoz = SconsumoVoz + parseFloat(value.CallUptake);
                                }
                            } else if (value.CallTypeTraffic == "SMS") {
                                cantsms += parseFloat(value.CallDuration);
                                var ConsumoSMS = value.CallUptake.indexOf(".");
                                if (ConsumoSMS > 0) {
                                    SConsumoSMS = parseFloat(SConsumoSMS) + parseFloat(value.CallUptake);
                                }
                            } else if (value.CallTypeTraffic == "MMS") {
                                cantmms += parseFloat(value.CallDuration);
                                var ConsumoMMS = value.CallUptake.indexOf(".");
                                if (ConsumoMMS > 0) {
                                    SConsumoMMS = parseFloat(SConsumoMMS) + parseFloat(value.CallUptake);
                                }
                            } else if (value.CallTypeTraffic == "DATOS") {
                                cantdatos += parseFloat(value.CallDuration);
                                if (value.CallUptake.indexOf("MB") == -1) {
                                    SconsumoDatos = parseFloat(SconsumoDatos) + parseFloat(value.CallUptake);
                                }
                            } else {
                                SconsumoIP = parseFloat(SconsumoIP) + parseFloat(value.CallUptake);
                            }
                        })
                    }

                    //'convertir de segundos a horas y minutos
                    var hours = (parseInt(cantvoz) / 3600)
                    var min = parseInt((parseInt(cantvoz) - (parseInt(hours) * 3600)) / 60);
                    var seconds = parseInt(cantvoz) - ((parseInt(hours) * 3600) + (parseInt(min) * 60))
                    //'2 dísgitos
                    var strHorasCad, strminutusCad, strsegundoCad;
                    strHorasCad = hours.toString();
                    strminutusCad = min.toString();
                    strsegundoCad = seconds.toString();
                    if (hours.toString().Length < 2) {
                        strHorasCad = "0" + hours.toString();
                    }
                    if (min.toString().Length < 2) {

                        strminutusCad = "0" + min.toString();
                    }
                    if (seconds.toString().Length < 2) {
                        strsegundoCad = "0" + seconds.toString();
                    }
                    var cantm = SconsumoVoz + SconsumoIP + SConsumoSMS + SConsumoMMS + SconsumoDatos;
                    $('#voz').text(parseInt(strHorasCad) + ":" + parseInt(strminutusCad) + ":" + parseInt(strsegundoCad));
                    $('#sms').text(cantsms.toString());
                    $('#mms').text(cantmms.toString());
                    $('#gprs').text(cantdatos.toFixed(2).toString());
                    $('#consumo').text(cantm.toFixed(2).toString());
                    $('#registro').text(rows)
                },
                error: function (ex) {
                    controls.divDetailVisualizeCall.showMessageErrorLoading($.app.const.messageErrorLoading);
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

    };

    $.fn.contenedorVisualizeCallDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getExportExcelCall'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('contenedorVisualizeCallDetail'),
                options = $.extend({}, $.fn.contenedorVisualizeCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('contenedorVisualizeCallDetail', data);
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

    $.fn.contenedorVisualizeCallDetail.defaults = {
    }

    $('#contenedorVisualizeCallDetail').contenedorVisualizeCallDetail();

})(jQuery);