(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormHistoryHR.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblHistoryHR: $('#tblHistoryHR', $element)
            , LoadingPostpaidHistoryHR: $('#LoadingPostpaidHistoryHR', $element)
        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {

            var that = this;
            that.getHistoryHR();
        },

        getDataHistoryHR: function myfunction(HistoryHR) {
            var that = this,
                controls = that.getControls();
           
            that.tblHistoryHR = controls.tblHistoryHR.DataTable({
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "ordering": false,
                "searching": false,
                "select": "single",
                "data": HistoryHR,
                "destroy": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": "No records available",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "FechaEmision" },
                    { "data": "FechaVencimiento" },
                    { "data": "TotalCurrentCharges" },
                    { "data": null },
                    { "data": "InvoiceNumber", "bVisible": false },
                    { "data": "Periodo", "bVisible": false }
                ]
                , columnDefs: [{
                    "targets": 3,
                    "render": function (full, meta) {
                        return "<a style='cursor:pointer'>Ver PDF</a>";
                    }
                }]

            });
            controls.LoadingPostpaidHistoryHR.hide();
        },
        getHistoryHR: function myfunction() {
            var that = this;
            var results;
            $.app.ajax({

                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetHistoryHR',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID },
                complete: function () {
                    if (that.result != null && that.result.data != null) {
                    if (that.result.data.listHistoryHR != null) {
                        that.HistoryHR_click();
                    }
                    }                    

                },
                success: function (result) {
                    that.result = result;

                    that.getDataHistoryHR(result.data.listHistoryHR);

                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormHistoryHR',
                        message: msger
                    });
                }
            });

        },
        HistoryHR_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblHistoryHR.find('tbody').find('a').addEvent(that, 'click', that.getAdditionalHistoryHR);

        },
        getAdditionalHistoryHR: function (send, args) {

            var that = this;
            var dataRowTM = that.tblHistoryHR.row($(send).parents('td')).data();
            var typeCallTM = $(send).parents('td').index();

            if (typeCallTM == '3') {

                var data = { strIdSession: Session.IDSESSION, strInvoiceNumber: dataRowTM.InvoiceNumber, strFechaEmision: dataRowTM.FechaEmision, strPeriodo: dataRowTM.Periodo };

                $.app.ajax({
                    async: true,
                    type: "POST",
                    dataType: "json",
                    url: '~/Dashboard/PostpaidDataBilling/GetHistoryHRFileName',
                    data: data,
                    success: function (result) {


                        if (result.Data.data.FlagBill == "1") {
                            
                            var valFilePath = result.Data.data.FilePath;
                            var valFileName = result.Data.data.FileName;
                            if (result.Data.data.arrBuffer != null && result.Data.data.arrBuffer != "" && result.Data.data.arrBuffer != undefined) {
                                var url = $.app.getPageUrl({ url: '~/Dashboard/PostPaidDataCollection/ShowFile' });
                                var urlfin = url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION + "&strEmissionDate=";
                                window.open(urlfin, "FACTURA_ELECTRONICA", "");
                            } else {
                                showAlert("El archivo no existe");
                            }
                        }
                        else {
                            showAlert("No hay Hoja de Resumen a mostrar");
                        }
                    },

                    error: function (msger) {
                        $.app.error({
                            id: 'FormHistoryHR',
                            message: msger
                        });
                    }
                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };

    $.fn.FormHistoryHR = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormHistoryHR'),
                options = $.extend({}, $.fn.FormHistoryHR.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormHistoryHR', data);
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

    $.fn.FormHistoryHR.defaults = {
    }

    $('#FormHistoryHR', $('.modal:last')).FormHistoryHR();

})(jQuery);