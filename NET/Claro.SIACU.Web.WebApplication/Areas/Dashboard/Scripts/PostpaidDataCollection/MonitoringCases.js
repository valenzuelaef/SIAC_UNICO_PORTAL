(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidDetailAmountDispute.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            btnSearchDataMonitoringCases: $('#btnSearchDataMonitoringCases', $element),
            rbCaseId: $('#rbCaseId', $element),
            rbCustomerAccount: $('#rbCustomerAccount', $element),
            rbDateFromTo: $('#rbDateFromTo', $element),
            txtCaseId: $('#txtCaseId', $element),
            containerDateRange: $('#containerDateRange', $element),
            txtMonitoringCasesDateFrom: $('#txtMonitoringCasesDateFrom', $element),
            txtMonitoringCasesDateTo: $('#txtMonitoringCasesDateTo', $element),
            txtCustomerAccount: $('#txtCustomerAccount', $element),
            divDetailMonitoringCases: $('#divDetailMonitoringCases', $element),
            tblDetailAmountDispute: $('#tblDetailAmountDispute', $element),
            btnClean: $('#btnClean', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.btnSearchDataMonitoringCases.addEvent(that, 'click', that.btnSearchDataMonitoringCases_click);
            controls.btnClean.addEvent(that, 'click', that.btnClean_click);
            controls.rbCaseId.addEvent(that, 'click', that.rbCaseId_click);
            controls.rbCustomerAccount.addEvent(that, 'click', that.rbCustomerAccount_click);
            controls.rbDateFromTo.addEvent(that, 'click', that.rbDateFromTo_click);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });
            that.render();

            var fDateToDay = new Date();
            var fDatePrevious = new Date();

            if (Session.DATASERVICE.Application === 'POSTPAID') {
                fDateToDay = $.app.date.format(fDateToDay, { format: 'dd/mm/yy' });
                fDatePrevious = $.app.date.addMonth(fDatePrevious, -1, { format: 'dd/mm/yy' });
                controls.txtMonitoringCasesDateFrom.val(fDatePrevious);
                controls.txtMonitoringCasesDateTo.val(fDateToDay);

            }
            else {
                controls.txtMonitoringCasesDateFrom.val('');
                controls.txtMonitoringCasesDateTo.val('');
            };

        },
        render: function () {
            var that = this;
            that.getLoadDetailAmountDispute();
            that.cleanFilter();
        },
        caseNumber_click: function () {
            this.getControls().tblDetailAmountDispute.find('tbody').find('a[class="redirectProblemManagement"]').addEvent(this, 'click', this.redirectProblemManagement);
           
        },

        dataDetailAmountDispute: null,

        redirectProblemManagement: function (send, args) {
            var that = this;
            var dataRowTM = that.tblDetailAmountDispute.row($(send).parents('td')).data();
           // console.log(dataRowTM);

            var typeCaseNumber = dataRowTM[8];
            //console.log(typeCaseNumber);

            Session.CasoID = typeCaseNumber;
            Session.tipoApp = '';
            Session.EstadoForm='E';
            Session.tipoCasoInteraccion='1';
            Session.CasoInteraccionId = typeCaseNumber;
            Session.telefono = '';

            Session.flagClienteAntiguo = '';
            Session.opcion='';
            Session.contactoId='';
            Session.perfiles='';
            Session.alturaVentana='325';
            Session.anchoVentana='950';
            Session.tipocontacto = '';
          
                      
           $.redirect.GetParamsData("SU_REC_GPRO", "RECLAMOS");

            //alert("redireccion");
        },
        getLoadDetailAmountDispute: function () {
            var controls = this.getControls();

            this.tblDetailAmountDispute = controls.tblDetailAmountDispute.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true,
                columnDefs: [
                    {
                        targets: 8,
                        render: function (data, type, full, meta) {
                            return "<a class='redirectProblemManagement' href='#'><u>" + data + "</u></a>";

                        }
                    }
                ],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });

            this.caseNumber_click();
        },
        btnSearchDataMonitoringCases_click: function (send, args) {
            var controls = this.getControls();

            var rbCaseIdChecked = controls.rbCaseId.is(":checked");
            if (rbCaseIdChecked) {
                if (controls.txtCaseId.val() === "") {

                    showAlert('Debe ingresar un número de caso.', this.strTitleMessage);
                    return;
                }
            }
            var rbCustomerAccountChecked = controls.rbCustomerAccount.is(":checked");
            if (rbCustomerAccountChecked) {
                if (controls.txtCustomerAccount.val() === "") {

                    showAlert('Debe ingresar una cuenta cliente.', this.strTitleMessage);
                    return;
                }
            }
            var rbDateFromToChecked = controls.rbDateFromTo.is(":checked");
            if (rbDateFromToChecked) {
                if (controls.txtMonitoringCasesDateFrom.val() === "") {

                    showAlert('Debe ingresar una fecha desde.', this.strTitleMessage);
                    return;
                }
                if (controls.txtMonitoringCasesDateTo.val() === "") {
                    showAlert('Debe ingresar una fecha hasta.', this.strTitleMessage);

                    return;
                }
            }
            this.showMonitoringCases();
            controls.divDetailMonitoringCases.css("display", "block")
        },

        btnClean_click: function (send, args) {
            var controls = this.getControls();
            var rbCaseIdChecked = controls.rbCaseId.is(":checked");

            if (Session.DATASERVICE.Application === 'POSTPAID') {
                if (rbCaseIdChecked) {

                    controls.txtCaseId.val('');
                }
                var rbCustomerAccountChecked = controls.rbCustomerAccount.is(":checked");
                if (rbCustomerAccountChecked) {
                    controls.txtCustomerAccount.val('');

                }
                var rbDateFromToChecked = controls.rbDateFromTo.is(":checked");
                if (rbDateFromToChecked) {
                    controls.txtMonitoringCasesDateFrom.val('');

                    controls.txtMonitoringCasesDateTo.val('');
                }
                controls.divDetailMonitoringCases.css("display", "none")
            } else {
                controls.divDetailMonitoringCases.css("display", "none")

            }
        },


        rbCaseId_click: function (send, args) {

            this.validateRadioButton("CaseId");
        },
        rbCustomerAccount_click: function (send, args) {
            this.validateRadioButton("CustomerAccount");
        },
        rbDateFromTo_click: function (send, args) {
            this.validateRadioButton("DateFromTo");
        },
        getControls: function () {
            return this.m_controls || {};
        },
        showMonitoringCases: function () {
            var controls = this.getControls();

            var objDetailMonitoringCases = {

                strCaseId: controls.txtCaseId.val(),
                strCustomerAccount: controls.txtCustomerAccount.val(),
                strDateFrom: controls.txtMonitoringCasesDateFrom.val(),
                strDateTo: controls.txtMonitoringCasesDateTo.val(),
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                dataType: 'html',
                url: '~/Dashboard/PostpaidDataCollection/DetailMonitoringCases',
                cache: false,
                container: controls.divDetailMonitoringCases,
                data: objDetailMonitoringCases,
                error: function (ex) {
                    controls.divDetailMonitoringCases.html('<div align="center"> Error al cargar los datos ... </div>');
                }
            });
        },
        validateRadioButton: function (opt) {
            var controls = this.getControls();
            this.cleanFilter();

            if (opt === "CaseId") {

                this.Numbers();
                controls.rbCustomerAccount.removeAttr('checked');
                controls.rbDateFromTo.removeAttr('checked');
                controls.txtCaseId.removeAttr('disabled', 'disabled');
                controls.txtCaseId.focus();
            }
            if (opt === "CustomerAccount") {

                this.NumbersPoint();
                controls.rbCaseId.removeAttr('checked');
                controls.rbDateFromTo.removeAttr('checked');
                controls.txtCustomerAccount.removeAttr('disabled', 'disabled');
                controls.txtCustomerAccount.focus();
            }
            if (opt === "DateFromTo") {

                controls.rbCaseId.removeAttr('checked');
                controls.rbCustomerAccount.removeAttr('checked');
                controls.txtMonitoringCasesDateFrom.removeAttr('disabled', 'disabled');
                controls.txtMonitoringCasesDateTo.removeAttr('disabled', 'disabled');
                controls.txtMonitoringCasesDateFrom.attr('readonly', true);
                controls.txtMonitoringCasesDateTo.attr('readonly', true);

            }
            controls.btnSearchDataMonitoringCases.removeAttr('disabled', 'disabled');
        },
        cleanFilter: function () {
            var controls = this.getControls();

            controls.btnSearchDataMonitoringCases.attr('disabled', 'disabled');
            controls.txtCaseId.attr('disabled', 'disabled');
            controls.txtCaseId.val('');
            controls.txtCustomerAccount.val('');
            controls.txtCustomerAccount.attr('disabled', 'disabled');
            controls.txtMonitoringCasesDateFrom.attr('disabled', 'disabled');
            controls.txtMonitoringCasesDateTo.attr('disabled', 'disabled');
            controls.txtMonitoringCasesDateFrom.val('');
            controls.txtMonitoringCasesDateTo.val('');

        },
        strTitleMessage: "Detalle de Monto en Disputa",
        setControls: function (value) {
            this.m_controls = value;
        },


        Numbers: function () {
            var controls = this.getControls();
            $.onlyNumbers(controls.txtCaseId);
        },

        NumbersPoint: function () {
            var controls = this.getControls();
            $.onlyNumbersPoint(controls.txtCustomerAccount);
        }

    };
    $.fn.FormPostpaidDetailAmountDispute = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidDetailAmountDispute'),
                options = $.extend({}, $.fn.FormPostpaidDetailAmountDispute.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidDetailAmountDispute', data);
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
    $.fn.FormPostpaidDetailAmountDispute.defaults = {
    }
    $('#PostpaidDetailAmountDispute', $('.modal:last')).FormPostpaidDetailAmountDispute();
})(jQuery);

