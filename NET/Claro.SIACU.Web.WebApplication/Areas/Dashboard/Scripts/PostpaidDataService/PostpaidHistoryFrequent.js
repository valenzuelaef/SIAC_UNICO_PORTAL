(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidFrequents.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblNumberPhone: $('#lblNumberPhone', $element)
            , lblCodeCustomer: $('#lblCodeCustomer', $element)
            , lblCustomer: $('#lblCustomer', $element)
            , tblPetitions: $('#tblPetitions', $element)
            , errorPetitions: $('#errorPetitions', $element)
            , btnExportHistoryFrequent: $('#btnExportHistoryFrequent')
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            this.getControls().btnExportHistoryFrequent.prop('disabled', true);
            that.render();
            $('#lblCodeCustomer').after(Session.DATACUSTOMER.Account);
            $('#lblCustomer').after(Session.DATACUSTOMER.BusinessName);    
        },
        render: function () {
            var that = this;
            that.getPetitions();
        },


        createTablePetitions: function (response) {
            var that = this,
            controls = that.getControls();


            controls.lblNumberPhone.after(Session.DATASERVICE.CellPhone);
            controls.tblPetitions.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , select : 'single'
                , data: response.data.HistoricalStriations
                , scrollY: 300
                , scrollCollapse: true
                , order: [[0, 'desc']]
                , columns: [
                    { "data": "Plan" },
                    { "data": "Modification" },
                    { "data": "User" },
                    { "data": "Date" },
                    { "data": "Destination" },
                    { "data": "Description" },
                    { "data": "Factor" },

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
          
            if (controls.tblPetitions.DataTable().rows().count() == 0) { controls.btnExportHistoryFrequent.prop('disabled', true); } else { controls.btnExportHistoryFrequent.prop('disabled', false); }
            
        },

        getPetitions: function (petitionType) {
            var that = this,
            controls = that.getControls();
           

            var objPetitions = {};
            objPetitions.Application = Session.ORIGINTYPE;
            objPetitions.ContractID = Session.DATACUSTOMER.ContractID;
            objPetitions.Telephone = Session.TELEPHONE;
            
            objPetitions.strIdSession = Session.IDSESSION;
            objPetitions.CustomerId = Session.DATACUSTOMER.CustomerID;
            objPetitions.Customer = Session.DATACUSTOMER.BusinessName;
            
            
            controls.errorPetitions.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblPetitions.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetHistoricalStriations',
                data: JSON.stringify(objPetitions),
                success: function (response) {

                    $('#lblNumberPhone').after(response.data.TelephoneNumber);
                    controls.tblPetitions.find('tbody').html('');
                   
                    that.createTablePetitions(response);
                },
                error: function (msger) {
                    $.app.error({
                        id: 'errorPetitions',
                        message: msger,
                        click: function () {
                            that.getPetitions();
                        }
                    });
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
    }

    $.fn.PostPaidFrequents = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getPetitions'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidFrequents'),
                options = $.extend({}, $.fn.PostPaidFrequents.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidFrequents', data);
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

    $.fn.PostPaidFrequents.defaults = {
    }

    $('#contenedorPeticiones', $('.modal:last')).PostPaidFrequents();

})(jQuery);