(function ($) {
    'use strict'
    var Form = function ($element, options) {
        $.extend(this, $.fn.FormListService.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , rdBtnChecked: $("input[name='rdbtnLine']:radio", $element)
            , btnSelectListService: $('#btnSelectListService', $element)
            , btnClose: $('#btnClose', $element)
        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = that.getControls();
            controls.btnSelectListService.addEvent(this, 'click', that.btnSelectListService_Click);
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            that.render();
            that.resizeContent();
        },

        render: function () {
            var that = this;
            that.DataListService();
            that.fnDisableBtn();
            that.rdbtnLinesNumberChange();
           
            that.ValidarRadioButtonCuenta();
        },
        DataListService: function () {
            var that = this;
          
            var cuenta = window.opener.$("#txtHdNumberAccount").val();
            var activaInactiva = window.opener.$("#txtHdActivaInactiva").val();
           
            $.app.ajax(
            {
                async: false,
                modal: true,
                type: "POST",
                dataType: "json",
                url: '~/Coliving/SearchCustomer/ListServiceCountCurrent',
                data:
                    {
                        strCuenta: cuenta,
                        strActivaInactiva: activaInactiva
                    },
                success: function (result) {
                    if (result.data != null) {
                        that.DataTablePost(result);
                    }
                },
                error: function () {
                    modalAlert('Estimado usuario, presentamos intermitencia de aplicativo.', 'Mensaje Transacciones');
                }
            });
        },
        DataTablePost: function (result) {
    

            console.log(result.data);
            $('#tListService').find('tbody').html('');
            $('#tListService').DataTable({
                "info": false,
                "scrollY": 500,
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": false,
                "ordering": false,
                "select": 'single',
                "data": result.data.ListSuscription,
                "columns": [
                    { "data": "LineNumber" },
                    { "data": "RatePlan" },
                    { "data": "LineStatus" },
                    {
                        "data": null
                        , render: function () { return '<input class="tdRdBtn" name="rdbtnLine" type="radio"/>' }
                    },
                ],
                columnDefs: [
                    { "className": "tdLinesNumber", "targets": [0] },
                ],
                language: {
                    lengthMenu: "",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },

        ValidarRadioButtonCuenta: function () {
            var that = this;
            var txtLineNumberListService = window.opener.$("#txtLineNumberListService").val();
            $("#tListService tr").each(function () {
                if ($(".tdLinesNumber", this).text() == txtLineNumberListService) {
                    $("input:radio[name=rdbtnLine]", this).attr('checked', true);
                    that.fnEnableBtn();
                }
            });
        },
        btnSelectListService_Click: function () {
            var valor = $("input:radio[name=rdbtnLine]:checked").parents("tr").find("td.tdLinesNumber").text();

           
            window.opener.$('#txtLineNumberListService').val(valor);
            window.opener.$("#txtHdLineNumber").val(valor);
            window.close();
        },
        rdbtnLinesNumberChange: function () {
            var that = this;
            //controls = that.getControls();
            $('#tbListService').on("click", "input.tdRdBtn", function () {
                that.fnEnableBtn();
                var valor = $("input:radio[name=rdbtnLine]:checked").parents("tr").find("td.tdLinesNumber").text();
                console.log("valor obtenerido" + valor);
            });
        },
        fnEnableBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSelectListService.removeAttr("disabled");
         
        },
        fnDisableBtn: function () {
            var that = this,
                controls = that.getControls();
            controls.btnSelectListService.attr("disabled", "disabled");
        },

        btnClose_click: function () {
            window.close();
        },
        resizeContent: function () {
            $(".modal-footer")[0].remove();
            var heightWindow = parseInt($(".modal-dialog").height()) + 50;

            if (heightWindow >= 738) {
                window.resizeTo(screen.width, screen.height);
                window.moveTo(0, 0);
            }
            else {
                window.resizeTo(900, heightWindow);
                window.moveTo(screen.width / 2 - 450, (screen.height / 2) - ((heightWindow) / 2));
            }
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        strUrl: window.location.href
    };


    $.fn.FormListService = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormListService'),
                options = $.extend({}, $.fn.FormListService.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormListService', data);
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

    $.fn.FormListService.defaults = {
    }
    $('#divListService').FormListService();

})(jQuery);