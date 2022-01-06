(function ($, undefined) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.Queue.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            dataTable: null,
            modal: $element.parent().parent().parent()
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            $('#tempId a').click({ that: that }, that.queue_click);
        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        queue_click: function (e) {
            e.preventDefault()
            var that = e.data.that
            var strQueueName = $(e.target).data('id')
            that.block2()
            $.app.ajax({
                context: that,
                type: 'POST',
                url: that.strUrl + '/Cases/Queue/QueueDetail',
                data: { strIdSession: Session.IDSESSION, strQueueName: strQueueName },
                success: that.queue_click_success,
                error: that.queue_click_error,
            })
        },
        queue_click_success: function (response) {
            var controls = this.getControls()
            if (controls.dataTable !== null) {
                controls.dataTable.destroy()
                controls.dataTable = null
            }

            $('#queueTreeData').html(response)
            $('#queueDetailTable a.caseNote').click({ that: this }, this.case_click);

            controls.dataTable = $('#queueDetailTable').DataTable(this.getDataTableConfig())

            this.unblock2()
        },
        queue_click_error: function (ex) {
            this.unblock2()
            $('#divPostCustomerInfo').showMessageErrorLoading($.app.const.messageErrorLoading);
        },
        case_click: function (e) {
            e.preventDefault()
            var that = e.data.that
            var strCaseId = $(e.target).data('id')

            that.block2()

            $.app.ajax({
                context: that,
                type: 'POST',
                url: that.strUrl + '/Cases/Queue/CaseNotes',
                data: { strIdSession: Session.IDSESSION, strCaseId: strCaseId },
                success: that.case_click_success,
                error: that.case_click_error,
            })
        },
        case_click_success: function (response) {
            $('#queueCaseNotes').html(response)
            that.unblock2()
        },
        case_click_error: function (ex) {
            this.unblock2()
            $('#divPostCustomerInfo').showMessageErrorLoading($.app.const.messageErrorLoading);
        },
        getDataTableConfig: function(){
            return {
                "scrollY": "200px",
                "scrollX": "100%",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "language": {
                    "zeroRecords": " ",
                    "info": "",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "emptyTable": "No existen datos"
                }
            }
        },
        block2: function () {
            var controls = this.getControls()
            controls.modal.block({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .9,
                    color: '#fff'
                },
                overlayCSS: { backgroundColor: '#FFFFFF', opacity: 0.0, cursor: 'wait' },
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
            });
        },
        unblock2: function () {
            var controls = this.getControls()
            controls.modal.unblock()
        },
        strUrl: window.location.href,
        strUrlLogo: '/Images/loading2.gif'
    };

    $.fn.Queue = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('Queue'),
                options = $.extend({}, $.fn.Queue.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('Queue', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw 'Unknown method: ' + option;
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

    $.fn.Queue.defaults = {
    }
})(jQuery)

$(document).ready(function () {
    var container = $('#container')
    container.Queue()
})