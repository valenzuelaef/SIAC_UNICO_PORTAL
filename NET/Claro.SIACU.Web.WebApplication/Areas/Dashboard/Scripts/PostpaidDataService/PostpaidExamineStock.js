(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidExamineStock.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbId: $('#tbId', $element)
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
            that.getIds();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getIds: function () {
            var controls = this.getControls();
            var stUrlLogo =  "/Images/loading2.gif";
            controls.tbId.find('tbody').html('<tr><td colspan="2" align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</td></tr>');

            $.app.ajax({
                type: "POST",
                dataType: "json",
                url: '~/Dashboard/PostpaidDataService/GetMaterials',
                data: { strIdSession: Session.IDSESSION },
                success: function (result) {
                    var trHTML = '';
                    if (result.data != null) {
                        controls.tbId.DataTable({
                            "serverSide": false,
                            "ordering": false,
                            "scrollY": "180px",
                            "scrollCollapse": true,
                            "info":false,
                            "select":"single",
                            "paging": true,
                            "destroy": true,
                            "searching": false,
                            "data": result.data,
                            ajax: function (data, callback, settings) {
                                var out = [];

                                for (var i = data.start, ien = data.start + data.length ; i < ien ; i++) {
                                    out.push([i + '-1', i + '-2']);
                                }

                                setTimeout(function () {
                                    callback({
                                        draw: data.draw,
                                        data: out,
                                        recordsTotal: 160421,
                                        recordsFiltered: 160421
                                    });
                                }, 50);
                            },
                            "language": {
                                "lengthMenu": "Mostrar _MENU_ registros por página",
                                "zeroRecords": "No existen datos",
                                "info": " ",
                                "infoEmpty": " ",
                                "infoFiltered": "(filtered from _MAX_ total records)"
                            },
                            "columns": [
                              { "data": "Id" },
                              { "data": "Description" }
                            ]
                        })
                    }
                    else {
                        controls.tbId.find('tbody').html('<tr><td colspan="2" align="center">No existen datos</td></tr>');
                    }
                },
                error: function (msger) {
                    controls.tbId.find('tbody').html('<tr><td colspan="2" align="center">No existen datos</td></tr>');
                    $.app.error({
                        message: msger
                    });
                }
            });
        },

    };

    $.fn.PostpaidExamineStock = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidExamineStock'),
                options = $.extend({}, $.fn.PostpaidExamineStock.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidExamineStock', data);
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
    $.fn.PostpaidExamineStock.defaults = {
    }

    $('#PostpaidStock', $('.modal:last')).PostpaidExamineStock();

})(jQuery);