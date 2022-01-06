(function ($, undefined) {
    'use strict'

    var Form = function ($element, options) {
        $.extend(this, $.fn.formMassiveInstant.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element
            , txtDateStart: $('#txtDateStart', $element)
            , txtDateEnd: $('#txtDateEnd', $element)
            , btnImportLine: $('#btnImportLine', $element)
            , btnClearFile: $('#ClearFile', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            $('#InstMasiveFrame').hide();
            $('#MakeChargePetitionFrame').hide();
            ValidateFrame(Session.TypeMenuInst);
            var that = this,
            controls = this.getControls();
            controls.btnClearFile.addEvent(that, 'click', that.btnClearFile_click);
            var f = new Date();
            var dateHoy = f.getDate() < 10 ? '0' + f.getDate() : f.getDate(), dateMonth = (f.getMonth() + 1) < 10 ? '0' + (f.getMonth() + 1) : (f.getMonth() + 1), dateMonthPre = f.getMonth() < 10 ? '0' + f.getMonth() : f.getMonth();
            var fDateToDay = dateHoy + "/" + dateMonth + "/" + f.getFullYear();
            var fDatePrevious = dateHoy + "/" + dateMonthPre + "/" + f.getFullYear();
            var urlInst = "";
            if (Session.TypeMenuInst == "INSTMASIVE") {
                urlInst = "~/Management/MassiveInstant/ImportLine"
            } else if (Session.TypeMenuInst == "MakeChargePetition") {
                urlInst = "~/Management/MassiveInstant/ImportArchive"
            }
            var uploader = new plupload.Uploader({
                runtimes: 'html5,flash,silverlight,html4',
                browse_button: 'FileText',
                url: that.strUrl + urlInst,
                filters: {
                    max_file_size: '10mb',
                    mime_types: [{ title: "files", extensions: "txt,csv" }]
                },

                multipart_params: {
                    strIdSession: Session.IDSESSION,
                    strAplication: Session.DATACUSTOMER.Application,
                    strTypeSearch: Session.SEARCHTYPE
                },
                multiple_queues: false,
                init: {
                    FilesAdded: function (sender, e) {
                        var stUrlLogo = "/Images/loading2.gif";
                        $('#184209_maindlg').block({
                            css: {
                                border: 'none',
                                padding: '15px',
                                backgroundColor: '#000',
                                '-webkit-border-radius': '10px',
                                '-moz-border-radius': '10px',
                                opacity: .8,
                                color: '#fff'
                            },
                            overlayCSS: { backgroundColor: '#FFFFFF', opacity: 0.0, cursor: 'wait' },
                            message: '<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                        });
                        uploader.start();
                    },
                    FileUploaded: function (sender, e, response) {
                        $('#184209_maindlg').unblock();
                        var data = $.parseJSON(response.response);
                        if (Session.TypeMenuInst == "INSTMASIVE") {
                            if (data.data.ResultFlag == false) {
                                Session.ListMassiveInstantCorrect = [];
                                Session.ListMassiveInstantFail = [];
                                $.each(data.data.listInstant, function (key, val) {
                                    if (val.Status == "Ok") {
                                        Session.ListMassiveInstantCorrect.push({ "Number": val.Number });
                                    }
                                    else {
                                        Session.ListMassiveInstantFail.push({ "Number": val.Number, "Description": val.Description });
                                    }
                                });

                                if (Session.ListMassiveInstantCorrect.length > 0) {
                                    MasiveInstantCorrect(Session.ListMassiveInstantCorrect);
                                    $("#lblCountLine").html("Total líneas para grabar: " + Session.ListMassiveInstantCorrect.length);
                                }

                                if (Session.ListMassiveInstantFail.length > 0) {
                                    MasiveInstantFail(Session.ListMassiveInstantFail);
                                }
                            }
                            else {
                                
                                showAlert('Solo se admiten archivos con 1000 registros como máximo.', 'Atención');
                            }

                        } else if (Session.TypeMenuInst == "MakeChargePetition") {
                            if (data.data[1] != "") {
                                Session.ArchivoIntantanea = data.data[0];
                                Session.NewArchivoIntantanea = data.data[1];
                                $('#MakeChargePetitionFrame').show();
                                $('#FileNameInst').html("Pedido Archivo para Cargar Instantáneas: <span style='font-weight:100'>" + data.data[1] + "</span>");
                            }
                            else {
                                $('#184209_maindlg').unblock();
                                
                                showAlert('Ocurrio un error al obtener el correlativo. Intentelo nuevamente', 'Error');
                            }
                        }
                    },
                    Error: function (sender, e) {
                        $('#184209_maindlg').unblock();
                   
                        showAlert('Ocurrio un error intentelo nuevamente', 'Error');
                    }
                }

            });

            uploader.init();
            controls.txtDateEnd.val(fDateToDay);
            controls.txtDateStart.val(fDatePrevious);
            controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateStart.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnImportLine.addEvent(that, 'click', that.btnImportLine_click);
            that.render();
        },
        render: function () {
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        btnClearFile_click: function () {
            $('#MakeChargePetitionFrame').hide();
            Session.ArchivoIntantanea = "";
            Session.NewArchivoIntantanea = "";
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'
    };

    $.fn.formMassiveInstant = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formMassiveInstant'),
                options = $.extend({}, $.fn.formMassiveInstant.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formMassiveInstant', data);
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

    $.fn.formMassiveInstant.defaults = {
    }

})(jQuery);

function MasiveInstantCorrect(ListIntMasive) {
    $('#tblInsMassCorrect').DataTable({
        "scrollY": "150px",
        "scrollCollapse": true,
        "destroy": true,
        "paging": false,
        "searching": false,
        "select": "single",
        "info" : false,
        "data": ListIntMasive,
        "language": {
            "lengthMenu": "Display _MENU_ records per page",
            "zeroRecords": "No existen datos",
            "info": " ",
            "infoempty": " ",
            "infoFiltered": "(filtered from _MAX_ total records)"
        }, "columns": [
                { "data": "Number" }
        ]
    });
}


function MasiveInstantFail(ListError) {
    $.window.open({
        modal: false,
        title: "Lista de Líneas Fallados al Registrar",
        url: '~/Management/MassiveInstant/MasiveInstantFail',
        data: { strIdSession: Session.IDSESSION },
        width: 1024,
        height: 600,
        buttons: {
            Cancelar: {
                click: function (sender, args) {
                    this.close();
                }
            }
        }
    });
}

$(document).ready(function () {
    $('#ContentMassiveInstant').formMassiveInstant();
    $('#txthoursVig').keyup(function () { this.value = this.value.replace(/[^0-9]/g, ''); });
    $('#txthoursCad').keyup(function () { this.value = this.value.replace(/[^0-9]/g, ''); });
    $('#txtminutesVig').keyup(function () { this.value = this.value.replace(/[^0-9]/g, ''); });
    $('#txtminutesCad').keyup(function () { this.value = this.value.replace(/[^0-9]/g, ''); });
});

function ValidateFrame(typeInst) {
    if (typeInst != "") {
        if (typeInst == "INSTMASIVE") {
            $('#InstMasiveFrame').show();
            $('#MakeChargePetitionFrame').hide();
        } else if (typeInst == "MakeChargePetition") {
            $('#InstMasiveFrame').hide();
            $('#MakeChargePetitionFrame').hide();
        }

    } else {
        
       

        showAlert('Ocurrio un error intentelo nuevamente', 'Datos Incorrectos');
    }
}