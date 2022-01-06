(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidDataService.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , btnPostPhoneListHFC: $('#btnPost_PhoneListHFC', $element)
            , btnPostPhoneListPost: $('#btnPost_PhoneListPost', $element)
            , btnPostPetition: $('#btnPost_Petition', $element)
            , btnPostAnnotations: $('#btnPost_Annotations', $element)
            , btnPostLoanRental: $('#btnPost_LoanRental', $element)
            , btnPostLineAgreement: $('#btnPost_LineAgreement', $element)
            , btnPostStriations: $('#btnPost_Treason', $element)
            , btnPostConsumptionBalance: $('#btnPost_ConsumptionBalance', $element)
            , btnPostPinPuk: $('#btnPost_PinPuk', $element)
            , btnPostCalls: $('#btnPost_Calls', $element)
            , btnPost_ActionsHistory: $('#btnPost_ActionsHistory', $element)
            , btnHFCLTEFrequently: $('#btnHFCLTE_Frequently', $element)
            , btnHFCLTEEquipment: $('#btnHFCLTE_Equipment', $element)
            , btnPostInstalacion: $('#btnPost_Instalacion', $element)
            , btnPostPlan: $('#btnPost_Plan', $element)
            , btnPostHistorialLinea: $('#btnPost_HistorialLinea', $element)
            , btnPostHistorySIM: $('#btnPost_HistorySIM', $element)
            , btnPostConsulta4G: $('#btnPost_Consulta4G', $element)
            , btnPost_Portabilidad: $('#btnPost_Portabilidad', $element)
            , btnPostIMR: $('#btnPost_IMR', $element)
            , lblportability: $('#lbl_portability', $element)
            , lblPostPortabilityState: $('#lblPost_PortabilityState', $element)
            , lblRoaming: $('#lblRoaming', $element)
            , btnPostActiveDays: $('#btnPost_ActiveDays', $element)
            , btnPostRegRoaming: $('#btnPost_RegRoaming', $element)
            , btnPostCPortabilidad: $('#btnPost_CPortabilidad', $element)
            , btnPostServices: $('#btnPost_Services', $element)
            , divPostContenedor: $('#navbar-body > #divContenido > #contenedor')
            , divPostLineInfo: $('#divPostLineInfo', $element)
            , divPostAccountInfo: $('#divPostAccountInfo', $element)
            , divDataServiceContent: $('#divDataServiceContent', $element)
            , lblPostCellPhone: $('#lblPost_CellPhone', $element)
            , lblPost_ChangeNumber: $('#lblPost_ChangeNumber', $element)
            , lblPost_ChangeNum: $("#lblPost_ChangeNum", $element)
            , lblPostServiceA: $('#lblPost_ServiceA', $element)
            , lblPostServiceI: $('#lblPost_ServiceI', $element)
            , lblPostTelephony: $('#lblPost_Telephony', $element)
            , lblFullClaroService: $('#lblFullClaroService', $element)
            , lblPostInternet: $('#lblPost_Internet', $element)
            , lblPostCable: $('#lblPost_Cable', $element)
            , lblPostServiceActivationDate: $('#lblPost_ServiceActivationDate', $element)
            , lblPostServicePackage: $('#lblPost_ServicePackage', $element)
            , lblPostPlan: $('#lblPost_Plan', $element)
            , lblPostServiceTFI: $('#lblPost_ServiceTFI', $element)
            , lblPostModCon: $('#lblPost_ModCon', $element)
            , lblPostServiceCombo: $('#lblPost_ServiceCombo', $element)
            , lblPostTermContract: $('#lblPost_TermContract', $element)
            , lblPostReason: $('#lblPost_Reason', $element)
            , lblPostStateLine: $('#lblPost_StateLine', $element)
            , lblPostStateDate: $('#lblPost_StateDate', $element)
            , lblPostNroICCID: $('#lblPost_NroICCID', $element)
            , lblPostNroIMSI: $('#lblPost_NroIMSI', $element)
            , lblPostCampaign: $('#lblPost_Campaign', $element)
            , lblPostMobileBanking: $('#lblPost_MobileBanking', $element)
            , lblPostLimitConsume: $('#lblPost_LimitConsume', $element)
            , lblPostTypeSolution: $('#lblPost_TypeSolution', $element)
            , lblPostTypeSolution2: $('#lblPost_TypeSolution2', $element)
            , lblPostSeller: $('#lblPost_Seller', $element)
            , lblPost_IMR: $('#lblPost_IMR', $element)
            , trPostCellPhone: $('#trPost_CellPhone', $element)
            , trPostServiciosFijos: $('#trPost_ServiciosFijos', $element)
            , trPostTelefoniaFija: $('#trPost_TelefoniaFija', $element)
            , trPostInternetFijo: $('#trPost_InternetFijo', $element)
            , trPostCableInstalacion: $('#trPost_CableInstalacion', $element)
            , trPostICCIDIMSI: $('#trPost_ICCIDIMSI', $element)
            , tdTecnologia4G: $('.td_Tecnologia4G', $element)
            , tdTecnologia4GVacio: $('.td_Tecnologia4GVacio', $element)
            , tdPostLimitConsume: $('.tdPost_LimitConsume', $element)
            , tdPostSVA: $('.tdPost_SVA', $element)
            , trPostSVAIMR: $('#trPost_SVAIMR', $element)
            , trPostDiasActivosRoaming: $('#trPost_DiasActivosRoaming', $element)
            , trPostlblMontoImr: $('#trPost_lblMontoImr', $element)
            , txtCriteriaValue: $("#txtCriteriaValue")
             , lblPostDegradation: $('#lblPostDegradation', $element)
             , lblPostTethering: $('#lblPostTethering', $element)
             , lblPost_ReasonFirst: $('#lblPost_ReasonFirst', $element)
             , lblPost_ReasonDateFirst: $('#lblPost_ReasonDateFirst', $element)
             , lblPost_StatusEquipo: $('#lblPost_StatusEquipo', $element)



        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = that.getControls();

            controls.btnPostPhoneListHFC.addEvent(that, 'click', that.btnPostGetService_click);
            controls.btnPostPhoneListPost.addEvent(that, 'click', that.btnPostGetService_click);
            controls.btnPostPlan.addEvent(that, 'click', that.btnPostGetPlanHistory_click);
            controls.btnPostAnnotations.addEvent(that, 'click', that.btnAnotation_click)
            controls.btnPostActiveDays.addEvent(that, 'click', that.btnPostGetActiveDays_click);
            controls.btnPostHistorySIM.addEvent(that, 'click', that.btnPostGetHistorySIM_click);
            controls.btnPostIMR.addEvent(that, 'click', that.btnPostGetIMRConsult_click);
            controls.btnHFCLTEEquipment.addEvent(that, 'click', that.btnHFCEquipments_click);
            controls.btnPostServices.addEvent(that, 'click', that.btnPostServices_click);
            controls.btnPostPetition.addEvent(that, 'click', that.btnPostPetition_click);
            controls.btnPostStriations.addEvent(that, 'click', that.btnPostStriations_click);
            controls.btnPostConsumptionBalance.addEvent(that, 'click', that.btnPostConsumptionBalance_click);
            controls.btnPostPinPuk.addEvent(that, 'click', that.btnPostPinPuk_click);

            if (Session.DATACUSTOMER.Application == "HFC" || Session.DATACUSTOMER.Application == "LTE" || Session.DATACUSTOMER.Application == "FTTH") {
                controls.btnPostHistorialLinea.removeAttr('profile');
                if (Session.DATACUSTOMER) {
                    $(controls.btnPostPlan).off('click');
                    $(controls.btnPostPlan).on('click', that.btnPostGetPlanHistory_click);
                    if (Session.DATACUSTOMER.ContractID != null) {
                        controls.btnPostPlan.show();
                    } else {
                        controls.btnPostPlan.hide();
                    }
                }
            }

            controls.btnPostHistorialLinea.addEvent(that, 'click', that.btnPostHistorialLinea_click);
            controls.btnPostLineAgreement.addEvent(that, 'click', that.btnPostLineAgreement_click);
            controls.btnPostConsulta4G.addEvent(that, 'click', that.btnPost_Consulta4G);
            controls.btnPost_Portabilidad.addEvent(that, 'click', that.btnPost_Portabilidad);
            controls.btnPostLoanRental.addEvent(that, 'click', that.btnPost_LoanRental);
            controls.btnPost_ActionsHistory.addEvent(that, 'click', that.btnPost_ActionsHistory_click);
            controls.btnPostCalls.addEvent(that, 'click', that.btnPostCalls_click);
            controls.btnPostInstalacion.addEvent(that, 'click', that.btnPost_Instalacion_click);
            controls.btnPostRegRoaming.addEvent(that, 'click', that.btnPostRegRoaming_click);
            controls.btnHFCLTEFrequently.addEvent(that, 'click', that.btnHFCLTEFrequently_click);
            controls.lblPostTelephony.addEvent(that, 'click', that.SearchTelephone);
            that.render();
        },
        render: function () {

            var that = this,
                controls = that.getControls();
            if (Session.TYPESERVICE == "PostPago - TFI") {

                controls.btnPostStriations.prop("disabled", true);
            } else {
                controls.btnPostStriations.prop("disabled", false);
            }
        },
        /*PCTL-502-MIGRACION-INICIO*/
        FLAG_COEXISTENCE_CONST: {
            MIGRATED: "1",
            PURE: "0"
        },
        FLAG_MIGRATED_CONST: {
            BSCS70: "0",
            MIGRADO: "1",
            TOBE: "2"
        },
        FLA_PLATFORM_CONST: {
            ASIS: "ASIS",
            TOBE: "TOBE"
        },
        /*PCTL-502-MIGRACION-FIN*/
        SearchTelephone: function () {
            if (Session.DATACUSTOMER.ContractID != null) {
                if (Session.DATACUSTOMER.ContractID == '0') return;
                var strContractID = Session.DATACUSTOMER.ContractID;
                var strApplication = Session.ORIGINTYPE;


                Session.ORIGINTYPESERVICE = strApplication;
                Session.CONTRACTIDSERVICE = strContractID;

                //INICIATIVA 616
                if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE")
                {
                    $.window.open({
                        modal: false,
                        title: "Listado de Teléfonos",
                        url: '~/Dashboard/PostpaidDataService/PhoneListAltTobe',
                        data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication, plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
                        width: 850,
                        height: 500,
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
                    $.window.open({
                        modal: false,
                        title: "Listado de Teléfonos",
                        url: '~/Dashboard/PostpaidDataService/PhoneListAlt',
                        data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication },
                        width: 850,
                        height: 500,
                        buttons: {
                            Cancelar: {
                                click: function (sender, args) {
                                    this.close();
                                }
                            }
                        }
                    });
                }

            }
        },
        btnPostGetService_click: function (send, args) {
            var that = this;
            /*PCTL-502-MIGRACION-INICIO*/
            var strflagMigrado = Session.DATACUSTOMER.objPostDataAccount.plataformaAT == that.FLA_PLATFORM_CONST.ASIS ? that.FLAG_MIGRATED_CONST.BSCS70 : Session.DATACUSTOMER.flagConvivencia == that.FLAG_COEXISTENCE_CONST.MIGRATED ? that.FLAG_MIGRATED_CONST.MIGRADO : that.FLAG_MIGRATED_CONST.TOBE;
            /*PCTL-502-MIGRACION-FIN*/
            $.window.open({
                modal: false,
                title: "Listado de Servicios",
                url: '~/Dashboard/PostpaidDataService/PhoneList',
                data: { strIdSession: Session.IDSESSION, strCustomerID: Session.DATACUSTOMER.CustomerID, strFlagPlataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strCsIdPub: Session.DATACUSTOMER.csIdPub, strFlagConvivencia: strflagMigrado, strTipoProducto: Session.DATACUSTOMER.itm.origenRegistro },
                width: 1231,
                height: 550,
                buttons: {

                    Seleccionar: {
                        click: function (sender, args) {

                            var modalJQuery = args.event.view.$;


                            var rowPost = modalJQuery('#tbServicePost').DataTable().rows({ selected: true }).data();
                            var rowHFC = modalJQuery('#tbServiceHFC').DataTable().rows({ selected: true }).data();
                            var item = !$.array.isEmptyOrNull(rowPost) ? rowPost[0] : rowHFC[0];

                            var type = 1;
                            var value = '';
                            var strApplication;
                            var CellPhone;
                            var ContractID;


                            if (item[5] != 'null') {
                                if (item[5] == 'POSTPAID') {
                                    strApplication = item[5];
                                    CellPhone = item[1];
                                    ContractID = item[2];
                                    type = 3;
                                    value = ContractID;
                                } else if (item[5] == 'IFI') {
                                    strApplication = item[5];
                                    CellPhone = item[1];
                                    ContractID = item[2];
                                    type = 1;
                                    value = item[1];
                                }
                                else {
                                    strApplication = item[5];
                                    CellPhone = '';
                                    ContractID = item[1];
                                    type = 3;
                                    value = ContractID;
                                }
                                if (Session.DATACUSTOMER.ProductTypeText != item[5]) {
                                    Session.DATACUSTOMER.ChangeApplication = true;
                                }
                                Session.DATACUSTOMER.Telephone = CellPhone;
                                Session.DATACUSTOMER.ContractID = ContractID;
                                Session.DATACUSTOMER.Application = strApplication;
                                Session.DATACUSTOMER.ValueSearch = CellPhone;
                                Session.DATACUSTOMER.IsLTE = strApplication == "LTE" ? true : false;
                                Session.DATACUSTOMER.coIdPub = item[7];
                                modalJQuery.blockUI({
                                    message: '<div align="center"><img src="../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                    css: {
                                        border: 'none',
                                        padding: '15px',
                                        backgroundColor: '#000',
                                        '-webkit-border-radius': '10px',
                                        '-moz-border-radius': '10px',
                                        opacity: .5,
                                        color: '#fff'
                                    }
                                });
                                Session.EST = false;
                                Session.bIsSelec = false;
                                if (strApplication == 'POSTPAID') {
                                    Session.bIsSelec = true;
                                    $('#navbar-body > #divContenido > #contenedor').PostPaid('dataServiceContent');
                                } else {
                                    $('#navbar-contenedor').form('validationSearch', { ValueShearch: value, Type: type, Application: strApplication });
                                }

                                modalJQuery.unblockUI();

                                this.close();
                            }
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
        btnPostGetPlanHistory_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                if (Session.DATACUSTOMER.ContractID != null && Session.DATACUSTOMER.ContractID != "0") {

                    $.window.open({
                        modal: false,
                        title: "Historial de Plan",
                        url: '~/Dashboard/PostpaidDataService/PlanHistory',
                        data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strCoID: Session.DATACUSTOMER.ContractID, strFlagPlataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strFlagConvivencia: Session.DATACUSTOMER.flagConvivencia },
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
            }

        },
        btnPostCalls_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                if (Session.ORIGINTYPE == "HFC" || Session.ORIGINTYPE == "FTTH") {
                    $.redirect.GetParamsData('SU_HFC_DLF', Session.ORIGINTYPE);
                } else if (Session.ORIGINTYPE == "LTE") {
                    $.redirect.GetParamsData('SU_LTE_DLLF', Session.ORIGINTYPE);
                } else {
                    $.redirect.GetParamsData('SU_ACP_LLTEL', 'POSTPAGO');
                }
            }
        },
        btnPostGetActiveDays_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                if (Session.DATACUSTOMER.ContractID != null && Session.DATACUSTOMER.ContractID != "0") {


                    $.window.open({
                        modal: false,
                        title: "Días Activos",
                        url: '~/Dashboard/PostpaidDataService/ActiveDays',
                        data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strtelefono: Session.DATASERVICE.CellPhone },
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
            }
        },
        btnPostGetHistorySIM_click: function () {

            var strfechaMigracion;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                strfechaMigracion = Session.DATACUSTOMER.itm.migCuentaProd;
            } else {
                strfechaMigracion = null;
            }

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: "Histórico de Cambios de SIM",
                    url: '~/Dashboard/PostpaidDataService/HistoricoSIM',
                    data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strTelephone: Session.DATASERVICE.CellPhone, plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, flagconvivencia: Session.DATACUSTOMER.flagConvivencia, FechaMigracion: strfechaMigracion },
                    //data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strTelephone: Session.DATASERVICE.CellPhone, plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, flagconvivencia: Session.DATACUSTOMER.flagConvivencia},
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
        },
        btnPostRegRoaming_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.redirect.GetParamsData('SU_ACP_CGR', 'POSTPAGO');
            }
        },
        btnPostGetIMRConsult_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                var that = this, controls = that.getControls();
                controls.lblPost_IMR.text('');
                controls.btnPostIMR.removeClass('claro-btn-info').prop("disabled", true).addClass('btn-danger');

                $.app.ajax({
                    type: "POST",
                    url: '~/Dashboard/PostpaidDataService/IMRConsult',
                    data: {
                        ContractID: Session.DATACUSTOMER.ContractID,
                        strTelephone: Session.DATACUSTOMER.ValueSearch,
                        strCustomerID: Session.DATACUSTOMER.CustomerID,
                        strAccount: Session.DATACUSTOMER.Account,
                        strSegment: Session.DATACUSTOMER.Segment2,
                        strCodCustomerType: Session.DATACUSTOMER.CodCustomerType,
                        intBillingCycle: Session.DATACUSTOMER.objPostDataAccount.BillingCycle,
                        strIdSession: Session.IDSESSION
                    },
                    success: function (result) {
                        controls.btnPostIMR.removeClass('btn-danger').prop("disabled", false).addClass('claro-btn-info');
                        if (result.data != ' ' && result.data != "") {
                            controls.lblPost_IMR.text(result.data);
                        }
                        else {

                            showAlert(result.message);
                        }
                    }
                });
            }
        },
        btnHFCEquipments_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {


                $.window.open({
                    modal: false,
                    title: "Consulta de Equipos",
                    url: '~/Dashboard/PostpaidDataService/Equipments',
                    data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strCustomerID: Session.DATACUSTOMER.CustomerID, strApplication: Session.DATACUSTOMER.Application },
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

        },
        btnPostServices_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                var that = this;
                $.window.open({
                    modal: false,
                    title: "Servicios Comerciales Contratados",
                    url: '~/Dashboard/PostpaidDataService/Services',
                    data: {},
                    width: 1231,
                    height: 580,
                    buttons: that.getButtonsService()
                }).maximize();
            }
        },
        btnHFCLTEFrequently_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                var that = this;

                $.window.open({
                    modal: false,
                    title: " Números Frecuentes Actuales ",
                    url: '~/Dashboard/PostpaidDataService/Treason',
                    data: {},
                    width: 1231,
                    height: 550,
                    buttons: {
                        Histórico: {
                            click: function () {

                                $.window.open({
                                    modal: false,
                                    title: " Histórico Números Frecuentes",
                                    url: '~/Dashboard/PostpaidDataService/HistoryTreason',
                                    data: {},
                                    width: 1231,
                                    height: 600,
                                    buttons: {
                                        Exportar: {
                                            id: 'btnExportHistoryFrequent',
                                            click: function () {

                                                that.GetExportHistoryFrequent();
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
                        },
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            }
        },
        GetExportHistoryFrequent: function () {
            var strUrlModal = '~/Dashboard/PostpaidDataService/HistoryTreasonExport';
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var objPetitions = {};
            objPetitions.Application = Session.ORIGINTYPE;
            objPetitions.ContractID = Session.DATACUSTOMER.ContractID;
            objPetitions.Telephone = Session.DATACUSTOMER.ContractID;
            objPetitions.CustomerId = Session.DATACUSTOMER.CustomerID;
            objPetitions.Customer = Session.DATACUSTOMER.BusinessName;

            objPetitions.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(objPetitions),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=HistoricoFrecuentes.xlsx";
                }
            });
        },
        btnPostPetition_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {


                $.window.open({
                    modal: false,
                    title: "Peticiones",
                    url: '~/Dashboard/PostpaidDataService/Petitions',
                    data: {},
                    width: 1231,
                    height: 540,
                    buttons: {
                        Refrescar: {
                            click: function (senders, args) {
                                var modalJQuery = args.event.view.$;
                                modalJQuery('#contenedorPeticiones').PostPaidPetitions('getPetitions', $('#cboPetitionsType').val());
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
        },
        btnAnotation_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: "Anotaciones",
                    url: '~/Dashboard/PostpaidDataAccount/AccountAnnotation',
                    data: { strIdSession: Session.IDSESSION, strplataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
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
            }
        },
        btnPostStriations_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {


                $.window.open({
                    modal: false,
                    title: "Triaciones",
                    url: '~/Dashboard/PostpaidDataService/Striations',
                    width: 1231,
                    height: 550,
                    buttons: {
                        'Histórico': {
                            click: function (sender, args) {
                                var modalJQuery = args.event.view.$;
                                modalJQuery('#contenedorTriaciones').PostPaidStriations('btnPostHistoricalStriations_click');

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
        },
        btnPostConsumptionBalance_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                var method = Session.DATASERVICE.Application == 'POSTPAID' ? 'ConsumptionBalance' : 'ConsumptionBalanceHFCLTE'

                $.window.open({
                    modal: false,
                    title: "Consumo/Saldo",
                    url: '~/Dashboard/PostpaidDataService/' + method,
                    data: {},
                    width: 1231,
                    height: 550,
                    buttons: {
                        Cerrar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                }).maximize();
            }
        },
        btnPostPinPuk_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: "Pin y Puk",
                    url: '~/Dashboard/PostpaidDataService/PinPuk',
                    data: {},
                    width: 500,
                    height: 330,
                    buttons: {
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            }
        },
        btnPostHistorialLinea_click: function () {
            var title = ""
            if (Session.DATACUSTOMER.Application == "HFC" || Session.DATACUSTOMER.Application == "FTTH")
                title = "Historial de Servicio";
            else
                title = "Historial de Línea"

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: title,
                    url: '~/Dashboard/PostpaidDataService/LineHistory',
                    data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, plataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, flagConvivencia: Session.DATACUSTOMER.flagConvivencia, coIdPub: Session.DATACUSTOMER.coIdPub },
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
        },
        btnPost_Consulta4G: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: "Consulta Tecnología",
                    url: '~/Dashboard/PostpaidDataService/Consulta_4G',
                    data: { strIdSession: Session.IDSESSION, strPhoneNumber: Session.DATACUSTOMER.ValueSearch, strICCID: Session.DATASERVICE.NumberICCID },
                    width: 512,
                    height: 300,
                    buttons: {
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            }
        },
        btnPost_Portabilidad: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                var n = Session.DATASERVICE.PortabilityState.indexOf('PORT-IN') != -1 ? 2 : (Session.DATASERVICE.PortabilityState.indexOf('PORT-OUT') != -1 ? 1 : 0);
                var TextDate = '';
                if (n == 1) {
                    TextDate = 'Fecha de Desactivación';
                } else if (n == 2) {
                    TextDate = 'Fecha de Activación';
                }
                $.window.open({
                    modal: false,
                    title: "Datos de Portabilidad",
                    url: '~/Dashboard/PostpaidDataService/PortabilityConsultation',
                    data: { strIdSession: Session.IDSESSION, strTelephone: (Session.DATACUSTOMER.CellPhone == undefined) ? Session.DATACUSTOMER.Telephone : Session.DATACUSTOMER.CellPhone, strTextDate: TextDate },
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
            }
        },
        btnPost_Instalacion_click: function () {
            if (Session.ORIGINTYPE == "HFC" || Session.ORIGINTYPE == "FTTH") {
                $.redirect.GetParamsData('SU_HFC_ADINS', Session.ORIGINTYPE);
            } else {
                $.redirect.GetParamsData('SU_LTE_ADINS', Session.ORIGINTYPE);
            }
        },
        btnPost_LoanRental: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                $.window.open({
                    modal: false,
                    title: "Préstamo Alquiler",
                    url: '~/Dashboard/PostpaidDataService/LoanRental',
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
            }
        },
        LineAgreement: function (send) {
            var strUrl;
            strUrl = '~/Dashboard/PostpaidDataService/PostLineAgreement',
            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: strUrl,
                data: {},
                success: function (result) {
                    var StrUrlRedirect;

                    var strRuta = result.DataRuta;
                    var strSistema = result.DataSystem;

                    StrUrlRedirect = strRuta + "paginas/siga_consulta_acuerdos.aspx?Criterio=T&Valor=" + Session.DATACUSTOMER.ValueSearch + "&MostrarMenu=0&Usuario=" + Session.USERACCESS.login + "&codusuario=" + Session.USERACCESS.userId + "&sistema=" + strSistema;
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
        btnPostLineAgreement_click: function (send) {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {
                this.LineAgreement(send);
            }
        },
        btnPost_ActionsHistory_click: function () {

            if (this.m_state == false) {
                showAlert("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
            } else {



                $.window.open({
                    modal: false,
                    title: "Historial Acciones",
                    url: '~/Dashboard/PostpaidDataService/HistoryActions',
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
            }
        },
        getButtonsService: function () {
            var buttons = {};

            if (Session.DATASERVICE.Application == 'DTH') {
                buttons = {
                    Consultar: {
                        click: function () {
                            showAlert('dth');
                        }
                    }
                }
            } else if (Session.DATASERVICE.Application == 'HFC' || Session.DATASERVICE.Application == 'FTTH') {
                buttons = {
                    'Consultar Equipos': {
                        click: function () {
                            $.window.open({
                                modal: false,
                                title: "Consulta de Equipos",
                                url: '~/Dashboard/PostpaidDataService/ComputerQuery',
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
                        }
                    },
                    'Tareas Programadas': {
                        click: function () {
                            $.window.open({
                                modal: true,
                                title: "Consulta de Tareas Programadas",
                                url: '~/Dashboard/PostpaidDataService/ScheduledTasks',
                                data: {},
                                width: 1231,
                                height: 550,
                                buttons: {
                                    Consultar: {
                                        click: function () {
                                            $('#containerScheduledTasks').PostPaidScheduledTasks('btnConsult_click');
                                        }
                                    },
                                    Exportar: {
                                        id: "btnExportarScheduledTask",
                                        click: function () {
                                            $('#containerScheduledTasks').PostPaidScheduledTasks('btnExportToExcel_click');
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
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            } else {
                buttons = {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            }
            return buttons;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        m_state: true,

        setDataService: function () {
            var that = this,
                controls = that.getControls(),
                oDataService = Session.DATASERVICE;
            var jsonDataService = JSON.stringify(oDataService, function (key, value) {
                if (value === null)
                    return "";

                return value;
            });
            oDataService = JSON.parse(jsonDataService);
            if (Session.bIsSelec) {
                $('#lblPost_ProductType').html(oDataService.TypeProduct);
                Session.bIsSelec = false;
            }
            if (oDataService.Application != 'HFC' && oDataService.Application != 'FTTH') {
                controls.btnHFCLTEFrequently.remove();
            }
            if (oDataService.Application != 'HFC' && oDataService.Application != 'LTE' && oDataService.Application != 'FTTH') {
                controls.lblPostCellPhone.text(oDataService.CellPhone);
                controls.lblPost_ChangeNumber.text(Session.DATACUSTOMER.indicadorCambioNumero === "1" ? "Si" : "No");
                if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
                    controls.lblPost_ChangeNumber.show();
                    controls.lblPost_ChangeNum.show();
                } else {
                    controls.lblPost_ChangeNumber.hide();
                    controls.lblPost_ChangeNum.hide();
                }


                controls.lblPostTypeSolution2.text(oDataService.TypeSolution == "null" ? "" : oDataService.TypeSolution);
                controls.lblPostNroICCID.text(oDataService.NumberICCID);
                controls.lblPostNroIMSI.text(oDataService.NumberIMSI);
                controls.lblPostLimitConsume.text(oDataService.LimitConsume);
                controls.trPostServiciosFijos.remove();
                controls.trPostTelefoniaFija.remove();
                controls.trPostInternetFijo.remove();
                controls.trPostCableInstalacion.remove();
                controls.tdTecnologia4GVacio.remove();
                controls.tdPostSVA.remove();
                controls.btnHFCLTEEquipment.remove();

                if (oDataService.StateDTH) {
                    controls.btnPostLoanRental.remove();
                    controls.btnPostStriations.remove();
                    controls.btnPostConsumptionBalance.remove();
                    controls.btnPostPinPuk.remove();
                    controls.btnPostCalls.remove();
                }


                var Linea = '';
                if (oDataService.CellPhone != null) {
                    Linea = oDataService.CellPhone;
                }

                that.getStatusEquipo(Linea);

            } else {
                controls.lblPostServiceA.addClass(oDataService.ServiceA);
                controls.lblPostServiceI.addClass(oDataService.ServiceI);
                controls.lblPostTelephony.addClass(oDataService.Telephony);
                controls.lblPostInternet.addClass(oDataService.Internet);
                controls.lblPostCable.addClass(oDataService.Cable);
                controls.lblPostTypeSolution.text(oDataService.TypeSolution == "null" ? "" : oDataService.TypeSolution);
                controls.trPostCellPhone.remove();
                controls.trPostICCIDIMSI.remove();
                controls.tdTecnologia4G.remove();
                controls.tdPostLimitConsume.remove();
                controls.trPostSVAIMR.remove();
                controls.trPostDiasActivosRoaming.remove();
                controls.trPostlblMontoImr.remove();
                controls.btnPostLoanRental.remove();
                controls.btnPostLineAgreement.remove();
                controls.btnPostStriations.remove();
                controls.btnPostPinPuk.remove();

                if (oDataService.TelephonyValue == "T") {
                    controls.btnPostConsumptionBalance.prop("disabled", false);
                    controls.btnPostCalls.prop("disabled", false);
                }
                else {
                    controls.btnPostConsumptionBalance.prop("disabled", true);
                    controls.btnPostCalls.prop("disabled", true);
                }


                if (Session.DATACUSTOMER.PlaneCodeInstallation.toUpperCase().indexOf('-F') > -1) {
                    $('#lblPost_ProductType').html("PostPago - FTTH");
                }
                else {
                    $('#lblPost_ProductType').html(oDataService.TypeProduct);
                }


            }
            if (oDataService.Portability == false) {
                controls.btnPost_Portabilidad.remove();
                controls.lblportability.remove();
            }
            if (oDataService.Roaming == false) {
                controls.btnPostRegRoaming.remove();
                controls.lblRoaming.remove();
            }
            if (oDataService.StateServicePackage) {
                controls.lblPostServicePackage.text(oDataService.ServicePackage);
            }
            else {
                controls.lblPostServicePackage.hide();
            }
            if (oDataService.StateServiceTFI) {
                controls.lblPostServiceTFI.text(oDataService.ServiceTFI);
            } else {
                controls.lblPostServiceTFI.hide();
            }
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
                // that.getLastChangePlan();
                controls.lblPostServiceCombo.hide();
            } else {
                if (oDataService.StateServiceCombo) {
                    controls.lblPostServiceCombo.text(oDataService.ServiceCombo);
                } else {
                    controls.lblPostServiceCombo.hide();
                }
            }


            that.setStatusFullClaroService();
            controls.lblPostServiceActivationDate.text(oDataService.ActivationDate);
            controls.lblPostPlan.text(oDataService.Plan);
            controls.lblPostTermContract.text(oDataService.TermContract);
            controls.lblPostReason.text(oDataService.Reason);
            //controls.lblPost_ReasonFirst.text(oDataService.LastReasonState);
            //controls.lblPost_ReasonDateFirst.text(oDataService.LastReasonStateDate);

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE" && Session.DATACUSTOMER.coIdPub != null && Session.DATACUSTOMER.coIdPub != "") {
                that.getLastReasonState();
            } else if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "ASIS" && Session.DATACUSTOMER.ContractID != "0" && Session.DATACUSTOMER.ContractID != "" && Session.DATACUSTOMER.ContractID != null) {
                that.getLastReasonState();
            } else {
                controls.lblPost_ReasonFirst.text("");
            }



            controls.lblPostStateLine.text(oDataService.StateLine);
            controls.lblPostStateDate.text(oDataService.StateDate);
            controls.lblPostCampaign.text(oDataService.Campaign);
            controls.lblPostMobileBanking.text(oDataService.MobileBanking);
            controls.lblPostSeller.text(oDataService.Seller);
            controls.lblPostPortabilityState.text(oDataService.PortabilityState);
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {

                that.getDegradationTobe();
                // controls.lblPostTethering.text(oDataService.Tethering);
            } else {
                controls.lblPostDegradation.text(oDataService.Degradation);
                controls.lblPostTethering.text(oDataService.Tethering);
            }


            controls.lblPostModCon.text(oDataService.SegmentoCliente);

            if (Session.DATACUSTOMER.ContractID != null && Session.DATACUSTOMER.ContractID != "0") {
                controls.btnPostPlan.prop("disabled", false);
                controls.btnPostHistorialLinea.prop("disabled", false);
                controls.btnPostHistorySIM.prop("disabled", false);
                controls.btnPostConsulta4G.prop("disabled", false);
                controls.btnPostIMR.prop("disabled", false);
                controls.btnPostRegRoaming.prop("disabled", false);
                controls.btnPost_Portabilidad.prop("disabled", false);

                if (oDataService.EnableEquipment == true) {
                    controls.btnHFCLTEEquipment.prop("disabled", false);
                }
                else {
                    controls.btnHFCLTEEquipment.prop("disabled", true);
                }

                if (oDataService.Application != 'HFC' && oDataService.Application != 'LTE' && oDataService.Application != 'FTTH') {
                    controls.btnPostActiveDays.prop("disabled", false);
                }

            } else {
                this.m_state = false;
            }

        },
        //mg13
        getLastChangePlan: function () {
            $.unblockUI();
            var that = this,
         controls = that.getControls(),
          strMessageClient = 'Ocurrio un Error en la Carga';

            if (this.m_state == false) {
                controls.lblPostServiceCombo.text("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
                controls.lblPostServiceCombo.css("color", "blue");
            } else {
                $.app.ajax({
                    type: 'POST',
                    async: true,
                    url: '~/Dashboard/PostpaidDataService/getLastChangePlan',
                    data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, strCoID: Session.DATACUSTOMER.ContractID, strFlagPlataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strFlagConvivencia: Session.DATACUSTOMER.flagConvivencia },
                    beforeSend: function () {

                        controls.lblPostServiceCombo.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                    },
                    success: function (d) {

                        console.log(d);
                        Session.DATASERVICE.StateServiceCombo = d.dataRow > 1;
                        if (Session.DATASERVICE.StateServiceCombo) {
                            controls.lblPostServiceCombo.html("");
                            controls.lblPostServiceCombo.text(d.dataList[1].PLAN_TARIFARIO);
                            Session.DATASERVICE.ServiceCombo = d.dataList[1].PLAN_TARIFARIO;
                        } else {
                            controls.lblPostServiceCombo.html("");
                            controls.lblPostServiceCombo.hide();
                        }

                    },

                    error: function (ex) {
                        controls.lblPostServiceCombo.html("");
                        controls.lblPostServiceCombo.css("color", "blue");
                        controls.lblPostServiceCombo.text(strMessageClient);

                    }
                });
            }


        },
        //mg13
        getDegradationTobe: function () {
            $.unblockUI();
            var that = this,
           controls = that.getControls(),
            strMessageClient = 'Ocurrio un Error en la Carga';

            if (this.m_state == false) {
                controls.lblPostDegradation.text("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
                controls.lblPostDegradation.css("color", "blue");
            } else {
                $.app.ajax({
                    type: 'POST',
                    async: true,
                    url: '~/Dashboard/Postpaid/getDegradationTobe',
                    data: { strIdSession: Session.IDSESSION, strMsidn: Session.DATASERVICE.CellPhone, coIdPub: Session.DATACUSTOMER.coIdPub },
                    beforeSend: function () {
                        //  controls.lblPostDegradation.text(oDataService.Degradation);
                        controls.lblPostDegradation.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');
                        controls.lblPostTethering.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                    },
                    success: function (d) {

                        console.log(d);
                        var listOptional = d.listOptional;
                        if (listOptional) {
                            var filterListDeg = listOptional.filter(function (op) {
                                return op.clave == 'degradacion';
                            });
                            if (filterListDeg != null && typeof filterListDeg != "undefined") {
                                controls.lblPostDegradation.html("");
                                controls.lblPostDegradation.css("color", "#333");
                                controls.lblPostDegradation.text(filterListDeg[0].valor);
                                Session.DATASERVICE.Degradation = filterListDeg[0].valor;
                            } else {
                                controls.lblPostDegradation.html("");
                            }

                            var filterListTe = listOptional.filter(function (op) {
                                return op.clave == 'tethering';
                            });
                            if (filterListTe != null && typeof filterListTe != "undefined") {
                                controls.lblPostTethering.html("");
                                controls.lblPostTethering.css("color", "#333");
                                controls.lblPostTethering.text(filterListTe[0].valor);
                                Session.DATASERVICE.Tethering = filterListTe[0].valor;

                            } else {
                                controls.lblPostTethering.html("");


                            }
                        } else {
                            controls.lblPostDegradation.html("");
                            controls.lblPostTethering.html("");
                        }





                    },

                    error: function (ex) {
                        controls.lblPostDegradation.html("");
                        controls.lblPostDegradation.css("color", "red");
                        controls.lblPostDegradation.text(strMessageClient);
                        controls.lblPostTethering.html("");
                        controls.lblPostTethering.css("color", "red");
                        controls.lblPostTethering.text(strMessageClient);
                    }
                });

            }

        },
        //mg13
        getLastReasonState: function () {
            $.unblockUI();
            var that = this,
           controls = that.getControls(),
            strMessageClient = 'Ocurrio un Error en la Carga';

            if (this.m_state == false) {
                controls.lblPost_ReasonFirst.text("Datos de Clientes no Cargados. Primero debe realizar una busqueda en la parte superior por telefono o nombres");
                controls.lblPost_ReasonFirst.css("color", "blue");
            } else {
                $.app.ajax({
                    type: 'POST',
                    async: true,
                    url: '~/Dashboard/Postpaid/getLastReasonState',
                    data: { strIdSession: Session.IDSESSION, strContractID: Session.DATACUSTOMER.ContractID, plataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, flagConvivencia: Session.DATACUSTOMER.flagConvivencia, coIdPub: Session.DATACUSTOMER.coIdPub },
                    beforeSend: function () {

                        controls.lblPost_ReasonFirst.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                    },
                    success: function (LastReasonState) {

                        console.log(LastReasonState);
                        controls.lblPost_ReasonFirst.html("");
                        controls.lblPost_ReasonFirst.css("color", "#333");
                        controls.lblPost_ReasonFirst.text(LastReasonState);
                    },

                    error: function (ex) {
                        controls.lblPost_ReasonFirst.html("");
                        controls.lblPost_ReasonFirst.css("color", "red");
                        controls.lblPost_ReasonFirst.text(strMessageClient);

                    }
                });

            }
        },
        setStatusFullClaroService: function () {
            var that = this,
            oCustomer = Session.DATACUSTOMER,
            controls = that.getControls(),
            oDataService = Session.DATASERVICE;
            var strMessageClient = 'No cumple requisito';
            var strCoId = oCustomer.ContractID;
            var plataformaAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            var strKeyName = "strKeyDocValue";

            $.unblockUI();
            $.app.ajax({
                type: 'POST',
                async: false,
                url: '~/Dashboard/Home/getKeyValue',
                data: { strKeyName: strKeyName, strIdSession: Session.IDSESSION },
                beforeSend: function () {
                    controls.lblFullClaroService.html("");
                    controls.lblFullClaroService.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                },
                success: function (data) {
                    var nroDoc = (oCustomer === undefined ? "" : (oCustomer.DNIRUC === "" ? (oCustomer.DocumentNumber === "" ? "" : oCustomer.DocumentNumber) : oCustomer.DNIRUC));
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

                                if (argItem[1].toUpperCase() === typeDoc.toUpperCase()) {
                                    strTypeDoc = argItem[0];
                                }
                            }
                        });
                    }
                    $.app.ajax({
                        type: 'POST',
                        async: true,
                        url: '~/Dashboard/Postpaid/getFullClaroStatusCoIdObj',
                        data: {
                            strCoId: strCoId,
                            strNroDoc: nroDoc,
                            strTypeDoc: strTypeDoc,
                            plataformaAT: plataformaAT,
                            strIdSession: Session.IDSESSION,
                        },
                        beforeSend: function () {
                            controls.lblFullClaroService.html("");
                            controls.lblFullClaroService.append('<img  width="20px" height="20px" src="/Images/loading.gif" alt="Cargando..." />');

                        },
                        success: function (data) {
                            if (data != undefined ? (data.data != undefined ? (data.data.nombreEtiqueta2 != undefined ? true : false) : false) : false) {
                                if (typeof Session.DATASERVICE != "undefined" && Session.DATASERVICE != null) {

                                    Session.DATASERVICE.statusFullClaroService = data.data.nombreEtiqueta2;
                                    Session.DATASERVICE.statusFullClaroServiceCode = data.data.codigoEtiqueta2;
                                    oDataService.statusFullClaroService = data.data.nombreEtiqueta2;
                                    oDataService.statusFullClaroServiceCode = data.data.codigoEtiqueta2;
                                    controls.lblFullClaroService.html("");
                                    controls.lblFullClaroService.text(oDataService.statusFullClaroService);
                                } else {
                                    controls.lblFullClaroService.html("");
                                    controls.lblFullClaroService.text("Se debe cargar los Datos del Servicio !!");
                                }

                            } else {
                                controls.lblFullClaroService.html("");
                                controls.lblFullClaroService.text("");
                            }
                        },
                        error: function (ex) {
                            controls.lblFullClaroService.html("");
                            controls.lblFullClaroService.text(strMessageClient);
                        }
                    });
                },
                error: function (ex) {
                    controls.lblFullClaroService.html("");
                    controls.lblFullClaroService.text(strMessageClient);
                }
            });
        },

        getStatusEquipo: function (Linea) {

            var that = this,
            controls = that.getControls();
            controls.lblPost_StatusEquipo.text('');


            var strUrlHomeStatusEquipo = '~/Dashboard/Home/GetStateEquipo';
            var strLinea = Linea;
            $.app.ajax({
                type: 'POST',
                async: true,
                url: strUrlHomeStatusEquipo,
                data: { strLinea: strLinea, strIdSession: Session.IDSESSION },
                success: function (data) {
                    console.log(data);


                    if (data != null) {
                        if (data.data.PO_CODERROR === '0') {
                            controls.lblPost_StatusEquipo.text(data.data.PO_ESTADO);

                        } else {
                            controls.lblPost_StatusEquipo.text('');
                        }
                    }

                },
                error: function (ex) {
                    console.log(ex);
                }

            });
        },

        strUrl: window.location.href
    };

    $.fn.PostPaidDataService = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['setDataService', 'getCustomer'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidDataService'),
                options = $.extend({}, $.fn.PostPaidDataService.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidDataService', data);
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

    $.fn.PostPaidDataService.defaults = {
    }

    $('#containerDataService').PostPaidDataService();
})(jQuery);
