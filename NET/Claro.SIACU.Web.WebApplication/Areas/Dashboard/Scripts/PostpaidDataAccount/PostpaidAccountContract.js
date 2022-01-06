(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountContract.defaults, $element.data(), typeof options == 'object' && options);

        this.setControls({
            form: $element
            , txtDateStart: $('#txtDateStart', $element)
            , txtDateEnd: $('#txtDateEnd', $element)
            , btnSearchContract: $('#btnSearchContract', $element)
            , cboTypeSuspended: $('#cboTypeSuspended', $element)
            , tblSuspendedContractList: $('#tblSuspendedContractList', $element)
            , containerDateRange: $('#containerDateRange', $element)
            , LoadingContractList: $('#LoadingContractList', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            var fDateToDay = new Date(), fDateBefore = new Date();
            fDateToDay = $.app.date.format(fDateToDay,  { format: 'dd/mm/yy' });
            fDateBefore = $.app.date.addMonth(fDateBefore, -1, { format: 'dd/mm/yy' });

            controls.txtDateStart.val(fDateBefore);
            controls.txtDateEnd.val(fDateToDay);
            controls.txtDateStart.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnSearchContract.addEvent(that, 'click', that.btnSearchContract_click);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });
        },
         
        btnSearchContract_click: function () {
            var controls = this.getControls();
            var strReasonID = controls.cboTypeSuspended.val();
            var strFechaIni = controls.txtDateStart.val();
            var strFechaFin = controls.txtDateEnd.val();
            controls.LoadingContractList.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblSuspendedContractList.find("tbody").html('<tr><td colspan="10" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataAccount/GetAccountSuspendedContractList',
                data: JSON.stringify({ strIdSession: Session.IDSESSION, strReasonID: strReasonID, strFechaIni: strFechaIni, strFechaFin: strFechaFin }),
                success: function (response) {
                    controls.tblSuspendedContractList.find('tbody').html('');
                    controls.tblSuspendedContractList.DataTable({
                        "pagingType": "full_numbers",
                        "scrollY": "300px",
                        "scrollCollapse": true,
                        "info": false,
                        "select": 'single',
                        "ordering": false,
                        "paging": false,
                        "pageLength": 50,
                        "searching": false,
                        "destroy": true,
                        "data": response.data.lstAccountSuspendedContract,
                        "columns": [
                               { "data": "Number" },
                               { "data": "ClienteId" },
                               { "data": "CustomerType" },
                               { "data": "BusinessName" },
                               { "data": "RatePlan" },
                               { "data": "Telephone" },
                               { "data": "SimCard" },
                               { "data": "DateActivation" },
                               { "data": "DateSuspended" },
                                { "data": "ReasonSuspended" },
                                { "data": "User" },
                        ],
                        "language": {
                            "lengthMenu": "  ",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)",
                            "oPaginate": {
                                "sFirst": "Primero",
                                "sPrevious": "Anterior",
                                "sNext": "Siguiente",
                                "sLast": "Último"
                            },
                        }
                    });


                },
                error: function (msger) {
                    controls.tblSuspendedContractList.find('tbody').html('');
                    $.app.error({
                        id: 'LoadingContractList',
                        message: msger
                    });
                }
            });
        },
        render: function () {
        }, 
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'

    };

    $.fn.formAccountContract = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountContract'),
                options = $.extend({}, $.fn.formAccountContract.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountContract', data);
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

    $.fn.formAccountContract.defaults = {
    }

    $('#ContentAccountSuspendedContract', $('.modal:last')).formAccountContract();

})(jQuery);
