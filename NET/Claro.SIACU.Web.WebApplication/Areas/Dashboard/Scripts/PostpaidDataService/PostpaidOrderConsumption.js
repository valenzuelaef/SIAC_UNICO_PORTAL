(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidOrderConsumption.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            , tblOrderConsumption: $('#tblOrderConsumption', $element)


        });


    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = that.getControls();

            that.render();
        },
        render: function () {
            var that = this;
            that.getOrderConsumption();
        },


        getOrderConsumption: function () {

            var that = this,
                controls = that.getControls(),
                objDataService = Session.DATASERVICE,
                objCustomer = Session.DATACUSTOMER,
                objRecharges = {};

            objRecharges.Telephone = objDataService.CellPhone;
            objRecharges.CustomerId = objCustomer.CustomerID;
            objRecharges.FlagPlatform = objDataService.FlagPlatform;
            objRecharges.strIdSession = Session.IDSESSION;
            objRecharges.Contract = Session.DATACUSTOMER.ContractID;
            objRecharges.StateLine = Session.DATASERVICE.StateLine;
            objRecharges.TypeCustomer = Session.DATACUSTOMER.CustomerType;


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonOrderConsumption',
                data: JSON.stringify(objRecharges),
                success: function (response) {
                    console.log(response);
                    that.setTableOrderConsumption(response.data);
                },
                error: function (msger) {

                    $.app.error({
                        id: 'idLoadingJanus',
                        message: msger,
                        click: function () {
                            that.getRecharges();
                        }
                    });
                }
            });

        },
        getControls: function () {
            return this.m_controls || {};
        },

        setTableOrderConsumption: function (data) {
            var controls = this.getControls(),
               tbl = controls.tblOrderConsumption;

            tbl.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
				, sScrollXInner: "100%"
                , autoWidth: true
                , data: data
                , select: 'single'
                , columns: [

                    { "data": "Code" },
                    { "data": "Description" }


                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1]
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


        setControls: function (value) {
            this.m_controls = value;
        }

    }

    $.fn.PostpaidOrderConsumption = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidOrderConsumption'),
                options = $.extend({}, $.fn.PostpaidOrderConsumption.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidOrderConsumption', data);
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

    $.fn.PostpaidOrderConsumption.defaults = {
    }

    $('#ContainerOrderConsumption', $('.modal:last')).PostpaidOrderConsumption();

})(jQuery);


