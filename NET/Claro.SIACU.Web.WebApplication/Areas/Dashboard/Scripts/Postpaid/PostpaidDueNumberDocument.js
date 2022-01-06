(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidDueDocumentNumber.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , chkCustomSearch: $('#chkCustomSearch', $element)
            , divTypeDocument: $('#divTypeDocument', $element)
            , txtNumberDocument: $('#txtNumberDocument', $element)
            , btnSearch: $('#btnSearch', $element)
            , divNameClient: $('#divNameClient', $element)
            , divNumberDocument: $('#divNumberDocument', $element)
            , divMobileDue: $('#divMobileDue', $element)
            , divFixedDue: $('#divFixedDue', $element)
            , divOverdueMobileDue: $('#divOverdueMobileDue', $element)
            , divOverdueFixedDue: $('#divOverdueFixedDue', $element)
            , divPunishedMobileDue: $('#divPunishedMobileDue', $element)
            , divPunishedFixedDue: $('#divPunishedFixedDue', $element)
            , divAntiquityDue: $('#divAntiquityDue', $element)
            , divAllServices: $('#divAllServices', $element)
            , tblMobileAccounts: $('#tblMobileAccounts', $element)
            , tblFixedAccounts: $('#tblFixedAccounts', $element)
            , errorMobileAccounts: $('#errorMobileAccounts', $element)
            , errorFixedAccounts: $('#errorFixedAccounts', $element)
            , btnExportAcount: $('#btnExportAcount')

        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();

            controls.btnSearch.addEvent(that, 'click', that.btnSearch_click);
            controls.chkCustomSearch.addEvent(that, 'change', that.chkCustomSearch_change);
            if (Session.ORIGINTYPE === 'HFC') { controls.chkCustomSearch.parent().hide(); } else { controls.chkCustomSearch.parent().show(); }
            if (Session.ORIGINTYPE === 'POSTPAID') { controls.btnExportAcount.show(); } else { controls.btnExportAcount.hide(); }
            that.render();

        },
        render: function () {
            var that = this, controls = that.getControls();
            controls.btnSearch.prop("style").display = "none";            
            that.getDocumentType();
            that.setNumberDocument();
            that.getParamsAccounts();
            that.getCaptions();
        },
        setNumberDocument: function () {
            this.getControls().txtNumberDocument.val(Session.DATACUSTOMER.DNIRUC);
        },
        getDocumentType: function () {
            var that = this,
                objDocumentType = {};

            objDocumentType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetDocumentType',
                data: JSON.stringify(objDocumentType),
                success: function (response) {
                    that.createComboDocumentType(response);
                }
            });
        },
        createComboDocumentType: function (response) {
            var that = this,
                controls = that.getControls(),
                sel = $('<select>', {
                    id: 'cboDocumentType',
                    name: 'cboDocumentType',
                    class: 'form-control input-sm',
                    disabled: "disabled"
                }, controls.form);

            $.extend(controls, { cboDocumentType: sel });

            if (response.data.listDocumentType == null)
                sel.append($('<option>', { value: '-1', html: 'Sin registros' }));
            else {
                $.each(response.data.listDocumentType, function (index, value) {
                    sel.append($('<option>', {
                        value: value.Code,
                        html: value.Description,
                    }));
                });

                if (Session.DATACUSTOMER.DNIRUC.length == 8)
                { sel.val('1') }
                if (Session.DATACUSTOMER.DNIRUC.length == 11)
                { sel.val('6') }
            }
            controls.divTypeDocument.append(sel);
        },
        getTypeDocument: function () {       
            switch (Session.DATACUSTOMER.DNIRUC.length) {             
                case 8:
                    return 1;
                case 11:
                    return 6;
            }         
        },
        getAccounts: function (params) {
            var controls = this.getControls();
            controls.errorMobileAccounts.html('');
            controls.errorFixedAccounts.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblMobileAccounts.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            controls.tblFixedAccounts.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            var that = this;
           
            var di = params.DocumentIdentity;
            var nu = params.NumberDocument;
            var is = params.strIdSession;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: '~/Dashboard/PostpaidDataCollection/ListsDueNumberDocument',
                data: JSON.stringify({ strDocumentIdentity: di, strNumberDocument: nu, strIdSession: is }),

                success: function (response) {
                    controls.tblMobileAccounts.find('tbody').html('');
                    controls.tblFixedAccounts.find('tbody').html('');
                    that.setDataAccounts(response);
                },
                error: function (msger) {
                    controls.tblMobileAccounts.find('tbody').html('');
                    controls.tblFixedAccounts.find('tbody').html('');
                    $.app.error({
                        id: 'errorMobileAccounts',
                        message: msger,
                        click: function () {
                            that.getCuentas();
                        }
                    });
                    $.app.error({
                        id: 'errorFixedAccounts',
                        message: msger,
                        click: function () {
                            that.getCuentas();
                        }
                    });
                }
            });
        },
        getParamsAccounts: function () {
            var that = this,
                oCuentas = {};

            oCuentas.DocumentIdentity = that.getTypeDocument();
            oCuentas.NumberDocument = Session.DATACUSTOMER.DNIRUC;
            oCuentas.strIdSession = Session.IDSESSION;

            that.getAccounts(oCuentas);

        },
        setDataAccounts: function (response) {
           
            if (response.data != null) {
               
                if (response.data.ObjCollectionDueDocumentNumberModel != null) {
                    this.setHeaderAccounts(response.data.ObjCollectionDueDocumentNumberModel);
                }
                if (response.data.ListCollectionDueDocumentNumberModelMovil != null) {
                   this.setListMobileAccount(response.data.ListCollectionDueDocumentNumberModelMovil);
                }
                if (response.data.ListCollectionDueDocumentNumberModelFija != null) {
                    this.setListFixedAccount(response.data.ListCollectionDueDocumentNumberModelFija);
                }
            }


        },
        getCaptions: function (){
            var that = this, 
                controls = that.getControls();

            var obj = {
                strIdSession: Session.IDSESSION,
                objGetDescriptionsRequest:
                {
                    tipoProceso: "LEYENDA_CONSULTA_DEUDA"
                }
            };



            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PostpaidDataCollection/GetDescriptions',
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var Leyenda = response.data.datosParametroClarify

                    if (Leyenda.length > 0)
                    {
                        $('#tblCaption tbody tr').remove();

                        Leyenda.forEach(function (i) {

                            console.log(i);
                            var cad = '';

                            cad += '<tr>';
                            cad += '    <td class="text-primary" style="width: 40%; color: black; font-weight: bold;">' + i.datoEvalua + '</td>';
                            cad += '    <td class="text-primary" style="width: 5%;; color: black; font-weight: bold;">:</td>';
                            cad += '    <td class="text-primary" style="color: black;"> <span>' + i.parametro1 + '</span></td>';
                            cad += '</tr>';

                            $('#tblCaption tbody').append(cad);
   
                        });
                    }
                },
                error: function (response) {
                    var cad = '';
                    cad += '<tr>';
                    cad += '    <td class="text-primary text-center" style="width: 100%; color: black; font-weight: bold;">Error al obtener datos...</td>';
                    cad += '</tr>';

                    $('#tblCaption tbody').append(cad);
                },
            });
        },

        setHeaderAccounts: function (data) {
            var that = this,
                controls = that.getControls();

            that.ClearControls();

            controls.divNameClient.append(" :   "+data.NameClient);
            controls.divNumberDocument.append(data.NumberDocument == null ? " :   " + controls.txtNumberDocument.val() : " :   " + data.NumberDocument);
            controls.divMobileDue.append(" :   " + data.MobileDue);
            controls.divFixedDue.append(" :   " + data.FixedDue);
            controls.divOverdueMobileDue.append(" :   " + data.OverdueMobileDue);
            controls.divOverdueFixedDue.append(" :   " + data.OverdueFixedDue);
            controls.divPunishedMobileDue.append(" :   " + data.PunishedMobileDue);
            controls.divPunishedFixedDue.append(" :   " + data.PunishedFixedDue);
            controls.divAntiquityDue.append(" :   " + Math.round(data.AntiquityDue) + ' Día(s)');
            controls.divAllServices.append(" :   " + (data.AllServices));
        },
        setListMobileAccount: function (data) {
            this.getControls().tblMobileAccounts.DataTable({
                info: false
                , paging: false
                , select: 'single'
                , searching: false
                , destroy: true
                , scrollX: true
                , sScrollXInner: "100%"
                ,autoWidth: true
                , data: data
                , columns: [
                    { "data": null },
                    { "data": "IdAccount" },
                    { "data": "StatusAccount" },
                    { "data": "AverageInvoiced" },
                    { "data": "CurrentDue" },
                    { "data": "ExpiredDue" },
                    { "data": "PunishedDue" },
                    { "data": "AccountServices" },
                    { "data": "RiskCenter" }
                ]
                , rowCallback: function (row, data, iDisplayIndex) {
                    var index = iDisplayIndex + 1;
                    $('td:eq(0)', row).html(index);
                }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
     
        
        GetExportDueNumber: function () {
            var that = this,
             controls = that.getControls();
            var strUrlModal = '~/Dashboard/PostpaidDataCollection/PostReportDueNumberDocument';
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var document = controls.txtNumberDocument.val();
            var oDueNumber = {};
            oDueNumber.strDocumentIdentity = that.getTypeDocument();
            oDueNumber.strNumberDocument = document;
            oDueNumber.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(oDueNumber),
                success: function (path) {

                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=DeudaPorNroDocumento.xlsx";
                }
            });
        },

        setListFixedAccount: function (data) {
            this.getControls().tblFixedAccounts.DataTable({
                info: false
                , paging: false
                , select: 'single'
                , searching: false
                , destroy: true
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: data
                , columns: [
                    { "data": null },
                    { "data": "IdAccount" },
                    { "data": "StatusAccount" },
                    { "data": "AverageInvoiced" },
                    { "data": "CurrentDue" },
                    { "data": "ExpiredDue" },
                    { "data": "PunishedDue" },
                    { "data": "AccountServices" },
                    { "data": "RiskCenter" }
                ]
                , rowCallback: function (row, data, iDisplayIndex) {
                    var index = iDisplayIndex + 1;
                    $('td:eq(0)', row).html(index);
                }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        btnSearch_click: function () {
            var that = this,
                controls = that.getControls(),
                oCuentas = {};

            if (controls.txtNumberDocument.val() === "") {
                
                showAlert('Debe ingresar un número de documento.', 'Alerta');
                return;
            }
            oCuentas.DocumentIdentity = controls.cboDocumentType.val();
            oCuentas.NumberDocument = controls.txtNumberDocument.val();
            oCuentas.strIdSession = Session.IDSESSION;

            that.getAccounts(oCuentas);
        },
        ClearControls:function()
        {
            var that = this, controls = that.getControls();
            controls.divNameClient.html('').html(" <label class=\"control-label\">Nombres Cliente</label> ");
            controls.divNumberDocument.html('').html(" <label class=\"control-label\">Documento Asociado</label> ");
            controls.divMobileDue.html('').html(" <label class=\"control-label\">Total Deuda Móvil</label> ");
            controls.divFixedDue.html('').html(" <label class=\"control-label\">Total Deuda Fija</label> ");
            controls.divOverdueMobileDue.html('').html(" <label class=\"control-label\">Total Deuda Vencida Móvil</label> ");
            controls.divOverdueFixedDue.html('').html(" <label class=\"control-label\">Total Deuda Vencida Fija</label> ");
            controls.divPunishedMobileDue.html('').html(" <label class=\"control-label\">Total Deuda Castigada Móvil</label> ");
            controls.divPunishedFixedDue.html('').html(" <label class=\"control-label\">Total Deuda Castigada Fija</label> ");
            controls.divAntiquityDue.html('').html(" <label class=\"control-label\">Antiguedad Deuda</label> ");
            controls.divAllServices.html('').html(" <label class=\"control-label\">Total Servicios</label> ");
        },

        chkCustomSearch_change: function (sender, arg) {
            var that = this,
                controls = that.getControls();

            if (sender.prop("checked")) {
                controls.cboDocumentType.prop("disabled", false);
                controls.txtNumberDocument.prop("disabled", false);
                controls.btnSearch.prop("style").display = "block";
                controls.btnSearch.prop("disabled", false);
            } else {
                controls.cboDocumentType.prop("disabled", true);
                controls.txtNumberDocument.prop("disabled", true);
                controls.btnSearch.prop("style").display = "none";
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta Deuda Número Documento",
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };
    $.fn.FormPostpaidDueDocumentNumber = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportDueNumber'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidDueDocumentNumber'),
                options = $.extend({}, $.fn.FormPostpaidDueDocumentNumber.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidDueDocumentNumber', data);
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

    $.fn.FormPostpaidDueDocumentNumber.defaults = {
    }
    $('#PostpaidDueDocumentNumber').FormPostpaidDueDocumentNumber();

})(jQuery);

