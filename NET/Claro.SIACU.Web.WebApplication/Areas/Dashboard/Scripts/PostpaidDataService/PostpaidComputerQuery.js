(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidComputerQuery.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblAccount: $('#lblAccount', $element)
            , lblCustomer: $('#lblCustomer', $element)
            , lblContact: $('#lblContact', $element)
            , tblComputerQuery: $('#tblComputerQuery', $element)
            , errorComputerQuery: $('#errorComputerQuery', $element)
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
            that.getComputerQuery();
        },
        createTableComputerQuery: function (response) {
            var that = this,
                controls = that.getControls();

            controls.tblComputerQuery.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , select : 'single'
                , data: response.data.Decos
                , columns: [
                    { "data": "MaterialCode" },
                    { "data": "SapCode" },
                    { "data": "SerieNumber" },
                    { "data": "AdressMac" },
                    { "data": "DescriptionMaterial" },
                    { "data": "EquipmentType" },
                    { "data": "ProductId" },
                    { "data": "Model" },
                    { "data": "ConvertType" },
                    { "data": "MainService" },
                    { "data": "Headend" },
                    { "data": "EphomeexChange" },
                    { "data": "Number" }
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
        getComputerQuery: function () {
            var that = this,
                controls = that.getControls(),
                objComputerQuery = {};

            objComputerQuery.CustomerID = Session.DATACUSTOMER.CustomerID;
            objComputerQuery.ContractID = Session.DATACUSTOMER.ContractID;
            objComputerQuery.strIdSession = Session.IDSESSION;

            controls.errorComputerQuery.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblComputerQuery.find('tbody').html('<tr><td colspan="13" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetComputerQuery',
                data: JSON.stringify(objComputerQuery),
                success: function (response) {
                    controls.tblComputerQuery.find('tbody').html('');
                    that.createTableComputerQuery(response);
                },
                error: function (msger) {
                    controls.tblComputerQuery.find('tbody').html('');
                    $.app.error({
                        id: 'errorComputerQuery',
                        message: msger,
                        click: function () {
                            that.getComputerQuery();
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
                oCustomer = Session.DATACUSTOMER;

            controls.lblCustomerId.after(oCustomer.CustomerID);
            controls.lblAccount.after(oCustomer.Account);
            controls.lblCustomer.after(oCustomer.BusinessName);
            controls.lblContact.after(oCustomer.FullName);
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidComputerQuery = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getPetitions'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidComputerQuery'),
                options = $.extend({}, $.fn.PostPaidComputerQuery.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidComputerQuery', data);
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

    $.fn.PostPaidComputerQuery.defaults = {
    }

    $('#containerComputerQuery', $('.modal:last')).PostPaidComputerQuery();

})(jQuery);