(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidHistoricalStriations.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblHistoricalStriations: $('#tblHistoricalStriations', $element)
            , lblClientCode: $('#lblClientCode', $element)
            , lblClient: $('#lblClient', $element)
            , lblTelephoneNumber: $('#lblTelephoneNumber', $element)
            , divPostpaid: $('#divPostpaid', $element)
            , errorHistoryStriation: $('#errorHistoryStriation', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },
        render: function () {
            var that = this;

            that.setCustomerInformation();
            that.getHistoricalStriations();
        },
        createTablegetHistoricalStriations: function (response) {
            var controls = this.getControls();
            var cont = 0;
            if (response.data.HistoricalStriations) {
                cont = response.data.HistoricalStriations.length;
            }
            $('#lblContHistoricalStriations').html('Se encontraron ' + cont + ' registro(s).');
          
            controls.tblHistoricalStriations.DataTable({
                 info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollY: 300
                , select: 'single'
                , scrollCollapse: true
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: response.data.HistoricalStriations
                , columns: [
                    { "data": "Plan" },
                    { "data": "Modification" },
                    { "data": "User" },
                    { "data": "Date" },
                    { "data": "Destination" },
                    { "data": "Description" },
                    { "data": "Factor" }
                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4, 5, 6]
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
        getHistoricalStriations: function () {
            var that = this,
                controls = that.getControls(),
                oDataService = Session.DATASERVICE,
                objHistoricalStriations = {};
            objHistoricalStriations.Application = Session.ORIGINTYPE;
            objHistoricalStriations.Telephone = oDataService.CellPhone;
            objHistoricalStriations.strIdSession = Session.IDSESSION;
            objHistoricalStriations.CustomerId = Session.DATACUSTOMER.CustomerID;
            controls.errorHistoryStriation.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblHistoricalStriations.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetHistoricalStriations',
                data: JSON.stringify(objHistoricalStriations),
                success: function (response) {
                    controls.tblHistoricalStriations.find('tbody').html('');
                    that.createTablegetHistoricalStriations(response);
                },
                error: function (ex) {
                    controls.tblHistoricalStriations.find('tbody').html('');
                    $.app.error({
                        id: 'errorHistoryStriation',
                        message: ex,
                        click: function () {
                            that.getHistoricalStriations();
                        }
                    });
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setCustomerInformation: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE;

            controls.lblClientCode.after(oCustomer.Account);
            controls.lblClient.after(oCustomer.FullName);
            controls.lblTelephoneNumber.after(oDataService.CellPhone);
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidHistoricalStriations = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidHistoricalStriations'),
                options = $.extend({}, $.fn.PostPaidHistoricalStriations.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidHistoricalStriations', data);
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

    $.fn.PostPaidHistoricalStriations.defaults = {
    }

    $('#contenedorHistoricalStriations', $('.modal:last')).PostPaidHistoricalStriations();

})(jQuery);