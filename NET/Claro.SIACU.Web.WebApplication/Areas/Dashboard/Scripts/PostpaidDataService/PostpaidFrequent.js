(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidFrequents.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
         
            , tblPetitions: $('#tblPetitions', $element)
            , errorPetitions: $('#errorPetitions', $element)
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
            that.getPetitions();
        },
       
        
        createTablePetitions: function (response) {
            var that = this,
            controls = that.getControls();

            controls.tblPetitions.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , data: response.data.Striations
                , scrollY: 300
                , select : 'single'
                , scrollCollapse: true
                , order: [[0, 'desc']]
                , columns: [
                    { "data": "NumberThreesome" },
                    { "data": "Telephone" },
                    { "data": "Factor" },
                    { "data": "TypeDestination" },
                    { "data": "CodeTypeDestination" },
                  
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
       
      
        getPetitions: function (petitionType) {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objPetitions = {};

            objPetitions.ContractId = oCustomer.ContractID;
            objPetitions.PetitionType = petitionType;
            objPetitions.strIdSession = Session.IDSESSION;

            controls.errorPetitions.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblPetitions.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetStriations',
                data: JSON.stringify(objPetitions),
                success: function (response) {
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