(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.CommonBanner.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtDate: $('#txtDate', $element)
            , btnSearchBanner: $('#btnSearchBanner', $element)
            , txtHour: $('#txtHour', $element)
            , txtMinute: $('#txtMinute', $element)
            , cmbPeriod: $('#cmbPeriod', $element)
            , chkIncludeCanceled: $('#chkIncludeCanceled', $element)
            , btnNewBanner: $('#btnNewBanner', $element)
            , btnModifyBanner: $('#btnModifyBanner', $element)
            , btnDeactivateBanner: $('#btnDeactivateBanner', $element)
            , tblBanner: $('#tblBanner', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            var f = new Date();
            var dateHoy = f.getDate() < 10 ? '0' + f.getDate() : f.getDate(),
                dateMonth = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1);

            var fDateToDay = dateHoy + "/" + dateMonth + "/" + f.getFullYear();
            controls.txtDate.val(fDateToDay);
            controls.txtDate.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnSearchBanner.addEvent(that, 'click', that.btnSearchBanner_click);
            controls.btnNewBanner.addEvent(that, 'click', that.btnNewBanner_click);
            controls.btnModifyBanner.addEvent(that, 'click', that.btnModifyBanner_click);
            controls.btnDeactivateBanner.addEvent(that, 'click', that.btnDeactivateBanner_click);
            that.render();

        },

        render: function () {},
        btnNewBanner_click: function () {
            $.window.open({
                modal: false,
                title: "Ingreso de Mensajes",
                url: '~/Management/Banner/Create',
                width: 1024,
                height: 600,
                buttons: {
                    Aceptar: {
                        click: function () {
                            $('#contentBannerNew').CommonBannerNew('createBanner');
                        }
                    }
                }
            });
        },

        btnModifyBanner_click: function () {
            var that = this;
            $.window.open({
                modal: false,
                title: "Actualización de Mensajes",
                url: '~/Management/Banner/Modify',
                width: 1024,
                height: 600,
                buttons: {
                    Aceptar: {
                        click: function () {
                            $('#contentBannerModify').CommonBannerModify('getModify', that.idBanner);
                            this.close();
                        }
                    }
                }
            });
        },

        getBannerId: function () {
            var that = this,
            objBannerId = {};
            objBannerId.strIdSession = Session.IDSESSION;
            objBannerId.intIdBanner = this.idBanner;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url:  '~/Management/Banner/GetBannerId',
                data: JSON.stringify(objBannerId),
                success: function (response) {
                    if (response.data.Banner);
                    that.setDataBanner(response.data.Banner);

                },
                error: function (ex) {
                    showAlert('vuelva a intentarlo mas tarde...');
                }
            });
        },
        setDataBanner: function (objBanner) {

            var aDateValidityStart = $.formatDate(objBanner.dateValidityStart, { format: 'dd/mm/yy hh:mm a' }).split(' '),
                aDateValidityEnd = $.formatDate(objBanner.dateValidityEnd, { format: 'dd/mm/yy hh:mm a' }).split(' '),
                aHourMinuteStart = aDateValidityStart[1].split(':'),
                aHourMinuteEnd = aDateValidityEnd[1].split(':');

            $('#txtMessage').val(objBanner.message);
            $('#cmbPriority').val(objBanner.priorityOrder);
            $('#txtStart').val(aDateValidityStart[0]);
            $('#txtHourStart').val(aHourMinuteStart[0]);
            $('#txtMinuteStart').val(aHourMinuteStart[1]);
            $('#cmbPeriodStart').val(aDateValidityStart[2]);
            $('#txtEnd').val(aDateValidityEnd[0]);
            $('#txtHourEnd').val(aHourMinuteEnd[0]);
            $('#txtMinuteEnd').val(aHourMinuteEnd[1]);
            $('#cmbPeriodEnd').val(aDateValidityEnd[2]);

        },
        btnDeactivateBanner_click: function () {
            var that = this;

            $.window.open({
                modal: true,
                title: "Desactivación de Mensajes",
                url: '~/Management/Banner/Deactivate',
                width: 1024,
                height: 600,
                buttons: {
                    Aceptar: {
                        click: function () {
                            that.btnDeactivateMessage();
                        }
                    }
                }
            });
        },

        btnDeactivateMessage: function () {
            var that = this;

            $.window.open({
                modal: false,
                title: "",
                text: 'El mensaje se va a desactivar, ¿desea continua?',
                url: "",
                width: 1024,
                height: 600,
                buttons: {
                    Aceptar: {
                        click: function () {
                            $('#contentBannerDeactivate').CommonBannerDeactivate('getDeactivate', that.idBanner);
                            this.close();
                        }
                    },
                    Cancelar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },

        changeRadioButton: function () {
            var that = this,
                controls = that.getControls();

            $("input[name=rbBanner]:radio").on('change', function () {
                that.idBanner = $(this).data('banner');
                controls.btnModifyBanner.prop("disabled", false),
                controls.btnDeactivateBanner.prop("disabled", false);
            })
        },

        btnSearchBanner_click: function () {
            var that = this,

                controls = that.getControls(),
                objBanner = {};

            if (controls.chkIncludeCanceled.prop('checked')) {
                objBanner.status = "2";
            } else {
                objBanner.status = "1";
            }

            objBanner.date = controls.txtDate.val();
            objBanner.strIdSession = Session.IDSESSION;


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url:  '~/Management/Banner/GetBanner',
                data: JSON.stringify(objBanner),
                complete: function () {
                    that.changeRadioButton();
                },
                success: function (response) {
                    that.createTableBanner(response);
                },
                error: function (ex) {
                    showAlert('vuelva a intentarlo mas tarde...');
                }
            });
        },

        createTableBanner: function (response) {
            var controls = this.getControls();

            controls.tblBanner.DataTable(
                {
                    info: false
                    , paging: false
                    , searching: false
                    , select: 'single'
                    , destroy: true
                    , data: response.data.salida
                    , columns: [
                        { "data": null },
                        { "data": "message" },
                        { "data": "priorityOrder" },
                        { "data": "dateValidityStart" },
                        { "data": "dateValidityEnd" },
                        { "data": "status" },
                        { "data": "loginRegistry" },
                        { "data": "loginModification" }

                    ]
                , columnDefs: [
                     {
                         targets: [3, 4],
                         render: function (data, type, full, meta) {
                             return $.formatDate(data);
                         }
                     },
                     {
                         targets: 0,
                         render: function (data, type, full, meta) {
                             var $rb = $('<input>', {
                                 type: 'radio'
                             , name: 'rbBanner'
                               , 'data-banner': full.idBanner
                             });

                             return $rb[0].outerHTML;
                         }
                     },
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4, 5, 6, 7]
                    }
                ],
                    rowCallback: function (row, data, iDisplayIndex) {
                        var index = iDisplayIndex + 1;
                        $("#TotalRegistros").html("Se encontraron " + index + " registros.");
                    }

                }
            );
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }


    $.fn.CommonBanner = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getBannerId', 'createTableBanner'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('CommonBanner'),
                options = $.extend({}, $.fn.CommonBanner.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CommonBanner', data);
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

    $.fn.CommonBanner.defaults = {
    }


})(jQuery);

$(function () {
    $('#contentBanner').CommonBanner();
})
