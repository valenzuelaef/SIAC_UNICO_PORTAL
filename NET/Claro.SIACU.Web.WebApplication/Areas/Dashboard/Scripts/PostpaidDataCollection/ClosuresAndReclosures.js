(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.ClosuresAndReclosures.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            lblCaso: $('#lblCaso', $element),
            errorManagementOfClosures: $('#errorManagementOfClosures', $element),
            errorReopenOfTheCase: $('#errorReopenOfTheCase', $element),
            tblManagementOfClosures: $('#tblManagementOfClosures', $element),
            tblReopenOfTheCase: $('#tblReopenOfTheCase', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.setCase();
            this.getManagementOfClosures();
            this.getReopenOfTheCase();
        },
        setCase: function () {
            this.getControls().lblCaso.html($('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[1]);
        },
        getManagementOfClosures: function () {
            var that = this,
                controls = that.getControls(),
                objCustomerInformation = {
                    IdCaso: $('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    strIdSession: Session.IDSESSION
                };

            controls.errorManagementOfClosures.html('');
            controls.tblManagementOfClosures.find('tbody').html('<tr><td colspan="5" align="center"><img src="' + "/Images/loading2.gif" + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetManagementOfClosures',
                data: JSON.stringify(objCustomerInformation),
                success: function (response) {
                    controls.tblManagementOfClosures.find('tbody').html('');
                    that.setManagementOfClosures(response);
                },
                error: function (ex) {
                    controls.tblManagementOfClosures.find('tbody').html('');
                    $.app.error({
                        id: 'errorManagementOfClosures',
                        message: ex
                    });
                }
            });
        },
        setManagementOfClosures: function (response) {
            this.getControls().tblManagementOfClosures.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollY: 200,
                scrollCollapse: true,
                destroy: true,
                data: response.data.ManagementOfClosures,
                columns: [
                    { "data": "ClosureCase" },
                    { "data": "Resolution" },
                    { "data": "Result" },
                    { "data": "State" },
                    { "data": "DateSentToTheBank" }
                ],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        getReopenOfTheCase: function () {
            var that = this,
                controls = that.getControls(),
                objCustomerInformation = {
                    IdCaso: $('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    strIdSession: Session.IDSESSION
                };

            controls.errorReopenOfTheCase.html('');
            controls.tblReopenOfTheCase.find('tbody').html('<tr><td colspan="3" align="center"><img src="' +  "/Images/loading2.gif" + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetReopenOfTheCase',
                data: JSON.stringify(objCustomerInformation),
                success: function (response) {
                    controls.tblReopenOfTheCase.find('tbody').html('');
                    that.setReopenOfTheCase(response);
                },
                error: function (ex) {
                    controls.tblReopenOfTheCase.find('tbody').html('');
                    $.app.error({
                        id: 'errorReopenOfTheCase',
                        message: ex
                    });
                }
            });
        },
        setReopenOfTheCase: function (response) {
            this.getControls().tblReopenOfTheCase.DataTable({
                info: false,
                paging: false,
                searching: false,
                select:'single',
                scrollY: 200,
                scrollCollapse: true,
                destroy: true,
                data: response.data.ReopenOfTheCases,
                columns: [
                    { "data": "ReopeningCase" },
                    { "data": "ReapvPhaseClarify" },
                    { "data": "ReapdReopeningProcess" }
                ],
                language: {
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
    };
    $.fn.ClosuresAndReclosures = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ClosuresAndReclosures'),
                options = $.extend({}, $.fn.ClosuresAndReclosures.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('ClosuresAndReclosures', data);
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
    $.fn.ClosuresAndReclosures.defaults = {};
    $('#containerClosuresAndReclosures', $('.modal:last')).ClosuresAndReclosures();
})(jQuery);

