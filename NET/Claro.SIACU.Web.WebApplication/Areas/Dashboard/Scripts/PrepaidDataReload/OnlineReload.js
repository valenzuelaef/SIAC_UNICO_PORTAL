(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.OnlineRecharge.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , txtDateStartOnlineRecharge: $('#txtDateStartOnlineRecharge', $element)
          , txtDateEndOnlineRecharge: $('#txtDateEndOnlineRecharge', $element)
          , cboEventTypeRecharge: $('#cboEventTypeRecharge', $element)
          , containerDateRangeOnLineRecharge: $('#containerDateRangeOnLineRecharge', $element)
          , btnSearchOnlineRecharge: $('#btnSearchOnlineRecharge', $element)
          , tblOnlineRecharge: $('#tblOnlineRecharge', $element)
          , errorOnlineRecharge: $('#errorOnlineRecharge', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.containerDateRangeOnLineRecharge.datepicker({
                format: 'dd/mm/yyyy',
                endDate: $.app.date.format(new Date(), { format: 'dd/mm/yy' })
            });

            controls.btnSearchOnlineRecharge.addEvent(that, 'click', that.btnSearchOnlineRecharge_click);
            controls.txtDateStartOnlineRecharge.val(window.opener.$('#txtReloadDateFrom').val());
            controls.txtDateEndOnlineRecharge.val(window.opener.$('#txtReloadDateTo').val());

            that.render();
        },
        render: function () {
            this.getEventType();
            this.getOnlineReload();
        },
        btnSearchOnlineRecharge_click: function () {
            this.getOnlineReload();
        },
        getEventType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataReload/GetEventType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.cboEventTypeRecharge
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },
        formatDate: function (data) {
            if (data == null) return null;
            for (var n = 0; n <= data.length - 1; n++) {
                data[n].DateTimeOperation = $.ConvertFormatfrom24to12(data[n].DateTimeOperation);
                data[n].ExpirationDate = $.ConvertFormatfrom24to12(data[n].ExpirationDate);
            }
            return data;
        },
        createTableOnlineRecharge: function (response) {
            var that = this,
                controls = that.getControls();
            $('#tblOnlineRecharge tbody').html('');
            response.data.listDataReloadModel = this.formatDate(response.data.listDataReloadModel);
            controls.tblOnlineRecharge.DataTable({
                info: false,
                paging: false,
                scrollY: 300,
                destroy: true,
                scrollCollapse: true,
                searching: false,
                select: 'single',
                data: response.data.listDataReloadModel,
                columns: [
                    { "data": "DateTimeOperation" },
                    { "data": "TypeEvent" },
                    { "data": "NominalAmount" },
                    { "data": "FinalBalance" },
                    { "data": "Bag" },
                    { "data": "Detail" },
                    { "data": "ExpirationDate" }
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
        getOnlineReload: function () {
            var that = this,
                controls = that.getControls(),
                objOnlineReload = {
                    strIdSession: Session.IDSESSION,
                    Msisdn: Session.TELEPHONE,
                    StartDate: controls.txtDateStartOnlineRecharge.val(),
                    EndDate: controls.txtDateEndOnlineRecharge.val(),
                    TrafficType: controls.cboEventTypeRecharge.find(':selected').val()
                };

            controls.errorOnlineRecharge.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblOnlineRecharge.find('tbody').html('<tr><td colspan="7" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataReload/GetOnlineReload',
                data: JSON.stringify(objOnlineReload),
                success: function (response) {
                    that.createTableOnlineRecharge(response);
                },
                error: function (msger) { }
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

    $.fn.OnlineRecharge = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('OnlineRecharge'),
                options = $.extend({}, $.fn.OnlineRecharge.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('OnlineRecharge', data);
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

    $.fn.OnlineRecharge.defaults = {};
    $('#containerOnlineRecharge').OnlineRecharge();
})(jQuery);