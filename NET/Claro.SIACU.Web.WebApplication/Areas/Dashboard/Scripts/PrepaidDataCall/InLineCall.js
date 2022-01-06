(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.InLineCallPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , txtDateStartInLine: $('#txtDateStartInLine', $element)
          , txtDateEndInLine: $('#txtDateEndInLine', $element)
          , cboEventType: $('#cboEventType', $element)
          , containerDateRangeInLine: $('#containerDateRangeInLine', $element)
          , btnSearchInLine: $('#btnSearchInLine', $element)
          , tblInLine: $('#tblInLine', $element)
          , btnCleanSearchInLine: $('#btnCleanSearchInLine', $element)
          , errorInline: $('#errorInline', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.containerDateRangeInLine.datepicker({
                format: 'dd/mm/yyyy',
                endDate: $.app.date.format(new Date(), { format: 'dd/mm/yy' })
            });

            controls.btnSearchInLine.addEvent(that, 'click', that.btnSearchInLine_click);
            controls.txtDateStartInLine.val(window.opener.$('#txtCallDateFrom').val());
            controls.txtDateEndInLine.val(window.opener.$('#txtCallDateTo').val());
            controls.btnCleanSearchInLine.addEvent(that, 'click', that.btnCleanSearchInLine_click);

            that.render();
        },
        render: function () {
            this.getEventType();
            this.getInLineCall();
        },
        btnSearchInLine_click: function () {
            this.getInLineCall();
        },

        btnCleanSearchInLine_click: function () {
            var controls = this.getControls();

            controls.txtDateStartInLine.val('');
            controls.txtDateEndInLine.val('');
            controls.cboEventType.val('');
            $('#tblInLine').find('tbody').html("");
        },

        getEventType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataCall/GetEventType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.cboEventType
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

                data[n].CallDateTime = $.ConvertFormatfrom24to12(data[n].CallDateTime);
                data[n].CallStart = $.ConvertFormatfrom24to12(data[n].CallStart);
                data[n].CallEnd = $.ConvertFormatfrom24to12(data[n].CallEnd);


            }
            return data;

        },
        createTableInLine: function (response) {
            var that = this,
                controls = that.getControls();
            
            response.data.listDataCallModel = this.formatDate(response.data.listDataCallModel);
            controls.tblInLine.DataTable({
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
                data: response.data.listDataCallModel,
                columns: [
                    { "data": "CallDateTime" },
                    { "data": "CallTelephoneDestination" },
                    { "data": "CallDuration" },
                    { "data": "CallStart" },
                    { "data": "CallEnd" },
                    { "data": "CallCost" },
                    { "data": "CallBalance" },
                    { "data": "CallBag" },
                    { "data": "CallPlan" },
                    { "data": "CallEventType" }
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
        getInLineCall: function () {
            var that = this,
                controls = that.getControls(),
                objInLineCall = {
                    strIdSession: Session.IDSESSION,
                    Msisdn: Session.TELEPHONE,
                    StartDate: controls.txtDateStartInLine.val(),
                    EndDate: controls.txtDateEndInLine.val(),
                    TrafficType: controls.cboEventType.find('option:selected').val()
                };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataCall/GetInLineCall',
                data: JSON.stringify(objInLineCall),
                success: function (response) {
                    response = that.formatDate(response);
                    that.createTableInLine(response);
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

    $.fn.InLineCallPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('InLineCallPrepaid'),
                options = $.extend({}, $.fn.InLineCallPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('InLineCallPrepaid', data);
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

    $.fn.InLineCallPrepaid.defaults = {};
    $('#containerInline').InLineCallPrepaid();
})(jQuery);