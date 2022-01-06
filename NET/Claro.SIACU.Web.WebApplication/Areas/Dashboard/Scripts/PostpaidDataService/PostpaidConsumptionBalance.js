(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidConsumptionBalance.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblTelephoneJanus: $('#lblTelephoneJanus', $element)
            , tblJanus: $('#tblJanus', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            , lblTelephonePost: $('#lblTelephonePost', $element)
            , tblPost: $('#tblPost', $element)
            , lblPrincipalBalance: $('#lblPrincipalBalance', $element)
            , lblExpirationDateBalance: $('#lblExpirationDateBalance', $element)
            , lblFreeTriosChanges: $('#lblFreeTriosChanges', $element)
            , lblStateImsi: $('#lblStateImsi', $element)
            , tblPre: $('#tblPre', $element)
            , containerJanus: $('#containerJanus', $element)
            , containerPostPaid: $('#containerPostPaid', $element)
            , containerPrePaid: $('#containerPrePaid', $element)
            , LoadingJanus: $('#LoadingJanus', $element)
            , lblMensajePaqViajero: $('#lblMensajePaqViajero', $element)
            , lblMensaje: $('#lblMensaje', $element)
            , lblMensajeM2M: $('#lblMensajeM2M', $element)
            , btnPost_ConsumptionBalanceCBIOS: $('#btnPost_ConsumptionBalanceCBIOS', $element)
            , containerB2E: $('#containerB2E', $element)
            , lblB2ETelephone: $('#lblB2ETelephone', $element)
            , lblB2EStateLine: $('#lblB2EStateLine', $element)
            , tblB2E: $('#tblB2E', $element)
            , btnPost_HistoryPackage: $('#btnPost_HistoryPackage', $element)
            , btnPost_HistoryRecharge: $('#btnPost_HistoryRecharge', $element)
            , btnPost_QueryBalanceShared: $('#btnPost_QueryBalanceShared', $element)
            , btnPost_HistoryPackage_Tobe: $('#btnPost_HistoryPackage_Tobe', $element)
            , btnPost_HistoryRecharge_Tobe: $('#btnPost_HistoryRecharge_Tobe', $element)
            , btnPost_QueryBalanceShared_Tobe: $('#btnPost_QueryBalanceShared_Tobe', $element)
            , aLink: $('#aLink', $element)
            , aLink_Tobe: $('#aLink_Tobe', $element)
            , containerJanusToBe: $('#containerJanusToBe', $element)
            , tblJanusToBe: $('#tblJanusToBe', $element)
            , lblTelephoneJanusToBe: $('#lblTelephoneJanusToBe', $element)
            , lblLimitedPlan: $('#lblLimitedPlan', $element)
            , lblStateLineToBe: $('#lblStateLineToBe', $element)
            , containerPrueba: $('#containerPrueba', $element)
            , tblPrueba: $('#tblPrueba', $element)
            , lblInternationalCoverage: $('#lblInternationalCoverage', $element)
            , lblThethering: $('#lblThethering', $element)


        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = that.getControls();
            controls.btnPost_ConsumptionBalanceCBIOS.addEvent(that, 'click', that.btnPost_ConsumptionBalanceCBIOS);
            controls.btnPost_HistoryPackage.addEvent(that, 'click', that.btnPost_HistoryPackage_click);
            controls.btnPost_HistoryRecharge.addEvent(that, 'click', that.btnPost_HistoryRecharge_click);
            controls.btnPost_QueryBalanceShared.addEvent(that, 'click', that.btnPost_QueryBalanceShared_click);
            controls.btnPost_HistoryPackage_Tobe.addEvent(that, 'click', that.btnPost_HistoryPackage_click);
            controls.btnPost_HistoryRecharge_Tobe.addEvent(that, 'click', that.btnPost_HistoryRecharge_click);
            controls.btnPost_QueryBalanceShared_Tobe.addEvent(that, 'click', that.btnPost_QueryBalanceShared_click);
            controls.aLink.addEvent(that, 'click', that.aLink_click);
            controls.aLink_Tobe.addEvent(that, 'click', that.aLink_click);
            that.render();
        },
        render: function () {
            var that = this;
            that.getRecharges();
        },
        aLink_click: function (e) {

            $.window.open({
                modal: false,
                title: "Orden de Consumo",
                url: '~/Dashboard/PostpaidDataService/OrderConsumption',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
            return false;
        },
        btnPost_HistoryPackage_click: function () {


            $.window.open({
                modal: false,
                title: "Histórico de Paquetes",
                url: '~/Dashboard/PostpaidDataService/GetHistoryPackage',
                data: { strTelephone: Session.DATASERVICE.CellPhone, strCustomerId: Session.DATACUSTOMER.CustomerID, strContract: Session.DATACUSTOMER.ContractID },
                width: 1300,
                height: 543,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnPost_HistoryRecharge_click: function () {
            $.window.open({
                modal: false,
                title: "Histórico de Recargas",
                url: '~/Dashboard/PostpaidDataService/GetHistoryRecharge',
                data: { strTelephone: Session.DATASERVICE.CellPhone, strCustomerId: Session.DATACUSTOMER.CustomerID, strContract: Session.DATACUSTOMER.ContractID },
                width: 1200,
                height: 395,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnPost_QueryBalanceShared_click: function () {
            $.window.open({
                modal: false,
                title: "Consulta de Saldo Compartido",
                url: '~/Dashboard/PostpaidDataService/GetQueryBalanceShared',
                data: { strTelephone: Session.DATASERVICE.CellPhone, strCustomerId: Session.DATACUSTOMER.CustomerID, strContract: Session.DATACUSTOMER.ContractID },
                width: 1231,
                height: 550,
                buttons: {
                    Cerrar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnPost_ConsumptionBalanceCBIOS: function () {

            $.window.open({
                modal: false,
                title: "Saldos CBIOS",
                url: '~/Dashboard/PostpaidDataService/GetBalanceCBIOS',
                data: {},
                width: 1231,
                height: 550,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        chooseToDisplayData: function (response) {
            var that = this,
                controls = that.getControls();
            if (response.MessageRechargesM2MVisible)
                controls.lblMensajeM2M.show();
            else controls.lblMensajeM2M.hide();
            if (response.MessageRechargesVisible)
                controls.lblMensajePaqViajero.show();
            else controls.lblMensajePaqViajero.hide();

            if (response.MessageVisible)
                controls.lblMensaje.show();
            else controls.lblMensaje.hide();

            if (response.MessageRecharges != null) {
                if (response.MessageRecharges.indexOf("Paquete Viajero:") != -1) {
                    controls.lblMensajePaqViajero.text(response.MessageRecharges);
                }
            }
            if (response.Message != null) {
                controls.lblMensaje.text(response.Message);
            }
            if (response.MessageRechargesM2M != null) {
                controls.lblMensajeM2M.text(response.MessageRechargesM2M);
            }
            if (response.Recharges != null || response.Services != null) {

                console.log(response.Recharges)
                if (response.Recharges.length >= 0) {
                    if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE") {
                        that.setDataJanusToBe(response);
                        controls.containerPostPaid.remove();
                        controls.containerPrePaid.remove();
                        controls.containerB2E.remove();
                        controls.containerJanus.remove();
                        controls.containerPrueba.remove();

                    } else {
                        if (Session.DATACUSTOMER.CustomerType === "B2E" || Session.DATACUSTOMER.CustomerType === "Consumer" || Session.DATACUSTOMER.CustomerType === "MASIVO") {
                            that.setDataB2E(response);
                            controls.containerPostPaid.remove();
                            controls.containerPrePaid.remove();
                            controls.containerJanus.hide();
                            controls.containerJanusToBe.remove();
                            controls.containerB2E.removeClass("hidden");
                            $('#btnPost_ConsumptionBalanceCBIOS').appendTo('#divbuttonsb2e');
                            $("#btnPost_ConsumptionBalanceCBIOS").css("display", "");

                        } else {
                            that.setDataJanus(response);
                            controls.containerPostPaid.remove();
                            controls.containerPrePaid.remove();
                            controls.containerB2E.remove();
                        }
                    }
                }
                else if (response.Services != null) {
                    if (response.Services.length >= 0) {
                        that.setDataPost(response);
                        controls.containerJanus.remove();
                        controls.containerPrePaid.remove();
                        controls.containerB2E.remove();
                        controls.containerPostPaid.removeClass("hidden");
                    }
                } else if (response.Service != null) {
                    if (response.Service >= 0) {
                        that.setDataPre(response);
                        controls.containerPostPaid.remove();
                        controls.containerJanus.remove();
                        controls.containerB2E.remove();
                        controls.containerPrePaid.removeClass("hidden");
                    }
                }
            } else {
                controls.lblTelephoneJanus.after(response.Telephone);
                controls.containerJanus.remove()
                controls.containerB2E.remove()
                that.setTableJanus();
            }
        },
        getRecharges: function () {
            var contrato;
            var that = this,
                controls = that.getControls(),
                objDataService = Session.DATASERVICE,
                objCustomer = Session.DATACUSTOMER,
                objRecharges = {};

            objRecharges.Telephone = objDataService.CellPhone;
            objRecharges.CustomerId = objCustomer.CustomerID;
            objRecharges.FlagPlatform = objDataService.FlagPlatform;
            objRecharges.strIdSession = Session.IDSESSION;

            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "ASIS") {
                contrato = Session.DATACUSTOMER.ContractID;
            } else {
                contrato = Session.DATACUSTOMER.coIdPub;
            }
            objRecharges.Contract = contrato;
            objRecharges.StateLine = Session.DATASERVICE.StateLine;
            objRecharges.TypeCustomer = Session.DATACUSTOMER.CustomerType;
            objRecharges.Account = Session.DATACUSTOMER.Account;
            objRecharges.FlagPlataforma = Session.DATACUSTOMER.objPostDataAccount.plataformaAT;
            objRecharges.FechaExpiracion = Session.DATACUSTOMER.objPostDataAccount.BillingCycle;


            controls.LoadingJanus.html('');
            var stUrlLogo = "/Images/loading2.gif";
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "ASIS") {
                controls.containerJanus.find('table tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            } else {
                controls.containerPrueba.find('table tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetRecharges',
                data: JSON.stringify(objRecharges),
                success: function (response) {
                    if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "ASIS") {
                        controls.containerJanus.find('table tbody').html('');
                        that.chooseToDisplayData(response.data);
                    } else {
                        controls.containerPrueba.find('table tbody').html('');
                        that.chooseToDisplayData(response.data);
                    }
                },
                error: function (msger) {
                    controls.containerJanus.find('table tbody').html('');
                    $.app.error({
                        id: 'LoadingJanus',
                        message: msger,
                        click: function () {
                            that.getRecharges();
                        }
                    });
                }
            });


        },
        getControls: function () {
            return this.m_controls || {};
        },

        setDataB2E: function (response) {
            var that = this,
               controls = that.getControls();
            controls.lblB2ETelephone.after(response.Telephone);

            if (response.ColorStateLine == "red") {
                controls.lblB2EStateLine.after("<span  class=\"label-red-service\">" + Session.DATASERVICE.StateLine + "</span>")
            } else {
                controls.lblB2EStateLine.after(Session.DATASERVICE.StateLine);
            }
            that.setTableB2E(response.Recharges);
        },
        setDataJanus: function (response) {
            var that = this,
                controls = that.getControls();
            controls.lblTelephoneJanus.after(response.Telephone);
            that.setTableJanus(response.Recharges);

        },



        setDataJanusToBe: function (response) {
            var that = this,
                controls = that.getControls();

            controls.lblTelephoneJanusToBe.after(response.Telephone);
            controls.lblStateLineToBe.after(Session.DATASERVICE.StateLine);

            controls.lblLimitedPlan.after(" " + Session.DATASERVICE.Plan).css("font-weight", "Bold");
            controls.lblInternationalCoverage
                .after(" " + "Beneficio que le permitirá utilizar una parte de los componentes de su plan (minutos, internet y SMS) en los países con Cobertura Internacional,");
            controls.lblThethering
                .after(" " + "Indica la cantidad total de GB disponibles para compartir en cada Plan.");


            that.setTableJanusToBe(response.Recharges);

        },
        setTableB2E: function (data) {
            var controls = this.getControls(),
               tbl = controls.tblB2E;
            tbl.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: data
                , select: 'single'
                , columns: [

                         { "data": null },
                        { "data": "TypePackage" },
                        { "data": "Bag" },
                        { "data": "Zone" },
                        { "data": "TypeUnity" },
                        { "data": "Unity" },
                        { "data": "Consumption" },
                        { "data": "Balance" },
                        { "data": "ExpirationDate" }

                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    }
                ]
                , rowCallback: function (row, data, iDisplayIndex) {
                    var index = iDisplayIndex + 1;
                    $('td:eq(0)', row).html(index);

                }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
            controls.LoadingJanus.hide();
            $("#tblB2E").css("width", '')
        },

        setTableJanus: function (data, flag) {
            var controls = this.getControls(),
                tbl = controls.tblJanus;
             tbl.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false

                , autoWidth: true
                , data: data
                , select: 'single'
                , columns: [
                    { "data": null },
                    { "data": "TypePackage" },
                    { "data": "Bag" },
                    { "data": "Zone" },
                    { "data": "TypeUnity" },
                    { "data": "Unity" },
                    { "data": "Consumption" },
                    { "data": "Balance" },
                    { "data": "ExpirationDate" }

                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3]
                    }
                ]
                , rowCallback: function (row, data, iDisplayIndex) {
                    var index = iDisplayIndex + 1;
                    $('td:eq(0)', row).html(index);
                }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
             });
             controls.containerJanus.removeClass('hidden');
            controls.LoadingJanus.hide();
        },
        //tobe
        setTableJanusToBe: function (data) {
            var list1 = [], list2 = [], list3 = [], that = this;
            var controls = this.getControls();
            $.each(data, function (i, value) {
                if (value.TypePackage == "Plan") {
                    list1.push(value);
                }
            });
            $.each(data, function (i, value) {
                if (value.TypePackage == "Servicios Adicionales") {
                    list2.push(value);
                }
            });
            $.each(data, function (i, value) {
                if (value.TypePackage == "Paquete") {
                    list3.push(value);
                }
            });

            var body = $('#tblJanusToBe tbody');
            body.append(that.divSeparacion(9));
            if (list1.length > 0) {
                that.dibujarPlan(body, list1);
            }
            if (list2.length > 0) {
                body.append(that.divSeparacion(9));
                that.dibujarSerAdi(body, list2);
            }
            if (list3.length > 0) {
                body.append(that.divSeparacion(9));
                that.dibujarPaquete(body, list3);
            }
            controls.containerJanusToBe.removeClass('hidden');
        },

        divSeparacion: function (n) {
            var tr = $('<tr>', {
                class: "model-header claro-red"
            }), td = $('<td>', {
                colspan: n,
                height: "14"
            })
            tr.append(td);
            return tr;
        },

        dibujarPlan: function (body, list1) {
            var sumUnidadAsig = 0;
            var sumConsumo = 0;
            var sumSaldo = 0;
            var contador = 0, that = this;
            var ilimitadoDatos = false, ilimitadoMinutos = false, ilimitadoSMS = false;
            console.log(list1)
            $.each(list1, function (i, val) {

                var espacio = "";
                var tr = $('<tr>');

                if (i == 0) {
                    var tdFirstColumn = $('<td>', {
                        rowspan: list1.length
                    }).css({
                        "vertical-align": "middle",
                        "font-weight": "Bold"
                    }).addClass("text-center");
                    tdFirstColumn.text(val.TypePackage);
                    tr.append(tdFirstColumn);

                }
                var negritaIni = '<strong>';
                var negritaFin = '</strong>';
                var type = val.TypeUnity;
                var unidades = val.Unity;
                var consumo = val.Consumption;
                var saldo = val.Balance;
                var sona = (val.Zone || '');
                var fechaExp = val.ExpirationDate;
                if (val.Unified == 0 || val.Unified > 3) {
                    contador = contador + 1;
                    val.UnifiedIndex = contador;
                    sona = negritaIni + sona + negritaFin;
                    type = negritaIni + type + negritaFin;
                    unidades = negritaIni + unidades + negritaFin;
                    consumo = negritaIni + consumo + negritaFin;
                    fechaExp = negritaIni + fechaExp + negritaFin;
                    saldo = negritaIni + saldo + negritaFin;

                }
                if (val.UnifiedIndex == null) {
                    espacio = "&nbsp&nbsp&nbsp&nbsp&nbsp";
                    negritaIni = '';
                    negritaFin = '';
                }
                if (val.Bag == "Saldo Disponible Prepago") {
                    unidades = "-";
                    consumo = "-"
                }
                var nomServicio = val.Bag;
                if (val.Bag == "L�mite de Consumo" || val.Bag == "L¿mite de Consumo") {
                    nomServicio = "Límite de Consumo";

                }

                if (nomServicio == "Límite de Consumo") {
                    var monto = val.Unity.split('.');
                    if (monto != null && parseInt(monto[0]) >= 4000) {
                        unidades = negritaIni + "Ilimitado" + negritaFin;
                        saldo = negritaIni + "Ilimitado" + negritaFin;
                    }
                }
                if ((val.UnifiedIndex > 1) && saldo == "") {
                    type = "";
                }
                if ((saldo.toUpperCase() == "ILIMITADO") && (type.toUpperCase() == "GIGABYTES" || type.toUpperCase() == "MEGABYTES") && (val.Unified != 0)) {
                    ilimitadoDatos = true;
                }
                if ((saldo.toUpperCase() == "ILIMITADO") && (type.toUpperCase() == "MENSAJES DE TEXTO") && (val.Unified != 0)) {
                    ilimitadoSMS = true;
                }
                if ((saldo.toUpperCase() == "ILIMITADO") && (type.toUpperCase() == "MINUTOS CON SEGUNDOS") && (val.Unified != 0)) {
                    ilimitadoMinutos = true;
                }
                var contenido = '<td>' + negritaIni + (val.UnifiedIndex || '') + negritaFin + '</td><td>' + negritaIni + espacio + nomServicio + negritaFin + '</td><td class="text-center">' + sona + '</td><td class="text-center">' + type +
            '</td><td class="text-center">' + unidades +
           '</td><td class="text-center">' + consumo + '</td><td class="text-center">' +
           saldo + '</td><td class="text-center">' + fechaExp + '</td>';
                //Suma tota de unidades asignadas
                sumUnidadAsig = sumUnidadAsig + that.ConvertGB(val.Unity);
                //Suma tota de consumo
                if (val.Unified != "6") {
                sumConsumo = sumConsumo + that.ConvertGB(val.Consumption);
                }
                //Suma tota de saldo
                sumSaldo = sumSaldo + that.ConvertGB(val.Balance);
                tr.append(contenido);
                body.append(tr);
            })
            var $row = $("#tblJanusToBe tbody tr").find("td");
            $row[6].innerHTML = '<strong>' + parseFloat(sumUnidadAsig).toFixed(2) + " GB </strong>";
            $row[7].innerHTML = '<strong>' + parseFloat(sumConsumo).toFixed(2) + " GB </strong>";
            $row[8].innerHTML = '<strong>' + parseFloat(sumSaldo).toFixed(2) + " GB </strong>";
            if ($row[17] != null && $row[17].innerHTML != "") {
                $row[9].innerHTML = '<strong>' + $row[17].innerHTML;
            }
            $.each($("#tblJanusToBe tbody tr"), function (i, val) {
                $.each($(val).find("td"), function (j, columna) {
                    if (i != 0) {
                        if ($(columna).html() == "<strong>1</strong>" && ilimitadoDatos) {
                            var campoUnidad = $(val).children().eq(5)[0];
                            var campoSaldo = $(val).children().eq(7)[0];
                            $(campoUnidad)[0].innerHTML = "<strong> Ilimitado </strong>";
                            $(campoSaldo)[0].innerHTML = "<strong> Ilimitado </strong>";
                        }
                        if ($(columna).html() == "<strong>2</strong>" && ilimitadoSMS) {
                            var campoUnidad = $(val).children().eq(4)[0];
                            var campoSaldo = $(val).children().eq(6)[0];
                            $(campoUnidad)[0].innerHTML = "<strong> Ilimitado </strong>";
                            $(campoSaldo)[0].innerHTML = "<strong> Ilimitado </strong>";
                        }
                        if ($(columna).html() == "<strong>3</strong>" && ilimitadoMinutos) {
                            var campoUnidad = $(val).children().eq(4)[0];
                            var campoSaldo = $(val).children().eq(6)[0];
                            $(campoUnidad)[0].innerHTML = "<strong> Ilimitado </strong>";
                            $(campoSaldo)[0].innerHTML = "<strong> Ilimitado </strong>";
                        }
                    }

                })
            });

        },

        dibujarSerAdi: function (body, list2) {
            $.each(list2, function (i, val) {
                var tr = $('<tr>');
                if (i == 0) {
                    var tdFirstColumn = $('<td>', {
                        rowspan: list2.length
                    }).css({
                        "vertical-align": "middle",
                        "font-weight": "Bold"
                    }).addClass("text-center");
                    tdFirstColumn.text(val.TypePackage);
                    tr.append(tdFirstColumn);

                }
                var contenido = '<td>' + (i + 1) + '</td><td>' + val.Bag + '</td><td class="text-center">' + (val.Zone || '') + '</td><td class="text-center">' + (val.TypeUnity || '') +
                     '</td><td class="text-center">' + (val.Unity || '') +
                    '</td><td class="text-center">' + (val.Consumption || '') + '</td><td class="text-center">' +
                    val.Balance + '</td><td class="text-center">' + val.ExpirationDate + '</td>';

                tr.append(contenido);
                body.append(tr);
            })
        },

        dibujarPaquete: function (body, list3) {
            $.each(list3, function (i, val) {
                var tr = $('<tr>');
                if (i == 0) {
                    var tdFirstColumn = $('<td>', {
                        rowspan: list3.length
                    }).css({
                        "vertical-align": "middle",
                        "font-weight": "Bold"
                    }).addClass("text-center");
                    tdFirstColumn.text(val.TypePackage);
                    tr.append(tdFirstColumn);

                }
                var contenido = '<td>' + (i + 1) + '</td><td>' + val.Bag + '</td><td class="text-center">' + (val.Zone || '') + '</td><td class="text-center">' + (val.TypeUnity || '') +
                     '</td><td class="text-center">' + (val.Unity || '') +
                    '</td><td class="text-center">' + (val.Consumption || '') + '</td><td class="text-center">' +
                    val.Balance + '</td><td class="text-center">' + val.ExpirationDate + '</td>';

                tr.append(contenido);
                body.append(tr);
            })
        },

        ConvertGB: function (value) {
            let indGB = value.indexOf("GB");
            let indMB = value.indexOf("MB");
            let valueGB = 0;
            if (indGB > 0) {
                valueGB = parseFloat(value.substring(0, indGB));
            } else if (indMB > 0) {
                valueGB = parseFloat(value.substring(0, indMB)) / 1024;
            }
            return valueGB;
        },

        Merge: function () {
            var dimension_col = null;
            var columnCount = 1
            for (dimension_col = 0; dimension_col <= columnCount; dimension_col++) {
                var first_instance = null;
                var rowspan = 1;
                $("#tblJanusToBe").find('tr').each(function () {
                    var dimension_td = $(this).find('td:nth-child(' + dimension_col + ')');

                    if (first_instance == null) {
                        first_instance = dimension_td;
                    } else if (dimension_td.text() === first_instance.text()) {
                        dimension_td.remove();
                        ++rowspan;
                        first_instance.attr('rowspan', rowspan);
                    } else {
                        first_instance = dimension_td;
                        rowspan = 1;
                    }
                });
            }
        },


        setDataPost: function (response) {
            var that = this,
                controls = that.getControls();

            if (response.ActivationDate == null)
                lblActivationDate.addClass('hide');
            else
                controls.lblActivationDate.after(response.ActivationDate);

            controls.lblTelephonePost.after(response.Telephone);
            that.setTablePost(response.Services);
        },
        setTablePost: function (data) {
            var controls = this.getControls();

            controls.tblPost.DataTable({
                info: false
               , paging: false
               , destroy: true
               , searching: false

               , autoWidth: true
               , data: data
                , select: 'single'
               , columns: [
                { "data": null },
                { "data": "Package" }
               ]
               , columnDefs: [
                {
                    className: "text-center",
                    targets: [0, 1]
                }
               ]
               , rowCallback: function (row, data, iDisplayIndex) {
                   var index = iDisplayIndex + 1;
                   $('td:eq(0)', row).html(index);
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
        setDataPre: function (response) {
            var that = this,
                controls = that.getControls();

            controls.lblPrincipalBalance.after(response.Service.PrincipalBalance);
            controls.lblExpirationDateBalance.after(response.Service.ExpirationDateBalance);
            controls.lblStateLine.after(response.Service.StateLine);
            controls.lblIdProvider.after(response.Service.IdProvider);
            controls.lblActivationDate.after(response.Service.ActivationDate)
            controls.lblExpirationDateLine.after(response.Service.ExpirationDateLine);
            controls.lblFreeTriosChanges.after(response.Service.FreeTriosChanges);
            controls.lblStateImsi.after(response.Service.StateImsi);
            that.setTablePre(response.Service.ListAccount);
        },
        setTablePre: function (data) {
            var controls = this.getControls();

            controls.tblPre.DataTable({
                info: false
               , paging: false
               , destroy: true
               , searching: false

               , autoWidth: true
               , data: data
               , select: 'single'
               , columns: [
                { "data": null },
                { "data": "Name" },
                { "data": "Balance" },
                { "data": "ExpirationDate" }
               ]
               , columnDefs: [
                {
                    className: "text-center",
                    targets: [0, 1, 2, 3]
                }
               ]
              , rowCallback: function (row, data, iDisplayIndex) {
                  var index = iDisplayIndex + 1;
                  $('td:eq(0)', row).html(index);
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
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostpaidConsumptionBalance = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidConsumptionBalance'),
                options = $.extend({}, $.fn.PostpaidConsumptionBalance.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidConsumptionBalance', data);
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

    $.fn.PostpaidConsumptionBalance.defaults = {
    }

    $('#containerConsumptionBalance', $('.modal:last')).PostpaidConsumptionBalance();

})(jQuery);