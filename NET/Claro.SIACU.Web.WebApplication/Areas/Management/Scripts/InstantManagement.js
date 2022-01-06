function formatDateTime(dateInstant, hourInstant, minuteInstant, typeInstant) {
    if (typeInstant == "-1") {
       
        showAlert('TIPO DE FECHA INVALIDO', 'Datos Incorrectos');
        return null;
    } else {
        var xtype = "pm";
        if (typeInstant == "AM") xtype = "am"
        return dateInstant + ' ' + hourInstant + ':' + minuteInstant + ':00 ' + xtype;
    }
}
function validateAll() {
    var fechaInicio;
    var fechaFin;
    var hora_inicio;
    var hora_fin;

    if ($.trim(document.getElementById("commentInstant").value) == '') {
       
        showAlert('El Mensaje es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("commentInstant").focus();
        return false;
    } if ($.trim(document.getElementById("txtDateValidityStart").value) == '') {
       
        showAlert('La Fecha Inicio es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txtDateValidityStart").focus();
        return false;
    } if (validarFecha("txtDateValidityStart") == false) {
        document.getElementById("txtDateValidityStart").focus();
        document.getElementById("txtDateValidityStart").select();
        return false;
    }
    hora_inicio = document.getElementById("txthoursStart").value;
    if (hora_inicio == '') {
       
        showAlert('La hora de la fecha de inicio es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txthoursStart").focus();
        return false;
    }

    hora_inicio = document.getElementById("txtminutesStart").value;
    if (hora_inicio == '') {
       
        showAlert('Los minutos de la hora inicio es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txtminutesStart").focus();
        return false;
    }
    if (document.getElementById('cboTypeStart').value == '-1') {
        
        showAlert('El tipo de hora inicio es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById('cboTypeStart').focus();
        return false;
    }
    if (validarHoraIngreso('txthoursStart', 'txtminutesStart', document.getElementById('cboTypeStart').value) == false) {
        document.getElementById('txthoursStart').focus();
        document.getElementById('txthoursStart').select();
        return false;
    } if ($.trim(document.getElementById("txtDateValidityEnd").value) == '') {
        
        showAlert('La Fecha de caducidad es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txtDateValidityEnd").focus();
        return false;
    }


    if (validarFecha("txtDateValidityEnd") == false) {
        document.getElementById(control).focus();
        document.getElementById(control).select();
        return false;
    }


    hora_inicio = document.getElementById("txthoursEnd").value;
    if (hora_inicio == '') {
       
        showAlert('La hora de la fecha de caducidad es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txthoursEnd").focus();
        return false;
    }

    hora_inicio = document.getElementById("txtminutesEnd").value;
    if (hora_inicio == '') {
       
        showAlert('Los minutos de la hora de caducidad es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById("txtminutesEnd").focus();
        return false;
    }
    if (document.getElementById('cboTypeEnd').value == '-1') {
       
        showAlert('El tipo de hora es un dato obligatorio', 'Datos Incorrectos');
        document.getElementById('cboTypeEnd').focus();
        return false;
    }

    if (validarHoraIngreso('txthoursEnd', 'txtminutesEnd', document.getElementById('cboTypeEnd').value) == false) {
        document.getElementById('txthoursEnd').focus();
        document.getElementById('txthoursEnd').select();
        return false;
    }
    if (!ComparaFecha(document.getElementById('txtDateValidityStart').value, document.getElementById('txtDateValidityEnd').value, '0')) {
        
        showAlert('La Fecha de vigencia debe ser menor o igual que la Fecha de caducidad ', 'Datos Incorrectos');

        return false;
    }
    fechaInicio = $.trim(document.getElementById('txtDateValidityStart').value);
    fechaFin = $.trim(document.getElementById('txtDateValidityEnd').value);
    
    if (fechaInicio == fechaFin) {

        hora_inicio = $.trim(document.getElementById('txthoursStart').value) + ':' + $.trim(document.getElementById('txtminutesStart').value); + ' ' + document.getElementById('cboTypeStart').value;
        hora_fin = $.trim(document.getElementById('txthoursEnd').value) + ':' + $.trim(document.getElementById('txtminutesEnd').value); + ' ' + document.getElementById('cboTypeEnd').value;
        if (hora_inicio == hora_fin) {
            
            showAlert('La Hora de la fecha de caducidad debe ser mayor a la hora de la fecha de inicio vigencia ', 'Datos Incorrectos');
            document.getElementById('txthoursEnd').focus();
            document.getElementById('txthoursEnd').select();
            return false;
        }

        if (!ComparaHora(hora_inicio, hora_fin, '0')) {
            
            showAlert('La Hora de la fecha de caducidad debe ser mayor a la hora de la fecha de inicio vigencia', 'Datos Incorrectos');
            document.getElementById('txthoursEnd').focus();
            document.getElementById('txthoursEnd').select();
            return false;
        }
    }
    return true;

}

function validarHoraIngreso(control_hh, control_mm, valor_am_pm) {
    var hora = document.getElementById(control_hh).value;
    var minutos = document.getElementById(control_mm).value;
    hora = hora + ':' + minutos;
    if (valor_am_pm != '')
        hora = hora + ' ' + valor_am_pm;

    hora = $.trim(hora);

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

function ComparaFecha(fechainicio, fechafin, flag) {
    var comp1;
    var comp2;
    comp1 = fechainicio.substr(6, 4) + '' + fechainicio.substr(3, 2) + '' + fechainicio.substr(0, 2);
    comp2 = fechafin.substr(6, 4) + '' + fechafin.substr(3, 2) + '' + fechafin.substr(0, 2);
    if (flag == '0') {
        if ((comp1) > (comp2)) {
            return false;
        }
    }
    if (flag == '1') {
        if ((comp1) >= (comp2)) {
            return false;
        }
    }
    return true;
}

function ComparaHora(hora_inicio, hora_fin, flag) {
    /*
		Las horas tienen q tener el formato hh:mm AM/PM
		9:42 AM - 1:15 PM (ok)
		12:14 am 1:15 am (ok)
		11:15 am 12:60 am (error)
		11:02 am 11:01 am (error)
	*/


    /*para la hora 1*/
    var arrHT = hora_inicio.split(' ');
    var arrH = arrHT[0].split(':');
    var h = arrH[0];
    var m = arrH[1];
    var am_pm = arrHT[1];
    var s = 0;
    am_pm = am_pm.replace(".", "");

    if ((am_pm == 'PM') || (am_pm == 'pm')) {
        if (parseFloat(h) != 12)
            s = 12;
    } else {
        if ((am_pm == 'AM') || (am_pm == 'am')) {
            if (parseFloat(h) == 12) {
                s = -12;
            }
        }
    }

    var h1 = parseFloat(h) + parseFloat(s);
    m = parseFloat(m);
    if (m < 10) m = '0' + m;
    h1 = h1 + '' + m;

    /*Para la hora 2*/
    arrHT = hora_fin.split(' ');
    arrH = arrHT[0].split(':');
    am_pm = arrHT[1];

    h = arrH[0];
    m = arrH[1];
    s = 0;

    m = parseFloat(m);
    am_pm = am_pm.replace(".", "");
   

    if (m < 10) m = '0' + m;
    if ((am_pm == 'PM') || (am_pm == 'pm')) {
        if (parseFloat(h) != 12)
            s = 12;
    } else {
        if ((am_pm == 'AM') || (am_pm == 'am')) {
            if (parseFloat(h) == 12) {
                s = -12;
            }
        }
    }
    var h2 = parseFloat(h) + parseFloat(s);
    h2 = h2 + '' + m;
    h1 = parseFloat(h1);
    h2 = parseFloat(h2);
    showAlert(h1 + "   " + h2)
    if (flag == '0') {
        showAlert(h1 + "   " + h2)
        if ((h1) > (h2)) {
            return false;
        }
    }
    if (flag == '1') {
        if ((h1) >= (h2)) {
            return false;
        }
    }
    return true;
}

function validarFecha(oControl) {
    var Day, Month, Year;
    var Fecha = document.getElementById(oControl);
    var valor = Fecha.value;
   

    if (validateDateMask(valor) == false) {
        
        showAlert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)', 'Datos Incorrectos');
        return false;
    }

    Day = getvalue(valor, 1, "/");
    Month = getvalue(valor, 2, "/");
    Year = getvalue(valor, 3, "/");

    if ((isNumber(Day) && isNumber(Month) && isNumber(Year) && (Year.length == 4) && (Day.length <= 2) && (Month.length <= 2)) || ((Month == 2) && (Day <= 29))) {
        if ((Day != 0) && (Month != 0) && (Year != 0) && (Month <= 12) && (Day <= 31) && (Month != 2)) {

            if (Month == 4 || Month == 6 || Month == 9 || Month == 11) {
                if (Day > 30) {
                    
                    showAlert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)', 'Datos Incorrectos');
                    return false;
                }
            } else if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                if (Day > 31) {
                    
                    showAlert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)', 'Datos Incorrectos');
                    return false;
                }
            }
            return true;
        }
        else if ((Month == 2) && (Day <= 29) && ((Year % 4) == 0) && ((Year % 100) != 0))
            return true;
        else if ((Month == 2) && (Day <= 29) && ((Year % 400) == 0))
            return true;
        else if ((Month == 2) && (Day <= 28))
            return true;
        else {
            if (Month > 12) {
                
                showAlert('El campo de mes debe como maximo 12.', 'Datos Incorrectos');
            }
            else if (Year.length != 4) {
                
                showAlert('El año debe tener 4 cifras.', 'Datos Incorrectos');
            }
            else if ((Month == 2) && (Day == 29) && ((Year % 4) == 0) && (Year % 100) == 0) {
                
                showAlert('Año no bisiesto.', 'Datos Incorrectos');
            }
            else {
               
                showAlert('Fecha no valida', 'Datos Incorrectos');
            }
            if (Fecha.disabled == false)
                Fecha.focus();

            Fecha.select();
            return false;
        }
    }
    else {
       
        showAlert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)', 'Datos Incorrectos');
        return false;
    }
}
function validateDateMask(strDate) {
    if (mask(strDate, '##/##/####') != 1)
        return false;
    else
        return true;
}
function mask(InString, Mask) {
    var LenStr;
    var LenMsk;
    var Count = 0;
    var StrChar;
    var MskChar;

    LenStr = InString.length;
    LenMsk = Mask.length;
    if ((LenStr == 0) || (LenMsk == 0))
        return 0;
    if (LenStr != LenMsk)
        return 0;
    for (Count = 0; Count <= InString.length; Count++) {
        StrChar = InString.substring(Count, Count + 1);
        MskChar = Mask.substring(Count, Count + 1);
        if (MskChar == '#') {
            if (!isNumberChar(StrChar))
                return 0;
        }
        else if (MskChar == '?') {
            if (!isAlphabeticChar(StrChar))
                return 0;
        }
        else if (MskChar == '!') {
            if (!isNumOrChar(StrChar))
                return 0;
        }
        else {
            if (MskChar != StrChar)
                return 0;
        }
    }
    return 1;
}
function getvalue(strData, intFieldNumber, separator) {
    var intCurrentField, intFoundPos, strValue, strNames;
    var bool = false;
    strNames = strData;
    intCurrentField = 0;
    while ((intCurrentField != intFieldNumber) && !bool) {
        intFoundPos = strNames.indexOf(separator);
        intCurrentField = intCurrentField + 1;
        if (intFoundPos != 0) {
            strValue = strNames.substring(0, intFoundPos);
            strNames = strNames.substring(intFoundPos + 1, strNames.length);
        }
        else {
            if (intCurrentField == intFieldNumber)
                strValue = strNames;
            else
                strValue = "";
            bool = true;
        }
    }
    if (strValue != "")
        return strValue;
    else
        return strNames;
}
function isNumber(pString) {
    var ok = "yes"; var temp;
    var valid = "0123456789";
    for (var i = 0; i < pString.length ; i++) {
        temp = "" + pString.substring(i, i + 1);
        if (valid.indexOf(temp) == "-1") ok = "no";
    }
    if (ok == "no") { return (false); } else { return (true); }
}
function isNumberChar(InString) {
    if (InString.length != 1)
        return (false);
    var RefString = "1234567890";
    if (RefString.indexOf(InString, 0) == -1)
        return (false);
    return (true);
}
function isAlphabeticChar(InString) {
    if (InString.length != 1)
        return (false);
    InString = InString.toLowerCase();
    var RefString = "abcdefghijklmnopqrstuvwxyz";
    if (RefString.indexOf(InString.toLowerCase(), 0) == -1)
        return (false);
    return (true);
}
function isNumOrChar(InString) {
    if (InString.length != 1)
        return (false);
    InString = InString.toLowerCase();
    var RefString = "1234567890abcdefghijklmnopqrstuvwxyz";
    if (RefString.indexOf(InString, 0) == -1)
        return (false);
    return (true);
}


(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.VisualizeCallPrepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            , commentInstant: $('#commentInstant', $element)
            , txtDateValidityStart: $('#txtDateValidityStart', $element)
            , txthoursStart: $('#txthoursStart', $element)
            , txtminutesStart: $('#txtminutesStart', $element)
            , cboTypeStart: $('#cboTypeStart', $element)
            , txtDateValidityEnd: $('#txtDateValidityEnd', $element)
            , txthoursEnd: $('#txthoursEnd', $element)
            , txtminutesEnd: $('#txtminutesEnd', $element)
            , cboTypeEnd: $('#cboTypeEnd', $element)
            , hdidinstant: $('#hdidinstant', $element)
            , btnManagementInstant: $('#btnManagementInstant', $element)
            , hdoption: $('#hdoption', $element)
            , divModalError: $('#divModalError', $element)
            , btnCancel: $('#btnCancel', $element)
            , btnSearchInstant: $('#btnSearchInstant')

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            controls.btnManagementInstant.addEvent(that, 'click', that.btnManagementInstant_click);
            controls.btnCancel.addEvent(that, 'click', that.btnCancel_click);
            that.render();

        },
        render: function () {
            if ($("#hdoption").val() == "D") {
                $('#txtDateValidityStart').prop('disabled', true);
                $('#txthoursStart').prop('disabled', true);
                $('#txtminutesStart').prop('disabled', true);
                $('#cboTypeStart').prop('disabled', true);
                $('#txtDateValidityEnd').prop('disabled', true);
                $('#txthoursEnd').prop('disabled', true);
                $('#txtminutesEnd').prop('disabled', true);
                $('#cboTypeEnd').prop('disabled', true);
                $('#commentInstant').prop('disabled', true);
                $("#date").datepicker("option", "disabled", true);
                $('input:radio[name=rbColour]').prop('disabled', true);
            }
        },
        btnCancel_click: function () {

            
        },
        btnManagementInstant_click: function () {
            var xoption = $("#hdoption").val();

            if (xoption != 'D') {
                if (validateAll() == true)
                    this.showManagementInstant(xoption);
            } else {
                if (!showConfirm("La instantánea se va a eliminar, desea continuar?")) {
                    return;
                }
                this.showManagementInstant(xoption);
            }
        },
        showManagementInstant: function (option) {
            var controls = this.getControls();
            var valhdidinstant = $("#hdidinstant").val();
            var vDateValidityStart = formatDateTime(controls.txtDateValidityStart.val(), controls.txthoursStart.val(), controls.txtminutesStart.val(), controls.cboTypeStart.val());
            var vDateValidityEnd = formatDateTime(controls.txtDateValidityEnd.val(), controls.txthoursEnd.val(), controls.txtminutesEnd.val(), controls.cboTypeEnd.val());

            $.window.open({
                modal: true,
                title: "Mensaje",
                url: '~/Management/PrepaidInstant/MessageInstant',
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, stridInstant: valhdidinstant, strOption: option, strcomment: controls.commentInstant.val(), strDateValidityStart: vDateValidityStart, strDateValidityEnd: vDateValidityEnd, strColour: $('input:radio[name=rbColour]:checked').val(), strbusqInstant: Session.BUSQINSTANT, strcontactoId: Session.DATACUSTOMER.Account },
                width: 1024,
                height: 600,
                buttons: {
                    Aceptar: {
                        click: function (sender, args) {
                            this.close();
                            controls.btnSearchInstant.trigger("click");
                        }
                    }
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
        },


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

    $('#contenedorManagementInstant').VisualizeCallPrepaid();






});
