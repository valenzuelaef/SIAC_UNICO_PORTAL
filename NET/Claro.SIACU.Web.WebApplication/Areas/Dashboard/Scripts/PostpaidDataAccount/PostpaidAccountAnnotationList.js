(function ($) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.formAccountAnnotationList.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblAccountAnnotationList: $('#tblAccountAnnotationList', $element)
            , lknDescription: $('#lknDescription', $element)

        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.tblAccountAnnotationList.find("tr").find('td:eq(7)').each(function () {
                var $this = $(this);
                $(this).find('a').on("click", function () { that.SearchDetailAnnotation($this) });
            });
            that.render();
        },
        SearchDetailAnnotation: function (sender) {


            var td = sender.parent().find('td');

            var description = Session.DATACUSTOMER.objPostDataAccount.plataformaAT === "TOBE" ? td.eq(12).text() : td.eq(3).text();
            var usuCrea = td.eq(4).text();
            var fechaCrea = td.eq(5).text();
            var ticket = td.eq(8).text();
            var dateClose = td.eq(9).text();
            var dateOpen = td.eq(10).text();
            var dateModif = td.eq(11).text();
            var CodAnotacion = td.eq(0).text();

            $.window.open({
                modal: true,
                title: "Detalle Anotaciones",
                url: '~/Dashboard/PostpaidDataAccount/AccountDetailAnnotation',
                data: {
                    strIdSession: Session.IDSESSION,
                    strCustomerId: Session.DATACUSTOMER.CustomerID,
                    strNumberTickler: ticket,
                    strDescription: description,
                    strUsuCrea: usuCrea,
                    strFechaCrea: fechaCrea,
                    strdateClose: dateClose,
                    strdateOpen: dateOpen,
                    strdateModif: dateModif,
                    platform: Session.DATACUSTOMER.objPostDataAccount.plataformaAT,
                    strCodeAnotacion : CodAnotacion

                },
                width: 1231,
                height: 580,
                buttons: {
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },
        desingAnnotationList: function () {
            var that = this,
            controls = that.getControls();
            controls.tblAccountAnnotationList.DataTable({
                info: false,
                scrollY: "330px",
                select: 'single',
                paging: false,
                searching: false,
                destroy: true,
                scrollX: true,
                sScrollXInner: "100%",
                autoWidth: true,
                order: [[5, 'desc']],
                columnsDefs: [
                    { type: 'date-ddmmyyyyhhmisstt', targets: [5] }
                ],
                language:
                    {
                        lengthMenu: "Display _MENU_ records per page",
                        zeroRecords: "No existen datos",
                        info: " ",
                        infoEmpty: " ",
                        infoFiltered: "(filtered from _MAX_ total records)"
                    }
            });
        },
        render: function () {
            var that = this;
            that.desingAnnotationList();

        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href,
        strUrlTemplate: '/Home/DialogTemplate'

    };

    $.fn.formAccountAnnotationList = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('formAccountAnnotationList'),
                options = $.extend({}, $.fn.formAccountAnnotationList.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('formAccountAnnotationList', data);
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

    $.fn.formAccountAnnotationList.defaults = {
    }

    $('#ContentAccountAnnotationList', $('.modal:last')).formAccountAnnotationList();

})(jQuery);
