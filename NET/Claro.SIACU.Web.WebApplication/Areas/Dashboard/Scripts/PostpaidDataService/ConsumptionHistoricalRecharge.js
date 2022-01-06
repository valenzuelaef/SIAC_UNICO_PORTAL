(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidConsumptionRecharge.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtlblDateStartConsumption: $('#txtlblDateStartConsumption', $element)
            , txtlblDateEndConsumption: $('#txtlblDateEndConsumption', $element)
            , cboConsumptionType: $('#cboConsumptionType', $element)
            , btnSearchConsumptionRecharge: $('#btnSearchConsumptionRecharge', $element)
            , btnCleanConsumptionRecharge: $('#btnCleanConsumptionRecharge', $element)
            , tblConsumptionRecharge: $('#tblConsumptionRecharge', $element)
            , lblMensaje: $('#lblMensaje', $element)
            , containerDateRangeInLine: $('#containerDateRangeInLine', $element)

        });


    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            console.log("hihiih");
            var that = this, controls = that.getControls();
            controls.btnSearchConsumptionRecharge.addEvent(that, 'click', that.btnSearchConsumptionRecharge_click);

            controls.btnCleanConsumptionRecharge.addEvent(that, 'click', that.btnCleanConsumptionRecharge_click);

            controls.containerDateRangeInLine.datepicker({
                format: 'dd/mm/yyyy',
                endDate: $.app.date.format(new Date(), { format: 'dd/mm/yy' })
            });
            that.render();
        },
        render: function () {
            var that = this;
            console.log("4353535");
            that.getConsumptiontype();
            that.getConsumptionRecharge();

        },
        getConsumptiontype: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetTypeConsumption',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.cboConsumptionType
                        .populateDropDown({
                            dataSource: response.data.lstConsumptionTypeHelper,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },
        btnSearchConsumptionRecharge_click: function () {
            var that = this;

            that.getConsumptionRecharge();
        },

        btnCleanConsumptionRecharge_click: function () {
            var that = this,
                 controls = that.getControls();
            controls.txtlblDateStartConsumption.val("");
            controls.txtlblDateEndConsumption.val("");
            $('#cboConsumptionType').val('-1');
            that.getConsumptionRecharge();

        },
        setTableConsumptionRecharge: function (data) {
            var controls = this.getControls(),
                tbl = controls.tblConsumptionRecharge;
            if (data == null) data = {};
            console.log(data);
            controls.tblConsumptionRecharge.find('tbody').html('');
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

                   { "data": "AmountConsumed" },
                   { "data": "Balance" },
                   { "data": "DateEvent" },
                   { "data": "IdBag" },
                   { "data": "NumberDestinationAPN" },
                   { "data": "TypeConsumption" }

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

        getConsumptionRecharge: function () {

            var that = this,
                 controls = that.getControls(),
                 objDataService = Session.DATASERVICE,
                 objCustomer = Session.DATACUSTOMER,
                 objConsumptionRecharge = {};
            objConsumptionRecharge.strIdSession = Session.IDSESSION;
            objConsumptionRecharge.strDateStart = controls.txtlblDateStartConsumption.val();
            objConsumptionRecharge.strDateEnd = controls.txtlblDateEndConsumption.val();
            objConsumptionRecharge.strMsisdn = objDataService.CellPhone;
            objConsumptionRecharge.strTypeConsumption = controls.cboConsumptionType.find('option:selected').text();
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblConsumptionRecharge.find('tbody').html('<tr><td colspan="6" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonConsumptionRecharge',
                data: JSON.stringify(objConsumptionRecharge),
                success: function (response) {

                    console.log(response);
                    if (response.data != null) {
                        console.log(1);
                        that.setTableConsumptionRecharge(response.data.lstConsumptionRechargeHelper);
                        if (response.data.strMensajeAlert != null && response.data.strMensajeAlert != "")
                            showAlert(response.data.strMensajeAlert, "Mensaje Consumo Recargas");
                    } else {
                        console.log(2);
                        that.setTableConsumptionRecharge(null);

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
        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        }

    }

    $.fn.PostpaidConsumptionRecharge = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidConsumptionRecharge'),
                options = $.extend({}, $.fn.PostpaidConsumptionRecharge.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidConsumptionRecharge', data);
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

    $.fn.PostpaidConsumptionRecharge.defaults = {
    }

    $('#ContainerConsumptionRecharge', $('.modal:last')).PostpaidConsumptionRecharge();

})(jQuery);