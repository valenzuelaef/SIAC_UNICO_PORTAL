(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPhoneListAltHistory.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , divPostListAltHistorySearch: $('#divPostListAltHistorySearch', $element)
            , lblPost_PhoneListHistoryContractId: $('#lblPost_PhoneListHistoryContractId', $element)
            , btnPost_TelephoneAltHistorySearch: $('#btnPost_TelephoneAltHistorySearch', $element)
            , btnPost_TelephoneAltHistoryClear: $('#btnPost_TelephoneAltHistoryClear', $element)
            , tbPostListAltHistory: $('#tbPostListAltHistory', $element)

        });

    };


    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnPost_TelephoneAltHistorySearch.addEvent(that, 'click', that.TelephoneAltHistorySearch);
            controls.btnPost_TelephoneAltHistoryClear.addEvent(that, 'click', that.TelephoneAltHistoryClear);

            that.getDataTable();
        },
        TelephoneAltHistorySearch: function () {

            var that = this,
            controls = this.getControls();

            var strApplication = Session.ORIGINTYPESERVICE;
            var strContractID = controls.lblPost_PhoneListHistoryContractId.val();
            var strUrlView = '~/Dashboard/PostpaidDataService/PhoneListAltHistorySearch';

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

        },
        TelephoneAltHistoryClear: function () {
            var controls = this.getControls();
            controls.lblPost_PhoneListHistoryContractId.val('');
        },
        getDataTable: function (objPhoneList) {

            var controls = this.getControls();

            controls.tbPostListAltHistory.DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "searching": false,
                "data": objPhoneList,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "emptyTable": "No existen datos"
                }, "columns": [
                    { "data": "MSISDN" },
                    { "data": "ActivationDate" },
                    { "data": "DeactivationDate" }
                ]
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

    $.fn.FormPhoneListAltHistory = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPhoneListAltHistory'),
                options = $.extend({}, $.fn.FormPhoneListAltHistory.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPhoneListAltHistory', data);
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



    $.fn.FormPhoneListAltHistory.defaults = {
    }

    $('#PostpaidPhoneListAltHistory', $('.modal:last')).FormPhoneListAltHistory();

})(jQuery);







