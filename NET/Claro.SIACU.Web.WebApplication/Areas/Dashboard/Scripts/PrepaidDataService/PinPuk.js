(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {
        $.extend(this, $.fn.PrepaidPinPukService.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , txtTelefono: $('#txtTelefono', $element)
            , btnSearchPin: $('#btnSearchPin', $element)
            , btnClearPin: $('#btnClearPin', $element)
            , divPinPuk: $('#divPinPuk', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            controls.btnSearchPin.addEvent(that, 'click', that.btnSearchPin_click);
            controls.btnClearPin.addEvent(that, 'click', that.btnClearPin_click);

            controls.txtTelefono.keyup(function (e) {
                if (e.keyCode == 13) {
                    that.btnSearchPin_click();
                    return false;
                } else {
                    var value = controls.txtTelefono.val();
                    controls.txtTelefono.val((value + '').replace(/[^0-9]/g, ''));
                }
              
            });

            that.render();
        },
        render: function () {
            this.loadData();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        },
        loadData: function () {
            var controls = this.getControls();

            $.app.ajax({
                type: 'POST',
                dataType: 'html',
                url: '~/Dashboard/PrepaidDataService/PinkpukSearch',
                cache: false,
                container: controls.divPinPuk,
                data: { strIdSession: Session.IDSESSION, strTelephone: controls.txtTelefono.val() },                
                error: function (ex) {
                }
            });
        },
        btnSearchPin_click: function () {
            var that = this, controls = this.getControls();

            if (controls.txtTelefono.val() == '') {
               
                showAlert('Debe Ingresar un Número de Teléfono', 'Consulta');
                that.btnClearPin_click();
            }
            else if ((controls.txtTelefono.val().length < 7 || controls.txtTelefono.val().length > 9)
                      || (controls.txtTelefono.val().length == 9 && controls.txtTelefono.val().substr(0, 1) != '9')) {
               
                showAlert('Número de Teléfono Inválido.', 'Consulta');
                that.btnClearPin_click();
            } else {
                that.loadData();
            }

        },
        btnClearPin_click: function () {
            var controls = this.getControls();
            controls.txtTelefono.val('');
            controls.divPinPuk.html('');
            controls.txtTelefono.focus();
        }

    };

    $.fn.PrepaidPinPukService = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidPinPukService'),
                options = $.extend({}, $.fn.PrepaidPinPukService.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidPinPukService', data);
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

    $.fn.PrepaidPinPukService.defaults = {
    }

    $('#containerPinPuk', $('.modal:last')).PrepaidPinPukService();

})(jQuery);