(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

             btnListPospaid: $('#btnlistpostpago')
            , btnListPrepaid: $('#btnlistprepago')

        })
    };

    Form.prototype = {

        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnListPospaid.addEvent(that, 'click', that.btnListPospaid_click);
            controls.btnListPrepaid.addEvent(that, 'click', that.btnListPrepaid_click);
            

            that.render();
        },
        render: function () {
            var that = this;
            that.loadCustomer();
       

        },
        applicativeRoute: window.location.href,
        loadCustomer: function () {

            
            var contPost = 0, contPre = 0;

            Session.CUSTOMERPRODUCT.ListOptionalObjectCustomer.forEach(function (item) {

                var strValue = item.Value;
                var strItemsValue = strValue.split('|');
              
                if (strItemsValue[2] == "BSCS") {
                    contPost += 1;
                }
                else {
                    contPre += 1;
                 } 
            });


            if (contPost == 0) {
                
                $('#btnlistpostpago').replaceWith('<span>No se encontró información</span>');
            }
            if (contPre == 0) {
               
                $('#btnlistprepago').replaceWith('<span>No se encontró información</span>');
            }
           

            if (Session.CUSTOMERPRODUCT.ListOptionalObjectCustomer.length > 0) {

                var strValue = Session.CUSTOMERPRODUCT.ListOptionalObjectCustomer[0].Value;
                var strItemsValue = strValue.split('|');

                if (strItemsValue[2] == "BSCS") {
                    $('#lblNombresPostpaid').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].Names);
                    $('#lblApellidosPostpaid').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].Surnames);
                } else {

             
                    $('#lblNombresPrepaid').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].Names);
                    $('#lblApellidosPrepaid').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].Surnames);
                }
           
            }

        },
        btnListPospaid_click: function () {

            var trHTML = '';
            Session.CUSTOMERPRODUCT.ListOptionalObjectCustomer.forEach(function (item) {
                var strValue = item.Value;
                var strItemsValue = strValue.split('|');

                if (strItemsValue[2] == "BSCS")
                {
                    trHTML += '<tr><td>' + strItemsValue[0] + ' ' + strItemsValue[1] +'</td></tr>';
                }
               
           });
       
           $('#updatebody').html(trHTML);

        },


        btnListPrepaid_click: function () {
            var trHTML = '';
            Session.CUSTOMERPRODUCT.ListOptionalObjectCustomer.forEach(function (item) {
                var strValue = item.Value;
                var strItemsValue = strValue.split('|');

                if (strItemsValue[2] != "BSCS") {
                    trHTML += '<tr><td>' + strItemsValue[0] + ' ' + strItemsValue[1] + '</td></tr>';
                }

            });

            $('#updatebody').html(trHTML);

        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta de Clientes",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('form'),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('form', data);
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

    $.fn.form.defaults = {
    }

})(jQuery);

$(document).ready(function () {

    $('#contenedor-listCustomerOptional').form();

});
