(function ($, undefined) {
    'use strict'
    var Form = function ($element, options) {
        $.extend(this, $.fn.FormSearchCustomerLine.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblTypeDocDescription: $('#lblTypeDocDescription', $element)
            , lblNumeroDocumento: $('#lblNumeroDocumento', $element)
            , lblNumeroCuenta: $('#lblNumeroCuenta', $element)
            , lblNombres: $('#lblNombres', $element)
            , lblMigradoOne: $('#lblMigradoOne', $element)
            , lblDireccion: $('#lblDireccion', $element)
            , lblLineNumber: $('#lblLineNumber', $element)
            , Modalitylabelchange: $('#Modalitylabelchange', $element)
            , lblModality: $('#lblModality', $element)
            , lblTypeAccount: $('#lblTypeAccount', $element)
            , lblPlanTarifario: $('#lblPlanTarifario', $element)
            , lblEstado: $('#lblEstado', $element)
            , txtHdUrlWebSpecial: $('#txtHdUrlWebSpecial', $element)
            , txtHdNameLinkWebSpecial: $('#txtHdNameLinkWebSpecial', $element)
            , txtHdNameLinkPostVenta: $('#txtHdNameLinkPostVenta', $element)
            , txtHdUrlPostVenta: $("#txtHdUrlPostVenta", $element)
            , txtHdTypeDocId: $('#txtHdTypeDocId', $element)
            , txtHdNumberAccount: $('#txtHdNumberAccount', $element)
            , txtHdMigrateOne: $('#txtHdMigrateOne', $element)
            , txtHdDocNumber: $('#txtHdDocNumber', $element)
            , txtHdstrPrepagoOlo: $("#txtHdstrPrepagoOlo", $element)

            , tdRatePlanElement: $(".tdRatePlanElement", $element)
            , tdAccountTypeElement: $(".tdAccountTypeElement", $element)
            , tdAccountNumberElement: $(".tdAccountNumberElement", $element)
            , trRow5: $("#trRow5", $element)
            , btnClose: $('#btnClose', $element)
            , btnSpecialCases: $('#btnSpecialCases', $element)
            , btnPostSale: $('#btnPostSale', $element)
            , btnSale: $('#btnSale', $element)
            , txtHdAccountType: $('#txtHdAccountType', $element)

        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = that.getControls();
            controls.btnPostSale.addEvent(this, 'click', that.btnPostSale_Click);
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            controls.btnSale.addEvent(this, 'click', that.btnSale_click);
            controls.btnSpecialCases.addEvent(this, 'click', that.btnSpecialCases_click);
            that.render();
            that.resizeContent();
        },
        render: function () {
            var that = this;
            that.DataCustomerLine();
            that.Modalitylabel_render();
        },
        DataCustomerLine: function () {
            var that = this,
                controls = that.getControls();
            var lineNumber = window.opener.$('#txtTypeDes').val().trim();
            var SearchType = {
                LineNumber: lineNumber
            };

            var data = {
                strIdSession: Session.IDSESSION,
                objSearchType: SearchType
            }
            $.app.ajax(
            {
                async: false,
                modal: true,
                type: "POST",
                dataType: "json",
                url: '~/Coliving/SearchCustomer/DataCustomerLine',
                data: data,
                success: function (result) {
                    if (result.data != null) {
                        console.log(result);
                        that.fnModality_render(result.data.ProductType, function (r) { });
                        controls.lblTypeDocDescription.html(result.data.DocumentTypeDescription);
                        controls.lblNumeroDocumento.html(result.data.DocumentNumber);
                        controls.lblNumeroCuenta.html(result.data.AccountNumber);
                        controls.lblNombres.html(result.data.CustomerName);
                        controls.lblDireccion.html(result.data.Address);
                        controls.lblTypeAccount.html(result.data.Segment);
                        $("#lblEstado").html(result.data.SubscriptionStatus);
                        controls.lblLineNumber.html(result.data.ServiceIdentifier);
                        controls.lblPlanTarifario.html(result.data.PoName);
                        controls.lblModality.html(result.data.ProductType);
                        controls.lblMigradoOne.html(result.data.MigrateOne);

                        controls.txtHdDocNumber.val(result.data.DocumentNumber);
                        controls.txtHdTypeDocId.val(result.data.DocumentType);
                        controls.txtHdMigrateOne.val(result.data.MigrateOne);
                        controls.txtHdNumberAccount.val(result.data.AccountNumber);

                    }
                    else {
                        that.fnDisableButtons();
                        modalAlert('No se encontró información con los datos ingresados', 'Mensaje Transacciones', function () {
                            window.close();
                        });
                    }
                },
                error: function () {
                    that.fnDisableButtons();
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                }
            });
        },
        //Remover Campos en el caso de modalidad PREPAGO
        fnModality_render: function (_modality, callback) {
            console.log(_modality);
            var that = this,
                controls = that.getControls();

            var _strPrepago = getValueConfig(['strKeyModalidadPrepago']).strKeyModalidadPrepago;

            if (_modality != null && _modality.toUpperCase() == _strPrepago.toUpperCase()) {

                controls.tdAccountTypeElement.remove();
                controls.tdRatePlanElement.remove();
                controls.tdAccountNumberElement.remove();
                $("#_tdLblTypeAccount").html($("#_tdStatus").html());
                $("#tdLblTypeAccount").html($("#tdStatus").html());

                controls.txtHdAccountType.val(getValueConfig(['strCustomerType']).strCustomerType.split("|")[0].split(";")[0]);
                controls.trRow5.remove();

            }
            return callback("OK");
        },
        // habilitar web casos especiales, al seleccionar modalidad
        Modalitylabel_render: function () {
            var that = this,
                controls = that.getControls();
            var _strPrepagoOlo = controls.txtHdstrPrepagoOlo.val();
            var _strPrepago = getValueConfig(['strKeyModalidadPrepago']).strKeyModalidadPrepago;
            var _strPostPago = getValueConfig(['strKeyModalidadPostPago']).strKeyModalidadPostPago;
            var _strMigradoOne = controls.lblMigradoOne.text().toUpperCase();
            var _strSi = "SI";
            var _strNo = "NO";
            switch (controls.lblModality.text().toUpperCase()) {

                case _strPrepagoOlo:
                    controls.btnSpecialCases.attr("disabled", "disabled");
                    break;
                case _strPrepago:
                case _strPostPago:
                    if (_strMigradoOne == _strSi) {
                        controls.btnSpecialCases.attr("enabled", "enabled");
                    }
                    else if (_strMigradoOne == _strNo) {
                        that.VerificarCuentaFullStack();
                    }
                    break;
                default:
                    break;
            }
        },

        VerificarCuentaFullStack: function () {
            var that = this,
               controls = that.getControls();
            var documentType = controls.txtHdTypeDocId.val();
            var documentNumber = controls.lblNumeroDocumento.text();
            var documentTypeDescription = controls.lblTypeDocDescription.text();
            var searchType = {
                DocumentType: documentType,
                DocumentNumber: documentNumber,
                DocumentTypeDescription: documentTypeDescription
            };
            $.app.ajax(
            {
                async: false,
                modal: true,
                type: "POST",
                dataType: "json",
                url: '~/Coliving/SearchCustomer/VerificarCuentaFullStack',
                data:
                    {
                        strIdSession: Session.IDSESSION,
                        objSearchType: searchType
                    },
                success: function (result) {
                    if (result.data != null) {
                        if (result.data) {
                            controls.btnSpecialCases.prop("disabled", false);
                            console.log('opcion habilitada');
                        } else {
                            controls.btnSpecialCases.prop("disabled", true);
                            console.log('opcion deshabilitada');
                        }
                    }
                },
                error: function () {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                }
            });
        },
        validatePrefixLineNumber: function (lineNumber, callback) {
            var keyValue = getValueConfig(['gInternationalCode']).gInternationalCode;//51
            var _lineNumber = lineNumber;
            var prefix = _lineNumber.substring(0, 2); //Se obtiene los 2 primeros carácateres de la línea
            if (prefix == keyValue) {
                return callback(_lineNumber.substring(2));
            }
            else {
                return callback(_lineNumber);
            }
        },
        btnPostSale_Click: function () {
            var controls = this.getControls();
            var _lineNumber = controls.lblLineNumber.text().trim();
            this.validatePrefixLineNumber(_lineNumber, function (r) {
                _lineNumber = r;
            });
            //Redireccionando de acuerdo si la Linea Migró o NO
            var strSi = "SI";
            var strNo = "NO";
            var valueMigrate = controls.lblMigradoOne.text().toUpperCase();
            if (valueMigrate == strNo) {
                window.opener.$('#modalTypeSearch').modal('hide');
                window.opener.$("#ul-button-Search").find("a[data-value='1']").trigger("click");
                window.opener.$("#txtCriteriaValue").val(_lineNumber);
                window.opener.$('#btnCriteriaSearch').click();
                window.close();
            }
            else if (valueMigrate == strSi) {
                //Indicamos el alto y ancho que tendra la nueva ventana
                var altura = 500;
                var anchura = 1000;

                // calculamos la posicion x e y para centrar la ventana
                var y = parseInt((window.screen.height / 2) - (altura / 2));
                var x = parseInt((window.screen.width / 2) - (anchura / 2));
                window.open(controls.txtHdUrlPostVenta.val(), controls.txtHdNameLinkPostVenta.val(), 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',toolbar=no,location=no,status=no,menubar=no,directories=no')
            }
            else {
                modalAlert('Estimado usuario, la linea no indica el estado de la migración', 'Mensaje Transacciones');
            }
        },

        btnSale_click: function () {
            var controls = this.getControls();
            var _DocumentTypeDescription = controls.lblTypeDocDescription.text();
            var _DocumentNumber = controls.txtHdDocNumber.val();

            var _data = {
                strIdSession: Session.IDSESSION,
                DocumentTypeDescription: _DocumentTypeDescription,
                DocumentNumber: _DocumentNumber,
            };
            $.window.open({
                modal: false,
                title: "Criterios de Venta",
                data: _data,
                url: '/Coliving/SearchCustomer/SIACU_Criteria_Sale',
                width: 600,
                height: "auto"
            });
        },

        btnSpecialCases_click: function () {
            var controls = this.getControls();
            var _LineNumber = controls.lblLineNumber.text();
            var _AccountNumber = controls.txtHdNumberAccount.val();
            var _DocumentTypeDescription = controls.lblTypeDocDescription.text();
            var _DocumentTypeCode = controls.txtHdTypeDocId.val();
            var _DocumentNumber = controls.txtHdDocNumber.val();
            var _MigrateStatus = controls.txtHdMigrateOne.val();
            $.window.open({
                modal: false,
                data: {
                    LineNumber: _LineNumber,
                    AccountNumber: _AccountNumber,
                    DocumentTypeDescription: _DocumentTypeDescription,
                    DocumentTypeCode: _DocumentTypeCode,
                    DocumentNumber: _DocumentNumber,
                    MigrateStatus: _MigrateStatus
                },
                title: controls.txtHdNameLinkWebSpecial.val(),
                url: controls.txtHdUrlWebSpecial.val(),
                width: 600
            });
        },

        fndisablebtn: function () {
            var that = this,
                controls = that.getcontrols();
            controls.btnspecialcases.attr("disabled", "disabled");
        },

        fnenabledBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSpecialCases.attr("enabled", "enabled");
        },
        fnDisableButtons: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSpecialCases.prop("disabled", true);
            controls.btnPostSale.prop("disabled", true);
            controls.btnSale.prop("disabled", true);
        },
        btnClose_click: function () {
            window.close();
        },
        resizeContent: function () {
            $(".modal-dialog:eq(0) .modal-footer").remove();
            var heightWindow = ($(".modal-dialog:eq(0)").height());

            if (heightWindow >= 738) {
                window.resizeTo(screen.width, screen.height);
                window.moveTo(0, 0);
            }

            else {
                window.resizeTo(1250, (heightWindow + 75));
                window.moveTo(screen.width / 2 - 625, (screen.height / 2) - ((heightWindow + 75) / 2));
            }
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strUrl: window.location.href
    };

    $.fn.FormSearchCustomerLine = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormSearchCustomerLine'),
                options = $.extend({}, $.fn.FormSearchCustomerLine.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormSearchCustomerLine', data);
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

    $.fn.FormSearchCustomerLine.defaults = {
    }
    $('#modalDetailProductClient').FormSearchCustomerLine();

})(jQuery);
