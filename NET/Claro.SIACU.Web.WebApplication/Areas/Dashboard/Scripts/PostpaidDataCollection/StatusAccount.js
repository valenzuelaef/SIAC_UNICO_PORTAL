(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidStatusAccount.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , DueNumberDocumentlg: $('#DueNumberDocumentlg', $element)
            , StatusAccountLDIlg: $('#StatusAccountLDIlg', $element)
            , BtnFeeEquipmentlg: $('#BtnFeeEquipment', $element)
            , PostpaidDueDocumentNumber: $('#PostpaidDueDocumentNumber', $element)
            , tblStatusAccount: $('#tblStatusAccount', $element)
            , lblClientId: $('#ClientId', $element)
            , lblNameClient: $('#NameClient', $element)
            , lblNameClient2: $('#NameClient2', $element)
            , lblNumberServices: $('#NumberServices', $element)
            , lblContratoId: $('#ContratoId', $element)
            , divStatusAccountHeader: $('#StatusAccountHeader', $element)
            , divStatusAccountHFCHeader: $('#StatusAccountHFCHeader', $element)
            , lblMensajeStatusAccount: $('#lblMensajeStatusAccount', $element)
            , lblUnpaidDebt: $('#UnpaidDebt', $element)
            , lblExpiredDebt: $('#ExpiredDebt', $element)
            , lblTotalDebt: $('#TotalDebt', $element)
            , lblClaimAmount: $('#ClaimAmount', $element)
            , lblToolTipDeudaTotal: $("#ToolTipDeudaTotal", $element)
            , btnDeferredCollections: $('#btnDeferredCollections', $element)
            , tblDescription: $("#tblDescription", $element)
            , hdDataDeferredCollections: $("#hdDataDeferredCollections", $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.DueNumberDocumentlg.addEvent(that, 'click', that.DueNumberDocument);
            controls.StatusAccountLDIlg.addEvent(that, 'click', that.StatusAccountLDI);
            controls.BtnFeeEquipmentlg.addEvent(that, 'click', that.FeeEquipment);
            controls.btnDeferredCollections.addEvent(that, 'click', that.DeferredCollections);

            controls.lblUnpaidDebt.addEvent(that, 'click', that.ShowUnpaidDebt);
            controls.lblExpiredDebt.addEvent(that, 'click', that.ShowExpiredDebt);
            controls.lblClaimAmount.addEvent(that, 'click', that.ShowDetailAmountDispute);

            that.render();
        },
        isMovil: false,
        render: function () {
            var that = this;
            var controls = this.getControls();
            if (Session.ORIGINTYPE == "POSTPAID") {
                controls.BtnFeeEquipmentlg.prop('disabled', true);

            }


            that.getLoadDataTable();
            that.getDEscripciconCarify();
            that.getDueTooltip();
            that.getAccounts();
            that.ValidateOriginType();
        },
        getAddditionalInvoice: function (send, args) {
            var that = this;
            var dataRowTM = that.tblStatusAccount.row($(send).parents('td')).data();
            var data = that.tblStatusAccount.row($(send).parents('tr')).data();

            console.log(data);

            var typeCallTM = dataRowTM.Type;

            console.log("typeCallTM => " + typeCallTM);
            console.log("entró 1");

            var idOnBase = data.IdOnBase;
            var numDoc = data.Document;
            var fecEmi = data.DateIssue;
            console.log("idOnBase => " + idOnBase);
            console.log("numDoc => " + numDoc);
            console.log("fecEmi => " + fecEmi);
            var strplataform = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;

            var cadAnio = fecEmi.substring((fecEmi.lastIndexOf("/") + 1), fecEmi.length);
            var cadMes = fecEmi.substring((fecEmi.indexOf("/") + 1), fecEmi.lastIndexOf("/"));

            var cadPeriodo = cadAnio + cadMes;

            console.log("cadPeriodo => " + cadPeriodo);

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostPaidDataCollection/GetFileRuteToBe',
                data: {
                    strIdSession: Session.IDSESSION, strDocumenName: dataRowTM.Document, strPeriodo: cadPeriodo, strPlataform: strplataform, strIdOnBase: idOnBase, DateIssue: dataRowTM.DateIssue, strType: dataRowTM.Type, isHistoryHR: false
                },
                success: function (result) {
                    if (result.data.FlagBill == "1") {
                        var valFileName = result.data.FileName;
                        var valFilePath = result.data.FilePath;
                        var strNameForm = "NO";
                        var valEmissionDate = result.data.EmissionDate;

                        if (result.data.arrBuffer != null && result.data.arrBuffer != "" && result.data.arrBuffer != undefined) {
                            console.log("Inicio ShowFileToBe");
                            var url = '/Dashboard/PostPaidDataCollection/ShowFileToBe';
                            var urlfin = url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + strNameForm + "&strIdSession=" + Session.IDSESSION + "&strEmissionDate=" + valEmissionDate + "&strIdOnBase=" + idOnBase + "&strDocumenName=" + numDoc + "&strPeriodo=" + cadPeriodo;
                            window.open(urlfin, "FACTURA_ELECTRONICA", "");

                            console.log("Fin ShowFileToBe");
                        } else {
                            showAlert("El archivo no existe");
                        }
                    }
                    else {
                        showAlert("El archivo no existe");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showAlert("El archivo no existe");
                }
            });
        },

        ValidateOriginType: function () {
            var that = this,
            controls = that.getControls();
            var BtnEquipo = document.getElementById("BtnFeeEquipment");
            var BtnStatusAccountLDIlg = document.getElementById("StatusAccountLDIlg");

            if (Session.DATASERVICE.Application == "HFC" || Session.DATASERVICE.Application == "LTE") {
                BtnEquipo.style.visibility = 'hidden';
                BtnStatusAccountLDIlg.style.visibility = 'hidden';
                BtnStatusAccountLDIlg.style.display = 'none';

                controls.StatusAccountLDIlg.css('display', 'none');
                controls.BtnFeeEquipmentlg.css('visibility', 'hidden');
            }

        },
        StatusAccountLDI: function () {

            $.window.open({
                modal: false,
                title: "Estado Cuenta LDI",
                url: '~/Dashboard/PostpaidDataCollection/StatusAccountLDI',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strNameClient: Session.DATACUSTOMER.BusinessName, strNumberServices: Session.DATACUSTOMER.ValueSearch, strContactId: Session.DATACUSTOMER.Account },
                width: 1231,
                height: 550,
                buttons: {

                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },
        getBill_click: function () {

        },

        FeeEquipment: function () {

            var strCustomer;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                strCustomer = Session.DATACUSTOMER.csIdPub;
            } else {
                strCustomer = Session.DATACUSTOMER.CustomerID;
            }

            $.window.open({
                modal: false,
                title: "Consulta Cuotas de Equipo ",
                url: '~/Dashboard/PostpaidDataCollection/FeeEquipment',
                data: { strIdSession: Session.IDSESSION, strCustomerId: strCustomer, strNameClient: Session.DATACUSTOMER.BusinessName, strNumberServices: Session.DATACUSTOMER.ValueSearch, strContactId: Session.DATACUSTOMER.Account },
                width: 1024,
                height: 600,
                buttons: {
                    Exportar: {
                        id: 'btnExportFeeEquipment',
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#ContentFeeEquipment').PostpaidFeeEquipment('GetExportFeeEquipment');
                        }
                    },
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },

        DueNumberDocument: function () {

            $.window.open({
                modal: false,
                title: "Consulta Deuda Número Documento",
                url: '~/Dashboard/PostpaidDataCollection/DueNumberDocument',
                width: 1231,
                height: 600,
                buttons: {
                    Exportar: {
                        id: 'btnExportAcount',
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#PostpaidDueDocumentNumber').FormPostpaidDueDocumentNumber('GetExportDueNumber');
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
        DeferredCollections: function () {
            $.window.open({
                modal: false,
                title: "Consulta de Cobranzas Diferidas",
                url: '~/Dashboard/PostpaidDataCollection/DeferredCollections',
                width: 1231,
                height: 600,
                buttons: {
                    Exportar: {
                        click: function (sender, args) {
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#DeferredCollections').FormDeferredCollections('getExportDeferredCollections');
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
        showHiddenButtonDeferredCollection: function () {
            var that = this,
           controls = that.getControls();

            if (that.dataDeferredCollections != null &&
                that.dataDeferredCollections.data != null &&
                that.dataDeferredCollections.data.DeferredCollections != null &&
                that.dataDeferredCollections.data.DeferredCollections.length > 0) {
                controls.btnDeferredCollections.prop('disabled', false);
            }

        },
        getDeferredCollections: function () {
            var that = this,
            controls = that.getControls();

            var objDeferredCollections = {
                cuenta: Session.DATACUSTOMER.Account,
                periodo: "T",
                tipoConsulta: "3",
                nameClient: controls.lblNameClient.text(),
                numberServices: controls.lblNumberServices.text(),
                contratoId: controls.lblContratoId.text(),
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostPaidDataCollection/GetDeferredCollections',
                data: JSON.stringify(objDeferredCollections),
                complete: function () {
                    that.showHiddenButtonDeferredCollection();
                },
                success: function (response) {
                    that.dataDeferredCollections = response;
                    controls.hdDataDeferredCollections.data('dataDeferredCollections', response);
                }
            });
        },
        GetExportStatusAccount: function () {

            var strUrlModal = '~/Dashboard/PostpaidDataCollection/PostReportStatusAccount',
                strUrlResult = '~/Dashboard/Home/DownloadExcel',
                oDueNumber = {};

            oDueNumber.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oDueNumber.strNameClient = Session.DATACUSTOMER.BusinessName;

            oDueNumber.strCsIdPub = Session.DATACUSTOMER.csIdPub;
            oDueNumber.strplataform = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;

            oDueNumber.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oDueNumber.strContactId = Session.DATACUSTOMER.Account;
            oDueNumber.strIdSession = Session.IDSESSION;
            oDueNumber.strOriginType = Session.ORIGINTYPE;


            console.log("strplataform =>" + oDueNumber.strplataform);

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(oDueNumber),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=EstadoCuenta.xlsx";
                }
            });
        },
        GetClaimByReceipt: function (ListStatusAccount) {
            var that = this, controls = that.getControls();
            var ListClaimByReceipt = [];
            var documents = "";
            for (var i = 0; i < ListStatusAccount.length; i++) {
                documents = documents + ",'" + ListStatusAccount[i].Document + "'";
            }
            documents = documents.substr(1);

            var objBSS_StatusAccountRequest = {
                tipoConsulta: "4",
                numeroDocumentos: documents
            }

            $.app.ajax({
                async: false,
                type: 'POST',
                dataType: 'json',
                data: { strIdSession: Session.IDSESSION, objBSS_StatusAccountRequest: objBSS_StatusAccountRequest },
                url: '~/Dashboard/PostPaidDataCollection/GetClaimByReceipt',
                success: function (response) {
                    if (response.data.responseStatus.codigoRespuesta == 0) {
                        ListClaimByReceipt = response.data.responseDataConsultar.listaReclamoPorRecibo
                    }
                },
                error: function (msger) {
                }
            });
            return ListClaimByReceipt;
        },

        getLoadDataTable: function () {
            var that = this,
            controls = that.getControls();
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostPaidDataCollection/StatusAccounts',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strCsIdPub: Session.DATACUSTOMER.csIdPub, strplataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strNameClient: Session.DATACUSTOMER.BusinessName, strNumberServices: Session.DATACUSTOMER.ValueSearch, strContactId: Session.DATACUSTOMER.Account, strOriginType: Session.ORIGINTYPE },
                complete: function () {
                    controls.lblNameClient.text(Session.DATACUSTOMER.BusinessName);
                    controls.lblNumberServices.text(Session.DATACUSTOMER.ValueSearch);
                    controls.lblContratoId.text(Session.DATACUSTOMER.Account);
                    if ($(".dataTables_scrollBody").length != 0) {
                        $(".dataTables_scrollBody").animate({ scrollTop: $('.dataTables_scrollBody')[$(".dataTables_scrollBody").length - 1].scrollHeight }, 1000);

                    }
                    that.additionalInvoice_click();
                    that.FunctionTableDescription();
                    that.allowCancelInvoice(); //PROY-140464
                    $("#customers").append($("#tblDescription_wrapper"));
                    $("#tblDescription").show();

                },
                success: function (data) {
                    console.log("correcto estado cuenta");
                    var jsonObjData = JSON.stringify(data, function (key, value) {
                        if (value == null)
                            return "";
                        return value;
                    });
                    data = JSON.parse(jsonObjData);
                    controls.lblClientId.text(data.list.ClientId);
                    //controls.lblNameClient.text(data.list.NameClient);
                    controls.lblNameClient2.text(data.list.NameClient2);
                    // controls.lblContratoId.text(data.list.ContratoId);
                    // controls.lblNumberServices.text(data.list.NumberServices)
                    controls.BtnFeeEquipmentlg.prop("disabled", !data.list.IsEnabled);
                    if (data.list.blMessageStatusAccountVisible) {
                        controls.lblMensajeStatusAccount.show();
                        controls.lblMensajeStatusAccount.text(data.list.strMessageStatusAccount)
                    } else {
                        controls.lblMensajeStatusAccount.hide();
                    }
                    var ListClaimByReceipt = [];

                    $('#tblStatusAccount tbody').html('');
                    that.tblStatusAccount = controls.tblStatusAccount.DataTable({
                        "info": false,
                        "scrollY": "300px",
                        "scrollCollapse": true,
                        "paging": false,
                        "select": "single",
                        "destroy": true,
                        "data": data.list.listStatusAccountModel,
                        "columns": [
                            { "data": "CorrelativeNumber" },
                            { "data": "Type" },
                            { "data": "Document" },
                            { "data": "Ajuste" },
                            { "data": "DescriptionPaid" },
                            { "data": "DateRegister", type: 'date-dd-mmm-yyyy' },
                            { "data": "DateIssue", type: 'date-dd-mmm-yyyy' },
                            { "data": "DateDue", type: 'date-dd-mmm-yyyy' },
                            { "data": "Charge" },
                            { "data": "Payment" },
                            { "data": "ImportPending" },
                            { "data": "AmountClaimed" },
                            { "data": "Balance" },
                            { "data": "IsInvoceCancelable" }, //PROY-140464
                            { "data": "StatusDocument" }, //PROY-140464
                            { "data": "IdOnBase" }
                        ],
                        columnDefs: [

                            {
                                targets: 2,
                                render: function (data, type, full, meta) {
                                    var cadena = "",
                                        strCodDocMGR = "";
                                    for (var i = 0, d = full.Document.length; i < d; i++) {
                                        if (data[i] != "-")
                                            cadena += data[i];
                                        else
                                            break;
                                    }
                                    console.log("cadena => " + cadena);
                                    if ((full.Type === "Factura" && cadena === "AM02") ||
                                        (full.Type === "Factura" && cadena === "AM01") ||
                                        (full.Type === "Factura" && cadena === "AMP1") ||
                                        (full.Type === "Factura" && cadena === "AMP2") ||
                                        (full.Type === "Factura" && cadena === "AMX") ||
                                        (full.Type === "Factura" && cadena === "T001") ||
                                        (full.Type === "Factura" && cadena === "SB01") ||
                                        (full.Type === "Factura" && cadena === "SB02") ||
                                        (full.Type === "Ajuste" && cadena === "AM01") ||
                                        (full.Type === "Ajuste" && cadena === "AMP1") ||
                                        (full.Type === "Ajuste" && cadena === "AMP2") ||
                                        (full.Type === "Ajuste" && (cadena === "TP01" || cadena === "TP02")
                                        && (Session.ORIGINTYPE === "POSTPAID"))) {
                                        return "<a class='Document' style='cursor: pointer'>" + data + "</a>";
                                    }
                                     //INC000003124634 
                                    if ((full.Type == "Ajuste" && (cadena == "DAJ1" || cadena == "DAJ2" || cadena == "SA01" || cadena == "SA02"))) {
                                        return "<a class='documentAdjustment' href='#'>" + data + "</a>";
                                    }
                                    console.log("1");
                                    if (cadena != null && cadena != "" && full.Type != null && full.Type != "") {
                                        if (cadena.length >= strCodDocMGR.length) {
                                            if (full.Type.toUpperCase() == "" && cadena.substring(0, strCodDocMGR.length).toUpperCase() == strCodDocMGR.toUpperCase()) {
                                                return "<a class='Document' style='cursor: pointer'>" + data + "</a>";
                                            }
                                        }
                                    }
                                    console.log("2");
                                    return data;
                                    console.log("Data" + data);
                                }
                            },
                            {
                                targets: 11,
                                render: function (data, type, full, meta) {
                                    var count = 0;
                                    for (var i = 0; i < ListClaimByReceipt.length; i++) {
                                        if (full.Document == ListClaimByReceipt[i].documento) {
                                            count = count + 1;
                                            if (count > 1) { break; }
                                        }
                                    }

                                    if (count > 1)//si el documento tiene mas de un reclamo asociado,abrir ventana
                                        return "<a class='reclaimedAmount' href='#'><u>" + data + "</u></a>";
                                    else if (count == 1)//si el documento tiene solo un reclamo asociado, redirecciona
                                        return "<a class='redirectProblemManagement' href='#'><u>" + data + "</u></a>";
                                    else//si el documento no tiene un reclamo asociado
                                        return data;
                                }
                            },
                            {//PROY-140464 - INI
                                targets: 13,
                                render: function (data, type, full, meta) {
                                    if (full.IsInvoceCancelable.length === 0) {
                                        return "";
                                    } else {
                                        if (full.IsInvoceCancelable == "1" && Session.USERACCESS.canCancelInvoice) {
                                            return "<input type='button' class='allowCancelInvoice' value='Anulación' />";
                                        } else {
                                            return "";
                                        }
                                    }
                                }
                            },//PROY-140464 - FIN
                            {
                                targets: 15,
                                visible: false,
                                searchable: false,
                                orderable: false
                            }
                        ],
                        "drawCallback": function (settings) {
                            if ($(".dataTables_scrollBody").length != 0) {
                                $(".dataTables_scrollBody").animate({ scrollTop: $('.dataTables_scrollBody')[$(".dataTables_scrollBody").length - 1].scrollHeight }, 1000);

                            }
                        },
                        "language": {
                            "lengthMenu": "Display _MENU_ records per page",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)"
                        }
                    });

                    if (Session.ORIGINTYPE == "POSTPAID") {
                        if (controls.tblStatusAccount.DataTable().rows().count() > 0) {
                            controls.BtnFeeEquipmentlg.show();
                        } else {
                            controls.BtnFeeEquipmentlg.hide();
                        }
                    }
                    controls.lblClaimAmount.html("<a class='ClaimAmount' href='#'><u>" + controls.tblStatusAccount.DataTable().column(11).data().sum().toFixed(2) + "</u></a>");
                },
                error: function () { console.log("error estado cuenta"); },
            });
            if (Session.DATASERVICE.Application == 'POSTPAID') {
                controls.divStatusAccountHFCHeader.css("display", "none")
            } else {
                controls.divStatusAccountHeader.css("display", "none")
            }
        },

        getAssociatedClaims: function (send, args) {
            var that = this,
               controls = that.getControls();

            var document = send.parents("tr").find("td").eq(2).find("a").html();
            var ListClaimByReceipt = controls.tblStatusAccount.data("ListClaimByReceipt");
            var ClaimByReceipt = [];
            for (var i = 0; i < ListClaimByReceipt.length; i++) {
                if (ListClaimByReceipt[i].documento == document) {
                    ClaimByReceipt.push(ListClaimByReceipt[i]);
                }
            }

            controls.tblStatusAccount.data("ClaimByReceipt", ClaimByReceipt);

            $.window.open({
                modal: false,
                title: "Reclamos Asociados",
                url: '~/Dashboard/PostpaidDataCollection/AssociatedClaims',
                width: screen.availWidth,
                height: screen.availHeight,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });

        },
        addRow: function () {
            this.tblDescription.row.add({ 'datoEvalua': 'Documentos Reclamados', 'parametro1': '' }).draw();
        },
        getDEscripciconCarify: function () {
            var that = this,
                controls = that.getControls();
            var obj = {
                strIdSession: Session.IDSESSION,
                objGetDescriptionsRequest: {
                    tipoProceso: "ESTADO_CUENTA"
                }
            };
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostpaidDataCollection/GetDescriptions',
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                complete: function () {
                    that.addRow();
                },
                success: function (response) {
                    that.setTableDescription(response.data.datosParametroClarify);
                }
            });

        },

        getDueTooltip: function () {
            var that = this,
                controls = that.getControls();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: '~/Dashboard/PostpaidDataCollection/GetDueTooltip',
                success: function (Data) {
                    controls.lblToolTipDeudaTotal.text(Data.strDueToolTip);
                },
                error: function (msger) {

                }
            });
        },

        FunctionTableDescription: function () {

            var that = this,
                controls = that.getControls(),
                table = that.tblStatusAccount;

            $('#search-inp').on('keyup', function () {
                table.search($(this).val()).draw();
            });

            $("#search-inp").after('<div  id="customers"></div>');
            $('#customers').hide();
            $("#search-inp").focus(function () {
                $('#customers').fadeIn(200);
            });
            $("#search-inp").focusout(function () {
                $('#customers').fadeOut(500);
            });
            $("#tblDescription >tbody > tr").click(function () {
                $(this).addClass('highlight').siblings().removeClass('highlight');
            });

            $('#tblDescription').on('click', 'tbody tr', function (event) {
                if ($('table > tbody > tr.highlight') !== undefined) {
                    $("#search-inp").val($('table > tbody > tr.highlight').text().trim());

                    var DataRows = that.tblDescription.row(this).data(),
                        filtros = DataRows.parametro1,
                        aFiltros = filtros.split('%');


                    $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                        var sFiltros = '';
                        if (aFiltros[0] == "") {
                            sFiltros += settings.aoData[dataIndex].anCells[11].innerHTML.indexOf('</a>') != -1 ? true : false;
                        } else {
                            $.each(aFiltros, function (index1, filtro) {
                                var aOpradores = ['>', '<', '==', '!='];

                                if (filtro != '||' && filtro != '&') {
                                    $.each(aOpradores, function (index2, operador) {
                                        if (filtro.indexOf(operador) != -1) {
                                            var aFiltro = filtro.split(operador);
                                            var aColumn = [];

                                            aColumn[0] = aFiltro[0].charAt(0);
                                            aColumn[1] = aFiltro[1].charAt(0);

                                            if (aColumn[0] == "$") {
                                                var columna = aFiltro[0].split("$")[1];

                                                var tipo = isNaN(parseInt(data[columna])) ? "'" + data[columna] + "'" : data[columna];

                                                sFiltros += tipo + operador;

                                            } else {
                                                sFiltros += aFiltro[0] + operador;

                                            }
                                            if (aColumn[1] == "$") {
                                                var columna = aFiltro[1].split("$")[1];//11

                                                sFiltros += data[columna];

                                            } else {

                                                sFiltros += aFiltro[1];

                                            }
                                            return false;
                                        }
                                    });
                                } else {
                                    sFiltros += filtro;
                                }
                            });
                        }
                        return eval(sFiltros) ? true : false;
                    });
                    table.draw();
                    $.fn.dataTable.ext.search.pop();
                }
            });
        },

        setTableDescription: function (data) {
            var that = this,
                controls = that.getControls();

            that.tblDescription = controls.tblDescription.DataTable({
                "info": false,
                "scrollY": "100px",
                "scrollCollapse": true,
                "searching": false,
                "paging": false,
                "select": "single",
                "destroy": true,
                "width": "50px",
                "Height": "250px",
                "data": data,
                "ordering": false,

                "columns": [
                    { "data": "datoEvalua" },
                    { "data": "parametro1" }
                ],
                columnDefs: [
                    {
                        "targets": [1],
                        "visible": false
                    }

                ],
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "search": "Buscar:"
                }
            });
        },

        getAccounts: function () {
            var that = this,
            controls = that.getControls();
            var is = Session.IDSESSION;
            var Dato = 0;
            var cboBuscar = window.opener.$('#ddlTipoBusqueda').val();
            if (cboBuscar == "1") {
                console.log("busqueda por telefono: 2");
                var doc_consulta = Session.DATACUSTOMER.Telephone
                var cod_consulta = 2
            } else if (cboBuscar == "3") {
                console.log("busqueda por contrato:4");
                var doc_consulta = Session.DATACUSTOMER.ContractID
                var cod_consulta = 4
            } else {
                console.log("busqueda por cuenta: 1 , busqueda alterna");
                var doc_consulta = Session.DATACUSTOMER.Account;
                var cod_consulta = 1;
            }


            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostpaidDataCollection/ListsDueNumberDocument_EC',
                data: { strCod_consulta: cod_consulta, strDocLinea: doc_consulta, strIdSession: is, strPlataform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT, strDocNum: Session.DATACUSTOMER.DocumentNumber, strDocType: Session.DATACUSTOMER.DocumentTypeId },
                success: function (response) {
                    console.log("entrando a Succes getAccounts");
                    if (response.data.ListCollectionDueDocumentNumberModelMovil.length != 0) {
                        console.log("entrando a getAccounts");
                        $.each(response.data.ListCollectionDueDocumentNumberModelMovil, function (key, value) {
                            if (value.IdAccount == Session.DATACUSTOMER.Account) {
                                var deudaNVencida = (parseFloat(value.CurrentDue) - parseFloat(parseFloat(value.ExpiredDue) + parseFloat(value.PunishedDue))).toFixed(2);
                                var deudaVencida = (parseFloat(value.ExpiredDue) + parseFloat(value.PunishedDue)).toFixed(2);
                                var deudaCorriente = controls.lblTotalDebt.text(parseFloat(value.CurrentDue).toFixed(2));


                                if (deudaNVencida == "-0.00" || deudaNVencida == "0") {
                                    controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + "0.00" + "</u></a>");
                                } else {
                                    controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + deudaNVencida + "</u></a>");
                                } if (deudaVencida == "-0.00" || deudaVencida == "0") {
                                    controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + "0.00" + "</u></a>");
                                } else {
                                    controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + deudaVencida + "</u></a>");
                                } if (deudaCorriente == "-0.00" || deudaCorriente == "0") {
                                    controls.lblTotalDebt.text(0.00);
                                }
                                Dato = 1;
                            }
                        });
                    }
                    else {
                        controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + "0.00" + "</u></a>");
                        controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + "0.00" + "</u></a>");
                        controls.lblTotalDebt.text(0.00);
                    }
                    if (Dato == 0) {
                        if (response.data.ListCollectionDueDocumentNumberModelFija.length != 0) {
                            $.each(response.data.ListCollectionDueDocumentNumberModelFija, function (key, value) {
                                if (value.IdAccount == Session.DATACUSTOMER.Account) {

                                    var deudaNVencida = (parseFloat(value.CurrentDue) - parseFloat(parseFloat(value.ExpiredDue) + parseFloat(value.PunishedDue))).toFixed(2);
                                    var deudaVencida = (parseFloat(value.ExpiredDue) + parseFloat(value.PunishedDue)).toFixed(2);
                                    var deudaCorriente = controls.lblTotalDebt.text(parseFloat(value.CurrentDue).toFixed(2));


                                    if (deudaNVencida == "-0.00" || deudaNVencida == "0") {
                                        controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + "0.00" + "</u></a>");
                                    } else {
                                        controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + deudaNVencida + "</u></a>");
                                    } if (deudaVencida == "-0.00" || deudaVencida == "0") {
                                        controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + "0.00" + "</u></a>");
                                    } else {
                                        controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + deudaVencida + "</u></a>");
                                    } if (deudaCorriente == "-0.00" || deudaCorriente == "0") {
                                        controls.lblTotalDebt.text(0.00);
                                    }

                                }
                            });
                        }
                        else {
                            controls.lblUnpaidDebt.html("<a class='UnpaidDebt' href='#'><u>" + "0.00" + "</u></a>");
                            controls.lblExpiredDebt.html("<a class='ExpiredDebt' href='#'><u>" + "0.00" + "</u></a>");
                            controls.lblTotalDebt.text(0.00);
                        }
                    }
                },
                error: function () { },
            });
        },

        ShowUnpaidDebt: function (send, args) {
            $.window.open({
                modal: false,
                title: "Deuda No Vencida",
                url: '~/Dashboard/PostpaidDataCollection/UnpaidDebt',
                width: 800,
                height: 500,
                buttons: {
                    Aceptar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },
        ShowExpiredDebt: function (send, args) {
            $.window.open({
                modal: false,
                title: "Deuda Vencida",
                url: '~/Dashboard/PostpaidDataCollection/ExpiredDebt',
                width: 800,
                height: 500,
                buttons: {
                    Aceptar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },
        ShowDetailAmountDispute: function () {
            $.window.open({
                modal: false,
                title: "Detalle de Monto en Disputa",
                url: '~/Dashboard/PostpaidDataCollection/DetailAmountDispute',
                data: { strIdSession: Session.IDSESSION, CustomerId: Session.DATACUSTOMER.CustomerID },
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            }).maximize();
        },
        additionalInvoice_click: function () {
            var that = this,
            controls = that.getControls();
            controls.tblStatusAccount.find('tbody').find('a[class="Document"]').addEvent(that, 'click', that.getAddditionalInvoice);
            controls.tblStatusAccount.find('tbody').find('a[class="Ajuste"]').addEvent(that, 'click', $.Ajuste.ValidateRedirect);
            controls.tblStatusAccount.find('tbody').find('a[class="AjusteHFC"]').addEvent(that, 'click', $.Ajuste.ValidateRedirect);

            controls.tblStatusAccount.find('tbody').find('a[class="documentAdjustment"]').addEvent(that, 'click', that.redirectAuthorizedDocumentSettings);
            controls.tblStatusAccount.find('tbody').find('a[class="reclaimedAmount"]').addEvent(that, 'click', that.getAssociatedClaims);
            controls.tblStatusAccount.find('tbody').find('a[class="redirectProblemManagement"]').addEvent(that, 'click', that.redirectProblemManagement);

        },
        redirectProblemManagement: function (send, args) {
            var that = this,
               controls = that.getControls();

            var document = send.parents("tr").find("td").eq(2).find("a").html();
            var ListClaimByReceipt = controls.tblStatusAccount.data("ListClaimByReceipt");
            var Claim = [];
            for (var i = 0; i < ListClaimByReceipt.length; i++) {
                if (ListClaimByReceipt[i].documento == document) {
                    Claim = ListClaimByReceipt[i];
                    break;
                }
            }

            Session.EstadoForm = "E";
            Session.tipoCasoInteraccion = "1";
            Session.CasoInteraccionId = Claim.caso;
            Session.telefono = Claim.reclamo;
            Session.CasoID = Claim.caso;
            $.redirect.GetParamsData("SU_REC_GPRO", "RECLAMOS");
        },
        redirectAuthorizedDocumentSettings: function (send, args) {
            var that = this;
            var oData = that.tblStatusAccount.row($(send).parents('td')).data();
            var strDocument = oData.Document;

            if (Session.ORIGINTYPE == 'POSTPAID' || Session.ORIGINTYPE == 'IFI')
                Session.TRANSACCION = 'TRANSACCION_AJUSTE_DA';
            if (Session.ORIGINTYPE == 'DTH')
                Session.TRANSACCION = 'TRANSACCION_DTH_AJUSTE_DA';
            if (Session.ORIGINTYPE == 'HFC')
                Session.TRANSACCION = 'TRANSACCION_REGISTRO_AJUSTES_HFC';
            if (Session.ORIGINTYPE == 'LTE')
                Session.TRANSACCION = 'TRANSACCION_REGISTRO_AJUSTES_LTE';

            that.getInteraction(strDocument);
        },
        getInteraction: function (strDocument) {
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostPaidDataCollection/GetInteraction',
                data: { strIdSession: Session.IDSESSION, Document: strDocument },
                complete: function () {
                    $.redirect.GetParamsData("SU_REC_ADAC", "RECLAMOS");
                },
                success: function (data) {
                    Session.CasoID = data.interaction;
                }
            });
        },
        getTypeDocument: function () {
            switch (Session.DATACUSTOMER.DNIRUC.length) {
                case 8:
                    return 1;
                case 11:
                    return 6;
            }
        },
        //PROY-140464 - INI
        allowCancelInvoice: function () {
            var that = this,
                controls = that.getControls();

            controls.tblStatusAccount.find('tbody').find('[class="allowCancelInvoice"]').addEvent(that, 'click', that.CancelInvoice);
        },
        CancelInvoice: function (send, args) {
            var that = this,
                objDetailsRow = that.tblStatusAccount.row($(send).parents('td')).data();

            var data = {
                strIdSession: Session.IDSESSION,
                strDocumentInvoice: objDetailsRow.Document,
                strCharge: objDetailsRow.Charge,
                strPayment: objDetailsRow.Payment
            }

            $.window.open({
                modal: false,
                title: "Motivo",
                data: data,
                url: '/Dashboard/PostPaidDataCollection/CancelInvoice',
                width: 450,
                height: 230
            });

        },
        //PROY-140464 - FIN
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };
    $.fn.FormPostpaidStatusAccount = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportStatusAccount'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidStatusAccount'),
                options = $.extend({}, $.fn.FormPostpaidStatusAccount.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidStatusAccount', data);
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

    $.fn.FormPostpaidStatusAccount.defaults = {};

    $('#PostpaidStatusAccount', $('.modal:last')).FormPostpaidStatusAccount();
})(jQuery);
