function CustomerSearch(valTypeProduct, valTelephone) {

    var stUrlLogo = "/Images/loading2.gif";

    $.window.close();
    $.window.close();
    $.blockUI({
        message: '<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff',
        }
    });

    var valSearchType = ((valTypeProduct == "HFC" || valTypeProduct == "LTE") ? "3" : "1");
    if (!ValidateMigration(valSearchType, valTelephone)) {
    var data = { strSearchType: valSearchType, strSearchValue: valTelephone, strIdSession: Session.IDSESSION, NotEvalState: false, FlagSearchType: false, userId: Session.USERACCESS.userId, IsPrepaid: false, IsPostPaid: (typeof Session.IsPostPaid != 'undefined' && Session.IsPostPaid !=null ? Session.IsPostPaid : false) };
    var strUrl = '~/Home/ValidateQuery';
    var isclosed = false;
    Session.TELEPHONE = valTelephone;
    $.app.ajax({
        type: "POST",
        dataType: "json",
        url: strUrl,
        data: data,
        complete: function () {
            Session.NotEvalState=false;

            if (!isclosed) {
            if (data.strSearchType == '3' || data.strSearchType == '1') {
                window.opener.window.opener.$("#navbar-contenedor").form("showDialogLoad");
            }
            } else {
                return false;
            }
            window.opener.close();
            window.close();


        },
        success: function (result) {
            if (result.Options == null) {
                window.opener.window.opener.$('#divContenido').html("");
                window.opener.window.opener.showAlert("Usted no está autorizado para consultar este tipo de producto");
                Session.IsPostPaid = false;
                isclosed = true;
                return false;
            }
            if (result.OriginType != "" && result.data != null) {
                window.opener.window.opener.$("#navbar-contenedor").form("builtContent",
                {
                    paramResult: JSON.stringify(result),
                    paramSearchType: '1',
                    paramSearchValue: data.strSearchValue
                }
                );


            } else {


                window.opener.window.opener.showAlert('No se encontraron Resultados', this.strTitleMessage);
                window.opener.window.opener.$('#divContenido').html("");
                isclosed = true;

            }
        },
        error: function (msger) {
            $.app.error({
                id: 'divContenido',
                message: msger
            });
        }
    });
}

}

function ValidateMigration (tipoBusqueda, valorBusqueda) {
    console.log(4);
    console.log(tipoBusqueda);
    console.log(valorBusqueda);
    var resultado = false;
    var strUrlContent = '~/Home/ValidateMigration';
    var data = {
        strIdSession: Session.IDSESSION,
        strValue: valorBusqueda,
        strType: tipoBusqueda
    }

    $.app.ajax({
        type: "POST",
        url: strUrlContent,
        data: data,
        async:false,
        success: function (data) {
            console.log(1);
            console.log(data);
            resultado = data.estado;
            if (resultado) {
                window.opener.window.opener.showAlert(data.mensaje, 'Mensaje');
            }
        },
        error: function (msg) {
            consol.log("Error validateMigration " + msg);
        }
    });
    return resultado;
}

function CustomerPreactivo() {
    modalAlert("El contrato aún no se encuentra activo", this.strTitleMessage);
    $(this).focus();
}

(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            tblDetailPostpaid: $('#tblDetailPostpaid')
        })
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.tblDetailPostpaid.DataTable({
                "scrollY": "400px",
                "scrollCollapse": true,
                "select": "single",
                "info": false,
                "paging": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });

            that.render();
        },
        render: function () {
            var that = this;
            that.CustomerSearch();
        },

        applicativeRoute: window.location.href,
        CustomerSearch: function () {


        },
        getControls: function () {
            return this.m_controls || {};
        },
        strTitleMessage: "Detalle de Productos",
        setControls: function (value) {
            this.m_controls = value;
        }

    };

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['builtContent', 'showDialogLoad'];

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

    $('#contenedor-detail').form();

})(jQuery);