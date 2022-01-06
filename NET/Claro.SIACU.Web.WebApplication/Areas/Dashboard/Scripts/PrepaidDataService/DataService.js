(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.DataServicePrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
        
          , lblPre_Answer: $('#lblPre_Answer', $element)
          , trStatePortability: $('#trStatePortability', $element)
          , lblPre_BalancePrincipal: $('#lblPre_BalancePrincipal', $element)
          , lblPre_StatusIMSI: $('#lblPre_StatusIMSI', $element)
          , lblPre_DateExpirationBalance: $('#lblPre_DateExpirationBalance', $element)
          , lblPre_DateExpirationLine: $('#lblPre_DateExpirationLine', $element)
          , hidStatus: $('#hidStatus', $element)
          , lblPre_StateLine_2: $('#lblPre_StateLine_2', $element)
          , hidIsTFI: $('#hidIsTFI', $element)
          , lblPre_Payment: $('#lblPre_Payment', $element)
          , lblPre_BalancePending: $('#lblPre_BalancePending', $element)
          , lblPre_PlanRate: $('#Session.PlanRate', $element)
          , btnPortabilityConsultation: $('#btnPortabilityConsultation', $element)
          , btnPinkpuk: $('#btnPinkpuk', $element)
          , btnSalesdues: $('#btnSalesdues', $element)
          , btnDatasale: $('#btnDatasale', $element)
          , btnHistoricalStriations: $('#btnHistoricalStriations', $element)
          , btnHistoricalBonds: $('#btnHistoricalBonds', $element)
          , btnReloadService: $('#btnReloadService', $element)
           , btnHistorialBalance: $('#btnHistorialBalance', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnPortabilityConsultation.addEvent(that, 'click', that.btnPortabilityConsultation_click);
            controls.btnPinkpuk.addEvent(that, 'click', that.btnPinkpuk_click);
            controls.btnSalesdues.addEvent(that, 'click', that.btnSalesdues_click);
            controls.btnDatasale.addEvent(that, 'click', that.btnDatasale_click);
            controls.btnHistoricalStriations.addEvent(that, 'click', that.btnHistoricalStriations_click);
            controls.btnHistoricalBonds.addEvent(that, 'click', that.btnHistoricalBonds_click);
            controls.btnReloadService.addEvent(that, 'click', that.btnReloadService_click);
            controls.btnHistorialBalance.addEvent(that, 'click', that.btnHistorialBalance_click);

            if (Session.DATACUSTOMER.BlackList == "0") {
                controls.btnPinkpuk.prop("disabled", true);
                controls.btnSalesdues.prop("disabled", true);
                controls.btnDatasale.prop("disabled", true);
            }
            that.render();
        },
        render: function () {

            this.validateItems();
            if (Session.DATASERVICE != null) { 
            var list = Session.DATASERVICE.listTrio;
            var strNumberTriados = '';
            $(list).each(function (e) {
                if (strNumberTriados== '') {
                    strNumberTriados = this.Number;
                } else {
                    strNumberTriados = strNumberTriados + ',' + this.Number;
                }

            });
            Session.STRIPNUMBERTRI = strNumberTriados;
            } else {
                Session.STRIPNUMBERTRI = "";
            }
            
        },

        btnDatasale_click: function () {

            $.window.open({
                modal: false,
                title: "CONSULTA DATOS DE VENTA",
                url: '~/Dashboard/PrepaidDataService/Datasale',
                data: { strIdSession: Session.IDSESSION, strTelephone: Session.TELEPHONE },
                width: 900,
                height: 650,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });

        },
        btnSalesdues_click: function () {

            
            var strUrlModal2 = $.app.getPageUrl({ url: '~/Dashboard/PrepaidDataService/SalesduesFormato'});

            $.window.open({
                modal: false,
                title: "Detalle de Cuotas",
                url: '~/Dashboard/PrepaidDataService/Salesdues',
                data: { strIdSession: Session.IDSESSION, strTelephone: Session.TELEPHONE },
                width: 900,
                height: 750,
                buttons: {
                    Imprimir: {
                        click: function () {

                            var mywindow = window.open(strUrlModal2 + "?strIdSession=" + Session.IDSESSION + "&strTelephone=" + Session.TELEPHONE, "CONSULTA VENTA EN CUOTA", "");

                            mywindow.focus();
                            mywindow.print();


                        }
                    },
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });

        },
        btnPinkpuk_click: function () {

            $.window.open({
                modal: false,
                title: "CONSULTA PIN PUK",
                url: '~/Dashboard/PrepaidDataService/Pinkpuk',
                data: { strIdSession: Session.IDSESSION, strTelephone: Session.TELEPHONE },
                width: 700,
                height: 500,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });

        },
        btnPortabilityConsultation_click: function () {

            $.window.open({
                modal: false,
                title: "Datos de Portabilidad",
                url: '~/Dashboard/PrepaidDataService/PortabilityConsultation',
                data: { strIdSession: Session.IDSESSION, strTelephone: Session.DATACUSTOMER.TelephoneCustomer },
                width: 700,
                height: 300,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnHistoricalStriations_click: function () {

            var controls = this.getControls();
                
                Session.PlanRate = controls.lblPre_PlanRate.text();

            $.window.open({
                modal: false,
                title: "Consulta Histórico de Triaciones",
                url: '~/Dashboard/PrepaidDataService/HistoricalStriations',
                width: 900,
                height: 650,
                buttons: {
                    Exportar: {
                        click: function () {
                            var strUrlResult = $.app.getPageUrl({ url:'~/Dashboard/Home/DownloadExcel'}),
                             objHistoricalTriationRFA = Session.HistoricalTriationRFA;

                            $.app.ajax({
                                type: 'POST',
                                url: '~/Dashboard/PrepaidDataService/ExportHistoricalTriation',
                                dataType: 'JSON',
                                data: { strIdSession: Session.IDSESSION, strTelephone: objHistoricalTriationRFA.Telephone, strPlanTariff: objHistoricalTriationRFA.PlanTariff, strDateStartHT: objHistoricalTriationRFA.DateFrom, strDateEndHT: objHistoricalTriationRFA.DateTo },
                                success: function (path) {
                                    window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ServiceHistoricalTriation.xlsx";
                                },
                                error: function (ex) {
                                }
                            });
                        }
                    },
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        },
                        id: "btnCloseHistTriation",
                    }
                }
            });

        },
        btnHistoricalBonds_click: function () {

            $.window.open({
                modal: false,
                type: 'post',
                title: "Consulta Histórico de Bonos",
                url: '~/Dashboard/PrepaidDataService/HistoricalBonds',
                width: 900,
                height: 400,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {

                            var modalJQuery = args.event.view.$;

                            modalJQuery('#ContentHistoricalBonds').formHistoricalBonds('GetExportHistoricalBonds');
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
        validateItems: function () {

               var controls = this.getControls();

            if (controls.lblPre_Answer.text() != '') {
                controls.lblPre_Answer.css('color', 'red');
            }
            else {
                controls.trStatePortability.hide();
            }

            if (controls.lblPre_BalancePrincipal.text() == '' || controls.lblPre_BalancePrincipal.text() == '0.00') {
                controls.lblPre_BalancePrincipal.css('color', 'red');
            }

            if (controls.lblPre_StatusIMSI.text() == 'Bloqueado') {
                controls.lblPre_StatusIMSI.addClass('label-red-service');
            }
            
               

            if (controls.lblPre_DateExpirationLine.text() != '') {
                if (this.subtractionDays(this.formatDate(controls.lblPre_DateExpirationLine.text())) > 0) {
                    controls.lblPre_DateExpirationLine.css('color', 'red');
                }
            }
            
            switch (controls.hidStatus.val()) {
                case '0': controls.lblPre_StateLine_2.css('color', 'green');
                    break;

                case '2': controls.lblPre_StateLine_2.css('color', 'orange');
                    break;

                case '4':
                case '5':
                    controls.lblPre_StateLine_2.css('color', 'red');
                    break;

                case '10': controls.lblPre_StateLine_2.css('color', 'brown');
                    break;
            }

            if (controls.hidIsTFI.val(true)) {
                controls.lblPre_Payment.visible = true;
                controls.lblPre_BalancePending.visible = true;
            }
            else {
                controls.lblPre_Payment.visible = false;
                controls.lblPre_BalancePending.visible = false;
            }

        },
        formatDate: function (day_1) {

            var element = day_1.split(' ');
            var date = element[0].split('-');
            var dateformat = date[0];
            return dateformat;

        },
        subtractionDays: function (day_1) {

            var aDate1 = day_1.split('/');
            var fDate1 = new Date(aDate1[2], aDate1[1] - 1, aDate1[0]);
            var dif = new Date() - fDate1;
            var days = Math.floor(dif / (1000 * 60 * 60 * 24));
            return days;

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
        btnHistorialBalance_click: function () {
            
            $.window.open({
                //modal: false,
                title: "Historial de Saldo de Bolsa Prepago",
                url: '~/Dashboard/PrepaidDataService/HistoricalBalance',
                data: {},
                width: 1231,
                height: 570,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {

                            var modalJQuery = args.event.view.$;

                            modalJQuery('#containerHistoricalBalance').HistoricalBalancePrepaid('GetExportHistoricalBalance');
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
        btnReloadService_click: function () {
            $('#contenedor').Prepaid('dataCustomerPre');
            Session.CUSTOMEROLD = (Session.ORIGINTYPE == 'PREPAID' ? '1' : '0');
            Session.MESSAGEPREPAID = (Session.ORIGINTYPE == 'PREPAID' ? '1' : '0');
            $('#contenedorDataCustomer').PrepaidDataCustomer('validateTelephoneCustomer');
        }
    };



    $.fn.DataServicePrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['validateItems'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('DataServicePrepaid'),
                options = $.extend({}, $.fn.DataServicePrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('DataServicePrepaid', data);
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

    $.fn.DataServicePrepaid.defaults = {
    }

    $('#containerDataService').DataServicePrepaid();

})(jQuery);


