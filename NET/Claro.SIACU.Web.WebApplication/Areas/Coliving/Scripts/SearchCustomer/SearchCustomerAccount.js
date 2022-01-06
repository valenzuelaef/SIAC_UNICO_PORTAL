(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.SearchCustomerAccount.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , lblTypeDocDescription: $('#lblTypeDocDescription', $element)
            , lblNroDocId: $('#lblNroDocId', $element)
            , lblNroAccount: $('#lblNroAccount', $element)
            , lblFullName: $('#lblFullName', $element)
            , lblAdress: $('#lblAdress', $element)
            , lblMigrateToOne: $('#lblMigrateToOne', $element)
            , lblModality: $('#lblModality', $element)
            , lblTypeAccount: $('#lblTypeAccount', $element)
            , txtHdLineNumber: $("#txtHdLineNumber", $element)
            , txtHdUrlWebSpecial: $('#txtHdUrlWebSpecial', $element)
            , txtHdNameLinkWebSpecial: $('#txtHdNameLinkWebSpecial', $element)
            , txtHdNameLinkPostVenta: $('#txtHdNameLinkPostVenta', $element)
            , txtHdUrlPostVenta: $("#txtHdUrlPostVenta", $element)
            , txtHdCustomerId: $("#txtHdCustomerId", $element)
            , txtHdTypeDocId: $('#txtHdTypeDocId', $element)
            , txtHdNumberAccount: $('#txtHdNumberAccount', $element)
            , txtHdMigrateOne: $('#txtHdMigrateOne', $element)
            , txtHdDocNumber: $('#txtHdDocNumber', $element)
            , btnTypeSearch: $('#btnTypeSearch', $element)
            , rdBtnChecked: $("input[name='rdbtnLine']:radio", $element)
            , btnSale: $('#btnSale', $element)
            , btnPostSale: $('#btnPostSale', $element)
            , btnSpecialCases: $('#btnSpecialCases', $element)
            , btnClose: $('#btnClose', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.btnSale.addEvent(this, 'click', that.btnSale_click);
            controls.btnPostSale.addEvent(this, 'click', that.btnPostSale_click);
            controls.btnSpecialCases.addEvent(this, 'click', that.btnSpecialCases_click);
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            that.render();
            that.resizeContent();
        },

        render: function () {
            var that = this;
            //controls = that.getControls();
            that.fnDisableBtn();
            that.loadDataCustomerAccount();
            that.rdbtnLinesNumber_Change();

        },
        fnValidate_Migrate: function (_resp) {
            var that = this;
            //controls = that.getControls();
            if (_resp.toUpperCase() == "SI") {
                that.fnEnableBtn();
            }
            else if (_resp.toUpperCase() == "NO") {
                that.fnValidate_AccountFullStack();
            }
            else {
                console.log("ERROR MIGRATE");
            }
        },
        fnValidate_AccountFullStack: function () {
            var that = this,
                controls = that.getControls();

            var documentType = controls.txtHdTypeDocId.val();
            var documentNumber = controls.lblNroDocId.text();
            var documentTypeDescription = controls.lblTypeDocDescription.text();
            var _searchType = {
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
               data: { strIdSession: Session.IDSESSION, objSearchType: _searchType },
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
        fnDisableBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSpecialCases.attr("disabled", "disabled");
        },

        fnEnableBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSpecialCases.removeAttr("disabled");
        },
        fnDisableButtons: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSpecialCases.prop("disabled", true);
            controls.btnPostSale.prop("disabled", true);
            controls.btnSale.prop("disabled", true);
        },
        fnValidate_CheckedLine: function () {
            //var that = this;
            //controls = that.getControls();
            var _res;
            if ($("input:radio[name=rdbtnLine]").is(':checked')) {
                _res = true;
                return _res;
            }
            else {
                _res = false;
                return _res;
            }
        },
        fnGet_Line: function () {
            var that = this,
               controls = that.getControls();
            if ($("input:radio[name=rdbtnLine]").is(':checked')) {
                controls.txtHdLineNumber.val($("input:radio[name=rdbtnLine]:checked").parents("tr").find("td.tdLineNumber").text());
            }
            else {
                console.log('Seleccione un rdbtn');
            }
        },

        rdbtnLinesNumber_Change: function () {
            var that = this;
            //controls = that.getControls();

            $("#tableLinesNumber .tdRdBtn").each(function () {
                $("input:radio[name=rdbtnLine]", this).addEvent(that, 'change', that.fnGet_Line);
            });
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

        btnPostSale_click: function () {
            var controls = this.getControls();
            var _AccountNumber = controls.lblNroAccount.text().trim();
            //Redireccionando de acuerdo si la Linea Migró o NO
            var valueMigrate = controls.lblMigrateToOne.text().toUpperCase();
            var strSi = "SI";
            var strNo = "NO";
            if (valueMigrate == strNo) {
                window.opener.$("#modalTypeSearch").modal('hide');
                window.opener.$("#ul-button-Search").find("a[data-value=2]").trigger('click');
                window.opener.$("#txtCriteriaValue").val(_AccountNumber);
                window.opener.$("#btnCriteriaSearch").trigger('click');
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
                modalAlert('Estimado usuario, la Cuenta no indica el estado de la migración', 'Mensaje Transacciones');
            }
        },
        btnSpecialCases_click: function () {
            var that = this,
                controls = that.getControls();
            if (that.fnValidate_CheckedLine()) {
                var _LineNumber = controls.txtHdLineNumber.val();
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
                    width: 600,
                    height: "auto"
                });
            } else {
                modalAlert("Debe de seleccionar una línea de la cuenta", "Mensaje");
            }

        },

        btnClose_click: function () {
            window.close();
        },
        fnLoad_DataCustomer: function (data) {
            var that = this,
                controls = that.getControls();
            controls.lblTypeDocDescription.text(data.DocumentTypeDescription);
            controls.lblNroDocId.text(data.DocumentNumber);
            controls.lblNroAccount.text(data.AccountNumber);
            controls.lblFullName.text(data.CustomerName);
            controls.lblAdress.text(data.Address);
            controls.lblMigrateToOne.text(data.MigrateOne);
            controls.lblModality.text(data.ProductType);
            data.Segment != null ? controls.lblTypeAccount.text(data.Segment) : "";

            controls.txtHdDocNumber.val(data.DocumentNumber);
            controls.txtHdTypeDocId.val(data.DocumentType);
            controls.txtHdMigrateOne.val(data.MigrateOne);
            controls.txtHdNumberAccount.val(data.AccountNumber);
        },

        fnFill_TablaCustomer: function (data) {
            let _tr = '';
            data.forEach(function (_value, _index) {
                _tr += '<tr>'
                    + '<td class="tdLineNumber">' + _value.LineNumber + '</td>'
                    + '<td class="tdRatePlan">' + _value.RatePlan + '</td>'
                    + '<td class="tdState">' + _value.LineStatus + '</td>'
                    + '<td class="tdRdBtn"><input type="radio" name="rdbtnLine" /></td>'
                    + '</tr>';
            });
            $("#tbodyLineNumber").append(_tr);
        },
        loadDataCustomerAccount: function () {
            var that = this,
                controls = that.getControls();
            console.log("customerId: " + controls.txtHdCustomerId.val().trim());
            var _objSearchType = {
                CustomerId: controls.txtHdCustomerId.val().trim()
            };
            var _data = {
                strIdSession: Session.IDSESSION,
                objSearchType: _objSearchType
            };

            $.app.ajax(
                      {
                          async: false,
                          modal: true,
                          type: "POST",
                          dataType: "json",
                          url: '~/Coliving/SearchCustomer/DataCustomerAccount',
                          data: _data,
                          success: function (result) {
                              console.log(result);
                              if (result.data != null) {
                                  console.log(result);
                                  that.fnLoad_DataCustomer(result.data, result);
                                  that.fnFill_TablaCustomer(result.data.ListSuscription);
                                  that.fnValidate_Migrate(result.data.MigrateOne);
                              } else {
                                  that.fnDisableButtons();
                                  modalAlert('No se encontró información con los datos ingresados', 'Mensaje Transacciones', function () {
                                      window.close();
                                  });
                              }
                          },
                          error: function () {
                              modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                              that.fnDisableButtons();
                          }
                      });

        },

        resizeContent: function () {
            $(".modal-dialog:eq(0) .modal-footer").remove();
            var heightWindow = ($(".modal-dialog:eq(0)").height());
            if (heightWindow >= 738) {
                window.resizeTo(screen.width, screen.height);
                window.moveTo(0, 0);
            }

            else {
                window.resizeTo(1250, (heightWindow + 80));
                window.moveTo(screen.width / 2 - 625, (screen.height / 2) - ((heightWindow + 80) / 2));
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
    $.fn.SearchCustomerAccount = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];
        this.each(function () {
            var $this = $(this),
                data = $this.data('SearchCustomerAccount'),
                options = $.extend({}, $.fn.SearchCustomerAccount.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('SearchCustomerAccount', data);
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
    $.fn.SearchCustomerAccount.defaults = {
        objCustomer: {}
    };
    $('#divContainerSearchCustomerAccount').SearchCustomerAccount();

})(jQuery);
