(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.VisualizeReloadPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

          , btnSearchDataReload: $('#btnSearchDataReload', $element)
          , divDetailVisualizeReload: $('#divDetailVisualizeReload', $element)
          , txtDetailReloadDateFrom: $('#txtDetailReloadDateFrom', $element)
          , txtDetailReloadDateTo: $('#txtDetailReloadDateTo', $element)
          , containerDateRange: $('#containerDateRange', $element)
          , btnClearDataReload: $('#btnClearDataReload', $element)
          , ddlTipoMov: $('#ddlTipoMov', $element)
          , ddlCredDev: $('#ddlCredDev', $element)
          , hidMovementType: $('#hidMovementType', $element)
          , hidcreditoDebito: $('#hidcreditoDebito', $element)
          , btnCopyRowDataReload: $('#btnCopyRowDataReload')


        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSearchDataReload.addEvent(that, 'click', that.btnSearchDataReload_click);
            controls.btnClearDataReload.addEvent(that, 'click', that.btnClearDataReload_click);
            controls.btnCopyRowDataReload.addEvent(that, 'click', that.btnCopyRowDataReload_click);
            that.render();
        },
        render: function () {
            this.dataCustomerVisulizeReload();
            this.getMovementType();
            this.getCredDevType();
            this.showDetailReload();
        },
        btnCopyRowDataReload_click: function () {
            var fn = function (str) {
                var $temp = $("<textarea>");
                $("body").append($temp);
                $temp.val(str).select();
                document.execCommand("copy");
                $temp.remove();
            };
            var strRow = '';
            $('.SelectCopy').each(function (i, e) {

                if ($(e).prop('checked') == true) {
                    var td = $(e).parent().parent().find('td');
                    td.splice(0, 1);
                    var str = "";
                    for (var n = 0; n <= td.length - 1; n++) {
                        str = str + " " + $(td[n]).html();

                    }
                    if (str != '') {
                        strRow = strRow + str + '\n';
                    }

                };
            });

            if (strRow != '') {
                fn(strRow);
            }
        },
        getMovementType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '~/Dashboard/PrepaidDataReload/GetMovementType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.ddlTipoMov
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description',
                            selectedValue: controls.hidMovementType.val()
                        });

                }
            });
        },

        getCredDevType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '~/Dashboard/PrepaidDataReload/GetCredDevType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.ddlCredDev
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description',
                            selectedValue: controls.hidcreditoDebito.val()
                        });
                }
            });
        },

        getEventType: function () {
            var that = this,
                controls = that.getControls(),
                objEventType = { strIdSession: Session.IDSESSION };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataReload/GetMovementType',
                data: JSON.stringify(objEventType),
                success: function (response) {
                    controls.btnTipoMov
                        .populateDropDown({
                            dataSource: response.data.EventTypes,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });
                }
            });
        },

        btnSearchDataReload_click: function () {
            var controls = this.getControls();

            if (controls.txtDetailReloadDateFrom.val() == '' || controls.txtDetailReloadDateTo.val() == '') {
                showAlert('Debe Ingresar una fecha de inicio o una fecha de fin ', 'Alerta');
            }
            else {
                this.showDetailReload();
            }
        },

        btnClearDataReload_click: function () {
            var controls = this.getControls();
            controls.txtDetailReloadDateFrom.val('');
            controls.txtDetailReloadDateTo.val('');
            
            $('#tblDetailVisualizeReload').DataTable().clear().draw();
        },

        dataCustomerVisulizeReload: function () {
            var controls = this.getControls();


            var fDateToDay = new Date(), fDatePrevious = new Date();
            fDateToDay = $.app.date.format(fDateToDay, { format: 'dd/mm/yy' });
            fDatePrevious = $.app.date.addMonth(fDatePrevious, -2, { format: 'dd/mm/yy' });

            controls.containerDateRange.datepicker({
                format: 'dd/mm/yyyy',
                startDate: fDatePrevious,
                endDate: fDateToDay,
                beforeShowDay: function (date) {
                    $.app.date.limitedDate(date);
                }
            });

        },
        showDetailReload: function () {
            var controls = this.getControls();
           
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PrepaidDataReload/DetailVisualizeReload',
                dataType: 'html',

                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: $("#hidDetReloadTelephone").val(), strDateFrom: $("#txtDetailReloadDateFrom").val(), strDateTo: $("#txtDetailReloadDateTo").val(), strMovementType: $('#ddlTipoMov option:selected').val(), strcreditoDebito: $('#ddlCredDev option:selected').val() },
                container: controls.divDetailVisualizeReload,
                error: function (ex) {
                    controls.divDetailVisualizeReload.showMessageErrorLoading($.app.const.messageErrorLoading);
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        }

    };

    $.fn.VisualizeReloadPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('VisualizeReloadPrepaid'),
                options = $.extend({}, $.fn.VisualizeReloadPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('VisualizeReloadPrepaid', data);
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

    $.fn.VisualizeReloadPrepaid.defaults = {
    }

    $('#contenedorVisualizeReload').VisualizeReloadPrepaid();

})(jQuery);
