
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountRelationPlan.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , tblRelationPlan: $('#tblRelationPlan', $element)
            , tblBagDetail: $('#tblBagDetail', $element)
            , tblRequests: $('#tblRequests', $element)
            , btnGenerateRequest: $('#btnGenerateRequest', $element)
            , btnRefresh: $('#btnRefresh', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = that.getControls();

            controls.btnGenerateRequest.addEvent(that, 'click', that.btnGenerateRequest_click);
            controls.btnRefresh.addEvent(that, 'click', that.btnRefresh_click);
            this.render();
        },
        btnRefresh_click: function () {
            this.getPlanCorporate();
        },
        btnGenerateRequest_click: function () {
            var that = this,
                controls = that.getControls(),
                objRequests = {
                    strIdSession: Session.IDSESSION,
                    strCustomerId: Session.DATACUSTOMER.CustomerID,
                    objCustomer: Session.DATACUSTOMER,
                    strApplication: "SIACU"
                };
            $('#lblMensaje').text("Generando Solicitud...");
            document.getElementById("btnGenerateRequest").disabled = true;
            $.app.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(objRequests),
                url: '~/Dashboard/PostpaidDataAccount/RegisterSolicitude',
                complete: function () {
                    document.getElementById("btnGenerateRequest").disabled = false;
                },
                success: function (result) {
                    if (result.data.MessageResponse.Header.HeaderResponse.status.code == "0") {
                        if (result.data.MessageResponse.Body.registrarSolicitudPendienteResponse.pendiente == null)
                            modalAlert("Solicitud " + result.data.MessageResponse.Body.registrarSolicitudResponse.idsolicitud + " Registrada Exitosamente", "Mensaje", that.getPlanCorporate());
                        else {
                            var estado = result.data.MessageResponse.Body.registrarSolicitudPendienteResponse.pendiente == "0" ? "pendiente" : "en proceso",
                                usuario = result.data.MessageResponse.Body.registrarSolicitudPendienteResponse.usuario;
                            modalAlert("El usuario <b>" + usuario + "</b> tiene una solicitud <b>" + estado + "</b>, por favor espere un momento hasta que termine.", "Aviso", that.getPlanCorporate());
                        }
                    } else {
                        modalAlert("Ocurrio un error, intentelo denuevo porfavor", "Mensaje");
                        $('#lblMensaje').text("");
                    }
                },
                error: function () {
                    $('#lblMensaje').text("Ocurrio un error con el servidor remoto.");
                    modalAlert("Ocurrio un error, intentelo denuevo porfavor", "Mensaje");
                }
            });
        },
        desingtblRelationPlan: function () {
            var that = this,
            controls = that.getControls();
            controls.tblRelationPlan.DataTable(
                 {
                     info: false
               , scrollX: true
                , scrollY: "200px"
                , select: "single"
                , paging: false
                , scrollCollapse: true
                , searching: false
                , destroy: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                 }
            );

        },
        desingtblBagDetail: function () {
            var that = this,

            controls = that.getControls();
            controls.tblBagDetail.DataTable({
                info: false
                , scrollY: 150
                , select: "single"
                , orderCellsTop: true
                , paging: false
                , searching: false
                , destroy: true
                , language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
            });
        },
        crearTablaRelacionLineasCorporativo: function (aListResquests) {
            var that = this,
                controls = that.getControls();
            $('#lblMensaje').text("");
            that.tableRequest = controls.tblRequests.DataTable({
                info: false
                   , paging: false
                   , searching: false
                   , destroy: true
                   , scrollY: 400
                   , scrollCollapse: true
                   , order: [[0, 'desc']]
                   , data: aListResquests
                   , columns: [
                       { "data": "id" },
                       { "data": "usuario" },
                       { "data": "aplicativo" },
                       { "data": "fechaSolicitud" },
                       { "data": "fechaTermino" },
                       { "data": "estado" },
                       { "data": null }
                   ]
                   , columnDefs: [
                       { className: "text-center", targets: "_all" },
                       {
                           targets: 3,
                           render: function (data, type, full, meta) {
                               var d = new Date(data);
                               return d.toLocaleString();
                           }
                       },
                       {
                           targets: 4,
                           render: function (data, type, full, meta) {
                               if (data == null)
                                   return "-";
                               else
                                   var d = new Date(data);
                               return d.toLocaleString();
                           }
                       },
                       {
                           targets: 5,
                           render: function (data, type, full, meta) {
                               switch (data) {
                                   case "0":
                                       return "Pendiente";
                                   case "1":
                                       return "En Proceso";
                                   case "2":
                                       return "Terminada";
                                   case "3":
                                       return "Fallida";
                               }
                           }
                       },
                       {
                           targets: 6,
                           render: function (data, type, full, meta) {
                               if (full.estado == "2")
                                   return "<a class='bolder' href='#'>Descargar</a>";
                               else
                                   return "Descargar";
                           }
                       },
                   ]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        buildCorporateInterface: function () {
            if (Session.DATACUSTOMER.CustomerType.toUpperCase() == "BUSINESS" || Session.DATACUSTOMER.CustomerType.toUpperCase() == "B2E") {
                $("#divCorporativo").removeClass("hide");
                $('.modal-footer button').first().remove();
                this.getPlanCorporate();
            }
        },
        download_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblRequests.find('tbody a').addEvent(that, 'click', that.getExcelRelationPlan);
        },
        getExcelRelationPlan: function (send, args) {
            var that = this,
            objExcelRelationPlan = {},
            objRow = that.tableRequest.row($(send).parents('tr')).data(),
            strFileName = objRow.nombreArchivo.split('.'),
            request = { strIdSession: Session.IDSESSION, strFileName: strFileName[0] };

            var parentsup = $(send).closest('tr'),
                linkDownload = $(send).closest('tr').find("td").eq(6).find("a").detach();

            parentsup.find("td").eq(6).append("Descargando...");

            args.event.preventDefault();

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataAccount/GetExcelRelationPlan',
                data: JSON.stringify(request),
                complete: function () {
                    parentsup.find("td").eq(6).html(linkDownload);
                },
                success: function (path) {
                    switch (path) {
                        case "error":
                            modalAlert("Ocurrio un error al procesar el mensaje.", "Aviso");
                            break;
                        case "1":
                            modalAlert("Se debe enviar campos obligatorios.", "Aviso");
                            break;
                        case "2":
                            modalAlert("No se encontró el archivo.", "Aviso");
                            break;
                        case "3":
                            modalAlert("Error al desencriptar las credenciales.", "Aviso");
                            break;
                        case "4":
                            modalAlert("El archivo existe pero está vacío.", "Aviso");
                            break;
                        case "-1":
                            modalAlert("Ocurrió un error inesperado.", "Aviso");
                            break;
                        case "-2":
                            modalAlert("Error con el servidor FTP.", "Aviso");
                            break;
                        case "-3":
                            modalAlert("Error de disponibilidad del servicio ebsConsultaClaveWS.", "Aviso");
                            break;
                        case "-4":
                            modalAlert("Error de timeout al llamar al servicio ebsConsultaClaveWS.", "Aviso");
                            break;
                        default:
                            $('#lblMensaje').text("");
                            window.location = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }) + '?strPath=' + path + "&strNewfileName=" + objRow.nombreArchivo;
                    }
                },
                error: function () {
                    $('#lblMensaje').text("Ocurrio un error con el servidor remoto.");
                    modalAlert("Ocurrio un error con el servidor remoto.", "Aviso");
                }
            });
        },
        GetExportRelationPlan: function () {

            var strUrlModal = '~/Dashboard/PostpaidDataAccount/AccountExportRelationPlan';
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.objDatacustomer = Session.DATACUSTOMER;

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=RelationPlan.xlsx";
                }
            });
        },
        getPlanCorporate: function () {
            var that = this,
                request = { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID };
            $('#lblMensaje').text("Consultando Solicitudes...");
            document.getElementById("btnRefresh").disabled = true;
            $.app.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(request),
                url: '~/Dashboard/PostpaidDataAccount/GetSolicitude',
                complete: function () {
                    that.download_click();
                    document.getElementById("btnRefresh").disabled = false;
                },
                success: function (result) {
                    that.crearTablaRelacionLineasCorporativo(result.data);
                },
                error: function () {
                    $('#lblMensaje').text("Ocurrio un error con el servidor remoto.");
                    modalAlert("Ocurrio un error con el servidor remoto.", "Aviso");
                }
            });
        },
        render: function () {
            var that = this;
            that.desingtblRelationPlan();
            that.desingtblBagDetail();
            that.buildCorporateInterface();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.formAccountRelationPlan = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportRelationPlan'];

        this.each(function () {
            var $this = $(this),
                data = $this.data('formAccountRelationPlan'),
                options = $.extend({}, $.fn.formAccountRelationPlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountRelationPlan', data);
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

    $.fn.formAccountRelationPlan.defaults = {
    }

    $('#ContentPostpaidAccountRelationPlan', $('.modal:last')).formAccountRelationPlan();

})(jQuery);
