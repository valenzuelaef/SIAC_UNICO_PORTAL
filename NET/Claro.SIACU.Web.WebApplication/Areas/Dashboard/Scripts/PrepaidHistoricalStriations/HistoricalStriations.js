
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formHistoricalStriations.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtDateStartHT: $('#txtDateStartHT', $element)
            , txtDateEndHT: $('#txtDateEndHT', $element)
            , btnVisualize: $('#btnVisualize', $element)
            , containerDateRange: $('#containerDateRange', $element)
            , tblHistoricalStriations: $('#tblHistoricalStriations', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            var f = new Date();
            var dateHoy = f.getDate() < 10 ? '0' + f.getDate() : f.getDate(),
                dateMonth = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1),
                dateMonthPre = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1);

            var fDateToDay = dateHoy + "/" + dateMonth + "/" + f.getFullYear();
            var fDatePrevious = dateHoy + "/" + dateMonthPre + "/" + (f.getFullYear() - 1);

            controls.txtDateEndHT.val(fDateToDay);
            controls.txtDateStartHT.val(fDatePrevious);
            controls.btnVisualize.addEvent(that, 'click', that.btnVisualize_click);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });
            that.render();
        },
        render: function () {
          
            if (Session.TELEPHONE !== '' && $('#lblPre_PlanRate').html() !== '') {
                this.btnVisualize_click();
            } else {
                $.window.close();
                showAlert("El número no es un teléfono prepago");
            }

        },
        daysBetweeen: function (startDate, finishDate) {
            var d1 = startDate.split("/");
            var dat1 = new Date(d1[2], parseFloat(d1[1]) - 1, parseFloat(d1[0]));
            var d2 = finishDate.split("/");
            var dat2 = new Date(d2[2], parseFloat(d2[1]) - 1, parseFloat(d2[0]));

            var finish = dat2.getTime() - dat1.getTime();
            var days = Math.floor(finish / (1000 * 60 * 60 * 24))
            if (days > 366) {
                showAlert("El rango excede los 12 meses", "mensaje");
                return false;
            }
            return true;
        },
        btnVisualize_click: function () {

            var that = this, controls = this.getControls();
            var oHistorical = {};

            oHistorical.strIdSession = Session.IDSESSION;
            oHistorical.strTelephone = Session.TELEPHONE;
            oHistorical.strPlanTariff = Session.PlanRate;
            oHistorical.strDateStartHT = controls.txtDateStartHT.val();
            oHistorical.strDateEndHT = controls.txtDateEndHT.val();
            if (that.daysBetweeen(oHistorical.strDateStartHT, oHistorical.strDateEndHT)) {
                var stUrlLogo =  "/Images/loading2.gif";
                controls.tblHistoricalStriations.find('tbody').html('<tr><td colspan="10" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

                $.app.ajax({
                    type: 'POST',
                    url: '~/Dashboard/PrepaidDataService/HistoricalStriationsSearch',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'JSON',
                    cache: true,
                    data: JSON.stringify(oHistorical),
                    complete: function () {
                        that.optionTriacion_click();
                    },
                    success: function (result) {
                        controls.tblHistoricalStriations.find('tbody').html('');
                        Session.HistoricalTriationRFA = result.data;
                        that.createTableHistoricalStriations(result);
                    },
                    error: function (msger) {
                        controls.tblHistoricalStriations.find('tbody').html('');
                        $.app.error({
                            id: 'errorHistoricalStriations',
                            message: msger,
                            click: function () {
                                that.btnVisualize_click();
                            }
                        });
                    }
                });

            }

        },
        optionTriacion_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblHistoricalStriations.find('tbody a').addEvent(that, 'click', that.actionTriationDetail);
        },
        triationDetailExcel: function () {
            var that = this;
            var row = that.rowTriationDetail;
            var objHistoricalTriationRFA = Session.DetalleHistoricalTriationRFA;

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PrepaidDataService/ExportDetailHistoricalTriation',
                dataType: 'JSON',
                data: { objHistoricalTriationRFA: objHistoricalTriationRFA, strOption: row.Option, strTransaction: row.Transaction, strDate: row.Date, strApplicative: row.Applicative },
                success: function (path) {
                    window.location =  $.app.getPageUrl({ url:'~/Dashboard/Home/DownloadExcel'}) + '?strPath=' + path + "&strNewfileName=DetalleHistoricoTriaciones.xlsx";
                },
                error: function (ex) {
                }
            });
        },
        getTriationDetail: function (row) {
            var that = this,
                controls = that.getControls(),
                objTriationDetail = {
                    strTelephone: Session.TELEPHONE,
                    strIdInteraction: row.Interaction,
                    strIdSession: Session.IDSESSION
                };

            that.rowTriationDetail = row;

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PrepaidDataService/DetailsStriations',
                data: JSON.stringify(objTriationDetail),
                complete: function () {
                    controls.btnTriationDetailExcel.addEvent(that, 'click', that.triationDetailExcel);
                },
                success: function (response) {
                    Session.DetalleHistoricalTriationRFA = response.data;
                    that.fillTriationDetail(response);
                },
                error: function (ex) {
                    controls.tblTriationDetail.find('tbody').html('');
                    $.app.error({
                        id: 'errorTriationDetail',
                        message: ex
                    });
                }
            });
        },
        fillTriationDetail: function (response) {
            if (response.data.lstNumbersTriationModel != null) {
                var data = response.data.lstNumbersTriationModel,
                    $tr0 = $('<tr>'),
                    $td01 = $('<td>'),
                    $td02 = $('<td>'),
                    $tr1 = $('<tr>'),
                    $td11 = $('<td>'),
                    $td12 = $('<td>'),
                    $tr2 = $('<tr>'),
                    $td21 = $('<td>'),
                    $td22 = $('<td>'),
                    $tr3 = $('<tr>'),
                    $td31 = $('<td>'),
                    $td32 = $('<td>'),
                    $tr4 = $('<tr>'),
                    $td41 = $('<td>'),
                    $td42 = $('<td>'),
                    $tr5 = $('<tr>'),
                    $td51 = $('<td>'),
                    $td52 = $('<td>'),
                    $tr6 = $('<tr>'),
                    $td61 = $('<td>'),
                    $td62 = $('<td>'),
                    $tr7 = $('<tr>'),
                    $td71 = $('<td>'),
                    $td72 = $('<td>'),
                    $tr8 = $('<tr>'),
                    $td81 = $('<td>'),
                    $td82 = $('<td>'),
                    $tr9 = $('<tr>'),
                    $td91 = $('<td>'),
                    $td92 = $('<td>'),
                    $tr10 = $('<tr>'),
                    $td101 = $('<td>'),
                    $td102 = $('<td>'),
                    $tr11 = $('<tr>'),
                    $td111 = $('<td colspan="2">'),
                    $btn = $('<button>', {
                        id: 'btnTriationDetailExcel',
                        class: 'btn claro-btn-info btn-sm',
                        html: 'Excel'
                    });

                $.extend(controls, { btnTriationDetailExcel: $btn });

                $tr0
                .append($td01.text('Nro trío 1:'))
                .append($td02.text(data[0].Description));

                $tr1
                .append($td11.text('Nro trío 2:'))
                .append($td12.text(data[0].Description2));

                $tr2
                .append($td21.text('Nro trío 3:'))
                .append($td22.text(data[0].Description3));

                $tr3
                .append($td31.text('Nro trío 4:'))
                .append($td32.text(data[0].Description4));

                $tr4
                .append($td41.text('Nro trío 5:'))
                .append($td42.text(data[0].Description5));

                $tr5
                .append($td51.text('Nro trío 6:'))
                .append($td52.text(data[0].Description6));

                $tr6
                .append($td61.text('Nro trío 7:'))
                .append($td62.text(data[0].Description7));

                $tr7
                .append($td71.text('Nro trío 8:'))
                .append($td72.text(data[0].Description8));

                $tr8
                .append($td81.text('Nro trío 9:'))
                .append($td82.text(data[0].Description9));

                $tr9
                .append($td91.text('Nro trío 10:'))
                .append($td92.text(data[0].Description10));

                $tr10
                .append($td101.text('Monto a cobrar:'))
                .append($td102.text(data[0].Amount));

                $tr11
                .append($td111.append($btn));

                this.getControls().tblTriationDetail.find('tbody').empty()
                .append($tr0)
                .append($tr1)
                .append($tr2)
                .append($tr3)
                .append($tr4)
                .append($tr5)
                .append($tr6)
                .append($tr7)
                .append($tr8)
                .append($tr9)
                .append($tr10)
                .append($tr11);
            } else {
                this.getControls().tblSubTableTracking.find('tbody').empty().append('');
            }
        },
        actionTriationDetail: function (send, args) {
            var that = this,
              detailRows = [];

            args.event.preventDefault();

            var tr = $(send).closest('tr');
            var row = that.tblHistoricalStriations.row(tr);
            var idx = $.inArray(tr.attr('id'), detailRows);

            if (row.child.isShown()) {
                tr.removeClass('details');
                row.child.hide();
                detailRows.splice(idx, 1);
            }
            else {
                tr.addClass('details');
                row.child(that.createTriationDetail()).show();
                that.getTriationDetail(row.data());

                if (idx === -1)
                    detailRows.push(tr.attr('id'));

            }
        },
        createTriationDetail: function () {
            var that = this,
            controls = that.getControls(),
            $divContainer = $('<div>'),
            $table = $('<table>', {
                id: 'tblTriationDetail',
                class: 'table claro-modal-tabla table-bordered table-hover'
            }, controls.form),
            $thead = $('<thead>', { class: 'claro-bg-second' }),
            $trhead = $('<tr>'),
            $th0 = $('<th>'),
            $th1 = $('<th>'),
            $tbody = $('<tbody>'),
            $trbody = $('<tr>'),
            $td = $('<td>', {
                colspan: 2,
                align: 'center'
            }),
            $img = $('<img>', {
                src:  '/Images/loading2.gif',
                width: 25,
                height: 25
            }),
            $divError = $('<div>', { id: 'errorTriationDetail' });

            $.extend(controls, { tblTriationDetail: $table });

            $trhead
            .append($th0.text('Trio'))
            .append($th1.text('Numero'))
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
        createTableHistoricalStriations: function (response) {

            var count = 0;
            if (response.data.lstHistoricalTriationRFA) {
                count = response.data.lstHistoricalTriationRFA.length;
            }
            $('#lblCountHistoricalStriations').html('Se encontraron ' + count + ' registro(s).');

            this.tblHistoricalStriations = this.getControls().tblHistoricalStriations.DataTable({
                info: false,
                paging: false,
                searching: false,
                select: 'single',
                scrollY: 350,
                scrollCollapse: true,
                destroy: true,
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true,
                data: response.data.lstHistoricalTriationRFA,
                columns: [
                    { "data": "Option" },
                    { "data": "Transaction" },
                    { "data": "Date" },
                    { "data": "Applicative" }
                ],
                columnDefs: [
                     {
                         targets: 0,
                         render: function (data, type, full, meta) {
                             return '<a href="#">' + data + '</a>';
                         }
                     },
                     {
                         targets: 2,
                         type: "date"
                     }
                ],
                order: [[2, 'desc']],
                language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
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

    $.fn.formHistoricalStriations = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formHistoricalStriations'),
                options = $.extend({}, $.fn.formHistoricalStriations.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formHistoricalStriations', data);
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

    $.fn.formHistoricalStriations.defaults = {
    }

    $('#ContentHistoricalStriations', $('.modal:last')).formHistoricalStriations();

})(jQuery);