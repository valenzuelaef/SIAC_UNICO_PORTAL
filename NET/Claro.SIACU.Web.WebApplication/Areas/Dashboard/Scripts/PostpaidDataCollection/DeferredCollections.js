(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormDeferredCollections.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblDeferredCollections: $('#tblDeferredCollections', $element)
            , lblContratoIdDeferred: $('#contratoIdDeferred', $element)
            , lblNameClientDeferred: $('#nameClientDeferred', $element)
            , lblNumberServicesDeferred: $('#numberServicesDeferred', $element)
            , totalBilledAmount: $('#totalBilledAmount', $element)
            , totalAmountNotBilled: $('#totalAmountNotBilled', $element)
            , errorDeferredCollections: $('#errorDeferredCollections', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            var that = this,
                controls = that.getControls();

            that.getDeferredCollections();
        },
        getDeferredCollections: function () {
            var oDataDeferredCollections = window.opener.$("#hdDataDeferredCollections").data('dataDeferredCollections');

            this.getControls().lblContratoIdDeferred.text(oDataDeferredCollections.data.ContratoId);
            this.getControls().lblNameClientDeferred.text(oDataDeferredCollections.data.NameClient);
            this.getControls().lblNumberServicesDeferred.text(oDataDeferredCollections.data.NumberServices);
            this.getControls().totalBilledAmount.text(oDataDeferredCollections.data.TotalBilledAmount);
            this.getControls().totalAmountNotBilled.text(oDataDeferredCollections.data.TotalAmountNotBilled);
            this.createTableDeferredCollections(oDataDeferredCollections.data.DeferredCollections);

        },
        createTableDeferredCollections: function (response) {
            var that = this,
            controls = that.getControls();

            that.tblDeferredCollections = controls.tblDeferredCollections.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , select: 'single'
                , data: response
                , scrollY: 300
                , scrollCollapse: true
                , columns: [
                    { "data": "Nro" },
                    { "data": "Comentario" },
                    { "data": "FechaIngreso" },
                    { "data": "Importe" },
                    { "data": "Periodo" }
                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 4]
                    },
                    {
                        className: "text-right",
                        targets: 3
                    }
                ]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        getExportDeferredCollections: function () {
            var strUrlModal = '~/Dashboard/PostPaidDataCollection/ExportDeferredCollections',
                strUrlResult = '~/Dashboard/Home/DownloadExcel';

            var objDeferredCollections = {
                cuenta: Session.DATACUSTOMER.Account,
                periodo: "T",
                tipoConsulta: "3",
                nameClient: this.getControls().lblNameClientDeferred.text(),
                numberServices: this.getControls().lblNumberServicesDeferred.text(),
                contratoId: this.getControls().lblContratoIdDeferred.text(),
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(objDeferredCollections),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=DeferredCollections.xlsx";
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
    };
    $.fn.FormDeferredCollections = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getExportDeferredCollections'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormDeferredCollections'),
                options = $.extend({}, $.fn.FormDeferredCollections.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormDeferredCollections', data);
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

    $('#DeferredCollections').FormDeferredCollections();

})(jQuery);

