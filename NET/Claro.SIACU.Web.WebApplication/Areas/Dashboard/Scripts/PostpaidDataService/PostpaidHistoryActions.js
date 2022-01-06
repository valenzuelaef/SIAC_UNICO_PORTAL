(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidHistoryActions.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtDateStart: $('#txtDateStart', $element)
            , txtDateEnd: $('#txtDateEnd', $element)
            , btnSearchHistoryActions: $('#btnSearchHistoryActions', $element)
            , tblHistoryActions: $('#tblHistoryActions', $element)
            , divHistoryActions: $('#divHistoryActions', $element)
            , contentHistoryActions: $('#contentHistoryActions', $element)
            , containerDateRange: $('#containerDateRange', $element)
        });

    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            var fDateToDay = new Date(), fDatePrevious = new Date();
            fDateToDay = $.app.date.format(fDateToDay, { format: 'dd/mm/yy' });
           fDatePrevious = $.app.date.addMonth(fDatePrevious, -1, { format: 'dd/mm/yy' });

        
            controls.txtDateEnd.val(fDateToDay);
            controls.txtDateStart.val(fDatePrevious);
            controls.txtDateStart.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy', });
            controls.btnSearchHistoryActions.addEvent(that, 'click', that.btnSearchHistoryActions_click);
            controls.txtDateStart.attr('readonly', true);
            controls.txtDateEnd.attr('readonly', true);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });

            that.render();
        },

        render: function () {
            var that = this;
            that.btnSearchHistoryActions_click();
        },
        createTableAccordingToApplication: function () {
            if (Session.DATASERVICE.Application == 'POSTPAID')
                this.getHistoryActionsPostPaid();
            else
                this.getHistoryActionsHFC();
        },
        btnSearchHistoryActions_click: function () {
            this.createTableAccordingToApplication();
        },
        getHistoryActionsPostPaid: function () {
            var that = this,
            controls = that.getControls(),
            objHistoryActions = {};

            controls.divHistoryActions.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.contentHistoryActions.find('table tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            objHistoryActions.Telephone = Session.DATASERVICE.CellPhone;
            objHistoryActions.StartDate = controls.txtDateStart.val();
            objHistoryActions.EndDate = controls.txtDateEnd.val();
            objHistoryActions.strIdSession = Session.IDSESSION;
            objHistoryActions.plataform = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            objHistoryActions.flagConvivencia = Session.DATACUSTOMER.flagConvivencia

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetHistoryActions',
                data: JSON.stringify(objHistoryActions),
                success: function (response) {
                    that.createTableHistoryActions(response);
                },
                error: function (ex) {
                    controls.contentHistoryActions.find('table tbody').html('');
                    $.app.error({
                        id: 'divHistoryActions', message: ex,
                    });
                }
            });
        },
        getHistoryActionsHFC: function () {
            var that = this,
                controls = that.getControls(),
                objHistoryActions = {};

            controls.divHistoryActions.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.contentHistoryActions.find('table tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            objHistoryActions.Contract = Session.DATACUSTOMER.ContractID;
            objHistoryActions.StartDate = controls.txtDateStart.val();
            objHistoryActions.EndDate = controls.txtDateEnd.val();
            objHistoryActions.strIdSession = Session.IDSESSION;

            objHistoryActions.plataform = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            objHistoryActions.flagConvivencia = Session.DATACUSTOMER.flagConvivencia;
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE")
            {
                objHistoryActions.TelephoneTOBE = "51" + Session.DATASERVICE.CellPhone;
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetHistoryActions',
                data: JSON.stringify(objHistoryActions),
                success: function (response) {
                    that.createTableHistoryActions(response);
                },
                error: function (ex) {
                    controls.contentHistoryActions.find('table tbody').html('');
                    $.app.error({
                        id: 'divHistoryActions', message: ex,
                    });
                }
            });
        },
        createTableHistoryActions: function (response) {
            var controls = this.getControls();

            controls.tblHistoryActions.DataTable(
                {
                    info: false
                    , scrollY: 300
                    , paging: false
                    , destroy: true
                    , select: 'single'
                    , searching: false
                    , data: response.data
                    , columns: [
                        { "data": null },
                        { "data": "Contract" },
                        { "data": "Description" },
                        { "data": "Service" },
                        { "data": "Date" },
                        { "data": "Hour" },
                        { "data": "User" }

                    ]
                    , columnDefs: [
                        { className: "text-center", targets: [0,1,2,3,4,5,6] },
                        { type: 'de_datetime', targets: 4 }
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
                }
            );
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }
    $.fn.PostPaidHistoryActions = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidHistoryActions'),
                options = $.extend({}, $.fn.PostPaidHistoryActions.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidHistoryActions', data);
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

    $.fn.PostPaidHistoryActions.defaults = {
    }

    $('#contentHistoryActions', $('.modal:last')).PostPaidHistoryActions();

})(jQuery);