
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            ddlTipoBusqueda: $('#ddlTipoBusqueda'),
            txtCriteriaValue: $("#txtCriteriaValue"),
            panelPostpaid: $('#panelPostpaid'),
            panelPrepaid: $('#panelPrepaid'),
            panelOlo: $('#panelOlo'),
            divPrepaid: $('#divPrepaid'),
            divPostpaid: $('#divPostpaid'),
            divOlo: $('#divOlo')
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
            var controls = this.getControls();
            that.CustomerProducts();
            that.printProducts();
        },

        printProducts: function () {
            var that = this;
            var controls = this.getControls();

            if (Session.USERACCESS.isOnlyOLO) {
                //controls.panelPostpaid.hide();
                that.PostpaidProducts();
                controls.panelPrepaid.hide();
                //that.OloProducts();
            }
            else {
            that.PostpaidProducts();
            that.PrepaidProducts();
                //that.OloProducts();
            }
        },


        applicativeRoute: window.location.href,
        CustomerProducts: function () {

            if (Session.CUSTOMERPRODUCT.isRedirecBussines === "not") {
                $('#lbldocIdentidad').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentIdentity);
                $('#lblNombresApellidos').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].Names);
                $('#lbltipoDocIdentidad').text(Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentType);
            }
            else if (Session.CUSTOMERPRODUCT.isRedirecBussines === "ok") {
                $('#lbldocIdentidad').text(Session.CUSTOMERPRODUCT.DocumentIdentity);
                $('#lblNombresApellidos').text(Session.CUSTOMERPRODUCT.Names);
                $('#lbltipoDocIdentidad').text(Session.CUSTOMERPRODUCT.DocumentType);
            }
            else {
                showAlert("No se pudo cargar los datos del cliente");

            }

        },
        PostpaidProducts: function () {
            var that = this;
            var controls = this.getControls();

            var valTypeDoc, valDocIdentity;
            
            $("#LoadingPostPaid").html('');
            var stUrlLogo = + "/Images/loading2.gif";
            controls.divPostpaid.find('table tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            if (Session.CUSTOMERPRODUCT.isRedirecBussines === "ok") {
                
                valTypeDoc = Session.CUSTOMERPRODUCT.DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.DocumentIdentity;
                
            }
            else {
                
                valTypeDoc = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentIdentity;
            }

            var data = { strDocumentType: valTypeDoc, strDocumentIdentity: valDocIdentity, strIdSession: Session.IDSESSION };
            var strUrl = '~/Dashboard/Home/PostpaidProducts';
            
           
            if (Session.CUSTOMERPRODUCT.listProducPost !== null && typeof Session.CUSTOMERPRODUCT.listProducPost !== 'undefined' && Session.CUSTOMERPRODUCT.listProducPost !== '[]') {
               
                that.PostpaidTable(JSON.parse(Session.CUSTOMERPRODUCT.listProducPost));
            } else {
               
                $.app.ajax({
                    async: false,
                    type: "POST",
                    url: strUrl,
                    data: data,
                    success: function (result) {
                        Session.CUSTOMERPRODUCT.listProducPost = JSON.stringify(result.data.olistProducPost);

                        that.PostpaidTable(JSON.parse(Session.CUSTOMERPRODUCT.listProducPost));


                    },
                    error: function (ex) {
                        controls.divPostpaid.find('table tbody').html('');
                        $.app.error({
                            id: 'LoadingPostPaid', message: ex,
                            click: function () {
                                that.PostpaidProducts();
                            }
                        });

                    }
                });
            }




        },

        Cuentas: [],

        PostpaidTable: function (listProducPost) {

            $('#tblPostpaid').DataTable({
                "order": [[5, "desc"], [0, 'desc']],
                "scrollY": "180px",
                "scrollCollapse": true,
                "info": false,
                "select": "single",
                "paging": false,
                "destroy": true,
                "searching": false,
                "data": listProducPost,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "Product" },
                    { "data": "Account" },
                    { "data": "CodClient" },
                    { "data": "TypeClient" },
                    { "data": "DateAccountActivation" },
                    { "data": "NumberServicesActive" },
                    { "data": "NumberServicesNotActive" },
                    { "data": "Id" }
                ]

                                , "columnDefs": [{
                                    targets: 5,
                                    render: function (data, type, full, meta) {
                                        return '<a class="btn btn-default btn-xs" style="width: 40px;" href="javascript:OpenDetail(\'' + full.CodClient + '\',\'' + full.CodProduct + '\',\'' + full.IdPlan + '\',' + '\'A' + '\',\'' + full.TypeProduct + '\',\'' + full.NumberServicesActive + '\')">' + full.NumberServicesActive + '</a>';
                                    }
                                },
                                {
                                    targets: 6,
                                    render: function (data, type, full, meta) {
                                        return '<a class="btn btn-default btn-xs" style="width: 40px;" href="javascript:OpenDetail(\'' + full.CodClient + '\',\'' + full.CodProduct + '\',\'' + full.IdPlan + '\',' + '\'I' + '\',\'' + full.TypeProduct + '\',\'' + full.NumberServicesNotActive + '\')">' + full.NumberServicesNotActive + '</a>';
                                    }
                                }
                                ,
                                {
                                    targets: [7],
                                    visible: false
                                }
                                ]

            });

            $("#LoadingPostPaid").hide();
            $("#collapse1").show();
        },

        PrepaidProducts: function () {
            var that = this;
            var controls = this.getControls();
            var valTypeDoc, valDocIdentity;
            
            $("#LoadingPrepaid").html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.divPrepaid.find('table tbody').html('<tr><td colspan="5" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            if (Session.CUSTOMERPRODUCT.isRedirecBussines === "ok") {
                valTypeDoc = Session.CUSTOMERPRODUCT.DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.DocumentIdentity;
            }
            else {
                valTypeDoc = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentIdentity;

            }


            var data = { strDocumentType: valTypeDoc, strDocumentIdentity: valDocIdentity, strState: '0', strIdSession: Session.IDSESSION };
            var strUrl = '~/Dashboard/Home/PrepaidProducts';

            if (Session.CUSTOMERPRODUCT.listProducPre !== null && typeof Session.CUSTOMERPRODUCT.listProducPre !== 'undefined' && Session.CUSTOMERPRODUCT.listProducPre !== '[]') {

                that.PrepaidTable(JSON.parse(Session.CUSTOMERPRODUCT.listProducPre));
            } else {

                $.app.ajax({
                    async: true,
                    type: "POST",
                    url: strUrl,
                    data: data,
                    success: function (result) {
                        Session.CUSTOMERPRODUCT.listProducPre = JSON.stringify(result.data);
                        that.PrepaidTable(JSON.parse(Session.CUSTOMERPRODUCT.listProducPre));
                        


                    },

                    error: function (ex) {
                        controls.divPrepaid.find('table tbody').html('');
                        $.app.error({
                            id: 'LoadingPrepaid', message: ex,
                            click: function () {
                                that.PrepaidProducts();
                            }
                        });

                    }
                });
            }
        },
        PrepaidTable: function (listProducPre) {
            $('#tblPrepaid').DataTable({
                "order": [[4, "asc"], [0, 'asc']],
                "scrollY": "180px",
                "scrollCollapse": true,
                "info": false,
                "select": "single",
                "paging": false,
                "destroy": true,
                "searching": false,
                "data": listProducPre,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "TypeProduct" },
                    { "data": "Telephone" },
                    { "data": "DateActive" },
                    { "data": "State" },
                    { "data": "Id" }
                ]
                , "columnDefs": [{
                    targets: 1,
                    render: function (data, type, full, meta) {

                        return '<a href="javascript:search(\'' + full.Telephone + '\',\'' + full.TypeProduct +  '\', true)">' + full.Telephone + '</a>';
                    }
                }
                , {
                    targets: [4],
                    visible: false
                }
                ]
            });

            $("#LoadingPrepaid").hide();
            $("#collapse2").show();
        },

        OloProducts: function () {
            var that = this;
            var controls = this.getControls();

            $("#LoadingOlo").html('');
            var stUrlLogo = "/Images/loading2.gif";
            controls.divOlo.find('table tbody').html('<tr><td colspan="5" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            var valTypeDoc, valDocIdentity;
            if (Session.CUSTOMERPRODUCT.isRedirecBussines == "ok") {
                valTypeDoc = Session.CUSTOMERPRODUCT.DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.DocumentIdentity;
            }
            else {
                valTypeDoc = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentType;
                valDocIdentity = Session.CUSTOMERPRODUCT.listDataCustomerModel[0].DocumentIdentity;
            }

            var objCustomerRequest = {
                CustomerByDocument: {
                    idTipDocCliente: that.CodTypeDocOLO(valTypeDoc),
                    docCliente: valDocIdentity
                },
                Flag: 'ByDocument'
            }

            $.app.ajax({
                async: false,
                type: 'POST',
                dataType: 'json',
                data: { strIdSession: Session.IDSESSION, objCustomerRequest: objCustomerRequest },
                url: '/SISOMV/Dashboard/Olo/GetCustomer',
                success: function (response) {
                    if (response != null
                && response.data.responseData != null
                    && response.data.responseStatus.codigoRespuesta == 0
                        && response.data.responseData.clienteType != null
                            && response.data.responseData.listaCuentas != null
                                && response.data.responseData.listaCuentas.cuentas != null) {

                    that.OloTable(response);
                    } else {
                        controls.divOlo.find('table tbody').html('<tr><td colspan="5" align="center">No existen datos </td></tr>');
                    }
                },
                error: function (ex) {
                    controls.divOlo.find('table tbody').html('');
                    $.app.error({
                        id: 'LoadingOlo', message: ex,
                        click: function () {
                            that.OloProducts();
                        }
                    });
                }
            });


        },

        CodTypeDocOLO: function (valTypeDoc) {
            var that = this,
               controls = that.getControls(),
               data = { strIdSession: Session.IDSESSION };

            var codTypeDoc = valTypeDoc;
         
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                async: false,
                url: '~/SISOMV/Dashboard/Common/GetDocumentTypeList',
                data: data,
                success: function (response) {
                    if (response.data.codigoRespuesta == '0') {
                        var listTypeDoc = response.data.listaTipoDocCliente;
                        for (var i = 0; i < listTypeDoc.length; i++) {
                            if (listTypeDoc[i].tipoDocCliente.descripcion == valTypeDoc.toUpperCase().trim()) {
                                codTypeDoc = listTypeDoc[i].tipoDocCliente.idDocumento;
                                break;
                            }
                        }
                    }

                },
                 error: function (ex) {
                    console.log(ex);
                }
            });
         
            return codTypeDoc;
        },

        OloTable: function (response) {
            var that = this;
            var listProductOlo = [];


                that.Cuentas = response.data.responseData.listaCuentas.cuentas;

            for (var i = 0, len = that.Cuentas.length; i < len; i++) {

                if (that.Cuentas[i].listaAcuerdos != null && that.Cuentas[i].listaAcuerdos.acuerdos != null) {

                    for (var j = 0, count = that.Cuentas[i].listaAcuerdos.acuerdos.length; j < count; j++) {

                        listProductOlo.push(that.Cuentas[i].listaAcuerdos.acuerdos[j]);

                    }
                }
            }
            



            $('#tblOlo').DataTable({
                "order": [[4, "asc"], [0, 'asc']],
                "scrollY": "180px",
                "scrollCollapse": true,
                "info": false,
                "select": "single",
                "paging": false,
                "destroy": true,
                "searching": false,
                "data": listProductOlo,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": "idCuenta" },
                    { "data": "numero.numero" },
                    { "data": "getSubscriptionProfileResponse.status.startDate" },
                    { "data": "estado" },
                    { "data": "idAcuerdo" }
                ]
                , "columnDefs": [
                    {
                        targets: 0,
                        render: function () {

                            return 'INTERNET OLO PREPAGO';
                        }
                    },

                    {
                    targets: 1,
                    render: function (data, type, full, meta) {

                            return '<a href="javascript:searchOlo(' + full.numero.numero + ')">' + full.numero.numero + '</a>';
                    }
                    },

                    {
                        targets: 2,
                        render: function (data, type, full, meta) {

                            return moment(data).format('DD/MM/YYYY hh:mm:ss a');
                }
                    },

                    {
                        targets: 3,
                        render: function (data, type, full, meta) {
                            var state;
                            if (data != '1') {
                                state = "Inactivo";
                            }
                            else {
                                state = "Activo";
                            }
                            return state;
                        }
                    },

                    {
                    targets: [4],
                    visible: false
                }
                ]
            });

            $("#LoadingOlo").hide();
            $("#collapse3").show();
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
            allowedMethods = ['builtContent', 'showDialogLoad', 'GetOptions', 'displayContentOlo'];

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
    $('#contenedor-products').form();
});


