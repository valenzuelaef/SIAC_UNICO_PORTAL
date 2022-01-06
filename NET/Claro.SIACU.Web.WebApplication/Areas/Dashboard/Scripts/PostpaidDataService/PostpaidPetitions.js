(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidPetitions.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblNumberPhone: $('#lblNumberPhone', $element)
            , lblPetitionsType: $('#lblPetitionsType', $element)
            , tblPetitions: $('#tblPetitions', $element)
            , tblPetitionsHFC: $('#tblPetitionsHFC', $element)
            , errorPetitions: $('#errorPetitions', $element)
            , errorPetitionsHFC: $('#errorPetitionsHFC', $element)
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

            that.getPetitionsType();
            that.getPetitions();

            if (Session.DATACUSTOMER.Application == 'POSTPAID')
                that.setCellPhone();
            else
                that.changeCellPhoneWithCustomerId();

        },
        changeCellPhoneWithCustomerId: function () {
            var that = this,
                controls = that.getControls();

            controls.lblNumberPhone.text('Customer ID:');
            controls.lblNumberPhone.after(Session.DATACUSTOMER.CustomerID);
        },
        createComboPetitionsType: function (response) {
            var that = this,
                controls = that.getControls(),
                sel = $('<select>', {
                    id: 'cboPetitionsType',
                    name: 'cboPetitionsType',
                    class: 'form-control'
                }, controls.form);

            $.extend(controls, { cboPetitionsType: sel });

            if (response.data.PetitionTypes == null)
                sel.append($('<option>', { value: '', html: 'Todos' }));
            else {
                $.each(response.data.PetitionTypes, function (index, value) {
                    sel.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }

            controls.lblPetitionsType.after(sel);
        },
        createTablePetitions: function (response) {
            var that = this,
            controls = that.getControls();
            console.log(response.data.Petitions);
            controls.tblPetitions.DataTable({
                info: false
                , ordering: false
                , paging: false
                , searching: false
                , destroy: true
                , select: 'single'
                , data: response.data.Petitions
                , scrollY: 300
                , scrollCollapse: true
                , columns: [
                    { "data": "DateRequest" },
                    { "data": null },
                    { "data": "DateRequest" },
                    { "data": "ExpirationDate" },
                    { "data": "State" },
                    { "data": "Action" },
                    { "data": "User" },
                    { "data": "OrderID" }
                ]
                , columnDefs: [
                    {
                        visible: false,
                        targets: 0
                    },

                    {
                        className: "text-center",
                        targets: [1, 2, 3, 4]
                    }
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
            });
        },

        createTablePetitionsHFC: function (response) {
            var that = this,
            controls = that.getControls();

            controls.tblPetitionsHFC.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , select: 'single'
                , data: response.data.Petitions
                , scrollY: 300
                , scrollCollapse: true
                , order: [[0, 'desc']]
                , columns: [
                    { "data": "DateRequest" },
                    { "data": null },
                    { "data": "Action" },
                    { "data": "State" },
                    { "data": "DateRequest" },
                    { "data": "ExpirationDate" },
                    { "data": "User" },
                    { "data": "OrderID" }
                ]
                , columnDefs: [
                    { visible: false, targets: 0 },
                    {
                        className: "text-center",
                        targets: [1, 3, 4, 5]
                    }
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
            });
        },
        changePetitionsType: function () {
            var that = this,
                controls = this.getControls();

            controls.cboPetitionsType.addEvent(that, 'change', that.cboPetitionsType_change);
        },
        cboPetitionsType_change: function (sender, args) {
            this.getPetitions(sender.val())
        },
        getPetitionsType: function () {
            var that = this,
                objPetitionsType = {};

            objPetitionsType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetPetitionsType',
                data: JSON.stringify(objPetitionsType),
                complete: function () {
                    that.changePetitionsType();
                },
                success: function (response) {
                    that.createComboPetitionsType(response);
                }
            });
        },
        getPetitions: function (petitionType) {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objPetitions = {};
                objPetitions.ContractId = oCustomer.ContractID;
                objPetitions.PetitionType = petitionType;
                objPetitions.strIdSession = Session.IDSESSION;
                objPetitions.flagConvivencia = Session.DATACUSTOMER.flagConvivencia;
                objPetitions.flagPlataforma = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
                objPetitions.coIdPub = Session.DATACUSTOMER.coIdPub;
                objPetitions.Application = Session.DATACUSTOMER.Application;
                controls.errorPetitions.html('');
                controls.errorPetitionsHFC.html('');

            var stUrlLogo = "/Images/loading2.gif";
            controls.tblPetitions.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            controls.tblPetitionsHFC.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetPetitions',
                data: JSON.stringify(objPetitions),
                success: function (response) {
                    if (Session.DATACUSTOMER.Application == 'POSTPAID') {
                        controls.tblPetitionsHFC.css("display", "none");
                        controls.tblPetitions.find('tbody').html('');
                        that.createTablePetitions(response);
                    }
                    else {
                        controls.tblPetitions.css("display", "none");
                        controls.tblPetitionsHFC.find('tbody').html('');
                        that.createTablePetitionsHFC(response);
                    }

                },
                error: function (msger) {
                    if (Session.DATACUSTOMER.Application == 'POSTPAID') {
                        controls.tblPetitions.find('tbody').html('').html('<tr><td colspan="8" align="center"><div id="errorPetitions"></div></td></tr>');
                        $.app.error({
                            id: 'errorPetitions',
                            message: msger
                        });
                    }
                    else {
                        controls.tblPetitionsHFC.find('tbody').html('').html('<tr><td colspan="8" align="center"><div id="errorPetitionsHFC"></div></td></tr>');
                        error({
                            id: 'errorPetitionsHFC',
                            message: msger
                        });
                    }
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setCellPhone: function () {
            var that = this,
                controls = that.getControls(),
                oDataService = Session.DATASERVICE;

            controls.lblNumberPhone.after(oDataService.CellPhone);
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidPetitions = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getPetitions'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidPetitions'),
                options = $.extend({}, $.fn.PostPaidPetitions.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidPetitions', data);
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

    $.fn.PostPaidPetitions.defaults = {
    }

    $('#contenedorPeticiones', $('.modal:last')).PostPaidPetitions();

})(jQuery);