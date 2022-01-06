(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidConsumptionBalanceHFCLTE.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblTelephone: $('#lblTelephone', $element)
            , tblConsumptionBalanceHFCLTE: $('#tblConsumptionBalanceHFCLTE', $element)
            , errorConsumptionBalanceHFCLTE: $('#errorConsumptionBalanceHFCLTE', $element)
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

            that.setCustomerId();
            that.getTelephoneType();
            that.createTableBalance('');
        },
        createComboTelephoneType: function (response) {
            var that = this,
                controls = that.getControls(),
                sel = $('<select>', {
                    id: 'cboTelephoneType',
                    name: 'cboTelephoneType',
                    class: 'form-control'
                }, controls.form);
           
            $.extend(controls, { cboTelephoneType: sel });

            sel.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            if (response.data.TelephoneTypes != null) {
                $.each(response.data.TelephoneTypes, function (index, value) {
                    sel.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }

            controls.lblTelephone.after(sel);
        },
        createTableBalance: function (response) {
            var that = this,
            controls = that.getControls();

            controls.tblConsumptionBalanceHFCLTE.DataTable({
                "info": false
                , "paging": false
                , "destroy": true
                , "searching": false
                , "select" : 'single'
                , "data": response.data
                , "scrollY": 400
                , "scrollCollapse": true
                ,"scrollX": true
                ,"sScrollXInner": "100%"
                ,"autoWidth": true
                , "columns": [
                    { "data": null },
                    { "data": "WalletDescription" },
                    { "data": "Consumo" },
                    { "data": "Balances" }
                ]
                , "columnDefs": [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    }
                ]
                , "rowCallback": function (row, data, iDisplayIndex) {
                    var index = iDisplayIndex + 1;
                    $('td:eq(0)', row).html(index);
                }
                , "language": {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        changeTelephoneType: function () {
            var controls = this.getControls();

            controls.cboTelephoneType.addEvent(this, 'change', this.cboTelephoneType_change);
        },
        cboTelephoneType_change: function (sender, args) {
            var that = this,
                controls = that.getControls(),

            objConsumptionBalance = {};

            objConsumptionBalance.Telephone = sender.val();
            objConsumptionBalance.CustomerType = Session.DATACUSTOMER.CustomerType;
            objConsumptionBalance.strIdSession = Session.IDSESSION;

            controls.errorConsumptionBalanceHFCLTE.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblConsumptionBalanceHFCLTE.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            
            //INICIATIVA 616
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
                var request = {
                    strIdSession: Session.IDSESSION,
                    msisdn: sender.val(),
                    coIdPub: Session.DATACUSTOMER.coIdPub
                };

                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '~/Dashboard/PostpaidDataService/GetBalanceFijaTobe',
                    data: JSON.stringify(request),
                    success: function (response) {
                        controls.tblConsumptionBalanceHFCLTE.find('tbody').html('');
                        that.createTableBalance(response);
                    },
                    error: function (msger) {
                        controls.tblConsumptionBalanceHFCLTE.find('tbody').html('');
                        $.app.error({
                            id: 'errorConsumptionBalanceHFCLTE',
                            message: msger,
                            click: function () {
                                that.cboTelephoneType_change(sender, args);
                            }
                        });
                    }
                });
            }
            else {
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '~/Dashboard/PostpaidDataService/GetBalance',
                    data: JSON.stringify(objConsumptionBalance),
                    success: function (response) {
                        controls.tblConsumptionBalanceHFCLTE.find('tbody').html('');
                        that.createTableBalance(response);
                    },
                    error: function (msger) {
                        controls.tblConsumptionBalanceHFCLTE.find('tbody').html('');
                        $.app.error({
                            id: 'errorConsumptionBalanceHFCLTE',
                            message: msger,
                            click: function () {
                                that.cboTelephoneType_change(sender, args);
                            }
                        });
                    }
                });
            }


            
        },
        getTelephoneType: function () {
            var that = this,
                objTelephoneType = {};

            //RPB - Proyecto One Fija

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
                var response = {
                    data: {
                        TelephoneTypes: [{
                            Description: Session.DATASERVICE.CellPhone
                        }]
                    }
                };

                that.createComboTelephoneType(response);
                that.changeTelephoneType();
            } else {
                objTelephoneType.strIdSession = Session.IDSESSION;
                objTelephoneType.ContractId = Session.DATACUSTOMER.ContractID;
                objTelephoneType.Application = Session.ORIGINTYPE;

                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '~/Dashboard/PostpaidDataService/GetTelephoneType',
                    data: JSON.stringify(objTelephoneType),
                    success: function (response) {
                        that.createComboTelephoneType(response);
                        that.changeTelephoneType();
                    },
                    error: function (msger) {
                        $.app.error({
                            id: 'containerConsumptionBalanceHFCLTE',
                            message: msger
                        });
                    }
                });
            }

        },
        getControls: function () {
            return this.m_controls || {};
        },
        setCustomerId: function () {
            var controls = this.getControls();

            controls.lblCustomerId.after(Session.DATACUSTOMER.CustomerID);
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostpaidConsumptionBalanceHFCLTE = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidConsumptionBalanceHFCLTE'),
                options = $.extend({}, $.fn.PostpaidConsumptionBalanceHFCLTE.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidConsumptionBalanceHFCLTE', data);
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

    $.fn.PostpaidConsumptionBalanceHFCLTE.defaults = {
    }

    $('#containerConsumptionBalanceHFCLTE', $('.modal:last')).PostpaidConsumptionBalanceHFCLTE();

})(jQuery);