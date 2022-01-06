(function ($, undefined) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.SearchCases.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            searchTextBox: $('#searchCaseText', $element),
            byCaseId: $('#searchCaseByCaseId', $element),
            byReportId: $('#searchCaseByReportId', $element),
            byComplaintId: $('#searchCaseByComplaintId', $element),
            divSearchCaseResults: $('#searchCaseResults', $element),
            dataTable: null,
            modal: $element.parent().parent().parent()
        })
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            $('#searchCaseStart').click({ that: that }, that.start_click)
            $('#searchCaseClear').click({ that: that }, that.clear_click)
        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        start_click: function (e) {

            e.preventDefault()
            var that = e.data.that
            var searchValue = that.getControls().searchTextBox.val()
            var searchBy = that.getSelectedSearchByValue()

            that.block2()
            $.app.ajax({
                context: that,
                type: 'POST',
                url: that.strUrl + '/Cases/Search/Start',
                data: { strIdSession: Session.IDSESSION, strSearchValue: searchValue, strSearchBy: searchBy },
                success: that.start_click_success,
                error: that.start_click_error,
            })
        },
        getSelectedSearchByValue: function () {
            var controls = this.getControls()
            if (controls.byCaseId.is(':checked')) {
                return 'byCaseId'
            }
            else if (controls.byReportId.is(':checked')) {
                return 'byReportId'
            }
            else if (controls.byComplaintId.is(':checked')) {
                return 'byComplaintId'
            }
            else {
                return 'byCaseId'
            }
        },
        start_click_success: function (response) {                     
            var that = this
            var controls = that.getControls()

            if (controls.dataTable != null) {
                controls.dataTable.destroy()
                controls.dataTable = null
            }
            
            controls.divSearchCaseResults.html(response)
            controls.dataTable = $('#searchDetailTable').DataTable(that.getDataTableConfig())
            this.unblock2()
        },
        start_click_error: function () {
            this.unblock2()
            $('#divPostCustomerInfo').showMessageErrorLoading($.app.const.messageErrorLoading);
        },
        clear_click: function (e) {
            e.preventDefault()
            var that = e.data.that
            var controls = that.getControls()
            controls.searchTextBox.val('')
            controls.byReportId.prop('checked', false)
            controls.byComplaintId.prop('checked', false)
            controls.byCaseId.prop('checked', true)
        },
        getDataTableConfig: function () {
            return {
                "scrollY": "200px",

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

    $.fn.SearchCases = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('SearchCases'),
                options = $.extend({}, $.fn.SearchCases.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('SearchCases', data);
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

    $.fn.SearchCases.defaults = {
    }
})(jQuery)

$(document).ready(function () {
    $('#container').SearchCases()
})