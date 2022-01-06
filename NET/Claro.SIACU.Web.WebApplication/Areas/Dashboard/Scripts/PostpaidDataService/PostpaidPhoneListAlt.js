(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidPhoneListAlt.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , divPostListAltSearch: $('#divPostListAltSearch', $element)
            , lblPost_PhoneListContractId: $('#lblPost_PhoneListContractId', $element)
            , btnPost_TelephoneAltSearch: $('#btnPost_TelephoneAltSearch', $element)
            , btnPost_TelephoneAltHistory: $('#btnPost_TelephoneAltHistory', $element)
            , btnPost_TelephoneAltClear: $('#btnPost_TelephoneAltClear', $element)
            , tbPostListAltSearch: $('#tbPostListAltSearch', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnPost_TelephoneAltSearch.addEvent(that, 'click', that.TelephoneAltSearch);
            controls.btnPost_TelephoneAltClear.addEvent(that, 'click', that.TelephoneAltClear);
            controls.btnPost_TelephoneAltHistory.addEvent(that, 'click', that.TelephoneAltHistory);

            $.onlyNumbers(controls.lblPost_PhoneListContractId);
            that.getDataTable();
        },
        render: function () {

        },
        TelephoneAltSearch: function () {

            var that = this,
            controls = this.getControls();

            var strApplication = Session.ORIGINTYPESERVICE;
            var strContractID = controls.lblPost_PhoneListContractId.val();
            var strUrlView = '';


            if (strContractID === "") {
                showAlert('Digite un Contrato a ser buscado', 'Mensaje');
            }

            //INICIATIVA-616
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE") {
                strUrlView = '~/Dashboard/PostpaidDataService/PhoneListAltSearchTOBE';
                $.app.ajax({
                    type: 'POST',
                    url: strUrlView,
                    data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication, plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
                    success: function (response) {
                        that.getDataTable(response);
                    },
                    error: function (ex) {

                    }
                });
            } else {
                strUrlView = '~/Dashboard/PostpaidDataService/PhoneListAltSearch';
                $.app.ajax({
                    type: 'POST',
                    url: strUrlView,
                    data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication },
                    success: function (response) {
                        that.getDataTable(response);
                    },
                    error: function (ex) {

                    }
                });
            }

           

            

        },
        TelephoneAltClear: function () {
            var controls = this.getControls();
            controls.lblPost_PhoneListContractId.val('');
        },
        TelephoneAltHistory: function () {
            var strApplication = Session.ORIGINTYPESERVICE;
            var strContractID = Session.CONTRACTIDSERVICE;

            $.window.open({
                modal: false,
                title: "Historial de Lineas Desactivas",
                url: '~/Dashboard/PostpaidDataService/PhoneListAltHistory',
                data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication },
                width: 850,
                height: 500,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        getDataTable: function (dataPhoneList) {

            var controls = this.getControls();
            controls.tbPostListAltSearch.DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "searching": false,
                "data": dataPhoneList,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "emptyTable": "No existen datos"
                },
                "columns": [
                    { "data": null },
                    { "data": "CellPhone" },
                    { "data": "StateLine" },
                    { "data": "ContractID" }
                ]
                 , rowCallback: function (row, data, iDisplayIndex) {
                     var index = iDisplayIndex + 1;
                     $('td:eq(0)', row).html(index);
                 }
                , "columnDefs": [{
                }],
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
    };

    $.fn.FormPostpaidPhoneListAlt = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidPhoneListAlt'),
                options = $.extend({}, $.fn.FormPostpaidPhoneListAlt.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidPhoneListAlt', data);
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

    $.fn.FormPostpaidPhoneListAlt.defaults = {
    }

    $('#PostpaidPhoneListAlt', $('.modal:last')).FormPostpaidPhoneListAlt();

})(jQuery);





