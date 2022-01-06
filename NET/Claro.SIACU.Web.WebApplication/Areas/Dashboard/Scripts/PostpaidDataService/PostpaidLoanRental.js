(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidLoanRental.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , NombreBusq: $('#NombreBusq', $element)
            , tblLoanRental: $('#tblLoanRental', $element)
            , txtDateStart: $('#txtDateStart', $element)
            , txtDateEnd: $('#txtDateEnd', $element)
            , btnSearchContract: $('#btnSearchContract', $element)
            , BtnExport: $('#BtnExport', $element)
            , cboLoanRentalType: $('#cboLoanRentalType', $element)
            , lblZonaAlmacen: $('#lblZonaAlmacen', $element)
            , LoanRental: $('#LoanRental', $element)
            , cbowarehouseAreaType: $('#cbowarehouseAreaType', $element)
            , btnSearchStock: $('#btnSearchStock', $element)
            , warehouseArea: $('#warehouseArea', $element)
            , objMaterial: $('#txtmaterialcode', $element)
            , objDescripcion: $('#txtDescription', $element)
            , containerDateRange: $('#containerDateRange', $element)
            , btnExaminar: $("#btnExaminar", $element)

        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {

            var that = this,
                controls = this.getControls();

            controls.btnSearchContract.addEvent(that, 'click', that.btnSearchContract_click);
            controls.BtnExport.addEvent(that, 'click', that.BtnExport_click);
            controls.btnSearchStock.addEvent(that, 'click', that.btnSearchStock_click);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });
            controls.cboLoanRentalType.addEvent(that, 'change', that.cboLoanRentalType_change);
            controls.cbowarehouseAreaType.addEvent(that, 'change', that.cbowarehouseAreaType_change);
            controls.btnExaminar.addEvent(that, 'click', that.btnExaminar_click);
            that.render();
        },
        render: function () {
            var that = this;
            that.getLoanRentalSelecType();
            that.getCombowarehouseAreaSelecType();
            that.cbowarehouseAreaType_change();
        },
        cbowarehouseAreaType_change: function () {
            var controls = this.getControls();
            var value=controls.cbowarehouseAreaType.val();
            
            if(typeof(value)=='undefined' || value==null || value.trim().length==0){
                controls.btnSearchStock.attr('disabled','disabled');
                controls.btnExaminar.attr('disabled', 'disabled');
               
            }else{
                controls.btnSearchStock.removeAttr('disabled');
                controls.btnExaminar.removeAttr('disabled');
            }
          
        },
        btnSearchStock_click: function () {
            var controls = this.getControls();
            var objMaterial = controls.objMaterial.val();
            var objDescripcion = controls.objDescripcion.val();

            if (objMaterial == "" && objDescripcion == "") {
                showAlert("Ingrese el Código ó la Descripción del Material a buscar", "ERROR");
                return false;
            }

            if (objMaterial != "") {
                $("#txtmaterialcode").val('');
                this.SearchStock(1);
            }
            else {
                this.SearchStock(2);
            }
        },
        SearchStock: function (type) {
            var that = this,
                controls = this.getControls();
            var objMaterial = controls.objMaterial.val();
            var objDescripcion = controls.objDescripcion.val();
            var cbowarehouseAreaType = controls.cbowarehouseAreaType.val();

            var stUrlLogo =  "/Images/loading2.gif";
            controls.warehouseArea.find('tbody').html('<tr><td colspan="4" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            var data = "";

            if (type == 1) {
                data = { strIdSession: Session.IDSESSION, strSeries: "%%", strDescription: objMaterial, cbowarehouseAreaType: cbowarehouseAreaType }
            }
            else if (type == 2) {
                data = { strIdSession: Session.IDSESSION, strSeries: "%" & objDescripcion & "%", strDescription: "", cbowarehouseAreaType: cbowarehouseAreaType }
            }

            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: '~Dashboard/PostpaidDataService/SearchStock',
                data: data,
                complete: function () { controls.cbowarehouseAreaType.val(null); that.cbowarehouseAreaType_change(); },
                success: function (result) {
                    controls.warehouseArea.DataTable({
                        "scrollY": "180px",
                        "scrollCollapse": true,
                        "paging": false,
                        "destroy": true,
                        "searching": false,
                        "select": "single",
                        "data": result.data.listStockWarehouse,
                        "language": {
                            "lengthMenu": "Display _MENU_ records per page",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)"
                        },
                        "columns": [
                              { "data": "Codigo" },
                              { "data": "Descripcion" },
                              { "data": "Warehouse" },
                              { "data": "quantity" }
                        ]
                    });
                },
                error: function (msger) {
                    controls.warehouseArea.find('tbody').html('');
                    $.app.error({
                        id: 'errorWarehouseArea',
                        message: msger,
                        click: function () {
                            that.SearchStock(type);
                        }
                    });
                }
            });
        },
        SearchContract: function (send) {
            var that = this,
             controls = this.getControls();
            var strUrl;
            var valTypeNroDocumento = Session.DATACUSTOMER.DNIRUC;
            var valTypeStarDate = controls.txtDateStart.val();
            var valTypeEndDate = controls.txtDateEnd.val();
            var valTypeNumber = controls.NombreBusq.val();
            var valTypeModel = controls.cboLoanRentalType.val();
            var months = 0;

            if (valTypeStarDate != "" && valTypeEndDate != "") {

                var star = valTypeStarDate.split('/');
                var end = valTypeEndDate.split('/');
                var fecha1 = star[2] + star[1] + star[0];
                var fecha2 = end[2] + end[1] + end[0];

                valTypeStarDate = fecha1;
                valTypeEndDate = fecha2;

                if (end[2] - star[2] > 0) {
                    months = (12 - parseInt(star[1])) + parseInt(end[1]);
                }
                else {
                    months = parseInt(end[1]) - parseInt(star[1]);
                }
            } else {
                valTypeStarDate = "";
                valTypeEndDate = "";
            }

            if (months > 5) {
                showAlert("El rango de fechas inicial y final no debe de ser mayor a 5 meses");
                return false;
            }
            else {
                var data = { strIdSession: Session.IDSESSION, strStarDate: valTypeStarDate, strEndDate: valTypeEndDate, strNumber: valTypeNumber, strModel: valTypeModel, StrNroDocumento: valTypeNroDocumento };
                strUrl = '~/Dashboard/PostpaidDataService/SearchLoanRental';

            }
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblLoanRental.find('tbody').html('<tr><td colspan="12" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: strUrl,
                data: data,
                success: function (result) {
                    controls.tblLoanRental.DataTable({
                        "scrollY": "180px",
                        "scrollCollapse": true,
                        "paging": false,
                        "destroy": true,
                        "select": "single",
                        "searching": false,
                        "data": result.data,
                        "language": {
                            "lengthMenu": "Dispay _MENU_ records per page",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)"
                        },
                        "columns": [
                         { "data": "Businessname" },
                         { "data": "RUC" },
                         { "data": "address" },
                         { "data": "Model" },
                         { "data": "IMEI" },
                         { "data": "reasonSAP" },
                         { "data": "modalitySAP" },
                         { "data": "Date" },
                         { "data": "NroClarify" },
                         { "data": "NroOrder" },
                         { "data": "Denomination" }
                        ]
                    });
                },
                error: function (msger) {
                    controls.tblLoanRental.find('tbody').html('');
                    $.app.error({
                        id: 'errorLoanRental',
                        message: msger,
                        click: function () {
                            that.SearchContract(send);
                        }
                    });
                }
            });
        },
        Exportar: function (send) {
            var controls = this.getControls();
            var strUrl;
            var valTypeNroDocumento = Session.DATACUSTOMER.DNIRUC;
            var valTypeStarDate = controls.txtDateStart.val();
            var valTypeEndDate = controls.txtDateEnd.val();
            var valTypeNumber = controls.NombreBusq.val();
            var valTypeModel = controls.cboLoanRentalType.val();
            var star = valTypeStarDate.split('/');
            var end = valTypeEndDate.split('/');
            var months;
            if (end[2] - star[2] > 0) {
                months = (12 - parseInt(star[1])) + parseInt(end[1]);
            }
            else {
                months = parseInt(end[1]) - parseInt(star[1]);
            }

            if (valTypeModel == "") {
                showAlert("Seleccione el tipo de busqueda");
                return false;
            }
            if (valTypeStarDate > valTypeEndDate) {
                showAlert("La fecha inicial no debe ser mayor a la fecha final");
                return false;
            } else if (months > 5) {
                showAlert("El rango de fechas inicial y final no debe de ser mayor a 5 meses");
                return false;
            }
            else {
                var data = { strIdSession: Session.IDSESSION, strStarDate: valTypeStarDate, strEndDate: valTypeEndDate, strNumber: valTypeNumber, strModel: valTypeModel, StrNroDocumento: valTypeNroDocumento };
                strUrl = '~/Dashboard/PostpaidDataService/ExportLoanRental';

            }
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrl,
                data: JSON.stringify(data),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=PrestamoAlquiler.xlsx";
                }
            });
        },
        btnSearchContract_click: function (send) {
            this.SearchContract(send);
        },
        BtnExport_click: function (send) {

            this.Exportar(send);
        },
        createComboLoanRentalType: function (response) {
            var that = this,
                controls = that.getControls();
            $.each(response.data, function (index, value) {
                controls.cboLoanRentalType.append($('<option>', { value: value.Code, html: value.Description }));
            });
        },
        cboLoanRentalType_change: function (sender, args) {
            var that = this,
                controls = that.getControls();
            controls.NombreBusq.val("");
        },
        getLoanRentalSelecType: function () {
            var that = this;

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetLoanRentalType',
                data: { strIdSession: Session.IDSESSION },
                success: function (response) {
                    that.createComboLoanRentalType(response);
                },
                error: function (ex) {
                    showAlert('Vuelva a intentarlo mas tarde...', 'Préstamo/Alquiler');
                }
            });
        },
        getCombowarehouseAreaSelecType: function () {
            var that = this;
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetLoanRentalListwarehouseAreaType',
                data: { strIdSession: Session.IDSESSION },
                success: function (response) {
                    that.createCombowarehouseAreaType(response);
                },
                error: function (ex) {
                    showAlert('vuelva a intentarlo mas tarde...', 'Préstamo/Alquiler');
                }
            });
        },
        createCombowarehouseAreaType: function (response) {
            var that = this,
                controls = that.getControls();

            controls.cbowarehouseAreaType.append($('<option>', { value: '', html: 'Seleccionar' }));

            $.each(response.data, function (index, value) {
                controls.cbowarehouseAreaType.append($('<option>', { value: value.Code, html: value.Description }));
            });
        },
        btnExaminar_click: function () {
            $.window.open({
                modal: true,
                title: "Códigos de Material",
                url: '~/Dashboard/PostpaidDataService/ExamineStock',
                data: {},
                width: 1024,
                height: 450,
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
        strUrl: window.location.href
    }

    $.fn.PostPaidLoanRental = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidLoanRental'),
                options = $.extend({}, $.fn.PostPaidLoanRental.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidLoanRental', data);
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

    $.fn.PostPaidLoanRental.defaults = {
    }
    $('#LoanRental', $('.modal:last')).PostPaidLoanRental();

})(jQuery);