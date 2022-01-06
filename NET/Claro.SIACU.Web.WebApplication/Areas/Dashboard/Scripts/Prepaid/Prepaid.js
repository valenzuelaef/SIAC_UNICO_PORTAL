
(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.Prepaid.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tabDataCall: $('.nav-tabs a[href="#divDataCall"]', $element)
            , tabDataReload: $('.nav-tabs a[href="#divDataReload"]', $element)
            , divDataReload: $('#divDataReload', $element)
            , divDataCall: $('#divDataCall', $element)
            , divDataCustomer: $('#divDataCustomer', $element)
            , divDataService: $('#divDataService', $element)
            , lblPre_BannerMessage: $('#lblPre_BannerMessage', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.tabDataCall.addEvent(that, 'click', that.tabDataCall_click);
            controls.tabDataReload.addEvent(that, 'click', that.tabDataReload_click);
            that.render();

        },

        render: function () {
            this.dataCustomerPre();
        },

        tabDataService_click: function () {
            var that = this,
                controls = this.getControls();

            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "btnReloadService";
            that.objError.funct = that.tabDataService_click;
            that.objError.that = that;

            var objDataService = Session.DATACUSTOMER.oDataService;

               if (objDataService != null && objDataService.Transaction != null) {
                controls.divDataService.showMessageErrorLoading(that.objError);
            } else {
                $.app.ajax({
                    type: 'POST',
                    url: '~/Dashboard/Prepaid/DataService',
                    dataType: 'html',
                    data: { strIdSession: Session.IDSESSION, Clear: (typeof (Session.IsClear) == 'undefined' || Session.IsClear == null ? false : Session.IsClear), objDataService: objDataService },
                    container: controls.divDataService,
                    complete: function () {
                        Session.IsClear = false;
                        if (typeof Session.strMsjValidationPre != "undefined" && Session.strMsjValidationPre != null) {
                            if (Session.strMsjValidationPre.indexOf("baja") > 0) {
                                $("#containerDataService *").prop('disabled', true);
                            }
                        }

                    },
                    error: function (ex) {
                        controls.divDataService.showMessageErrorLoading(that.objError);
                    },
                    success: function (result) {
                       
                        if ($("#containerDataService").attr('transaction') != undefined) {
                            controls.divDataService.showMessageErrorLoading(that.objError);
                        }
                        if (Session.DATASERVICE != null) {
                            Session.DATASERVICE.RateMB = $('#lblPre_RateMB').html();
                        Session.DATASERVICE.Plan = $('#lblPre_PlanRate').html();
                        }
                        if (typeof (Session.IsClear) != 'undefined' && Session.IsClear != null && Session.IsClear == true) {
                            var keys = ['gDisabledDetCallDetReChange'];
                            var val = getValueConfig(keys);
                            var value = (typeof val.gDisabledDetCallDetReChange != 'undefined' ? val.gDisabledDetCallDetReChange : "" );
                            var arr = value.split('|');
                            var VisibleCall = arr[0];
                            var VisibleRechange = arr[1];
                            $.each($('#divDashboardTabs li a'), function (i, e) {
                                if ($(e).attr('href').indexOf('divDataCall') != -1) {
                                    $(e).prop('disabled', parseInt(VisibleCall)==1?true:false);
                                } else if ($(e).attr('href').indexOf('divDataReload') != -1) {
                                    $(e).prop('disabled', parseInt(VisibleRechange)==1?true:false);
                                } 
                            });

                            $.each($('#carousel_inner li a'), function (i, e) {
                                console.log($(e).attr('profile'));
                                var opcion = $(e).attr('profile');
                                if ((opcion != 'SU_SIACA_TSAR' && opcion != 'SU_SIACA_TRECL' && opcion != 'SU_SIACA_INON')) {
                                    $(e).css('pointer-events', 'none');
                                    $(e).css('color', 'rgba(60, 50, 50, 0.4)');
                                    $(e).addClass('disabled');
                                }

                            });
                        }
                       
                        
                    }
                 
                });

            }
       
        },
        tabDataCall_click: function () {
            var that = this,
                controls = this.getControls();

            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "btnReloadCall";
            that.objError.funct = that.tabDataCall_click;
            that.objError.that = that;

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Prepaid/DataCall',
                dataType: 'html',
                cache: true,
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, isTFI: Session.DATASERVICE.IsTFI },
                container: controls.divDataCall,
                error: function (ex) {
                    that.objError.session = $(ex.responseText).find('title').html();
                    controls.divDataCall.showMessageErrorLoading(that.objError);
                },
                complete: function (result) {
                    if (result.statusText === 'OK') {
                        if ($("#contenedorDataCall").children().length <= 0) {
                            controls.divDataCall.showMessageErrorLoading(that.objError);
                        }
                    }
                }
            });
        },
        tabDataReload_click: function () {
            var that = this,
                controls = this.getControls();

            that.objError.message = $.app.const.messageErrorLoading;
            that.objError.buttonID = "btnReloadReload";
            that.objError.funct = that.tabDataReload_click;
            that.objError.that = that;

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Prepaid/DataReload',
                dataType: 'html',
                cache: true,
                data: { strIdSession: Session.IDSESSION, strTelephoneCustomer: Session.TELEPHONE, strMovementType: "", strcreditoDebito:"" },
                container: controls.divDataReload,
                error: function (ex) {
                    controls.divDataReload.showMessageErrorLoading(that.objError);
                },
                complete: function (result) {
                    if (result.statusText === 'OK') {
                        if ($("#contenedorDataReload").children().length <= 0) {
                            controls.divDataReload.showMessageErrorLoading(that.objError);
                        }
                    }
                }
            });

        },
        dataCustomerPre: function () {

            var that = this,
                controls = this.getControls();

            if (Session.DATACUSTOMER.BannerMessage != "") {
                controls.lblPre_BannerMessage.append(Session.DATACUSTOMER.BannerMessage);
            } else {
                $("#alertInfoPrepaid").css("display", "none");
            }

            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Prepaid/DataCustomer',
                dataType: 'html',
                cache: false,
                container: controls.divDataCustomer,
                success: function (result) {

                    if (Session.DATACUSTOMER.TelephoneCustomer != null && Session.DATACUSTOMER.TelephoneCustomer != "") {
                        $('#contenedorDataCustomer').PrepaidDataCustomer('customerPrepaid', Session.DATACUSTOMER);
                    }

                  
                },
                complete: function (result) {
                }
               
            });

            that.tabDataService_click();
            if (typeof Session.strMsjValidationPre != "undefined" && Session.strMsjValidationPre != null) {
                if (Session.strMsjValidationPre.indexOf("baja") > 0) {
                   
                    $("#divDashboardTabs").css("pointer-events", "none");
                }
            }


        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.href;
        }
    };

    $.fn.Prepaid = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['tabDataCall_click', 'tabDataReload_click', 'dataCustomerPre', 'tabDataService_click'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('Prepaid'),
                options = $.extend({}, $.fn.Prepaid.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('Prepaid', data);
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

    $.fn.Prepaid.defaults = {
        objError: {
            session: Session.IDSESSION,
            }
    }

    $('#contenedor').Prepaid();

})(jQuery);


