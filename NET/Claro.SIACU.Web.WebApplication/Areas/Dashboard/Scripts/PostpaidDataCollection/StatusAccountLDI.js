(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidStatusAccountLDI.defaults, $element.data(), typeof options === 'object' && options);

        this.SetControls({
            form: $element
            , tblBilling: $('#tblBilling', $element)
            , tblPayment: $('#tblPayment', $element)
            , tvFacturas: $('#tvFacturas', $element)
            , tvPagos: $('#tvPagos', $element)
            , btnExportBill: $('#btnExportBill', $element)
            , btnExportPayment: $('#btnExportPayment', $element)
        });

    };

    Form.prototype = {
        Constructor: Form,
        Init: function () {
            var that = this,
            controls = that.GetControls();
            controls.btnExportBill.addEvent(that, 'click', that.btnShowExportBill);
            controls.btnExportPayment.addEvent(that, 'click', that.btnShowExportPayment);
            this.Render();

        },
        Render: function () {
            this.InitializeTables();
            this.ShowMessageEmptyTables();

        },
        btnShowExportBill: function(){
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oContract.strNameClient = Session.DATACUSTOMER.BusinessName;
            oContract.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oContract.strContactId = Session.DATACUSTOMER.Account;
            oContract.strType = "B";

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataCollection/ExportStatusAccountLDI',
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=StatusAccountLDI.xlsx";
                }
            });
        },
        btnShowExportPayment: function () {
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oContract.strNameClient = Session.DATACUSTOMER.BusinessName;
            oContract.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oContract.strContactId = Session.DATACUSTOMER.Account;
            oContract.strType = "P";

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataCollection/ExportStatusAccountLDI',
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=StatusAccountLDI.xlsx";
                }
            });
        },
        InitializeTables: function () {
            this.InitializeBillingTable();
            this.InitializePaymentTable();
        },
        ShowMessageEmptyTables: function () {
            if (this.GetControls().tblBilling.DataTable().rows().count() === 0 && this.GetControls().tblPayment.DataTable().rows().count() === 0) {
                showAlert('No existen registros de facturas y pagos con este Nº de Cuenta.','Alerta');
            } else if (this.GetControls().tblBilling.DataTable().rows().count() === 0) {
                showAlert('No existen registros de facturas con este Nº de Cuenta.','Alerta');
            } else if (this.GetControls().tblPayment.DataTable().rows().count() === 0) {
                showAlert('No existen registros de pagos con este Nº de Cuenta.','Alerta');
            }
        },
        InitializeBillingTable: function () {
            var that = this,
                controls = that.GetControls();

            controls.tblBilling.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true
,                columnDefs: [{
                    targets: 4,
                    render: function (data, type, full, meta) {
                        switch (full[3].substring(0, 2)) {
                            case "85":
                                return "Americatel Peru S.A.";
                            case "40":
                                return "IDT";
                            case "60":
                                return "Convergia";
                            default:
                                return "-";
                        }

                    }
                }],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
            that.TotalCurrentBilling();
        },

        GetExportStusAccountLDI: function () {

            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oContract.strNameClient = Session.DATACUSTOMER.BusinessName;
            oContract.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oContract.strContactId = Session.DATACUSTOMER.Account;


            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataCollection/ExportStatusAccountLDI',
                data: JSON.stringify(oContract),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=StatusAccountLDI.xlsx";
                }
            });
        },

        InitializePaymentTable: function () {
            var that = this,
                controls = that.GetControls();

            controls.tblPayment.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true,
                columnDefs: [{
                    targets: 5,
                    render: function (data, type, full, meta) {
                        switch (full[4].substring(0, 2)) {
                            case "85":
                                return "Americatel Peru S.A.";
                            case "40":
                                return "IDT";
                            case "60":
                                return "Convergia";
                            default:
                                return "-";
                        }
                    }
                },
                {
                    targets: 9,
                    render: function (data, type, full, meta) {
                        return data * -1;
                    }
                }
                ],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
            that.TotalCurrentPayment();
        },
        TotalCurrentBilling: function () {
            var controls = this.GetControls();
            controls.tvFacturas.empty().append('<b>Total Vigente:</b> ' + controls.tblBilling.DataTable().column(10).data().sum());
        },
        TotalCurrentPayment: function () {
            var controls = this.GetControls();
            controls.tvPagos.empty().append('<b>Total Vigente:</b> ' + controls.tblPayment.DataTable().column(9).data().sum() * -1);
        },
        GetControls: function () {
            return this.m_controls || {};
        },
        SetControls: function (value) {
            this.m_controls = value;
        }
    };
    $.fn.FormPostpaidStatusAccountLDI = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportStusAccountLDI'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidStatusAccountLDI'),
                options = $.extend({}, $.fn.FormPostpaidStatusAccountLDI.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidStatusAccountLDI', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.Init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.FormPostpaidStatusAccountLDI.defaults = {};
    $('#PostpaidStatusAccountLDI', $('.modal:last')).FormPostpaidStatusAccountLDI();
})(jQuery);