function OpenDetail(CodClient, CodProduct, IdPlan, State, TypeProduct, NumberServicesActive) {
    var valNumberServicesActive = parseInt(NumberServicesActive);
    if (valNumberServicesActive > 0) {

        var strUrl = '~/Dashboard/Home/ProductDetails';
        Session.NotEvalState = true;
        Session.IsPostPaid = true;
        $.window.open({
            modal: false,
            title: "LÍNEAS ASOCIADAS A LA CUENTA",
            url: strUrl,
            buttons: {
                Cerrar: {
                    click: function () {
                        this.close();
                    }
                }
            },
            data: { strIdSession: Session.IDSESSION, strCustomerId: CodClient, strCodeProduct: CodProduct, strIdPlan: IdPlan, strState: State, strProductType: TypeProduct },
            width: 1231,
            height: 550,
        });
    }
    else {
        return;
    }

}


function search(valTelephone, valTypeProduct, IsPrepaid) {
    var stUrlLogo = "/Images/loading2.gif";
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

    var valSearchType = ((valTypeProduct === "HFC" || valTypeProduct === "LTE" || valTypeProduct === "FTTH") ? "3" : "1");

    var data = { strSearchType: valSearchType, strSearchValue: valTelephone, strIdSession: Session.IDSESSION, NotEvalState: false, FlagSearchType: false, userId: Session.USERACCESS.userId, IsPrepaid: (typeof IsPrepaid != 'undefined' && IsPrepaid != null ? IsPrepaid : false),IsPostPaid:false };
    var strUrl = '~/Home/ValidateQuery';
    var isclosed = false;
    Session.TELEPHONE = valTelephone;
    $.app.ajax({
        type: "POST",
        dataType: "json",
        url: strUrl,
        data: data,
        complete: function () {

            if (data.strSearchType === '3' || data.strSearchType === '1') {
                window.opener.$("#navbar-contenedor").form("showDialogLoad");

            }
            if (isclosed) return false;
            window.close();
        },
        success: function (result) {
            if (result.Options == null) {
                window.opener.$('#divContenido').html("");
                window.opener.showAlert('Usted no está autorizado para consultar este tipo de producto', this.strTitleMessage);
                isclosed = true;
                return false;
            }
            if (result.OriginType !== "" && result.data !== null) {
                window.opener.$("#navbar-contenedor").form("builtContent",
                {
                    paramResult: JSON.stringify(result),
                    paramSearchType: '1',
                    paramSearchValue: data.strSearchValue
                }
                );
            } else {

                showAlert('No se encontraron Resultados', this.strTitleMessage);
                $('#divContenido').html("");
            }
        }
    });
}
function searchOlo(valTelephone) {
    var stUrlLogo = "/Images/loading2.gif";
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

    //PRIMERO Buscar en la session el customer 

    var CustomerOlo = window.opener.$("#navbar-contenedor").form("GetCustomerOlo", valTelephone);

    var isclosed = false;
    Session.TELEPHONE = valTelephone;
    var option = window.opener.$("#navbar-contenedor").form("GetOptions");

    if (option == null) {
                window.opener.$('#divContenido').html("");
                window.opener.showAlert('Usted no está autorizado para consultar este tipo de producto', this.strTitleMessage);
                isclosed = true;
                return false;
            }
    if (Session.DATACUSTOMER.clienteType != null) {

        window.opener.$("#navbar-contenedor").form("displayContentOlo");


            } else {

                showAlert('No se encontraron Resultados', this.strTitleMessage);
                $('#divContenido').html("");
            }
    window.close();






}