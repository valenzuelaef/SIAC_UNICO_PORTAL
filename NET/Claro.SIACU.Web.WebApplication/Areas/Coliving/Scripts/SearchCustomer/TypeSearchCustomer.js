(function ($, undefined) {
    'use strict'
    var Form = function ($element, options) {
        $.extend(this, $.fn.TypeSearchCustomer.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , modalTypeSearch: $('#modalTypeSearch', $element)
            , cboTypeId: $('#cboTypeId', $element)
            , cboDocTypeId: $('#cboDocTypeId', $element)
            , txtTypeDes: $('#txtTypeDes', $element)
            , lblTypeDes: $('#lblTypeDes', $element)
            , divDocTypeId: $('#divDocTypeId', $element)
            , btnTypeConsult: $('#btnTypeConsult', $element)
            , btnClose: $('#btnClose', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            that.fnInit_SessionParameters();
            controls.modalTypeSearch.modal('show');
            controls.cboTypeId.addEvent(this, 'change', that.cboTypeId_Change);
            controls.btnTypeConsult.addEvent(this, 'click', that.btnTypeConsult_Click);
            controls.btnClose.addEvent(this, 'click', that.btnClose_Click);
            controls.txtTypeDes.on('keypress', function () {
                that.TxtTypeDes_presskeyIntro(event);
            });
            that.render();
        },
        render: function () {
            var that = this,
                controls = that.getControls();
            controls.divDocTypeId.css("display", "none");
            controls.lblTypeDes.text("Nro de Línea:");

        },
        fnInit_SessionParameters: function () {
            var that = this;
            $.app.ajax(
            {
                modal: false,
                type: "POST",
                data: { strIdSession: Session.IDSESSION },
                dataType: "json",
                url: '~/Coliving/Common/InitParametersColiving',
                complete: function () {
                    $.unblockUI();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Cargando valores .... </div>',
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
                success: function (result) {
                    if (result.data != null && result.data > 0) {
                        that.cboTypeId_Fill();
                        that.cboDocTypeId_Fill();
                    }
                    else {
                        modalAlert("Ocurrió un error al cargar los valores, vuelva a intentar", "Mensaje", function () {
                            window.close();
                        });
                        $("#divSearchType").remove();
                    }
                }, error: function () {
                    modalAlert("Ocurrió un error interno", "Mensaje", function () {
                        window.close();
                    });
                }
            });
        },
        cboTypeId_Fill: function () {
            var controls = this.getControls();
            $.app.ajax(
               {
                   async: false,
                   modal: false,
                   type: "POST",
                   dataType: "json",
                   url: '~/Coliving/Common/getSearchType',
                   success: function (result) {
                       if (result.data.length > 0) {
                           $.each(result.data, function (index, value) {
                               controls.cboTypeId.append('<option value="' + value.Code + '">' + value.Description + '</option>');
                           });
                       }
                       else {
                           modalAlert("Ocurrió un error", "Mensaje");
                       }
                   }, error: function () {
                       modalAlert("Ocurrió un error", "Mensaje");
                   }
               });
        },
        cboDocTypeId_Fill: function () {
            var controls = this.getControls();
            $.app.ajax(
               {
                   async: false,
                   modal: false,
                   type: "POST",
                   dataType: "json",
                   url: '~/Coliving/Common/getDocumentType',
                   success: function (result) {
                       if (result.data.length > 0) {
                           $.each(result.data, function (index, value) {
                               controls.cboDocTypeId.append('<option value="' + value.Code + '">' + value.Description + '</option>');
                           });
                       }
                       else {
                           modalAlert("Ocurrió un error", "Mensaje");
                       }
                   }, error: function () {
                       modalAlert("Ocurrió un error", "Mensaje");
                   }
               });
        },
        cboTypeId_Change: function () {
            var that = this,
                controls = that.getControls();
            var strkeySearchType = getValueConfig(['strkeySearchType']).strkeySearchType;
            var _codLine = strkeySearchType.split("|")[0].split(";")[1];
            var _codAccount = strkeySearchType.split("|")[1].split(";")[1];
            var _codDocument = strkeySearchType.split("|")[2].split(";")[1];

            if (controls.cboTypeId.val() == _codLine) {
                controls.divDocTypeId.css("display", "none");
                controls.lblTypeDes.text("Nro de Línea:");
                controls.txtTypeDes.val("");
            }
            else if (controls.cboTypeId.val() == _codAccount) {
                controls.divDocTypeId.css("display", "none");
                controls.lblTypeDes.text("Cuenta:");
                controls.txtTypeDes.val("");
            }
            else if (controls.cboTypeId.val() == _codDocument) {
                controls.divDocTypeId.css("display", "block");
                controls.lblTypeDes.text("Nro Doc. Identidad:");
                controls.txtTypeDes.val("");
            }
        },
        btnClose_Click: function () {

            window.close();
        },

        btnTypeConsult_Click: function () {
            var that = this,
                controls = that.getControls();

            var _value = controls.txtTypeDes.val();
            var _res = true;

            that.ValidateDescriptionEmpty(_value, function (r) {
                _res = r;
            });

            if (_res == false) {
                return;
            }
            var strkeySearchType = getValueConfig(['strkeySearchType']).strkeySearchType;
            var _codLine = strkeySearchType.split("|")[0].split(";")[1];
            var _codAccount = strkeySearchType.split("|")[1].split(";")[1];
            var _codDocument = strkeySearchType.split("|")[2].split(";")[1];

            switch (controls.cboTypeId.val()) {
                case _codLine:
                    that.SearchCustomerLine();
                    break;
                case _codAccount:
                    that.SearchCustomerAccount();
                    break;
                case _codDocument:
                    that.SearchCustomerDocument();
                    break;
            }
        },
        ValidateDescriptionEmpty: function (_value, callback) {
            var that = this,
                controls = that.getControls();
            var _res = true;
            if (_value.trim() == null || _value.trim() == "") {
                controls.modalTypeSearch.modal("hide");
                modalAlert('Por favor, ingrese un valor antes de realizar la consulta', 'Mensaje Transacciones', function (_res) {
                    setTimeout(function () {
                        controls.modalTypeSearch.modal('show');
                    }, 10);
                });
                _res = false;
            }
            return callback(_res);
        },

        SearchCustomerAccount: function () {
            var that = this,
                controls = that.getControls();

            $.window.open({
                modal: false,
                data: { CustomerId: controls.txtTypeDes.val() },
                title: "Detalle de Productos por Cuenta",
                url: '~/Coliving/SearchCustomer/SIACU_Search_Customer_Account',
                width: 1231,
            });
        },
        SearchCustomerLine: function () {
            var controls = this.getControls();
            $.window.open({
                modal: false,
                title: "Detalle de Producto por Cliente",
                url: '~/Coliving/SearchCustomer/SIACU_Search_Customer_Line',
                data:
                    {
                        strTipoIdentificador: controls.cboTypeId.val(),
                    },
                width: 1240
            });
        },
        TxtTypeDes_presskeyIntro: function (event) {
            var controls = this.getControls();
            if (event.keyCode === 13) {
                event.preventDefault();
                controls.btnTypeConsult.trigger('click');
            }
        },
        SearchCustomerDocument: function () {
            $.window.open({
                modal: false,
                title: "Detalle de Cuentas por Cliente",
                url: '~/Coliving/SearchCustomer/SIACU_Search_Customer_Doc_Type',
                width: 1240,
            });
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strUrl: window.location.href
    };
    $.fn.TypeSearchCustomer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];
        this.each(function () {
            var $this = $(this),
                data = $this.data('TypeSearchCustomer'),
                options = $.extend({}, $.fn.TypeSearchCustomer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('TypeSearchCustomer', data);
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
    $.fn.TypeSearchCustomer.defaults = {
        objCustomer: {}
    };
    $('#divContainer').TypeSearchCustomer();

})(jQuery);