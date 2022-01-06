(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.CancelInvoiceForm.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            cboReasonCancelInvoice: $('#cboReasonCancelInvoice', $element),
            btnCancelInvoice: $('#btnCancelInvoice', $element),
            nroDocumentInvoice: $('#nroDocumentInvoice', $element),
            btnClose: $('#btnClose', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnCancelInvoice.addEvent(that, 'click', that.cancelInvoice_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);

            that.render();
        },

        render: function () {
            this.getReasonForCancelInvoice();
        },

        getReasonForCancelInvoice: function () {
            var that = this,
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostPaidDataCollection/ListReasonCancelInvoice',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    if (response != null) {
                        that.displayCancelReason(response.data);
                    }
                    else {
                        alert('No se encontró información en la obtención de la lista de motivos.');
                    }
                },
                error: function (msg) {
                    alert('Error en la consulta de lista de motivos.');
                },
            });
        },

        displayCancelReason: function (data) {
            var that = this,
                controls = that.getControls();

            controls.cboReasonCancelInvoice.append($('<option>', { value: '', html: 'Seleccionar' }));

            $.each(data, function (index, value) {
                controls.cboReasonCancelInvoice.append($('<option>', { value: value.Code, html: value.Description }));
            });
        },

        cancelInvoice_click: function () {
            var that = this,
				controls = that.getControls(),
				codigoAnulacion = controls.cboReasonCancelInvoice.val();

            if (codigoAnulacion.length === 0) {
                alert('Debe seleccionar un motivo.');
            } else {
                that.validateProfile();
            }
        },

        validateProfile: function () {
            var that = this,
                controls = that.getControls();

            var objValidateProfile = {
                strIdSession: Session.IDSESSION,
                tipoProducto: Session.DATASERVICE.TypeService,
                codigoUsuarioAnulacion: Session.USERACCESS.login
            }

            $.app.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'text',
                url: '~/Dashboard/PostPaidDataCollection/IsValidProfile',
                data: JSON.stringify(objValidateProfile),
                success: function (response) {
                    if (response != '') {
                        that.identifyTypeOfDocument(response);
                    } else {
                        alert('Su perfil no tiene asignado un monto.');
                    };
                },
                error: function (msg) {
                    alert('Error en la consulta para validación de perfil.');
                }
            });
        },

        identifyTypeOfDocument: function (data) {
            var that = this,
				nroDocumento = $('#nroDocumentInvoice').val(),
				Cargo = $('#strCharge').val(),
                Abono = $('#strPayment').val(),
				importeDocumentoAnulado = '',
				tipoDocumento = '',
				codigoPerfil = data;

            if (nroDocumento.includes('DAJ1')) {
                importeDocumentoAnulado = Abono;
                tipoDocumento = 'Nota de Crédito';
            }
            else if (nroDocumento.includes('DAJ2')) {  //DAJ1: Nota de Credito, DAJ2: Nota de Debito DCAJ: Documento Control
                importeDocumentoAnulado = Cargo;
                tipoDocumento = 'Nota de Débito';
            } else {
                importeDocumentoAnulado = Abono;
                tipoDocumento = 'Documento de control';
            }
            that.validateAmountPerProfile(importeDocumentoAnulado, codigoPerfil, tipoDocumento, nroDocumento);
        },

        validateAmountPerProfile: function (importeDocumentoAnulado, perfilDelUsuario, tipoDocumento, nroDocumento) {
            var that = this,
                codigoModalidad = Session.MODALIDAD = "Corporativo" ? "1" : "2",
                transaccion = '';

            if (Session.ORIGINTYPE == 'POSTPAID') {
                transaccion = 'TRANSACCION_AJUSTE_DA';
            } else if (Session.ORIGINTYPE == 'HFC') {
                transaccion = 'TRANSACCION_REGISTRO_AJUSTES_HFC';
            };

            var objEvaluateAmount = {
                idSession: Session.IDSESSION,
                perfilDelUsuario: perfilDelUsuario,
                monto: importeDocumentoAnulado,
                codigoModalidad: codigoModalidad,
                transaccion: transaccion
            };

            $.app.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'text',
                url: '~/Dashboard/PostPaidDataCollection/GetEvaluateAmount',
                data: JSON.stringify(objEvaluateAmount),
                success: function (response) {
                    if (response == "1") {
                        that.cancelInvoice(response, importeDocumentoAnulado, perfilDelUsuario, tipoDocumento, nroDocumento);
                    } else {
                        alert('Su perfil no tiene asignado el monto para proceder con la anulación.');
                    }
                },
                error: function (msg) {
                    alert('Error en la consulta para evaluación de montos.');
                },
            });
        },

        cancelInvoice: function (data, importeDocumentoAnulado, perfilDelUsuario, tipoDocumento, nroDocumento) {
            var that = this,
				controls = that.getControls(),
				valorAnulacion = controls.cboReasonCancelInvoice.val(),
				motivoAnulacion = $("#cboReasonCancelInvoice option:selected").text();

            modalConfirm('Esta seguro de anular el documento: ' + nroDocumento + '.', '', function (result) {

                if (result == true) {
                    var objCancelInvoice = {
                        strIdSession: Session.IDSESSION,
                        numeroDocumento: nroDocumento,
                        codigoAnulacion: valorAnulacion,
                        codigoSistemaOrigen: Session.USERACCESS.areaId,
                        comentarios: "Anulacion de documento.",
                        numeroTelefonico: Session.DATASERVICE.CellPhone,
                        puntoAtencion: Session.USERACCESS.areaName,
                        codigoUsuarioAnulacion: Session.USERACCESS.login,
                        tipoServicio: Session.DATASERVICE.TypeService,
                        motivoAnulacion: motivoAnulacion,
                        importeAnulacion: importeDocumentoAnulado,
                        nombreCompletoCliente: Session.DATACUSTOMER.FullName,
                        idCliente: Session.DATACUSTOMER.CustomerID,
                        tipoDocumento: tipoDocumento,
                        nombreCompletoUsuarioAnulacion: Session.USERACCESS.fullName
                    };

                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'text',
                        url: '~/Dashboard/PostPaidDataCollection/ExecuteCancelInvoice',
                        data: JSON.stringify(objCancelInvoice),
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
                            that.validationInvoiceCancelation(response, nroDocumento);
                        },
                        error: function (msg) {
                            alert('Error en la conexión al servidor.');
                        },
                        complete: function () {
                            $.unblockUI();
                        }
                    });
                }
                else {
                    return;
                }
            });
        },

        validationInvoiceCancelation: function (data, numeroDocumento) {
            if (data == "0") {
                alert('Se realizo la anulación del documento: ' + numeroDocumento);
                window.close();
                window.parent.opener.location.reload();
            } else {
                alert('No ha sido posible completar la operación.');
            }

        },

        btnClose_click: function () {
            window.close();
        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.CancelInvoiceForm = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getExportDeferredCollections'];

        this.each(function () {
            var $this = $(this),
                data = $this.data('CancelInvoiceForm'),
                options = $.extend({}, $.fn.CancelInvoiceForm.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CancelInvoiceForm', data);
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

    $('#CancelInvoice').CancelInvoiceForm();

})(jQuery);