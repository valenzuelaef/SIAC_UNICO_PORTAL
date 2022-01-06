
(function ($, undefined) {
    'use strict'
    var Form = function ($element, options) {
        $.extend(this, $.fn.FormCustomerDocument.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , txtHdDocNumber: $('#txtHdDocNumber ', $element)
            , txtHdLineNumber: $('#txtHdLineNumber', $element)
            , txtHdTypeDocId: $('#txtHdTypeDocId ', $element)
            , txtHdMigrateOne: $('#txtHdMigrateOne ', $element)
            , txtHdValidateMigration: $('#txtHdValidateMigration', $element)
            , txtHdUrlWebSpecial: $('#txtHdUrlWebSpecial', $element)
            , txtHdNameLinkWebSpecial: $('#txtHdNameLinkWebSpecial', $element)
            , txtHdNameLinkPostVenta: $('#txtHdNameLinkPostVenta', $element)
            , txtHdUrlPostVenta: $("#txtHdUrlPostVenta", $element)
            , txtHdstrPrepagoOlo: $("#txtHdstrPrepagoOlo", $element)
            , txtHdStatus: $('#txtHdStatus', $element)
            , txtHdKeyMigradoFullStack: $('#txtHdKeyMigradoFullStack', $element)
            , txtHdNumberAccount: $("#txtHdNumberAccount", $element)

            , lblTypeDocDescription: $('#lblTypeDocDescription', $element)
            , lblNumberDocument: $('#lblNumberDocument', $element)
            , lblNames: $('#lblNames', $element)
            , lblAddress: $('#lblAddress', $element)

            , btnSpecialCases: $('#btnSpecialCases', $element)
            , btnSale: $('#btnSale', $element)
            , btnClose: $('#btnClose', $element)
            , btnPostSale: $('#btnPostSale', $element)
            , btnTechnicalService: $('#btnTechnicalService', $element)
            , txtHdUrlDestino: $('#txtHdUrlDestino', $element)

        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = that.getControls();
            controls.btnSale.addEvent(this, 'click', that.btnSale_click);
            controls.btnSpecialCases.addEvent(this, 'click', that.btnSpecialCases_click);
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            controls.btnPostSale.addEvent(this, 'click', that.btnPostSale_click);
            controls.btnTechnicalService.addEvent(this, 'click', that.btnTechnicalService_click);

            that.render();
            that.resizeContent();
        },
        render: function () {
            var that = this;
            that.DisabledTechnicalService();
            that.LoadEnaDisSpecialCase();
            that.DataCustomerDocument();
            that.addEventListServiceActive();
            that.addEventListServiceInactive();
            that.rdbLinesNumberChange();
            that.rdbLinePreChange();
            that.removeEmptyRow();
        },
        removeEmptyRow: function () {
            $("#TbServiciosPost_wrapper .row").first().remove();
            $("#TbServiciosPre_wrapper .row").first().remove();
        },
        btnSale_click: function () {
            var controls = this.getControls();
            var _bol = true;
            $("#TbServiciosPost tr").each(function () {
                if ($("input[name='rdbtnLine']:radio").is(':checked')) {
                    return _bol = false;
                }
            });
            if (_bol == false) {
                var _DocumentTypeDescription = controls.lblTypeDocDescription.text();
                var _DocumentNumber = controls.txtHdDocNumber.val();

                var _data = {
                    strIdSession: Session.IDSESSION,
                    DocumentTypeDescription: _DocumentTypeDescription,
                    DocumentNumber: _DocumentNumber,
                };
                var txtLineNumberListService = $("#txtLineNumberListService").text();
                console.log("txtLineNumberListService: " + txtLineNumberListService);
                $.window.open({
                    modal: false,
                    title: "Criterios de Venta",
                    data: _data,
                    url: '/Coliving/SearchCustomer/SIACU_Criteria_Sale',
                    width: 600,
                    height: 280
                });
            } else {
                modalAlert('Estimado usuario, Seleccione una cuenta o una linea', 'Mensaje Transacciones');
            }
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
        btnTechnicalService_click: function () {
            var controls = this.getControls();
            var _DocumentTypeDescription = controls.lblTypeDocDescription.text();
            var _ClientType = controls.txtHdMigrateOne.val();
            var _MigrateStatus = controls.txtHdMigrateOne.val().toUpperCase();
            var _Client = controls.lblNames.text();
            var _LineNumber = controls.txtHdLineNumber.val();
            var strURLDestino = controls.txtHdUrlDestino.val();
            var altura = window.screen.height;
            var anchura = window.screen.width;
            var y = parseInt((window.screen.height / 2) - (altura / 2));
            var x = parseInt((window.screen.width / 2) - (anchura / 2));

            if (_MigrateStatus == 'NO') {
                window.open(strURLDestino.split("|")[0], '', 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',top=0,left=0,toolbar=no,location=no,status=no,menubar=no,directories=no');
            } else {

                if (_LineNumber.length != 0) {

                    $.window.open({
                        modal: false,
                        data: {
                            strLineNumber: _LineNumber,
                            strClientType: _ClientType,
                            strClient: _Client,
                            strMigrateStatus: _MigrateStatus
                        },
                        title: "SISTEC",
                        url: "~/Coliving/SearchCustomer/SIACU_ListTechnicalServiceOST",
                        width: 1000,
                        height: "auto"
                    });

                }
                else {
                    modalAlert('Estimado usuario, Seleccione una linea', 'Mensaje Transacciones');
                }

            }

        },
        btnPostSale_click: function () {
            var controls = this.getControls();
            //Redireccionando de acuerdo si la Linea Migró o NO
            var valueMigrate = controls.txtHdMigrateOne.val().toUpperCase();
            var valueAccount = controls.txtHdNumberAccount.val();
            var valueLine = controls.txtHdLineNumber.val();
            var strSi = "SI";
            var strNo = "NO";
            this.validatePrefixLineNumber(valueLine, function (r) {
                valueLine = r;
            });

            if (valueMigrate == strNo) {
                if (valueAccount != "") {
                    window.opener.$("#modalTypeSearch").modal('hide');
                    window.opener.$("#ul-button-Search").find("a[data-value=2]").trigger('click');
                    window.opener.$("#txtCriteriaValue").val(valueAccount);
                    window.opener.$("#btnCriteriaSearch").trigger('click');
                }
                else if (valueAccount == "" && valueLine != "") {
                    window.opener.$('#modalTypeSearch').modal('hide');
                    window.opener.$("#ul-button-Search").find("a[data-value='1']").trigger("click");
                    window.opener.$("#txtCriteriaValue").val(valueLine);
                    window.opener.$('#btnCriteriaSearch').click();
                }
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
                modalAlert('Estimado usuario, Seleccione una cuenta o una linea', 'Mensaje Transacciones');
            }

        },
        btnSpecialCases_click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.txtHdLineNumber.val().length > 0) {
                var _DocumentTypeDescription = controls.lblTypeDocDescription.text();
                var _LineNumber = controls.txtHdLineNumber.val();
                var _DocumentTypeCode = controls.txtHdTypeDocId.val();
                var _AccountNumber = controls.txtHdNumberAccount.val();
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
                return;
            }
        },
        rdbLinesNumberChange: function () {
            var that = this;
            var controls = that.getControls();

            var strPrepagoOlo = controls.txtHdstrPrepagoOlo.val();
            var migradoFullStack = controls.txtHdKeyMigradoFullStack.val();
            var strNoMigrado = migradoFullStack.split('|')[0].split(';')[1].toUpperCase();
            var strSiMigrado = migradoFullStack.split('|')[1].split(';')[1].toUpperCase();

            $('#TbServiciosPostBody').on("change", "input.tdRdBtn", function () {

                that.EnabledTechnicalService();

                $("#txtLineNumberListService").val('');
                $("#txtHdLineNumber").val('');
                $('#txtHdModality').val('');
                $('#txtHdAccountType').val('');
                $('#txtHdMigrateOne').val('');
                $('#txtHdNumberAccount').val('');

                $(this).parents("#TbServiciosPostBody").find("td.tdLineActive i.btnLinesActive", this).css("pointer-events", "none");
                $(this).parents("#TbServiciosPostBody").find("td.tdLineInactive i.btnLinesInactive", this).css("pointer-events", "none");

                $(this).parents("tr").find("td.tdLineActive i.btnLinesActive", this).css("pointer-events", "auto");
                $(this).parents("tr").find("td.tdLineInactive i.btnLinesInactive", this).css("pointer-events", "auto");

                var valortdAccount = $(this).parents("tr").find("td.tdAccountId").text();
                $('#txtHdNumberAccount').val(valortdAccount);

                var valortdAccountParent = $(this).parents("tr").find("td.tdAccountParent").text();
                $('#txtHdAccountType').val(valortdAccountParent);

                var valortdModality = $(this).parents("tr").find("td.tdModality").text();
                $('#txtHdModality').val(valortdModality);

                var valorTdMigrationOne = $(this).parents("tr").find("td.tdMigrateOne").text();
                $('#txtHdMigrateOne').val(valorTdMigrationOne);

                if (valorTdMigrationOne == 'SGA') {
                    that.DisabledSpecialCasesBtn();
                    return false
                }
                if (valortdModality == strPrepagoOlo && valorTdMigrationOne == strNoMigrado) {
                    that.LoadEnaDisSpecialCase();
                    return false
                }
                controls.txtHdValidateMigration.val() == strSiMigrado ? that.EnabledSpecialCasesBtn() : that.LoadEnaDisSpecialCase();
            });
        },
        rdbLinePreChange: function () {
            var that = this;
            var controls = that.getControls();

            var strSiMigrado = controls.txtHdKeyMigradoFullStack.val().split('|')[1].split(';')[1].toUpperCase();

            $('#TbServiciosPreBody').on("change", "input.tdRdBtn", function () {

                that.EnabledTechnicalService();

                $("#TbServiciosPostBody").find("td.tdLineActive i.btnLinesActive").css("pointer-events", "none");
                $("#TbServiciosPostBody").find("td.tdLineInactive i.btnLinesInactive").css("pointer-events", "none");

                $("#txtLineNumberListService").val('');
                $("#txtHdLineNumber").val('');
                $('#txtHdModality').val('');
                $('#txtHdAccountType').val('');
                $('#txtHdMigrateOne').val('');
                $('#txtHdNumberAccount').val('');

                var tdLineNumberPre = $(this).parents("tr").find("td.tdLineNumberPre").text();
                var tdMigrateOnePre = $(this).parents("tr").find("td.tdMigrateOnePre").text();
                $('#txtHdLineNumber').val(tdLineNumberPre);
                $('#txtHdMigrateOne').val(tdMigrateOnePre);
                $('#txtHdModality').val("PREPAGO");
                $('#txtHdAccountType').val(getValueConfig(['strCustomerType']).strCustomerType.split("|")[0].split(";")[0]);
                controls.txtHdValidateMigration.val() == strSiMigrado ? that.EnabledSpecialCasesBtn() : that.LoadEnaDisSpecialCase();
            });
        },
        validateStatusAccount: function () {
            var that = this;
            var controls = that.getControls();
            var strSiMigrado = controls.txtHdKeyMigradoFullStack.val().split('|')[1].split(';')[1].toUpperCase();
            $("#TbServiciosPostBody tr").each(function () {
                if ($("td.tdMigrateOne", this).text() == strSiMigrado) {
                    controls.txtHdValidateMigration.val(strSiMigrado);
                    return false;
                }
            });
        },
        addEventListServiceActive: function () {
            var that = this;
            var controls = that.getControls();
            var status = controls.txtHdStatus.val();
            var strActivo = status.split('|')[1].split(';')[1];

            $('#TbServiciosPostBody').on("click", "td i.btnLinesActive", function () {
                var tdAccountId = $(this).parents("tr").find("td.tdAccountId").text();
                $('#txtHdNumberAccount').val(tdAccountId);
                $('#txtHdActivaInactiva').val(strActivo);
                that.btnListService();
            });
        },
        addEventListServiceInactive: function () {
            var that = this;
            var controls = that.getControls();
            var status = controls.txtHdStatus.val();
            var strDesactivo = status.split('|')[3].split(';')[1];
            $('#TbServiciosPostBody').on("click", "td i.btnLinesInactive", function () {
                var tdAccountId = $(this).parents("tr").find("td.tdAccountId").text();
                $('#txtHdNumberAccount').val(tdAccountId);
                $('#txtHdActivaInactiva').val(strDesactivo);
                that.btnListService();
            });
        },
        btnListService: function () {
            $.window.open({
                modal: false,
                title: "Listado de Servicios",
                url: '~/Coliving/SearchCustomer/ListService',
                data: {
                },
                width: 550,
                height: 300
            });

        },
        DataCustomerDocument: function () {
            var that = this;
            var controls = that.getControls();
            var _DocumentType = window.opener.$('#cboDocTypeId').val();
            var _DocumentNumber = window.opener.$('#txtTypeDes').val().trim();

            var _SearchType = {
                DocumentType: _DocumentType,
                DocumentNumber: _DocumentNumber
            };

            $.app.ajax({
                modal: false,
                    type: "POST",
                    dataType: "json",
                    url: '~/Coliving/SearchCustomer/DataCustomerDocument',
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
                async: false,
                    data:
                      {
                          strIdSession: Session.IDSESSION,
                          objSearchType: _SearchType
                      },
                    success: function (result) {
                        console.log(result);
                        if (result.data != null) {
                            controls.lblTypeDocDescription.text(result.data.DocumentTypeDescription);
                            controls.txtHdTypeDocId.val(result.data.DocumentType);
                            controls.txtHdDocNumber.val(result.data.DocumentNumber);
                            controls.lblNumberDocument.text(result.data.DocumentNumber);
                            controls.lblNames.text(result.data.CustomerName);
                            controls.lblAddress.text(result.data.Address);

                            that.DataTablePost(result);
                            that.DataTablePre(result);
                            that.validateStatusAccount();
                        } else {
                            that.DisabledSpecialCasesBtn();
                            modalAlert('No se encontró información con los datos ingresados', 'Mensaje Transacciones', function () {
                                window.close();
                            });
                        }
                    },
                error: function (error) {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                    console.log(error);
                        that.DisabledSpecialCasesBtn();
                    }
                });
        },
        DataTablePost: function (result) {
            var _keyCantRows = parseInt(getValueConfig(['strQuantityRowsPostpago']).strQuantityRowsPostpago);
            var _cantData = parseInt(result.data.ListPostPaidService.length);
            var _validatePaginator = false;
            var _strFlagPaginator = getValueConfig(['strFlagPaginator']).strFlagPaginator;
            var boolValue = _strFlagPaginator.toLowerCase() == 'true' ? true : false;
            if (boolValue) {
                _validatePaginator = _cantData > _keyCantRows;
            }
            console.log(_cantData);
            console.log(_keyCantRows);
            $('#TbServiciosPost').find('tbody').html('');
            $('#TbServiciosPost').DataTable({
                "info": false,
                "scrollY": 300,
                "scrollCollapse": true,
                "paging": _validatePaginator,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": _keyCantRows,
                "searching": false,
                "destroy": false,
                "ordering": false,
                "select": 'single',
                "data": result.data.ListPostPaidService,
                "columns": [
                    { "data": "BillingAccountId" },
                    { "data": "MigrateOne" },
                    { "data": "ProductType" },
                    { "data": "CustomerType", },
                    {

                        "data": null,
                        "render": function (data) {
                            return data.CountActivateLine > 0 ?
                                '<div class="col-xs-12">' +
                                   '<i style="color: #337ab7;pointer-events:none" class="fa fa-900px fa-plus-circle btn btn-xs col-md-4 col-xs-3 btnLinesActive" data-toggle="modal"></i>' +
                                '<span class="col-md-8 col-xs-9">' + data.CountActivateLine + '</span>' +
                                '</div>' :
                                '<div class="col-xs-12">' +
                                   '<i class="col-md-4 col-xs-3"></i>' +
                                   '<span class="col-md-8 col-xs-9">' + data.CountActivateLine + '</span>' +
                              '</div>'
                        }
                    },
                    {
                        "data": null,
                        "render": function (data) {
                            return data.CountInactiveLine > 0 ?
                                '<div class="col-xs-12">' +
                                    '<i style="color: #337ab7;pointer-events:none" class="fa fa-900px fa-plus-circle btn btn-xs col-md-4 col-xs-3 btnLinesInactive" data-toggle="modal"></i>' +
                                    '<span class="col-md-8 col-xs-9">' + data.CountInactiveLine + '</span>' +
                                '</div>' :
                                '<div class="col-xs-12">' +
                                    '<i class="col-md-4 col-xs-3"></i>' +
                                    '<span class="col-md-8 col-xs-9">' + data.CountInactiveLine + '</span>' +
                                '</div>'
                        }
                    },
                    {
                        "data": null
                        , render: function () { return '<input class="tdRdBtn" name="rdbtnLine" type="radio"/>' }
                    },
                ],
                columnDefs: [
                    { "className": "tdAccountId", "targets": [0] },
                    { "className": "tdMigrateOne", "targets": [1] },
                    { "className": "tdModality", "targets": [2] },
                    { "className": "tdAccountParent", "targets": [3] },
                    { "className": "tdLineActive", "targets": [4] },
                    { "className": "tdLineInactive", "targets": [5] },
                    { "className": "tdRdbLine", "targets": [6] },
                ],
                language: {
                    lengthMenu: "",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)",
                    paginate:
                            {
                                previous: "<",
                                next: ">",
                                first: "|<",
                                last: ">|"
                            }
                }

            });
            if (_validatePaginator) {
                $("#TbServiciosPost_wrapper").find(".row:eq(2)").find("div").first().append(this.ShowRowsTotal(_cantData));
            }
        },
        ShowRowsTotal: function (_cant) {
            var _element = '<div class="col-xs-12" style="height: 30px;display: flex;align-items: center;">';
            _element += '<span style="color: #337ab7">';
            _element += 'Total de Registros: ' + _cant;
            _element += '</span><div></div></div>';
            return _element;
        },
        DataTablePre: function (result) {
            var _keyCantRows = parseInt(getValueConfig(['strQuantityRowsPrepago']).strQuantityRowsPrepago);
            var _cantData = parseInt(result.data.ListPrePaidService.length);
            var _validatePaginator = false;
            var _strFlagPaginator = getValueConfig(['strFlagPaginator']).strFlagPaginator;
            var boolValue = _strFlagPaginator.toLowerCase() == 'true' ? true : false;
            if (boolValue) {
                _validatePaginator = _cantData > _keyCantRows;
            }
            console.log(result.data.ListPrePaidService.length);
            console.log(_keyCantRows);
            $('#TbServiciosPre').find('tbody').html('');
            $('#TbServiciosPre').DataTable({
                "info": false,
                "scrollY": 300,
                "scrollCollapse": true,
                "paging": _validatePaginator,
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": _keyCantRows,
                "searching": false,
                "destroy": false,
                "ordering": false,
                "select": 'single',
                "data": result.data.ListPrePaidService,
                "columns": [
                    { "data": "LineNumber" },
                    { "data": "MigrateOne" },
                    { "data": "LineStatus" },
                    { "data": null, render: function () { return '<input class="tdRdBtn" name="rdbtnLine" type="radio"/>' } },
                ],
                columnDefs: [
                    { "className": "tdLineNumberPre", "targets": [0] },
                    { "className": "tdMigrateOnePre", "targets": [1] },
                ],
                language: {
                    lengthMenu: "",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)",
                    paginate:
                            {
                                previous: "<",
                                next: ">",
                                first: "|<",
                                last: ">|"
                            }

                }
            });
            if (_validatePaginator) {
                $("#TbServiciosPre_wrapper").find(".row:eq(2)").find("div").first().append(this.ShowRowsTotal(_cantData));
            }
        },
        LoadEnaDisSpecialCase: function () {
            var that = this,
               controls = that.getControls();
            controls.btnSale.prop("disabled", false);
            controls.btnPostSale.prop("disabled", false);
            controls.btnSpecialCases.prop("disabled", true);
        },
        EnabledTechnicalService: function () {
            var that = this,
                controls = that.getControls();
            controls.btnTechnicalService.prop("disabled", false);
        },
        DisabledTechnicalService: function () {
            var that = this,
                controls = that.getControls();
            controls.btnTechnicalService.prop("disabled", true);
        },
        EnabledSpecialCasesBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSale.prop("disabled", false);
            controls.btnPostSale.prop("disabled", false);
            controls.btnSpecialCases.prop("disabled", false);
        },

        DisabledSpecialCasesBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSale.prop("disabled", true);
            controls.btnPostSale.prop("disabled", true);
            controls.btnSpecialCases.prop("disabled", true);
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
                window.resizeTo(1250, (heightWindow + 65));
                window.moveTo(screen.width / 2 - 625, (screen.height / 2) - ((heightWindow + 65) / 2));
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
    $.fn.FormCustomerDocument = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormCustomerDocument'),
                options = $.extend({}, $.fn.FormCustomerDocument.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormCustomerDocument', data);
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
    $.fn.FormCustomerDocument.defaults = {
    }
    $('#divSearchCustomerDocument').FormCustomerDocument();
})(jQuery);
