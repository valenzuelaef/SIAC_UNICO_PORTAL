(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormAssociatedClaims.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblAssociatedClaims: $('#tblAssociatedClaims', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {

            window.moveTo(0, 0);
            window.resizeTo(screen.availWidth, screen.availHeight);

            var that = this;
            that.getAssociatedClaims();
        },
        getAssociatedClaims: function () {
            var that = this;

            var ListClaimByReceipt = window.opener.$('#tblStatusAccount').data("ClaimByReceipt");
            console.log(ListClaimByReceipt);
            that.createTableAssociatedClaims(ListClaimByReceipt);

        },
        associatedClaims_click: function () {
            this.getControls().tblAssociatedClaims.find('tbody a').addEvent(this, 'click', this.redirectProblemManagement);
        },
        redirectProblemManagement: function (send, args) {
            var that = this,
              controls = that.getControls();

            var Claim = send.parents("tr").find("td").eq(0).find("a>u").html();
            var ListClaimByReceipt = window.opener.$('#tblStatusAccount').data("ClaimByReceipt");

            for (var i = 0; i < ListClaimByReceipt.length; i++) {
                if (ListClaimByReceipt[i].reclamo == Claim) {
                    Claim = ListClaimByReceipt[i];
                    break;
                }
            }

            Session.EstadoForm = "E";
            Session.tipoCasoInteraccion = "1";
            Session.CasoInteraccionId = Claim.caso;
            Session.telefono = Claim.reclamo;
            Session.CasoID = Claim.caso;

            $.redirect.GetParamsData("SU_REC_GPRO", "RECLAMOS");

        },
        createTableAssociatedClaims: function (response) {
            var that = this,
            controls = that.getControls();

            that.tblAssociatedClaims = controls.tblAssociatedClaims.DataTable({
                info: false
                , paging: false
                , searching: false
                , destroy: true
                , select: 'single'
                , data: response
                , scrollY: 300
                , scrollCollapse: true
                , columns: [
                    { "data": "reclamo" },
                    { "data": "creacion" },
                    { "data": "interaccion" },
                    { "data": "monto" },
                    { "data": "estado" }
                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4]
                    },
                    {
                        targets: 0,
                        render: function (data, type, full, meta) {
                            return "<a href='#'><u>" + data + "</u></a>";
                        }
                    },
                    {
                    targets: 1,
                    render: function (data, type, full, meta) {

                        return moment(data).format('DD/MM/YYYY hh:mm:ss a');
                    }
                },
                ]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
            that.associatedClaims_click();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };
    $.fn.FormAssociatedClaims = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormAssociatedClaims'),
                options = $.extend({}, $.fn.FormAssociatedClaims.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormAssociatedClaims', data);
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

    $('#AssociatedClaims').FormAssociatedClaims();

})(jQuery);

