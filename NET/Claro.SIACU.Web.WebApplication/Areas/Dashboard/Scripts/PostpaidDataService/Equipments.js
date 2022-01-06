//tbHistoricoSIM
(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPospaidEquipments.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbEquipmentsPost: $('#tbEquipmentsPost', $element)
             , lblPost_EquipmentsCustomerId: $('#lblPost_EquipmentsCustomerId', $element)
             , lblPost_EquipmentsCuenta: $('#lblPost_EquipmentsCuenta', $element)
             , lblPost_EquipmentsCliente: $('#lblPost_EquipmentsCliente', $element)
             , lblPost_EquipmentsContacto: $('#lblPost_EquipmentsContacto', $element)

        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            controls.tbEquipmentsPost.DataTable({
                "info": false,
                "select" : 'single',
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
           
            that.setCustomer();
        },
        setCustomer:function(){
            var controls = this.getControls();
            var oCustomer = Session.DATACUSTOMER;
            controls.lblPost_EquipmentsCustomerId.text(oCustomer.CustomerID);
            controls.lblPost_EquipmentsCuenta.text(oCustomer.Account);
            controls.lblPost_EquipmentsCliente.text(oCustomer.BusinessName);
            controls.lblPost_EquipmentsContacto.text(oCustomer.FullName);
            
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };


    $.fn.FormPospaidEquipments = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPospaidEquipments'),
                options = $.extend({}, $.fn.FormPospaidEquipments.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPospaidEquipments', data);
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



    $.fn.FormPospaidEquipments.defaults = {
    }

    $('#PostpaidConsultaEquipmentsList', $('.modal:last')).FormPospaidEquipments()

})(jQuery);