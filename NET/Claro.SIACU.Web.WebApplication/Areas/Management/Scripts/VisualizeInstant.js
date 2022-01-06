function validateDateandHour() {


    var hora_inicio = document.getElementById("txthours").value;
    if (hora_inicio != '') {

         hora_inicio = document.getElementById("txtminutes").value;
        if (hora_inicio == '') {
           
            showAlert('Los minutos de la hora es un dato obligatorio', 'Información');
            document.getElementById("txtminutes").focus();
            return false;
        }
        if (document.getElementById('cboType').value == '-1') {
           
            showAlert('El tipo de hora es un dato obligatorio', 'Información');
            document.getElementById('cboType').focus();
            return false;
        }

        if (validarHoraIngreso('txthours', 'txtminutes', document.getElementById('cboType').value) == false) {
            document.getElementById('txthours').focus();
            document.getElementById('txthours').select();
            return false;
        }
    }

}

function validarHoraIngreso(control_hh, control_mm, valor_am_pm) {
    var hora = document.getElementById(control_hh).value;
    var minutos = document.getElementById(control_mm).value;
    hora = hora + ':' + minutos;
    if (valor_am_pm != '')
        hora = hora + ' ' + valor_am_pm;

    

    var er_fh = /^(1|01|2|02|3|03|4|04|5|05|6|06|7|07|8|08|9|09|10|11|12)\:([0-5]0|[0-5][1-9])\ (AM|PM|A.M.|P.M|A.M|P.M.)$/
    if (hora == "") {
       
        showAlert('Introduzca la hora.', 'Datos Incorrectos');
        return false
    }
    hora = hora.toUpperCase();
    if (!(er_fh.test(hora))) {
       
        showAlert('El dato en el campo hora es incorrecto.\n\nFormato valido (HH:MM AM/PM)', 'Datos Incorrectos');
        return false
    }
    /* VALIDAMOS  Q SOLO INGRESE UNA "A/P" Y UNA SOLA "M"*/

    var arrHT = hora.split(' ');
    var am_pm = arrHT[1];
    var cant_A, cant_P, cant_M;
    cant_A = 0; cant_P = 0; cant_M = 0;
    var temp;
    if (am_pm.length > 4) {
        
        showAlert('El dato en el campo hora es incorrecto.\n\nFormato valido (HH:MM AM/PM)', 'Datos Incorrectos');
        return false
    }
    for (var i = 0; i < am_pm.length ; i++) {
        temp = am_pm.substring(i, 1);
        temp = temp.toUpperCase();
        if (temp == 'A')
            cant_A++;
        if (temp == 'P')
            cant_P++;
        if (temp == 'M')
            cant_M++;
    }
    if ((cant_A > 1) || (cant_P > 1) || (cant_M > 1)) {

        showAlert('El dato en el campo hora es incorrecto.\n\nFormato valido (HH:MM AM/PM)', 'Datos Incorrectos');
        return false
    }

    return true
}

(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.VisualizeCallPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            , divDetailVisualizeCall: $('#divDetailVisualizeCall', $element)
            , txtDateInstant: $('#txtDateInstant', $element)
            , btnSearchInstant: $('#btnSearchInstant', $element)
            , cboType: $('#cboType', $element)
            , txthours: $('#txthours', $element)
            , txtminutes: $('#txtminutes', $element)
            , btnNewInstant: $('#btnNewInstant', $element)
            , hdidinstant: $('#hdidinstant', $element)
            , btnEditInstant: $('#btnEditInstant', $element)
            , btnDeleteInstant: $('#btnDeleteInstant', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();



            controls.btnSearchInstant.addEvent(that, 'click', that.btnSearchInstant_click);
            controls.btnNewInstant.addEvent(that, 'click', that.btnNewInstant_click);
            controls.btnEditInstant.addEvent(that, 'click', that.btnEditInstant_click);
            controls.btnDeleteInstant.addEvent(that, 'click', that.btnDeleteInstant_click);

            that.render();
        },
        render: function () {
        },
        btnDeleteInstant_click: function () {
            if (!$('input:radio[name=rbRowInstant]').is(':checked')) {
               
                showAlert('Necesita Seleccionar una instancia de la grilla.', 'Información');
            }
            else {
                this.showManagementInstant('D')
            };

        },
        btnEditInstant_click: function () {
            if (!$('input:radio[name=rbRowInstant]').is(':checked')) {
               
                showAlert('Necesita Seleccionar una instancia de la grilla.', 'Información');
            } else {
                this.showManagementInstant('U')
            };
        },
        btnNewInstant_click: function () {
            this.showManagementInstant('N');
        },
        showManagementInstant: function (option) {
      
     

            if (Session.ORIGINTYPE == "POSTPAID") {
                if (Session.BUSQINSTANT == "Cuenta") {
                    $('#chkCuenta').prop("checked", true);
                    $('#chkLinea').prop("checked", false);
                } else {
                    $('#chkCuenta').prop("checked", false);
                    $('#chkLinea').prop("checked", true);
                }
            }

            $.window.open({
                modal: true,
                title: "Administracion de Instantaneas",
                url: '~/Management/PrepaidInstant/ManagementInstant',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, stridInstant: $('input:radio[name=rbRowInstant]:checked').val(), strOption: option },
                width: 1024,
                height: 600,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnSearchInstant_click: function () {



            var fecha = $("#txtDateInstant").val();
            if (validateDateandHour() == false) {
                return;
            }

            if (($("#txtDateInstant").val()) != "") {
                fecha = $("#txtDateInstant").val();
            }
            if ($("#txthours").val() != "" && $("#txtminutes").val() != "" && $("#cbohour").val() != "-1") {
                fecha = $("#txtDateInstant").val() + " " + $("#txthours").val() + ":" + $("#txtminutes").val() + " " + $("#cboType").val()
            }

            this.showDetailCall(fecha);
        },

        showDetailCall: function (date) {

            var that = this;
            var controls = that.getControls();
            var strphone = $("#txtCriteriaValue").val();

            if (Session.ORIGINTYPE == "POSTPAID") {
                strphone = strphone + "|" + Session.DATACUSTOMER.Account
            }

            var data = { strIdSession: Session.IDSESSION, strTelephoneCustomer: strphone, strDateFrom: date, strOriginType: Session.ORIGINTYPE};

            $.app.ajax({
                type: 'POST',
                url: '~/Management/PrepaidInstant/DetailInstant',
                dataType: 'html',
                cache: true,
                data: data,
                container: controls.divDetailVisualizeCall,
                error: function (ex) {
                    controls.divDetailVisualizeCall.showMessageErrorLoading($.app.const.messageErrorLoading);
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
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        }


    };

    $.fn.VisualizeCallPrepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('VisualizeCallPrepaid'),
                options = $.extend({}, $.fn.VisualizeCallPrepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('VisualizeCallPrepaid', data);
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

    $.fn.VisualizeCallPrepaid.defaults = {
    }

})(jQuery);

$(document).ready(function () {

    $('#contenedorVisualizeCall').VisualizeCallPrepaid();


});
