(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidContact.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbContactPost: $('#tbContactPost', $element)
            , hidPerfil: $('#hidPerfil', $element)
            , hidNroCuenta: $('#hidNroCuenta', $element)
            , hidTelefono: $('#hidTelefono', $element)
            , hidAvailableEmails: $('#hidAvailableEmails', $element)
            , hidDocumentTypeDNI: $('#hidDocumentTypeDNI', $element)
            , hidDocumentTypeRUC: $('#hidDocumentTypeRUC', $element)
            , hidDocumentTypePAS: $('#hidDocumentTypePAS', $element)
            , hidDocumentTypeCEX: $('#hidDocumentTypeCEX', $element)
            , hidDocumentTypeCIP: $('#hidDocumentTypeCIP', $element)
            , hidDocumentTypeCFA: $('#hidDocumentTypeCFA', $element)
            , tbContactPostMessage: $('#tbContactPostMessage', $element)
            , AddbtnPostPaid: $('#AddbtnPostPaid', $element)
            , SavebtnPostPaid: $('#SavebtnPostPaid', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.fn_CargarInfoContacto();
            $(window).data('vintContBD', 0);
            $(window).data('vintAgregar', 0);
            
        },
        fn_CargarInfoContacto: function () {
           
            var that = this;
            
            var controls = this.getControls();
         

            if (that.fn_RegistroContacto() === true) {
                showAlert('Ud., no tie_e los permisos asignados, consulte con el Administrador.','Alerta')

            } else {

                var urlAdd = '~/Dashboard/PostpaidDataCustomer/GetContact';
                var stroptionPermissions = Session.USERACCESS.optionPermissions;
                $.app.ajax({
                    type: 'POST',
                    url: urlAdd,
                    data: {
                        strIdSession: Session.IDSESSION,
                        strAccount: Session.DATACUSTOMER.Account,
                        strTelephone: Session.DATACUSTOMER.ValueSearch
                    },
                    success: function (result) {
                        var strAcceso = Session.USERACCESS.optionPermissions;
                        var NewState = result.data.NewState;
                        var ModifyState = result.data.ModifyState;
                        var ConsultState = result.data.ConsultState;
                        if ((stroptionPermissions.indexOf(NewState) == -1) && (stroptionPermissions.indexOf(ModifyState) == -1) && (stroptionPermissions.indexOf(ConsultState) > -1)) {
                            controls.hidPerfil.val(result.data.ConsultConst);

                        } else if ((stroptionPermissions.indexOf(NewState) > -1) && (stroptionPermissions.indexOf(ModifyState) == -1) && (stroptionPermissions.indexOf(ConsultState) == -1)) {
                            controls.hidPerfil.val(result.data.AddConst);
                        } else if ((stroptionPermissions.indexOf(NewState) == -1) && (stroptionPermissions.indexOf(ModifyState) > -1) && (stroptionPermissions.indexOf(ConsultState) == -1)) {
                            controls.hidPerfil.val(result.data.EditConst);
                        } else if ((stroptionPermissions.indexOf(NewState) > -1) && (stroptionPermissions.indexOf(ModifyState) > -1) && (stroptionPermissions.indexOf(ConsultState) > -1)) {
                            controls.hidPerfil.val(result.data.AddEditConst);
                        } else if ((strAcceso.indexOf(NewState) > -1) && (strAcceso.indexOf(ModifyState) > -1) && (strAcceso.indexOf(ConsultState) == -1)) {
                            controls.hidPerfil.val(result.data.AddEditConst);
                        }

                        controls.hidNroCuenta.val(result.data.AccountNumber);
                        controls.hidTelefono.val(result.data.Telephone);
                        controls.hidAvailableEmails.val(result.data.AvailableEmails);
                        controls.hidDocumentTypeDNI.val(result.data.DocumentTypeDNI);
                        controls.hidDocumentTypeRUC.val(result.data.DocumentTypeRUC);
                        controls.hidDocumentTypePAS.val(result.data.DocumentTypePAS);
                        controls.hidDocumentTypeCEX.val(result.data.DocumentTypeCEX);
                        controls.hidDocumentTypeCIP.val(result.data.DocumentTypeCIP);
                        controls.hidDocumentTypeCFA.val(result.data.DocumentTypeCFA);



                        var listContact = result.data.lstContact;
                        var ltdo = result.data.lstDataType;
                        var ltc = result.data.lstContactType;
                        var ltd = result.data.lstDocumentType;
                        var ltcc = result.data.lstContactTypeField;
                       
                        var grdContactos = new ContactoGrilla("tbContactPost", ltdo, ltc, ltd);
                        grdContactos.permiteMultipleEdicion(true);
                        grdContactos.comandoEjecutado(that.createDelegate(that, that.grdContactos_comandoEjecutado));
                       
                        for (var i = 0, l = ltcc.length; i < l; i++) {
                            var tipoContactoCampo = ltcc[i];
                            grdContactos.columnas().agregar(tipoContactoCampo);
                        }
                        $(window).data('vintContBD', listContact.length);
                        controls.tbContactPost.find('thead').find('tr').append('<th>COPIAR</th>');
                        var opEdicion = controls.hidPerfil.val();


                        switch (opEdicion) {
                            case 'NM':
                                controls.tbContactPost.find('thead').find('tr').append('<th></th>');
                              
                                break;
                            case 'N':
                           
                                break;
                            case 'M':
                                controls.tbContactPost.find('thead').find('tr').append('<th></th>');
                                controls.AddbtnPostPaid.css('display', 'none');
                           
                                break;
                            case 'C':
                                controls.AddbtnPostPaid.css('display', 'none');
                                controls.SavebtnPostPaid.css('display', 'none');
                                break;
                        }

                       if (listContact != null) {
                           that.LlenarGrillaContactos(listContact, grdContactos.tiposContacto(), grdContactos);
                        }
                    },
                    error: function (ex) {
                        $.app.error({
                            id: 'PostpaidContact',
                            message: ex
                        });
                    }

                });

            }
            
        },
        createDelegate: function (instance, method, args) {
            return function () {
                return method.apply(instance, arguments);
            }
        },
        grdContactos_comandoEjecutado: function (sender, args) {
            var comando = args['comando'];
            var controls = this.getControls();
            
            var opPerfil = controls.hidPerfil.val();

            switch (comando) {
                case 'ultimaFila_teclaAbajo_tab':
                    if ((opPerfil === 'N') || (opPerfil === 'MN')) {
                        document.getElementById('hidAccion').value = 'N';
                        sender.crearContacto();
                    }
                    break;
                case 'contacto_eliminado':
                    if (sender.filas().conteo() < 1) {
                        break;
                    }
            }
        },
        LlenarGrillaContactos: function (ListaContacto, ListaTipoContacto, grdContactos) {
            if (ListaContacto === null) {
                ListaContacto = [];
            }

            var controls = this.getControls();
            var conteoGrilla = grdContactos.filas().conteo();
           
            while (conteoGrilla > 0) {
                grdContactos.filas().trae(0).eliminar();
                conteoGrilla = grdContactos.filas().conteo();
            }

            if (ListaContacto !== null && ListaContacto.length > 0) {
               for (var intContactoPosicion = 0, intContactoCantidad = ListaContacto.length-1; intContactoPosicion <= intContactoCantidad; intContactoPosicion++) {
                    var objContacto = ListaContacto[intContactoPosicion]
                        , blnAgregar = false
                        , intTipoContacto = objContacto['TCCTN_CODIGO']
                        , arrAdicional = objContacto['CamposAdicionales']
                        , strTipoDocumento = null
                        , strNroDocumento = null;

                    for (var intTipoContactoPosicion = 0, intTipoContactoCantidad = ListaTipoContacto.length; intTipoContactoPosicion < intTipoContactoCantidad; intTipoContactoPosicion++) {
                        var objTipoContacto = ListaTipoContacto[intTipoContactoPosicion];

                        if (objTipoContacto['TCCTN_CODIGO'] === intTipoContacto) {
                            blnAgregar = true;

                            break;

                        }
                    }

                    if (blnAgregar) {
                        for (var intAdicionalPosicion = 0, intAdicionalCantidad = arrAdicional.length; intAdicionalPosicion < intAdicionalCantidad; intAdicionalPosicion++) {
                            var objAdicional = arrAdicional[intAdicionalPosicion];

                            if (objAdicional['TCCCN_CODIGO'] === 4 && strTipoDocumento === null) {
                                strTipoDocumento = objAdicional['TCCVV_VALOR'];
                            } else if (objAdicional['TCCCN_CODIGO'] === 5 && strNroDocumento === null) {
                                strNroDocumento = objAdicional['TCCVV_VALOR'];
                            } else if (strTipoDocumento !== null && strNroDocumento !== null) {
                                break;
                            }
                        }

                        for (var intContactoPosicion1 = 0, intContactoCantidad1 = grdContactos.filas().conteo() ; intContactoPosicion1 < intContactoCantidad1; intContactoPosicion1++) {
                            var objContacto1 = grdContactos.filas().trae(intContactoPosicion1)._contacto
                                , intTipoContacto1 = objContacto1['TCCTN_CODIGO']
                                , arrAdicional1 = objContacto1['CamposAdicionales']
                                , strTipoDocumento1 = null
                                , strNroDocumento1 = null;

                            for (var intAdicionalPosicion1 = 0, intAdicionalCantidad1 = arrAdicional1.length; intAdicionalPosicion1 < intAdicionalCantidad1; intAdicionalPosicion1++) {
                                var objAdicional1 = arrAdicional1[intAdicionalPosicion1];

                                if (objAdicional1['TCCCN_CODIGO'] === 4 && strTipoDocumento1 === null) {
                                    strTipoDocumento1 = objAdicional1['TCCVV_VALOR'];
                                } else if (objAdicional1['TCCCN_CODIGO'] === 5 && strNroDocumento1 === null) {
                                    strNroDocumento1 = objAdicional1['TCCVV_VALOR'];
                                } else if (strTipoDocumento1 !== null && strNroDocumento1 !== null) {
                                    if (intTipoContacto === intTipoContacto1
                                        && strTipoDocumento === strTipoDocumento1
                                        && strNroDocumento === strNroDocumento1) {

                                        blnAgregar = false;
                                    }

                                    break;
                                }
                            }
                        }
                    }

                    if (blnAgregar) {
                        grdContactos.agregarContacto(objContacto);
                    }
                }
            } else {
                controls.tbContactPostMessage.html(
                '<div class="alert alert-danger" role="alert">' +
                '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"> El cliente Consultado no tiene contactos</span>' +
                '</div>');
            }
            window.opener.$(window).data('grdContactos', grdContactos);
        },
        fn_RegistroContacto: function () {
            var controls = this.getControls();
            var opPerfil = controls.hidPerfil.val();
            var blnEstado = false;

            if ((opPerfil != "undefined") || (opPerfil != null)) {

                if (opPerfil === 'M') {
                   controls.AddbtnPostPaid.css('display', 'none');
                }

                document.getElementById('hidAccion').value = '';
            } else {
                blnEstado = true;
            }
            return blnEstado;
        },
        fn_Agregar: function (grdContactos) {
            document.getElementById('hidAccion').value = 'N';
            var  grdContactos=window.opener.$(window).data('grdContactos');
            grdContactos.crearContacto();
            var n = parseInt($(window).data('vintAgregar')) + 1;
            $(window).data('vintAgregar', n);
        },
        fn_Guardar: function (grdContactos) {
            var grdContactos = window.opener.$(window).data('grdContactos');
           
            if (grdContactos.validarContactos()) {
                grdContactos.llenarVistaDeEstado(grdContactos);
                document.getElementById('hidId').value = '';
                document.getElementById('hidAccion').value = '';
                document.getElementById('hidNuevo').value = '';
                document.getElementById('hidEditar').value = '';
                $(window).data('vintAgregar', 0);
            }
          
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };


    $.fn.FormPostpaidContact = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['fn_CargarInfoContacto', 'fn_Agregar', 'fn_Guardar'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidContact'),
                options = $.extend({}, $.fn.FormPostpaidContact.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidContact', data);
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



    $.fn.FormPostpaidContact.defaults = {
    }
    $('#PostpaidContact', $('.modal:last')).FormPostpaidContact();
})(jQuery);
