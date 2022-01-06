(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidDataBilling.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblInternationalRoamingDetail: $('#tblInternationalRoamingDetail', $element)
            , strInvoiceNumber: $('#hidInvoiceNumber', $element)
            , btnListNationalLongDistancedlg: $('#btnListNationalLongDistancedlg', $element)
            , btnListInternationalLongDistancedlg: $('#btnListInternationalLongDistancedlg', $element)
            , btnInternationalRoamingDetaildlg: $('#btnInternationalRoamingDetaildlg', $element)
            , btnDebtDetaildlg: $('#btnDebtDetaildlg', $element)
            , btnAdditionalLocalTrafficDetaildlg: $('#btnAdditionalLocalTrafficDetaildlg', $element)
            , btnConsumeLocalTrafficDetaildlg: $('#btnConsumeLocalTrafficDetaildlg', $element)
            , ListOtrosCargosAbonosdlg: $('#ListOtrosCargosAbonosdlg', $element)
            , ListServiciosAdicionalesdlg: $('#ListServiciosAdicionalesdlg', $element)
            , btnHistoryInvoice: $('#btnHistoryInvoice', $element)
            , btnListHistoricoEntregadlg: $('#btnListHistoricoEntregadlg', $element)
            , btnFixedChargedlg: $('#btnFixedChargedlg', $element)
            , btnShowInvoice: $('#btnShowInvoice', $element)
            , btnHistoryHR: $('#btnHistoryHR', $element)
            , PostpaidDueDocumentNumber: $('#PostpaidDueDocumentNumber', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = that.getControls();

            controls.btnAdditionalLocalTrafficDetaildlg.addEvent(that, 'click', that.btnAdditionalLocalTrafficDetail_click);
            controls.btnConsumeLocalTrafficDetaildlg.addEvent(that, 'click', that.btnConsumeLocalTrafficDetail_click);
            controls.btnDebtDetaildlg.addEvent(that, 'click', that.btnDebtDetail_click);
            controls.btnHistoryInvoice.addEvent(that, 'click', that.btnHistoryInvoice_click);
            controls.btnInternationalRoamingDetaildlg.addEvent(that, 'click', that.btnInternationalRoamingDetail_click);
            controls.btnListInternationalLongDistancedlg.addEvent(that, 'click', that.btnListInternationalLongDistance_click);
            controls.btnListNationalLongDistancedlg.addEvent(that, 'click', that.btnListNationalLongDistance_click);
            controls.ListOtrosCargosAbonosdlg.addEvent(that, 'click', that.ListOtrosCargosAbonos_click);
            controls.ListServiciosAdicionalesdlg.addEvent(that, 'click', that.ListServiciosAdicionales_click);
            controls.btnListHistoricoEntregadlg.addEvent(that, 'click', that.btnListHistoricoEntrega_click);
            controls.btnFixedChargedlg.addEvent(that, 'click', that.btnFixedCharge_click);
            controls.btnShowInvoice.addEvent(that, 'click', that.btnShowInvoice_click);
            controls.btnHistoryHR.addEvent(that, 'click', that.btnHistoryHR_click);



            that.render();
        },
        render: function () {
        },
        btnAdditionalLocalTrafficDetail_click: function myfunction() {

            $.window.open({
                modal: false,
                title: "Tráfico Local Adicional",
                url: '~/Dashboard/PostpaidDataBilling/BillingAdditionalLocalTrafficDetail',
                type: 'POST',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnConsumeLocalTrafficDetail_click: function myfunction() {

            $.window.open({
                modal: false,
                title: "Tráfico Local a Consumo",
                url: '~/Dashboard/PostpaidDataBilling/BillingConsumeLocalTrafficDetail',
                type: 'POST',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        getTypeDocument: function () {
            switch (Session.DATACUSTOMER.DocumentNumber.length) {
                case 8:
                    return 1;
                case 11:
                    return 6;
            }
        },
        btnDebtDetail_click: function myfunction() {
            /**
            Version Nueva
            */

            $.window.open({
                modal: false,
                async: false,
                title: "Consulta Deuda Número Documento",
                url: '~/Dashboard/PostpaidDataCollection/DueNumberDocument',
                width: 1231,
                height: 600,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#PostpaidDueDocumentNumber').FormPostpaidDueDocumentNumber('GetExportDueNumber');

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

        btnHistoryInvoice_click: function myfunction() {

            if (Session.DATACUSTOMER.objPostDataAccount) {
                if (Session.DATACUSTOMER.objPostDataAccount.ResponsiblePayment == '') {
                    showAlert('El cliente no es responsable de Pago');
                    return;
                }
            }
            $.window.open({
                modal: false,
                title: "Detalle de Facturas Anteriores",
                url: '~/Dashboard/PostpaidDataBilling/BillingHistoryInvoice',
                type: 'post',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        },
                        id: "btnCloseHistoryBilling"
                    }
                }
            });

        },
        btnInternationalRoamingDetail_click: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.window.open({
                modal: false,
                title: "Detalle de Roaming Internacional",
                url: '~/Dashboard/PostpaidDataBilling/BillingInternationalRoamingDetail',
                type: 'post',
                data: { strIdSession: Session.IDSESSION, strInvoiceNumber: controls.strInvoiceNumber.val() },
                width: 1231,
                height: 500,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnListInternationalLongDistance_click: function myfunction() {


            $.window.open({
                modal: false,
                title: "Detalle Larga Distancia Internacional",
                url: '~/Dashboard/PostpaidDataBilling/BillingDetailLongDistance',
                type: 'post',
                data: { strTypeApplication: Session.DATACUSTOMER.Application, strIndicator: "International" },
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnListNationalLongDistance_click: function myfunction() {

            $.window.open({
                modal: false,
                title: "Detalle de llamada de Larga Distancia Nacional",
                url: '~/Dashboard/PostpaidDataBilling/BillingDetailLongDistance',
                type: 'post',
                data: { strTypeApplication: Session.DATACUSTOMER.Application, strIndicator: "National" },
                width: 1231,
                height: 500,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        ListOtrosCargosAbonos_click: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.window.open({
                modal: false,
                title: "Detalle de Otros Cargos y Abonos",
                url: '~/Dashboard/PostpaidDataBilling/BillingOtherCharges',
                data: { strIdSession: Session.IDSESSION, intGroupBox: '1', strInvoiceNumber: controls.strInvoiceNumber.val() },
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        ListServiciosAdicionales_click: function myfunction() {

            $.window.open({
                modal: false,
                title: "Detalle Servicios TIM",
                url: '~/Dashboard/PostpaidDataBilling/BillingPrintTimServiceDetail',
                type: 'post',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        getButtons: function () {
            var btn;
            var that = this;
            if (Session.ORIGINTYPE == 'HFC') {
                btn = {

                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }

                }
            } else {
                btn = {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;

                            that.GetExportHitoricDelivery(modalJQuery('#FormHistoricDelivery').FormHistoricDelivery('getHistoricDelivery'));
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }

                }
            }
            return btn;
        },
        btnListHistoricoEntrega_click: function myfunction() {


            $.window.open({
                modal: false,
                title: "Detalle de Distribución de Recibos",
                url: '~/Dashboard/PostpaidDataBilling/BillingHistoricDelivery',
                type: 'post',
                data: {},
                width: 1231,
                height: 500,
                order: [1, "desc"],
                buttons: this.getButtons()
            });
        },
        GetExportHitoricDelivery: function (lstHistoricDelivery) {


            if (lstHistoricDelivery.length > 0) {
                var strUrlModal = '~/Dashboard/PostpaidDataBilling/BillingExportHistoricDelivery';
                var strUrlResult = '~/Dashboard/Home/DownloadExcel';
                var data = { strIdSession: Session.IDSESSION, strCustomerID: Session.DATACUSTOMER.CustomerID, strTelephone: Session.DATACUSTOMER.ValueSearch, flagPlataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT };

                $.app.ajax({
                    type: 'POST',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    url: strUrlModal,
                    data: JSON.stringify(data),
                    success: function (path) {

                        window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=HistoricDelivery.xlsx";
                    }
                });
            } else {
                showAlert('No hay registros a exportar', 'Alerta');
            }
        },
        btnFixedCharge_click: function myfunction() {

            $.window.open({
                modal: false,
                title: "Cargo Fijo",
                url: '~/Dashboard/PostpaidDataBilling/BillingFixedCharge',
                type: 'post',
                data: {},
                width: 1231,
                height: 600,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },

        btnShowInvoice_click: function myfunction(sender, args) {
            var valFlagBill = sender.data('flagbill');
            var valNroinvoice = sender.data('nroinvoice');
            var varCUSTOMERID;
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "ASIS") {
                varCUSTOMERID = Session.DATACUSTOMER.CustomerID;
            } else if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE" && Session.DATACUSTOMER.flagConvivencia == "1") {
                varCUSTOMERID = Session.DATACUSTOMER.CustomerID;
            } else if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE" && Session.DATACUSTOMER.flagConvivencia == "0") {
                varCUSTOMERID = Session.DATACUSTOMER.csIdPub;
            }

            var valFilePath = sender.data('path');
            var valFileName = sender.data('filename');

            if (valFlagBill == "1") {
                var url = '~/Dashboard/PostpaidDataBilling/ExistFileTOBE';
                $.app.ajax({
                    type: 'GET',
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    url: url,
                    data: { strFilePath: valFilePath, strFileName: valFileName, strIdSesion: Session.IDSESSION, strcustomerId: varCUSTOMERID, strNroInvoice: valNroinvoice },
                    success: function (result) {
                        if (result.Exist == true) {
                            try {
                                var url = '/Dashboard/PostpaidDataBilling/ShowInvoiceTOBE';
                                window.open(url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION + "&strcustomerId=" + varCUSTOMERID + "&strNroInvoice=" + valNroinvoice, "FACTURA_ELECTRÓNICA", "");
                            } catch (e) {
                                showAlert('El idOnBase no existe');
                            }
                        }
                        else {
                            showAlert('El archivo no existe.');
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        console.log('Error Peticion ajax');
                        showAlert('El archivo no existe.');
                    }
                });
            } else {
                console.log('valor flagbill <> 1')
                showAlert("El archivo no existe.VerificarNro Recibo");
            }
        },
        btnHistoryHR_click: function myfunction() {


            $.window.open({
                modal: false,
                title: "Histórico de HR",
                url: '~/Dashboard/PostpaidDataBilling/BillingHistoryHR',
                type: 'post',
                data: {},
                width: 1024,
                height: 650,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.FormPostpaidDataBilling = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getControls'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidDataBilling'),
                options = $.extend({}, $.fn.FormPostpaidDataBilling.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidDataBilling', data);
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

    $.fn.FormPostpaidDataBilling.defaults = {
    }
    $('#FormPostpaidDataBilling').FormPostpaidDataBilling();
})(jQuery);


