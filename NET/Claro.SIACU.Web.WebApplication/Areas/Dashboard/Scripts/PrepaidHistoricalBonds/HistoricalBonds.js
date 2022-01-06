(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formHistoricalBonds.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtDateStartHB: $('#txtDateStartHB', $element)
            , txtDateEndHB: $('#txtDateEndHB', $element)
            , btnVisualize: $('#btnVisualize', $element)
            , containerDateRange: $('#containerDateRange', $element)
            , tblHistoricalBonds: $('#tblHistoricalBonds', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            var f = new Date();
            var dateHoy = f.getDate() < 10 ? '0' + f.getDate() : f.getDate(),
                dateMonth = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1),
                dateMonthPre = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1);

            var fDateToDay = dateHoy + "/" + dateMonth + "/" + f.getFullYear();
            var fDatePrevious = dateHoy + "/" + dateMonthPre + "/" + parseInt(f.getFullYear() - 1);

            controls.txtDateEndHB.val(fDateToDay);
            controls.txtDateStartHB.val(fDatePrevious);
            controls.containerDateRange.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnVisualize.addEvent(that, 'click', that.btnVisualize_click);
            
            that.render();
        },
        btnVisualize_click: function () {
            var controls = this.getControls();
            var oHistorical = {};
            oHistorical.strIdSession = Session.IDSESSION;
            oHistorical.strTelephone = Session.TELEPHONE;
            oHistorical.strPlanTariff = Session.PlanRate;
            oHistorical.strDateStartHB = controls.txtDateStartHB.val();
            oHistorical.strDateEndHB = controls.txtDateEndHB.val();
            var stUrlLogo = "/Images/loading2.gif";
            $('#tblHistoricalBonds tbody').html('<tr><td colspan="10" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/PrepaidDataService/HistoricalBondsSearch',
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                cache: true,
                data: JSON.stringify(oHistorical),
                success: function (result) {
                    Session.listHistoricalBonds = [];
                    if (result.data.listHistoricalBondsModel !== null && result.data.listHistoricalBondsModel !== undefined) {
                        Session.listHistoricalBonds = result.data.listHistoricalBondsModel;
                    var trHTML = '';
                   
                        result.data.listHistoricalBondsModel.forEach(function (item) {
                            trHTML += '<tr><td class="col-md-2">' + item.IdTransaction +
                                      '</td><td class="col-md-2">' + item.Transaction +
                                      '</td><td class="col-md-2">' + item.Promotion +
                                      '</td><td class="col-md-2">' + item.RegistrationDate +
                                      '</td><td class="col-md-2">' + item.Applicative +
                                      '</td></tr>' +
                                      '<tr align=left id="' + item.Accountant + '" style=display: none; align=center></tr>';
                        });
                        $('#tblHistoricalBonds tbody').html(trHTML);
                    } else {
                        $('#tblHistoricalBonds tbody').html('<tr><td colspan="10" align="center">No existen datos</td></tr>');
                    }
                    
                },
                error: function (ex) {

                }
            });
        },
        render: function () {
            this.getDataTable();
            this.btnVisualize_click();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate',
        getDataTable: function () {
            var controls = this.getControls();

            controls.tblHistoricalBonds.DataTable({
                "scrollY": "200px",
                "info": false,
                "scrollCollapse": true,
                "paging": false,
                "select": "single",
                "searching": false,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
        },
        GetExportHistoricalBonds: function () {
                      
        var strUrlModal = '~/Dashboard/PrepaidDataService/ExportExcelHistoricalBonds';
        var strUrlResult = '~/Dashboard/Home/DownloadExcel';
      
        var lstBondsHistorical = Session.listHistoricalBonds;
       
        $.app.ajax({
            type: 'POST',
            cache: false,
            url: strUrlModal,
            data: { listHistoricalBonds: lstBondsHistorical, Telephone: Session.TELEPHONE },
            success: function (path) {
                window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=HistoricoBonosPrepago.xlsx";
        }
        });
           
    },

    };

    $.fn.formHistoricalBonds = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['GetExportHistoricalBonds'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formHistoricalBonds'),
                options = $.extend({}, $.fn.formHistoricalBonds.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formHistoricalBonds', data);
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

    $.fn.formHistoricalBonds.defaults = {
    }

    $('#ContentHistoricalBonds', $('.modal:last')).formHistoricalBonds();

})(jQuery);

