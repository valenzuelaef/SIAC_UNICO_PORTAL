var string = String;

if (!string.format) {
    string.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

(function ($, undefined) {
    var __jControlWindow__1 = function ($element, options) {
        $.control.control.call(this, options, $element);

        var id = this.getId();

        if ($.string.isEmptyOrNull(id)) {
            id = string.format('window-{0}', $.guid);
        }

        this.getElement().attr('id', id);
        this._setSettings('id', id);
    }
    __jControlWindow__1.prototype = {
        constructor: __jControlWindow__1,
        init: function () {
            this.render();
        },
        render: function () {
            $(document.body).show();

            var $modal = this.getElement(),
                $dialog = this._createDialogElement($modal),
                $content = this._createContentElement($dialog);

            this._createHeaderElement($content);
            var $body = this._createBodyElement($content);
            this._createFooterElement($content);
            this.getWidth();
            this.getHeight();
            var    that = this,
                url = this.getUrl();

            if (!$.string.isEmptyOrNull(url)) {
                
                $.blockUI({
                    message: $('#ModalLoad'),
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .3,
                        color: '#fff',
                    }
                });
                

                $.ajax({
                    type: that.getType(),
                    url: $.app.getPageUrl({ url: url }),
                    data: that.getData(),
                    async: true,
                    cache: false,
                    complete: function (jqXHR, textStatus) {
                       
                        $.unblockUI;
                      
                    },
                    success: function (data, textStatus, jqXHR) {
                        $body.html(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $body.html(errorThrown);
                    }
                });
            } else {
                $body.html(this.getText());

                var t = setTimeout(function () {
                    
                    clearTimeout(t);
                }, 1);
            }

            if (this.getModal() == true) {
                
            }

            
        },
        _createElement: function (className, $container) {
            var $element = $(string.format('.{0}', className), $container);

            if ($element.length === 0) {
                $element = $('<div>');

                $element
                    .addClass(className)
                    .appendTo($container);
            } else {
                throw string.format('The {0} element already exists', className);
            }

            return $element;
        },
        _createDialogElement: function ($container) {
            return this._createElement(__jControlWindow__1.dialogClass, $container);
        },
        _createContentElement: function ($container) {
            return this._createElement(__jControlWindow__1.contentClass, $container);
        },
        _createBodyElement: function ($container) {
            return this._createElement(__jControlWindow__1.bodyClass, $container);
        },
        _createHeaderElement: function ($container) {
            var $header = this._createElement(__jControlWindow__1.headerClass, $container);

            
            this._createHeaderTitleElement($header);

            return $header;
        },
        _createHeaderTitleElement: function ($container) {
            var title = this.getTitle();

            if (!$.string.isEmptyOrNull(title)) {
                this._createElement(__jControlWindow__1.titleClass, $container).text(title);
            }
        },
        _createHeaderControlboxElement: function ($container) {
            var $controlBox=null,
                buttons;

            buttons = __jControlWindow__1.controlBox;

            if (this.getControlBox() && !$.array.isEmptyOrNull(buttons)) {
                var modal = this.getModal();

                $controlBox = this._createElement('modal-controlbox', $container);

                $controlBox.addClass('pull-right');

                var $button;

                if (!modal && this.getMinimizeBox()) {
                    $button = new $.control.button(buttons['minimize']);
                    this.add($button, $controlBox);
                }

                if (this.getMaximizeBox()) {
                    $button = new $.control.button(buttons['maximize']);
                    this.add($button, $controlBox);
                }

                $button = new $.control.button(buttons['close']);
                this.add($button, $controlBox);
            }
        },
        _createFooterElement: function ($container) {
            var that,
                $footer,
                buttons = this.getButtons();

            $footer = this._createElement(__jControlWindow__1.footerClass, $container);

            if (buttons != null) {
                that = this;

                $.each(buttons, function (index, button) {
                    if ($.string.isEmptyOrNull(button.text) && $.string.isEmptyOrNull(button.html)) {
                        button.text = index;
                    }

                    var $button = new $.control.button(button);

                    that.add($button, $footer);
                });
            } else {
                $footer = null;
            }

            return $footer;
        },
        close: function () {
            window.close();
        },
        getTarget: function () {
            return this._getSettings('target');
        },
        getAsync: function () {
            return this._getSettings('async');
        },
        getData: function () {
            return this._getSettings('data');
        },
        getType: function () {
            return this._getSettings('type');
        },
        getWidth: function () {
            return this._getSettings('width');
        },
        getHeight: function () {
            return this._getSettings('height');
        },
        getTitle: function () {
            return this._getSettings('title');
        },
        getText: function () {
            return this._getSettings('text');
        },
        getModal: function () {
            return this._getSettings('modal');
        },
        getUrl: function () {
            return this._getSettings('url');
        },
        _getAutoSize: function () {
            return this._getSettings('autoSize');
        },
        getControlBox: function () {
            return this._getSettings('controlBox');
        },
        getMaximizeBox: function () {
            return this._getSettings('maximizeBox');
        },
        getMinimizeBox: function () {
            return this._getSettings('minimizeBox');
        },
        getButtons: function () {
            return this._getSettings('buttons');
        }
    };

    __jControlWindow__1.controlBox = {
        minimize: {
            enable: true,
            class: 'fa fa-window-minimize',
            click: function (e) {
                this.minimize();
            }
        },
        maximize: {
            enable: true,
            id: 'window-maximize',
            class: 'fa fa-window-maximize',
            click: function (sender, args) {
                this._resize();
            }
        },
        close: {
            enable: true,
            class: 'fa fa-window-close',
            click: function (sender, args) {
                this.close();
            }
        }
    };
    __jControlWindow__1.contentClass = 'modal-content';
    __jControlWindow__1.dialogClass = 'modal-dialog';
    __jControlWindow__1.headerClass = 'modal-header claro-red';
    __jControlWindow__1.titleClass = 'modal-title';
    __jControlWindow__1.bodyClass = 'modal-body';
    __jControlWindow__1.footerClass = 'modal-footer';
    __jControlWindow__1.loadingClass = 'modal-loading';
    __jControlWindow__1.class = 'modal';
    __jControlWindow__1.toolbar = null;
    __jControlWindow__1.overlay = [];
    __jControlWindow__1.modals = [];
    __jControlWindow__1.defaults = {
        autoSize: false,
        type: 'GET',
        async: true,
        data: null,
        target: '',
        url: '',
        title: null,
        modal: false,
        width: '300px',
        height: '160px',
        controlBox: true,
        maximizeBox: true,
        minimizeBox: true,
        buttons: null,
        complete: null,
    }
    __jControlWindow__1.descriptor = {
        settings: ['autoSize', 'async', 'data', 'target', 'url', 'data', 'text', 'title', 'type', 'modal', 'width', 'height', 'controlBox', 'maximizeBox', 'minimizeBox', 'buttons'],
        events: ['complete']
    }

    $.fn.windowPopup = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('windowPopup'),
                options = $.extend({}, $.fn.windowPopup.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new __jControlWindow__1($this, options);
                $this.data('windowPopup', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
               
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });
        return value || this;

    };
})(jQuery, null);
