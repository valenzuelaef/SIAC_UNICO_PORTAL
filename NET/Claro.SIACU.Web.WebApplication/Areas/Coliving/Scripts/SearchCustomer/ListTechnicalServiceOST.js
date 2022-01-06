(function ($) {
    'use strict'
    var Form = function ($element, options) {
        $.extend(this, $.fn.FormListTechnicalServiceOST.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , txtHdLineNumber: $('#txtHdLineNumber', $element)
            , txtHdClientType: $('#txtHdClientType', $element)
            , txtHdClient: $('#txtHdClient', $element)
            , txtHdMigrateStatus: $('#txtHdMigrateStatus', $element)
            , txtHdUrlDestino: $('#txtHdUrlDestino', $element)
            , btnClose: $('#btnClose', $element)
        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = that.getControls();
            controls.btnClose.addEvent(this, 'click', that.btnClose_click);
            that.render();
            that.resizeContent();
        },

        render: function () {
            var that = this;
            that.DataListTechnicalService();
            that.lnkIrApp();
        },
        DataListTechnicalService: function () {
            var that = this;
            var controls = that.getControls();
            var _LineNumber = controls.txtHdLineNumber.val();

            $.app.ajax(
            {
                async: false,
                modal: true,
                type: "POST",
                dataType: "json",
                url: '~/Coliving/SearchCustomer/GetListOST',
                data:
                    {
                        strIdSession: Session.IDSESSION,
                        objSearchOst: {
                            IdBusca: _LineNumber,
                            IdCriterio: "2",
                            IdEstado: "-1",
                            IdCAC: "-1"
                        }
                    },
                success: function (result) {
                    console.log(result);
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
            var that = this;
            var controls = that.getControls();
            var strURLDestino = controls.txtHdUrlDestino.val();
            var ostLegado = [];

            console.log(result.data);
            
            ostLegado = $.grep(result.data, function (h) {
                return h.Origen == 'LEGADO';
            });

            if (ostLegado.length == 0) {

                var altura = window.screen.height;
                var anchura = window.screen.width;
                var y = parseInt((window.screen.height / 2) - (altura / 2));
                var x = parseInt((window.screen.width / 2) - (anchura / 2));

                window.open(strURLDestino.split("|")[1], '', 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',top=0,left=0,toolbar=no,location=no,status=no,menubar=no,directories=no');
                window.close();
            }

            $('#tListTechnicalServiceOST').find('tbody').html('');
            $('#tListTechnicalServiceOST').DataTable({
                "dom": '<"toolbar">frtip',
                "info": false,
                "scrollY": 500,
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": false,
                "ordering": false,
                "select": 'single',
                "data": result.data,
                "columns": [
                    { "data": "NroOst" },
                    { "data": "Cac" },
                    { "data": "FechaGeneracion" },
                    { "data": "Imei" },
                    { "data": "Marca" },
                    { "data": "Modelo" },
                    { "data": "Origen" },
                    {
                        "data": null
                        , render: function () { return '<a id="lnkIrA" class="tdlnkIrA" href="#">IR</a>' }
                    },
                ],
                columnDefs: [
                    { "className": "tdNroOst", "targets": [0] },
                    { "className": "tdOrigen", "targets": [6] },
                ],
                language: {
                    lengthMenu: "",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
            $("div.toolbar").html('<b>Listado de OST</b>');
        },

        lnkIrApp: function () {
            var that = this;
            var controls = that.getControls();
            var strURLDestino = controls.txtHdUrlDestino.val();

            $('#tbListTechnicalServiceOST').on("click", ".tdlnkIrA", function () {
                var altura = window.screen.height;
                var anchura = window.screen.width;
                var y = parseInt((window.screen.height / 2) - (altura / 2));
                var x = parseInt((window.screen.width / 2) - (anchura / 2));
                var origen = $(this).parents("tr").find("td.tdOrigen").text();

                if (origen == "LEGADO") {
                    window.open(strURLDestino.split("|")[0], '', 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',toolbar=no,location=no,status=no,menubar=no,directories=no');
                } else if (origen == "ONE") {
                    window.open(strURLDestino.split("|")[1], '', 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',toolbar=no,location=no,status=no,menubar=no,directories=no');
                }
                else {
                    modalAlert('Estimado usuario, Seleccione una linea', 'Mensaje Transacciones');
                }

            });

            $('#lnkAqui').on("click", function () {
                var altura = window.screen.height;
                var anchura = window.screen.width;
                var y = parseInt((window.screen.height / 2) - (altura / 2));
                var x = parseInt((window.screen.width / 2) - (anchura / 2));
                window.open(strURLDestino.split("|")[1], '', 'width=' + anchura + ',height=' + altura + ',top=' + y + ',left=' + x + ',top=0,left=0,toolbar=no,location=no,status=no,menubar=no,directories=no');
            });

            

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


    $.fn.FormListTechnicalServiceOST = function () {
        var option = arguments[0],
            args = arguments,
            value;

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormListTechnicalServiceOST'),
                options = $.extend({}, $.fn.FormListTechnicalServiceOST.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormListTechnicalServiceOST', data);
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

    $.fn.FormListTechnicalServiceOST.defaults = {
    }
    $('#divListTechnicalServiceOST').FormListTechnicalServiceOST();

})(jQuery);