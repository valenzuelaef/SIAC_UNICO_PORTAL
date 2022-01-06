
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , TabsDataPost: $('#TabsDataPost', $element)
          , divPostCustomerInfo: $('#divPostCustomerInfo', $element)
          , postAccountInfo: $('#PostAccountInfo', $element)
          , marquee: $('marquee', $element)
          , divPostAccountInfo: $('#divPostAccountInfo', $element)
          , postLineInfo: $('#PostLineInfo', $element)
          , divPostLineInfo: $('#divPostLineInfo', $element)
          , postBillingInfo: $('#PostBillingInfo', $element)
          , divPostBillingInfo: $('#divPostBillingInfo', $element)
          , postPaymentInfo: $('#PostPaymentInfo', $element)
          , divPostPaymentInfo: $('#divPostPaymentInfo', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.marquee.addEvent(that, 'mouseover', that.marquee_mouseover)
                            .addEvent(that, 'mouseout', that.marquee_mouseout);
            controls.postAccountInfo.addEvent(that, 'click', that.postAccountInfo_click);
            controls.postLineInfo.addEvent(that, 'click', that.postLineInfo_click);
            controls.postBillingInfo.addEvent(that, 'click', that.postBillingInfo_click);
            controls.postPaymentInfo.addEvent(that, 'click', that.postPaymentInfo_click);

            that.render();

        },
        render: function () {

            this.dataCustomerPost();


        },

        calculateDivHeight: function (send) {
            $('#scrollbudyPost').height(window.innerHeight);
        },
        account: function () {

            var that = this,
                controlsAccount = that.setControlsAccount(),
                controls = $.extend(that.getControls(), controlsAccount),
                oCustomer = Session.DATACUSTOMER,
                oAccount = Session.DATACUSTOMER.objPostDataAccount;

            var jsonDataCustomer = JSON.stringify(oCustomer, function (key, value) {
                if (value == null)
                    return "";
                return value;
            });

            var jsonDataAcount = JSON.stringify(oAccount, function (key, value) {
                if (value == null)
                    return "";
                return value;
            });

            oCustomer = JSON.parse(jsonDataCustomer);
            oAccount = JSON.parse(jsonDataAcount);



            controls.lblPost_CustomerType.text(oAccount.CustomerType);
            controls.lblPost_Modality.text(oCustomer.origen);
            
			
			//INICIATIVA-616
            if (oAccount.plataformaAT == "TOBE") {
                controls.lblPost_Segment.text(oCustomer.Segment2);
            }
            else {
                controls.lblPost_Segment.text(oAccount.Segment);
            }
			
            controls.lblPost_BillingCycle.text(oAccount.BillingCycle);

            controls.lblPost_AccountStatus.text(oAccount.AccountStatus);
            controls.lblPost_CreditLimit.text(oAccount.CreditLimit);
            controls.lblPost_LineTotal.text(oAccount.TotalLines);
            controls.lblPost_SubAccountsTotal.text(oAccount.TotalAccounts);
            controls.lblPost_ActivationDate.text(oAccount.ActivationDate);
            controls.chkPost_ResponsiblePayment.attr('checked', oAccount.ResponsiblePayment == "X" ? true : false);
            controls.lblPost_Consultant.text(oAccount.Consultant);
            controls.lblPost_SaldoCreditLimit.text(oAccount.SaldoCreditLimit);
            controls.lblPost_SaldoCreditLimitTOBE.text(oAccount.SaldoCreditLimit);
            if (oAccount.plataformaAT == "TOBE") {
                var strCsIdPub = Session.DATACUSTOMER.csIdPub;
                var nuevoCust = "";
                var posicionCortar = strCsIdPub.indexOf("CUST");
                if (posicionCortar >= 0) {
                    nuevoCust = strCsIdPub.replace("CUST", "9");
                    controls.lblCod_Pago.text(nuevoCust);
                } else if (Session.DATACUSTOMER.flagConvivencia=="1") {
                    controls.lblCod_Pago.text(Session.DATACUSTOMER.CustomerID);
                }

              
            }
            else {
                controls.lblCod_Pago.text(Session.DATACUSTOMER.CustomerID);
            }


            if (oCustomer.Application != "HFC" && oCustomer.Application != "LTE") {
                controls.btnAccountPlan.remove();
                controls.trPostBagShareTOBE.remove();

            } else {

                controls.trPostLineService_PinPuk.remove();
                controls.trPostBagShare.remove();
                controls.trPostSubAccountsTotal.remove();

                if (oAccount.plataformaAT != "TOBE") {
                    controls.trPostBagShareTOBE.remove();
                }

                controls.btnSuspendedContract.remove();
                controls.btnRelationshipPlans.remove();
                controls.btnAgreement.remove();
            }

        },
        customer: function () {
            var that = this,
                controlsCustomer = that.setControlsCustomer(),
                controls = $.extend(that.getControls(), controlsCustomer),
                oCustomer = Session.DATACUSTOMER;
            
            that.setStatusFullClaroClient();

            controls.lblPost_BusinessName.text(oCustomer.BusinessName);
            if (oCustomer.DNIRUC.length == 11) {
                if (oCustomer.DNIRUC.substring(0, 2) == "10") {
                    controls.lblPost_FullName.text(oCustomer.FullName);
                } else {
                    if ((oCustomer.CustomerContact == "")) {
                        controls.lblPost_FullName.text(oCustomer.FullName);
                    } else {
                        controls.lblPost_FullName.text(oCustomer.CustomerContact);
                    }
                }
            } else {
                controls.lblPost_FullName.text(oCustomer.FullName);
            }
            controls.lblPost_DocumentNumber.text(oCustomer.DNIRUC);
            controls.lblPost_Account.text(oCustomer.Account);
            controls.lblPost_LegalAgent.text(oCustomer.LegalAgent);
            controls.lblPost_CustomerID.text(oCustomer.CustomerID);
            controls.lblPost_PhoneReference1.text(oCustomer.PhoneContact);
            //controls.lblPost_PhoneReference2.text(oCustomer.PhoneReference);
            controls.lblPost_Email.text(oCustomer.Email);
            controls.lblPost_Segment2.text(oCustomer.Segment2);
            controls.lblPost_Reference.text(oCustomer.Reference);
            controls.lblPost_Departament.text(oCustomer.Departament);
            controls.lblPost_Province.text(oCustomer.Province);
            controls.lblPost_District.text(oCustomer.District);
            controls.lblPost_InvoiceAddress.text(oCustomer.InvoiceAddress);
            controls.lblPost_InvoiceCode.text(oCustomer.InvoiceCode);
            controls.lblPost_PlaneCode.text(oCustomer.PlaneCode);
            controls.lblPost_CodeCenterPopulate.text(((oCustomer.PlaneCodeInstallation == "null") ? "" : oCustomer.PlaneCodeInstallation));
            controls.lblPost_HubCode.text(oCustomer.HubCode);
            controls.lblPost_DescriptionCenterPopulate.text(oCustomer.DescriptionCenterPopulate);
            controls.checkAppMiclaro.attr('checked', oCustomer.IsAppMiclaro);
            controls.checkAppMiclaro.attr('disabled', 'disabled');
            controls.lblVersionAppMiClaro.text(oCustomer.AppMiclaroVersion);
            controls.lblFechaUltTranAppMiClaro.text(oCustomer.AppMiClaroLastDate);

            if (Session.DATACUSTOMER.PlaneCodeInstallation.toUpperCase().indexOf('-F') > -1) {
                controls.lblPost_ProductType.text("PostPago - FTTH");
            }
            else {
                controls.lblPost_ProductType.text(oCustomer.ProductTypeText);
            }

            Session.TYPESERVICE = oCustomer.ProductTypeText;


            if (oCustomer.BannerMessage != '') {
                controls.lblPost_BannerMessage.append(oCustomer.BannerMessage);
            } else {
                $("#alertInfoPostpaid").css("display", "none");
            }


            if (oCustomer.Application != "HFC" && oCustomer.Application != "LTE") {
                controls.trPostPlaneCode.remove();
                controls.trPostHubCode.remove();

            } else {

                controls.btnPost_DireccionCustomer.remove();

            }
        },
        dataCustomerPost: function () {
            var that = this,
                controls = this.getControls();

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataCustomer',
                container: controls.divPostCustomerInfo,
                success: function (response) {

                    $.app.ajax({
                        type: 'POST',
                        url: '~/Dashboard/Postpaid/DataAccount',
                        container: controls.divPostAccountInfo,
                        success: function (result) {
                            that.postPago();
                        },
                        error: function (ex) {
                            controls.divPostAccountInfo.showMessageErrorLoading($.app.const.messageErrorLoading);

                        }
                    });

                },
                error: function (ex) {
                    controls.divPostCustomerInfo.showMessageErrorLoading($.app.const.messageErrorLoading);

                }
            });
        },

        dataServiceContent: function () {
            $.unblockUI();
            var that = this,
                controls = that.getControls();
            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "iddivPostLineInfo";
            that.objError.funct = that.postLineInfo_click;
            that.objError.that = that;
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataServiceContent',
                async: true,
                container: controls.divPostLineInfo,
                data: {
                    strContratoID: Session.DATACUSTOMER.ContractID,
                    strApplication: Session.DATACUSTOMER.Application,
                    strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                    strDocumentType: Session.DATACUSTOMER.DocumentType,
                    strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                    strModality: Session.DATACUSTOMER.Modality,
                    strIsLTE: Session.DATACUSTOMER.IsLTE,
                    strIdSession: Session.IDSESSION,
                    strphone: Session.DATACUSTOMER.CellPhone,
                    strphonespeed: Session.DATACUSTOMER.Telephone,
                    plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                    strCustomerId: Session.DATACUSTOMER.CustomerID,
                    flagconvivencia: Session.DATACUSTOMER.flagConvivencia,
                    coIdPub: Session.DATACUSTOMER.coIdPub

                },
                complete: function () {
                    that.templateDataService();
                },
                success: function (response) {
                    Session.DATASERVICE = response.data;
                    Session.TELEPHONE = Session.DATASERVICE.CellPhone;
                    Session.CONTRATOID = Session.DATACUSTOMER.ContractID;
                    Session.CUSTOMERID = Session.DATACUSTOMER.CustomerID;
                    Session.TYPESERVICE = Session.DATASERVICE.TypeProduct;
                },
                error: function (ex) {
                    Session.DATASERVICE = null;
                    //$.app.error({
                    //    id: 'divPostLineInfo',
                    //    message: ex,
                    //    click: function () { that.dataServiceContent() }
                    //});
                    controls.divPostLineInfo.showMessageErrorLoading(that.objError);
                }
            });
        },
        getCustomer: function () {
            var that = this,
                controls = that.getControls(),
                application = Session.DATACUSTOMER.Application;
            var value = "";
            if (application == "HFC" || application == "LTE") {
                value = Session.DATACUSTOMER.ContractID;
            } else {
                value = Session.DATACUSTOMER.CellPhone;
            }

            blockUI('#DatosClientecollapse');
            blockUI('#DatosFacturacioncollapse');
            var controlsCustomer = that.getControls();
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetCustomer',
                data: {
                    strValue: value,
                    strApplication: application,
                    strIdSession: Session.IDSESSION
                },
                success: function (res) {

                    $.app.ajax({
                        type: 'POST',
                        url: '~/Dashboard/Postpaid/DataCustomer',
                        container: controlsCustomer.divPostCustomerInfo,
                        success: function (response) {

                            $.app.ajax({
                                type: 'POST',
                                url: '~/Dashboard/Postpaid/DataAccount',
                                container: controls.divPostAccountInfo,
                                success: function (result) {
                                    Session.DATACUSTOMER = res.data;
                                    that.customer();
                                },
                                error: function (ex) {
                                    controls.divPostAccountInfo.showMessageErrorLoading($.app.const.messageErrorLoading);

                                }
                            });

                        },
                        error: function (ex) {
                            controls.divPostCustomerInfo.showMessageErrorLoading($.app.const.messageErrorLoading);

                        }
                    });


                },
                error: function (ex) {
                    controls.divPostCustomerInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        marquee_mouseover: function (send, args) {
            send.attr('scrollamount', 0);
        },
        marquee_mouseout: function (send, args) {
            send.attr('scrollamount', 5);
        },
        postAccountInfo_click: function (send, args) {
            var that = this,
                controls = that.getControls();
            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "iddivPostAccountInfo";
            that.objError.funct = that.postAccountInfo_click;
            that.objError.that = that;
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataAccount',
                container: controls.divPostAccountInfo,
                success: function (response) {
                    that.account();
                },
                error: function (ex) {

                    controls.divPostAccountInfo.showMessageErrorLoading(that.objError);
                }
            });
        },
        postLineInfo_click: function () {

            var that = this;

            that.dataServiceContent(false);
        },
        templateDataService: function () {
            var that = this,
                controls = that.getControls();
            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "iddivPostLineInfo";
            that.objError.funct = that.postLineInfo_click;
            that.objError.that = that;
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataService',
                container: controls.divPostLineInfo,
                success: function (response) {
                    $('#containerDataService').PostPaidDataService('setDataService');
                },
                error: function (ex) {

                    controls.divPostLineInfo.showMessageErrorLoading(that.objError);

                }
            });
        },
        postBillingInfo_click: function () {
            var that = this,
                controls = that.getControls();

            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "iddivPostBillingInfo";
            that.objError.funct = that.postBillingInfo_click;
            that.objError.that = that;
            console.log('postBillingInfo_click: ~/Dashboard/Postpaid/DataBilling');
            var n = {
                strCustomerID: Session.DATACUSTOMER.CustomerID,
                strApplication: Session.DATACUSTOMER.Application,
                strIdSession: Session.IDSESSION,
                strContratoID: Session.DATACUSTOMER.ContractID,
                strInvoiceNumber: "",
                plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                strcsIdPub: Session.DATACUSTOMER.csIdPub,
                strbmIdPub: Session.DATACUSTOMER.objPostDataAccount.bmIdPub,
                coIdPub: Session.DATACUSTOMER.coIdPub
            };
            console.log('parametros de entrada:');
            console.log(n);
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataBilling',
                container: controls.divPostBillingInfo,
                data: {
                    strCustomerID: Session.DATACUSTOMER.CustomerID,
                    strApplication: Session.DATACUSTOMER.Application,
                    strIdSession: Session.IDSESSION,
                    strContratoID: Session.DATACUSTOMER.ContractID,
                    strInvoiceNumber: "",
                    plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                    strcsIdPub: Session.DATACUSTOMER.csIdPub,
                    strbmIdPub: Session.DATACUSTOMER.objPostDataAccount.bmIdPub,
                    coIdPub: Session.DATACUSTOMER.coIdPub
                },
                error: function (ex) {

                    controls.divPostBillingInfo.showMessageErrorLoading(that.objError);
                }
            });
        },
        postPaymentInfo_click: function () {
            var that = this,
                controls = that.getControls();
            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "iddivPostPaymentInfo";
            that.objError.funct = that.postPaymentInfo_click;
            that.objError.that = that;
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/DataPaymentCollection',
                container: controls.divPostPaymentInfo,
                data: { csIdPub: !Session.DATACUSTOMER.csIdPub ? Session.DATACUSTOMER.CustomerID : Session.DATACUSTOMER.csIdPub, strCustomerId: Session.DATACUSTOMER.CustomerID, strPaymentMethod: Session.DATACUSTOMER.PaymentMethod, strIdSession: Session.IDSESSION, strCodePlanTariff: Session.DATACUSTOMER.CodePlanTariff, plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
                error: function (ex) {
                    // controls.divPostPaymentInfo.showMessageErrorLoading(that.objError);
                    controls.divPostPaymentInfo.showMessageErrorLoading(that.objError);
                }
            });
        },
        postPago: function () {
            var that = this;

            that.customer();
            that.account();
        },
        setControlsAccount: function () {
            var controls = this.getControls(),
                oControlsAccount = {
                    chkPost_ResponsiblePayment: $('#chkPost_ResponsiblePayment', controls.form)
                  , lblPost_CustomerType: $('#lblPost_CustomerType', controls.form)
                  , lblPost_Modality: $('#lblPost_Modality', controls.form)
                  , lblPost_Segment: $('#lblPost_Segment', controls.form)
                  , lblPost_BillingCycle: $('#lblPost_BillingCycle', controls.form)

                  , lblPost_AccountStatus: $('#lblPost_AccountStatus', controls.form)
                  , lblPost_CreditLimit: $('#lblPost_CreditLimit', controls.form)
                  , lblPost_LineTotal: $('#lblPost_LineTotal', controls.form)
                  , lblPost_SubAccountsTotal: $('#lblPost_SubAccountsTotal', controls.form)
                  , lblPost_ActivationDate: $('#lblPost_ActivationDate', controls.form)


                  , trPostLineService_PinPuk: $('#trPostLineService_PinPuk', controls.form)
                  , trPostBagShare: $('#trPostBagShare', controls.form)

                  , trPostBagShareTOBE: $('#trPostBagShareTOBE', controls.form)

                  , trPostSubAccountsTotal: $('#trPostSubAccountsTotal', controls.form)
                  , btnAccountPlan: $('#btnRelationshipPlanHFCLTE', controls.form)
                  , btnRelationshipPlans: $('#btnRelationshipPlans', controls.form)
                  , btnSuspendedContract: $('#btnSuspendedContract', controls.form)
                  , btnAgreement: $('#btnAgreement', controls.form)
                  , lblPost_Consultant: $('#lblPost_Consultant', controls.form)
                  , lblPost_SaldoCreditLimit: $('#lblPost_SaldoCreditLimit', controls.form)

                  , lblPost_SaldoCreditLimitTOBE: $('#lblPost_SaldoCreditLimitTOBE', controls.form)

                  , lblCod_Pago: $('#lblCod_Pago', controls.form)
                }

            return oControlsAccount;
        },
        setControlsCustomer: function () {
            var controls = this.getControls(),
                oControlsCustomer = {
                    // chkPost_Membership: $('#chkPost_Membership', controls.form)
                    lblPost_BusinessName: $('#lblPost_BusinessName', controls.form)
                     , lblPost_FullName: $('#lblPost_FullName', controls.form)
                     , lblPost_DocumentNumber: $('#lblPost_DocumentNumber', controls.form)
                     , lblPost_Account: $('#lblPost_Account', controls.form)
                     , lblPost_LegalAgent: $('#lblPost_LegalAgent', controls.form)
                     , lblFullClaroClient: $('#lblFullClaroClient', controls.form)
                     , lblPost_CustomerID: $('#lblPost_CustomerID', controls.form)
                     , lblPost_PhoneReference1: $('#lblPost_PhoneReference1', controls.form)
                     //, lblPost_PhoneReference2: $('#lblPost_PhoneReference2', controls.form)
                     , lblPost_Email: $('#lblPost_Email', controls.form)
                     , lblPost_Segment2: $('#lblPost_Segment2', controls.form)
                     , lblPost_Reference: $('#lblPost_Reference', controls.form)
                     , lblPost_Departament: $('#lblPost_Departament', controls.form)
                     , lblPost_Province: $('#lblPost_Province', controls.form)
                     , lblPost_District: $('#lblPost_District', controls.form)
                     , lblPost_InvoiceAddress: $('#lblPost_InvoiceAddress', controls.form)
                     , lblPost_InvoiceCode: $('#lblPost_InvoiceCode', controls.form)
                     , lblPost_PlaneCode: $('#lblPost_PlaneCode', controls.form)
                     , lblPost_CodeCenterPopulate: $('#lblPost_CodeCenterPopulate', controls.form)
                     , lblPost_HubCode: $('#lblPost_HubCode', controls.form)
                     , lblPost_DescriptionCenterPopulate: $('#lblPost_DescriptionCenterPopulate', controls.form)
                     , lblPost_BannerMessage: $('#lblPost_BannerMessage', controls.form)
                     , trPostPlaneCode: $('#trPostPlaneCode', controls.form)
                     , trPostHubCode: $('#trPostHubCode', controls.form)
                     , trPostSegmento: $('#trPostSegmento', controls.form)
                     , lblPost_ProductType: $('#lblPost_ProductType', controls.form)
                     , btnPost_DireccionCustomer: $('#btnPost_DireccionCustomer', controls.form)
                     , checkAppMiclaro: $('#checkAppMiclaro', controls.form)
                     , lblVersionAppMiClaro: $('#lblVersionAppMiClaro', controls.form)
                     , lblFechaUltTranAppMiClaro: $('#lblFechaUltTranAppMiClaro', controls.form)
                };

            return oControlsCustomer;
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        setStatusFullClaroClient: function () {
            $.unblockUI();
           
            var that = this,
            controlsCustomer = that.setControlsCustomer(),
            controls = $.extend(that.getControls(), controlsCustomer),
            oCustomer = Session.DATACUSTOMER;
            var strMessageClient = 'No cumple requisito';
            var strKeyName = "strKeyDocValue";
            var plataformaAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            $.app.ajax({
                type: 'POST',
                async: false,
                url: '~/Dashboard/Home/getKeyValue',
                data: { strKeyName: strKeyName, strIdSession: Session.IDSESSION },
                beforeSend: function () {
                    controls.lblFullClaroClient.html("");
                    controls.lblFullClaroClient.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                },
                success: function (data) {
                    var nroDoc = (oCustomer == undefined ? "" : (oCustomer.DNIRUC == "" ? (oCustomer.DocumentNumber == "" ? "" : oCustomer.DocumentNumber) : oCustomer.DNIRUC));
                    var typeDoc = oCustomer.DocumentType;

                    var strTypeDoc = "";
                    var argKeyDocValue = data.data;
                    var argType = argKeyDocValue.split("|");
                    var arrTypeDoc = [];
                    if (argType.length > 0) {
                        argType.forEach(function (element) {
                            var argItem = element.split(",");
                            if (argItem.length > 0) {
                                arrTypeDoc.push({
                                    key: argItem[0],
                                    value: argItem[1]
                                });

                                if (argItem[1].toUpperCase() == typeDoc.toUpperCase()) {
                                    strTypeDoc = argItem[0];
                                }
                            }
                        });
                    }
                    
                    $.app.ajax({
                        type: 'POST',
                        async: true,
                        url: '~/Dashboard/Postpaid/getFullClaroStatusLineObj',
                        data: { strNroDoc: nroDoc, strTypeDoc: strTypeDoc, strIdSession: Session.IDSESSION, plataformaAT: plataformaAT },
                        beforeSend: function () {
                            controls.lblFullClaroClient.html("");
                            controls.lblFullClaroClient.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                        },
                        success: function (data) {

                            if (data !== undefined) {
                                if (data.data !== undefined) {
                                    if (data.data.nombreEtiqueta !== undefined) {
                                        Session.DATACUSTOMER.StatusCodeFullClaroDesc = data.data.nombreEtiqueta;
                                        Session.DATACUSTOMER.StatusCodeFullClaro = data.data.codigoEtiqueta;
                                        Session.DATACUSTOMER.BonoElegidoFullClaro = data.data.bonoElegido;
                                        Session.DATACUSTOMER.BonoLineaFullClaro = data.data.bonoLinea;
                                        Session.DATACUSTOMER.EstadoFullClaro = data.data.estado;
                                        oCustomer.StatusCodeFullClaroDesc = data.data.nombreEtiqueta;
                                        oCustomer.StatusCodeFullClaro = data.data.codigoEtiqueta;
                                        oCustomer.BonoElegidoFullClaro = data.data.bonoElegido;
                                        oCustomer.BonoLineaFullClaro = data.data.bonoLinea;
                                        oCustomer.EstadoFullClaro = data.data.estado;
                                        controls.lblFullClaroClient.html("");
                                        controls.lblFullClaroClient.text(oCustomer.StatusCodeFullClaroDesc);
                                    }
                                }
                            }
                        },
                        error: function (ex) {
							controls.lblFullClaroClient.html("");
                            controls.lblFullClaroClient.text(strMessageClient);
                        }
                    });
                    
                },
                error: function (ex) {
					controls.lblFullClaroClient.html("");
                    controls.lblFullClaroClient.text(strMessageClient);
                }
            });
			
			
        },
        strUrl: window.location.href

    };

    $.fn.PostPaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['dataServiceContent', 'getCustomer', 'calculateDivHeight'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaid'),
                options = $.extend({}, $.fn.PostPaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaid', data);
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

    $.fn.PostPaid.defaults = {
        objError: {
            session: Session.IDSESSION,
        }
    }
    $('#contenedor').PostPaid();
})(jQuery);

$(window).resize(function () {
    $('#contenedor').PostPaid('calculateDivHeight');
})