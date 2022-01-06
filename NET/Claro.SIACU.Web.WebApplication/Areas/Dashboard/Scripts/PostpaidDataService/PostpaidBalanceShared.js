(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidBalanceShared.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , btnPost_BalanceSharedExport: $('#btnPost_BalanceSharedExport', $element)
            , lblMensaje: $('#lblMensaje', $element)
            , lblBalanceSharedTotalRows: $('#lblBalanceSharedTotalRows', $element)
            , tblBalanceSharedLine: $('#tblBalanceSharedLine', $element)
            , tblBalanceSharedBag: $('#tblBalanceSharedBag', $element)
            , lblBalanceSharedTelephone: $('#lblBalanceSharedTelephone', $element)
            , lblBalanceSharedAccount: $('#lblBalanceSharedAccount', $element)

        });


    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = that.getControls();
            controls.btnPost_BalanceSharedExport.addEvent(that, 'click', that.btnPost_BalanceSharedExport_click);
            console.log("qqqq");
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
            controls.lblMensaje.hide();
            that.getBalanceShared();
        },
        btnPost_BalanceSharedExport_click: function () {

            var that = this,
               controls = that.getControls(),
               objDataService = Session.DATASERVICE,
               objCustomer = Session.DATACUSTOMER,
               objBalanceShared = {};
            objBalanceShared.strIdSession = Session.IDSESSION;
            objBalanceShared.Telephone = objDataService.CellPhone;
            objBalanceShared.CustomerId = objCustomer.CustomerID;
            objBalanceShared.Account = Session.DATACUSTOMER.Account;
            objBalanceShared.TypeCustomer = Session.DATACUSTOMER.CustomerType;

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: "~/Dashboard/PostpaidDataService/PostReportBalanceShared",
                data: JSON.stringify(objBalanceShared),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }) + '?strPath=' + path + "&strNewfileName=ReporteDeBolsaCompartida.xlsx";;
                }
            });

        },
        getBalanceShared: function () {

            var that = this,
                controls = that.getControls(),
                objDataService = Session.DATASERVICE,
                objCustomer = Session.DATACUSTOMER,
                objBalanceShared = {};
            objBalanceShared.strIdSession = Session.IDSESSION;
            objBalanceShared.Telephone = objDataService.CellPhone;
            objBalanceShared.CustomerId = objCustomer.CustomerID;
            objBalanceShared.Account = Session.DATACUSTOMER.Account;
            objBalanceShared.TypeCustomer = Session.DATACUSTOMER.CustomerType;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonBalanceShared',
                data: JSON.stringify(objBalanceShared),
                success: function (response) {

                    console.log(response);
                    if (response.data != null) {
                        console.log(1);
                        if (response.data.MessageTypeCustomer != null) {

                            showAlert(response.data.MessageTypeCustomer, "Mensaje Bolsa Compartida", function () { window.parent.close(); });

                        } else {
                            controls.lblBalanceSharedTelephone.after(response.data.Line);
                            controls.lblBalanceSharedAccount.after(response.data.Account);
                            that.setTableBalanceSharedLine(response.data.lstSharedBagLine);
                            that.setTableBalanceSharedBag(response.data.lstSharedBag);
                            controls.lblBalanceSharedTotalRows.after((response.data.CountSharedBag != null) ? response.data.CountSharedBag : "0");
                            if (response.data.Error != null) {
                                controls.lblMensaje.show();
                                controls.lblMensaje.after("<span  class=\"label-red-service\">" + response.data.Error + "</span>");
                            } else {
                                controls.lblMensaje.hide();
                            }
                        }

                    } else {
                        console.log(2);
                        that.setTableBalanceSharedLine(null);
                        that.setTableBalanceSharedBag(null);
                        controls.lblBalanceSharedTotalRows.after("0");
                    }

                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerBalanceShared',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });

        },
        setTableBalanceSharedLine: function (data) {
            var controls = this.getControls(),
                tbl = controls.tblBalanceSharedLine;


            tbl.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: data
                , select: 'single'
                , columns: [

                    { "data": "Line" },
                    { "data": "Bag" },
                    { "data": "Unit" },
                    { "data": "Stopper" },
                    { "data": "Consumption" },
                    { "data": "Balance" },
                    { "data": "DateValidity" }


                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    }
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
        setTableBalanceSharedBag: function (data) {
            var controls = this.getControls(),
                tbl = controls.tblBalanceSharedBag;

            tbl.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: data
                , select: 'single'
                , columns: [

                    { "data": "GroupBag" },
                    { "data": "Bag" },
                    { "data": "Unit" },
                    { "data": "Stopper" },
                    { "data": "Consumption" },
                    { "data": "Balance" },
                    { "data": "DateValidity" },


                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    }
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
        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostpaidBalanceShared = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidBalanceShared'),
                options = $.extend({}, $.fn.PostpaidBalanceShared.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidBalanceShared', data);
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

    $.fn.PostpaidBalanceShared.defaults = {
    }

    $('#ContainerBalanceShared', $('.modal:last')).PostpaidBalanceShared();

})(jQuery);