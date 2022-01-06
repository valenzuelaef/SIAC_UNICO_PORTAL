(function ($, undefined) {
    'use strict';

    var SessionTransf = function () { };
    SessionTransf.CALLE_LEG = "";
    SessionTransf.URBANIZACION_LEGAL = "";
    SessionTransf.POSTAL_LEGAL = "";
    SessionTransf.DEPARTEMENTO_LEGAL = "";
    SessionTransf.PROVINCIA_LEGAL = "";
    SessionTransf.DISTRITO_LEGAL = "";
    SessionTransf.PAIS_LEGAL = "";
    SessionTransf.CONTACTO = "";
    SessionTransf.REP_LEGAL = "";
    SessionTransf.NUM_DOC_REP_LEGAL = "";
    var Form = function ($element, options) {
        $.extend(this, $.fn.FormDataCustomer.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , li03Cli: $('#li03Cli', $element)
            , tab03Cli: $('#tab03Cli', $element)
            , lblPost_BusinessNameDataCustomer: $('#lblPost_BusinessNameDataCustomer', $element)
            , lblPost_AccountDataCustomer: $('#lblPost_AccountDataCustomer', $element)
            , lblPost_FullNameDataCustomer: $('#lblPost_FullNameDataCustomer', $element)
            , btnPostContact: $('#btnPostContact', $element)
            , lblPost_DocumentNumberDataCustomer: $('#lblPost_DocumentNumberDataCustomer', $element)
            , lblPost_PositionDataCustomer: $('#lblPost_PositionDataCustomer', $element)
            , lblPost_BirthPlaceDataCustomer: $('#lblPost_BirthPlaceDataCustomer', $element)
            , lblPost_BirthDateDataCustomer: $('#lblPost_BirthDateDataCustomer', $element)
            , lblPost_CivilStatusDataCustomer: $('#lblPost_CivilStatusDataCustomer', $element)
            , lblPost_CustomerContactDataCustomer: $('#lblPost_CustomerContactDataCustomer', $element)
            , lblPost_TradenameDataCustomer: $('#lblPost_TradenameDataCustomer', $element)
            , lblPost_LegalAgentDataCustomer: $('#lblPost_LegalAgentDataCustomer', $element)
            , lblPost_DocumentTypeDataCustomer: $('#lblPost_DocumentTypeDataCustomer', $element)
            , lblPost_RepresentativeDocumentNumberDataCustomer: $('#lblPost_RepresentativeDocumentNumberDataCustomer', $element)
            , lblPost_FaxDataCustomer: $('#lblPost_FaxDataCustomer', $element)
            , lblPost_PhoneContactDataCustomer: $('#lblPost_PhoneContactDataCustomer', $element)
            , lblPost_EmailDataCustomer: $('#lblPost_EmailDataCustomer', $element)
            , lblPost_AssessorDataCustomer: $('#lblPost_AssessorDataCustomer', $element)
            , lblPost_InvoiceAddressDataCustomer: $('#lblPost_InvoiceAddressDataCustomer', $element)
            , lblPost_InvoiceUrbanizationDataCustomer: $('#lblPost_InvoiceUrbanizationDataCustomer', $element)
            , lblPost_InvoiceDistrictDataCustomer: $('#lblPost_InvoiceDistrictDataCustomer', $element)
            , lblPost_InvoiceProvinceDataCustomer: $('#lblPost_InvoiceProvinceDataCustomer', $element)
            , lblPost_InvoiceCodeDataCustomer: $('#lblPost_InvoiceCodeDataCustomer', $element)
            , lblPost_InvoiceDepartamentDataCustomer: $('#lblPost_InvoiceDepartamentDataCustomer', $element)
            , lblPost_InvoiceCountryDataCustomer: $('#lblPost_InvoiceCountryDataCustomer', $element)
            , lblPost_LegalAddressDataCustomer: $('#lblPost_LegalAddressDataCustomer', $element)
            , lblPost_LegalUrbanizationDataCustomer: $('#lblPost_LegalUrbanizationDataCustomer', $element)
            , lblPost_LegalDistrictDataCustomer: $('#lblPost_LegalDistrictDataCustomer', $element)
            , lblPost_LegalProvinceDataCustomer: $('#lblPost_LegalProvinceDataCustomer', $element)
            , lblPost_LegalCodeDataCustomer: $('#lblPost_LegalCodeDataCustomer', $element)
            , lblPost_LegalDepartamentDataCustomer: $('#lblPost_LegalDepartamentDataCustomer', $element)
            , lblPost_LegalCountryDataCustomer: $('#lblPost_LegalCountryDataCustomer', $element)
            , lblPost_PhoneReferenceDataCustomer: $('#lblPost_PhoneReferenceDataCustomer', $element)
            , lblPost_SellerDataCustomer: $('#lblPost_SellerDataCustomer', $element)
        });

    };


    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            controls.btnPostContact.addEvent(that, 'click', that.getContact);

            this.render();


        },
        render: function () {

                this.setDataCustomer();
                this.setHistorical();

        },
        setDataCustomer: function () {

            var objCustomer = Session.DATACUSTOMER;            
            var controls = this.getControls();
            if (objCustomer.ContactFlag == 1) {
                if (objCustomer.CustomerType.toLowerCase() == "business" && objCustomer.DNIRUC.length == 11) {
                    controls.btnPostContact.show();
                } else {
                    controls.btnPostContact.hide();
                }
            } else {
                controls.btnPostContact.show();
            }


            if (objCustomer.Application == "HFC" || objCustomer.Application == "LTE") {
                controls.li03Cli.remove();
                controls.tab03Cli.remove();
                $(controls.btnPostContact).remove();
            }

            controls.lblPost_BusinessNameDataCustomer.text(objCustomer.BusinessName);
            controls.lblPost_AccountDataCustomer.text(objCustomer.Account);

            if (objCustomer.DNIRUC.length == 11) {
                if (objCustomer.DNIRUC.substring(0, 2) == "10") {
                    controls.lblPost_FullNameDataCustomer.text(objCustomer.FullName);
                    SessionTransf.CONTACTO = objCustomer.FullName;
                } else {
                    if (objCustomer.CustomerContact == null || objCustomer.CustomerContact == "") {
                        controls.lblPost_FullNameDataCustomer.text(objCustomer.FullName);
                        SessionTransf.CONTACTO = objCustomer.FullName;
                    } else {
                        controls.lblPost_FullNameDataCustomer.text(objCustomer.CustomerContact);
                        SessionTransf.CONTACTO = objCustomer.CustomerContact;
                    }
                }
            } else {
                controls.lblPost_FullNameDataCustomer.text(objCustomer.FullName);
                SessionTransf.CONTACTO = objCustomer.FullName;
            }

            controls.lblPost_DocumentNumberDataCustomer.text(objCustomer.DNIRUC);
            controls.lblPost_PositionDataCustomer.text(objCustomer.Position);
            controls.lblPost_BirthPlaceDataCustomer.text(objCustomer.BirthPlace);
            controls.lblPost_BirthDateDataCustomer.text(objCustomer.BirthDate);
            controls.lblPost_CivilStatusDataCustomer.text(objCustomer.CivilStatus);
            if (objCustomer.DNIRUC.length == 11) {
                if (objCustomer.DNIRUC.substring(0, 2) == "10") {
                    controls.lblPost_CustomerContactDataCustomer.text(objCustomer.FullName);
                } else {
                    if (objCustomer.CustomerContact == null || objCustomer.CustomerContact == "") {
                        controls.lblPost_CustomerContactDataCustomer.text(objCustomer.FullName);
                    } else {
                        controls.lblPost_CustomerContactDataCustomer.text(objCustomer.CustomerContact);
                    }
                }
            } else {
                controls.lblPost_CustomerContactDataCustomer.text(objCustomer.FullName);
            }
            controls.lblPost_TradenameDataCustomer.text(objCustomer.Tradename);
            controls.lblPost_LegalAgentDataCustomer.text(objCustomer.LegalAgent);
            SessionTransf.REP_LEGAL = objCustomer.LegalAgent;
            controls.lblPost_DocumentTypeDataCustomer.text(Session.DATACUSTOMER.DocumentType);

            controls.lblPost_RepresentativeDocumentNumberDataCustomer.text(objCustomer.DocumentNumber);
            SessionTransf.NUM_DOC_REP_LEGAL = objCustomer.DocumentNumber;
            controls.lblPost_FaxDataCustomer.text(objCustomer.Fax);
            controls.lblPost_PhoneContactDataCustomer.text(objCustomer.PhoneContact);
            controls.lblPost_EmailDataCustomer.text(objCustomer.Email);
            controls.lblPost_AssessorDataCustomer.text(objCustomer.Assessor);
            controls.lblPost_InvoiceAddressDataCustomer.text(objCustomer.InvoiceAddress);
            controls.lblPost_InvoiceUrbanizationDataCustomer.text(objCustomer.InvoiceUrbanization);
            controls.lblPost_InvoiceDistrictDataCustomer.text(objCustomer.InvoiceDistrict);
            controls.lblPost_InvoiceProvinceDataCustomer.text(objCustomer.InvoiceProvince);
            controls.lblPost_InvoiceCodeDataCustomer.text(objCustomer.InvoiceCode);
            controls.lblPost_InvoiceDepartamentDataCustomer.text(objCustomer.InvoiceDepartament);
            controls.lblPost_InvoiceCountryDataCustomer.text(objCustomer.InvoiceCountry);
            controls.lblPost_LegalAddressDataCustomer.text(objCustomer.LegalAddress);
            controls.lblPost_LegalUrbanizationDataCustomer.text(objCustomer.LegalUrbanization);
            controls.lblPost_LegalDistrictDataCustomer.text(objCustomer.LegalDistrict);
            controls.lblPost_LegalProvinceDataCustomer.text(objCustomer.LegalProvince);
            controls.lblPost_LegalCodeDataCustomer.text(objCustomer.LegalPostal);
            controls.lblPost_LegalDepartamentDataCustomer.text(objCustomer.LegalDepartament);
            controls.lblPost_LegalCountryDataCustomer.text(objCustomer.LegalCountry);

            controls.lblPost_PhoneReferenceDataCustomer.text(objCustomer.PhoneReference);
            if (Session.DATASERVICE != null) {
                if (Session.DATASERVICE.Seller == null)
                    controls.lblPost_SellerDataCustomer.text('');
                else
                    controls.lblPost_SellerDataCustomer.text(Session.DATASERVICE.Seller);
            }
            SessionTransf.CALLE_LEG = objCustomer.LegalAddress;
            SessionTransf.URBANIZACION_LEGAL = objCustomer.LegalUrbanization;
            SessionTransf.POSTAL_LEGAL = objCustomer.LegalPostal;
            SessionTransf.DEPARTEMENTO_LEGAL = objCustomer.LegalDepartament;
            SessionTransf.PROVINCIA_LEGAL = objCustomer.LegalProvince;
            SessionTransf.DISTRITO_LEGAL = objCustomer.LegalDistrict;
            SessionTransf.PAIS_LEGAL = objCustomer.LegalCountry;
        },
        getContact: function () {
            $.window.open({
                modal: true,
                title: "Contacto",
                url: '~/Dashboard/PostpaidDataCustomer/Contact',
                data: {},
                width: 1024,
                height: 280,
                buttons: {
                    Agregar: {
                        id: 'AddbtnPostPaid',
                        click: function (sender, args) {
                            $(this).addClass("pull-right");
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#PostpaidContact').FormPostpaidContact('fn_Agregar');
                        }

                    },
                    Guardar: {
                        id: 'SavebtnPostPaid',
                        click: function (sender, args) {
                            $(this).addClass("pull-right");
                            var modalJQuery = args.event.view.$;
                            modalJQuery('#PostpaidContact').FormPostpaidContact('fn_Guardar');
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
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
        setHistorical: function () {
            var objCustomer = Session.DATACUSTOMER;
            var parametro = {};

            parametro.strIdSession = Session.IDSESSION;
            parametro.strCustomerID = Session.DATACUSTOMER.CustomerID;
            parametro.plataforma = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            parametro.flagconvivencia = Session.DATACUSTOMER.flagConvivencia;
            $("#tbDatosCliente").html("");
            $("#tbDatosContacto").html("");
            $("#tbDatosDomicilio").html("");
            $("#tbDatosFacturacion").html("");
            $("#tbDatosRepLegal").html("");
            $("#tbDatosContactCliente").html("");
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parametro),
                url: '/Dashboard/PostpaidDataCustomer/GetDataHistory',
                error: function (data) {

                },
                success: function (response) {
                    console.log(response);
                    if (response.data.length != 0) {

                        for (var i = 0; i < response.data.length; i++) {

                            var _motivocli = "";
                            var _motivodataminor = "";
                            var _motivolegal = "";
                            var _motivofactura = "";
                            var _motivoreplegal = "";
                            var _motivocontact = "";
                            var _nombrecontacto = "";
                            if (response.data[i].updCliente == "1" || response.data[i].ChangeMot == "") {

                                var tr = '<tr><td>';

                                var TipDocTIT = "";
                                if (response.data[i].NroDoc.length == 11) {
                                    TipDocTIT = "RUC";
                                } else {
                                    if (response.data[i].DocType == "2" || response.data[i].DocType == 2) {
                                        TipDocTIT = "DNI";
                                    }
                                    if (response.data[i].DocType == "11") {
                                        TipDocTIT = "CIRE";
                                    }
                                    if (response.data[i].DocType == "12") {
                                        TipDocTIT = "CIE";
                                    }
                                    if (response.data[i].DocType == "13") {
                                        TipDocTIT = "CPP";
                                    }
                                    if (response.data[i].DocType == "14") {
                                        TipDocTIT = "CTM";
                                    }
                                    if (response.data[i].DocType == "4") {
                                        TipDocTIT = "Carnet de Extranjería";
                                    }
                                    if (response.data[i].DocType == "1") {
                                        TipDocTIT = "Pasaporte";
                                    }
                                }

                                if (response.data[i].NroDoc.length == 11) {
                                    tr = tr + response.data[i].BusinessName;
                                    tr = tr + ' ' + TipDocTIT;
                                    tr = tr + ' ' + response.data[i].NroDoc;
                                } else {
                                    tr = tr + response.data[i].FirstName + ' ' + response.data[i].LastName;
                                    tr = tr + ' ' + TipDocTIT;
                                    tr = tr + ' ' + response.data[i].NroDoc;
                                }

                                if (response.data[i].ChangeMot == "") {
                                    _motivocli = "Registro";
                                } else {
                                    _motivocli = response.data[i].ChangeMot;
                                }



                                tr = tr + '</td><td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivocli + '</td><tr>';
                                $("#tbDatosCliente").append(tr);

                            }

                            if (response.data[i].updDataMinor == "2" || response.data[i].ChangeMot == "") {

                                var changes = '';
                                var tr = '<tr>';
                                var td = '<td>';
                                var _td = '</td>';
                                var desc = "";
                                if (response.data[i].Email != "") {
                                    changes = changes + ' Correo: ' + response.data[i].Email;
                                }
                                if (response.data[i].Movil != "") {
                                    changes = changes + ' Tel. Referencia 1: ' + response.data[i].Movil;
                                }
                                if (response.data[i].Telephone != "") {
                                    changes = changes + ' Tel. Referencia 2: ' + response.data[i].Telephone;
                                }

                                if (response.data[i].ChangeMot == "") {
                                    _motivodataminor = "Registro";
                                } else {
                                    _motivodataminor = response.data[i].ChangeMot;
                                }
                                //  response.data[i].Email != "" && response.data[i].Movil != "" && response.data[i].Telephone != "") ||
                                if (_motivodataminor == "Registro") desc = '<td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivodataminor + '</td>';
                                if (response.data[i].Email == "" && response.data[i].Movil == "" && response.data[i].Telephone == "") desc = "";
                                else desc = '<td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivodataminor + '</td>';

                                if (changes != "") {
                                    var tr = '<tr><td>' + changes + '</td>' + desc + '<tr>';
                                    $("#tbDatosContacto").append(tr);
                                }
                               
                              

                            }

                            if (response.data[i].updDirLegal == "3" || response.data[i].ChangeMot == "") {

                                if (response.data[i].ChangeMot == "") {
                                    _motivolegal = "Registro";
                                } else {
                                    _motivolegal = response.data[i].ChangeMot;
                                }

                                var tr = '<tr><td>' + 'Calle. ' + response.data[i].AddressLegal + ' Referencia. ' + response.data[i].AddressNoteLegal + ' Departamento. ' + response.data[i].DepartmentLegal + ' Provincia. ' + response.data[i].ProvinceLegal + ' Distrito. ' + response.data[i].DistrictLegal + '</td><td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivolegal + '</td><tr>';
                                $("#tbDatosDomicilio").append(tr);

                            }

                            if (response.data[i].updDirFac == "4" || response.data[i].ChangeMot == "") {

                                if (response.data[i].ChangeMot == "") {
                                    _motivofactura = "Registro";
                                } else {
                                    _motivofactura = response.data[i].ChangeMot;
                                }

                                var tr = '<tr><td>' + 'Calle. ' + response.data[i].AddressFact + ' Referencia. ' + response.data[i].AddressNoteFact + ' Departamento. ' + response.data[i].DepartmentFact + ' Provincia. ' + response.data[i].ProvinceFact + ' Distrito. ' + response.data[i].DistrictFact + '</td><td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivofactura + '</td><tr>';
                                $("#tbDatosFacturacion").append(tr);

                            }

                            if (response.data[i].updRepLegal == "5" || response.data[i].ChangeMot == "") {

                                var tipdocrepleg = "";
                                if (response.data[i].DocType == "2" || response.data[i].DocType == 2) {
                                    tipdocrepleg = "DNI";
                                }
                                if (response.data[i].DocType == "11") {
                                    tipdocrepleg = "CIRE";
                                }
                                if (response.data[i].DocType == "12") {
                                    tipdocrepleg = "CIE";
                                }
                                if (response.data[i].DocType == "13") {
                                    tipdocrepleg = "CPP";
                                }
                                if (response.data[i].DocType == "14") {
                                    tipdocrepleg = "CTM";
                                }
                                if (response.data[i].DocType == "4") {
                                    tipdocrepleg = "Carnet de Extranjería";
                                }
                                if (response.data[i].DocType == "1") {
                                    tipdocrepleg = "Pasaporte";
                                }

                                if (response.data[i].ChangeMot == "") {
                                    _motivoreplegal = "Registro";
                                } else {
                                    _motivoreplegal = response.data[i].ChangeMot;
                                }

                                var tr = '<tr><td>' + response.data[i].LegalRep + ' ' + tipdocrepleg + ' ' + response.data[i].Fax + '</td><td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivoreplegal + '</td><tr>';
                                $("#tbDatosRepLegal").append(tr);

                            }

                            if (response.data[i].updContact == "6" || response.data[i].ChangeMot == "") {

                                if (response.data[i].ChangeMot == "") {
                                    _motivocontact = "Registro";
                                } else {
                                    _motivocontact = response.data[i].ChangeMot;
                                }
                                if ((response.data[i].Contact == null) || (response.data[i].Contact == "")) {
                                    _nombrecontacto = response.data[i].FirstName + ' ' + response.data[i].LastName;
                                } else {
                                    _nombrecontacto = response.data[i].Contact;
                                }

                                var tr = '<tr><td>' + _nombrecontacto + '</td><td>' + response.data[i].fechaRegistroCli + '</td><td>' + _motivocontact + '</td><tr>';
                                $("#tbDatosContactCliente").append(tr);

                            }

                        }

                        if (Session.DATACUSTOMER.DNIRUC.length == 11) {
                            ValidDoc = Session.DATACUSTOMER.DNIRUC.substring(0, 2);
                            if (ValidDoc != "10") {
                            } else if (ValidDoc == "10") {
                                document.getElementById("RepreLegalHHead").style.display = "none";
                                document.getElementById("DatosRepLegalcollapse").style.display = "none";
                                document.getElementById("ContactClientHHead").style.display = "none";
                                document.getElementById("DatosContactClientcollapse").style.display = "none";
                            }
                        } else {
                            document.getElementById("RepreLegalHHead").style.display = "none";
                            document.getElementById("DatosRepLegalcollapse").style.display = "none";
                            document.getElementById("ContactClientHHead").style.display = "none";
                            document.getElementById("DatosContactClientcollapse").style.display = "none";
                        }

                    } else {

                        var TipDoc = "";
                        var ValidDoc = "";
                        var TipoDocumento = Session.DATACUSTOMER.DocumentType;
                        if (Session.DATACUSTOMER.DNIRUC.length == 11) {

                            ValidDoc = Session.DATACUSTOMER.DNIRUC.substring(0, 2);
                            if (ValidDoc != "10") {
                                TipDoc = "RUC";
                            } else if (ValidDoc == "10") {
                                TipDoc = "RUC";
                            }

                        } else {
                            if ((TipoDocumento.indexOf('Carnet') > -1) || (TipoDocumento.indexOf('CARNET') > -1) || (TipoDocumento.indexOf('carnet') > -1)) {
                                TipDoc = "Carnet de Extranjería";
                            } else if ((TipoDocumento.indexOf('Cie') > -1) || (TipoDocumento.indexOf('CIE') > -1) || (TipoDocumento.indexOf('cie') > -1)) {
                                TipDoc = "CIE";
                            } else if ((TipoDocumento.indexOf('Cire') > -1) || (TipoDocumento.indexOf('CIRE') > -1) || (TipoDocumento.indexOf('cire') > -1)) {
                                TipDoc = "CIRE";
                            } else if ((TipoDocumento.indexOf('Cpp') > -1) || (TipoDocumento.indexOf('CPP') > -1) || (TipoDocumento.indexOf('cpp') > -1)) {
                                TipDoc = "CPP";
                            } else if ((TipoDocumento.indexOf('Ctm') > -1) || (TipoDocumento.indexOf('CTM') > -1) || (TipoDocumento.indexOf('ctm') > -1)) {
                                TipDoc = "CTM";
                            } else if ((TipoDocumento.indexOf('Dni') > -1) || (TipoDocumento.indexOf('DNI') > -1) || (TipoDocumento.indexOf('dni') > -1)) {
                                TipDoc = "DNI"
                            } else if ((TipoDocumento.indexOf('Pasaporte') > -1) || (TipoDocumento.indexOf('PASAPORTE') > -1) || (TipoDocumento.indexOf('pasaporte') > -1)) {
                                TipDoc = "Pasaporte";
                            } else if ((TipoDocumento.indexOf('Cip') > -1) || (TipoDocumento.indexOf('CIP') > -1) || (TipoDocumento.indexOf('cip') > -1)) {
                                TipDoc = "CIP";
                            }
                        }

                        var tr1 = '<tr><td>' + Session.DATACUSTOMER.BusinessName + ' ' + TipDoc + ' : ' + Session.DATACUSTOMER.DNIRUC + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';
                        var tr2 = '<tr><td>' + 'Correo: ' + Session.DATACUSTOMER.Email + ' Tel. Referencia 1: ' + Session.DATACUSTOMER.PhoneContact + ' Tel. Referencia 2: ' + Session.DATACUSTOMER.PhoneReference + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';
                        var tr3 = '<tr><td>' + 'Calle. ' + SessionTransf.CALLE_LEG + ' Referencia. ' + SessionTransf.URBANIZACION_LEGAL + ' Departamento. ' + SessionTransf.DEPARTEMENTO_LEGAL + ' Provincia. ' + SessionTransf.PROVINCIA_LEGAL + ' Distrito. ' + SessionTransf.DISTRITO_LEGAL + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';
                        var tr4 = '<tr><td>' + 'Calle. ' + Session.DATACUSTOMER.InvoiceAddress + ' Referencia. ' + Session.DATACUSTOMER.InvoiceUrbanization + ' Departamento. ' + Session.DATACUSTOMER.InvoiceDepartament + ' Provincia. ' + Session.DATACUSTOMER.InvoiceProvince + ' Distrito. ' + Session.DATACUSTOMER.InvoiceDistrict + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';
                        var tr5 = '<tr><td>' + SessionTransf.REP_LEGAL + ' ' + Session.DATACUSTOMER.DocumentType + ' : ' + SessionTransf.NUM_DOC_REP_LEGAL + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';
                        var tr6 = '<tr><td>' + SessionTransf.CONTACTO + '</td><td>' + Session.DATACUSTOMER.ActivationDate.substring(0, 10) + '</td><td>' + 'Registro' + '</td></tr>';

                        $("#tbDatosCliente").append(tr1);
                        $("#tbDatosContacto").append(tr2);
                        $("#tbDatosDomicilio").append(tr3);
                        $("#tbDatosFacturacion").append(tr4);
                        $("#tbDatosRepLegal").append(tr5);
                        $("#tbDatosContactCliente").append(tr6);

                        if (Session.DATACUSTOMER.DNIRUC.length == 11) {
                            ValidDoc = Session.DATACUSTOMER.DNIRUC.substring(0, 2);
                            if (ValidDoc != "10") {
                            } else if (ValidDoc == "10") {
                                document.getElementById("RepreLegalHHead").style.display = "none";
                                document.getElementById("DatosRepLegalcollapse").style.display = "none";
                                document.getElementById("ContactClientHHead").style.display = "none";
                                document.getElementById("DatosContactClientcollapse").style.display = "none";
                            }
                        } else {
                            document.getElementById("RepreLegalHHead").style.display = "none";
                            document.getElementById("DatosRepLegalcollapse").style.display = "none";
                            document.getElementById("ContactClientHHead").style.display = "none";
                            document.getElementById("DatosContactClientcollapse").style.display = "none";
                        }
                    }
                },
                complete: function () {

                }
            });

        },
        loadCustomerData: function () {
            var controls = this.getControls();
            var parametro = {};

            parametro.strIdSession = Session.IDSESSION;
            parametro.strTransaction = Session.IDSESSION;
            parametro.strTelefono = Session.DATACUSTOMER.Telephone;
            parametro.strCustomerId = Session.DATACUSTOMER.Account;

            $.app.ajax({
                type: 'POST',
                cache: false,
                async: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(parametro),
                url: '/Dashboard/PostpaidDataCustomer/GetCustomerChangeData',
                success: function (response) {

                    if (response.objCus != null) {
                        controls.lblPost_LegalAddressDataCustomer.text(response.objCus.TIPO_CLIENTE);
                        controls.lblPost_LegalUrbanizationDataCustomer.text(response.objCus.URBANIZACION_LEGAL);
                        controls.lblPost_LegalCodeDataCustomer.text(response.objCus.POSTAL_LEGAL);
                        controls.lblPost_LegalDepartamentDataCustomer.text(response.objCus.DEPARTEMENTO_LEGAL);
                        controls.lblPost_LegalProvinceDataCustomer.text(response.objCus.PROVINCIA_LEGAL);
                        controls.lblPost_LegalDistrictDataCustomer.text(response.objCus.DISTRITO_LEGAL);
                        controls.lblPost_LegalCountryDataCustomer.text(response.objCus.PAIS_LEGAL);

                        SessionTransf.CALLE_LEG = response.objCus.TIPO_CLIENTE;
                        SessionTransf.URBANIZACION_LEGAL = response.objCus.URBANIZACION_LEGAL;
                        SessionTransf.POSTAL_LEGAL = response.objCus.POSTAL_LEGAL;
                        SessionTransf.DEPARTEMENTO_LEGAL = response.objCus.DEPARTEMENTO_LEGAL;
                        SessionTransf.PROVINCIA_LEGAL = response.objCus.PROVINCIA_LEGAL;
                        SessionTransf.DISTRITO_LEGAL = response.objCus.DISTRITO_LEGAL;
                        SessionTransf.PAIS_LEGAL = response.objCus.PAIS_LEGAL;

                    }


                },
                complete: function () {

                }
            });
        }
    };


    $.fn.FormDataCustomer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormDataCustomer'),
                options = $.extend({}, $.fn.FormDataCustomer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormDataCustomer', data);
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

    $.fn.FormDataCustomer.defaults = {
    }

    $('#PospaidDataCustomer', $('.modal:last')).FormDataCustomer();
})(jQuery);


