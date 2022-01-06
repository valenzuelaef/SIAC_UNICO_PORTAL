(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.CommonBannerNew.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtMessage: $('#txtMessage', $element)
            , cmbPriority: $('#cmbPriority', $element)
            , txtStart: $('#txtStart', $element)
            , txtHourStart: $('#txtHourStart', $element)
            , txtMinuteStart: $('#txtMinuteStart', $element)
            , cmbPeriodStart: $('#cmbPeriodStart', $element)
            , txtEnd: $('#txtEnd', $element)
            , txtHourEnd: $('#txtHourEnd', $element)
            , txtMinuteEnd: $('#txtMinuteEnd', $element)
            , cmbPeriodEnd: $('#cmbPeriodEnd', $element)
            , chkIncludeCanceled: $('#chkIncludeCanceled', $element)

        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.txtStart.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtEnd.datepicker({ format: 'dd/mm/yyyy', });
            that.render();

        },

        render: function () {

        },

        createBanner: function () {
            var that = this,

            controls = that.getControls(),
            objCreate = {};

            objCreate.strIdSession = Session.IDSESSION;
            objCreate.ID_BANNER = 0;
            objCreate.MENSAJE = controls.txtMessage.val();
            objCreate.FECHA_VIGENCIA_INICIO = controls.txtStart.val() + ' ' + controls.txtHourStart.val() + ':' + controls.txtMinuteStart.val() + ' ' + controls.cmbPeriodStart.val();
            objCreate.FECHA_VIGENCIA_FIN = controls.txtEnd.val() + ' ' + controls.txtHourEnd.val() + ':' + controls.txtMinuteEnd.val() + ' ' + controls.cmbPeriodEnd.val();
            objCreate.ORDEN_PRIORIDAD = controls.cmbPriority.val();
            objCreate.DATE = $("#txtDate").val();

            if (controls.chkIncludeCanceled.prop('checked')) {
                objCreate.STATUS = "2";
            } else {
                objCreate.STATUS = "1";
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url:  '~/Management/Banner/GetCreate',
                data: JSON.stringify(objCreate),
                success: function (response) {
                    
                    $('#contentBanner').CommonBanner('createTableBanner', response);
                },
                error: function (ex) {
                    showAlert('vuelva a intentarlo mas tarde...');
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
    $.fn.CommonBannerNew = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['createBanner'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('CommonBannerNew'),
                options = $.extend({}, $.fn.CommonBannerNew.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CommonBannerNew', data);
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

    $.fn.CommonBannerNew.defaults = {
    }


})(jQuery);

$(function () {
    $('#contentBannerNew').CommonBannerNew();
})
