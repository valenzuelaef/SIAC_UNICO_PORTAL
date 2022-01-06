(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidStriations.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblTriaciones: $('#tblTriaciones', $element)
            , divPostpaid: $('#divPostpaid', $element)
            , errorTriaciones: $('#errorTriaciones', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },
        render: function () {
            this.getStriations();
        },
        btnPostHistoricalStriations_click: function () {
           
            $.window.open({
                modal: false,
                title: "Histórico Triaciones",
                url: '~/Dashboard/PostpaidDataService/HistoricalStriations',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        createTableStriations: function (response) {
            var controls = this.getControls();

            controls.tblTriaciones.DataTable({
                info: false
                , paging: false
                , select: 'single'
                , destroy: true
                , searching: false
                , scrollY: 300
                , scrollCollapse: true
                ,scrollX: true
                ,sScrollXInner: "100%"
                ,autoWidth: true
                , data: response.data.Striations
                , columns: [
                    { "data": "NumberThreesome" },
                    { "data": "Telephone" },
                    { "data": "Factor" },
                    { "data": "TypeDestination" },
                    { "data": "Portability" }
                ]
                , columnDefs: [
                    {
                        targets: 0,
                        render: function (data, type, full, meta) {
                            return 'Triado ' + data;
                        }
                    },
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4]
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
        getStriations: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objTriaciones = {};

            objTriaciones.ContractId = oCustomer.ContractID;
            objTriaciones.strIdSession = Session.IDSESSION;

            controls.errorTriaciones.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblTriaciones.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetStriations',
                data: JSON.stringify(objTriaciones),
                success: function (response) {
                    controls.tblTriaciones.find('tbody').html('');
                    that.createTableStriations(response);
                },
                error: function (msger) {
                    controls.tblTriaciones.find('tbody').html('');
                    $.app.error({
                        id: 'errorTriaciones',
                        message: msger,
                        click: function () {
                            that.getStriations();
                        }
                    });
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

    $.fn.PostPaidStriations = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['btnPostHistoricalStriations_click'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidStriations'),
                options = $.extend({}, $.fn.PostPaidStriations.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidStriations', data);
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

    $.fn.PostPaidStriations.defaults = {
    }

    $('#contenedorTriaciones', $('.modal:last')).PostPaidStriations();

})(jQuery);