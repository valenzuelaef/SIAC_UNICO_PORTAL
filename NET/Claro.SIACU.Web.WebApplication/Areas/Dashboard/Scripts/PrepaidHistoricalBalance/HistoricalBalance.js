(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.HistoricalBalancePrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , txtDateStartHistoricalBalance: $('#txtDateStartHistoricalBalance', $element)
          , txtDateEndHistoricalBalance: $('#txtDateEndHistoricalBalance', $element)
          , containerDateRangeHistoricalBalance: $('#containerDateRangeHistoricalBalance', $element)
          , btnSearchHistoricalBalance: $('#btnSearchHistoricalBalance', $element)
          , tblHistoricalBalance: $('#tblHistoricalBalance', $element)
          , btnCleanSearchHistoricalBalance: $('#btnCleanSearchHistoricalBalance', $element)
          , errorInline: $('#errorInline', $element)
          , hidendDateHistoricalBalance: $('#hidendDateHistoricalBalance', $element)
          , hidstartDateHistoricalBalance: $('#hidstartDateHistoricalBalance', $element)


        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.containerDateRangeHistoricalBalance.datepicker({
                format: 'dd/mm/yyyy',
                //  startDate: controls.hidstartDateHistoricalBalance.val(),
                //  endDate: $.app.date.format(new Date(), { format: 'dd/mm/yyyy' }),

            });

            controls.btnSearchHistoricalBalance.addEvent(that, 'click', that.btnSearchHistoricalBalance_click);
            controls.txtDateStartHistoricalBalance.val(controls.hidstartDateHistoricalBalance.val());
            controls.txtDateEndHistoricalBalance.val(controls.hidendDateHistoricalBalance.val());
            controls.btnCleanSearchHistoricalBalance.addEvent(that, 'click', that.btnCleanSearchHistoricalBalance_click);



            that.render();
        },
        render: function () {
            var that = this;

            that.getHistoricalBalance();
        },
        btnSearchHistoricalBalance_click: function () {
            var that = this;
            that.getHistoricalBalance();
        },

        btnCleanSearchHistoricalBalance_click: function () {
            $('#tblHistoricalBalance').find('tbody').html("");
        },


        createTableHistoricalBalance: function (response) {
            var that = this,
                controls = that.getControls();
            controls.tblHistoricalBalance.DataTable({
                info: false,
                paging: false,
                scrollY: 300,
                destroy: true,
                scrollCollapse: true,
                searching: false,
                select: 'single',
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true,
                data: response,
                columns: [
                    { "data": "Name" },
                    { "data": "CommercialName" },
                    { "data": "Destination" },
                    { "data": "Balance" },
                    { "data": "Unity" },
                    { "data": "Expiration" },
                    { "data": "NameCode" },

                ],
                order: [[0, 'desc']],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        CompareDates: function (fecha, fecha2, flag) {

            var xMonth = fecha.substring(3, 5);
            var xDay = fecha.substring(0, 2);
            var xYear = fecha.substring(6, 10);
            var yMonth = fecha2.substring(3, 5);
            var yDay = fecha2.substring(0, 2);
            var yYear = fecha2.substring(6, 10);
            if (xYear > yYear) {
                return (true)
            }
            else {
                if (xYear == yYear) {
                    if (xMonth > yMonth) {
                        return (true)
                    }
                    else {
                        if (xMonth == yMonth) {
                            if (!flag) {
                                if (xDay > yDay)
                                    return (true);
                                else
                                    return (false);
                            } else {
                            if (xDay >= yDay)
                                return (true);
                            else
                                return (false);
                        }
                        }
                        else
                            return (false);
                    }
                }
                else
                    return (false);
            }

        },
        getFilterHistoricalBalanceByRangeDate: function (lstBalanceHistorical) {
            var that = this,
              controls = that.getControls();
            var newlstBalanceHistorical = [];
            var startDate = controls.txtDateStartHistoricalBalance.val();
            var endDate = controls.txtDateEndHistoricalBalance.val();

            if (!$.isEmptyObject(lstBalanceHistorical)) {

                $.each(lstBalanceHistorical, function (clave, valor) {

                    if (!$.string.isEmptyOrNull(valor.Expiration)) {

                        var dateVar = valor.Expiration;
                        var dsplit = dateVar.split(" ");
                        var d = new Date();
                        var strDateNow = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();
                        if (that.CompareDates(dsplit[0], startDate,true) && that.CompareDates(endDate, dsplit[0]),true) {

                            if (that.CompareDates(strDateNow, dsplit[0],false)) {
                                if (valor.Balance != "0") {
                                    newlstBalanceHistorical.push(valor);
                                }
                            } else {
                                newlstBalanceHistorical.push(valor);
                            }

                        }
                    } else {
                            newlstBalanceHistorical.push(valor);
                        }
                });
            }

            return newlstBalanceHistorical;

        },
        getHistoricalBalance: function () {
            var that = this;
               
            var lstBalanceHistorical = Session.DATASERVICE.listBalance;


            that.createTableHistoricalBalance(that.getFilterHistoricalBalanceByRangeDate(lstBalanceHistorical));

        },
        GetExportHistoricalBalance: function () {
            var that = this;
                
            var strUrlModal = '~/Dashboard/PrepaidDataService/ExportExcelHistoricalBalance';
            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var arrlstBalanceHistorical = [];
            var lstBalanceHistorical = Session.DATASERVICE.listBalance;
            arrlstBalanceHistorical= that.getFilterHistoricalBalanceByRangeDate(lstBalanceHistorical);
            
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(arrlstBalanceHistorical),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=HistoricoSaldoBolsaPrepago.xlsx";
                }
            });
           
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };

    $.fn.HistoricalBalancePrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportHistoricalBalance'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HistoricalBalancePrepaid'),
                options = $.extend({}, $.fn.HistoricalBalancePrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HistoricalBalancePrepaid', data);
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

    $.fn.HistoricalBalancePrepaid.defaults = {};
    $('#containerHistoricalBalance').HistoricalBalancePrepaid();
})(jQuery);