(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormFixedChargeDetails.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblFixedChargeDetailsTIMPro: $('#tblFixedChargeDetailsTIMPro', $element)
            , tblFixedChargeDetailsTIMProOne: $('#tblFixedChargeDetailsTIMProOne', $element)
            , tblFixedChargeDetailsTIMProTwo: $('#tblFixedChargeDetailsTIMProTwo', $element)
            , tblFixedChargeDetailsTIMProStock: $('#tblFixedChargeDetailsTIMProStock', $element)
            , tblFixedChargeDetailsTIMProStockOne: $('#tblFixedChargeDetailsTIMProStockOne', $element)
            , tblFixedChargeDetailsTIMProStockTwo: $('#tblFixedChargeDetailsTIMProStockTwo', $element)
            , tblFixedChargeDetailsTIMProStockThree: $('#tblFixedChargeDetailsTIMProStockThree', $element)
            , tblFixedChargeDetailsTIMMax: $('#tblFixedChargeDetailsTIMMax', $element)
            , tblFixedChargeDetailsTIMMaxTwo: $('#tblFixedChargeDetailsTIMMaxTwo', $element)
            , tblDiscountFixedChargeDetails: $('#tblDiscountFixedChargeDetails', $element)
            , tblFixedChargeDetailsTIMProBag: $('#tblFixedChargeDetailsTIMProBag', $element)
            , tblFixedChargeDetailsTIMProBagOne: $('#tblFixedChargeDetailsTIMProBagOne', $element)
            , tblFixedChargeDetailsTIMProBagTwo: $('#tblFixedChargeDetailsTIMProBagTwo', $element)
            , tblFixedChargeDetailsTIMProBagThree: $('#tblFixedChargeDetailsTIMProBagThree', $element)

            , LoadingPostpaidTP: $('#LoadingPostpaidTP', $element)
            , LoadingPostpaidTP1: $('#LoadingPostpaidTP1', $element)
            , LoadingPostpaidTP2: $('#LoadingPostpaidTP2', $element)
            , LoadingPostpaidTimMax: $('#LoadingPostpaidTimMax', $element)
            , LoadingPostpaidTimMax2: $('#LoadingPostpaidTimMax2', $element)
            , LoadingPostpaidDiscount: $('#LoadingPostpaidDiscount', $element)
            , LoadingPostpaidBag: $('#LoadingPostpaidBag', $element)
            , LoadingPostpaidBag1: $('#LoadingPostpaidBag1', $element)
            , LoadingPostpaidBag2: $('#LoadingPostpaidBag2', $element)
            , LoadingPostpaidBag3: $('#LoadingPostpaidBag3', $element)
            , hidInvoiceNumber: $('#hidInvoiceNumber', $element)
            , divFormFixedChargeDetails: $('#FormFixedChargeDetails', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            that.getFixedChargeDetails();
            that.getFixedChargeDetailTimProOne();
            that.getFixedChargeDetailTimProTwo();
            that.getFixedChargeDetailTimMax();
            that.getFixedChargeDetailTimMaxTwo();
            that.getDiscountFixedChargeDetail();
            that.getFixedChargeDetailTimProBag();
            that.getFixedChargeDetailTimProBagOne();
            that.getFixedChargeDetailTimProBagTwo();
            that.getFixedChargeDetailTimProBagThree();
            that.render();
        },
       
        exist: false,
        getDataFixedChargeDetailsTIMPro: function myfunction(DetailsTIMPro) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMPro.DataTable({
                "scrollY": "350px",
                "info": false,
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "data": DetailsTIMPro,
                "select": "single",
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTP.hide();
        },
        getFixedChargeDetails: function myfunction() {
            var that = this;


            $.app.ajax({
                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimPro',
                data: { intGroupBox: '1', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                
             
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimPro != null) {
                    that.getDataFixedChargeDetailsTIMPro(result.data.listFixedChargeDetailTimPro);
                        that.exist = true;
                    }
                    else {
                        $("#divFixedChargeDetailsTIM").css('display', 'none');
                    }

                },

                error: function (msger) {
                    $.app.error({
                        id: 'FormFixedChargeDetails',
                        message: msger
                    });

                }
            });

        },
        getDataFixedChargeDetailTimProOne: function myfunction(DetailTimProOne) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProOne.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "data": DetailTimProOne,
                "select": "single",
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                    { "data": "Fu3" },
                    { "data": "Fu4" },
                    { "data": "MsgTexto5" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTP1.hide();
        },
        getFixedChargeDetailTimProOne: function myfunction() {
            var that = this;

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProOne',
                data: { intGroupBox: '3', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimProOne != null) {
                    that.getDataFixedChargeDetailTimProOne(result.data.listFixedChargeDetailTimProOne);
                        that.exist = true;
                    }
                    else {
                        $("#divFixedChargeDetailsTIMProOne").css('display', 'none');

                    }

                },

                error: function (msger) {

                }
            });

        },
        getDataFixedChargeDetailTimProTwo: function myfunction(DetailTimProTwo) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProTwo.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimProTwo,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "MsgTexto5" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTP2.hide();
        },
        getFixedChargeDetailTimProTwo: function myfunction() {
            var that = this;

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProTwo',
                data: { intGroupBox: '4', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                success: function (result) {

                    if (result.data.listFixedChargeDetailTimProTwo != null) {
                    that.getDataFixedChargeDetailTimProTwo(result.data.listFixedChargeDetailTimProTwo);
                        that.exist = true;
                    }

                    else {
                        $("#divFixedChargeDetailsTIMProTwo").css('display', 'none');
                    }
                },

                error: function (msger) {

                }
            });

        },
        getDataFixedChargeDetailTimMax: function myfunction(DetailTimMax) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMMax.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimMax,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                    { "data": "Fu3" },
                    { "data": "MsgTexto4" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTimMax.hide();
        },
        getFixedChargeDetailTimMax: function myfunction() {
            var that = this;

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimMax',
                data: { intGroupBox: '2', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimMax != null)
                    {
                    that.getDataFixedChargeDetailTimMax(result.data.listFixedChargeDetailTimMax);
                        that.exist = true;
                    }
                    else
                    {
                        $("#divFixedChargeDetailsTIMMax").css('display', 'none');
                    }
                   

                },

                error: function (msger) {

                }
            });

        },
        getDataFixedChargeDetailTimMaxTwo: function myfunction(DetailTimMaxTwo) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMMaxTwo.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimMaxTwo,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "MsgTexto4" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidTimMax2.hide();
        },
        getFixedChargeDetailTimMaxTwo: function myfunction() {
            var that = this;

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimMaxTwo',
                data: { intGroupBox: '5', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimMaxTwo != null)
                    {
                    that.getDataFixedChargeDetailTimMaxTwo(result.data.listFixedChargeDetailTimMaxTwo);
                        that.exist = true;
                    }
                    else
                    {
                        $("#divFixedChargeDetailsTIMMaxTwo").css('display', 'none');
                    }
                  

                },

                error: function (msger) {

                }
            });

        },
        getDataDiscountFixedChargeDetail: function myfunction(DetailTimMaxTwo) {
            var that = this,
                controls = that.getControls();

            controls.tblDiscountFixedChargeDetails.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimMaxTwo,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Msisdn" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }]
            });
            controls.LoadingPostpaidDiscount.hide();
        },
        getDiscountFixedChargeDetail: function myfunction() {
            var that = this;

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetDiscountFixedChargeDetail',
                data: { intGroupBox: '0', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.data.listDiscountFixedChargeDetail != null)
                    {
                    that.getDataDiscountFixedChargeDetail(result.data.listDiscountFixedChargeDetail);
                        that.exist = true;

                    }
                    else
                    {
                        $("#divDiscountFixedChargeDetails").css('display', 'none');
                    }

                },

                error: function (msger) {

                }
            });

        },
        getDataFixedChargeDetailTimProBag: function myfunction(DetailTimProOne) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProBag.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimProOne,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Quantity" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }, {
                    "targets": 1,
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });
            controls.LoadingPostpaidBag.hide();
        },
        getFixedChargeDetailTimProBag: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProBag',
                data: { intGroupBox: '1', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                complete: function () {
                    that.FixedChargeDetailTimProBag_click();
                },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimProBag != null) {
                    that.getDataFixedChargeDetailTimProBag(result.data.listFixedChargeDetailTimProBag);
                        that.exist = true;
                    }
                    else {
                        controls.LoadingPostpaidBag.hide();
                        $("#divFixedChargeDetailsTIMProBag").css('display', 'none');
                    }

                },

                error: function (msger) {

                }
            });

        },
        FixedChargeDetailTimProBag_click: function () {
            var that = this,
                controls = that.getControls();
            controls.tblFixedChargeDetailsTIMProBag.find('tbody').find('a').addEvent(that, 'click', that.getFixedChargeDetailTimProBagDialog);
        },
        getFixedChargeDetailTimProBagDialog: function () {

            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIMpro Bolsa",
                url: '~/Dashboard/PostpaidDataBilling/BillingFixedChargeTimProBag',
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

        },
        getDataFixedChargeDetailTimProBagOne: function myfunction(DetailTimProOne) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProBagOne.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimProOne,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Quantity" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Fu2" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }, {
                    "targets": 1,
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });
            controls.LoadingPostpaidBag1.hide();
        },
        getFixedChargeDetailTimProBagOne: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProBagOne',
                data: { intGroupBox: '2', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                complete: function () {
                    that.FixedChargeDetailTimProBagOne_click();
                },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimProBagOne != null) {
                    that.getDataFixedChargeDetailTimProBagOne(result.data.listFixedChargeDetailTimProBagOne);
                        that.exist = true;
                    }
                    else {
                        controls.LoadingPostpaidBag1.hide();
                        $("#divFixedChargeDetailsTIMProBagOne").css('display', 'none');
                    }


                },

                error: function (msger) {

                }
            });

        },
        FixedChargeDetailTimProBagOne_click: function () {
            var that = this,
                controls = that.getControls();
            controls.tblFixedChargeDetailsTIMProBagOne.find('tbody').find('a').addEvent(that, 'click', that.getFixedChargeDetailTimProBagDialogOne);
        },
        getFixedChargeDetailTimProBagDialogOne: function () {
       
            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIMpro Bolsa 1",
                url: '~/Dashboard/PostpaidDataBilling/BillingFixedChargeTimProBagOne',
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



        },
        getDataFixedChargeDetailTimProBagTwo: function myfunction(DetailTimProBagTwo) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProBagTwo.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimProBagTwo,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Quantity" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu1" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }, {
                    "targets": 1,
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });
            controls.LoadingPostpaidBag2.hide();
        },
        getFixedChargeDetailTimProBagTwo: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProBagTwo',
                data: { intGroupBox: '3', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                complete: function () {
                    that.FixedChargeDetailTimProBagTwo_click();
                },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimProBagTwo != null)
                    {
                    that.getDataFixedChargeDetailTimProBagTwo(result.data.listFixedChargeDetailTimProBagTwo);
                        that.exist = true;
                    }
                    else
                    {
                        controls.LoadingPostpaidBag2.hide();
                        $("#divFixedChargeDetailsTIMProBagTwo").css('display', 'none');
                    }
                 

                },

                error: function (msger) {

                }
            });

        },
        FixedChargeDetailTimProBagTwo_click: function () {
            var that = this,
                controls = that.getControls();
            controls.tblFixedChargeDetailsTIMProBagTwo.find('tbody').find('a').addEvent(that, 'click', that.getFixedChargeDetailTimProBagDialogTwo);
        },
        getFixedChargeDetailTimProBagDialogTwo: function () {
            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIMpro Bolsa 2",
                url: '~/Dashboard/PostpaidDataBilling/BillingFixedChargeTimProBagTwo',
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

        },
        getDataFixedChargeDetailTimProBagThree: function myfunction(DetailTimProBagThree) {
            var that = this,
                controls = that.getControls();

            controls.tblFixedChargeDetailsTIMProBagThree.DataTable({
                "info": false,
                "scrollY": "350px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select": "single",
                "data": DetailTimProBagThree,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [

                    { "data": "Quantity" },
                    { "data": "RatePlan" },
                    { "data": null },
                    { "data": "Fu4" },
                    { "data": "Amount" }
                ]
                , columnDefs: [{
                    "targets": 2,
                    "render": function (full, meta) {
                        return full.FromDate + " - " + full.ToDate;
                    }
                }, {
                    "targets": 1,
                    "render": function (data, type, full, meta) {
                        return '<a style="cursor:pointer">' + data + '</a>';
                    }
                }]
            });
            controls.LoadingPostpaidBag3.hide();
        },
        getFixedChargeDetailTimProBagThree: function myfunction() {
            var that = this,
                controls = that.getControls();

            $.app.ajax({

                async: false,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataBilling/GetFixedChargeDetailTimProBagThree',
                data: { intGroupBox: '4', strInvoiceNumber: window.opener.$('#hidInvoiceNumber').val(), strIdSession: Session.IDSESSION },
                complete: function () {
                    that.FixedChargeDetailTimProBagThree_click();
                },
                success: function (result) {
                    if (result.data.listFixedChargeDetailTimProBagThree != null)
                    {
                    that.getDataFixedChargeDetailTimProBagThree(result.data.listFixedChargeDetailTimProBagThree);
                        that.exist = true;
                    }
                    else
                    {
                        controls.LoadingPostpaidBag3.hide();
                        $("#divFixedChargeDetailsTIMProBagThree").css('display', 'none');
                    }

                },

                error: function (msger) {

                }
            });

        },
        FixedChargeDetailTimProBagThree_click: function () {
            var that = this,
                controls = that.getControls();
            controls.tblFixedChargeDetailsTIMProBagThree.find('tbody').find('a').addEvent(that, 'click', that.getFixedChargeDetailTimProBagDialogThree);
        },
        getFixedChargeDetailTimProBagDialogThree: function () {
           
            $.window.open({
                modal: true,
                type: 'post',
                title: "Detalle TIMpro Bolsa 3",
                url: '~/Dashboard/PostpaidDataBilling/BillingFixedChargeTimProBagThree',
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

        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
        
            if (that.exist != true)
            {
                $("#FormFixedChargeDetails").text('No existen datos');
            }

        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };



    $.fn.FormFixedChargeDetails = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormFixedChargeDetails'),
                options = $.extend({}, $.fn.FormFixedChargeDetails.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormFixedChargeDetails', data);
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

    $.fn.FormFixedChargeDetails.defaults = {
    }

    $('#FormFixedChargeDetails', $('.modal:last')).FormFixedChargeDetails();

})(jQuery);