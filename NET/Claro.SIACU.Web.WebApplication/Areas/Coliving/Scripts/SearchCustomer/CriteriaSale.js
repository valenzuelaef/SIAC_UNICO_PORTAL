(function ($, undefined) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.CriteriaSale.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , trCustomerSubType: $('#trCustomerSubType', $element)
            , trProductType: $('#trProductType', $element)
            , trLineNumber: $('#trLineNumber', $element)
            , trRatePlan: $('#trRatePlan', $element)
            , trSerialNumber: $('#trSerialNumber', $element)
            , trSerialType: $('#trSerialType', $element)

            , cboOperationType: $('#cboOperationType', $element)
            , cboCustomerSubType: $('#cboCustomerSubType', $element)
            , cboProductType: $("#cboProductType", $element)
            , lblModality: $("#lblModality", $element)
            , lblCustomerType: $("#lblCustomerType", $element)
            , lblCustomerSubType: $("#lblCustomerSubType", $element)
            , lblTypeDocDescription: $("#lblTypeDocDescription", $element)
            , lblLineNumber: $("#lblLineNumber", $element)
            , lblRatePlan: $("#lblRatePlan", $element)
            , lblProductType: $("#lblProductType", $element)

            , txtSerialNumber: $("#txtSerialNumber", $element)

            , txtHdCustomerSubTypeId: $("#txtHdCustomerSubTypeId", $element)
            , txtHdProductTypeId: $("#txtHdProductTypeId", $element)
            , txtHdstrPrepagoOlo: $("#txtHdstrPrepagoOlo", $element)
            , rdSerialType: $("input[name='rdSerialType']:radio", $element)

            , btnRedirectSale: $('#btnRedirectSale', $element)
            , btnClose: $('#btnClose', $element)
        });

    }

    var inhHdTypeDocId = window.opener.$('#txtHdTypeDocId'),
        inhTypeDocDescription = window.opener.$('#lblTypeDocDescription'),
        inhAccountType = window.opener.$('#lblTypeAccount'),
        inhModality = window.opener.$('#lblModality'),
        inhLineNumber = window.opener.$('#lblLineNumber'),
        inhHdModality = window.opener.$('#txtHdModality'),
        inhHdLineNumber = window.opener.$('#txtHdLineNumber'),
        inhHdAccountType = window.opener.$('#txtHdAccountType'),
        inhHdNumberAccount = window.opener.$('#txtHdNumberAccount'),
        inhHdMigrateOne = window.opener.$('#txtHdMigrateOne'),
        inhHdDocNumber = window.opener.$('#txtHdDocNumber'),

        strObjcustomerType = getValueConfig(['strCustomerType']).strCustomerType,
        strMasivo = strObjcustomerType.split("|")[0].split(";")[0],
        intCodMasivo = parseInt(strObjcustomerType.split("|")[0].split(";")[1]),
        strCorporativo = strObjcustomerType.split("|")[1].split(";")[0],
        intCodCorporativo = parseInt(strObjcustomerType.split("|")[1].split(";")[1]),

        strObjOperationType = getValueConfig(['strOperationType']).strOperationType,
        intCodAlta = parseInt(strObjOperationType.split("|")[0].split(";")[1]),
        intCodMigracion = parseInt(strObjOperationType.split("|")[1].split(";")[1]),
        intCodPortabilidad = parseInt(strObjOperationType.split("|")[2].split(";")[1]),
        //intCodTransferencia = parseInt(strObjOperationType.split("|")[3].split(";")[1]),
        //intCodDevolucion = parseInt(strObjOperationType.split("|")[4].split(";")[1]),
        intCodRenovacion = parseInt(strObjOperationType.split("|")[5].split(";")[1]),
        intCodChipRepuesto = parseInt(strObjOperationType.split("|")[6].split(";")[1]);
    //intCodTodos = parseInt(strObjOperationType.split("|")[7].split(";")[1]),
    //intCodRecarga = parseInt(strObjOperationType.split("|")[8].split(";")[1]);

    var inheritedForm = {
        typeDocId: inhHdTypeDocId.val(),
        typeDocDescription: inhTypeDocDescription.text(),
        customerType: inhAccountType.length > 0 ? inhAccountType.text().toUpperCase() : inhHdAccountType.length > 0 ? inhHdAccountType.val().toUpperCase() : "-",
        modality: inhModality.length > 0 ? inhModality.text() : inhHdModality.length > 0 ? inhHdModality.val() : "0",
        lineNumber: inhLineNumber.length > 0 ? inhLineNumber.text() : inhHdLineNumber.length > 0 ? inhHdLineNumber.val() : "",
        AccountNumber: inhHdNumberAccount.val(),
        Migration: inhHdMigrateOne.val(),
        DocumentNumber: inhHdDocNumber.val(),
        MigrationId: function () {
            if (this.Migration == 'SI') {
                return 1;
            }
            else if (this.Migration == 'NO') {
                return 0;
            }
            else {
                return -1;
            }
        },
        customerTypeId: function () {
            var strEmpresas = "EMPRESAS";
            if (this.customerType == strMasivo) {
                return intCodMasivo;
            }
            else if (this.customerType == strCorporativo || this.customerType == strEmpresas) {
                return intCodCorporativo;
            }
            else {
                return 0;
            }
        }
    };


    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = that.getControls();
            that.validatePrefixLineNumber(inheritedForm.lineNumber, function (r) { inheritedForm.lineNumber = r })
            controls.cboOperationType.addEvent(this, 'change', that.cboOperationType_change);
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            controls.btnRedirectSale.addEvent(this, 'click', that.btnRedirectSale_click);
            that.render();
        },

        render: function () {
            var that = this;
            //controls = that.getControls();
            that.loadOperationTypeItems();
            that.loadValues();
            that.resizeContent();
        },
        validatePrefixLineNumber: function (_lineNumber, callback) {
            var keyValue = getValueConfig(['gInternationalCode']).gInternationalCode;//51
            var lineNumber = _lineNumber;
            var prefix = lineNumber.substring(0, 2); //Se obtiene los 2 primeros carácateres de la línea
            if (prefix == keyValue) {
                return callback(lineNumber.substring(2));
            }
            else {
                return callback(lineNumber);
            }
        },
        btnClose_click: function () {
            window.close();
        },
        fnHideElement: function (_element) {
            $(_element).css("display", "none");
        },
        fnShowElement: function (_element) {
            $(_element).css("display", "block");
        },
        fnEnabledElement: function (_element) {
            $(_element).prop("disabled", false);
        },
        fnDisabledElement: function (_element) {
            $(_element).prop("disabled", true);
        },
        hideBlock: function () {
            var that = this,
            controls = that.getControls();

            //controls.trProductType.hide();
            controls.trLineNumber.hide();
            controls.trRatePlan.hide();
            controls.trSerialNumber.hide();
            controls.trSerialType.hide();

            controls.txtSerialNumber.val("");
        },
        loadValues: function () {
            var that = this,
               controls = that.getControls();
            controls.lblTypeDocDescription.text(inheritedForm.typeDocDescription);
            controls.lblCustomerType.text(inheritedForm.customerType);
            controls.lblModality.text(inheritedForm.modality);
            controls.lblLineNumber.text(inheritedForm.lineNumber);
        },

        loadOperationTypeItems: function () {
            console.log(inheritedForm.customerTypeId());
            var that = this,
                controls = that.getControls();

            var data = { Segment: inheritedForm.modality.toUpperCase(), CustomerTypeId: inheritedForm.customerTypeId() };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Coliving/Common/getOperationType',
                data: JSON.stringify(data),
                async: false,
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
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
                },
                success: function (response) {
                    console.log(response)
                    if (response.data.length > 0) {
                        $.each(response.data, function (index, value) {
                            console.log("index: " + index + " - value: " + value.Description);
                            controls.cboOperationType.append('<option value="' + value.Code + '">' + value.Description + '</option>');
                        });
                        var customerType_id = inheritedForm.customerTypeId();
                        var modality_des = inheritedForm.modality.toUpperCase();
                        var operation_id = controls.cboOperationType.val();
                        that.cboOperationType_change(customerType_id, modality_des, operation_id);
                    }
                    else {
                        console.log(inheritedForm.modality.toUpperCase());
                        var _strPrepago = getValueConfig(['strKeyModalidadPrepago']).strKeyModalidadPrepago;
                        if (inheritedForm.modality.toUpperCase() == _strPrepago) {
                            modalAlert('Las redirecciones para la Modalidad PREPAGO fueron desactivadas', 'Mensaje Transacciones');
                        }
                        else {
                        modalAlert('Sucedió un error interno para cargar los items de Tipo Operación', 'Mensaje Transacciones');
                        }
                        that.fnDisabledElement(controls.btnRedirectSale);
                    }
                },
                error: function () {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                    that.fnDisabledElement(controls.btnRedirectSale);
                }
            });
        },
        cboOperationType_change: function () {
            var that = this,
            controls = that.getControls();
            var customerType_id = inheritedForm.customerTypeId();
            var modality_des = inheritedForm.modality.toUpperCase();
            var operation_id = controls.cboOperationType.val();
            var _strPrepagoOlo = controls.txtHdstrPrepagoOlo.val();
            var _strPrepago = getValueConfig(['strKeyModalidadPrepago']).strKeyModalidadPrepago;
            var _strPostPago = getValueConfig(['strKeyModalidadPostPago']).strKeyModalidadPostPago;
            that.hideBlock();
            controls.txtHdCustomerSubTypeId.val("");
            controls.txtHdProductTypeId.val("");
            that.fnEnabledElement(controls.btnRedirectSale);

            switch (modality_des) {
                case _strPostPago:
                    if (customerType_id == intCodMasivo || customerType_id == intCodCorporativo) {/*1002=MASIVO* ||1003=EMPRESAS O CORPORATIVO*/
                        if (parseInt(operation_id) == intCodRenovacion)/*Renovación*/ {
                            if (inheritedForm.lineNumber == "") {
                                modalAlert('Estimado usuario, seleccione una linea para realizar esta operación', 'Mensaje Transacciones');
                                that.fnDisabledElement(controls.btnRedirectSale);
                                return;
                            }
                            controls.trLineNumber.show();
                        }
                        this.loadCustomerSubTypeItems(modality_des, customerType_id, operation_id);
                    }
                    break;
                case _strPrepagoOlo:
                case _strPrepago:
                    if (customerType_id == intCodMasivo)/*MASIVO*/ {
                        this.loadCustomerSubTypeItems(modality_des, customerType_id, operation_id);
                    }
                    break;

                default:
                    break;
            }
        },

        loadCustomerSubTypeItems: function (modality, customerType_id, operationType_id) {
            var that = this,
                controls = that.getControls();

            var data = {
                Segment: modality.toUpperCase(),
                CustomerTypeId: customerType_id,
                OperationTypeId: operationType_id
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Coliving/Common/getCustomerSubType',
                data: JSON.stringify(data),
                async: false,
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
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
                },
                success: function (response) {
                    console.log(response);
                    controls.cboCustomerSubType.empty();
                    if (response.data.length == 1) {
                        controls.lblCustomerSubType.text(response.data[0].Description);

                        that.fnShowElement(controls.lblCustomerSubType);
                        that.fnHideElement(controls.cboCustomerSubType);

                        controls.txtHdCustomerSubTypeId.val(response.data[0].Code);
                        that.loadProductTypeItems(modality.toUpperCase(), customerType_id, operationType_id, controls.txtHdCustomerSubTypeId.val());

                    }
                    else if (response.data.length > 1) {

                        $.each(response.data, function (index, value) {
                            console.log("index: " + index + " - value: " + value.Description);
                            controls.cboCustomerSubType.append('<option value="' + value.Code + '">' + value.Description + '</option>');
                        });

                        that.fnHideElement(controls.lblCustomerSubType);
                        that.fnShowElement(controls.cboCustomerSubType);

                        controls.cboCustomerSubType.on("change", function () {
                            that.cboCustomerSubType_change();
                        });
                        controls.txtHdCustomerSubTypeId.val(response.data[0].Code);
                        that.loadProductTypeItems(modality.toUpperCase(), customerType_id, operationType_id, controls.txtHdCustomerSubTypeId.val());
                    }
                    else {
                        modalAlert('Sucedió un error interno para cargar los items de Sub Tipo de Clientes', 'Mensaje Transacciones');
                        that.fnDisabledElement(controls.btnRedirectSale);
                    }
                },
                error: function () {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                    that.fnDisabledElement(controls.btnRedirectSale);
                }
            });
        },
        cboCustomerSubType_change: function () {
            var that = this,
            controls = that.getControls();
            controls.txtHdCustomerSubTypeId.val(controls.cboCustomerSubType.val());
            controls.txtHdProductTypeId.val("");
            var customerType_id = inheritedForm.customerTypeId();
            var modality_des = inheritedForm.modality.toUpperCase();
            var operation_id = controls.cboOperationType.val();
            var customerSubType_id = controls.txtHdCustomerSubTypeId.val();
            that.loadProductTypeItems(modality_des, customerType_id, operation_id, customerSubType_id);
        },
        loadProductTypeItems: function (modality, customerType_id, operationType_id, customerSubType_id) {
            var that = this,
                controls = that.getControls();

            var data = {
                Segment: modality.toUpperCase(),
                CustomerTypeId: customerType_id,
                OperationTypeId: operationType_id,
                CustomerSubTypeId: customerSubType_id
            };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Coliving/Common/getProductType',
                data: JSON.stringify(data),
                async: false,
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
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
                },
                success: function (response) {
                    controls.cboProductType.empty();
                    if (response.data.length == 1) {
                        controls.lblProductType.text(response.data[0].Description);
                        that.fnShowElement(controls.lblProductType);
                        that.fnHideElement(controls.cboProductType);
                        controls.txtHdProductTypeId.val(response.data[0].Code);
                    }
                    else if (response.data.length > 1) {
                        $.each(response.data, function (index, value) {
                            console.log("index: " + index + " - value: " + value.Description);
                            controls.cboProductType.append('<option value="' + value.Code + '">' + value.Description + '</option>');
                        });
                        that.fnHideElement(controls.lblProductType);
                        that.fnShowElement(controls.cboProductType);

                        controls.txtHdProductTypeId.val(response.data[0].Code);
                        controls.cboProductType.on("change", function () {
                            that.cboProductType_change();
                        });
                    }
                    else {
                        modalAlert('Sucedió un error interno para cargar los items de Tipo de Producto', 'Mensaje Transacciones');
                        that.fnDisabledElement(controls.btnRedirectSale);
                    }
                },
                error: function () {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                    that.fnDisabledElement(controls.btnRedirectSale);
                }
            });
        },
        cboProductType_change: function () {
            var that = this,
            controls = that.getControls();
            controls.txtHdProductTypeId.val(controls.cboProductType.val());
            console.log("cboProductType_change");
        },
        fnValidarCuenta: function (callback) {
            var _strIdSession = Session.IDSESSION;
            var _objSearchType = {
                DocumentType: inheritedForm.typeDocId,
                DocumentNumber: inheritedForm.DocumentNumber
            };
            var data = {
                strIdSession: _strIdSession,
                objSearchType: _objSearchType
            };
            var strKeyMigradoFullStack = getValueConfig(['strKeyMigradoFullStack']).strKeyMigradoFullStack;
            var intSomeMigro = parseInt(strKeyMigradoFullStack.split("|")[3].split(";")[0]);
            var intNoMigro = parseInt(strKeyMigradoFullStack.split("|")[0].split(";")[0]);
            var intError = -1;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Coliving/SearchCustomer/VerificarCuentaFullStack_RS',
                data: JSON.stringify(data),
                async: false,
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
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
                },
                success: function (response) {
                    if (response != null && response.data == true) {
                        return callback(intSomeMigro);/*Migro(*)*/
                    }
                    else {
                        return callback(intNoMigro);/*No Migro*/
                    }
                },
                error: function () {
                    return callback(intError);/*Error*/
                }
            });
        },
        fn_getUrl: function (FilterParameters, callback) {
            var intError = -1;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Coliving/Common/GetCriteriaSaleUrl',
                data: JSON.stringify(FilterParameters),
                async: false,
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                        baseZ: $.app.getMaxZIndex() + 1,
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
                },
                success: function (response) {
                    console.log(response.data);
                    if (response.data != null) {
                        return callback(response.data);
                    }
                    else {
                        return callback(null);
                    }
                },
                error: function () {
                    return callback(intError);/*Error*/
                }
            });
        },
        btnRedirectSale_click: function () {
            var that = this;
            var controls = that.getControls();
            var customerSubTypeId = controls.txtHdCustomerSubTypeId.val();
            var productTypeId = controls.txtHdProductTypeId.val();
            var customerType_id = inheritedForm.customerTypeId();
            var modality_des = inheritedForm.modality.toUpperCase();
            var operation_id = controls.cboOperationType.val();
            var codeMigrationStatus = inheritedForm.MigrationId();
            var _strPrepagoOlo = controls.txtHdstrPrepagoOlo.val();
            var _strPrepago = getValueConfig(['strKeyModalidadPrepago']).strKeyModalidadPrepago;
            var _strPostPago = getValueConfig(['strKeyModalidadPostPago']).strKeyModalidadPostPago;
            var _parameters = {
                strIdSession: Session.IDSESSION,
                Segment: modality_des,
                CustomerTypeId: customerType_id,
                OperationTypeId: operation_id,
                CustomerSubTypeId: customerSubTypeId,
                ProductTypeId: productTypeId,
                DocumentTypeId: inheritedForm.typeDocId,

                MigrationStatusId: codeMigrationStatus
            };
            var intOperation_id = parseInt(operation_id);
            var strKeyMigradoFullStack = getValueConfig(['strKeyMigradoFullStack']).strKeyMigradoFullStack;
            var intSinRelev = parseInt(strKeyMigradoFullStack.split("|")[4].split(";")[0]);
            var strError = "-1";
            switch (modality_des) {
                case _strPostPago:
                    if (customerType_id == intCodMasivo)/*MASIVO*/ {
                        if (intOperation_id == intCodAlta || intOperation_id == intCodPortabilidad)/*Alta o Portabilidad*/ {
                            if (inheritedForm.typeDocId != strError) {
                                that.fnValidarCuenta(function (r) { _parameters.MigrationStatusId = r; });
                            }
                        }
                        else if (intOperation_id == intCodMigracion)/*Migración Pre a Post*/ {
                            _parameters.MigrationStatusId = intSinRelev;/*EstadoMigracion*/
                        }
                        else if (intOperation_id == intCodRenovacion || intOperation_id == intCodChipRepuesto)/*Renovació o Chip Repuesto*/ {
                            /*Si migro o no*/
                        }
                        that.fn_openSaleWindow(_parameters);
                    }
                    else if (customerType_id == intCodCorporativo)/*EMPRESAS O CORPORATIVO*/ {
                        that.fn_openSaleWindow(_parameters);
                    }
                    break;
                case _strPrepago:
                    that.fn_openSaleWindow(_parameters);
                    break;
                case _strPrepagoOlo:
                    _parameters.MigrationStatusId = intSinRelev;/*EstadoMigracion*/
                    that.fn_openSaleWindow(_parameters);
                    break;
                default:
                    break;
            }
        },
        resizeContent: function () {
            $(".modal-dialog:eq(0) .modal-footer").remove();
            var heightWindow = ($(".modal-dialog:eq(0)").height());
            console.log(heightWindow);

            if (heightWindow >= 738) {
                window.resizeTo(screen.width, screen.height);
                window.moveTo(0, 0);
            }
            else {
                window.resizeTo(600, (heightWindow + 65));
                window.moveTo(screen.width / 2 - 300, (screen.height / 2) - ((heightWindow + 65) / 2));
            }
        },
        fn_openSaleWindow: function (_parameters) {
            var _url = "";
            var intError = -1;
            this.fn_getUrl(_parameters, function (r) {
                if (r == intError) {
                    modalAlert("Sucedio un error interno al obtener la url para la redirección", "Mensaje")
                }
                else if (r != null && r != "") {
                    _url = r.Url;
                    //Indicamos el alto y ancho que tendra la nueva ventana
                    var altura = 500;
                    var anchura = 1000;

                    // calculamos la posicion x e y para centrar la ventana
                    var y = parseInt((window.screen.height / 2) - (altura / 2));
                    var x = parseInt((window.screen.width / 2) - (anchura / 2));
                    window.open(_url, r.Page, 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',toolbar=no,location=no,status=no,menubar=no,directories=no')
                }
                else {
                    modalAlert("El tipo de documento o el estado de la migración no son válidos para la redirección", "Mensaje")
                }
            });

        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
    }

    $.fn.CriteriaSale = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];
        this.each(function () {
            var $this = $(this),
                data = $this.data('CriteriaSale'),
                options = $.extend({}, $.fn.CriteriaSale.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CriteriaSale', data);
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
    $.fn.CriteriaSale.defaults = {
        objCustomer: {}
    };
    $('#divCriteriaSale').CriteriaSale();
})(jQuery);
