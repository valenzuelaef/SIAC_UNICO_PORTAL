(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PrepaidExportExcelCall.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblExportExcelCall: $('#tblExportDataCall', $element)
             , txtCallDateFrom: $('#txtCallDateFrom', $element)
            , txtCallDateTo: $('#txtCallDateTo', $element)
            , trBag: $('#trBag', $element)
            , hBagCode: $('#hBagCode', $element)
            , hBagGroupCode: $('#hBagGroupCode', $element)
            , strBagGroup: $('#lblBagGroup', $element)
            , strBag: $('#lblBag', $element)
            , strDateFrom: $('#lblDateFrom', $element)
            , strDateTo: $('#lblDateTo', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.GetExportExcelCall();
            this.getDataTable();
            this.cboGroupBag_validation();
        },
        hideHead: function (num) {
            $('#ContentTable div.dataTables_scrollHead div.dataTables_scrollHeadInner table thead th:nth-child(' + num + ')').hide();
            $('#tblExportDataCall thead th:nth-child(' + num + ')').hide();
            $('#tblExportDataCall tbody td:nth-child(' + num + ')').hide();
        },
        hideTableTR: function (num) {
            this.hideHead(num);
        },
        showTableTR: function (num) {
            $('#ContentTable div.dataTables_scrollHead div.dataTables_scrollHeadInner table thead th:nth-child(' + num + ')').show();
            $('#tblExportDataCall thead th:nth-child(' + num + ')').show();
            $('#tblExportDataCall tbody td:nth-child(' + num + ')').show();
        },
        showAllTableTR: function () {

            for (var i = 0; i <= 32; i++) {
                this.showTableTR(i);
                i++;
            }

        },
        cboGroupBag_validation: function () {

            var that = this,
            controls = this.getControls();

            var cboGroupBagItem = controls.hBagGroupCode.val();
            this.showAllTableTR();



            if (cboGroupBagItem == "") {

                for (var i = 11; i <= 30; i++) {
                    that.hideTableTR(i);
                }

            }
            else if (cboGroupBagItem == 0) {

                that.hideTableTR(31);
                that.hideTableTR(32);
            }

            if (cboGroupBagItem > 0) {

                controls.trBag.css("display", "block");
                that.cboBag_Validation();

            } else {
                controls.trBag.css("display", "none");
            }
        },
        cboBag_Validation: function () {
            var controls = this.getControls();
            var i;
            var cboGroupBagItem = controls.hBagGroupCode.val();
            var cboBagItem = controls.hBagCode.val();

            
            this.showAllTableTR();

            if (cboGroupBagItem == 2) {
                for (i = 11; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            } else if (cboGroupBagItem == 1) {

                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 23; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            } else if (cboGroupBagItem == 3) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 22; i++) {
                    this.hideTableTR(i);
                }

            } else if (cboGroupBagItem == 0) {
                this.hideTableTR(31);
                this.hideTableTR(32);
            }

            if (cboGroupBagItem == 1 && cboBagItem == 1) {

                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);

                for (i = 13; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 2) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 12; i++) {
                    this.hideTableTR(i);
                }
                for (i = 15; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 3) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 18; i++) {
                    this.hideTableTR(i);
                }
                for (i = 17; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 4) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 16; i++) {
                    this.hideTableTR(i);
                }
                for (i = 19; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 5) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 18; i++) {
                    this.hideTableTR(i);
                }
                for (i = 21; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 6) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 20; i++) {
                    this.hideTableTR(i);
                }
                for (i = 23; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 1 && cboBagItem == 0) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 23; i <= 30; i++) {
                    this.showTableTR(i);
                }

            }

            if (cboGroupBagItem == 3 && cboBagItem == 1) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 22; i++) {
                    this.hideTableTR(i);
                }
                for (i = 25; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 3 && cboBagItem == 2) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 24; i++) {
                    this.hideTableTR(i);
                }
                for (i = 27; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 3 && cboBagItem == 3) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 26; i++) {
                    this.hideTableTR(i);
                }
                for (i = 29; i <= 30; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 3 && cboBagItem == 4) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);
                for (i = 11; i <= 28; i++) {
                    this.hideTableTR(i);
                }
            }
            else if (cboGroupBagItem == 3 && cboBagItem == 0) {
                this.hideTableTR(3);
                this.hideTableTR(4);
                this.hideTableTR(5);

                for (i = 11; i <= 22; i++) {
                    this.hideTableTR(i);
                }
            }

        },
        GetExportExcelCall: function () {

            var controls = this.getControls();

            var strUrlResult = $.app.getPageUrl({ url: '~/Dashboard/Home/DownloadExcel' });
            var oContract = {};
            oContract.strTelephoneCustomer = Session.TELEPHONE;
            oContract.strDateFrom = controls.strDateFrom.html();

            oContract.strDateTo = controls.strDateTo.html();
            oContract.strGroupBag = controls.hBagGroupCode.attr('value');
            oContract.strBag = controls.hBagCode.attr('value');
            oContract.strDescripcionGroupBag = controls.strBagGroup.html();
            oContract.strDescripcionBag = controls.strBag.html();


            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PrepaidDataCall/GetExportExcelCall',

                data: JSON.stringify(oContract),
                success: function (path) {

                    window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ReporteDeLlamadas.xlsx";
                }
            });
        },

        getDataTable: function () {
            var controls = this.getControls();

            controls.tblExportExcelCall.DataTable({
                "scrollX": true,
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "info": false,
                "select": "single",
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });


        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'
    };



    $.fn.PrepaidExportExcelCall = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidExportExcelCall'),
                options = $.extend({}, $.fn.PrepaidExportExcelCall.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidExportExcelCall', data);
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

    $.fn.PrepaidExportExcelCall.defaults = {
    }

    $('#contenedorExportExcelCall').PrepaidExportExcelCall();

})(jQuery);