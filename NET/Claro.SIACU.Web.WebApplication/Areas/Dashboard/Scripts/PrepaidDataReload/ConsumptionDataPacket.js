(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.ConsumptionDataPacket.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , txtDateStartDataPacket: $('#txtDateStartDataPacket', $element)
          , txtDateEndDataPacket: $('#txtDateEndDataPacket', $element)
          , containerConsumptionDataPacket: $('#containerConsumptionDataPacket', $element)
          , btnSearchDataPacket: $('#btnSearchDataPacket', $element)
          , tblDataPacket: $('#tblDataPacket', $element)
          , errorConsumptionDataPacket: $('#errorConsumptionDataPacket', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.containerConsumptionDataPacket.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnSearchDataPacket.addEvent(that, 'click', that.btnSearchDataPacket_click);
            controls.txtDateStartDataPacket.val($.app.date.addMonth(new Date(), -10, { format: 'dd/mm/yy' }));
            controls.txtDateEndDataPacket.val($.app.date.format(new Date(), { format: 'dd/mm/yy' }));
            that.render();
        },
        render: function () {
            this.getConsumptionDataPacket();
        },
        createTableConsumptionDataPacket: function (response) {
            var that = this,
                controls = that.getControls();
            controls.tblDataPacket.find('tbody').html('');
            controls.tblDataPacket.DataTable({
                info: false,
                paging: false,
                scrollY: 300,
                destroy: true,
                scrollCollapse: true,
                searching: false,
                select: 'single',
                data: response.data.ConsumptionDataPackets,
                columns: [
                    { "data": "DescriptionPackage" },
                    { "data": "ActivationDate" },
                    { "data": "ExpirationDate" },
                    { "data": "MBAvailable" },
                    { "data": "MBGranted" },
                    { "data": "MBConsumed" },
                    { "data": "State" }
                ],
                order: [1, 'desc'],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        btnSearchDataPacket_click: function () {
            this.getConsumptionDataPacket();
        },
        getExcelConsumptionDataPacket: function () {
            var controls = this.getControls(),
                objExcelConsumptionDataPacket = {
                    strIdSession: Session.IDSESSION,
                    PackageODCS: { Msisdn: Session.TELEPHONE },
                    StartDate: controls.txtDateStartDataPacket.val(),
                    EndDate: controls.txtDateEndDataPacket.val()
                };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PrepaidDataReload/GetExcelConsumptionDataPacket',
                data: JSON.stringify(objExcelConsumptionDataPacket),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' }) + '?strPath=' + path + "&strNewfileName=ReporteConsumoPaqueteDatos.xlsx";
                }
            });
        },
        getConsumptionDataPacket: function () {
            var that = this,
                controls = that.getControls(),
                objConsumptionDataPacket = {
                    strIdSession: Session.IDSESSION,
                    PackageODCS: { Msisdn: Session.TELEPHONE },
                    StartDate: controls.txtDateStartDataPacket.val(),
                    EndDate: controls.txtDateEndDataPacket.val()
                };

            controls.errorConsumptionDataPacket.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblDataPacket.find('tbody').html('<tr><td colspan="7" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataReload/GetConsumptionDataPacket',
                data: JSON.stringify(objConsumptionDataPacket),
                success: function (response) {
                    that.createTableConsumptionDataPacket(response);
                },
                error: function (msger) {  }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        }
    };

    $.fn.ConsumptionDataPacket = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getExcelConsumptionDataPacket'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ConsumptionDataPacket'),
                options = $.extend({}, $.fn.ConsumptionDataPacket.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('ConsumptionDataPacket', data);
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
    $.fn.ConsumptionDataPacket.defaults = {};
    $('#containerConsumptionDataPacket').ConsumptionDataPacket();
})(jQuery);