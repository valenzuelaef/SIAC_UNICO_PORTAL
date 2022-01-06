(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidHistoricalPackage.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCustomerCodeHistoricalPackage: $('#lblCustomerCodeHistoricalPackage', $element)
            , lblNameCustomerHistoricalPackage: $('#lblNameCustomerHistoricalPackage', $element)
            , lblNameContactHistoricalPackage: $('#lblNameContactHistoricalPackage', $element)
            , lblClicleBillingHistoricalPackage: $('#lblClicleBillingHistoricalPackage', $element)
            , lblTotalMbHistoricalPackage: $('#lblTotalMbHistoricalPackage', $element)
            , lblBagAvailableHistoricalPackage: $('#lblBagAvailableHistoricalPackage', $element)
            , containerDateRangeHistoricalPackage: $('#containerDateRangeHistoricalPackage', $element)
            , txtDateStartHistoricalPackage: $('#txtDateStartHistoricalPackage', $element)
            , txtDateEndHistoricalPackage: $('#txtDateEndHistoricalPackage', $element)
            , cboHistoricalPackageType: $('#cboHistoricalPackageType', $element)
            , btnPost_SearchHistoricalPackage: $('#btnPost_SearchHistoricalPackage', $element)
            , btnPost_CleanHistoricalPackage: $('#btnPost_CleanHistoricalPackage', $element)
            , tblHistoricalPackage: $('#tblHistoricalPackage', $element)
            , tblHistoricalPackageToBe: $('#tblHistoricalPackageToBe', $element)
            , lblTotalRowsHistoricalPackage: $('#lblTotalRowsHistoricalPackage', $element)
            , lblMensaje: $('#lblMensaje', $element)
            , hiddenDataBag: $('#hiddenDataBag', $element)
            , tblHistoryAsis: $('#tblHistoryAsis', $element)
            , tblHistoryTobe: $('#tblHistoryTobe', $element)

        });

    }

    Form.prototype = {
        constructor: Form,
        setFlagOne: "",
        init: function () {
            var that = this, controls = that.getControls();
            controls.btnPost_SearchHistoricalPackage.addEvent(that, 'click', that.btnPost_SearchHistoricalPackage_click);

            controls.btnPost_CleanHistoricalPackage.addEvent(that, 'click', that.btnPost_CleanHistoricalPackage_click);

            controls.containerDateRangeHistoricalPackage.datepicker({
                format: 'dd/mm/yyyy',
                endDate: $.app.date.format(new Date(), { format: 'dd/mm/yy' })
            });
            that.render();
        },

        render: function () {
            var that = this, controls = that.getControls();
            controls.lblCustomerCodeHistoricalPackage.after(Session.DATACUSTOMER.Account);
            controls.lblNameCustomerHistoricalPackage.after(Session.DATACUSTOMER.BusinessName);
            controls.lblNameContactHistoricalPackage.after(Session.DATACUSTOMER.FullName);
            controls.lblClicleBillingHistoricalPackage.after(Session.DATACUSTOMER.objPostDataAccount.BillingCycle);
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT !== "TOBE") {
            that.getMBTotal();
            that.getMBAvailable();
            controls.tblHistoryTobe.hide();
            } else {
                controls.hiddenDataBag.hide();
                controls.tblHistoryAsis.hide();
                controls.tblHistoryTobe.show();
            }
            that.getTypePackage();
            that.getHistoricalPackage();
        },

        getHistoricalPackage: function () {
            var that = this,
               controls = that.getControls(),

              objTotalMbPurchasedPackageRequestPospaid = {
                  strIdSession: Session.IDSESSION,
                  plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                  flagConvivencia: Session.DATACUSTOMER.flagConvivencia,
                  objPackageODCS: {
                      Msisdn: Session.DATASERVICE.CellPhone
                      , ActivationDate: controls.txtDateStartHistoricalPackage.val()
                      , ExpirationDate: controls.txtDateEndHistoricalPackage.val()
                      , TypePackage: controls.cboHistoricalPackageType.find('option:selected').val()
                      , coIdPub: Session.DATACUSTOMER.coIdPub
                  }
              };

            var stUrlLogo = "/Images/loading2.gif";

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {

                controls.tblHistoricalPackageToBe.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            } else {

                controls.tblHistoricalPackage.find('tbody').html('<tr><td colspan="6" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonHistoricalPackage',
                data: JSON.stringify(objTotalMbPurchasedPackageRequestPospaid),
                success: function (response) {
                    if (response.data != null) {
                        console.log('GetJsonHistoricalPackage');
                        console.log(response.data);

                        that.setFlagOne = response.data.objHistoricalPackageHelper.FlagOne;

                        controls.lblTotalRowsHistoricalPackage.empty();
                        controls.lblTotalRowsHistoricalPackage.append("Total de Registros: ");
                        controls.lblTotalRowsHistoricalPackage.append(response.data.objHistoricalPackageHelper.TotalRows);


                        if (response.data.objHistoricalPackageHelper.IsVisibleMensaje == true) {
                            controls.lblMensaje.val(response.data.objHistoricalPackageHelper.strMensaje);
                        } else {
                            controls.lblMensaje.val("");
                        }
                        if (response.data.objHistoricalPackageHelper.strMensajeAlert != null && response.data.objHistoricalPackageHelper.strMensajeAlert != "")
                            showAlert(response.data.objHistoricalPackageHelper.strMensajeAlert, "Mensaje Histórico Paquetes");


                        if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                            var lista;
                               console.log(response.data.objHistoricalPackageHelper.lstPackageODCS);
                               lista = response.data.objHistoricalPackageHelper.lstPackageODCS;
                               that.setTableHistoricalPackageTobe(lista);
                            } else {
                                that.setTableHistoricalPackage(response.data.objHistoricalPackageHelper.lstPackageODCS);
                            }
                        
                    } else {

                        if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                            that.setTableHistoricalPackageTobe(null);
                        } else {
                            that.setTableHistoricalPackage(null);
                        }
                    }
                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalPackage',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });
        },

       

        getHistoricalPackageOneJanus: function () {
            var that = this,
               controls = that.getControls(),
               Flag = that.setFlagOne,
            objTotalMbPurchasedPackageRequestPospaid = {
                strIdSession: Session.IDSESSION,
                plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                flagConvivencia: Session.DATACUSTOMER.flagConvivencia,
                objPackageODCS: {
                    Msisdn: Session.DATASERVICE.CellPhone
                    , ActivationDate: controls.txtDateStartHistoricalPackage.val()
                    , ExpirationDate: controls.txtDateEndHistoricalPackage.val()
                    , TypePackage: controls.cboHistoricalPackageType.find('option:selected').val()
                    , coIdPub: Session.DATACUSTOMER.coIdPub
                },
                strFlagOne: Flag
            };
            var stUrlLogo = "/Images/loading2.gif";

            var ruta = '';
            var data;
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                controls.tblHistoricalPackage.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
                ruta = '~/Dashboard/PostpaidDataService/GetJsonHistoricalPackage';
                data = JSON.stringify(objTotalMbPurchasedPackageRequestPospaid)
            } else {
                controls.tblHistoricalPackage.find('tbody').html('<tr><td colspan="6" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
                ruta = '~/Dashboard/PostpaidDataService/GetJsonHistoricalPackageOneJanus';
                data = JSON.stringify(objTotalMbPurchasedPackageRequestPospaid)
            }
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: ruta,
                data: data,
                success: function (response) {
                    if (response.data != null) {
                        that.setFlagOne = response.data.objHistoricalPackageHelper.FlagOne;
                        controls.lblTotalRowsHistoricalPackage.empty();
                        controls.lblTotalRowsHistoricalPackage.append("Total de Registros: ");
                        controls.lblTotalRowsHistoricalPackage.append(response.data.objHistoricalPackageHelper.TotalRows);
                        if (response.data.objHistoricalPackageHelper.IsVisibleMensaje == true) {
                            controls.lblMensaje.val(response.data.objHistoricalPackageHelper.strMensaje);
                        } else {
                            controls.lblMensaje.val("");
                        }
                        if (response.data.objHistoricalPackageHelper.strMensajeAlert != null && response.data.objHistoricalPackageHelper.strMensajeAlert != "")
                            showAlert(response.data.objHistoricalPackageHelper.strMensajeAlert, "Mensaje Histórico Paquetes");

                        if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                            that.setTableHistoricalPackageTobe(response.data.objHistoricalPackageHelper.lstPackageODCS);
                        } else {
                            that.setTableHistoricalPackage(response.data.objHistoricalPackageHelper.lstPackageODCS);
                        }

                    } else {

                        if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                            that.setTableHistoricalPackageTobe(null);
                        } else {
                            that.setTableHistoricalPackage(null);
                        }

                    }
                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalPackage',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });
        },
        getTypePackage: function () {
            var that = this,
               controls = that.getControls(),

             objTypePakageRequestCommon = {
                 strIdSession: Session.IDSESSION,
                 Plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT
             };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonTypePackage',
                data: JSON.stringify(objTypePakageRequestCommon),
                success: function (response) {
                    controls.cboHistoricalPackageType
                        .populateDropDown({
                            dataSource: response.data.lstConsumptionTypeHelper,
                            fieldValue: 'Code',
                            fieldText: 'Description'
                        });


                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalPackage',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });
        },
        getMBAvailable: function () {
            var that = this,
               controls = that.getControls(),

              objMbBagRequestPospaid = {
                  strIdSession: Session.IDSESSION,
                  strMsidn: Session.DATASERVICE.CellPhone,
                  strCustomerId: Session.DATACUSTOMER.CustomerID

              };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonMBAvailable',
                data: JSON.stringify(objMbBagRequestPospaid),
                success: function (response) {
                    controls.lblBagAvailableHistoricalPackage.after(response.data.objMbAvailableHelper.strMbAvailable);
                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalPackage',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });
        },

        getMBTotal: function () {
            var that = this,
               controls = that.getControls(),

              objTotalMbPurchasedPackageRequestPospaid = {
                  strIdSession: Session.IDSESSION,
                  objPackageODCS: {
                      Msisdn: Session.DATASERVICE.CellPhone
                      , ActivationDate: controls.txtDateStartHistoricalPackage.val()
                      , ExpirationDate: controls.txtDateEndHistoricalPackage.val()
                  }
              };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetJsonMBTotal',
                data: JSON.stringify(objTotalMbPurchasedPackageRequestPospaid),
                success: function (response) {
                    controls.lblTotalMbHistoricalPackage.after(response.data.objMbTotalHelper.TotalMBCicle);

                },
                error: function (msger) {

                    $.app.error({
                        id: 'idContainerHistoricalPackage',
                        message: msger,
                        click: function () {
                            that.getBalanceShared();
                        }
                    });
                }
            });
        },

        btnPost_SearchHistoricalPackage_click: function () {
            var that = this;

            that.getHistoricalPackageOneJanus();
        },

        btnPost_CleanHistoricalPackage_click: function () {
            location.reload();
        },
      
        setTableHistoricalPackage: function (data) {
            var controls = this.getControls(),
                tbl = controls.tblHistoricalPackage;
            console.log(4);
            if (data == null) data = {};
            controls.tblHistoricalPackage.find('tbody').html('');
            controls.tblHistoricalPackage.DataTable({
                info: false
               , paging: false
               , destroy: true
               , searching: false
               , scrollX: true
               , sScrollXInner: "100%"
               , autoWidth: true
               , data: data
               , select: 'single'
               , columns: [

                   { "data": "NameBag" },
                   { "data": "validity" },
                   { "data": "cost" },
                   { "data": "TypePackage" },
                   { "data": "AcquisitionDate" },
                   { "data": "State" }

               ]
               , columnDefs: [
                   {
                       className: "text-center",
                       targets: [0, 1, 2, 3]
                   }
               ]

               , language: {
                   lengthMenu: "Display _MENU_ records per page",
                   zeroRecords: "No existen datos",
                   info: " ",
                   infoEmpty: " ",
                   infoFiltered: "(filtered from _MAX_ total records)"
               }
            });
        },

        setTableHistoricalPackageTobe: function (data) {
            var controls = this.getControls();
            controls.tblHistoricalPackageToBe.find('tbody').html('');
            if (data == null) {
                controls.tblHistoricalPackageToBe.find('tbody').html('<tr><td colspan="8" align="center">No se encontraron registros</td></tr>');
            } else {
                controls.tblHistoricalPackageToBe.DataTable({
                info: false
               , paging: false
               , destroy: true
               , searching: false
               , scrollX: true
               , sScrollXInner: "100%"
               , autoWidth: true
               , data: data               
               , select: 'single'
               , order: [[5, 'desc']]
               , columns: [

                   { "data": "NameBag" },
                   { "data": "TypePackage" },
                   { "data": "validity" },
                   { "data": "cost" },
                   { "data": "TypePurchase" },
                   { "data": "AcquisitionDate" },
                   { "data": "State" },
                   {
                       "data": null,
                       "render": function (data) {
                           return data.TypeBalance == "2147483647" ? "Ilimitado" : data.TypeBalance
                       }
                   }

               ]
               , columnDefs: [
                   {
                       className: "text-center",
                       targets: [0, 1, 2, 3]
                   }
               ]

               , language: {
                   lengthMenu: "Display _MENU_ records per page",
                   zeroRecords: "No existen datos",
                   info: " ",
                   infoEmpty: " ",
                   infoFiltered: "(filtered from _MAX_ total records)"
               }
            });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        }


    }

    $.fn.PostpaidHistoricalPackage = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidHistoricalPackage'),
                options = $.extend({}, $.fn.PostpaidHistoricalPackage.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidHistoricalPackage', data);
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

    $.fn.PostpaidHistoricalPackage.defaults = {
    }

    $('#ContainerHistoricalPackage', $('.modal:last')).PostpaidHistoricalPackage();

})(jQuery);