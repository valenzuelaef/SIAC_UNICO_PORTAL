(function ($, undefined) {
    'use strict'

    var Form = function ($element, options) {
        $.extend(this, $.fn.formPostDataAccount.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , btnAnotation: $('#btnAnotation', $element)
            , btnWarranty: $('#btnWarranty', $element)
            , btnCreditLimit: $('#btnCreditLimit', $element)
            , btnPinPuk: $('#btnPinPuk', $element)
            , btnSharedBag: $('#btnSharedBag', $element)
            , btnRelationshipPlans: $('#btnRelationshipPlans', $element)
            , btnSuspendedContract: $('#btnSuspendedContract', $element)
            , btnCuentaCliente: $('#btnCuentaCliente', $element)
            , btnRelationshipPlanHFCLTE: $('#btnRelationshipPlanHFCLTE', $element)
            , ContenedorPinPuk: $('#ContenedorPinPuk', $element)
            , btnAgreement: $('#btnAgreement', $element)
            , btnInstMasiva: $('#btnInstMasiva', $element)
            , ddlTipoBusqueda: $('#ddlTipoBusqueda', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnAnotation.addEvent(that, 'click', that.btnAnotation_click);
            controls.btnWarranty.addEvent(that, 'click', that.btnWarranty_click);
            controls.btnCreditLimit.addEvent(that, 'click', that.btnCreditLimit_click);
            controls.btnPinPuk.addEvent(that, 'click', that.btnPinPuk_click);
            controls.btnSharedBag.addEvent(that, 'click', that.btnSharedBag_click);
            controls.btnRelationshipPlans.addEvent(that, 'click', that.btnRelationshipPlans_click);
            controls.btnSuspendedContract.addEvent(that, 'click', that.btnSuspendedContract_click);
            controls.btnCuentaCliente.addEvent(that, 'click', that.btnCuentaCliente_click);
            controls.btnRelationshipPlanHFCLTE.addEvent(that, 'click', that.btnRelationshipPlanHFCLTE_click);
            controls.btnAgreement.addEvent(that, 'click', that.btnAgreement_click);
            controls.btnInstMasiva.addEvent(that, 'click', that.btnInstMasiva_click);
            that.render();
        },
        render: function () {
            var that = this,
                    controls = that.getControls();
            if (Session.DATACUSTOMER.ProductTypeText === "PostPago - DTH") {
                controls.btnPinPuk.hide();
                $("#lbl_PinPuk").hide();
            } else {
                controls.btnPinPuk.show();
                $("#lbl_PinPuk").show();
            }
            controls.btnAnotation.hide();
        },

        btnAgreement_click: function (send) {
            this.Agreement(send);
        },

        Agreement: function (send) {

           
            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataAccount/ExistAgreement_Origin',
                data: {},
                success: function (result) {
                    var StrUrlRedirect;
                    var strRuta = result.DataRuta;
                    StrUrlRedirect = strRuta + "paginas/siga_consulta_acuerdos.aspx?Criterio=C&Valor=" + Session.DATACUSTOMER.Account + "&MostrarMenu=0" + "&Usuario=" + Session.USERACCESS.login + "&codusuario=" + Session.USERACCESS.userId + "&sistema=SIACP";
                    console.log(StrUrlRedirect);
                    var w = 980;
                    var h = 650;
                    var leftScreen = (screen.width - w) / 2;
                    var topScreen = (screen.height - h) / 2;
                    var opciones = "directories=no,scrollbars=yes,status=yes,resizable=no,width=" + w + ",height=" + h + ",left=" + leftScreen + ",top=" + topScreen;
                    window.open(StrUrlRedirect, "", opciones);
                }


            });
        },

        btnAnotation_click: function () {
            
            $.window.open({
                modal: false,
                title: "Anotaciones",
                url: '~/Dashboard/PostpaidDataAccount/AccountAnnotation',
                data: { strIdSession: Session.IDSESSION, strType: "C" },
                width: 1231,
                height: 580,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnWarranty_click: function () {

            var strCustomer;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                strCustomer = Session.DATACUSTOMER.csIdPub;
            } else {
                strCustomer = Session.DATACUSTOMER.CustomerID;
            }

            $.window.open({
                modal: false,
                title: "Garantía",
                url: '~/Dashboard/PostpaidDataAccount/AccountWarranty',
                data: {
                    strIdSession: Session.IDSESSION,
                    strCustomerId: strCustomer,
                    strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                    strDocumentType: Session.DATACUSTOMER.DocumentType,
                    strAplication: Session.DATACUSTOMER.Application
                },
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
        btnInstMasiva_click: function () {
            
            $.window.open({
                modal: true,
                title: "Instantanea Masiva",
                url: '~/Management/MassiveInstant/Index',
                data: { strIdSession: Session.IDSESSION, strAplication: Session.DATACUSTOMER.Application },
                width: 1024,
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

        btnCreditLimit_click: function () {
            
            $.window.open({
                modal: false,
                title: "Gestión Límite de Crédito",
                url: '~/Dashboard/PostpaidDataAccount/AccountCreditLimit',
                data: { strIdSession: Session.IDSESSION, srtAccount: Session.DATACUSTOMER.Account, strBusinessName: Session.DATACUSTOMER.BusinessName, strCustomerId: Session.DATACUSTOMER.CustomerID, strAplication: Session.DATACUSTOMER.Application },
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

        btnPinPuk_click: function () {
            
            var that = this;
            $.window.open({
                modal: false,
                title: "Pin y Puk Business",
                url: '~/Dashboard/PostpaidDataAccount/AccountPinPuk',
                data: { strIdSession: Session.IDSESSION, srtAccount: Session.DATACUSTOMER.Account, strBusinessName: Session.DATACUSTOMER.BusinessName },
                width: 1231,
                height: 570,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#ContentPostpaidAccountPinPuk').formAccountPinPuk('GetExportPinPuk');
                        }
                    },
                    Constancia: {
                        click: function (sender, args) {
                            that.GetExportPinPukConstancy(that.strUrl);


                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },

        btnSharedBag_click: function () {
            var that = this,
                controls = that.getControls(),
                strtypeSearch = controls.ddlTipoBusqueda.val(),
                strCustomerType = Session.DATACUSTOMER.objPostDataAccount.CustomerType;
           
            if (strCustomerType == "Consumer") {
                $.window.open({
                    modal: false,
                    title: "Bolsa Compartida",
                    url: '~/Dashboard/PostpaidDataAccount/AccountSharedBag',
                    data: { strIdSession: Session.IDSESSION, srtAccount: Session.DATACUSTOMER.Account, srtValueSearch: Session.DATACUSTOMER.ValueSearch, strtypeSearch: strtypeSearch, strCustomerId: Session.DATACUSTOMER.CustomerID, strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType },
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
            }
            else {
                showAlert("El cliente no es un Tipo Postpago Consumer", "Mensaje Bolsa Compartida");
            }
        },
        btnRelationshipPlans_click: function () {
            Session.DATACUSTOMER.BannerMessage = "";           
            $.window.open({
                type: 'POST',
                modal: false,
                title: "Relación de Planes",
                url: '~/Dashboard/PostpaidDataAccount/AccountRelationPlan',
                data: { strIdSession: Session.IDSESSION, objCustomer: Session.DATACUSTOMER },
                width: 1450,
                height: 550,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                           var modalJQuery = args.event.view.$;
                           modalJQuery('#ContentPostpaidAccountRelationPlan').formAccountRelationPlan('GetExportRelationPlan');
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

        btnSuspendedContract_click: function () {
           
            $.window.open({
                modal: false,
                title: "Contrato Suspendido",
                url: '~/Dashboard/PostpaidDataAccount/AccountSuspendedContract',
                type: 'POST',
                data: { strIdSession: Session.IDSESSION },
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


        btnCuentaCliente_click: function () {
            
            $.window.open({
                modal: false,
                title: "Cuenta Cliente",
                url: '~/Dashboard/PostpaidDataAccount/AccountSubAccount',
                type: 'POST',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strAccountId: Session.DATACUSTOMER.Account },
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

   
        GetExportPinPukConstancy: function (strUrl) {
            
            var prm = '?strIdSession=' + Session.IDSESSION + '&srtAccount=' + Session.DATACUSTOMER.Account + '&strBusinessName=' + Session.DATACUSTOMER.BusinessName
            var view = '/Dashboard/Format/AccountPinPukConstancy' + prm ;
            var ventimp = window.open(view, 'popimpr', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=850, height=650');
            ventimp.focus();
            $(ventimp.document).ready(function () {
                ventimp.window.focus();
                ventimp.document.close();
                ventimp.window.print();
            });

        },


        btnRelationshipPlanHFCLTE_click: function () {
           
            $.window.open({
                modal: false,
                title: "Listado Servicios",
                url: '~/Dashboard/PostpaidDataAccount/AccountRelationPlanHFCLTE',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strAplication: Session.DATACUSTOMER.Application, strContractID: Session.DATACUSTOMER.ContractID, strState: Session.DATACUSTOMER.StateAgreement, strPlan: Session.DATASERVICE.Plan, strAddress: Session.DATACUSTOMER.InvoiceAddress, strTelephony: Session.DATASERVICE.TelephonyValue, strInternet: Session.DATASERVICE.InternetValue, strCable: Session.DATASERVICE.CableValue, strPlataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
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
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.formPostDataAccount = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formPostDataAccount'),
                options = $.extend({}, $.fn.formPostDataAccount.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formPostDataAccount', data);
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

    $.fn.formPostDataAccount.defaults = {
    }
    $('#ContentPostpaidDataAccount').formPostDataAccount();
})(jQuery);
