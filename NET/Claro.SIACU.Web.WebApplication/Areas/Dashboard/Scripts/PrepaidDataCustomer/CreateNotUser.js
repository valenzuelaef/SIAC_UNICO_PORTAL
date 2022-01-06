(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidCreateNotUser.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

             , txtNotUser_Telephone: $('#txtNotUser_Telephone', $element)
             , txtNotUser_DateBirth: $('#txtNotUser_DateBirth', $element)
             , chkNotUser_Registered: $('#chkNotUser_Registered', $element)
             , ddlNotUser_MotiveRegister: $('#ddlNotUser_MotiveRegister', $element)
             , txtNotUser_Name: $('#txtNotUser_Name', $element)
             , txtNotUser_LastName: $('#txtNotUser_LastName', $element)
             , ddlNotUser_Sex: $('#ddlNotUser_Sex', $element)
             , ddlNotUser_TypeDocument: $('#ddlNotUser_TypeDocument', $element)
             , txtNotUser_NumberDocument: $('#txtNotUser_NumberDocument', $element)
             , ddlNotUser_StateCivil: $('#ddlNotUser_StateCivil', $element)
             , ddlNotUser_Occupation: $('#ddlNotUser_Occupation', $element)
             , txtNotUser_Position: $('#txtNotUser_Position', $element)
             , txtNotUser_TelephoneReference: $('#txtNotUser_TelephoneReference', $element)
             , txtNotUser_Fax: $('#txtNotUser_Fax', $element)
             , txtNotUser_Email: $('#txtNotUser_Email', $element)
             , txtNotUser_Address: $('#txtNotUser_Address', $element)
             , txtNotUser_District: $('#txtNotUser_District', $element)
             , txtNotUser_ZipCode: $('#txtNotUser_ZipCode', $element)
             , txtNotUser_Urbanization: $('#txtNotUser_Urbanization', $element)
             , txtNotUser_City: $('#txtNotUser_City', $element)
             , txtNotUser_Department: $('#txtNotUser_Department', $element)
             , txtNotUser_Reference: $('#txtNotUser_Reference', $element)
             , ddlNotUser_ConfirmMail: $('#ddlNotUser_ConfirmMail', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            var that = this;
            that.setTelephone();
            that.loadControlDateTime();


        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        loadControlDateTime: function () {
            var controls = this.getControls();

            controls.txtNotUser_DateBirth.datepicker({
                format: 'dd/mm/yyyy',
                beforeShow: function (input) {
                    $(input).css({
                        "position": "relative",
                        "z-index": 999999
                    });
                }
            }).data('datepicker');
        },
        setTelephone: function () {
            $('#txtNotUser_Telephone').val(Session.TELEPHONE);
        },
        getRoute: function () {
            return window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        },
        registerNotUser: function () {
            var controls = this.getControls();

            var data = {
                strIdSession: Session.IDSESSION,
                strModality: "NU",
                strTypeProcess: "CC",
                strTelephoneCustomer: Session.TELEPHONE,
                strRegistered: $('#chkNotUser_Registered:checked').val(),
                strMotiveRegister: controls.ddlNotUser_MotiveRegister.val(),
                strName: controls.txtNotUser_Name.val(),
                strLastName: controls.txtNotUser_LastName.val(),
                strDateBirth: controls.txtNotUser_DateBirth.val(),
                strSex: controls.ddlNotUser_Sex.val(),
                strTypeDocument: controls.ddlNotUser_TypeDocument.val(),
                strNumberDocument: controls.txtNotUser_NumberDocument.val(),
                strStateCivil: controls.ddlNotUser_StateCivil.val(),
                strOccupation: controls.ddlNotUser_Occupation.val(),
                strPosition: controls.txtNotUser_Position.val(),
                strTelephoneReference: controls.txtNotUser_TelephoneReference.val(),
                strFax: controls.txtNotUser_Fax.val(),
                strEmail: controls.txtNotUser_Email.val(),
                strAddress: controls.txtNotUser_Address.val(),
                strDistrict: controls.txtNotUser_District.val(),
                strZipCode: controls.txtNotUser_ZipCode.val(),
                strUrbanization: controls.txtNotUser_Urbanization.val(),
                strCity: controls.txtNotUser_City.val(),
                strDepartment: controls.txtNotUser_Department.val(),
                strReference: controls.txtNotUser_Reference.val(),
                strConfirmMail: controls.ddlNotUser_ConfirmMail.val()
            };

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PrepaidDataCustomer/RegisterNotUser' ,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: true,
                data: JSON.stringify(data),
                success: function (result) {
                    showAlert(result.message);
                },
                error: function (ex) {
                }
            });
        }
    };

    $.fn.PrepaidCreateNotUser = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['registerNotUser'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidCreateNotUser'),
                options = $.extend({}, $.fn.PrepaidCreateNotUser.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidCreateNotUser', data);
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

    $.fn.PrepaidCreateNotUser.defaults = {
    }

    $('#contenedorCreateNotUser', $('.modal:last')).PrepaidCreateNotUser();

})(jQuery);

