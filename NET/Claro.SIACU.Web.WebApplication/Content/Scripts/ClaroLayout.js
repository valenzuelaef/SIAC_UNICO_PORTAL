if (!!Session) {
    function showDialogManagementInstant() {


        var valValueSearch = $("#txtCriteriaValue").val();
        var valPhone = valValueSearch;

        var valOriginType = Session.ORIGINTYPE;

        var strUrlModal = '~/Management/PrepaidInstant/Index';
        $.window.open({
            type: 'post',
            modal: false,
            title: "Instantaneas",
            url: strUrlModal,
            data: { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType },
            width: 900,
            height: 600,
            buttons: {
                Cerrar: {
                    click: function (sender, args) {
                        this.close();
                    }
                }
            }
        });
    }

    function blockUI(div) {
        var stUrlLogo = window.location.href + "/Images/loading2.gif";
        $(div).html('').append('<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando .... </div>');
    }

    function showDialogSearch() {
        $.window.open({
            modal: false,
            title: "CONSULTA PRODUCTOS",
            url: '~/Dashboard/Home/CustomersNames',
            width: 900,
            height: 600,
            buttons: {
                Seleccionar: {
                    click: function (sender, args) {
                        var modalJQuery = args.event.view.$;
                        var controls = modalJQuery('#contenedor-customerNames').form('getControls');

                        var dataRow = controls.tblDatosCliente.DataTable().row({ selected: true }).data();

                        if (!$.isEmptyObject(dataRow)) {
                            Session.CUSTOMERPRODUCT = {};
                            Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                            Session.CUSTOMERPRODUCT.DocumentType = dataRow.DocumentType;
                            Session.CUSTOMERPRODUCT.DocumentIdentity = dataRow.DocumentIdentity;
                            Session.CUSTOMERPRODUCT.Names = dataRow.Names;

                            $.window.open({
                                modal: false,
                                title: "CONSULTA PRODUCTOS",
                                url: '~/Dashboard/Home/Products',
                                width: 900,
                                height: 600,
                                buttons: {
                                    Cerrar: {
                                        click: function () {
                                            this.close();
                                        }
                                    }
                                }
                            });
                            this.close();


                        } else {
                            showAlert('Debe seleccionar un cliente.', this.strTitleMessage);
                            return;
                        }
                    }
                },
                Cancelar: {
                    click: function (sender, args) {
                        $('#ddlTipoBusqueda').val('');
                        $('#txtCriteriaValue').val('');
                        $('#btnSearch').html("Buscar");

                        this.close();
                    }
                }
            }
        });

    }



    (function ($, undefined) {

        'use strict';

        var Form = function ($element, options) {
            $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

            this.setControls({
                form: $element,
                body: $('body'),
                btnCriteriaSearch: $('#btnCriteriaSearch', $element),
                btnResult: $('#btnResult', $element),
                btnInstant: $('#btnInstant', $element),
                btnCerrarInstant: $('#btncerrarinstant', $element),
                btnSearch: $('#btnSearch', $element),
                btnClean: $('#btnClean', $element),
                divButtonSearch: $('#div-button-search .dropdown-menu li a', $element),
                divInstant: $('#divInstant', $element),
                ddlTipoBusqueda: $('#ddlTipoBusqueda', $element),
                divMainBody: $('#navbar-body'),
                divMainHeader: $('#main-header'),
                divMainFooter: $('#main-footer'),
                divModalError: $('#divModalError', $element),
                divContenido: $('#divContenido', $element),
                divPostLineInfo: $('#divPostLineInfo', $element),
                flagDivInstant: $('#flagDivInstant', $element),
                idmMenu: $('#idmMenu', $element),
                myModal: $('#myModal', $element),
                myModalLoad: $('#myModalLoad', $element),
                txtCriteriaValue: $('#txtCriteriaValue', $element),

                idmToolbar: $('#idmToolbar', $element),
                idulListPerfiles: $('#idulListPerfiles', $element),
                uiBarra: $('#uiBarra', $element),
                idUserName: $('#idUserName', $element),
                idSession: $('#idSession', $element),
                btnCriteria: $('#btnCriteria', $element),
                ddlBusquedaDesplegable: $('#ul-button-Search', $element),
                nroNodo: $('#nroNodo', $element),
            });
        }

        Form.prototype = {
            constructor: Form,
            init: function () {
                if (window.opener != null) {
                    var that = this,
                        controls = this.getControls();

                    Session.ORIGINTYPE = '';

                    controls.divButtonSearch.addEvent(that, 'click', that.btnValidateTypeSearch_click);
                    controls.btnCriteriaSearch.addEvent(that, 'click', that.btnCriteriaSearch_click);
                    controls.btnResult.addEvent(that, 'click', that.searchbyDocumentNumber);
                    controls.btnInstant.addEvent(that, 'click', that.btnInstant_click);
                    controls.btnCerrarInstant.addEvent(that, 'click', that.btnCerrarInstant_click);
                    controls.btnClean.addEvent(that, 'click', that.btnClean_click);
                    $(window).addEvent(that, 'resize', that.resizeContent);

                    that.render();
                }

            },

            render: function () {
                Session.NotEvalState = false;
                $.app.NotMaximed();
                this.enterValidationSearch();
                this.createMenu();
            },
            addAttributeParentProduct: function (data, strValue) {
                for (var i = 0; i < data.length; i++) {
                    data[i].parentProduct = strValue;
                    if (data[i].items != null && data[i].items.length > 0) {
                        data[i].items = this.addAttributeParentProduct(data[i].items, strValue);
                    }
                }
                return data;
            },
            addAttributeDescriptionAux: function (data, strValue) {
                for (var i = 0; i < data.length; i++) {
                    data[i].parentProduct = strValue;
                    if (data[i].isDefault === true) {
                        data[i].descriptionAux = strValue;
                    }
                    if (data[i].items != null && data[i].items.length > 0) {
                        data[i].items = this.addAttributeDescriptionAux(data[i].items, strValue);
                    }
                }
                return data;
            },
            showDialogLoad: function () {




                var strUrlTemplate = '/Home/DialogTemplate';
                var controls = this.getControls(),
                    valPhone = "",
                    valAccount = Session.DATACUSTOMER.Account,
                    valContract = Session.DATACUSTOMER.ContractID,
                    valOriginType = Session.ORIGINTYPE;
                if (valOriginType === "PREPAID" && Session.DATASERVICE != null && Session.DATASERVICE.NumberCellphone != null)
                    valPhone = Session.DATASERVICE.NumberCellphone
                else
                    valPhone = Session.TELEPHONE

                if (valOriginType === "PREPAID") {
                    var strUrlFillInstantPrepaid = '~/Dashboard/Home/FillInstantPrepaid';
                    var strUrlInstantPrepaid = '~/Dashboard/Home/InstantsPrepaid';
                    var dataInstantPrepaid = { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType };
                    $.app.ajax({
                        type: 'POST',
                        url: strUrlFillInstantPrepaid,
                        data: dataInstantPrepaid,
                        success: function (response) {
                            var n = 0;
                            if (response.listInstant.length) n = response.listInstant.length;
                            $('#spidnotify').html(n);
                            if (response.listInstant.length > 0) {
                                $.window.open({
                                    type: 'POST',
                                    buttons: {
                                        Cerrar: {
                                            click: function () {
                                                $('#hdnCantReg').val("0");
                                                Session.IDINSTANT = $('#hdnIdInstants').val();
                                                Session.DESCRIPTIONINSTANT = $('#hdnDescriptionInstants').val();
                                                Session.ORIGINAPP = 'PREPAGO';
                                                var flag = 1;

                                                for (var i = 0; i < response.listInstant.length; i++) {
                                                    if (response.listInstant[0].Description === "CLIENTE TIENE PENDIENTE ACTUALIZACIÓN DE DATOS. Preguntar al cliente lo siguiente: Estimado cliente, usted es la persona que utiliza el servicio de manera permanente?. Si la respuesta es SI, indicar lo siguiente: Hemos verificado que necesitamos actualizar sus datos a fin de atender sus solicitudes sin ningún inconveniente Si la respuesta es NO, marcar No deseo ser titular") {

                                                        flag = 0;
                                                        break;
                                                    }
                                                }
                                                if (flag === 1) {
                                                    this.close();

                                                } else {
                                                    $.redirect.GetParamsData('SU_SIACA_INS', "PREPAGO");

                                                }

                                            }
                                        }

                                    },
                                    title: "Instantaneas",
                                    template: strUrlTemplate,
                                    url: strUrlInstantPrepaid,
                                    data: response,
                                    width: 900,
                                    height: 600,
                                    closeOnEscape: false

                                });
                            } else {
                                $('#hdnCantReg').val("0");
                                $('#spidnotify').html("0");
                            }
                        },
                        error: function (ex) {
                            controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                        }
                    });


                } else {
                    var strUrlFillInstant = '~/Dashboard/Home/FillInstant';

                    var strUrlInstant = '~/Dashboard/Home/Instants';
                    var dataInstant = { strIdSession: Session.IDSESSION, strPhone: valPhone, strAccount: valAccount, strContract: valContract, strOriginType: valOriginType };
                    $.app.ajax({
                        type: 'POST',
                        url: strUrlFillInstant,
                        data: dataInstant,
                        success: function (response) {
                            var n = 0;
                            if (response.listInstant.length) n = response.listInstant.length;
                            $('#spidnotify').html(n);
                            if (response.listInstant.length > 0) {
                                $.window.open({
                                    type: 'POST',
                                    modal: false,
                                    title: "Instantaneas",
                                    template: strUrlTemplate,
                                    url: strUrlInstant,
                                    data: response,
                                    width: 900,
                                    height: 600,
                                    buttons: {
                                        Cerrar: {
                                            click: function (sender, args) {
                                                this.close();
                                            }
                                        }
                                    }
                                });
                            }
                        },
                        error: function (ex) {
                            controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                        }
                    });


                }
            },

            aMenuDefaultReclamos_click: function (send, args) {
                var that = this;
                $.redirect.GetParamsData(that.code.toString(), Session.ORIGINAPP);
            },
            aMenuReclamos_click: function (send, args) {
                var that = this;
                $.window.open({
                    modal: false,
                    cache: false,
                    title: "",
                    iconClass: 'glyphicon-comment',
                    text: 'Usted tiene un servicio consultado previamente, se limpiará la información consultada anteriormente, ¿Desea continuar?',
                    buttons: {
                        Ok: {
                            click: function () {
                                this.close();
                                location.reload();
                                $.redirect.GetParamsData(that.code.toString(), Session.ORIGINAPP);
                            }
                        },
                        Cancelar: {
                            click: function () {
                                this.close();
                            }
                        },
                    },
                    width: 400,
                    height: 200,
                });

            },



            aMenuOption_click: function (send, args) {

                $.window.open({
                    modal: false,
                    title: send.data.name,
                    url: send.data.url,
                    width: 1231,
                    height: 700,
                    buttons: {
                        Cerrar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                }).maximize();

            },


            aMenuComplete_click: function (send, args) {
                var that = this;
                Session.ORIGINAPP = this.parentProduct;

                if ('SU_REC_RSGA' === that.code.toString()) {
                    $.redirect.GetParamsData('SU_REC_RSGA', "RECLAMOS");
                }
                else if ('SU_ACP_AIN' === that.code.toString() || 'SU_SIACA_INST' === that.code.toString() || 'SU_HFC_LIN' === that.code.toString() || 'SU_ACP_PEP' === that.code.toString() || 'SU_SIACA_PEP' === that.code.toString()) {
                    $.ValidateCode(this.code, Session.ORIGINAPP, null, true);
                }
                else if ("SU_ACP_IL" === that.code.toString()) {
                    if (Session.ORIGINTYPE != '') {
                        if (Session.ORIGINTYPE === "POSTPAID" || Session.ORIGINTYPE === "DTH" || Session.ORIGINTYPE === "HFC" || Session.ORIGINTYPE === "LTE") {
                            if (Session.DATASERVICE === null) {
                                $.app.ajax({
                                    async: false,
                                    type: 'POST',
                                    url: '~/Dashboard/Postpaid/DataServiceContent',
                                    data: {
                                        strIdSession: Session.IDSESSION,
                                        strContratoID: Session.DATACUSTOMER.ContractID,
                                        strApplication: Session.DATACUSTOMER.Application,
                                        strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                                        strDocumentType: Session.DATACUSTOMER.DocumentType,
                                        strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                                        strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                                        strIsLTE: Session.DATACUSTOMER.IsLTE,
                                        strphone: Session.DATACUSTOMER.ValueSearch,
                                        strCustomerId: Session.DATACUSTOMER.CustomerID,
                                        plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                                        strphonespeed: Session.DATACUSTOMER.Telephone,
                                        flagconvivencia: Session.DATACUSTOMER.flagConvivencia,
                                        coIdPub: Session.DATACUSTOMER.coIdPub
                                    },
                                    success: function (response) {
                                        Session.DATASERVICE = response.data;
                                    },
                                    error: function (ex) {
                                        controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                                    }
                                });
                            }
                        }
                        Session.ORIGINAPP = this.parentProduct;
                        $.ValidateCode(this.code, Session.ORIGINAPP, null, true);
                    }
                }
                else {
                    if (Session.DATASERVICE === null) {
                        $.app.ajax({
                            async: false,
                            type: 'POST',
                            url: '~/Dashboard/Postpaid/DataServiceContent',
                            data: {
                                strIdSession: Session.IDSESSION,
                                strContratoID: Session.DATACUSTOMER.ContractID,
                                strApplication: Session.DATACUSTOMER.Application,
                                strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                                strDocumentType: Session.DATACUSTOMER.DocumentType,
                                strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                                strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                                strIsLTE: Session.DATACUSTOMER.IsLTE,
                                strphone: Session.DATACUSTOMER.ValueSearch,
                                strCustomerId: Session.DATACUSTOMER.CustomerID,
                                plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                                strphonespeed: Session.DATACUSTOMER.Telephone,
                                flagconvivencia: Session.DATACUSTOMER.flagConvivencia,
                                coIdPub: Session.DATACUSTOMER.coIdPub
                            },
                            success: function (response) {
                                Session.DATASERVICE = response.data;
                            },
                            error: function (ex) {
                                controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                            }
                        });
                    }
                    $.ValidateCode(this.code, Session.ORIGINTYPE);

                }

            },
            btnValidateTypeSearch_click: function (send, args) {
                var controls = this.getControls();
                var strName = send.attr('data-value');

                if (strName != "6") {
                    controls.ddlTipoBusqueda.val(send.attr('data-value'));
                    controls.btnSearch.html(send.text() + " <span class='caret'></span>");
                } else {
                    controls.ddlTipoBusqueda.val("6");
                }
                this.initialiceValidationSearch(send);
            },
            btnCriteriaSearch_click: function (send, args) {
                Session.EST = true;
                this.validationSearch();
            },
            btnInstant_click: function (send, args) {

                var controls = this.getControls(),
                     valOriginType = Session.ORIGINTYPE,
                     valValueSearch = ""
                if (valOriginType === "PREPAID" && Session.DATASERVICE != null && Session.DATASERVICE.NumberCellphone != null)
                    valValueSearch = Session.DATASERVICE.NumberCellphone
                else
                    valValueSearch = Session.TELEPHONE


                if ((valValueSearch === undefined || valValueSearch === "") && valOriginType === "") {
                    modalAlert('Debes ingresar un valor válido para la búsqueda', 'Mensaje');
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
                else {
                    $('#btnInstant').prop('title', 'Instantaneas');
                    $('#spidnotify').html('');
                    var valPhone = valValueSearch;
                    var valAccount = Session.DATACUSTOMER.Account;
                    var valContract = Session.DATACUSTOMER.ContractID;
                    valOriginType = Session.ORIGINTYPE;
                    var strUrlTemplate = '/Home/DialogTemplate';

                    if (valOriginType === "PREPAID") {
                        var strUrlFillInstantPrepaid = '~/Dashboard/Home/FillInstantPrepaid';
                        var strUrlInstantPrepaid = '~/Dashboard/Home/InstantsPrepaid';
                        var dataInstantPrepaid = { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType };
                        $.app.ajax({
                            async: false,
                            type: 'POST',
                            url: strUrlFillInstantPrepaid,
                            data: dataInstantPrepaid,
                            success: function (response) {
                                var n = 0;
                                if (response.listInstant.length) n = response.listInstant.length;
                                $('#spidnotify').html(n);

                                $.window.open({
                                    type: 'POST',
                                    buttons: {
                                        Cerrar: {
                                            click: function () {
                                                $('#hdnCantReg').val("0");
                                                Session.IDINSTANT = $('#hdnIdInstants').val();
                                                Session.DESCRIPTIONINSTANT = $('#hdnDescriptionInstants').val();
                                                Session.ORIGINAPP = 'PREPAGO';
                                                var flag = 1;

                                                for (var i = 0; i < response.listInstant.length; i++) {
                                                    if (response.listInstant[0].Description === "CLIENTE TIENE PENDIENTE ACTUALIZACIÓN DE DATOS. Preguntar al cliente lo siguiente: Estimado cliente, usted es la persona que utiliza el servicio de manera permanente?. Si la respuesta es SI, indicar lo siguiente: Hemos verificado que necesitamos actualizar sus datos a fin de atender sus solicitudes sin ningún inconveniente Si la respuesta es NO, marcar No deseo ser titular") {

                                                        flag = 0;
                                                        break;
                                                    }
                                                }
                                                if (flag === 1) {
                                                    this.close();

                                                } else {
                                                    $.redirect.GetParamsData('SU_SIACA_INS', "PREPAGO");

                                                }

                                            }
                                        }

                                    },
                                    title: "Instantaneas",
                                    template: strUrlTemplate,
                                    url: strUrlInstantPrepaid,
                                    data: response,
                                    width: 900,
                                    height: 600,
                                    closeOnEscape: false

                                });

                            }
                        });
                    }
                    else {
                        var strUrlFillInstant = '~/Dashboard/Home/FillInstant';
                        var strUrlInstant = '~/Dashboard/Home/Instants';
                        var dataInstant = { strIdSession: Session.IDSESSION, strPhone: valPhone, strAccount: valAccount, strContract: valContract, strOriginType: valOriginType };
                        $.app.ajax({
                            async: false,
                            type: 'POST',
                            url: strUrlFillInstant,
                            data: dataInstant,
                            success: function (response) {
                                var n = 0;
                                if (response.listInstant.length) n = response.listInstant.length;
                                $('#spidnotify').html(n);
                                $.window.open({
                                    type: 'POST',
                                    modal: false,
                                    title: "Instantaneas",
                                    template: strUrlTemplate,
                                    url: strUrlInstant,
                                    data: response,
                                    width: 900,
                                    height: 600,
                                    buttons: {
                                        Cerrar: {
                                            click: function (sender, args) {
                                                this.close();
                                            }
                                        }
                                    }

                                });
                            }
                        });
                    }
                }
            },
            btnCerrarInstant_click: function (send, args) {
            },
            btnClean_click: function (send, args) {
                var controls = this.getControls();

                if (controls.txtCriteriaValue.val() != "") {
                    controls.txtCriteriaValue.val("");
                }
            },
            createMenu: function () {

                var that = this,
                    controls = this.getControls();
                $.blockUI({
                    message: controls.myModalLoad,
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

                $.app.ajax({
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    url: '~/Home/Logon',
                    data: null,
                    success: function (result) {
                        if (result.data.employee != null && result.data.menu != null && result.data.profiles != null) {
                            $("#txtCriteriaValue").attr('maxlength', result.SearchLength);
                            Session.USERACCESS = result.data.employee;
                            if (result.data.employee.optionPermissions != null) {
                                Session.USERACCESS.isOnlyOLO = that.isOnlyOLO(result.data.employee.optionPermissions);
                                Session.USERACCESS.canCancelInvoice = that.canCancelInvoice(result.data.employee.optionPermissions);
                            }
                            Session.IDSESSION = result.idSession;
                            Session.NODO = result.nroNodo;
                            that.toAssignSession(Session.IDSESSION, controls.idSession);
                            controls.nroNodo.html('');
                            controls.nroNodo.append('<b>' + Session.NODO + '</b>');
                            var i;

                            if (result.data != null) {
                                //Inicio Listar perfiles por usuario
                                that.tolistProfilesUser(result.data.profiles, controls.idulListPerfiles);
                                //Fin Listar perfiles por usuario

                                //Inicio de asignar nombre de usuario a etiqueta
                                that.toAssignUser(result.data.employee, controls.idUserName, controls.btnCriteria);

                                that.toFillCombo(result.data.options, controls.ddlBusquedaDesplegable)

                                var divButtonSearch = $('#div-button-search .dropdown-menu li a');
                                divButtonSearch.addEvent(that, 'click', that.btnValidateTypeSearch_click);
                                controls.txtCriteriaValue.val("");
                                if (result.data.options[0] != null) {
                                    controls.ddlTipoBusqueda.attr('value', result.data.options[0].code);



                                    $('#btnSearch').html(result.data.options[0].name + '<span class="caret"></span>');
                                    divButtonSearch = $('#div-button-search .dropdown-menu li a[data-value="' + result.data.options[0].code + '"]');

                                    divButtonSearch.data("value", result.data.options[0].code);
                                    for (i = 0; i < result.data.options.length; i++) {
                                        if (result.data.options[i].code == "1") {
                                            controls.ddlTipoBusqueda.attr('value', 1);
                                            $('#btnSearch').html('Teléfono  <span class="caret"></span>');
                                            divButtonSearch = $('#div-button-search .dropdown-menu li a[data-value="1"]');
                                            divButtonSearch.data("value", 1);

                                        }

                                    }
                                } else {
                                    $(".navbar-form").html("");

                                }
                                controls.txtCriteriaValue.focus();
                                that.btnValidateTypeSearch_click(divButtonSearch, null);

                                //Fin de Asignar nombre de usuario

                                result.data = that.newFieldData(result.data.menu);
                                Session.USERACCESS.optionPermissionsMenu = result.OptionPermissionsMenu;

                                for (i = 0; i < result.data.length; i++) {
                                    if (result.data[i].items != null) {
                                        result.data[i].items = that.addAttributeDescriptionAux(result.data[i].items, result.data[i].name);
                                    }
                                }

                                var menu = new Object();
                                var indiceMenu = 0;
                                var max = 0;

                                for (var i = 0, len = result.data.length; i < len; i++) {
                                    if (result.data[i].items != null) {
                                        if (max < result.data[i]["items"].length) {
                                            max = result.data[i]["items"].length;
                                            menu[0] = result.data[i];
                                            indiceMenu = i;
                                        }
                                    }
                                }

                                menu.length = 1;

                                for (i = 0; i < result.data.length ; i++) {
                                    if (i != indiceMenu) {
                                        if (result.data[i].items != null) {
                                            if (menu[0] != null) {
                                                menu[0].items = that.getReorderParent(menu[0].items, result.data[i].items);
                                                menu[0].items = menu[0].items.concat(result.data[i].items);
                                            }
                                        }
                                    }
                                }

                                if (menu[0] != null) {
                                    menu[0].items = that.getValidateOptionPopup(menu[0].items);
                                }


                                //Inicio Reordenar Items Menus por name
                                //Inicio Hijos Menus 
                                if (menu[0] != null) {
                                    for (i = 0; i < menu[0].items.length ; i++) {
                                        menu[0].items[i].items.sort(function (a, b) {
                                            var nameA = a.name.toLowerCase(), nameB = b.name.toLowerCase()
                                            if (nameA < nameB) //sort string ascending
                                                return -1
                                            if (nameA > nameB)
                                                return 1
                                            return 0 //default return value (no sorting)
                                        });
                                        //Inicio Nietos Menus
                                        for (var x = 0; x < menu[0].items[i].items.length ; x++) {
                                            if (menu[0].items[i].items[x].items != null) {
                                                menu[0].items[i].items[x].items.sort(function (a, b) {
                                                    var nameA = a.name.toLowerCase(), nameB = b.name.toLowerCase()
                                                    if (nameA < nameB) //sort string ascending
                                                        return -1
                                                    if (nameA > nameB)
                                                        return 1
                                                    return 0 //default return value (no sorting)
                                                });
                                            }

                                        }
                                        //Fin Nietos Menus
                                    }
                                }
                                //Fin Hijos Menus
                                //Fin Reordenar Items Menus por name      

                                var strMenu = that.getMenu(menu, "Y");
                                controls.idmMenu.html(strMenu);
                                $.SmartMenus.Bootstrap.init();

                                $('#divContenido').html('');
                                $.unblockUI();
                            }

                            that.resizeContent();
                        } else {


                            that.ShowMessageErrorAcces("Estimado Usuario, usted no tiene acceso al aplicativo");

                        }


                        if (typeof window.opener != 'undefined' && window.opener != null) {
                            window.opener.focus();
                            try {
                                var c = window.opener.open('', '_parent', '');

                                c.close();
                            } catch (ex) {

                            }

                        }

                    },
                    error: function (msg) {
                        $.unblockUI();
                        $.app.error({
                            id: 'divContenido',
                            message: msg,
                            click: function () { that.createMenu() }
                        });
                    }
                });
            },
            ShowMessageErrorAcces: function (text) {

                var cadHtmlError = '<div id="ContainerError"><div id="HeaderError"><div id="ImgHeaderError"><img src="/Images/graf_ico_error.gif" alt="Claro"></div></div>';
                cadHtmlError += '<div id="BodyError"><div id="ImgBodyError"><img src="/Images/img_marca.png" alt="Claro"></div><div id="TextBodyError">' + text + '</div></div></div>';
                $('#main-header').html('');
                $('#navbar-body').html(cadHtmlError);
                $('#navbar-body').css("padding-top", 70);


            },
            toAssignUser: function (data, control1, control2) {
                control1.html('');
                control2.html('');
                var strFullName = '';
                var strIdCodigo = '';
                var strSite = '';
                strFullName = data.fullName;
                strIdCodigo = data.login;
                strSite = data.areaName;
                control1.append('<b>' + strIdCodigo + ' - ' + strFullName + '</b>');
                if (strSite != '') {
                    control2.append('<span class="glyphicon glyphicon-cog text-left" style="width:185px;"> <label style="font-family: Arial, Verdana; font-weight:bold;"> ' + strSite + ' </label> </span>');
                } else {
                    control2.append('');
                }

            },
            toAssignSession: function (data, control1) {
                control1.html('');
                control1.append('<b>Session ID: ' + data + '</b>');
            },
            toFillCombo: function (data, control1) {
                control1.html('');
                for (var i = 0; i < data.length; i++) {
                    if (data[i] != null) {
                        if (data[i].name === "Teléfono") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-earphone"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Cuenta") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-credit-card"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Contrato") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-list-alt"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Customer Id") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-king"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "N° Documento Identidad") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-eye-close"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Nombres") {
                            control1.append('<li><a data-value="' + data[i].code + '" onclick="showDialogSearch()"><span class="glyphicon glyphicon-tasks"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Razón Social") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-th-large"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Número de Recibo") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-book"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "ICCID (Número de Chip)") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-erase"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else if (data[i].name === "Cintillo") {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-tent"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                        else {
                            control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-th-list"></span> &nbsp; ' + data[i].name + '</a></li>');
                        }
                    }
                }


            },
            tolistProfilesUser: function (data, control) {

                for (var i = 0; i < data.length; i++) {
                    if (data[i] != null) {
                        control.append('<li><a data-value="' + data[i].id + '"><span class="glyphicon glyphicon-chevron-right"></span>' + data[i].name + '</a></li>');
                    }
                }


            },
            createMenuDefaultOLO: function () {
                var $ul = $('<ul>');
                $ul.addClass('nav navbar-nav');

                var $li1 = $('<li>'),
                $a1 = $('<a>');
                $a1.html('Transacciones por Servicio');
                $li1.addClass('has-sub').append($a1);

                var $li2 = $('<li>'),
               $a2 = $('<a>');
                $a2.html('Consultas');
                $li2.addClass('has-sub').append($a2);

                $ul.append($li1).append($li2);
                return $ul;
            },

            createMenuDefault: function (data, codeOption, counter) {

                if (data != null && data.length > 0) {

                    var $ul = $('<ul>');

                    if (counter != 0)

                        $ul.addClass('dropdown-menu');

                    else

                        $ul.addClass('nav navbar-nav');


                    for (var i = 0; i < data.length; i++) {
                        if (data[i].items != null || counter != 0) {
                            if (data[i].state === "1") {

                                var $li = $('<li>'),
                                    $a = $('<a TypeOptions="1" profile="' + data[i].code + '">');

                                if (data[i].items != null && data[i].items.length > 0) {

                                    $a.html(data[i].name);

                                    $li
                                        .addClass('has-sub')
                                        .append($a)
                                        .append(this.createMenuDefault(data[i].items, null, 1));

                                }
                                else {

                                    if ((data[i].parentProduct) === "RECLAMOS") {
                                        $a
                                       .text(data[i].name)
                                       .addEvent(data[i], 'click', this.aMenuDefaultReclamos_click);
                                        $a.data('CodeValidate', 3);

                                    } else {

                                        if (data[i].code == "SU_ACP_GBC" || data[i].code == "SU_ACP_GCO" || data[i].code == "SU_ACP_CDC" || data[i].code == "SU_ACP_LMP" || data[i].code == "SU_ACP_GMB") {
                                            $a
                                           .text(data[i].name)
                                           .addEvent(data[i], 'click', this.aMenuDefaultReclamos_click);
                                            $a.data('CodeValidate', 3);
                                        } else {
                                            $a
                                               .text(data[i].name)
                                               .addEvent(data[i], 'click', this.openPopup_click);
                                            $a.data('CodeValidate', 1);
                                        }


                                    }
                                    $li.append($a);
                                }
                            }
                        }
                        $ul.append($li);
                    }
                }

                return $ul;
            },
            createMenuComplete: function (data, codeOption, counter) {

                if (data != null && data.length > 0) {

                    var $ul = $('<ul>');

                    if (counter != 0)

                        $ul.addClass('dropdown-menu');

                    else
                        $ul.addClass('nav navbar-nav');
                    for (var i = 0; i < data.length; i++) {

                        if (data[i].items != null || counter != 0) {

                            var $li = $('<li>'),
                                $a = $('<a TypeOptions="1" profile="' + data[i].code + '">');

                            if (data[i].items != null && data[i].items.length > 0) {

                                $a.html(data[i].name + '&nbsp; &nbsp;');

                                $li
                                    .addClass('has-sub')
                                    .append($a)
                                    .append(this.createMenuComplete(data[i].items, null, 1));

                            }
                            else {

                                if ((data[i].parentProduct) === "RECLAMOS") {
                                    $a
                                   .text(data[i].name)
                                   .addEvent(data[i], 'click', this.aMenuReclamos_click);
                                    $a.data('CodeValidate', 3);
                                } else if ((data[i].parentProduct) === "OLO") {

                                    if (data[i].url == null || data[i].url == '') {
                                        $a
                                       .text(data[i].name)
                                       .addEvent(data[i], 'click', this.aMenuComplete_click);


                                    } else {

                                        $a
                                       .text(data[i].name).on('click', data[i], this.aMenuOption_click)
                                        $a.data('CodeValidate', 4);

                                    }

                                }
                                else {
                                    $a
                                   .text(data[i].name)
                                   .addEvent(data[i], 'click', this.aMenuComplete_click);
                                    $a.data('CodeValidate', 4);
                                }

                                $li.append($a);


                            }
                        }
                        $ul.append($li);
                    }
                }

                return $ul;
            },
            enterValidationSearch: function () {

                var that = this,
                    controls = this.getControls();

                controls.txtCriteriaValue.keypress(function (e) {

                    if (e.which === 13) {
                        Session.EST = true;
                        that.validationSearch();
                    }


                });



            },
            getControls: function () {
                return this.m_controls || {};
            },
            getReorderParent: function (menu, data) {
                for (var i = 0; i < menu.length; i++) {
                    for (var j = 0; j < data.length; j++) {
                        if (menu[i].postfix === data[j].postfix && data[j].state === "1" && data[j].items != null) {
                            if (data[j].items != null) {
                                menu[i].items = this.getReorderParent(menu[i].items, data[j].items)
                                menu[i].items = menu[i].items.concat(data[j].items);
                            }
                        }
                    }

                }
                return menu;
            },
            getValidateOptionPopup: function (data) {
                var datai = data;
                var dataj = data;
                for (var i = 0; i < datai.length; i++) {

                    var buttons = [];
                    for (var j = i + 1; j < dataj.length; j++) {

                        if (datai[i].postfix === dataj[j].postfix && dataj[j].state === "1") {

                            dataj[j].state = '0';

                            if (datai[i].isDefault === true) {

                                if (datai[i].indicator === '') {
                                    buttons.push(datai[i]);
                                }

                                buttons.push(datai[j]);
                                datai[i].indicator = buttons;
                            }
                        }
                    }

                    if (datai[i].items != null)

                        datai[i].items = this.getValidateOptionPopup(datai[i].items);

                }
                return datai;
            },
            getMenu: function (data, indicator) {
                var $divNav = $('<div>'),
                    $divMenu = $('<div>');
                $divNav.addClass('nav');
                $divMenu
                    .attr('id', 'cssmenu')
                    .addClass('claro-black');

                for (var i = 0; i < data.length; i++) {

                    if (indicator === "Y") {
                        if (data[i] != null) {
                            $divMenu.append(this.createMenuDefault(data[i].items, null, 0));
                        }
                        else {

                            //MENU DEFAULT OLO
                            $divMenu.append(this.createMenuDefaultOLO());
                            break;
                        }

                    } else {

                        $divMenu.append(this.createMenuComplete(data[i].items, null, 0));

                    }
                }

                $divNav
                .append($divMenu)

                return $divNav;
            },
            initialiceValidationSearch: function (send) {
                var $element = send,
                    id = $element.data("value"),
                    controls = this.getControls();
                if ($.inArray(parseInt(id), [2]) != -1) {
                    $.onlyNumbersPoint(controls.txtCriteriaValue);
                }

                if ($.inArray(id, [1, 3, 4, 5, 9]) != -1) {
                    $.onlyNumbers(controls.txtCriteriaValue);

                }

                if ($.inArray(id, [6]) != -1) {
                    $.onlyNumbersLetters(controls.txtCriteriaValue);
                }

                if ($.inArray(id, [7]) != -1) {
                    $.onlyLettersSpaces(controls.txtCriteriaValue);
                }

                if ($.inArray(id, [8]) != -1) {
                    $.onlyNumbersLettersLine(controls.txtCriteriaValue);
                }
                controls.txtCriteriaValue.val("").focus();

            },
            newFieldData: function (data) {

                for (var i = 0; i < data.length; i++) {
                    data[i].state = "1";
                    data[i].indicator = "";
                    data[i].postfix = data[i].code.substr(parseInt(data[i].code.length - 4), 4);
                    if (data[i].items != null && data[i].items.length > 0) {
                        data[i].items = this.newFieldData(data[i].items);
                    }
                }
                return data;
            },
            openPopup_click: function (send, args) {

                var that = this;
                var strUrl = '~/Home/OptionsDefault';


                if (that.indicator != '') {
                    $.window.open({
                        buttons: {
                            Aceptar: {
                                click: function () {

                                    $.redirect.GetParamsData(Session.code, Session.ORIGINAPP);

                                    this.close();
                                }
                            },
                            Cancelar: {
                                click: function (sender, args) {
                                    this.close();
                                }
                            }
                        },
                        modal: false,
                        title: "Seleccione Producto",
                        url: strUrl,
                        width: 500,
                        height: 560,
                        maximize: false,
                        minimize: false
                    });

                    Session.btnDynamic = that.indicator;

                } else {
                    $.redirect.GetParamsData(that.code.toString(), Session.ORIGINAPP);

                }

            },
            searchbyDocumentNumber: function () {
                if (Session.CUSTOMERPRODUCT != null) {
                    var strUrl = '~/Dashboard/Home/Products';

                    $.window.open({
                        type: 'post',
                        modal: false,
                        title: "CONSULTA PRODUCTOS",
                        url: strUrl,
                        width: 900,
                        height: 600,
                        buttons: {
                            Cerrar: {
                                click: function (sender, args) {
                                    this.close();
                                }
                            }
                        }
                    });
                }



            },
            setControls: function (value) {
                this.m_controls = value;
            },
            sliderToolBar: function () {
                $('#carousel_ul li:first').before($('#carousel_ul li:last'));

                $('#right_scroll span').on('click', function () {
                    var item_width = $('#carousel_ul li').outerWidth() + 10,
                        left_indent = parseInt($('#carousel_ul').css('left')) - item_width;

                    $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
                        $('#carousel_ul li:last').after($('#carousel_ul li:first'));
                        $('#carousel_ul').css({ 'left': '-10px' });
                    });
                });

                $('#left_scroll span').on('click', function () {
                    var item_width = $('#carousel_ul li').outerWidth() + 10,
                        left_indent = parseInt($('#carousel_ul').css('left')) + item_width;

                    $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
                        $('#carousel_ul li:first').before($('#carousel_ul li:last'));
                        $('#carousel_ul').css({ 'left': '-10px' });
                    });
                });
            },

            HomologarSIAC_OLO: function () {
                var oCustomer = Session.DATACUSTOMER;
                var oClientType = Session.DATACUSTOMER.clienteType;
                var oListaCuentas = Session.DATACUSTOMER.listaCuentas;
                var linea = oListaCuentas.cuentas[0].listaAcuerdos.acuerdos[0].numero.numero;

                linea = (linea.indexOf("51") == 0 ? linea.replace("51", "") : linea);

                oCustomer.TelephoneCustomer = linea;
                oCustomer.Telephone = linea;
                oCustomer.FullName = oClientType.nombre + ' ' + oClientType.apePaterno + ' ' + oClientType.apeMaterno;
                oCustomer.LastName = oClientType.apePaterno + ' ' + oClientType.apeMaterno;
                oCustomer.Name = oClientType.nombre;
                oCustomer.Address = oClientType.direccion;
                oCustomer.DNIRUC = oClientType.numeroDoc;
                Session.PSTRTELEFONO = linea;
                oCustomer.DNIRUC = oClientType.numeroDoc;

            },

            GetCustomerICCID: function (valSearchValue) {
                var that = this, controls = that.getControls();
                var objCustomerRequest = {
                    CustomerById: {
                        numeroCuenta: valSearchValue
                    },
                }

                $.app.ajax({
                    async: false,
                    type: 'POST',
                    dataType: 'json',
                    data: { strIdSession: Session.IDSESSION, objCustomerRequest: objCustomerRequest },
                    url: '/SISOMV/Dashboard/Olo/GetCustomerICCID',
                    success: function (response) {
                        if (response != null && response.data.responseStatus.codigoRespuesta == 0) {
                            if (response.data.responseData.clienteType != null) {
                                Session.DATACUSTOMER = response.data.responseData;
                                Session.ORIGINTYPE = "OLO";
                                that.HomologarSIAC_OLO();
                                that.SearchUserOloInteraction();
                                that.getPointSale();
                            }

                        }
                        $.unblockUI();
                    },
                    error: function (msger) {
                        $.unblockUI();
                    }
                });
            },

            GetCustomerOlo: function (valSearchValue) {
                var that = this, controls = that.getControls();
                if (valSearchValue.length == 9) {
                    valSearchValue = '51' + valSearchValue;
                }
                var objCustomerRequest = {
                    CustomerById: {
                        numTelefono: valSearchValue
                    },
                    Flag: 'ById'
                }

                $.app.ajax({
                    async: false,
                    type: 'POST',
                    dataType: 'json',
                    data: { strIdSession: Session.IDSESSION, objCustomerRequest: objCustomerRequest },
                    url: '/SISOMV/Dashboard/Olo/GetCustomer',
                    success: function (response) {
                        if (response != null && response.data != null && response.data.responseStatus.codigoRespuesta == 0) {
                            if (response.data.responseData.clienteType != null) {
                                that.SearchUserOloInteraction();
                                Session.DATACUSTOMER = response.data.responseData;
                                Session.ORIGINTYPE = "OLO";
                                that.HomologarSIAC_OLO();
                                that.getPointSale();
                            }

                        }
                    },
                    error: function (msger) {
                        $.unblockUI();
                    }
                });
            },

            getPointSale: function () {
                var usuario = Session.USERACCESS.login;
                if (usuario) {
                    var request = {
                        user: {
                            codigoUsuario: usuario
                        }
                    }
                    $.app.ajax({
                        type: 'POST',
                        url: '~/SISOMV/Dashboard/Olo/GetPointSale',
                        dataType: 'json',
                        async: false,
                        data: { strIdSession: Session.IDSESSION, objPointSaleRequestSale: request },
                        success: function (result) {
                            if (result.data != null && result.data.codigoRespuesta == '0') {
                                Session.POINTSALE = result.data.listaPuntosventa["0"];
                            }
                        },
                        error: function (ex) {
                        }
                    });
                }
            },

            SearchUserOloInteraction: function () {
                var that = this, controls = that.getControls();
                var obj = {
                    searchUser: {
                        codigoUsuario: Session.USERACCESS.login
                    }
                };
                $.app.ajax({
                    type: 'POST',
                    url: '~/SISOMV/Dashboard/OloInteraction/SearchUserInteraction',
                    dataType: 'json',
                    cache: false,
                    async: false,
                    data: { strIdSession: Session.IDSESSION, request: obj },
                    success: function (result) {
                        if (result.data.codigoRespuesta == '0' && result.data.buscarUsuarioResponse != null && result.data.buscarUsuarioResponse.length > 0) {
                            Session.USERACCESS.Detail = result.data.buscarUsuarioResponse[0];
                        }
                    },
                    error: function (ex) {
                    }
                });
            },

            GetOptions: function () {
                var that = this;
                var strUrlMenu = '~/Home/GetOptions';
                var myOptions = null;
                $.app.ajax({
                    type: "POST",
                    url: strUrlMenu,
                    data: { strIdSession: Session.IDSESSION, strApplicationType: Session.ORIGINTYPE, strUserId: Session.USERACCESS.userId },
                    async: false,
                    complete: function () {
                        that.sliderToolBar();
                    },
                    success: function (resultdata) {

                        var strMenu = '';
                        var i;
                        if (resultdata.data != null) {
                            for (i = 0; i < resultdata.data.menu.length; i++) {
                                if (resultdata.data.menu[i].items != null) {
                                    resultdata.data.menu[i].items = that.addAttributeParentProduct(resultdata.data.menu[i].items, resultdata.data.menu[i].name);
                                }
                            }

                            if (Session.ORIGINTYPE === "OLO") {
                                strMenu = that.getMenu(resultdata.data.menu.slice(0, 1), 'N');
                            }
                            else {
                                strMenu = that.getMenu(resultdata.data.menu, 'N');
                            }

                            Session.MENU[Session.ORIGINTYPE] = strMenu;
                            $('#idmMenu').html(strMenu);
                            $.SmartMenus.Bootstrap.init();
                            var strToolbar = '';
                            if (resultdata.data.toolbar != null) {
                                strToolbar = that.getToolbar(resultdata.data.toolbar);
                                var parentProduct = "";
                                if (Session.ORIGINTYPE === "POSTPAID") {
                                    if ($('#ddlTipoBusqueda').val() === "1") {

                                        Session.BUSQINSTANT = "Telefono";

                                    } else if ($('#ddlTipoBusqueda').val() === "2") {
                                        Session.BUSQINSTANT = "Cuenta";
                                    }
                                }
                                if (Session.ORIGINTYPE === "POSTPAID" || Session.ORIGINTYPE === "DTH") parentProduct = "POSTPAGO";
                                else if (Session.ORIGINTYPE === "PREPAID") parentProduct = "PREPAGO";
                                else parentProduct = Session.ORIGINTYPE;

                                for (i = 0; i < resultdata.data.toolbar.length; i++) {
                                    if (resultdata.data.toolbar != null) {
                                        resultdata.data.toolbar = that.addAttributeParentProduct(resultdata.data.toolbar, parentProduct);
                                    }
                                }

                                $('#idmToolbar').html(strToolbar);

                                myOptions = resultdata.data;
                            }

                        } else {
                            myOptions = null;
                        }
                    },
                    error: function (msg) {
                        myOptions = null;
                    }
                });
                return myOptions;
            },
            displayContentOlo: function () {

                var strUrlContent = '~/SISOMV/Dashboard/Olo/Index';

                $.app.ajax({
                    type: "POST",
                    url: strUrlContent,
                    success: function (result3) {
                        var content;

                        if (!$.string.isEmptyOrNull(result3)) {
                            $.window.close();
                            content = result3;
                        } else {
                            $(this).focus();
                            $('#txtCriteriaValue').val('');
                            modalAlert('No se Encontraron Resultados', this.strTitleMessage);
                            content = "";
                        }

                        $('#divContenido').html(content);
                        $.unblockUI();
                    },
                    error: function (msg) {
                        $.unblockUI();
                    }

                });
            },
            validationSearch: function (Params) {
                var that = this,
                    controls = this.getControls();

                if (typeof (Params) !== 'undefined') {
                    controls.txtCriteriaValue.val(Params.ValueShearch);
                    controls.ddlTipoBusqueda.attr('value', Params.Type);
                    if (Params.Type === 1) {
                        $('#btnSearch').html('Teléfono  <span class="caret"></span>');
                    } else {
                        $('#btnSearch').html('Contrato  <span class="caret"></span>');
                    }

                }

                $('#spidnotify').html('');

                Session.CUSTOMERPRODUCT = null;

                Session.TELEPHONE = controls.txtCriteriaValue.val();
                controls.uiBarra.html("");

                var valSearchType = controls.ddlTipoBusqueda.val(),
                 valSearchValue = controls.txtCriteriaValue.val(),
                 strUrl,
                 strUrlTemplate = '/Home/DialogTemplate',
                 strTitleMessage = 'Busqueda';

                Session.SEARCHTYPE = valSearchType;
                var data;

                ////////CLIENTE OLO///////////
                if (that.validateOloCustomer(valSearchType, valSearchValue)) {

                    $.blockUI({
                        message: controls.myModalLoad,
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

                    Session.DATACUSTOMER = null;

                    //Busqueda por numero OLO
                    if (valSearchType === '1') {
                        that.GetCustomerOlo(valSearchValue);
                        if (Session.DATACUSTOMER != null && Session.DATACUSTOMER.clienteType != null) {
                            if (that.GetOptions() != null) {
                                that.displayContentOlo();
                                return;
                            }

                        }
                    }

                    // Busqueda ICCID(Nro Chip)
                    if (valSearchType === '9') {
                        that.GetCustomerICCID(valSearchValue);
                        if (Session.DATACUSTOMER != null && Session.DATACUSTOMER.clienteType != null) {
                            if (that.GetOptions() != null) {
                                that.displayContentOlo();
                                return;
                            }

                        }
                    }

                }
                /////////FIN CLIENTE OLO//////////


                // Validar Tipo de Busqueda Cliente Claro
                // Validar si el número ingresado concuerda con el tipo de búsqueda
                if (that.validate(valSearchType, valSearchValue)) {

                    $.blockUI({
                        message: controls.myModalLoad,
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

                    if (parseInt(Session.SEARCHTYPE) == 4 || parseInt(Session.SEARCHTYPE) == 2) {
                        Session.FlagRedirect = Session.EST;
                    } else {
                        Session.FlagRedirect = false;
                        Session.EST = false;
                    }
                    if (valSearchType === '1' || valSearchType === '2' || valSearchType === '3' || valSearchType === '4' || valSearchType === '8' || valSearchType === '9' || valSearchType === '10') {

                        if (valSearchType === "8" && (valSearchValue.startsWith("AM") || valSearchValue.startsWith("SB")) && valSearchValue.indexOf("-") > -1) {

                            var rec1 = valSearchValue.substring(0, 4);
                            var rec2 = valSearchValue.substring(5, valSearchValue.lenght);
                            valSearchValue = rec1.concat(rec2);
                        }

                        if (!that.ValidateMigration(valSearchType, valSearchValue)) {
                            //---ini 
                        strUrl = '~/Home/ValidateQuery';

                            console.log(2);
                        data = { strIdSession: Session.IDSESSION, strSearchType: valSearchType, strSearchValue: valSearchValue, NotEvalState: false, FlagSearchType: true, userId: Session.USERACCESS.userId, IsPrepaid: false, IsPostPaid: false }
                        $.app.ajax({
                            type: "POST",
                            dataType: "json",
                            url: strUrl,
                            data: data,
                            complete: function () {
                                if (data.strSearchType === '4' || data.strSearchType === '8') {

                                    $('#btnSearch').html('   Cuenta <span class="caret"></span>');
                                    controls.ddlTipoBusqueda.attr("value", 2);
                                    if (Session.DATACUSTOMER != null && typeof (Session.DATACUSTOMER) != "undefined")
                                        controls.txtCriteriaValue.val(Session.DATACUSTOMER.Account);
                                }

                                if (valSearchType === '9' && Session.DATACUSTOMER) {
                                    Session.DATACUSTOMER.ValueSearch = Session.DATACUSTOMER.Telephone;


                                }

                            },
                            success: function (result) {

                                Session.flagPrePagoDesac = false;
                                Session.strMsjValidationPre = "";
                                if (typeof (result.strMsjValidation) != 'undefined' && result.strMsjValidation != null && result.strMsjValidation != '') {
                                    Session.strMsjValidationPre = result.strMsjValidation;


                                    modalAlert(result.strMsjValidation, "Alerta Prepago", function () {

                                        if (Session.strMsjValidationPre.indexOf("baja") > 0 && (result.OriginType == "PREPAID" || result.OriginType == "PREPAGO")) {
                                            if (result.data == null) {
                                                $('#divContenido').html("");
                                                $('#idmToolbar').html('');
                                                $.unblockUI();
                                                that.createMenu();
                                                return false;
                                            }
                                            Session.flagPrePagoDesac = true;
                                            that.builtContent({ paramResult: result, paramSearchValue: valSearchValue, paramSearchType: valSearchType });
                                            Session.GETIGV = result.Igv;


                                            $("#carousel_ul li a").each(function () {

                                                if ($(this).attr("profile") != "SU_SIACA_BLIEQ" && $(this).attr("profile") != "SU_SIACA_DLIEQ" && $(this).attr("profile") != "SU_SIACA_TRECL" && $(this).attr("profile") != "SU_SIACA_TSAR" && $(this).attr("profile") != "SU_SIACA_INON") {
                                                    $(this).addClass("disabled");
                                                    $(this).attr("style", "max-width: 95px; pointer-events: none; color: rgba(60, 50, 50, 0.4);");
                                                }
                                            });

                                        }
                                    });


                                    return false;
                                }
                                if (result.Options == null) {
                                    $('#divContenido').html("");
                                    modalAlert("Usted no está autorizado para consultar este tipo de producto");
                                    return false;
                                }
                                if ((result.OriginType != "" && result.data != null)) {
                                    if (result.OriginType != "NOPRECISADO") {
                                        that.builtContent({ paramResult: result, paramSearchValue: valSearchValue, paramSearchType: valSearchType });
                                        Session.GETIGV = result.Igv;
                                    }
                                    else {
                                        $(this).focus();
                                        $('#txtCriteriaValue').val('');
                                        modalAlert('No se encontraron resultados', strTitleMessage);
                                        $('#divContenido').html("");
                                    }
                                } else {
                                    $(this).focus();
                                    $('#txtCriteriaValue').val('');
                                    modalAlert('No se encontraron resultados', strTitleMessage);
                                    $('#divContenido').html("");
                                }

                            },
                            error: function (msger) {
                                $.app.error({
                                    id: 'divContenido',
                                    message: msger,
                                    click: function () { that.validationSearch() }
                                });
                            }
                        });
                    }
                    //---fin
                        }



                    else if (valSearchType === "5") {
                        //Validación previa para saber el tipo de opción


                        var valTypeSearchCode = '1',
                            valValueSearch = $('#txtCriteriaValue').val();


                        data = { strIdSession: Session.IDSESSION, strSearchType: valTypeSearchCode, strSearchValue: valValueSearch };

                        $.app.ajax({
                            type: "POST",
                            dataType: "json",
                            url: '~/Dashboard/Home/CustomerValidate',
                            data: data,
                            success: function (result) {
                                Session.CUSTOMERPRODUCT = {};
                                Session.CUSTOMERPRODUCT = result.data;
                                if (result.data != null) {
                                    if (result.data.listDataCustomerModel.length > 1) {
                                        $.window.open({
                                            buttons: {
                                                Seleccionar: {
                                                    click: function (sender, args) {
                                                        var modalJQuery = args.event.view.$;
                                                        var selectedData = modalJQuery('#tblCustomersDocument', '#contenedor-customers-document').DataTable().rows({ selected: true }).data();

                                                        //  var selectedData = $('#tblCustomersDocument', '#contenedor-customers-document').DataTable().rows({ selected: true }).data();

                                                        if ($.array.isEmptyOrNull(selectedData)) {
                                                            showAlert('Debe seleccionar un cliente.', strTitleMessage);
                                                            return;
                                                        }

                                                        var data = selectedData[0],
                                                            DocumentType = data.DocumentType.trim(),
                                                            DocumentIdentity = data.DocumentIdentity.trim(),
                                                            Names = data.Names.trim();

                                                        Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                                                        Session.CUSTOMERPRODUCT.DocumentType = DocumentType;
                                                        Session.CUSTOMERPRODUCT.DocumentIdentity = DocumentIdentity;
                                                        Session.CUSTOMERPRODUCT.Names = Names;

                                                        this.close();

                                                        $.window.open({
                                                            modal: false,
                                                            title: "Consulta de Productos",
                                                            url: '~/Dashboard/Home/Products',
                                                            width: 900,
                                                            height: 600,
                                                            buttons: {
                                                                Cerrar: {
                                                                    click: function (sender, args) {
                                                                        this.close();
                                                                    }
                                                                }
                                                            }
                                                        });
                                                    }
                                                },
                                                Cancelar: {
                                                    click: function () {

                                                        that.getControls().ddlTipoBusqueda.val('');
                                                        that.getControls().txtCriteriaValue.val('');
                                                        that.getControls().btnSearch.html("Buscar");

                                                        this.close();

                                                    }
                                                }
                                            },
                                            type: 'POST',
                                            modal: false,
                                            title: "Consulta de Clientes",
                                            url: '~/Dashboard/Home/CustomersDocument',
                                            width: 900,
                                            height: 500,
                                        });
                                    }
                                    else {
                                        Session.CUSTOMERPRODUCT.isRedirecBussines = 'not';
                                        $.window.open({
                                            type: 'post',
                                            modal: false,
                                            title: 'Consulta de Productos',
                                            template: strUrlTemplate,
                                            url: '~/Dashboard/Home/Products',
                                            width: 900,
                                            height: 600,
                                            buttons: {
                                                Cerrar: {
                                                    click: function () {
                                                        this.close();
                                                    }
                                                }
                                            }
                                        });

                                    }
                                }
                                else {
                                    modalAlert("No se pudo realizar la consulta de productos");
                                }
                            },
                            error: function (msger) {

                                modalAlert('El nro de documento de identidad ingresado no devuelve registro alguno.', strTitleMessage);
                                $('#divContenido').html('');
                                return;
                            }
                        });
                    }
                    else if (valSearchType === "7") {


                        $.window.open({
                            modal: false,
                            title: "CONSULTA DE CLIENTES",
                            url: '~/Dashboard/Home/CustomersBusinessNames',
                            data: { strBusqueda: valSearchValue, strApellido: "", idModConsul: "1" },
                            width: 900,
                            height: 550,
                            buttons: {
                                Seleccionar: {
                                    click: function (sender, args) {
                                        var modalJQuery = args.event.view.$;
                                        var controls = modalJQuery('#contenedor-customerBusinessNames').form('getControls');

                                        var dataRow = controls.tblDatosCliente.DataTable().row({ selected: true }).data();

                                        if (!$.isEmptyObject(dataRow)) {
                                            Session.CUSTOMERPRODUCT = {};
                                            Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                                            Session.CUSTOMERPRODUCT.DocumentType = dataRow.DocumentType;
                                            Session.CUSTOMERPRODUCT.DocumentIdentity = dataRow.DocumentIdentity;
                                            Session.CUSTOMERPRODUCT.Names = dataRow.Names;

                                            $.window.open({
                                                modal: false,
                                                title: "CONSULTA PRODUCTOS",
                                                url: '~/Dashboard/Home/Products',
                                                width: 900,
                                                height: 600,
                                                buttons: {
                                                    Cerrar: {
                                                        click: function () {
                                                            this.close();
                                                        }
                                                    }
                                                }
                                            });

                                            this.close();
                                        } else {
                                            modalAlert('Debe seleccionar un cliente.', strTitleMessage);
                                            return;
                                        }
                                    }

                                },
                                Cancelar: {
                                    click: function () {

                                        this.close();

                                    }
                                }
                            }
                        });
                        $.unblockUI();
                        return;
                    }
                }
            },
            ValidateMigration: function (tipoBusqueda, valorBusqueda) {
                if (tipoBusqueda == 2) {
                    tipoBusqueda = 4;
                } else if (tipoBusqueda == 4) {
                    tipoBusqueda = 2;
                }
                var resultado = false;
                var strUrlContent = '~/Home/ValidateMigration';
                var data = {
                    strIdSession: Session.IDSESSION,
                    strValue: valorBusqueda,
                    strType: tipoBusqueda
                }

                $.app.ajax({
                    type: "POST",
                    url: strUrlContent,
                    data: data,
                    async:false,
                    success: function (data) {
                        console.log(1);
                        console.log(data);
                        resultado = data.estado;
                        if (resultado) {
                            modalAlert(data.mensaje, 'Mensaje');
                            $('#divContenido').html("");
                        }
                    },
                    error: function (msg) {
                        consol.log("Error validateMigration " + msg);
                    }
                });
                return resultado;
            },


            validateOloCustomer: function (valSearchType, valSearchValue) {
                var State = true;
                var strTitleMessage = "Buscar";
                var controls = this.getControls();

                if (valSearchType === '') {
                    State = false;

                    modalAlert('Seleccione un tipo de busqueda e ingrese un criterio de busqueda valido.', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                // Validando Nro OLO
                if (valSearchType === '1') {

                    if (valSearchValue === '') {
                        State = false;
                    }

                    else if ((valSearchValue.length < 8 || valSearchValue.length > 9)) {
                        State = false;

                    }
                }

                // Validando el ICCID(Nro Chip)
                if (valSearchType === '9') {
                    if (valSearchValue === '') {
                        State = false;
                    }

                    if (valSearchValue.length > 24) {
                        State = false;
                    }
                }


                return State;
            },




            isOnlyOLO: function (permissions) {
                var userOlo = true;
                var array = permissions.split(',').slice(0, -1);;
                $.each(array, function (key, value) {
                    if (value.indexOf("SU_OLO") < 0 && value != "") {
                        userOlo = false;
                        return;
                    }
                });
                return userOlo;
            },

            //PROY-140464 - INI
            canCancelInvoice: function (permissions) {
                var canICancelInvoice = false;
                var array = permissions.split(',').slice(0, -1);;
                $.each(array, function (key, value) {
                    if (value.indexOf("SU_ACP_ANJ") > 1 || value.indexOf("SU_HFC_ANJ") > -1 || value.indexOf("SU_IFI_ANJ") > -1) {
                        canICancelInvoice = true;
                        return;
                    }
                });
                return canICancelInvoice;
            },
            //PROY-140464 - FIN

            validate: function (valSearchType, valSearchValue) {
                var State = true;
                var strTitleMessage = "Buscar";
                var controls = this.getControls();

                if (valSearchType === '') {
                    State = false;

                    modalAlert('Seleccione un tipo de busqueda e ingrese un criterio de busqueda valido.', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                // Validando Nro de Teléfono
                if (valSearchType === '1') {

                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar un Número de Teléfono', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }

                    else if ((valSearchValue.length < 8 || valSearchValue.length > 11)
                            || (valSearchValue.length === 9 && valSearchValue.substr(0, 1) != '9' && valSearchValue.substr(0, 2) != '21' && valSearchValue.substr(0, 1) != '8')) {
                        State = false;

                        modalAlert('Número de Teléfono Inválido.', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                        controls.divContenido.html('');

                    }

                    if (valSearchValue.substr(0, 1) === '0') {
                        State = false;

                        modalAlert('Número Teléfono Inválido, no debe iniciar con Cero(0).', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando Nro Cuenta 
                if (valSearchType === '2') {

                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar el Número de la Cuenta', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }

                    if (valSearchValue.length > 24) {
                        State = false;

                        modalAlert('Número de Cuenta Inválido', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando Nro Contrato 
                if (valSearchType === '3') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar el Número de Contrato', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando el Customer Id 
                if (valSearchType === '4') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar el Customer ID', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }

                    if (valSearchValue.length > 24) {
                        State = false;

                        modalAlert('Customer ID Inválido', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando Nro Documento de Identidad 
                if (valSearchType === '5') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar un Número de Documento', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                    if (valSearchValue.length > 20 || valSearchValue.length < 8) {
                        State = false;
                        modalAlert('Debe Ingresar un Número de Documento Correcto', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando Razón Social
                if (valSearchType === '7') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar una Razón Social', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando el Customer Id 
                if (valSearchType === '8') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar el Número de Recibo', strTitleMessage);
                        $.unblockUI();
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }


                    if (valSearchValue.length > 24) {
                        State = false;

                        modalAlert('Número Recibo Inválido', strTitleMessage);
                        $.unblockUI();
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando el ICCID(Nro Chip)
                if (valSearchType === '9') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar el ICCID(Número de chip)', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }

                    if (valSearchValue.length > 24) {
                        State = false;

                        modalAlert('Nro ICCID Inválido', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }

                // Validando el Cintillo
                if (valSearchType === '10') {
                    if (valSearchValue === '') {
                        State = false;

                        modalAlert('Debe Ingresar un Cintillo', strTitleMessage);
                        $(this).focus();
                        controls.txtCriteriaValue.val('');
                    }
                }
                if (State === false) {
                    controls.idmToolbar.html('');


                }
                return State;
            },

            createToolbar: function (data) {

                var $ul = $('<ul>', {
                    id: 'carousel_ul',
                    class: 'carousel-ul'
                })

                for (var i = 0; i < data.length; i++) {
                    var n = data[i].name.length;
                    var max;
                    if (n <= 18) { max = "95px;" } else { max = "120px;" }
                    if (data[i].code === 'SU_ACP_INTER' || data[i].code === 'SU_ACP_INTERA') { max = "101px;" }
                    var $li = $('<li style="vertical-align:middle;" >'),
                    $a = $('<a typeoptions="2" style="max-width:' + max + '" profile="' + data[i].code + '"> '),

                    $span = $('<span>');

                    $span
                        .addClass('glyphicon glyphicon-th-large')
                        .css('margin-right', '2px');

                    if (Session.ORIGINTYPE == "OLO") {
                        if (data[i].url == null || data[i].url == '') {

                            $a
							.append($span)
							.append('<b>' + data[i].name + '</b>')
							.addEvent(data[i], 'click', this.acreateToolbar_click);

                        } else {
                            $a
                           .append($span)
                           .append('<b>' + data[i].name + '</b>').on('click', data[i], this.aMenuOption_click);
                        }
                    } else {
                        $a
                        .append($span)
                        .append('<b>' + data[i].name + '</b>')
                        .addEvent(data[i], 'click', this.acreateToolbar_click);
                    }




                    $a.data('CodeValidate', 2);
                    $.DisabledToolbar($a, data[i].code, Session.ORIGINTYPE);
                    $li.append($a);
                    $ul.append($li);
                }

                return $ul;
            },
            SplitName: function (name) {
                var strNewNaame = "";
                var chars = null;

                if (name != null && name.length > 0) {
                    chars = name.split(" ");


                    var n;
                    if (chars != null && chars.length > 1) {

                        if (chars.length > 2) {


                            for (n = 0; n <= chars.length - 1; n++) {
                                if (n < 2) {
                                    strNewNaame = strNewNaame + chars[n] + " "
                                } else if (n === 2) {

                                    if (n === chars.length - 1) {
                                        strNewNaame = strNewNaame + chars[n];
                                    } else if (n < chars.length - 1) {
                                        strNewNaame = strNewNaame + chars[n] + "<br/>";
                                    }
                                } else {

                                    if (n === chars.length - 1) {
                                        strNewNaame = strNewNaame + chars[n]
                                    } else {
                                        strNewNaame = strNewNaame + chars[n] + " "
                                    }
                                }
                            }
                        } else {

                            for (n = 0; n <= chars.length - 1; n++) {
                                if (n === 0) {
                                    strNewNaame = strNewNaame + chars[n] + "<br/>";
                                } else {
                                    strNewNaame = strNewNaame + chars[n]
                                }
                            }
                            strNewNaame = strNewNaame + "<br/>";
                        }




                    } else { strNewNaame = name + "<br/>"; }
                }

                return strNewNaame;

            },
            acreateToolbar_click: function (send, args) {
                if (Session.ORIGINTYPE != '') {
                    if (Session.ORIGINTYPE === "POSTPAID" || Session.ORIGINTYPE === "DTH" || Session.ORIGINTYPE === "HFC" || Session.ORIGINTYPE === "LTE" || Session.ORIGINTYPE === "OLO") {
                        if (Session.DATASERVICE === null) {
                            $.app.ajax({
                                async: false,
                                type: 'POST',
                                url: '~/Dashboard/Postpaid/DataServiceContent',
                                data: {
                                    strIdSession: Session.IDSESSION,
                                    strContratoID: Session.DATACUSTOMER.ContractID,
                                    strApplication: Session.DATACUSTOMER.Application,
                                    strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                                    strDocumentType: Session.DATACUSTOMER.DocumentType,
                                    strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                                    strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                                    strIsLTE: Session.DATACUSTOMER.IsLTE,
                                    strphone: Session.DATACUSTOMER.ValueSearch,
                                    plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                                    strCustomerId: Session.DATACUSTOMER.CustomerID,
                                    flagconvivencia: Session.DATACUSTOMER.flagConvivencia,
                                    coIdPub: Session.DATACUSTOMER.coIdPub

                                },
                                success: function (response) {
                                    Session.DATASERVICE = response.data;


                                },
                                error: function (ex) {
                                    controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                                }
                            });
                        }
                    }
                    Session.ORIGINAPP = this.parentProduct;
                    Session.CO = this.id;
                    $.ValidateCode(this.code, Session.ORIGINAPP);
                }
            },
            getToolbar: function (data) {
                var $divContainer = $('<div>'),
                    $divInner = $('<div>'),
                    $divScrollLeft = $('<div>'),
                    $divScrollRight = $('<div>'),
                    $spanLeft = $('<span>'),
                    $spanRight = $('<span>');

                $divContainer.attr('id', 'carousel_container')

                $divScrollLeft
                                .attr('id', 'left_scroll')
                                .addClass('carousel-scroll');

                $spanLeft
                        .addClass('glyphicon glyphicon-chevron-left')
                        .attr('aria-hidden', 'true')

                $divScrollLeft.append($spanLeft);

                $divContainer.append($divScrollLeft);

                $divInner
                         .attr('id', 'carousel_inner')
                         .addClass('carousel-inner')
                         .attr("style", "width:98%;");


                $divInner.append(this.createToolbar(data));

                $divContainer.append($divInner);

                $divScrollRight
                                .attr('id', 'right_scroll')
                                .addClass('carousel-scroll');

                $spanRight
                         .addClass('glyphicon glyphicon-chevron-right')
                         .attr('aria-hidden', 'true')

                $divScrollRight.append($spanRight);

                $divContainer.append($divScrollRight);

                return $divContainer;

            },
            resizeContent: function () {
                var controls = this.getControls();

                controls.divMainBody.css('height', $(window).outerHeight() - controls.divMainHeader.outerHeight() - controls.divMainFooter.outerHeight());
            },
            builtContent: function (arResult) {

                var that = this;
                if (typeof arResult.paramResult === "string") {
                    arResult = JSON.parse(arResult.paramResult);
                    arResult.paramResult = arResult;
                    if (arResult.paramResult.OriginType != 'PREPAID') {
                        $('#btnSearch').html('   Cuenta <span class="caret"></span>');
                        $('#ddlTipoBusqueda').attr("value", 2);

                        $('#txtCriteriaValue').val(arResult.paramResult.data.Account);
                    } else {
                        $('#btnSearch').html('&nbsp;Teléfono <span class="caret"></span>');
                        $('#ddlTipoBusqueda').val(1);
                        $('#txtCriteriaValue').val(arResult.paramResult.data.TelephoneCustomer);
                    }

                } else {
                    var SearchType = parseInt($('#ddlTipoBusqueda').val());
                    if (SearchType === 9) {
                        $('#btnSearch').html('&nbsp;Teléfono <span class="caret"></span>');
                        $('#ddlTipoBusqueda').val(1);
                        $('#txtCriteriaValue').val(arResult.paramResult.data.Telephone);
                    }

                }
                Session.DATACUSTOMER = arResult.paramResult.data;
                if (Session.flagPrePagoDesac) Session.DATACUSTOMER.isTfiInac = $.validateIsTfi(Session.DATACUSTOMER.TelephoneCustomer);
                Session.DATASERVICE = Session.DATACUSTOMER.oDataService;
                if (parseInt(Session.SEARCHTYPE) == 4 || parseInt(Session.SEARCHTYPE) == 2) {
                    Session.FlagRedirect = Session.EST;
                } else {
                    Session.FlagRedirect = false;
                    Session.EST = false;
                }
                Session.CODERESPONSEPREPAIDSERVICE = arResult.paramResult.strCodeResponseService;


                var strUrlMenu = '~/Home/GetOptions';

                Session.ORIGINTYPE = arResult.paramResult.OriginType;
                Session.CUSTOMEROLD = (arResult.paramResult.OriginType === 'PREPAID' ? '1' : '0');
                Session.MESSAGEPREPAID = (arResult.paramResult.OriginType === 'PREPAID' ? '1' : '0');

                $.app.ajax({
                    type: "POST",
                    url: strUrlMenu,
                    data: { strIdSession: Session.IDSESSION, strApplicationType: Session.ORIGINTYPE, strUserId: Session.USERACCESS.userId },
                    async: false,
                    complete: function () {
                        that.sliderToolBar();
                    },
                    success: function (resultdata) {
                        var strMenu = '';
                        var i;
                        if (resultdata.data != null) {
                            for (i = 0; i < resultdata.data.menu.length; i++) {
                                if (resultdata.data.menu[i].items != null) {
                                    resultdata.data.menu[i].items = that.addAttributeParentProduct(resultdata.data.menu[i].items, resultdata.data.menu[i].name);
                                }
                            }

                            if (Session.ORIGINTYPE === "OLO") {
                                strMenu = that.getMenu(resultdata.data.menu.slice(0, 1), 'N');
                            }
                            else {
                                strMenu = that.getMenu(resultdata.data.menu, 'N');
                            }

                            Session.MENU[Session.ORIGINTYPE] = strMenu;
                            $('#idmMenu').html(strMenu);
                            $.SmartMenus.Bootstrap.init();
                            var strToolbar = '';
                            if (resultdata.data.toolbar != null) {
                                strToolbar = that.getToolbar(resultdata.data.toolbar);
                                var parentProduct = "";
                                if (Session.ORIGINTYPE === "POSTPAID") {
                                    if ($('#ddlTipoBusqueda').val() === "1") {

                                        Session.BUSQINSTANT = "Telefono";

                                    } else if ($('#ddlTipoBusqueda').val() === "2") {
                                        Session.BUSQINSTANT = "Cuenta";
                                    }
                                }
                                if (Session.ORIGINTYPE === "POSTPAID" || Session.ORIGINTYPE === "DTH") parentProduct = "POSTPAGO";
                                else if (Session.ORIGINTYPE === "PREPAID") parentProduct = "PREPAGO";
                                else parentProduct = Session.ORIGINTYPE;

                                for (i = 0; i < resultdata.data.toolbar.length; i++) {
                                    if (resultdata.data.toolbar != null) {
                                        resultdata.data.toolbar = that.addAttributeParentProduct(resultdata.data.toolbar, parentProduct);
                                    }
                                }

                                $('#idmToolbar').html(strToolbar);


                            }

                            if (arResult.paramSearchType === '3' || arResult.paramSearchType === '1') {

                                that.showDialogLoad();
                            }




                            $.app.ajax({
                                type: 'POST',
                                dataType: 'json',
                                url: '~/Home/GetPortability',
                                data: { strIdSession: Session.IDSESSION, vTelefono: arResult.paramSearchValue },
                                success: function (result) {
                                    var ArrayPortability = [];
                                    ArrayPortability = result.data;
                                    if (ArrayPortability.length > 0) {
                                        ArrayPortability.push(result.respuesta);
                                    }
                                    else if (ArrayPortability.length === 0) {
                                        ArrayPortability.push("");
                                        ArrayPortability.push(result.respuesta);
                                    }
                                    Session.PORTABILITY = ArrayPortability;
                                    Session.PORTABILITYRESPUESTA = result.respuesta;
                                },
                                error: function (msg) {

                                }
                            });

                            var strUrlContent = '~/Home/DisplayContent';

                            var dataContent = { strOriginType: arResult.paramResult.OriginType };

                            $.app.ajax({
                                type: "POST",
                                url: strUrlContent,
                                data: dataContent,
                                success: function (result3) {
                                    var content;

                                    if (!$.string.isEmptyOrNull(result3)) {
                                        $.window.close();
                                        content = result3;
                                    } else {
                                        $(this).focus();
                                        $('#txtCriteriaValue').val('');
                                        modalAlert('No se Encontraron Resultados', this.strTitleMessage);
                                        content = "";
                                    }

                                    $('#divContenido').html(content);

                                    if (Session.DATACUSTOMER.ProductTypeText === "PostPago - DTH") {
                                        //no muestra



                                        $("#btnPinPuk").hide();
                                        $("#lbl_PinPuk").hide();
                                    } else {



                                        $("#btnPinPuk").show();
                                        $("#lbl_PinPuk").show();
                                    }
                                },
                                error: function (msg) {

                                }

                            });



                        } else {
                            $('#divContenido').html("");
                            modalAlert("Usted no está autorizado para consultar este tipo de producto");
                        }
                    },
                    error: function (msg) {

                    }
                });


            }
        }

        $.fn.form = function () {
            var option = arguments[0],
                args = arguments,
                value,
                        allowedMethods = ['builtContent', 'showDialogLoad', 'getControls', 'validationSearch', 'GetOptions', 'displayContentOlo', 'GetCustomerOlo', 'createMenuDefaultOLO'];

            this.each(function () {
                var $this = $(this),
                    data = $this.data('form'),
                    options = $.extend({}, $.fn.form.defaults,
                        $this.data(), typeof option === 'object' && option);

                if (!data) {
                    data = new Form($this, options);
                    $this.data('form', data);
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

        $.fn.form.defaults = {
        }

    })(jQuery, null);



    function TemplateAdministrationInst(strTittle, strTypeMenu) {

        var strUrlModal = '~/Management/MassiveInstant/Index';
        $.window.open({
            id: 184209,
            modal: false,
            title: strTittle,
            url: strUrlModal,
            data: { strIdSession: Session.IDSESSION, strAplication: Session.DATACUSTOMER.Application },
            buttons: {
                Guardar: {
                    click: function () {
                        var txtDescription = $('#txtDescription').val();
                        var txtDateStart = $("#txtDateStart").val();
                        var txtDateEnd = $("#txtDateEnd").val();
                        var txthoursVig = $('#txthoursVig').val();
                        var txtminutesVig = $('#txtminutesVig').val();
                        var cboTypeVig = $('#cboTypeVig').val();
                        var txthoursCad = $('#txthoursCad').val();
                        var txtminutesCad = $('#txtminutesCad').val();
                        var cboTypeCad = $('#cboTypeCad').val();

                        if (txtDateStart.length != 0 && txtDateEnd.length != 0 && txthoursVig.length != 0 && txtminutesVig.length != 0 && txthoursCad != 0 && cboTypeCad != 0 && cboTypeVig != "-1" && cboTypeCad != "-1" && txtDescription.trim().length != 0) {
                            var stUrlLogo = "/Images/loading2.gif";
                            $('#184209_maindlg').block({
                                css: {
                                    border: 'none', padding: '15px', backgroundColor: '#000',
                                    '-webkit-border-radius': '10px', '-moz-border-radius': '10px',
                                    opacity: .8, color: '#fff'
                                },
                                overlayCSS: { backgroundColor: '#FFFFFF', opacity: 0.0, cursor: 'wait' },
                                message: '<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Grabando ... </div>',
                            });

                            if (strTypeMenu === "INSTMASIVE") {
                                SaveInstMasive(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription);
                            }
                            else if (strTypeMenu === "MakeChargePetition") {
                                SaveProcessedOrders(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription);
                            }
                        }
                        else {
                            if (txtDescription.length <= 0) { modalAlert('Debe Ingresar una descripción', 'Datos Incorrectos'); return false; }
                            if (validarFecha("txtDateStart") === false) { return false; }
                            if (validarFecha("txtDateEnd") === false) { return false; }
                            if (validarHoraIngreso('txthoursVig', 'txtminutesVig', document.getElementById('cboTypeVig').value) === false) { return false; }
                            if (validarHoraIngreso('txthoursCad', 'txtminutesCad', document.getElementById('cboTypeCad').value) === false) { return false; }
                        }
                    }
                },
                Limpiar: {
                    click: function () {

                        clearFormInstMasive();
                    }
                },
                Cerrar: {
                    click: function (sender, args) {
                        this.close();
                    }
                }
            },
            width: 800,
            height: 500,
        });
    }

    function SaveInstMasive(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription) {
        if (Session.ListMassiveInstantCorrect.length > 0) {
            $.app.ajax({
                type: 'POST',
                url: '~/management/massiveinstant/SaveInstantMasive',
                data: {
                    strIdSession: Session.IDSESSION,
                    strAplication: Session.DATACUSTOMER.Application,
                    DateValidityStart: txtDateStart,
                    DateValidityEnd: txtDateEnd,
                    StartHour: txthoursVig,
                    StartMinutes: txtminutesVig,
                    StartType: cboTypeVig,
                    EndHour: txthoursCad,
                    EndMinutes: txtminutesCad,
                    EndType: cboTypeCad,
                    Description: txtDescription,
                    listInstant: Session.ListMassiveInstantCorrect,
                    ArchiveName: Session.ArchivoIntantanea
                },
                success: function (response) {
                    $('#184209_maindlg').unblock();
                    $.window.close();


                    modalAlert('Proceso terminado satisfactoriamente', 'Mensaje');
                },
                error: function () {

                    modalAlert('Ocurrior un error al grabar, intentelo nuevamente', 'Mensaje');
                    $('#184209_maindlg').unblock();
                }
            });
        } else {
            $('#184209_maindlg').unblock();
            if (Session.DATACUSTOMER.Application === "POSTPAID" || Session.DATACUSTOMER.Application === "PREPAID") {

                modalAlert('Debe importar líneas', 'Mensaje');
            } else {


                modalAlert('Debe importar contratos', 'Mensaje');
            }
        }
    }

    function SaveProcessedOrders(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription) {
        if (Session.NewArchivoIntantanea != "") {
            $.app.ajax({
                type: 'POST',
                url: '~/management/massiveinstant/SaveProcessedOrders',
                data: {
                    strIdSession: Session.IDSESSION,
                    strAplication: Session.DATACUSTOMER.Application,
                    DateValidityStart: txtDateStart,
                    DateValidityEnd: txtDateEnd,
                    StartHour: txthoursVig,
                    StartMinutes: txtminutesVig,
                    StartType: cboTypeVig,
                    EndHour: txthoursCad,
                    EndMinutes: txtminutesCad,
                    EndType: cboTypeCad,
                    Description: txtDescription,
                    ArchiveName: Session.ArchivoIntantanea,
                    NewArchiveName: Session.NewArchivoIntantanea
                },
                success: function (response) {
                    $('#184209_maindlg').unblock();
                    clearFormInstMasive();


                    modalAlert('Proceso terminado satisfactoriamente', 'Mensaje');

                },
                error: function () {


                    modalAlert('Ocurrio un error al grabar, intentelo nuevamente', 'Mensaje');
                }
            });
        }
        else {


            modalAlert('Debe seleccionar el archivo a importar', 'Atención');
            $('#184209_maindlg').unblock();
        }
    }


    function clearFormInstMasive() {
        $('#txtDescription').val("");
        $("#txtDateStart").val();
        $("#txtDateEnd").val();
        $('#txthoursVig').val("");
        $('#txtminutesVig').val("");
        $('#cboTypeVig').val("-1");
        $('#txthoursCad').val("");
        $('#txtminutesCad').val("");
        $('#cboTypeCad').val("-1");
        if (Session.TypeMenuInst === "INSTMASIVE") {
            Session.ListMassiveInstantCorrect = [];
            $('#tblInsMassCorrect tbody').empty();
        } else if (Session.TypeMenuInst === "MakeChargePetition") {
            $('#MakeChargePetitionFrame').hide();
            Session.ArchivoIntantanea = "";
            Session.NewArchivoIntantanea = "";
        }
    }

    function reloj() {
        var text2 = moment().format('DD/MM/YYYY hh:mm:ss a');
        $('#idSession').html('<b>Session ID: ' + Session.IDSESSION + '</b>&nbsp&nbsp  ' + text2 + '');
        var t = setTimeout(function () { reloj() }, 500);
    }

    $(document).ready(function () {
        $.window.toolbar = $('#uiBarra');
        moment.locale('es');
        reloj();
        $('#navbar-contenedor').form();
        $('#uiClock').html('');
    })
}