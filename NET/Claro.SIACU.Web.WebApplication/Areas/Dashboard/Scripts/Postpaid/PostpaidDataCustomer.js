(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormDataCustomer.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , btnPost_CustomerData: $('#btnPost_CustomerData', $element)
            , btnPost_DireccionCustomer: $('#btnPost_DireccionCustomer', $element)
            , btnPost_SegmentCustomer: $('#btnPost_SegmentCustomer', $element)
            , lblPost_CustomerID: $('#lblPost_CustomerID', $element)
            , lblPost_PhoneReference1: $('#lblPost_PhoneReference1', $element)
            //, lblPost_PhoneReference2: $('#lblPost_PhoneReference2', $element)
            , divPostContenedor: $('#navbar-body > #divContenido > #contenedor')
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnPost_CustomerData.addEvent(that, 'click', that.CustomerData);
            controls.btnPost_DireccionCustomer.addEvent(that, 'click', that.DireccionCustomer);
            controls.btnPost_SegmentCustomer.addEvent(that, 'click', that.SegmentCustomer);
            controls.lblPost_CustomerID.addEvent(that, 'click', that.lblPost_CustomerID_Click);
            controls.lblPost_PhoneReference1.addEvent(that, 'click', that.lblPost_PhoneReference_Click);
            //controls.lblPost_PhoneReference2.addEvent(that, 'click', that.lblPost_PhoneReference_Click);
            controls.divPostContenedor.PostPaid('dataServiceContent');
        },
        CustomerData: function () {
            var objCustomer = Session.DATACUSTOMER;
            objCustomer.BannerMessage = '';
           
            $.window.open({
                modal: false,
                title: "Datos del Cliente",
                url: '~/Dashboard/PostpaidDataCustomer/CustomerData',
                data: { strIdSession: Session.IDSESSION, strDocumentNumber: Session.DATACUSTOMER.DocumentNumber },
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
        
        DireccionCustomer: function () {
            $.window.open({
                modal: false,
                title: "Direcciones de Despacho",
                url: '~/Dashboard/PostpaidDataCustomer/AddressOffice',
                data: { strIdSession: Session.IDSESSION, strDocumentType: Session.DATACUSTOMER.DocumentType, strDocumentNumber: Session.DATACUSTOMER.DNIRUC },
                width: 500,
                height: 300,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        SegmentCustomer: function () {
            $.window.open({
                modal: false,
                title: "Segmento Cliente",
                url: '~/Dashboard/PostpaidDataCustomer/CustomerSegment',
                data: { strIdSession: Session.IDSESSION, strDocumentType: Session.DATACUSTOMER.DocumentType, strDocumentNumber: Session.DATACUSTOMER.DNIRUC },
                width: 800,
                height: 300,
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
        lblPost_CustomerID_Click: function (send, args) {
            if (Session.FlagRedirect == true && Session.EST==true) {
                modalAlert('Debe seleccionar un n&uacute;mero de la cuenta/servicio');
                return;
            }
            if (Session.DATASERVICE == null) {
                
               
                $.app.ajax({
                    async: false,
                    type: 'GET',
                    url: '~/Dashboard/Postpaid/DataServiceContent',
                    data: {
                        strIdSession: Session.IDSESSION,
                        strContratoID: Session.DATACUSTOMER.ContractID,
                        strApplication: Session.DATACUSTOMER.Application,
                        strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                        strDocumentType: Session.DATACUSTOMER.DocumentType,
                        strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                        strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                        strphonespeed: Session.DATACUSTOMER.Telephone,
                        plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                        strCustomerId: Session.DATACUSTOMER.CustomerID,
                        flagconvivencia: Session.DATACUSTOMER.flagConvivencia,
                        coIdPub: Session.DATACUSTOMER.coIdPub
                    },
                    success: function (response) {
                        Session.DATASERVICE = response.data;
                    },
                    error: function (ex) {
                        $('#divPostLineInfo').showMessageErrorLoading($.app.const.messageErrorLoading);
                    }
                });
            }

            if (Session.ORIGINTYPE != "HFC" && Session.ORIGINTYPE != "LTE") {
                Session.OPTIONREDIRECT = 'SU_ACP_DTGEC'
                Session.PSTRCODCLIENTE = "";
                $.redirect.GetParamsData(Session.OPTIONREDIRECT, "POSTPAGO");
            }
            else {
                if (Session.ORIGINTYPE == "LTE")
                    Session.OPTIONREDIRECT = 'SU_LTE_INTER';
                else
                    Session.OPTIONREDIRECT = 'SU_HFC_INTER';

                $.redirect.GetParamsData(Session.OPTIONREDIRECT, Session.ORIGINTYPE);
            }
        },

        lblPost_PhoneReference_Click: function (send, args) {
            if (Session.DATASERVICE == null) {
                
                $.app.ajax({
                    async: false,
                    type: 'GET',
                    url: '~/Dashboard/Postpaid/DataServiceContent',
                    data: {
                        strIdSession: Session.IDSESSION,
                        strContratoID: Session.DATACUSTOMER.ContractID,
                        strApplication: Session.DATACUSTOMER.Application,
                        strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                        strDocumentType: Session.DATACUSTOMER.DocumentType,
                        strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                        strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
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
                        $('#divPostLineInfo').showMessageErrorLoading($.app.const.messageErrorLoading);
                    }
                });
            }
            Session.OPTIONREDIRECT = 'SU_ACP_GAB'
            $.redirect.GetParamsData(Session.OPTIONREDIRECT, "POSTPAGO");
        },
        strUrl: window.location.href

    };

    $.fn.FormDataCustomer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormDataCustomer'),
                options = $.extend({}, $.fn.FormDataCustomer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormDataCustomer', data);
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



    $.fn.FormDataCustomer.defaults = {
    }

    $('#PostpaidDataCustomer').FormDataCustomer();
})(jQuery);





