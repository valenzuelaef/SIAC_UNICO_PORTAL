(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.TrackingDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            lblCaseNumber: $('#lblCaseNumber', $element),
            lblCustomerCode: $('#lblCustomerCode', $element),
            lblName: $('#lblName', $element),
            lblRecurrent: $('#lblRecurrent', $element),
            lblContact: $('#lblContact', $element),
            errorCustomerInformation: $('#errorCustomerInformation', $element),
            errorTrackingDetail: $('#errorTrackingDetail', $element),
            tblTrackingDetail: $('#tblTrackingDetail', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            this.render();
        },
        render: function () {
            this.getCustomerInformation();
            this.getTrackingDetail();
        },
        setCustomerInformation: function (response) {
            var that = this,
                controls = that.getControls();
            controls.lblCaseNumber.after(response.data.QueryOAC.ClarifyId);
            controls.lblCustomerCode.after(response.data.QueryOAC.CustomerCode);
            controls.lblName.after(response.data.QueryOAC.CustomerName);
            controls.lblRecurrent.after(response.data.QueryOAC.Recurrence);
            controls.lblContact.after(response.data.QueryOAC.CustomerContact);
        },
        getCustomerInformation: function () {
            var that = this;
            var objCustomerInformation = {
                IdCaso: window.opener.$('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    strIdSession: Session.IDSESSION
                };

            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetCustomerInformation',
                data: JSON.stringify(objCustomerInformation),
                success: function (response) {
                    that.setCustomerInformation(response);
                },
                error: function (ex) {
                    $.app.error({
                        id: 'errorCustomerInformation',
                        message: ex
                    });
                }
            });
        },
        createTableTrackingDetail: function (response) {
            var that = this,
                controls = that.getControls();          

            that.tblTrackingDetail = controls.tblTrackingDetail.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollY: 300,
                scrollCollapse: true,
                scrollX: false,
                destroy: true,
                data: response.data.QueryOACs,
                columns: [
                    { "data": null },
                    { "data": "DateCase" },
                    { "data": "LocationCase" },
                    { "data": "CaseLevel" },
                    { "data": "StateCase" },
                    { "data": "UserRegistrationCase" },
                    { "data": "OwnerCase" },
                    { "data": "CommentCase" },
                    { "data": "DateClosingCase" },
                    { "data": "ReopeningCase" }
                ],
                columnDefs: [
                    {
                        targets: 0,
                        className: "details-control",
                        defaultContent: "",
                        orderable: false
                    },
                    {
                        targets: 5,
                        render: function (data, type, full, meta) {
                            return full.UserRegistrationCase == '0' ? '' : full.UserRegistrationCase;
                        }
                    }  
                ],
                order: [[1, 'asc']],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        createSubTableTracking: function () {
            var that = this,
                controls = that.getControls(),
                $divContainer = $('<div>'),
                $table = $('<table>', {
                    id: 'tblSubTableTracking',
                    class: 'table claro-modal-tabla table-bordered table-hover'
                }, controls.form),
                $thead = $('<thead>', { class: 'claro-bg-second' }),
                $trhead = $('<tr>'),
                $th0 = $('<th>'),
                $th1 = $('<th>'),
                $th2 = $('<th>'),
                $th3 = $('<th>'),
                $th4 = $('<th>'),
                $th5 = $('<th>'),
                $th6 = $('<th>'),
                $th7 = $('<th>'),
                $tbody = $('<tbody>'),
                $trbody = $('<tr>'),
                $td = $('<td>', {
                    colspan: 10,
                    align: 'center'
                }),
                $img = $('<img>', {
                    src: window.location.href + '/Images/loading2.gif',
                    width: 25,
                    height: 25
                }),
                $divError = $('<div>', { id: 'errorSubTableTracking' });

            $.extend(controls, { tblSubTableTracking: $table });

            $trhead
            .append($th0.text(''))
            .append($th1.text(''))
            .append($th2.text('Nro. R/V'))
            .append($th3.text('Tipo Documento'))
            .append($th4.text('Reclamo Variación'))
            .append($th5.text('Tipo Transacción'))
            .append($th6.text('Monto'))
            .append($th7.text('Estado'));
            $thead.append($trhead);

            $td
            .append($img)
            .append('cargando...');

            $trbody.append($td);

            $tbody.append($trbody);

            $table
            .append($thead)
            .append($tbody);

            $divContainer
            .append($table)
            .append($divError);

            return $divContainer;
        },
        createThirdTableTracking: function () {
            var that = this,
                controls = that.getControls(),
                $divContainer = $('<div>'),
                $table = $('<table>', {
                    id: 'tblThirdTableTracking',
                    class: 'table claro-modal-tabla table-bordered table-hover'
                }, controls.form),
                $thead = $('<thead>', { class: 'claro-bg-second' }),
                $trhead = $('<tr>'),
                $th0 = $('<th>'),
                $th1 = $('<th>'),
                $th2 = $('<th>'),
                $th3 = $('<th>'),
                $tbody = $('<tbody>'),
                $trbody = $('<tr>'),
                $td = $('<td>', {
                    colspan: 10,
                    align: 'center'
                }),
                $img = $('<img>', {
                    src: '/Images/loading2.gif',
                    width: 25,
                    height: 25
                }),
                $divError = $('<div>', { id: 'errorThirdTableTracking' });

            $.extend(controls, { tblThirdTableTracking: $table });

            $trhead
            .append($th0.text('Caso'))
            .append($th1.text('Motivo'))
            .append($th2.text('Fecha Envío'))
            .append($th3.text('Monto'));
            $thead.append($trhead);

            $td
            .append($img)
            .append('cargando...');

            $trbody.append($td);

            $tbody.append($trbody);

            $table
            .append($thead)
            .append($tbody);

            $divContainer
            .append($table)
            .append($divError);

            return $divContainer;
        },
        fillSubTableTracking: function (response) {
            if (response.data.QueryOACs != null) {
                var $tbody = $('<tbody>');
                $.each(response.data.QueryOACs, function (index, value) {
                    var $tr = $('<tr>', { class: "text-success" }),
                        $td0 = $('<td>'),
                        $td1 = $('<td>'),
                        $td2 = $('<td>'),
                        $td3 = $('<td>'),
                        $td4 = $('<td>'),
                        $td5 = $('<td>'),
                        $td6 = $('<td>'),
                        $td7 = $('<td>');

                    $tr
                    .append($td0.text(''))
                    .append($td1.text(value.CaseClaimId))
                    .append($td2.text(value.DocumentNumberCase))
                    .append($td3.text(value.DocumentTypeCase))
                    .append($td4.text(value.VariableCaseClaim))
                    .append($td5.text(value.CaseTransactionType))
                    .append($td6.text(value.AmountCase))
                    .append($td7.text(value.StateCase));

                    $tbody.append($tr);
                });
                this.getControls().tblSubTableTracking.find('tbody').remove();
                this.getControls().tblSubTableTracking.append($tbody);
            } else {
                this.getControls().tblSubTableTracking.find('tbody').html('');
            }
        },
        fillThirdTableTracking: function (response) {
            if (response.data.QueryOACs != null) {
                var $tbody = $('<tbody>');
                $.each(response.data.QueryOACs, function (index, value) {
                    var $tr = $('<tr>', { class: "text-warning" }),
                        $td0 = $('<td>'),
                        $td1 = $('<td>'),
                        $td2 = $('<td>'),
                        $td3 = $('<td>')

                    $tr
                    .append($td0.text(value.IdCaseClarify))
                    .append($td1.text(value.MemdvName))
                    .append($td2.text(value.EnvimdShipping))
                    .append($td3.text(value.EnvimdAmount));

                    $tbody.append($tr);
                });
                this.getControls().tblThirdTableTracking.find('tbody').remove();
                this.getControls().tblThirdTableTracking.append($tbody);
            } else
                this.getControls().tblThirdTableTracking.find('tbody').html('');
        },
        createTableSubTableTracking: function () {
            var that = this,
                controls = that.getControls();

            that.tblSubTableTracking = controls.tblSubTableTracking.DataTable({
                info: false
            , paging: false
            , searching: false
            , destroy: true
            , select: 'single'
            , scrollY: 300
            , scrollX: false
            , scrollCollapse: true
            , columnDefs: [
                    {
                        targets: 0,
                        className: "details-control",
                        defaultContent: "",
                        orderable: false
                    },
                    {
                        targets: 1,
                        visible: false
                    }
            ]
            , order: [[1, 'asc']]
            , language: {
                lengthMenu: "Display _MENU_ records per page",
                zeroRecords: "No existen datos",
                info: " ",
                infoEmpty: " ",
                infoFiltered: "(filtered from _MAX_ total records)"
            }
            });
        },
        createTableThirdTableTracking: function () {
            var that = this,
            controls = that.getControls();

            controls.tblThirdTableTracking.DataTable({
                info: false
            , paging: false
            , searching: false
            , destroy: true
            , select: 'single'
            , scrollY: 300
            , scrollX: false
            , scrollCollapse: true
            , language: {
                lengthMenu: "Display _MENU_ records per page",
                zeroRecords: "No existen datos",
                info: " ",
                infoEmpty: " ",
                infoFiltered: "(filtered from _MAX_ total records)"
            }
            });
        },
        getSubTableTracking: function (row) {
            var that = this,
                objSubTableTracking = {
                    IdCaso: window.opener.$('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    strIdSession: Session.IDSESSION
                };

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetSubTableTracking',
                data: JSON.stringify(objSubTableTracking),
                complete: function () {
                    that.createTableSubTableTracking();
                    that.actionDisplayThirdTable();
                },
                success: function (response) {
                    that.fillSubTableTracking(response);
                },
                error: function (ex) {
                    controls.tblSubTableTracking.find('tbody').html('');
                    $.app.error({
                        id: 'errorSubTableTracking',
                        message: ex
                    });
                }
            });
        },
        getThirdTableTracking: function (row) {
            var that = this,
                objThirdTableTracking = {
                    IdCaso: window.opener.$('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    CaseClaimId: row[1],
                    strIdSession: Session.IDSESSION
                };

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetThirdTableTracking',
                data: JSON.stringify(objThirdTableTracking),
                complete: function () {
                    that.createTableThirdTableTracking();
                },
                success: function (response) {
                    that.fillThirdTableTracking(response);
                },
                error: function (ex) {
                    $.app.error({
                        id: 'errorThirdTableTracking',
                        message: ex
                    });
                }
            });
        },
        actionDisplaySubTable: function () {
            var that = this,
                controls = that.getControls(),
                detailRows = [];

            controls.tblTrackingDetail.find('>tbody').on('click', '> tr > td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = that.tblTrackingDetail.row(tr);
                var idx = $.inArray(tr.attr('id'), detailRows);

                if (row.child.isShown()) {
                    tr.removeClass('details');
                    row.child.hide();
                    detailRows.splice(idx, 1);
                }
                else {
                    tr.addClass('details');
                    row.child(that.createSubTableTracking()).show();
                    that.getSubTableTracking(row.data());

                    if (idx === -1) {
                        detailRows.push(tr.attr('id'));
                    }
                }
            });

            that.tblTrackingDetail.on('draw', function () {
                $.each(detailRows, function (i, id) {
                    $('#' + id + ' td.details-control').trigger('click');                    
                });
            });
        },
        actionDisplayThirdTable: function () {
            var that = this,
                controls = that.getControls(),
                detailRows = [];

            controls.tblSubTableTracking.find('tbody').on('click', 'tr td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = that.tblSubTableTracking.row(tr);
                var idx = $.inArray(tr.attr('id'), detailRows);

                if (row.child.isShown()) {
                    tr.removeClass('details');
                    row.child.hide();
                    detailRows.splice(idx, 1);
                }
                else {
                    tr.addClass('details');
                    row.child(that.createThirdTableTracking()).show();
                    that.getThirdTableTracking(row.data());

                    if (idx === -1) {
                        detailRows.push(tr.attr('id'));
                    }
                }
            });

            that.tblSubTableTracking.on('draw', function () {
                $.each(detailRows, function (i, id) {
                    $('#' + id + ' td.details-control').trigger('click');
                });
            });
        },
        getTrackingDetail: function () {
            var that = this,
                controls = that.getControls(),
                objTrackingDetail = {
                    IdCaso: window.opener.$('#PostpaidDetailMonitoringCases').FormPostpaidDetailMonitoringCases('getObjRow')[0],
                    strIdSession: Session.IDSESSION
                };

            controls.errorTrackingDetail.html('');
            controls.tblTrackingDetail.find('tbody').html('<tr><td colspan="10" align="center"><img src="' + that.strUrl + "/Images/loading2.gif" + '" width="25" height="25" /> Cargando ....</td></tr>');            

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataCollection/GetTrackingDetail',
                data: JSON.stringify(objTrackingDetail),
                complete: function () {
                    that.actionDisplaySubTable();
                },
                success: function (response) {
                    controls.tblTrackingDetail.find('tbody').html('');
                    that.createTableTrackingDetail(response);                    
                },
                error: function (ex) {
                    $.app.error({
                        id: 'errorTrackingDetail',
                        message: ex
                    });
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    };
    $.fn.TrackingDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('TrackingDetail'),
                options = $.extend({}, $.fn.TrackingDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('TrackingDetail', data);
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
    $.fn.TrackingDetail.defaults = {};
    $('#containerTrackingDetail', $('.modal:last')).TrackingDetail();
})(jQuery);

