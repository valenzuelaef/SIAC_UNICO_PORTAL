(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidDataCustomer.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

			, linkSegment: $('#linkSegment', $element)
			, btnConsultContact: $('#btnConsultContact', $element)
			, btnCustomersPrevious: $('#btnCustomersPrevious', $element)
			, lblPre_Name: $('#lblPre_Name', $element)
			, lblPre_Lastname: $('#lblPre_Lastname', $element)
			, lblPre_Address: $('#lblPre_Address', $element)
			, lblPre_NumberDocument: $('#lblPre_NumberDocument', $element)
			, lblPre_TelephoneCustomer: $('#lblPre_TelephoneCustomer', $element)
			, lblPre_State: $('#lblPre_State', $element)
			, lblPre_Segment: $('#lblPre_Segment', $element)
			, chkPre_Membership: $('#chkPre_Membership', $element)
			, lblPre_BannerMessage: $('#lblPre_BannerMessage', $element)
			, lblPre_ProductType: $('#lblPre_ProductType', $element)
			, hidTelephoneCustomer: $('#hidTelephoneCustomer', $element)
			, hidNumberDocument: $('#hidNumberDocument', $element)
			, hidTypeDocument: $('#hidTypeDocument', $element)
			, divDataCustomer: $('#divDataCustomer', $element)
			, hidCustomerCode: $('#hidCustomerCode', $element)
			, btnReloadCustomer: $('#btnReloadCustomer', $element)
			, btnCreateNotUser: $('#btnCreateNotUser', $element)
            , contenedorCustomerPast: $('#contenedorCustomerPast', $element)
			, ddlNotUser_MotiveRegister: $('#ddlNotUser_MotiveRegister', $element)
            , checkAppMiclaro: $('#checkAppMiclaro', $element)
            , lblVersionAppMiClaro: $('#lblVersionAppMiClaro', $element)
            , lblFechaUltTranAppMiClaro: $('#lblFechaUltTranAppMiClaro', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
				controls = this.getControls();

            controls.linkSegment.addEvent(that, 'click', that.linkSegment_click);
            controls.btnConsultContact.addEvent(that, 'click', that.btnConsultContact_click);
            controls.btnCustomersPrevious.addEvent(that, 'click', that.btnCustomersPrevious_click);
            controls.btnReloadCustomer.addEvent(that, 'click', that.btnReloadCustomer_click);
            controls.btnCreateNotUser.addEvent(that, 'click', that.btnCreateNotUser_click);

            that.render();
        },
        render: function () {
            var that = this;

            that.validateShowButtonRegisterNotUser();
        },


        linkSegment_click: function () {
            var controls = this.getControls();

            $.window.open({
                modal: false,
                title: "SEGMENTO CLIENTE",
                url:  '~/Dashboard/PrepaidDataCustomer/CustomerSegment',
                data: { strIdSession: Session.IDSESSION, strTypeDocument: controls.hidTypeDocument.val(), strNumberDocument: controls.hidNumberDocument.val(), strTelephoneCustomer: Session.TELEPHONE },
                width: 900,
                height: 470,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },
        btnConsultContact_click: function () {
            if (Session.DATACUSTOMER.CustomerCode != null && Session.DATACUSTOMER.CustomerCode != '') {
            $.window.open({
                modal: false,
                title: "CONSULTA DATOS DE CONTACTO CLARIFY",
                url: '~/Dashboard/PrepaidDataCustomer/CustomerQuery',
                closeOnEscape: false,
                type: 'post',
                data: {},
                width: 900,
                height: 500,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        },
                        id: "btnCloseCustomer",
                    }
                }
            });
            } else {
                $.redirect.GetParamsData('SU_SIACA_LDOL', 'PREPAGO');
            }           
        },
        btnCustomersPrevious_click: function () {
            var that = this;
            var IsTFI = false;
            if (Session.flagPrePagoDesac && typeof Session.DATACUSTOMER.isTfiInac != "undefined" && Session.DATACUSTOMER.isTfiInac != null) {

                if (Session.DATACUSTOMER.isTfiInac == "true") { IsTFI = true; }
            } else {
            if (Session.DATASERVICE.IsTFI) { IsTFI = Session.DATASERVICE.IsTFI }
            }

            $.window.open({
                modal: false,
                title: "LISTA DE CLIENTES ANTIGUOS",
                url: '~/Dashboard/PrepaidDataCustomer/CustomerPast',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, strFlagHistory: "0", IsTFI: IsTFI },
                width: 900,
                height: 500,
                buttons: {
                    Seleccionar: {
                        click: function (sender, args) {

                            var modalJQuery = args.event.view.$;

                            var rowPost = modalJQuery('#tblCustomerPast').DataTable().rows({ selected: true }).data();

                            if (modalJQuery('#tblCustomerPast').DataTable().rows({ selected: true }).data()[0] == undefined) {
                              
                                showAlert("Debe Seleccionar un Cliente!", "Mensaje");
                            }
                            else {

                                that.getDataCustomer(rowPost[0][1]);
                                this.close();
                            }

                        }
                    },
                    "...": {
                        click: function () {

                            var parameters = { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, strFlagHistory: "1" }

                            $.app.ajax({
                                type: 'POST',
                                url: '~/Dashboard/PrepaidDataCustomer/CustomerPast',
                                dataType: 'html',
                                cache: true,
                                data: parameters,
                                container: $('#contenedorCustomerPast'),
                                complete: function () {
                                    $("#tblCustomerPast").DataTable({
                                        "scrollY": "200px",
                                        "scrollCollapse": true,
                                        "paging": false,
                                        "searching": false,
                                        "destroy": true,
                                        "select": {
                                            "style": "os",
                                            "info": false
                                        },
                                        "language": {
                                            "lengthMenu": "Display _MENU_ records per page",
                                            "zeroRecords": "No existen datos",
                                            "info": " ",
                                            "infoEmpty": " ",
                                            "infoFiltered": "(filtered from _MAX_ total records)"
                                        },
                                        "columnDefs": [{
                                            "orderable": false,
                                            "className": 'select-radio',
                                            "targets": 0,
                                        },
                                        {
                                            "targets": 1,
                                            "visible": false
                                        }
                                        ]

                                    });
                                },
                                error: function (ex) {
                                }
                            });
                        }

                    },
                    Cerrar: {
                        click: function () {
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
        getRoute: function () {
            return window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        },
        customerPrepaid: function (objCustomer) {

            var that = this,
			controls = this.getControls();

            controls.lblPre_Name.text(objCustomer.Name);
            controls.lblPre_Lastname.text(objCustomer.Lastname);
            controls.lblPre_Address.text(objCustomer.Address);
            controls.lblPre_NumberDocument.text(objCustomer.TypeDocument.concat(' ', objCustomer.NumberDocument));
            if (objCustomer.TelephoneReference == null)
                controls.lblPre_TelephoneCustomer.text('');
            else
                controls.lblPre_TelephoneCustomer.text(objCustomer.TelephoneReference);

            controls.lblPre_State.text(objCustomer.State);
            controls.lblPre_Segment.text(objCustomer.Segment);
            controls.lblPre_Segment.css('color', 'red');
            controls.chkPre_Membership.prop("checked", objCustomer.Membership);
            controls.lblPre_BannerMessage.append(objCustomer.BannerMessage);
            //Set Hidden
            controls.hidTelephoneCustomer.val(objCustomer.TelephoneCustomer);
            controls.hidNumberDocument.val(objCustomer.NumberDocument);
            controls.hidTypeDocument.val(objCustomer.TypeDocument);

            controls.btnConsultContact.prop("disabled", false);
            controls.btnCustomersPrevious.prop("disabled", false);
            controls.btnCreateNotUser.prop("disabled", false);
            controls.checkAppMiclaro.attr('checked', objCustomer.IsAppMiclaro);
            if (objCustomer.AppMiclaroVersion == null)
                controls.lblVersionAppMiClaro.text('');
            else
                controls.lblVersionAppMiClaro.text(objCustomer.AppMiclaroVersion);
            controls.checkAppMiclaro.attr('disabled', 'disabled');
            if (objCustomer.AppMiClaroLastDate == null)
                controls.lblFechaUltTranAppMiClaro.text('');
            else
                controls.lblFechaUltTranAppMiClaro.text(objCustomer.AppMiClaroLastDate);


            if (controls.lblPre_State.text() != 'Activo') {
                controls.lblPre_State.text('I');
                controls.lblPre_State.css('color', 'red');
            }
            else {
                controls.lblPre_State.text('A');
                controls.lblPre_State.css('color', 'blue');
            }

            that.validateTelephoneCustomer();
            that.validateDataCustomer(objCustomer.Name);
            if (typeof Session.strMsjValidationPre != "undefined" && Session.strMsjValidationPre != null) {
                if (Session.strMsjValidationPre.indexOf("baja") > 0 && (Session.ORIGINTYPE == 'PREPAID' || Session.ORIGINTYPE == 'PREPAGO')) {
                    controls.lblPre_ProductType.text(objCustomer.ProductType.slice(0, 7));
                } else {
            controls.lblPre_ProductType.text(objCustomer.ProductType);
                }
            } else {
                controls.lblPre_ProductType.text(objCustomer.ProductType);
            }


            Session.TYPESERVICE = objCustomer.ProductType;
        },
        customerPrepaidClean: function () {
            var controls = this.getControls();

            controls.lblPre_Name.text('');
            controls.lblPre_Lastname.text('');
            controls.lblPre_Address.text('');
            controls.lblPre_NumberDocument.text('');
            controls.lblPre_TelephoneCustomer.text('');
            controls.lblPre_State.text('');
            controls.lblPre_Segment.text('');
            controls.chkPre_Membership.prop("checked", false);
            controls.lblPre_BannerMessage.text('');
            controls.lblPre_ProductType.text('');
            //Set Hidden
            controls.hidTelephoneCustomer.val('');
            controls.hidNumberDocument.val('');
            controls.hidTypeDocument.val('');

            controls.btnConsultContact.prop("disabled", true);
            controls.btnCustomersPrevious.prop("disabled", true);
            controls.btnCreateNotUser.prop("disabled", true);

            controls.checkAppMiclaro.attr('checked', false);
            controls.lblVersionAppMiClaro.text('');
            controls.lblFechaUltTranAppMiClaro.text('');

        },
        validateDataCustomer: function (data) {

            var that = this;
            if (data == 'null' || data == null) {
                that.customerPrepaidClean();
            }

        },
        validateTelephoneCustomer: function () {

            var that = this;

            var isTFI = false,
				strProviderID = "",
				strCustomerCode = "";

            if (Session.DATACUSTOMER.oDataService != null) {
                isTFI = Session.DATACUSTOMER.oDataService.IsTFI;
                strProviderID = Session.DATACUSTOMER.oDataService.ProviderID;
                strCustomerCode = Session.DATACUSTOMER.CustomerCode;
            }

            $(document).ajaxStop(function () {

                if (Session.CUSTOMEROLD == '1') {

                    if (Session.MESSAGEPREPAID == '1') {

                        Session.MESSAGEPREPAID = '0';

                        $.app.ajax({
                            type: 'POST',
                            url: '~/Dashboard/Prepaid/GetValidateTelephoneCustomer',
                            dataType: 'json', 
                            cache: false,
                            data: { strIdSession: Session.IDSESSION, strTelephone: Session.TELEPHONE, isTFI: isTFI, strProviderID: strProviderID, strCodResponde: Session.CODERESPONSEPREPAIDSERVICE, strCustomerCode: strCustomerCode },
                            success: function (result) {

                                Session.SHOWPOPUPCUSTOMEROLD = result.showPopud;
                                Session.SHOWDATACUSTOMERPREPAID = result.showData;
                                if (result.message == '') {
                                    if (Session.CUSTOMEROLD == '1' && Session.ORIGINTYPE == 'PREPAID') {
                                        Session.CUSTOMEROLD = '0';
                                        if (Session.SHOWPOPUPCUSTOMEROLD == '1') {
                                            Session.SHOWPOPUPCUSTOMEROLD = '0';
                                            that.showClientPast();
                                        }
                                    }
                                }
                                else {
                                    if (typeof Session.strMsjValidationPre != "undefined" && Session.strMsjValidationPre != null) {
                                        if (Session.strMsjValidationPre != "") {
                                            if (Session.strMsjValidationPre.indexOf("baja") <= 0 && (Session.ORIGINTYPE == 'PREPAID' || Session.ORIGINTYPE == 'PREPAGO')) {
                                    showAlert(result.message);
                                            }
                                        }

                                    } else {
                                    showAlert(result.message);
                                    }


                                }

                                if (Session.SHOWDATACUSTOMERPREPAID == '0') {
                                    that.customerPrepaidClean();
                                }
                            },
                            error: function (ex) {
                                

                            }
                        });
                    }
                }
            });

            if (Session.SHOWDATACUSTOMERPREPAID == '0') {
                that.customerPrepaidClean();
            }

        },
        getDataCustomer: function (obj) {
            var controls = this.getControls();

            blockUI('#divDataCustomer');

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Prepaid/GetDataCustomer',
                dataType: 'json',
                cache: false,
                data: { strIdSession: Session.IDSESSION, strCustomerCode: obj, strTelephone: Session.TELEPHONE },
                success: function (response) {
                    $.app.ajax({
                        type: 'POST',
                        url: '~/Dashboard/Prepaid/DataCustomer',
                        dataType: 'html',
                        cache: false,
                        container: $('#divDataCustomer'),
                        complete: function () {
                            Session.IsClear = true;
                            $('#contenedor').Prepaid('tabDataService_click');
                        },
                        success: function (result) {
                            Session.DATACUSTOMER = response;
                            $('#contenedorDataCustomer').PrepaidDataCustomer('customerPrepaid', Session.DATACUSTOMER);
                        }
                    });
                },
                error: function (ex) {
                    controls.divDataCustomer.showMessageErrorLoading($.app.const.messageErrorLoading);

                }
            });



        },
        showClientPast: function () {

            var controls = this.getControls();
            var IsTFI = false;
            if (Session.DATASERVICE.IsTFI) { IsTFI = Session.DATASERVICE.IsTFI }
            
            $.window.open({
                modal: false,
                title: "LISTA DE CLIENTES ANTIGUOS",
                url: '~/Dashboard/PrepaidDataCustomer/CustomerPast',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, strFlagHistory: "0", IsTFI: IsTFI },
                width: 900,
                height: 600,
                buttons: {
                    Seleccionar: {
                        click: function () {
                           
                            $('#contenedorDataCustomer').PrepaidDataCustomer('getDataCustomer', controls.hidCustomerCode.val())
                            this.close();
                        }
                    },
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    },
                }
            });

        },
        btnReloadCustomer_click: function () {
            $('#contenedor').Prepaid('dataCustomerPre');
        },
        btnCreateNotUser_click: function () {

            $.window.open({
                modal: false,
                title: "CREACION DE NO USUARIO",
                url: '~/Dashboard/PrepaidDataCustomer/CreateNotUser',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE },
                width: 900,
                height: 650,
                buttons: {
                    Guardar: {
                        click: function () {
                            $('#contenedorCreateNotUser').PrepaidCreateNotUser('registerNotUser');
                        }
                    },
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            }).maximize();

        },
        validateShowButtonRegisterNotUser: function () {
            var controls = this.getControls();

            var flagContact;
            if (Session.DATACUSTOMER.CustomerCode != "" || Session.DATACUSTOMER.CustomerCode != null) {
                flagContact = "E";
            }

            if (flagContact == "E") {
                controls.btnCreateNotUser.hide();
            }
            if (Session.TELEPHONE == "" || Session.TELEPHONE == null) {
                controls.btnCreateNotUser.hide();
            }

            SetDataRedirect(Session);
            if (Session.USERACCESS != null) {
                if (Session.USERACCESS.optionPermissions != null && Session.USERACCESS.optionPermissions != "") {
                    if (Session.USERACCESS.optionPermissions.indexOf(Session.DATACUSTOMER.PermissionInteraction) < 0) {
                        controls.btnCreateNotUser.hide();
                    }
                }
            }

            if (Session.DATACUSTOMER.CustomerCode != "" && Session.DATACUSTOMER.CustomerCode != null) {
                if (Session.DATACUSTOMER.oDataService != null) {
                    if (Session.DATACUSTOMER.oDataService.ContactType == "P") {
                        controls.btnCreateNotUser.hide();
                    }
                }
            }
            else {
                controls.btnCreateNotUser.hide();
            }
        }

    };

    $.fn.PrepaidDataCustomer = function () {
        var option = arguments[0],
			args = arguments,
			value,
			allowedMethods = ['selectCustomer', 'customerPrepaid', 'getDataCustomer', 'showClientPast', 'validateTelephoneCustomer', 'customerPrepaidClean'];

        this.each(function () {

            var $this = $(this),
				data = $this.data('PrepaidDataCustomer'),
				options = $.extend({}, $.fn.PrepaidDataCustomer.defaults,
					$this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidDataCustomer', data);
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

    $.fn.PrepaidDataCustomer.defaults = {
    }

    $('#contenedorDataCustomer').PrepaidDataCustomer();

})(jQuery);


