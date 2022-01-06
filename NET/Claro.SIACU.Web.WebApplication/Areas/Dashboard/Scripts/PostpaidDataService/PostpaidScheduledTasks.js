(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidScheduledTasks.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , txtContract: $('#txtContract', $element)
            , txtAccount: $('#txtAccount', $element)
            , txtPhone: $('#txtPhone', $element)
            , txtInteractionCode: $('#txtInteractionCode', $element)
            , txtAdviser: $('#txtAdviser', $element)
            , txtStartDate: $('#txtStartDate', $element)
            , txtEndDate: $('#txtEndDate', $element)
            , lblTransaction: $('#lblTransaction', $element)
            , lblState: $('#lblState', $element)
            , lblCacDac: $('#lblCacDac', $element)
            , tblScheduledTasks: $('#tblScheduledTasks', $element)
            , cboStateType: $('#cboStateType', $element)
            , cboTransactionType: $('#cboTransactionType', $element)
            , cboCacDacType: $('#cboCacDacType', $element)
            , errorScheduledTasks: $('#errorScheduledTasks', $element)
            , btnConsult: $('#btnConsult')
            , btnExportarScheduledTask: $('#btnExportarScheduledTask')
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
          
            var that = this,
            controls = this.getControls();

            controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', });
            controls.btnConsult.addEvent(that, 'click', that.btnConsult_click);
            controls.btnExportarScheduledTask.addEvent(that, 'click', that.btnExportToExcel_click);
            controls.btnExportarScheduledTask.attr('disabled', 'disabled');
            that.render();
        },
        render: function () {
            var that = this;

            that.setContractId();
            that.getStateType();
            that.getTransactionType();
            that.getCacDacType();
        },
        btnConsult_click: function () {
            var that = this,
                controls = that.getControls();
            var objScheduledTransaction = {
                    IdContract: controls.txtContract.val(),
                    Account: controls.txtAccount.val(),
                    StartDate: controls.txtStartDate.val(),
                    EndDate: controls.txtEndDate.val(),
                    State: controls.cboStateType.val(),
                    Adviser: controls.txtAdviser.val(),
                    TransactionType: controls.cboTransactionType.val(),
                    InteractionCode: controls.txtInteractionCode.val(),
                    CacDac: controls.cboCacDacType.val(),
                    strIdSession: Session.IDSESSION
                };
            controls.errorScheduledTasks.html('');
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tblScheduledTasks.find('tbody').html('<tr><td colspan="8" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetScheduledTransaction',
                data: JSON.stringify(objScheduledTransaction),
                complete: function () {
                    that.eliminar_click();
                    that.editar_click();
                },
                success: function (response) {
                    controls.tblScheduledTasks.find('tbody').html('');
                    if (response.data.ScheduledTransactions!=null) {
                        if (response.data.ScheduledTransactions.length > 0)
                            controls.btnExportarScheduledTask.removeAttr("disabled");
                    that.createTableScheduledTransaction(response);
                    } else {
                        controls.btnExportarScheduledTask.attr('disabled', 'disabled');
                       controls.tblScheduledTasks.find('tbody').html('<tr class="odd"><td align="center" valign="top" colspan="10" class="dataTables_empty">No existen datos</td></tr>');   
                    }
                  
                },
                error: function (msger) {
                    controls.tblScheduledTasks.find('tbody').html('');
                    $.app.error({
                        id: 'errorScheduledTasks',
                        message: msger,
                        click: function () {
                            that.btnConsult_click();
                        }
                    });
                }
            });
        },
        eliminar_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblScheduledTasks.find('tbody .deleteTasks').addEvent(that, 'click', that.deleteScheduledTasks);
        },
        editar_click: function () {
            var that = this,
                controls = that.getControls();

            controls.tblScheduledTasks.find('tbody .editTasks').addEvent(that, 'click', that.editScheduledTasks);
        },
        deleteScheduledTasks: function (send, args) {

            args.event.preventDefault();

           
        },
        editScheduledTasks: function (send, args) {

            var that = this,
                objContractServices = {},
                objRow = that.tblScheduledTasks.row($(send).parents('tr')).data();
           

            objContractServices.strIdSession = Session.IDSESSION;
            objContractServices.ServiceCode = objRow.ServiceCode;

            args.event.preventDefault();

        },
        btnExportToExcel_click: function () {
            var that = this,
                controls = that.getControls(),
                strUrlResult = '~/Dashboard/Home/DownloadExcel',

                objScheduledTransaction = {
                    
                    IdContract: controls.txtContract.val(),
                    Account: controls.txtAccount.val(),
                    StartDate: controls.txtStartDate.val(),
                    EndDate: controls.txtEndDate.val(),
                    State: controls.cboStateType.val(),
                    Adviser: controls.txtAdviser.val(),
                    TransactionType: controls.cboTransactionType.val(),
                    InteractionCode: controls.txtInteractionCode.val(),
                    CacDac: controls.cboCacDacType.val(),
                    strIdSession: Session.IDSESSION
                };
            
            $.app.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: '~/Dashboard/PostpaidDataService/ScheduledExportTransaction',
                data: JSON.stringify(objScheduledTransaction),
                success: function (path) {
                    window.location = $.app.getPageUrl({ url: strUrlResult }) + '?strPath=' + path + "&strNewfileName=ScheduledTasks.xlsx";
                }
            });
        },
        createStateType: function (response) {
            var that = this,
                controls = that.getControls(),
                sel = $('<select>', {
                    id: 'cboStateType',
                    name: 'cboStateType',
                    class: 'form-control'
                });

            sel.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.StateTypes, function (index, value) {
                    sel.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }

            controls.lblState.after(sel);
        },
        createCacDacType: function (response) {
            var that = this,
                controls = that.getControls(),
                sel = $('<select>', {
                    id: 'cboCacDacType',
                    name: 'cboCacDacType',
                    class: 'form-control'
                });

            sel.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.CacDacTypes, function (index, value) {
                    sel.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }

            controls.lblCacDac.after(sel);
        },
        createTransactionType: function (response) {
            var that = this,
            controls = that.getControls(),
            sel = $('<select>', {
                id: 'cboTransactionType',
                name: 'cboTransactionType',
                class: 'form-control'
            });

            sel.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.TransactionTypes, function (index, value) {
                    sel.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }

            controls.lblTransaction.after(sel);
        },
        createTableScheduledTransaction: function (response) {
           
           
            this.getControls().tblScheduledTasks.DataTable({
                info: false
                , paging: false
                ,scrollY:300
                , destroy: true
                , searching: false
                , select: 'single'
                , data: response.data.ScheduledTransactions
                , columns: [
                    { "data": "CoId" },
                    { "data": "CustomerId" },
                    { "data": "ServdDateProg" },
                    { "data": "ServdDateReg" },
                    { "data": "ServdDateEjec" },
                    { "data": "DescState" },
                    { "data": "DescServi" },
                    { "data": "ServcNumberAccount" },
                    { "data": "ServcTypeServ" },
                    { "data": null }
                ]
                , columnDefs: [
                    {
                        targets: 9,
                        render: function (data, type, full, meta) {
                            var actions = '';

                            if (full.ServcState != 3 && full.ServcState != 4) {
                                if (full.DescServi != 'REACTIVACION') {
                                    if (full.ServcCoSer == '1236') {
                                        if (full.ServcState == '1') {
                                            actions += '<a class="deleteTasks" href="#">Borrar Prog.</a>';
                                        }
                                    } else {
                                        actions += '<a class="deleteTasks" href="#">Borrar Prog.</a>';
                                    }
                                }
                                if (full.DescServi == 'REACTIVACION' || full.DescServi == 'SUSPENSION') {
                                    if (actions != '') {
                                        actions += '/<a class="editTasks" href="#">Editar Prog.</a>';
                                    } else {
                                        actions += '<a class="editTasks" href="#">Editar Prog.</a>';
                                }
                            }
                            } else
                                actions += '-';

                            return actions;
                        }
                    },
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4]
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
        getStateType: function () {
            var that = this,
                objStateType = {};

            objStateType.strIdSession = Session.IDSESSION;
            objStateType.IdList = "SERVC_ESTADO_HFC";

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objStateType),
                url: '~/Dashboard/PostpaidDataService/GetStateType',
                success: function (response) {
                    that.createStateType(response);
                }
            });
        },
        getTransactionType: function () {
            var that = this,
                objTransactionType = {};

            objTransactionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objTransactionType),
                url: '~/Dashboard/PostpaidDataService/GetTransactionType',
                success: function (response) {

                    that.createTransactionType(response);
                }
            });
        },
        getCacDacType: function () {
            var that = this,
            objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '~/Dashboard/PostpaidDataService/GetCacDacType',
                success: function (response) {
                    that.createCacDacType(response);
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setContractId: function () {
            this.getControls().txtContract.val(Session.DATACUSTOMER.ContractID);
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostPaidScheduledTasks = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['btnConsult_click', 'btnExportToExcel_click'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidScheduledTasks'),
                options = $.extend({}, $.fn.PostPaidScheduledTasks.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidScheduledTasks', data);
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

    $.fn.PostPaidScheduledTasks.defaults = {}

    $('#containerScheduledTasks', $('.modal:last')).PostPaidScheduledTasks();

})(jQuery);