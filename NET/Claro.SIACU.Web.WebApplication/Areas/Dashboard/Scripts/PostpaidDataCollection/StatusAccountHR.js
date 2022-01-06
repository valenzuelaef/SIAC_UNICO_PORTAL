(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidStatusAccountHR.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblStatusAccountHR: $('#tblStatusAccountHR', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            that.render();
        },
        render: function () {
            var that = this;

            that.DetailAmountDispute();
            if ($(".dataTables_scrollBody").length != 0) {
                $(".dataTables_scrollBody").animate({ scrollTop: $('.dataTables_scrollBody')[0].scrollHeight }, 1000);
            }
        },
        DetailAmountDispute: function () {
            var that = this,
            controls = that.getControls();
            that.TableHR = controls.tblStatusAccountHR.DataTable({
                info: false
              , paging: false
              , searching: false
              , scrollY: 400
              , scrollCollapse: true
              
              , destroy: true
              , select: 'single'
              , language: {
                  lengthMenu: "Display _MENU_ records per page",
                  zeroRecords: "No existen datos",
                  info: " ",
                  infoEmpty: " ",
                  infoFiltered: "(filtered from _MAX_ total records)"
              }
              , columnDefs: [
                  {
                      type: 'date-dd-mmm-yyyy',
                      targets: 5
                  },
                  {
                      type: 'date-dd-mmm-yyyy',
                      targets: 6
                  },
                  {
                      type: 'date-dd-mmm-yyyy',
                      targets: 7
                  },
                  {
                  "targets": 2,
                  "render": function (data, type, full, meta) {
                   
                      if (Session.USERACCESS.optionPermissions.indexOf('SU_ACP_LKHR') > 0) {
                          if (data.split("|")[1]=="") {
                              return data.split("|")[0];
                          } else {
                              return "<a class='Document' style='cursor: pointer'>" + data.split("|")[1] + "</a>";
                          }
                          
                      } else {
                          if (data.split("|")[1] == "") {
                              return data.split("|")[0];
                          } else {
                              return "<a class='Mensaje' style='cursor: pointer'>" + data.split("|")[1] + "</a>";
                          }
                          
                      }
                  }
                  },
                  {
                  "targets": 3,
                  "render": function (data, type, full, meta) {
                    
                      if (data.split("|")[1] == "") {
                          return data.split("|")[0];
                      } else {
                          return "<a class='Invoice' style='cursor: pointer'>" + data.split("|")[1] + "</a>";
                      }
                      }
                  }],
              "initComplete": function (settings, json) {
                  controls.tblStatusAccountHR.find('tbody').find('a[class="Document"]').addEvent(that, 'click', that.getDocumentHR);
                  controls.tblStatusAccountHR.find('tbody').find('a[class="Mensaje"]').addEvent(that, 'click', that.getMessageHR);
                  controls.tblStatusAccountHR.find('tbody').find('a[class="Invoice"]').addEvent(that, 'click', that.getInvoiceHR);
                
              }
            });
        },
        getDocumentHR: function (send) {
            var that = this;
                
           
            var ObjTableHr={};
            ObjTableHr = that.TableHR.row($(send).parents('td')).data();

            var DateIssueFormat = "";

                var xMonth = (ObjTableHr[6].split('/'))[1];
                var xYear = (ObjTableHr[6].split('/'))[2];

                console.log("xMonth: "+xMonth);
                console.log("xYear: " + xYear);
                DateIssueFormat = xYear + xMonth
           
            console.log(DateIssueFormat);
            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostPaidDataCollection/GetFileRute',
                data: {
                    strIdSession: Session.IDSESSION, strDocumenName: ObjTableHr[2].split('|')[1], DateIssue: DateIssueFormat, strType: ObjTableHr[1],isHistoryHR:false
                },
                success: function (result) {
                    if (result.data.FlagBill == "1") {
                        var valFilePath = result.data.FilePath;
                        var valFileName = result.data.FileName;
                        if (result.data.arrBuffer != null && result.data.arrBuffer != "" && result.data.arrBuffer != undefined) {
                            var url = $.app.getPageUrl({ url: '~/Dashboard/PostPaidDataCollection/ShowFile' });
                            var urlfin = url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION + "&strEmissionDate=";
                            window.open(urlfin, "HOJA DE RESUMEN", "");
                        } else {
                            showAlert("El archivo no existe");
                        }
                    }
                    else {
                        showAlert("El archivo no existe");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showAlert("El archivo no existe");
                }
            });
           
        },
        getMessageHR: function(send){
           
            showAlert("Su código de usuario no tiene los permisos necesarios para ver dicha Solicitud!. Por favor contáctese con su administrador.");
        },
        getInvoiceHR: function (send) {
           
            var that = this;


            var ObjTableHr = {};
            ObjTableHr = that.TableHR.row($(send).parents('td')).data();
            var DateIssueFormat = "";
            $.each(ObjTableHr[6].split('/'), function (index, value) {

                if (index == 2)
                    DateIssueFormat += value.slice(-2);
                else
                    DateIssueFormat += value

            });
            
            $.app.ajax({
                async: true,
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostPaidDataCollection/GetFileRute',
                data: {
                    strIdSession: Session.IDSESSION, strDocumenName: ObjTableHr[3].split('|')[1], DateIssue: DateIssueFormat, strType: ObjTableHr[1],isHistoryHR:false
                },
                success: function (result) {
                    if (result.data.FlagBill == "1") {
                        var valFilePath = result.data.FilePath;
                        var valFileName = result.data.FileName;
                        if (result.data.arrBuffer != null && result.data.arrBuffer != "" && result.data.arrBuffer != undefined) {
							var url = $.app.getPageUrl({ url: '~/Support/ShowFile.aspx' });
                            var urlfin = url + "?strFilePath=" + valFilePath + "&strFileName=" + valFileName + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION + "&strEmissionDate=";
                            window.open(urlfin, "FACTURA_ELECTRONICA", "");
                        } else {
                            showAlert("El archivo no existe");
                        }
                    }
                    else {
                        showAlert("El archivo no existe");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showAlert("El archivo no existe");
                }
            });
           
            
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        }
    }

    $.fn.FormPostpaidStatusAccountHR = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidStatusAccountHR'),
                options = $.extend({}, $.fn.FormPostpaidStatusAccountHR.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidStatusAccountHR', data);
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

    $.fn.FormPostpaidStatusAccountHR.defaults = {
    }

    $('#PostpaidStatusAccountHR', $('.modal:last')).FormPostpaidStatusAccountHR();
})(jQuery);

