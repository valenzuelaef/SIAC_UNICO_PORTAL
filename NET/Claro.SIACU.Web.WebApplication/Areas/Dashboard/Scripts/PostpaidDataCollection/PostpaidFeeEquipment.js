var isMovil = false;
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidFeeEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblFeeEquipment: $('#tblFeeEquipment', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
                



            that.render();
        },
        render: function () {

            if (Session.DATACUSTOMER.ProductTypeText == 'PostPago - Telefonía Móvil')
            {
                isMovil = true
            }

            this.getDataTable();

        },
        GetExportFeeEquipment: function () {

            var strUrlResult = '~/Dashboard/Home/DownloadExcel';
            var oContract = {};
            oContract.strIdSession = Session.IDSESSION;
            oContract.strCustomerId = Session.DATACUSTOMER.CustomerID;
            oContract.strNameClient = Session.DATACUSTOMER.BusinessName;
            oContract.strNumberServices = Session.DATACUSTOMER.ValueSearch;
            oContract.strContactId = Session.DATACUSTOMER.Account;
            oContract.boolIsMovil = isMovil

            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataCollection/CollectionExportFeeEquipment',
                data: JSON.stringify(oContract),
                success: function (path) {

                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=CollectionFeeEquipment.xlsx";
                }
            });
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

        getDataTable: function () {
            var controls = this.getControls();

             controls.tblFeeEquipment.DataTable({
                "info": false,
                "scrollX": true,
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": false,
                "searching": isMovil,
                "select": "single",
                "destroy": true,
                "ordering": true,
                columnDefs: [
                    {   targets: 1,
                        visible: (!isMovil)
                    }
                ],
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "search": "Buscar:"
                }
            });

        },



    };

    $.fn.PostpaidFeeEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportFeeEquipment'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidFeeEquipment'),
                options = $.extend({}, $.fn.PostpaidFeeEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidFeeEquipment', data);
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

    $.fn.PostpaidFeeEquipment.defaults = {
    }

    $('#ContentFeeEquipment').PostpaidFeeEquipment();

})(jQuery);