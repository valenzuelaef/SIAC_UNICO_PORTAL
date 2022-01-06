(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidDataPaymentCollection.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , DetailAmountDisputedlg: $('#DetailAmountDisputedlg', $element)
            , StatusAccountlg: $('#StatusAccountlg', $element)
            , StatusAccountHRlg: $('#StatusAccountHRlg', $element)
            , AffiliationToDebitlg: $('#MethodPaymentdlg', $element)
            , PaymentCommitment: $("#PaymentCommitment", $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.DetailAmountDisputedlg.addEvent(that, 'click', that.DetailAmountDispute);
            controls.AffiliationToDebitlg.addEvent(that, 'click', that.AffiliationToDebit);
            controls.StatusAccountlg.addEvent(that, 'click', that.StatusAccount);
            controls.PaymentCommitment.addEvent(that, 'click', that.PaymentCommitment_click);
            controls.StatusAccountHRlg.addEvent(that, 'click', that.StatusAccountHR);
            this.ValidateOriginType()
        },
        ValidateOriginType: function () {

            var BtnStatusAccountHR = document.getElementById("StatusAccountHRlg");
            var BtnPaymentCommitment = document.getElementById("PaymentCommitment");

            if (Session.ORIGINTYPE == "HFC") {
                BtnStatusAccountHR.style.visibility = 'hidden';
                BtnPaymentCommitment.style.visibility = 'hidden';
            }
        },
        PaymentCommitment_click: function () {
            $.window.open({
                modal: false,
                title: "Compromiso de Pago",
                url: '~/Dashboard/PostpaidDataCollection/PaymentCommitment',
                data: { strIdSession: Session.IDSESSION, strIdCustomer: Session.DATACUSTOMER.CustomerID },
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
        DetailAmountDispute: function () {
            $.window.open({
                modal: false,
                title: "Detalle de Monto en Disputa",
                url: '~/Dashboard/PostpaidDataCollection/DetailAmountDispute',
                data: { strIdSession: Session.IDSESSION, CustomerId: Session.DATACUSTOMER.CustomerID },
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            }).maximize();
        },
        StatusAccountHR: function () {
            var that = this;
            $.window.open({
                modal: false,
                title: "Consulta Estado de Cuenta Hojas de Resumen (HR)",
                url: '~/Dashboard/PostpaidDataCollection/StatusAccountHR',
                //Se agrega plataforma y csIdPub INC000003284774
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strNameClient: Session.DATACUSTOMER.BusinessName, strNumberServices: Session.DATACUSTOMER.ValueSearch, strContactId: Session.DATACUSTOMER.Account, strPlatAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strCsIdPub: Session.DATACUSTOMER.csIdPub },
                width: 1024,
                height: 600,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            that.GetExportStatusAccountHR();
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            }).maximize();
        },
        GetExportStatusAccountHR: function () {
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oContract.strNameClient = Session.DATACUSTOMER.BusinessName;
            oContract.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oContract.strContactId = Session.DATACUSTOMER.Account;
            //Se agrega plataforma y csIdPub INC000003284774
            oContract.strPlatAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            oContract.strCsIdPub = Session.DATACUSTOMER.csIdPub;
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataCollection/AccountExportStatusAccountHR',
                data: JSON.stringify(oContract),
                success: function (path) {

                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=StatusAccountHR.xlsx";
                }
            });
        },
        AffiliationToDebit: function () {
            $.window.open({
                modal: false,
                title: "Consulta de Afiliación a Débito Automático",
                url: '~/Dashboard/PostpaidDataCollection/AffiliationToDebit',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, FlagPlat: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, csIdPub: Session.DATACUSTOMER.csIdPub },
                width: 1000,
                height: 400,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        StatusAccount: function () {

            $.window.open({
                modal: false,
                title: "Estado de Cuenta",
                url: '~/Dashboard/PostpaidDataCollection/StatusAccount',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strNameClient: Session.DATACUSTOMER.BusinessName, strNumberServices: Session.DATACUSTOMER.ValueSearch, strContactId: Session.DATACUSTOMER.Account },
                width: 1380,
                height: 700,
                maximizeBox: false,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#PostpaidStatusAccount').FormPostpaidStatusAccount('GetExportStatusAccount');
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            }).maximize();
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };
    $.fn.FormPostpaidDataPaymentCollection = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidDataPaymentCollection'),
                options = $.extend({}, $.fn.FormPostpaidDataPaymentCollection.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidDataPaymentCollection', data);
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
    $.fn.FormPostpaidDataPaymentCollection.defaults = {};
    $('#PostpaidDataPaymentCollection').FormPostpaidDataPaymentCollection();
})(jQuery);








