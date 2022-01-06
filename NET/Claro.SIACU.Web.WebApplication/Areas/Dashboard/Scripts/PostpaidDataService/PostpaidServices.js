(function ($) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidServices.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , divDetail: $('#divDetail', $element)
            , lblServiceNumberHFC: $('#lblServiceNumberHFC', $element)
            , lblServiceNumberPostpaid: $('#lblServiceNumberPostpaid', $element)
            , lblCustomerTypeHFC: $('#lblCustomerTypeHFC', $element)
            , lblCustomerTypePostpaid: $('#lblCustomerTypePostpaid', $element)
            , lblBillingCycleHFC: $('#lblBillingCycleHFC', $element)
            , lblBillingCyclePostpaid: $('#lblBillingCyclePostpaid', $element)
            , lblPlanHFC: $('#lblPlanHFC', $element)
            , lblPlanPostpaid: $('#lblPlanPostpaid', $element)
            , lblCustomerHFC: $('#lblCustomerHFC', $element)
            , lblCustomerPostpaid: $('#lblCustomerPostpaid', $element)
            , tblContratos: $('#tblContratos', $element)
            , tblDetail: $('#tblDetail', $element)
            , errorDetailContract: $('#errorDetailContract', $element)
            , errorContract: $('#errorContract', $element)
            , dvHeaderServicePostpaid: $('#dvHeaderServicePostpaid', $element)
            , dvHeaderServiceHFC: $('#dvHeaderServiceHFC', $element)
            , spnDefault: $('#spnDefault', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.render();

        },
        render: function () {
            var that = this;

            that.HideColums();
            that.setCustomerInformation();
            that.fillApplicationData();
            that.ValidateOptions();
        },
        FLAG_COEXISTENCE_CONST: {
            MIGRATED: "1",
            PURE: "0",

        },
        FLAG_MIGRATED_CONST: {
            BSCS70: "0",
            MIGRADO: "1",
            TOBE: "2"
        },
        FLA_PLATFORM_CONST: {
            ASIS: "ASIS",
            TOBE: "TOBE"
        },
        ValidateOptions: function () {
            var that = this,
               controls = that.getControls();
            if (Session.ORIGINTYPE === 'LTE' || Session.ORIGINTYPE === 'HFC') {
                controls.spnDefault.show();
            } else {
                controls.spnDefault.hide();
            }
        },
        HideColums: function () {
            var that = this,
                    controls = that.getControls();

            that.table = controls.tblDetail.DataTable(

                {
                    "scrollY": "200px",
                    "scrollCollapse": true,
                    "paging": false,
                    "destroy": true,
                    "searching": false,
                    "scrollX": true,
                    "sScrollXInner": "100%",
                    "autoWidth": true,
                    "language": {
                        "lengthMenu": "Display _MENU_ records per page",
                        "zeroRecords": "No existen datos",
                        "info": " ",
                        "infoEmpty": " ",
                        "infoFiltered": "(filtered from _MAX_ total records)",
                        "emptyTable": "No existen datos"
                    },
                    "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false,
                            "searchable": false
                        },
                        {
                            "targets": [1],
                            "visible": false,
                            "searchable": false
                        },
                    ]
                });


            controls.tblContratos.on('click', '.radio', function () {
                $(this).siblings('.radio').attr('checked', false);
            });

        },

        createTableContracts: function (response) {
            var that = this,
                controls = that.getControls();
            that.tableContratos = controls.tblContratos.DataTable({
                info: false
                , paging: false
                , searching: false
                , select: 'single'
                , scrollY: 300
                , scrollCollapse: true
                , destroy: true
                , data: response.data.PhoneContracts
                , columns: [
                    { "data": null },
                    { "data": "CustomerCode" },
                    { "data": "ContractId" },
                    { "data": "Name" },
                    { "data": "State" },
                    { "data": "Date" },
                    { "data": "Reason" },
                { "data": "ContractIdPub" },
                { "data": "origen" }
                ]
                , columnDefs: [
                    {
                        targets: 0,
                        className: 'select-radio',
                        defaultContent: "",
                        orderable: false,
                    },
                     {
                         targets: 7,
                         visible: false,

                     },
                      {
                          targets: 8,
                          visible: false,

                     }
                ]
                , drawCallback: function () {
                    if (Session.DATASERVICE.Application !== 'POSTPAID')
                        $(this).DataTable().row(0).select();
                }
                , order: [[5, 'asc']]
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        createTableServices: function (response) {
            var that = this,
                controls = that.getControls();

            that.tableDetail = controls.tblDetail.DataTable({
                "info": false
                , "paging": false
                , "destroy": true
                , "searching": false
                , "scrollY": 300
                , "scrollCollapse": true
                , "ordering": false
                , "select": 'single'
                , "data": response.data.ContractServices
                , "columns": [
                    { "data": "GroupDescription" },
                    { "data": "GroupPos" },
                    { "data": "ServiceDescription" },
                    { "data": "ExclusiveDescription" },
                    { "data": "State" },
                    { "data": "ValidityDate" },
                    { "data": "SubscriptionFeeAmount" },
                    { "data": "AmountFixedCharge" },
                    { "data": "ModifiedShare" },
                    { "data": "ValidPeriods" }
                ]
                , "columnDefs": [
                    {
                        className: "text-center",
                        targets: [1, 3, 4, 5, 6, 7, 8, 9]
                    },
                     { visible: false, targets: 0 },
                     { visible: false, targets: 1 },
                     {
                         targets: 2,
                         render: function (data, type, full, meta) {
                             if (Session.DATACUSTOMER.Application == 'POSTPAID')
                                 return '<a href="#">' + data + '</a>';
                             else
                                 return data;
                         }
                     },
                     {
                         targets: 4,
                         render: function (data, type, full, meta) {
                             if (Session.DATACUSTOMER.Application == 'POSTPAID')
                                 switch (data) {
                                     case "A":
                                         data = "Activo";
                                         break;
                                     case "D":
                                         data = "Desactivo";
                                         break;
                                     case "P":
                                         data = "Activo";
                                 }

                             return data;
                         }
                     },
                     {
                         targets: 6,
                         render: function (data, type, full, meta) {
                             if (Session.DATACUSTOMER.Application == 'HFC' || Session.DATACUSTOMER.Application == 'LTE')
                                 if (data = "0") {
                                     data = "";
                                 }
                             return data;
                         }
                     },
                     {
                         targets: 9,
                         render: function (data, type, full, meta) {
                             if (Session.DATACUSTOMER.Application == 'POSTPAID')
                                 if (data) {
                                     if (data.substring(0, 2).trim() > 1) {
                                         data = data.substring(0, 2).trim() + ' DÍAS';
                                     }
                                 }
                             return data;
                         }
                     }
                ]
                , drawCallback: function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;

                    api.columns([0, 4], { page: 'current' }).data().each(function (column, i) {
                        if (i == 0) {
                            $.each(column, function (j, GroupDescription) {
                                if (last !== GroupDescription) {
                                    $(rows).eq(j).before(
                                        '<tr class="btn-danger"><td colspan="8">' + GroupDescription + '</td></tr>'
                                    );
                                    last = GroupDescription;
                                }
                            });
                        } else {
                            $.each(column, function (k, State) {
                                switch (State) {
                                    case 'A':
                                        api.row(k)
                                           .nodes()
                                           .to$()
                                           .addClass('text-success');
                                        break;
                                    case 'P':
                                        api.row(k)
                                           .nodes()
                                           .to$()
                                           .addClass('text-success');
                                        break;
                                    case 'D':
                                        api.row(k)
                                           .nodes()
                                           .to$()
                                           .addClass('text-danger');

                                }
                            });
                        }
                    });
                }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        clickRadioButton: function () {
            var that = this,
                controls = that.getControls();

            controls.tblContratos.find('tbody').on('click', 'tr', function () {
                if (!$(this).hasClass('selected')) {
                    var origen = that.tableContratos.row(this).data().origen;
                    if (origen == that.FLAG_MIGRATED_CONST.BSCS70) that.getContractServices(that.tableContratos.row(this).data().ContractId, that.tableContratos.row(this).data().origen);
                    else that.getContractServices(that.tableContratos.row(this).data().ContractIdPub, that.tableContratos.row(this).data().origen);

                }
                else { that.tableDetail.clear().draw(); }

            });
        },
        createDialogServiceBSCS: function () {

            $.window.open({
                modal: false,
                title: "Servicios BSCS relacionados al servicio comercial",
                url: '~/Dashboard/PostpaidDataService/ServicesBSCS',
                width: 500,
                height: 350,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        fillApplicationData: function () {
            if (Session.DATASERVICE.Application == 'POSTPAID')
                this.getPhoneContractPostPaid();
            else
                this.getPhoneContractHFCLTE();
        },
        getObjServicesBSCS: function () {
            return this.objServicesBSCS;
        },
        getServicesBSCS: function (send, args) {

            var that = this,
                objContractServices = {},
                objRow = that.tableDetail.row($(send).parents('tr')).data();

            objContractServices.strIdSession = Session.IDSESSION;
            objContractServices.ServiceCode = objRow.ServiceCode;

            args.event.preventDefault();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetServicesBSCS',
                data: JSON.stringify(objContractServices),
                complete: function () {
                    that.createDialogServiceBSCS();
                },
                success: function (response) {
                    that.setObjServicesBSCS(response);
                },
                error: function (ex) {
                    $.app.error({
                        id: 'contenedorServicesBSCS',
                        message: ex
                    });
                }
            });
        },
        getContractServices: function (contract, origen) {
            var controls = this.getControls();

            controls.errorDetailContract.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblDetail.find('tbody').html('<tr><td colspan="10" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            var that = this,
                oContractServices = {};
            oContractServices.origen = origen;
            oContractServices.ContractId = contract;
            oContractServices.Application = Session.DATACUSTOMER.Application;
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE")
            {
                if (Session.DATACUSTOMER.itm != null && Session.DATACUSTOMER.itm.origenRegistro == "02")
                {
                    if (Session.DATACUSTOMER.PlaneCodeInstallation.toUpperCase().indexOf('-F') > -1)
                    {
                        oContractServices.Application = "FTTH";
                    }
                }                
            }                     
            oContractServices.strIdSession = Session.IDSESSION;
            oContractServices.Telephone = Session.TELEPHONE;
            oContractServices.plataformaAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            oContractServices.coIdPub = contract;
            oContractServices.flagConvivencia = Session.DATACUSTOMER.flagConvivencia;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                $("#SubCharge").html("Cargo por suscripción al servicio según plan(con IGV)");
                $("#FixedCharge").html("Cargo fijo mensual al servicio según plan(con IGV)");
                $("#ModifyFee").html("Cuota modificada (s/.) con IGV");
            }
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetContractServices',
                data: JSON.stringify(oContractServices),
                complete: function () {
                    that.serviceDescription_click();
                },
                success: function (response) {
                    controls.tblDetail.find('tbody').html('');
                    that.createTableServices(response);
                },
                error: function (msger) {
                    controls.tblDetail.find('tbody').html('');
                    $.app.error({
                        id: 'errorDetailContract',
                        message: msger,
                        click: function () {
                            that.getContractServices();
                        }
                    });
                }
            });
        },

        getPhoneContractPostPaid: function () {
            var that = this,
                controls = that.getControls(),
                oPhoneContract = {},
                oDataService = Session.DATASERVICE;

            oPhoneContract.Telephone = oDataService.CellPhone;
            oPhoneContract.strIdSession = Session.IDSESSION;
            oPhoneContract.plataformaAT = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            oPhoneContract.flagMigrado = Session.DATACUSTOMER.objPostDataAccount.plataformaAT == that.FLA_PLATFORM_CONST.ASIS ? that.FLAG_MIGRATED_CONST.BSCS70 : Session.DATACUSTOMER.flagConvivencia == that.FLAG_COEXISTENCE_CONST.MIGRATED ? that.FLAG_MIGRATED_CONST.MIGRADO : that.FLAG_MIGRATED_CONST.TOBE;

            controls.errorContract.html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.tblContratos.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetPhoneContract',
                data: JSON.stringify(oPhoneContract),
                complete: function () {
                    that.clickRadioButton();
                },
                success: function (response) {
                    controls.tblContratos.find('tbody').html('');
                    that.createTableContracts(response);
                },
                error: function (msger) {
                    controls.tblContratos.find('tbody').html('');
                    $.app.error({
                        id: 'errorContract',
                        message: msger,
                        click: function () {
                            that.getPhoneContractPostPaid();
                        }
                    });
                }
            });
        },
        getPhoneContractHFCLTE: function () {
            var oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE;
            //RPB - Proy ONE
            var response = {
                data: {
                    PhoneContracts: [{
                        ContractId: oCustomer.ContractID,
                        CustomerCode: oCustomer.CustomerID,
                        Name: oCustomer.FullName,
                        State: oDataService.StateLine,
                        Date: oDataService.StateDate,
                        Reason: oDataService.Reason,
                        ContractIdPub: oCustomer.coIdPub,
                        origen: oCustomer.origen
                    }]
                }
            };
            this.createTableContracts(response);
            this.clickRadioButton();
            this.listContractServices();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        listContractServices: function () {
            var rowData = this.tableContratos.row(0).data();
            // RPB - Proyecto One Fija
            var origen = Session.DATACUSTOMER.origen;
            if (origen == this.FLAG_MIGRATED_CONST.BSCS70) this.getContractServices(rowData.ContractId, origen);
            else this.getContractServices(rowData.ContractIdPub, origen);
        },
        serviceDescription_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblDetail.find('tbody a').addEvent(that, 'click', that.getServicesBSCS);
        },
        setObjServicesBSCS: function (response) {
            this.objServicesBSCS = response;
        },
        setCustomerInformation: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oDataService = Session.DATASERVICE;

            if (Session.DATASERVICE.Application == 'POSTPAID') {
                controls.lblServiceNumberPostpaid.after(oDataService.CellPhone);
                controls.lblCustomerTypePostpaid.after(oCustomer.objPostDataAccount.CustomerType);
                controls.lblCustomerPostpaid.after(oCustomer.objPostDataAccount.CustomerCode);
                controls.lblCustomerPostpaid.after(oCustomer.BusinessName);
                controls.lblBillingCyclePostpaid.after(oCustomer.objPostDataAccount.BillingCycle);
                controls.lblPlanPostpaid.after(oDataService.Plan);
                controls.dvHeaderServiceHFC.css('display', 'none');
                controls.dvHeaderServicePostpaid.css('display', 'block');


            } else {
                controls.lblServiceNumberHFC.html('Customer ID:&nbsp;').after(oCustomer.CustomerID);
                controls.lblCustomerTypeHFC.html('Contacto:&nbsp;').after(oCustomer.FullName);
                controls.lblCustomerHFC.html('Cliente / Razón Social:&nbsp;').after(oCustomer.FullName);
                controls.lblBillingCycleHFC.after(oCustomer.objPostDataAccount.BillingCycle);
                controls.lblPlanHFC.after(oDataService.Plan);
                controls.dvHeaderServiceHFC.css('display', 'block');
                controls.dvHeaderServicePostpaid.css('display', 'none');

            }

        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidServices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['getObjServicesBSCS'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidServices'),
                options = $.extend({}, $.fn.PostPaidServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidServices', data);
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

    $.fn.PostPaidServices.defaults = {
    }

    $('#contenedorServices', $('.modal:last')).PostPaidServices();
})(jQuery);