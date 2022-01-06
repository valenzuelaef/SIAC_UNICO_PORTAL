
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormHistoryInvoice.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblHistoryInvoice: $('#tblHistoryInvoice', $element)
            , LoadingPostpaidHistoryInvoice: $('#LoadingPostpaidHistoryInvoice', $element)
             , divPostBillingInfo: $('#divPostBillingInfo', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },

        render: function () {
            var that = this;
            that.getHistoryInvoice();
        },

        applicativeRoute: window.location.href,
        getHistoryInvoice: function () {
            var that = this;
            var data = { strIdSession: Session.IDSESSION, strCustomerID: Session.DATACUSTOMER.CustomerID };

            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/getHistoryInvoice',
                data: data,
                complete: function () {
                    that.AdditionalHistoryInvoice_click();
                },
                success: function (result) {
                    console.log(result.data.listInvoice);
                    that.getDataHistoryInvoice(result.data.listInvoice);
                    
                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormHistoryInvoice',
                        message: msger
                    });
                }
            });

        },

        getDataHistoryInvoice: function myfuncion(listInvoice) {

            var that = this,
            controls = that.getControls();

            that.tblHistoryInvoice = controls.tblHistoryInvoice.DataTable({
                "info": false,
                "scrollY": "180px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": listInvoice,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": null },
                    { "data": "FechaEmision" },
                    { "data": "FechaVencimiento" },
                    { "data": "TotalCurrentCharges" },
                    { "data": null },
                    { "data": "InvoiceNumber", "bVisible": false },
                    { "data": "Periodo", "bVisible": false }
                ],
                columnDefs: [{
                    "targets": 0,
                    "render": function (full, meta) {
                        return "<a style='cursor:pointer'>Seleccionar</a>";
                    }
                },
                {
                    "targets": 4,
                    "render": function (full, meta) {
                        return "<a style='cursor:pointer'>Ver PDF</a>";
                    }
                }
                ]
            });
            controls.LoadingPostpaidHistoryInvoice.hide();
        },

        AdditionalHistoryInvoice_click: function () {
            var that = this,
            controls = that.getControls();
            controls.tblHistoryInvoice.find('tbody').find('a').addEvent(that, 'click', that.f);

        },
        getAdditionalHistoryInvoice: function (send, args) {
            var that = this;
            var dataRowTM = that.tblHistoryInvoice.row($(send).parents('td')).data();
            var typeCallTM = $(send).parents('td').index();
            if (typeCallTM == '0') {
               
                $.app.ajax({
                    type: 'POST',
                    url: '~/Dashboard/Postpaid/DataBilling',
                    
                    dataType: 'html',
                    async:false,
                    data: {
                        strCustomerID: Session.DATACUSTOMER.CustomerID,
                        strApplication: Session.DATACUSTOMER.Application,
                        strIdSession: Session.IDSESSION,
                        strContratoID: Session.DATACUSTOMER.ContractID,
                        strInvoiceNumber: dataRowTM.InvoiceNumber,
                        strFechaEmision: dataRowTM.FechaEmision,
                        strFechaVencimiento: dataRowTM.FechaVencimiento,
                        plataformaAT: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                        strcsIdPub: Session.DATACUSTOMER.csIdPub,
                        strbmIdPub: Session.DATACUSTOMER.objPostDataAccount.bmIdPub,
                        coIdPub: Session.DATACUSTOMER.coIdPub
                    },
                    complete: function () {
                        $('#btnCloseHistoryBilling').click();
                    },
                    success: function (result) {
                       
                        
                        window.opener.$('#divPostBillingInfo').html(result);
                    },
                    error: function (ex) {
                       
                        $.app.error({
                            id: 'divPostBillingInfo',
                            message: ex,
                            click: function () { that.getAdditionalHistoryInvoice() }
                        });
                    }
                });



            }
            if (typeCallTM == '4') {

                var data = { strIdSession: Session.IDSESSION, strInvoiceNumber: dataRowTM.InvoiceNumber, strFechaEmision: dataRowTM.FechaEmision, strPeriodo: dataRowTM.Periodo };

                $.app.ajax({
                    async: true,
                    type: "POST",
                    dataType: "json",
                    url: '~/Dashboard/PostpaidDataBilling/GetHistoryInvoiceFileName',
                    data: data,
                    success: function (result) {

                        if (result.data.FlagBill == "1") {
                            var valFilePath = result.data.FilePath;
                            var valFileName = result.data.FileName;
                            var url = $.app.getPageUrl({ url: '~/Dashboard/PostpaidDataBilling/ShowInvoice' });

                            window.open(url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                          
                            
                        }
                        else {
                            showAlert("No hay factura a mostrar");
                        }
                    },

                    error: function (msger) {
                        $.app.error({
                            id: 'FormHistoryInvoice',
                            message: msger
                        });
                    }
                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta de Historial de facturas ",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.FormHistoryInvoice = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('form'),
                options = $.extend({}, $.fn.FormHistoryInvoice.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormHistoryInvoice', data);
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

    $.fn.FormHistoryInvoice.defaults = {
    }

    $('#FormHistoryInvoice', $('.modal:last')).FormHistoryInvoice();

})(jQuery);