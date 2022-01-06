(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidHistoricalRecharge.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCicleBillingHistoricalRecharge: $('#lblCicleBillingHistoricalRecharge', $element)
            , txtlblDateStartHistoricalRecharge: $('#txtlblDateStartHistoricalRecharge', $element)
            , txtlblDateEndHistoricalRecharge: $('#txtlblDateEndHistoricalRecharge', $element)
            , btnSearchHistoricalRecharge: $('#btnSearchHistoricalRecharge', $element)
            , btnCleanHistoricalRecharge: $('#btnCleanHistoricalRecharge', $element)
            , tblHistoricalRecharge: $('#tblHistoricalRecharge', $element)
            , btnComsumptionHistoricalRecharge: $('#btnComsumptionHistoricalRecharge', $element)
            , containerDateRangeInLine: $('#containerDateRangeInLine', $element)


        });


    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = that.getControls();
            controls.btnSearchHistoricalRecharge.addEvent(that, 'click', that.btnSearchHistoricalRecharge_click);
            controls.btnComsumptionHistoricalRecharge.addEvent(that, 'click', that.btnComsumptionHistoricalRecharge_click);
            controls.btnCleanHistoricalRecharge.addEvent(that, 'click', that.btnCleanHistoricalRecharge_click);

            controls.containerDateRangeInLine.datepicker({
                format: 'dd/mm/yyyy',
                endDate: $.app.date.format(new Date(), { format: 'dd/mm/yy' })
            });
            that.render();
        },
        render: function () {
            var that = this;
            that.getHistoricalRecharge();
        },
        btnSearchHistoricalRecharge_click: function () {
            var that = this;

            that.getHistoricalRecharge();
        },
        btnComsumptionHistoricalRecharge_click: function () {
            $.window.open({
                modal: false,
                title: "Consumo de Recargas",
                url: '~/Dashboard/PostpaidDataService/GetConsumptionHistoricalRecharge',
                data: { strTelephone: Session.DATASERVICE.CellPhone },
                width: 1400,
                height: 590,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnCleanHistoricalRecharge_click: function () {

            var that = this,
              controls = that.getControls();
            controls.txtlblDateEndHistoricalRecharge.val("");
            controls.txtlblDateStartHistoricalRecharge.val("");
            that.getHistoricalRecharge();
        },
        getHistoricalRecharge: function () {
            var flagPlataform;
            var FlagcoId = Session.DATACUSTOMER.coIdPub;
            if (FlagcoId === null || FlagcoId === "") {
                //Asis
                flagPlataform = "0"
            } else {
                var flag = FlagcoId.substring(0, 3);
                if (flag == "MIG") {
                    //Migrado
                    flagPlataform = "2"
                } else {
                    //Tobe
                    flagPlataform = "1"
                }
            }

            var that = this,
                controls = that.getControls(),
                objDataService = Session.DATASERVICE,
                objCustomer = Session.DATACUSTOMER,
                objHistoricalRecharge = {};
            objHistoricalRecharge.strIdSession = Session.IDSESSION;
            objHistoricalRecharge.strDateEnd = controls.txtlblDateEndHistoricalRecharge.val();
            objHistoricalRecharge.strDateStart = controls.txtlblDateStartHistoricalRecharge.val();
            objHistoricalRecharge.strMsisdn = objDataService.CellPhone;
            objHistoricalRecharge.strFlagPlataform = flagPlataform;
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblHistoricalRecharge.find('tbody').html('<tr><td colspan="3" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonHistoryRecharge',
                data: JSON.stringify(objHistoricalRecharge),
                success: function (response) {

                    console.log(response);
                    if (response.data != null) {
                        console.log(1);
                        controls.lblCicleBillingHistoricalRecharge.empty()
                        controls.lblCicleBillingHistoricalRecharge.append("Ciclo Facturación : " + Session.DATACUSTOMER.objPostDataAccount.BillingCycle);
                        that.setTableHistoricalRecharge(response.data.lstHistoricalRechargeHelper);
                        if (response.data.MessageAlert != null && response.data.MessageAlert != "") {
                            showAlert(response.data.MessageAlert, "Mensaje Histórico Recargas");
                        }
                    } else {
                        console.log(2);
                        that.setTableHistoricalRecharge(null);

                    }

                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalRecharge',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });

        },
        setTableHistoricalRecharge: function (data) {
            var controls = this.getControls(),
                tbl = controls.tblHistoricalRecharge;
            if (data == null) data = {};
            console.log(data);
            controls.tblHistoricalRecharge.find('tbody').html('');
            console.log(4);

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

                    { "data": "amountRecharge" },
                    { "data": "canal" },
                    { "data": "dateRecharge" }



                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2]
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


        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostpaidHistoricalRecharge = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidHistoricalRecharge'),
                options = $.extend({}, $.fn.PostpaidHistoricalRecharge.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidHistoricalRecharge', data);
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

    $.fn.PostpaidHistoricalRecharge.defaults = {
    }

    $('#ContainerHistoricalRecharge', $('.modal:last')).PostpaidHistoricalRecharge();

})(jQuery);