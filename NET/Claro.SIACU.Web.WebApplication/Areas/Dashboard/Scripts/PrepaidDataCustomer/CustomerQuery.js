
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.CustomerQuery.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

              , lblPreQuery_Name: $('#lblPreQuery_Name', $element)
              , lblPreQuery_Lastname: $('#lblPreQuery_Lastname', $element)

              , lblPreQuery_Modality: $('#lblPreQuery_Modality', $element)
              , lblPreQuery_TelephoneCustomer: $('#lblPreQuery_TelephoneCustomer', $element)
              , lblPreQuery_TypeDocument: $('#lblPreQuery_TypeDocument', $element)
              , lblPreQuery_NumberDocument: $('#lblPreQuery_NumberDocument', $element)
              , lblPreQuery_Sex: $('#lblPreQuery_Sex', $element)
              , lblPreQuery_DateBirth: $('#lblPreQuery_DateBirth', $element)
              , lblPreQuery_PlaceBirth: $('#lblPreQuery_PlaceBirth', $element)
              , lblPreQuery_TelephoneReference: $('#lblPreQuery_TelephoneReference', $element)
              , lblPreQuery_StateCivil: $('#lblPreQuery_StateCivil', $element)
              , lblPreQuery_State: $('#lblPreQuery_State', $element)
              , lblPreQuery_Fax: $('#lblPreQuery_Fax', $element)
              , lblPreQuery_Email: $('#lblPreQuery_Email', $element)
              , lblPreQuery_EmailConfirmation: $('#lblPreQuery_EmailConfirmation', $element)
              , lblPreQuery_Ocupation: $('#lblPreQuery_Ocupation', $element)
              , lblPreQuery_Position: $('#lblPreQuery_Position', $element)
              , lblPreQuery_Role: $('#lblPreQuery_Role', $element)
              , lblPreQuery_Registered: $('#lblPreQuery_Registered', $element)
              , lblPreQuery_ReasonRegistry: $('#lblPreQuery_ReasonRegistry', $element)
              , lblPreQuery_Address: $('#lblPreQuery_Address', $element)
              , lblPreQuery_Urbanization: $('#lblPreQuery_Urbanization', $element)
              , lblPreQuery_District: $('#lblPreQuery_District', $element)
              , lblPreQuery_PostalCode: $('#lblPreQuery_PostalCode', $element)
              , lblPreQuery_City: $('#lblPreQuery_City', $element)
              , lblPreQuery_Department: $('#lblPreQuery_Department', $element)
              , lblPreQuery_Reference: $('#lblPreQuery_Reference', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.dataCustomerQuery();
        },
        dataCustomerQuery: function () {
            var controls = this.getControls();
            var jsonDataCustomer = Session.DATACUSTOMER;
            $.each(jsonDataCustomer, function (key, value) {
                if (value === null)
                    jsonDataCustomer[key] = "";
                else
                    jsonDataCustomer[key] = value;
            });
           

            Session.DATACUSTOMER = jsonDataCustomer;

            controls.lblPreQuery_Name.text(Session.DATACUSTOMER.Name);
            controls.lblPreQuery_Lastname.text(Session.DATACUSTOMER.Lastname);

            controls.lblPreQuery_Modality.text(Session.DATACUSTOMER.Modality);
            controls.lblPreQuery_TelephoneCustomer.text(Session.DATACUSTOMER.TelephoneCustomer);
            controls.lblPreQuery_TypeDocument.text(Session.DATACUSTOMER.TypeDocument);
            controls.lblPreQuery_NumberDocument.text(Session.DATACUSTOMER.NumberDocument);
            controls.lblPreQuery_Sex.text(Session.DATACUSTOMER.Sex);
            controls.lblPreQuery_DateBirth.text(Session.DATACUSTOMER.DateBirth);
            controls.lblPreQuery_PlaceBirth.text(Session.DATACUSTOMER.PlaceBirth);
            if (Session.DATACUSTOMER.TelephoneReference == null)
                controls.lblPreQuery_TelephoneReference.text('');
            else
                controls.lblPreQuery_TelephoneReference.text(Session.DATACUSTOMER.TelephoneReference);

            if (Session.DATACUSTOMER.StateCivil == null)
                controls.lblPreQuery_StateCivil.text('');
            else
                controls.lblPreQuery_StateCivil.text(Session.DATACUSTOMER.StateCivil);

            if (Session.DATACUSTOMER.State == null)
                controls.lblPreQuery_State.text('');
            else
                controls.lblPreQuery_State.text(Session.DATACUSTOMER.State);


            if (Session.DATACUSTOMER.Fax == null || Session.DATACUSTOMER.Fax == '---')
                controls.lblPreQuery_Fax.text('');
            else
                controls.lblPreQuery_Fax.text(Session.DATACUSTOMER.Fax);

            if (Session.DATACUSTOMER.Email == null || Session.DATACUSTOMER.Email == '---')
                controls.lblPreQuery_Email.text('');
            else
                controls.lblPreQuery_Email.text(Session.DATACUSTOMER.Email);

            if (Session.DATACUSTOMER.EmailConfirmation == null || Session.DATACUSTOMER.EmailConfirmation == '---')
                controls.lblPreQuery_EmailConfirmation.text('');
            else
                controls.lblPreQuery_EmailConfirmation.text(Session.DATACUSTOMER.EmailConfirmation);

            if (Session.DATACUSTOMER.Ocupation == null)
                controls.lblPreQuery_Ocupation.text('');
            else
                controls.lblPreQuery_Ocupation.text(Session.DATACUSTOMER.Ocupation);

            if (Session.DATACUSTOMER.Position == null)
                controls.lblPreQuery_Position.text('');
            else
                controls.lblPreQuery_Position.text(Session.DATACUSTOMER.Position);

            if (Session.DATACUSTOMER.Role == null)
                controls.lblPreQuery_Role.text('');
            else
                controls.lblPreQuery_Role.text(Session.DATACUSTOMER.Role);

            if (Session.DATACUSTOMER.Registered == null)
                controls.lblPreQuery_Registered.text('');
            else if (Session.DATACUSTOMER.Registered == 1)
                controls.lblPreQuery_Registered.text('Registrado');
            else
                controls.lblPreQuery_Registered.text(Session.DATACUSTOMER.Registered);


            if (Session.DATACUSTOMER.ReasonRegistry == null)
                controls.lblPreQuery_ReasonRegistry.text('');
            else
                controls.lblPreQuery_ReasonRegistry.text(Session.DATACUSTOMER.ReasonRegistry);

            if (Session.DATACUSTOMER.Address == null)
                controls.lblPreQuery_Address.text('');
            else
                controls.lblPreQuery_Address.text(Session.DATACUSTOMER.Address);

            if (Session.DATACUSTOMER.Urbanization == null)
                controls.lblPreQuery_Urbanization.text('');
            else
                controls.lblPreQuery_Urbanization.text(Session.DATACUSTOMER.Urbanization);

            if (Session.DATACUSTOMER.District == null)
                controls.lblPreQuery_District.text('');
            else
                controls.lblPreQuery_District.text(Session.DATACUSTOMER.District);


            if (Session.DATACUSTOMER.PostalCode == null)
                controls.lblPreQuery_PostalCode.text('');
            else
                controls.lblPreQuery_PostalCode.text(Session.DATACUSTOMER.PostalCode);

            if (Session.DATACUSTOMER.City == null)
                controls.lblPreQuery_City.text('');
            else
                controls.lblPreQuery_City.text(Session.DATACUSTOMER.City);


            if (Session.DATACUSTOMER.Department == null)
                controls.lblPreQuery_Department.text('');
            else
                controls.lblPreQuery_Department.text(Session.DATACUSTOMER.Department);


            if (Session.DATACUSTOMER.Reference == null)
                controls.lblPreQuery_Reference.text('');
            else
                controls.lblPreQuery_Reference.text(Session.DATACUSTOMER.Reference);



        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    };

    $.fn.CustomerQuery = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('CustomerQuery'),
                options = $.extend({}, $.fn.CustomerQuery.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('CustomerQuery', data);
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

    $.fn.CustomerQuery.defaults = {
    }

    $('#containerCustomerQuery', $('.modal:last')).CustomerQuery();

})(jQuery);