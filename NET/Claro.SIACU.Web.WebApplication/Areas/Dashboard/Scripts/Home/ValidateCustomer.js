(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , user: $('#txtUsuario', $element)
            , pass: $('#txtPass', $element)
            , btnValidate: $('#btnValidar', $element)
        })
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnValidate.addEvent(that, 'click', that.ValidateQuery_click);
            that.render();
        },

        render: function () {
        },

        ValidateQuery_click: function () {
            var that = this, controls = that.getControls();
            if (controls.user.val() != "" && controls.pass.val() != "") {
                var data = { strIdSession: Session.IDSESSION, strCustomer: controls.user.val(), strPass: controls.pass.val() };
               
                $.app.ajax({
                    type: "POST",
                    dataType: "json",
                    url: '~/Dashboard/Home/ValidateQuery',
                    data: data,
                    success: function (result) {
                        


                        window.opener.$('#btnSearchDataCall').data('dat', data);
                        window.opener.$('#contenedorVisualizeCallDetail').VisualizeCallPrepaid('showDetailCall', data);
                        window.close();
                    }
                });

                
            }
        },

        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Detalle de Productos",
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

    $('#contenedor-validate').form();

})(jQuery);