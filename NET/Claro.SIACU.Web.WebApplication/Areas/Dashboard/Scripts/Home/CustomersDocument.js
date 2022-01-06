
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            ddlTipoBusqueda: $('#ddlTipoBusqueda')
            , txtCriteriaValue: $("#txtCriteriaValue")
            , tblCustomersDocument: $('#tblCustomersDocument')

        })
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },

        render: function () {
            var that = this;
            that.CustomersDocument();
        },

        applicativeRoute: window.location.href,
        CustomersDocument: function () {
       
            if (Session.CUSTOMERPRODUCT.listDataCustomerModel != null) {
                $('#tblCustomersDocument').DataTable({
                    scrollY: "180px",
                    scrollCollapse: true,
                    paging: false,
                    info: false,
                    select: 'single',
                    destroy: true,
                    searching: false,
                    data: Session.CUSTOMERPRODUCT.listDataCustomerModel,
                    language: {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
                    , columns: [
                        {
                            data: null,
                            title: '',
                            defaultContent: ''
                        },
                        { data: "Names" },
                        { data: "DocumentType" },
                        { data: "DocumentIdentity" }
                    ]
                    , columnDefs: [{
                        orderable: false,
                        className: 'select-radio',
                        targets: 0,
                    }],
                    order: [[1, 'asc']]
                });
            }
            else {
                showAlert("No se pudo cargar los datos del cliente");
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Consulta de Clientes",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('form'),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('form', data);
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

    $.fn.form.defaults = {
    }


})(jQuery);

$(document).ready(function () {
    $('#contenedor-customers-document').form();
});


