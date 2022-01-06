(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.CommonBannerModify.defaults, $element.data(), typeof options === 'object' && options);

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
            $('#contentBanner').CommonBanner('getBannerId');
        },  


        getModify: function (idBanner) {
            var that = this,
            controls = that.getControls(),
            objModify = {};

            objModify.strIdSession = Session.IDSESSION;
            objModify.ID_BANNER = idBanner;
            objModify.MENSAJE = controls.txtMessage.val();
            objModify.FECHA_VIGENCIA_INICIO = controls.txtStart.val() + ' ' + controls.txtHourStart.val() + ':' + controls.txtMinuteStart.val() + ' ' + controls.cmbPeriodStart.val();
            objModify.FECHA_VIGENCIA_FIN = controls.txtEnd.val() + ' ' + controls.txtHourEnd.val() + ':' + controls.txtMinuteEnd.val() + ' ' + controls.cmbPeriodEnd.val();
            objModify.ORDEN_PRIORIDAD = controls.cmbPriority.val();
            objModify.DATE = $("#txtDate").val();

            if (controls.chkIncludeCanceled.prop('checked')) {
                objModify.STATUS = "2";
            } else {
                objModify.STATUS = "1";
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url:  '~/Management/Banner/GetModify',
                data: JSON.stringify(objModify),
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
    $.fn.CommonBannerModify = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getModify'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('CommonBannerModify'),
                options = $.extend({}, $.fn.CommonBannerModify.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CommonBannerModify', data);
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

    $.fn.CommonBannerModify.defaults = {
    }


})(jQuery);

$(function () {
    $('#contentBannerModify').CommonBannerModify();
})
