//! Filename  : ClaroAppCommon.js
//! version   : 1.0.0 (last revision Ene 19, 2017)
//! author    : everis - www.everis.com.
//! copyright : Claro - Americatel Perú
//! www.claro.com.pe

(function ($, undefined) {
    "use strict";
    var __jControl__ = function (options, $element) {
        var constructor = this.constructor,
			descriptor = constructor.descriptor,
			defaults = constructor.defaults;

        var that = this;

        if (this.__settings == null) {
            this.__settings = {};
        }

        $.each(descriptor.settings.concat(__jControl__.descriptor.settings), function (index, name) {
            var value = options[name];

            if (value == null || value == undefined) {
                value = defaults[name];
            }

            that.__settings[name] = value;
        });

        if (this.__events == null) {
            this.__events = {};
        }

        $.each(descriptor.events.concat(__jControl__.descriptor.events), function (index, name) {
            var value = options[name];

            if (value == null || value == undefined) {
                value = defaults[name];
            }

            that.__events[name] = value;
        });

        for (var member in __jControl__.prototype) {
            if (this[member] == null) {
                this[member] = __jControl__.prototype[member];
            }
        };

        $element = $element || $('<' + this._tagName + '>');

        var id = this.getId();

        if (!$.string.isEmptyOrNull(id)) {
            $element.attr('id', id);
        }

        var data = options.data;

        if (data != null) {
            for (var item in data) {
                $element.attr('data-' + item, data[item]);
            }
        }

        this._setElement($element);

        this.init();
    }

    __jControl__.prototype = {
        init: function (sender, args) {
            this.render();
        },
        render: function (sender, args) {

        },
        add: function (control, $container) {
            if (control == null) {
                throw 'no se ha especificado el control.';
            }

            $container = $container || this.getElement();

            if ($container == null) {
                throw 'El control no se ha especificado el elemento.';
            }

            if (control.id == null || control.id.length == 0) {
                //Generar AutoID
            }

            $container.append(control.getElement());

            control._setParent(this);
        },
        remove: function (control) {
        },
        getParent: function () {
            return this.parent;
        },
        getId: function () {
            return this._getSettings('id');
        },
        getClass: function () {
            return this._getSettings('class');
        },
        getElement: function () {
            return this.__$element;
        },
        _getSettings: function (value) {
            return !value ? this.__settings : this.__settings[value];
        },
        _getEvents: function (value) {
            return this.__events[value];
        },
        _setParent: function (control) {
            this.parent = control;
        },
        _setElement: function (value) {
            this.__$element = value;
        },
        _setSettings: function (name, value) {
            this.__settings[name] = value;
        },
        _tagName: null,
        __$element: null,
        __settings: {},
        __events: {}
    }
    __jControl__.defaults = {
        id: '',
        parent: null,
        $element: null
    }
    __jControl__.descriptor = {
        settings: ['id', 'class'],
        events: []
    }

    var __jControlButton__ = function (options, $element) {
        __jControl__.call(this, options, $element);

        var $button = this.getElement();

        if (this._getEvents('click') != null) {
            $button.addEvent(this, 'click', this.click);
        }

        var text = this.getText() || this.getHtml();

        if (text != null && text.length > 0) {
            $button.text(text);
        }

        var cssClass = this.getClass() || 'btn-primary';

        $button
			.addClass('btn')
			.attr('type', 'button');

        if (cssClass != null && cssClass.length > 0) {
            $button.addClass(cssClass);
        }
    }
    __jControlButton__.prototype = {
        _tagName: 'button',
        constructor: __jControlButton__,
        click: function (sender, args) {
            this._getEvents('click').call(this.getParent(), sender, args);
        },
        getText: function () {
            return this._getSettings('text');
        },
        getHtml: function () {
            return this._getSettings('html');
        }
    }
    __jControlButton__.defaults = {
        text: '',
        html: '',
        click: null,
        dblClick: null
    }
    __jControlButton__.descriptor = {
        settings: ['text', 'html'],
        events: ['click', 'dblclick']
    }

    var __jControlWindow__ = function (options, $element) {
        __jControl__.call(this, options, $element);

        var id = this.getId();

        if ($.string.isEmptyOrNull(id)) {
            id = string.format('window-{0}', $.guid);
        }

        this.getElement().attr('id', id);
        this._setSettings('id', id);
    }

    __jControlWindow__.prototype = {
        _tagName: 'div',
        constructor: __jControlWindow__,
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
            return this._createElement(__jControlWindow__.dialogClass, $container);
        },
        _createContentElement: function ($container) {
            return this._createElement(__jControlWindow__.contentClass, $container);
        },
        _createBodyElement: function ($container) {
            return this._createElement(__jControlWindow__.bodyClass, $container);
        },
        _createHeaderElement: function ($container) {
            var $header = this._createElement(__jControlWindow__.headerClass, $container);

            this._createHeaderControlboxElement($header);
            this._createHeaderTitleElement($header);

            return $header;
        },
        _createHeaderTitleElement: function ($container) {
            var title = this.getTitle();

            if (!$.string.isEmptyOrNull(title)) {
                this._createElement(__jControlWindow__.titleClass, $container).text(title);
            }
        },
        _createHeaderControlboxElement: function ($container) {
            var $controlBox,
				buttons;

            buttons = __jControlWindow__.controlBox;

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

            $footer = this._createElement(__jControlWindow__.footerClass, $container);

            if (buttons != null) {
                that = this;

                $.each(buttons, function (index, button) {
                    if ($.string.isEmptyOrNull(button.text) && $.string.isEmptyOrNull(button.html)) {
                        button.text = index;
                    }

                    var $button = new $.control.button(button);

                    that.add($button, $footer);
                });
            }

            return $footer;
        },
        _resizableStart: function (args) {
            this._maximized = args.maximize;

            var maximize = (this._maximized == null || this._maximized == false),
							$modal = this.getElement(),
							$dialog = $('> .modal-dialog', $modal),
							$content = $('> .modal-content', $dialog),
							$header = $('> .modal-header', $content),
							$controlbox = $('> .modal-controlbox', $header),
							$btnResize = $('#window-maximize', $controlbox),
							cssPrefix = 'fa fa-window-',
							oldClass,
							newClass;

            if (maximize) {
                oldClass = 'restore';
                newClass = 'maximize';
            } else {
                oldClass = 'maximize';
                newClass = 'restore';
            }

            $btnResize
				.removeClass(cssPrefix + oldClass)
				.addClass(cssPrefix + newClass);

            var t = setTimeout(function () {
                $('div.dataTables_scrollHeadInner')
							.css('width', '')
							.find('> table')
								.css('width', '');
                clearTimeout(t);
            }, 100);

        },
        _resizableResize: function (args) {
            var $modal = args.$modal,
				$header = args.$header,
				$body = args.$body,
				$footer = args.$footer,
				headerHeight = ($header != null && $header.length > 0 ? $header.outerHeight() : 0),
				footerHeight = ($footer != null && $footer.length > 0 ? $footer.outerHeight() : 0),
				bodyHeight = ($modal.outerHeight() - (headerHeight + footerHeight))

            $body.css('height', bodyHeight);
        },
        _resize: function () {
            var maximize = (this._maximized == null || this._maximized == false),
				$modal = this.getElement(),
				$dialog = $('> .modal-dialog', $modal),
				$content = $('> .modal-content', $dialog),
				$header = $('> .modal-header', $content),
				$body = $('> .modal-body', $content),
				$footer = $('> .modal-footer', $content),
				position = { left: 0, top: 0 },
				size = { width: '100%', height: '100%' },
				that = this;

            if (maximize) {
                this._oldPosition = $modal.position();
                this._oldSize = { height: $modal.outerHeight(), width: $modal.outerWidth() };
            } else {
                position = this._oldPosition;
                size = this._oldSize;
            }

            $modal.animate({
                left: position.left,
                top: position.top,
                width: size.width,
                height: size.height
            }, {
                duration: 100,
                start: function () { that._resizableStart.call(that, { maximize: maximize }); },
                progress: function () { that._resizableResize.call(that, { $modal: $modal, $header: $header, $body: $body, $footer: $footer }); }
            });
        },
        _focus: function () {
            if (__jControlWindow__.overlay.length > 0) {
                $('.modal-backdrop').css('z-index', $.app.getMaxZIndex() + 1);
            }

            var $modal = this.getElement(),
				zIndex = $modal.css('z-index'),
				maxZIndex = $.app.getMaxZIndex();

            if (zIndex != maxZIndex) {
                $modal.css('z-index', maxZIndex + 1);
            }
        },
        _initializePosition: function () {
            var $modal = this.getElement();

            $modal
			.offset({
			    top: (($(window).height() - $modal.outerHeight()) / 2),
			    left: (($(window).width() - $modal.outerWidth()) / 2)
			});
        },
        _showModal: function (args) {
            this._focus();

            if (this._getAutoSize() == true) {
                var $modal = args.$modal,
								$header = args.$header,
								$body = args.$body,
								$footer = args.$footer;

                $body.css('height', '');

                var headerHeight = ($header != null && $header.length > 0 ? $header.outerHeight() : 0),
					footerHeight = ($footer != null && $footer.length > 0 ? $footer.outerHeight() : 0),
					bodyHeight = $body.outerHeight();

                $modal.css('height', headerHeight + bodyHeight + footerHeight);
            }

            this._resizableResize(args);
            this._initializePosition();
        },
        open: function () {
            var currentWidth = this.getWidth(),
				currentHeight = this.getHeight(),
            url = $.app.getPageUrl({ url: '~/home/popup' });

            var maxWidth = ($(window).innerWidth() > currentWidth ? this.getWidth() : $(window).innerWidth()),
				maxHeight = ($(window).innerHeight() > currentHeight ? this.getHeight() : $(window).innerHeight());
            var wu = null;
            if (this.getModal() == true && !!window.showModalDialog) {
                wu = window.showModalDialog(url, this._getSettings(), string.format('dialogheight={0}px;dialogwidth={1}px;resizable={2};scroll={3}', maxHeight, maxWidth, 'false', 0));
            } else {

                window.dialogArguments = this._getSettings();

                var x = screen.width / 2 - (maxWidth / 2);
                var y = screen.height / 2 - (maxHeight / 2);
                wu = window.open(url, '_blank', string.format('height={0}px,width={1}px,resizable={2},scrollbars={3},left={4},top={5}', maxHeight, maxWidth, 1, 1, x, y));
            }
            return wu;
        },
        close: function () {

            this.getElement().remove();

            var count = __jControlWindow__.overlay.length;

            __jControlWindow__.overlay.splice(count - 1, 1);

            var $backdrop = $('> .modal-backdrop', document.body);

            if (__jControlWindow__.overlay.length == 0) {
                $backdrop.remove();
            }

            if (__jControlWindow__.overlay.length > 0) {
                var x = __jControlWindow__.overlay[__jControlWindow__.overlay.length - 1].getElement().css('z-index');

                $backdrop.css('z-index', x - 1);
            }

            var $modals = __jControlWindow__.modals;

            if (!$.array.isEmptyOrNull($modals)) {
                for (var i = 0, j = $modals.length; i < j; i++) {

                    var $modal = $modals[i];
                    if ($modal)
                        if ($modal.getId() == this.getId()) {
                            __jControlWindow__.modals.splice(i, 1);
                        }
                }
            }
        },
        maximize: function () {
            this._resize();
        },
        minimize: function () {
            var $toolbar = __jControlWindow__.toolbar;

            if ($toolbar.length == 1) {
                var $element = this.getElement();

                $element.hide();

                var $li = $('<li>'),
					$a = $('<a>');

                $a
					.addClass('glyphicon glyphicon-comment')
					.attr('title', this.getTitle());

                $a.addEvent(this, 'click', function (e) {
                    $element.show();
                    $li.remove();
                    this._focus();
                });

                $toolbar.append($li.append($a))
            }
        },
        login: function () {
            var $modal = this.getElement(),
				$dialog = this._createDialogElement($modal),
				$content = this._createContentElement($dialog),
				$header = this._createHeaderElement($content),
				$body = this._createBodyElement($content),
				$footer = this._createFooterElement($content),
				currentWidth = this.getWidth(),
				currentHeight = this.getHeight(),
                url = this.getUrl(),
				that = this;
            if (!$.isNumeric(currentWidth)) {
                currentWidth = currentWidth.replace('px', '');
            }
            if (!$.isNumeric(currentHeight)) {
                currentHeight = currentHeight.replace('px', '');
            }
            var maxWidth = ($(window).innerWidth() > currentWidth ? this.getWidth() : $(window).innerWidth()) / 2,
				maxHeight = ($(window).innerHeight() > currentHeight ? this.getHeight() : $(window).innerHeight()) / 2;
            $modal.addEvent(this, 'shown.bs.modal', function () {
                if (!$.string.isEmptyOrNull(url)) {
                    that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                    $content.addClass(__jControlWindow__.loadingClass);

                    $.ajax({
                        type: that.getType(),
                        url: url,
                        data: that.getData(),
                        async: true,
                        cache: false,
                        complete: function (jqXHR, textStatus) {
                            $content.removeClass(__jControlWindow__.loadingClass);
                            that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                            that.complete();
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
                        that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                        that.complete();
                        clearTimeout(t);
                    }, 1);
                }

                if (this.getModal() == true) {
                    __jControlWindow__.overlay.push(this);
                }
            });
            $modal
            	.css({
            	    width: maxWidth,
            	    height: maxHeight
            	});

            this._initializePosition();

            $modal
            	.addClass(__jControlWindow__.class)
            	.appendTo(document.body)
            	.addEvent(this, 'mousedown', function () {
            	    this._focus();
            	})
            	.modal({
            	    show: true,
            	    backdrop: (__jControlWindow__.overlay.length == 0 ? this.getModal() : false),
            	    keyboard: false
            	})
            	.draggable({
            	    handle: $header,
            	    containment: document.body
            	})
                .css('padding-right', '0px')
        },
        alert: function () {
            var $modal = this.getElement(),
				$dialog = this._createDialogElement($modal),
				$content = this._createContentElement($dialog),
				$header = this._createHeaderElement($content),
				$body = this._createBodyElement($content),
				$footer = this._createFooterElement($content),
				currentWidth = this.getWidth(),
				currentHeight = this.getHeight(),
				that = this;
            if (!$.isNumeric(currentWidth)) {
                currentWidth = currentWidth.replace('px', '');
            }
            if (!$.isNumeric(currentHeight)) {
                currentHeight = currentHeight.replace('px', '');
            }
            var maxWidth = ($(window).innerWidth() > currentWidth ? this.getWidth() : $(window).innerWidth()) / 2,
				maxHeight = ($(window).innerHeight() > currentHeight ? this.getHeight() : $(window).innerHeight()) / 2;
            $modal.addEvent(this, 'shown.bs.modal', function () {

                $body.html(this.getText());

                var t = setTimeout(function () {
                    that._showModal({ $modal: $modal, $header: $header, $body: $body, $footer: $footer });
                    that.complete();
                    clearTimeout(t);
                }, 1);


                if (this.getModal() == true) {
                    __jControlWindow__.overlay.push(this);
                }
            });
            $modal
            	.css({
            	    width: maxWidth,
            	    height: maxHeight
            	});

            this._initializePosition();

            $modal
            	.addClass(__jControlWindow__.class)
            	.appendTo(document.body)
            	.addEvent(this, 'mousedown', function () {
            	    that._focus();
            	})
            	.modal({
            	    show: true,
            	    backdrop: (__jControlWindow__.overlay.length == 0 ? this.getModal() : false),
            	    keyboard: false
            	})
            	.draggable({
            	    handle: $header,
            	    containment: document.body
            	})
                .css('padding-right', '0px')
        },



        complete: function () {
            if (this._getEvents('complete') != null) {
                this._getEvents('complete').call(this);
            }
        },
        restore: function () {
            this._resize();
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
    }

    __jControlWindow__.controlBox = {
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
    __jControlWindow__.contentClass = 'modal-content';
    __jControlWindow__.dialogClass = 'modal-dialog';
    __jControlWindow__.headerClass = 'modal-header claro-red';
    __jControlWindow__.titleClass = 'modal-title';
    __jControlWindow__.bodyClass = 'modal-body';
    __jControlWindow__.footerClass = 'modal-footer';
    __jControlWindow__.loadingClass = 'modal-loading';
    __jControlWindow__.class = 'modal';
    __jControlWindow__.toolbar = null;
    __jControlWindow__.overlay = [];
    __jControlWindow__.modals = [];
    __jControlWindow__.defaults = {
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
    __jControlWindow__.descriptor = {
        settings: ['autoSize', 'async', 'data', 'target', 'url', 'data', 'text', 'title', 'type', 'modal', 'width', 'height', 'controlBox', 'maximizeBox', 'minimizeBox', 'buttons'],
        events: ['complete']
    }

    __jControlWindow__.close = function () {
        var ele = $('div.modal.in:last'),
		id = ele.attr("id");
        var $modals = $.window.modals;

        if (!$.array.isEmptyOrNull($modals)) {
            for (var i = 0, j = $modals.length; i < j; i++) {
                var $modal = $modals[i];

                if ($modal.getId() == id) {
                    $modal.close();
                }
            }
        }
    }
    __jControlWindow__.open = function (options) {
        var w = null;

        if (w == null) {
            w = new __jControlWindow__(options);

            __jControlWindow__.modals.push(w);

            w.open();
        }

        return w;
    }

    __jControlWindow__.alert = function (options) {
        var w = null;

        if (w == null) {
            w = new __jControlWindow__(options);
            __jControlWindow__.modals.push(w);

            w.alert();
        }

        return w;
    }

    __jControlWindow__.login = function (options) {
        var w = null;

        if (w == null) {
            w = new __jControlWindow__(options);
            __jControlWindow__.modals.push(w);

            w.login();
        }

        return w;
    }

    $.extend($, {
        app: {
            getMaxZIndex: function () {
                var maxZIndex = 0,
				currentZIndex = 0;

                $('*').each(function () {
                    currentZIndex = $(this).css('z-index');

                    if ($.isNumeric(currentZIndex) && currentZIndex > maxZIndex) {
                        maxZIndex = parseInt(currentZIndex);
                    };
                })

                return maxZIndex;

            },
            NotMaximed: function () {
                $(document).keydown(function (event) {
                    if (event.keyCode == 122 ||
                                    (event.altKey && event.keyCode == 13)) {

                        event.preventDefault();
                    }
                });

            },
            ajax: function (settings) {
                var beforeSend = settings.beforeSend,
					success = settings.success,
					complete = settings.complete;

                settings.beforeSend = function () {
                    if (settings.container != null) {
                        settings.container.html('<div class="ajax-loading"></div>');
                    }

                    if (beforeSend != null) {
                        beforeSend.call(this, arguments[0], arguments[1]);
                    }
                }

                settings.success = function (response) {
                    if (settings.container != null) {
                        settings.container.html(response);
                    }
                    if (success != null) {
                        success.call(this, arguments[0], arguments[1], arguments[2]);
                    }
                }

                settings.complete = function (response) {
                    if (complete != null) {
                        complete.call(this, arguments[0], arguments[1]);
                    }
                }

                settings.url = $.app.getPageUrl({ url: settings.url });
                return $.ajax(settings);
            },
            date: {
                format: function (date, format) {
                    return $.formatDate(date, format);
                },
                addMonth: function (date, value, settings) {
                    var currentMonth = date.getMonth();
                    var newDate = date.setMonth(currentMonth + value);

                    if (settings != null) {
                        newDate = $.formatDate(newDate, settings);
                    }

                    return newDate;
                },
                limitedDate: function (date) {
                    var fDateToDay = new Date();
                    fDateToDay = this.format(fDateToDay, { format: 'dd/mm/yy' });
                    return date.valueOf() > fDateToDay.valueOf() ? 'disabled' : '';
                }
            },
            ddmmyyyyhhmisstt2Time: function (date) {
                var dateParts = date.split(' '),
					date_ddmmyyyy = dateParts[0].split('/'),
					date_hhmmss = dateParts[1].split(':');

                return (new Date(date_ddmmyyyy[2], date_ddmmyyyy[1], date_ddmmyyyy[0], date_hhmmss[0], date_hhmmss[1], (date_hhmmss[2] + (dateParts[2] != null && dateParts[2].toUpperCase() === 'P.M.' ? 12 : 0)))).getTime();
            },
            __$applicationUrl$__: null,
            setApplicationUrl: function (value) {
                $.app.__$applicationUrl$__ = value;
            },
            getApplicationUrl: function () {
                return $.app.__$applicationUrl$__;
            },
            getPageUrl: function (options) {
                var url = options.url || '';

                if (url.length > 1) {
                    var firstCharacter = url.substring(0, 1).toUpperCase();

                    switch (firstCharacter) {
                        case '~':
                            url = string.format('{0}{1}', $.app.getApplicationUrl(), options.url.substring(2, url.length));
                            break;
                        case '/':
                        case '\\':
                            url = string.format('{0}/{1}', window.location.origin, options.url.substring(1, url.length));
                            break;
                        default:
                            url = string.format('{0}/{1}', window.location.href, options.url);
                            break;
                    }

                }

                return url
            },
            const: {
                days: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                today: 'Hoy',
                loadingRecords: 'Cargando...',
                processing: 'Procesando...',
                zeroRecords: 'No se encontraron registros coincidentes.',
                emptyTable: 'No hay datos disponibles en la tabla.',
                formatDate: 'd/m/Y',
                actionSuccessClassName: 'action-exito',
                actionErrorClassName: 'action-error',
                messageErrorLoading: 'Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo<br> por favor intentelo otra vez en unos minutos  ...<br /><br /><label class="control-label">Id de Transacción:</label>'
            },
            error: function (options) {
                var u = { c: options.id, r: options.message, f: options.click };
                var d = $('<div align="center"></div>'), e;
                u.f != null && (e = $('<button type="button" class="btn claro-btn-info btn-sm" >Volver a cargar</button>').on("click", u.f), d.html(e));
                return $('#' + u.c).html('<br/><br/><div align="center">Estimado usuario, en este momento estamos presentando intermitencia en el aplicativo  <br> por favor intentelo otra vez en unos minutos  ... <br><label class="control-label">Id de Transacción : </label>' + ' <span> ' + $($(u.r.responseText)[1]).text() + '</span> </div><br />').append(d);
            },

        },
        window: __jControlWindow__,
        control: {
            control: __jControl__,
            button: __jControlButton__
        },
        array: {
            isEmptyOrNull: function (value) {
                return (value == null || this.isEmpty(value));
            },
            isEmpty: function (value) {
                return value.length === 0;
            }
        },
        string: {
            isEmptyOrNull: function (value) {
                return (typeof value == 'string' && !value.trim()) || typeof value == 'undefined' || value === null;
            }
        },
        formatDate: function (date, settings) {

            var formatDateTimeDefault = {

                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
			  'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre',
				  'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul',
								  'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves',
						   'Viernes', 'Sabado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                ampmNames: ['a.m.', 'p.m.'],
                getSuffix: function (num) {
                    if (num > 3 && num < 21) {
                        return 'th';
                    }

                    switch (num % 10) {
                        case 1: return "st";
                        case 2: return "nd";
                        case 3: return "rd";
                        default: return "th";
                    }
                },
                attribute: 'data-datetime',
                formatAttribute: 'data-dateformat',
                format: 'dd/mm/yy gg:ii:ss a'

            };

            if (!date)
                return '';

            if (typeof date != Date) {
                if ((/^\//).test(date)) {
                    var dateString = date.replace(/\//g, '');

                    date = new Date(dateString);
                }
                else {
                    date = new Date(date);
                }
            }

            settings = $.extend({}, formatDateTimeDefault, settings);

            var format = settings.format;
            var ticksTo1970 = (((1970 - 1) * 365 + Math.floor(1970 / 4)
								- Math.floor(1970 / 100)
								+ Math.floor(1970 / 400)) * 24 * 60 * 60 * 10000000);

            var output = '';
            var literal = false;
            var iFormat = 0;

            // Check whether a format character is doubled
            var lookAhead = function (match) {
                var matches = (iFormat + 1 < format.length
							   && format.charAt(iFormat + 1) == match);
                if (matches) {
                    iFormat++;
                }
                return matches;
            };

            // Format a number, with leading zero if necessary
            var formatNumber = function (match, value, len) {
                var num = '' + value;
                if (lookAhead(match)) {
                    while (num.length < len) {
                        num = '0' + num;
                    }
                }
                return num;
            };

            // Format a name, short or long as requested
            var formatName = function (match, value, shortNames, longNames) {
                return (lookAhead(match) ? longNames[value] : shortNames[value]);
            };

            // Get the value for the supplied unit, e.g. year for y
            var getUnitValue = function (unit) {
                switch (unit) {
                    case 'y': return date.getFullYear();
                    case 'm': return date.getMonth() + 1;
                    case 'd': return date.getDate();
                    case 'g': return date.getHours() % 12 || 12;
                    case 'h': return date.getHours();
                    case 'i': return date.getMinutes();
                    case 's': return date.getSeconds();
                    case 'u': return date.getMilliseconds();
                    default: return '';
                }
            };

            for (iFormat = 0; iFormat < format.length; iFormat++) {
                if (literal) {
                    if (format.charAt(iFormat) == "'" && !lookAhead("'")) {
                        literal = false;
                    }
                    else {
                        output += format.charAt(iFormat);
                    }
                } else {
                    switch (format.charAt(iFormat)) {
                        case 'a':
                            output += date.getHours() < 12
								? settings.ampmNames[0]
								: settings.ampmNames[1];
                            break;
                        case 'd':
                            output += formatNumber('d', date.getDate(), 2);
                            break;
                        case 'S':
                            var v = getUnitValue(iFormat && format.charAt(iFormat - 1));
                            output += (v && (settings.getSuffix || $.noop)(v)) || '';
                            break;
                        case 'D':
                            output += formatName('D',
												 date.getDay(),
												 settings.dayNamesShort,
												 settings.dayNames);
                            break;
                        case 'o':
                            var end = new Date(date.getFullYear(),
											   date.getMonth(),
											   date.getDate()).getTime();
                            var start = new Date(date.getFullYear(), 0, 0).getTime();
                            output += formatNumber(
								'o', Math.round((end - start) / 86400000), 3);
                            break;
                        case 'g':
                            output += formatNumber('g', date.getHours() % 12 || 12, 2);
                            break;
                        case 'h':
                            output += formatNumber('h', date.getHours(), 2);
                            break;
                        case 'u':
                            output += formatNumber('u', date.getMilliseconds(), 3);
                            break;
                        case 'i':
                            output += formatNumber('i', date.getMinutes(), 2);
                            break;
                        case 'm':
                            output += formatNumber('m', date.getMonth() + 1, 2);
                            break;
                        case 'M':
                            output += formatName('M',
												 date.getMonth(),
												 settings.monthNamesShort,
												 settings.monthNames);
                            break;
                        case 's':
                            output += formatNumber('s', date.getSeconds(), 2);
                            break;
                        case 'y':
                            output += (lookAhead('y')
									   ? date.getFullYear()
									   : (date.getYear() % 100 < 10 ? '0' : '')
									   + date.getYear() % 100);
                            break;
                        case '@':
                            output += date.getTime();
                            break;
                        case '!':
                            output += date.getTime() * 10000 + ticksTo1970;
                            break;
                        case "'":
                            if (lookAhead("'")) {
                                output += "'";
                            } else {
                                literal = true;
                            }
                            break;
                        default:
                            output += format.charAt(iFormat);
                    }
                }
            }

            return output;
        },
        onlyNumbersPoint: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^0-9.]/g)) {
                    this.value = this.value.replace(/[^0-9.]/g, '');
                }
            });
        },
        onlyNumbers: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9]/g, '');
                }
            });
        },
        onlyNumbersLetters: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9]/g, '');
                }
            });
        },
        onlyNumbersLettersLine: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-Z0-9-]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z0-9-]/g, '');
                }
            });
        },
        onlyLetters: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                if (this.value.match(/[^a-zA-ZñÑ]/g)) {
                    this.value = this.value.replace(/[^a-zA-Z]/g, '');
                }
            });
        },
        onlyLettersSpaces: function myfunction(input) {
            input.off('keyup');
            input.keyup(function () {
                this.value = this.value.replace(/(\s{2,})|[^a-zA-ZÑñ]/g, ' ');
                this.value = this.value.replace(/^\s*/, '');
            });
        },
        rowSelection: function (options) {
            var _rowSelectionDefault = {
                id: null,
                link: null
            };
            options = $.extend({}, _rowSelectionDefault, options);

            $((options.link == null ? '#' + options.id : '.' + options.link) + ' tbody tr').on('click', function (event) {
                if (event.target.type != 'radio')
                    $(this).find('td input[type=radio]').prop('checked', true).trigger('change');

                $('.' + options.link + ' tbody tr').removeAttr('style');
                $(this).css({ 'background': '#E3EEF7' }).siblings("tr").removeAttr('style');
            });
        }
    });

    $.fn.extend({
        enable: function (value) {
            return this.each(function () {
                this.disabled = (value != undefined ? !value : false);
            });
        },
        populateDropDown: function (options) {
            var settings = $.extend({}, $.fn.dropDown_defaults, options);
            this.html('');
            return this.each(function () {
                var dataSource = settings.dataSource;
                var $this = $(this);
                if (dataSource != null && dataSource.length > 0) {
                    $.each(dataSource, function (index, item) {
                        var value = item[settings.fieldValue];
                        var option;
                        option = $("<option>", {
                            value: value,
                            text: item[settings.fieldText],
                            selected: (typeof settings.selectedValue == "object" ? ($.inArray(value, settings.selectedValue) != -1) : (value == settings.selectedValue))
                        });
                        if (settings.saveData)
                            option.data("response", item);
                        $this.append(option);
                    });

                    if (options.multiple != 'multiple') {
                        if ($this.selectpicker != null) {

                            $this.selectpicker(settings);
                        }
                    }
                }
            });
        },

        hideMessage: function () {
            return this.each(function () {
                var $that = $(this);

                $that.hide();

                var className = $that.attr('class');

                if (className) {
                    $that.removeClass(className);
                }

                $that.empty();
            });
        },
        showMessage: function (type, message) {
            var className;

            switch (type) {
                case 'error':
                    className = $.app.const.actionErrorClassName;
                    break;
                case 'success':
                    className = $.app.const.actionSuccessClassName;
                    break;
                default:
                    className = '';
                    break;
            }

            return this.each(function () {
                $(this)
					.addClass(className)
					.html(message)
					.show();
            })
        },
        showMessageErrorLoading: function (objectError) {
            var className;
            className = $.app.const.actionErrorClassName;

            var div = $('<div>',
						{
						    class: 'containerError',
						    align: 'center'
						});

            if (typeof (objectError) === "object") {

                objectError.message += '<span>' + objectError.session + '</span><br /><br />';
                objectError.message += '<button id="' + objectError.buttonID + '" type="button" class="btn claro-btn-info btn-sm" data-dismiss="modal">Volver a cargar</button>';

                div.html(objectError.message);
                return this.each(function () {
                    $(this)
						.addClass(className)
						.html(div)
						.show();
                    $("#" + objectError.buttonID).addEvent(objectError.that, 'click', objectError.funct);
                    objectError.message = "";
                })
            } else {
                objectError += '<span>' + Session.IDSESSION + '</span><br /><br />';
                div.html(objectError);
                return this.each(function () {
                    $(this)
						.addClass(className)
						.html(div)
						.show();
                })
            }

        },
        addEvent: function (sender, name, event) {
            var fn = this;
            return this.each(function () {
                var that = $(this);
                function facadeEvent(e) {
                    var typeoption2 = that.attr('typeoptions');
                    var args = {};

                    if (typeof typeoption2 != 'undefined' && (typeoption2 == '1' || typeoption2 == '2')) {
                        args = {
                            sender: sender,
                            event: e,
                            control: that,
                            code: that.attr('profile'),
                            fn_response: event,
                            fn_validate: fn.ValidateMenu

                        };

                        $.functionValidateMenu.call(sender, that, args);
                        return;

                    } else {
                        args = {
                            event: e,
                            control: that
                        };
                        event.call(sender, that, args);
                    }
                }


                var atributo = that.attr('profile');
                var typeoption = that.attr('typeoptions');
                if (typeoption == undefined || typeoption == '3') {
                    if (atributo == undefined) {
                        that.unbind(name);
                        that.bind(name, facadeEvent);
                    } else {
                        var stroptionPermissions = Session.USERACCESS.optionPermissions;

                        var arr = atributo.split(",");
                        if (arr.length > 2) {
                            var i = Session.ORIGINTYPE == 'IFI' ? 2 : Session.DATACUSTOMER.Application == 'POSTPAID' ? 0 : Session.DATACUSTOMER.Application == 'HFC' ? 1 : Session.DATACUSTOMER.Application == 'LTE' ? 1 : Session.DATACUSTOMER.Application == 'FTTH' ? 1 : 0;
                        } else {
                            var i = Session.ORIGINTYPE == 'IFI' ? 1 : 0;
                        }

                        if (stroptionPermissions.indexOf(arr[i]) < 0) {
                            that.hide();
                        } else {
                            that.unbind(name);
                            that.bind(name, facadeEvent);
                        }

                    }
                }
                else if (typeoption == '1' || typeoption == '2') {
                    that.unbind(name);
                    that.bind(name, facadeEvent);

                }
            })
        }
    });

    $.fn.dropDown_defaults = {
        fieldValue: 'Id',
        fieldText: 'Description',
        title: '',
        selectedValue: null,
        dataSource: null,
    }

    // This will help DataTables magic detect the "ddmmyyyyhhmisstt" format; Unshift
    // so that it's the first data type (so it takes priority over existing)
    jQuery.fn.dataTableExt.aTypes.unshift(
		function (sData) {
		    if (/^([0-2]?\d|3[0-1])\/([0-2]?\d|3[0-1])\/\d{4}/i.test(sData)) {
		        return 'date-ddmmyyyyhhmisstt';
		    }
		    return null;
		}
	);

    // define the sorts
    jQuery.fn.dataTableExt.oSort['date-ddmmyyyyhhmisstt-asc'] = function (a, b) {
        var ordA = $.app.ddmmyyyyhhmisstt2Time(a),
			ordB = $.app.ddmmyyyyhhmisstt2Time(b);

        return (ordA < ordB) ? -1 : ((ordA > ordB) ? 1 : 0);
    };

    jQuery.fn.dataTableExt.oSort['date-ddmmyyyyhhmisstt-desc'] = function (a, b) {
        var ordA = $.app.ddmmyyyyhhmisstt2Time(a),
			ordB = $.app.ddmmyyyyhhmisstt2Time(b);

        return (ordA < ordB) ? 1 : ((ordA > ordB) ? -1 : 0);
    };

    jQuery.fn.dataTable.Api.register('sum()', function () {
        return this.flatten().reduce(function (a, b) {
            if (typeof a === 'string') {
                a = a.replace(/[^\d.-]/g, '') * 1;
            }
            if (typeof b === 'string') {
                b = b.replace(/[^\d.-]/g, '') * 1;
            }

            return a + b;
        }, 0);
    });

    $.fn.extend($, {
        ConvertFormatfrom24to12: function (data) {
            if (typeof data == 'undefined' || data == null || data.trim() == '')
            { return ''; } else {
                var fecha = data.split(' ');
                var t = data.split(' ')[1].split(':');
                var hor = data.split(' ')[1].split(':')[0];
                var s;
                hor = parseInt(hor);
                if (hor <= 12) {
                    s = 'a.m.';
                    if (hor < 10) { hor = '0' + hor; }
                } else {
                    hor = hor - 12;
                    if (hor < 10) { hor = '0' + hor; }
                    s = 'p.m.';
                }
                var fechafinal = fecha[0] + ' ' + hor + ':' + t[1] + ':' + t[2] + ' ' + s;
                return fechafinal;

            }

        },

        Ajuste: {
            ValidateRedirect: function (send, args, option) {

                var result = 0;
                var json = null;

                var est = false;
                if (typeof (option) != 'undefined' && option != null && option != "") {
                    est = true;
                } else {
                    json = $(send).data('json');
                }

                var js = null;
                $.app.ajax({
                    type: 'POST',
                    url: '~/Dashboard/PostPaidDataCollection/GetValuesConfig',
                    async: false,
                    data: { strIdSession: Session.IDSESSION, strOriginType: Session.ORIGINTYPE, strIsLTE: Session.DATASERVICE.IsLTE },
                    success: function (response) {
                        js = response;

                    }
                });

                if (typeof (js) != 'undefined' && js != null) {
                    if (js.FlagContigencia == '1') {
                        if (Session.USERACCESS.optionPermissions.indexOf(js.Option) != -1) {
                            Session.TELEFONO = (Session.TELEPHONE != null && (Session.ORIGINTYPE == 'POSTPAID' || Session.ORIGINTYPE == 'DTH' || Session.ORIGINTYPE == 'IFI') ? Session.TELEPHONE : ((Session.ORIGINTYPE == 'HFC' || Session.ORIGINTYPE == 'LTE') ? (Session.TELEF_REFERENCIA != '' ? Session.TELEF_REFERENCIA : Session.DATACUSTOMER.PhoneReference) : ""));
                            if (Session.ORIGINTYPE == 'PREPAID') Session.TELEFONO = Session.TELEPHONE;
                            if (!est) {

                                var fono = Session.TELEFONO;
                                if (fono == '') {
                                    modalAlert('Seleccione un teléfóno de la cuenta');
                                    return false;
                                }

                            }
                            if (json != null) {
                                Session.NUMDOCESTADOCUENTA = json.Data.Document;
                                Session.TIPODOCESTADOCUENTA = json.Data.TypeDoc;
                                Session.TIPODOCREFESTADOCUENTA = json.Data.TypeDocRef;
                            } else {
                                Session.NUMDOCESTADOCUENTA = "";
                                Session.TIPODOCESTADOCUENTA = "";
                                Session.TIPODOCREFESTADOCUENTA = "";
                            }

                            var app = "";
                            Session.TRANSACCION = "";
                            Session.TIPOAPP = "";
                            if (Session.ORIGINTYPE == 'POSTPAID' || Session.ORIGINTYPE == 'DTH' || Session.ORIGINTYPE == 'IFI') {
                                Session.TIPOAPP = "SIACPOST";
                                app = "POSTPAGO";
                                $.Ajuste.setSessionesAjuste();
                                if (Session.ORIGINTYPE == 'DTH') Session.TRANSACCION = js.Transaction;
                            } else if (Session.ORIGINTYPE == 'HFC' || Session.ORIGINTYPE == 'FTTH') {
                                $.Ajuste.setSessionesAjuste();
                                app = "HFC";
                                Session.TIPOAPP = "SIACHFC";
                                Session.TRANSACCION = js.Transaction;
                            } else if (Session.ORIGINTYPE == 'LTE') {
                                $.Ajuste.setSessionesAjuste();
                                app = "LTE";
                                Session.TIPOAPP = "SIACHFC";
                                Session.TRANSACCION = js.Transaction;
                            }


                            $.redirect.GetParamsData(js.Option, app);

                        } else {
                            modalAlert(js.MsjAlertNotPermission);
                        }
                    } else {
                        if (!est) {
                            modalAlert(js.MsjAlertSiacNoDisponible);
                        } else {
                            result = 1;
                        }
                    }
                }
                return result;

            },
            getValueSessionAjuste: function (NameSession) {
                var value = "";
                var typeservice = Session.TYPESERVICE;
                var codetypeservice = (typeservice.indexOf("PostPago - Telefonía Móvil") != -1 || typeservice.indexOf("PostPago - TFI") != -1 || typeservice.indexOf("PostPago - INTERNET INALAMBRICO") != -1 || typeservice.indexOf("PostPago - TPI") != -1 ? "1" : (typeservice.indexOf("PostPago - DTH") != -1 ? "2" : typeservice)); //bgngfh

                if (NameSession == "CONTRATOID") {
                    value = Session.DATACUSTOMER.ContractID;
                } else if (NameSession == "TRANSACCION") {
                    if (codetypeservice == "1") {
                        value = "TRANSACCION_AJUSTE_DA";
                    } else if (codetypeservice == "2") {
                        value = "TRANSACCION_DTH_AJUSTE_DA";
                    }
                } else if (NameSession == "NOMBRES") {
                    value = Session.DATACUSTOMER.Name;
                } else if (NameSession == "APELLIDOS") {
                    value = Session.DATACUSTOMER.LastName;
                } else if (NameSession == "CUSTOMER_ID") {
                    value = Session.DATACUSTOMER.CustomerID;
                } else if (NameSession == "CUSTOMERID") {
                    value = Session.DATACUSTOMER.CustomerID;
                } else if (NameSession == "CALLE_FAC") {
                    value = Session.DATACUSTOMER.InvoiceAddress;
                } else if (NameSession == "CICLO_FACTURACION") {
                    value = (Session.DATACUSTOMER.BillingCycle != "" ? Session.DATACUSTOMER.BillingCycle : Session.DATACUSTOMER.objPostDataAccount.BillingCycle);
                } else if (NameSession == "CUENTA") {
                    value = Session.DATACUSTOMER.Account;
                } else if (NameSession == "DEPARTAMENTO") {
                    value = Session.DATACUSTOMER.Departament;
                } else if (NameSession == "DISTRITO") {
                    value = Session.DATACUSTOMER.District;
                } else if (NameSession == "PROVINCIA") {
                    value = Session.DATACUSTOMER.Province;
                } else if (NameSession == "DEPARTEMENTO_FAC") {
                    value = Session.DATACUSTOMER.InvoiceDepartament;
                } else if (NameSession == "DISTRITO_FAC") {
                    value = Session.DATACUSTOMER.InvoiceDistrict;
                } else if (NameSession == "DNI_RUC") {
                    value = Session.DATACUSTOMER.DNIRUC;
                } else if (NameSession == "MODALIDAD") {
                    value = Session.DATACUSTOMER.Modality;
                } else if (NameSession == "NOMBRE_COMPLETO") {
                    value = Session.DATACUSTOMER.FullName;
                } else if (NameSession == "NRO_DOC") {
                    value = Session.DATACUSTOMER.DocumentNumber;
                } else if (NameSession == "OBJID_CONTACTO") {
                    value = Session.DATACUSTOMER.ContactCode;
                } else if (NameSession == "PROVINCIA_FAC") {
                    value = Session.DATACUSTOMER.InvoiceProvince;
                } else if (NameSession == "RAZON_SOCIAL") {
                    value = Session.DATACUSTOMER.BusinessName;
                } else if (NameSession == "TIPO_CLIENTE") {
                    value = Session.DATACUSTOMER.CustomerType;
                } else if (NameSession == "TIPO_DOC") {
                    value = Session.DATACUSTOMER.DocumentType;
                } else if (NameSession == "DIRECCION_DESPACHO") {
                    value = Session.DATACUSTOMER.OfficeAddress;
                } else if (NameSession == "REPRESENTANTE_LEGAL") {
                    value = Session.DATACUSTOMER.LegalAgent;
                } else if (NameSession == "FECACTIVACION") {
                    value = Session.DATASERVICE.ActivationDate;
                } else if (NameSession == "FECHADESACTIVACION") {
                    value = Session.DATASERVICE.DeactivationDate;
                } else if (NameSession == "TIPOSERVICIO") {
                    value = codetypeservice;
                } else if (NameSession == "ACTDESBOLSA") {
                    value = "";
                } else if (NameSession == "EMAIL") {
                    value = Session.DATACUSTOMER.Email;
                } else if (NameSession == "FLAGTFI") {
                    value = Session.DATASERVICE.FlagTFI;
                } else if (NameSession == "S_NOMBRES") {
                    value = "";
                } else if (NameSession == "S_APELLIDOS") {
                    value = "";
                } else if (NameSession == "TELEF_REFERENCIA") {
                    value = Session.DATACUSTOMER.PhoneReference;
                } else if (NameSession == "TIPOAPP") {
                    value = "SIACPOST";
                } else if (NameSession == "CODIGO_PLANO_INST") {
                    value = Session.DATACUSTOMER.PlaneCodeInstallation;
                } else if (NameSession == "DOMICILIO") {
                    value = Session.DATACUSTOMER.Address;
                }
                return value;
            },
            setSessionesAjuste: function () {
                var that = this;
                var sessions = [];
                sessions = ["TIPOAPP", "CUSTOMERID", "TRANSACCION", "CONTRATOID", "NOMBRES", "APELLIDOS", "CUSTOMER_ID", "CALLE_FAC", "CICLO_FACTURACION",
                  "CUENTA", "DEPARTAMENTO", "DISTRITO", "PROVINCIA", "DEPARTEMENTO_FAC", "DISTRITO_FAC", "DNI_RUC", "MODALIDAD",
                  "NOMBRE_COMPLETO", "NRO_DOC", "OBJID_CONTACTO", "PROVINCIA_FAC", "RAZON_SOCIAL", "TIPO_CLIENTE", "TIPO_DOC",
                  "DIRECCION_DESPACHO", "REPRESENTANTE_LEGAL", "FECACTIVACION", "FECHADESACTIVACION", "TIPOSERVICIO", "ACTDESBOLSA",
                  "EMAIL", "FLAGTFI", "S_NOMBRES", "S_APELLIDOS", "TELEF_REFERENCIA", "CODIGO_PLANO_INST", "DOMICILIO", "TELEFONO"];

                $(sessions).each(function (index, value) {
                    var val = Session[value];
                    if (typeof (val) != 'undefined') {
                        Session[value] = "";
                    }
                });

                if (Session.ORIGINTYPE == 'POSTPAID' || Session.ORIGINTYPE == 'IFI') {
                    sessions = ["TIPOAPP", "TRANSACCION", "CUSTOMERID", "CONTRATOID", "NOMBRES", "APELLIDOS", "CUSTOMER_ID", "CALLE_FAC", "CICLO_FACTURACION",
                    "CUENTA", "DEPARTAMENTO", "DISTRITO", "PROVINCIA", "DEPARTEMENTO_FAC", "DISTRITO_FAC", "DNI_RUC", "MODALIDAD",
                    "NOMBRE_COMPLETO", "NRO_DOC", "OBJID_CONTACTO", "PROVINCIA_FAC", "RAZON_SOCIAL", "TIPO_CLIENTE", "TIPO_DOC",
                    "DIRECCION_DESPACHO", "REPRESENTANTE_LEGAL", "FECACTIVACION", "FECHADESACTIVACION", "TIPOSERVICIO", "ACTDESBOLSA",
                    "EMAIL", "FLAGTFI", "S_NOMBRES", "S_APELLIDOS", "TELEF_REFERENCIA"];
                } else {
                    sessions = ["TELEFONO", "CUSTOMERID", "NOMBRES", "APELLIDOS", "CODIGO_PLANO_INST", "CUSTOMER_ID", "CALLE_FAC", "CICLO_FACTURACION",
                    "CUENTA", "DEPARTAMENTO", "DISTRITO", "PROVINCIA", "DEPARTEMENTO_FAC", "DISTRITO_FAC", "DNI_RUC", "MODALIDAD",
                    "NOMBRE_COMPLETO", "NRO_DOC", "OBJID_CONTACTO", "PROVINCIA_FAC", "RAZON_SOCIAL", "TIPO_CLIENTE", "TIPO_DOC",
                    "DIRECCION_DESPACHO", "REPRESENTANTE_LEGAL", "FECACTIVACION", "CONTRATOID", "TIPOSERVICIO", "EMAIL", "FLAGTFI", "S_NOMBRES",
                    "S_APELLIDOS", "TELEF_REFERENCIA", "DOMICILIO"];
                }




                $(sessions).each(function (index, value) {
                    var val = Session[value];
                    if (typeof (val) != 'undefined') {
                        Session[value] = that.getValueSessionAjuste(value);
                    }
                });
            },
        },
        ValidateMenu: function (option, parentProduct) {
			
			
			
			if(Session.DATACUSTOMER.ProductType.toUpperCase().indexOf("PREPAGO")>=0){
				
			}
			else{			
				var tProducto=Session.DATASERVICE.TypeProduct.toUpperCase();
				if(tProducto!=null || tProducto!="" ){
					if(tProducto.indexOf("POSTPAGO")>=0 && (tProducto.indexOf("MOVIL")>=0||tProducto.indexOf("MÓVIL")>=0) ){
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "ASIS") {
            $.getCovid19(option);
                $.getBloqSES(option);
                $.getBloqueosClaro(option);
            } else {
                $.getBloqueosClaroTobe(option);
            }

            if (Session.COVID_19 == "1") {
                var keyValue = getValueConfig(['strMsgCovid19']).strMsgCovid19;
                modalAlert(keyValue);
                return false;
            }
            if (Session.BloqSES == "1") {
                var keyValue = getValueConfig(['strMsgBloqSES']).strMsgBloqSES;
                modalAlert(keyValue);
                return false;
            }
            if (Session.BloqClaro == "1") {
                modalAlert(Session.MensajeClaro);
                return false;
            }
				}
           }
}

            if (Session.ORIGINTYPE != '') {
                parentProduct = Session.ORIGINTYPE;
            }
            if ((typeof Session.DATACUSTOMER != 'undefined' || Session.DATACUSTOMER != null) && (Session.DATACUSTOMER.indicadorCambioNumero === "1")) {
                if (Session.DATASERVICE) {
                    modalAlert('La línea ha realizado un cambio de número, la nueva línea es : \n\n ' + Session.DATASERVICE.CellPhone);

                } else {
                    modalAlert('La línea ha realizado un cambio de número');
                }
                return false;
            }
            if (Session.DATASERVICE) {
                if (parentProduct == 'PREPAID' || parentProduct == 'PREPAGO' || parentProduct == 'OLO') {
                    if (Session.DATASERVICE.NumberCellphone === 0 || Session.DATACUSTOMER.CustomerCode == null || Session.DATACUSTOMER.CustomerCode == '') {

                        if (option === "SU_ACPRE_GMP") {
                            showAlert('Datos del cliente no cargados.\n\nPrimero debe realizar una búsqueda en la parte superior.', 'Mensaje');
                            return false;
                        }

                        if (option == 'SU_SIACA_DLIEQ' || option === "SU_SIACA_INON" || option == 'SU_SIACA_TRECL' || option == 'SU_SIACA_TSAR' || option == 'SU_SIACA_RBO' || option == 'SU_SIACA_RCEC' || option == 'SU_SIACA_CAMTIT' || option == 'SU_SIACA_CAMSIM' || option == 'SU_SIACA_VICI' || option == 'SU_SIACA_BLIEQ' || option == 'SU_SIACA_TRIAC') {
                            modalAlert('Datos del cliente no cargados.');
                            return false;
                        } else {
                            return true;
                        }
                    }
                    else {

                        var tempBlackList = Session.DATACUSTOMER.BlackList;

                        if ((option.substring(3, 8) == "SIACA") || (option.substring(3, 7) == "SIAC")) {
                            var EsTFI = Session.DATASERVICE.IsTFI;
                            var ListaOpcTFI = Session.USERACCESS.optionPermissionsMenu.Data.TFI;

                            if (EsTFI === true) {
                                if (ListaOpcTFI.indexOf(option) == -1) {
                                    showAlert('Opción no disponible para FIJO', 'Mensaje');
                                    return false;
                                }
                            }
                        }

                        if (tempBlackList == '0') {

                            if ((option.substring(3, 8) == "SIACA") || (option.substring(3, 7) == "SIAC")) {
                                if (option !== 'SU_SIAC_DSRE' && option !== 'SU_SIAC_CEDT' && option !== "SU_SIAC_CTE" && option !== "SU_SIACA_TRECL" && option !== "SU_SIACA_TSAR" && option !== "SU_SIACA_INON" && option !== "SU_SIACA_CDC" && option !== "SU_SIACA_GRQ" && option !== "SU_SIACA_LBR") {

                                    showAlert('No puede acceder a esta transaccion debido a que la linea se encuentra en estado DESCONOCIDO.', 'Mensaje');
                                    return false;
                                }
                            }

                            if (option === 'SU_ACP_DSV') {
                                showAlert('No puede acceder a esta transaccion debido a que la linea se encuentra en estado DESCONOCIDO.', 'Mensaje');
                                return false;
                            }


                        }
                        if (option === 'SU_SIACA_ZM') {

                            showAlert('Debe actualizar los datos del clientes', 'Mensaje');
                            return false;
                        }
                        else {

                            if (option === "SU_SIACA_ACDE" || option === "SU_SIACA_CONR" || option === "SU_SIACA_PAQU") {
                                if (tempBlackList == '0') {

                                    showAlert('No puede acceder a esta transacción debido a que la línea se encuentra en estado DESCONOCIDO', 'Mensaje');
                                    return false;
                                }
                            }
                        }
                        if (option == "SU_SIACA_CAMSIM") {
                            Session.CLASEID = "";
                            Session.SUBCLASEID = "";
                            Session.TIPOID = "";
                            if (Session.DATASERVICE.IsTFI) {
                                Session.SUBCLASEID = "140206";
                                Session.CLASEID = "1402";
                                Session.TIPOID = "14";

                            } else {
                                Session.SUBCLASEID = "109215";
                                Session.CLASEID = "1092";
                                Session.TIPOID = "10";
                            }

                            return true;

                        }

                        return true;
                    }
                } else if (parentProduct == 'POSTPAID' || parentProduct == 'POSTPAGO' || parentProduct === "DTH" || parentProduct === "HFC" || parentProduct === "LTE" || parentProduct == 'IFI' || parentProduct == 'FTTH') {
                    //VALIDATE LINK
                    var strTitleMessage = "Mensaje";
                    if (option.substr(0, 8) === 'SU_ACP_X') {
                        if (Session.DATASERVICE.CellPhone == '' && Session.DATACUSTOMER.PhoneReference == '') {

                            showAlert('Seleccione un teléfono de la cuenta', strTitleMessage);
                            return false;
                        }
                    }
                    if (option !== "SU_ACP_AIN" && option !== "SU_ACP_NMD" && option !== "SU_ACP_BLCO" && option !== "SU_ACP_RYC") {
                        if (Session.DATASERVICE.CellPhone === '') {

                            showAlert('Seleccione un teléfono de la cuenta', strTitleMessage);
                            return false;
                        }
                    }
                    var TypeService = Session.DATASERVICE.TypeService;

                    var arrTPI = Session.USERACCESS.optionPermissionsMenu.Data.TPI;
                    var arrInternet = Session.USERACCESS.optionPermissionsMenu.Data.Internet;
                    var arrFijoPOST = Session.USERACCESS.optionPermissionsMenu.Data.Fijo;
                    var arrHCTNumberOnlyTFI = Session.USERACCESS.optionPermissionsMenu.Data.HCTNumberOnlyTFI;
                    //mg13
                    if (typeof Session.DATACUSTOMER != "undefined" && Session.DATACUSTOMER != null) {
                        if (Session.DATACUSTOMER.ProductTypeText == "PostPago - INTERNET INALAMBRICO" || Session.DATASERVICE.TypeProduct == "PostPago - INTERNET INALAMBRICO") {
                            $.getParametersTfiPermissions();
                            if (typeof Session.CodePlanTariffTFI != "undefined" && Session.CodePlanTariffTFI != null) {


                                if (Session.CodePlanTariffTFI.split('|').indexOf(Session.DATASERVICE.CodePlanTariff) >= 0) {
                                    if (typeof Session.TFIPermissionsMenu != "undefined" && Session.TFIPermissionsMenu != null) {
                                        if (Session.TFIPermissionsMenu.split(',').indexOf(option) >= 0) {
                                            showAlert('Esta transacción no está habilitada para este tipo de Plan.', strTitleMessage);
                                            return false;
                                        } else {
                                            if (option == 'SU_ACP_XDA' || option == 'SU_ACP_TREAJ' || option == 'SU_IFI_RAJU' || option == 'SU_IFI_TREGA') {

                                                var fono = (Session.TELEPHONE != null ? Session.TELEPHONE : "");
                                                if (fono == '' || (fono == '' && Session.PSTRTELREFER == '')) {
                                                    modalAlert('Seleccione un teléfóno de la cuenta');
                                                    return false;
                                                }
                                                var rpt = $.Ajuste.ValidateRedirect(null, null, option);
                                                if (rpt == 1) {
                                                    return true;
                                                } else {
                                                    return false;
                                                }
                                            }


                                            return true;
                                        }
                                    }
                                }
                            }

                        }

                    }

                    if (!$.string.isEmptyOrNull(Session.DATASERVICE.TypeService)) {
                        if ((Session.TYPESERVICE.indexOf('TPI') >= 0 && arrTPI.indexOf(option) < 0) || (TypeService === 'INTERNET' && arrInternet.indexOf(option) < 0) || (TypeService.indexOf('FIJO POST') != -1 && arrFijoPOST.indexOf(option) < 0) || (TypeService === 'HCTNUMEROCPLANSOLOTFI' && arrHCTNumberOnlyTFI.indexOf(option) < 0)) {
                            showAlert('Esta transacción no está habilitada para este tipo de Plan.', strTitleMessage);

                            return false;
                        }
                    }
                    if (option == 'SU_ACP_INTERA' || option == 'SU_LTE_INTER' || option == 'SU_HFC_INTER') {
                        if (Session.FlagRedirect == true && Session.EST == true) {
                            modalAlert('Debe seleccionar un n&uacute;mero de la cuenta/servicio');
                            return false;
                        }
                        Session.PSTRTELREFER = "";
                        Session.PSTRCODCLIENTE = Session.DATACUSTOMER.ContactCode;

                        if (option == 'SU_ACP_INTERA' && (Session.DATACUSTOMER.ProductTypeText == "PostPago - TFI" || Session.DATASERVICE.TypeProduct == "PostPago - TFI")) Session.PSTRCODCLIENTE = "";

                    }

                    if (parentProduct == 'POSTPAID' || parentProduct == 'POSTPAGO') {

                        if (option == 'SU_ACP_IL') {

                            Session.CODCLIENTE = Session.DATACUSTOMER.Account;
                        }

                        if (option == 'SU_ACP_MIPLA' || option == 'SU_ACP_MCP') {
                            Session.DATACUSTOMER.objPostDataAccount.Niche = Session.DATACUSTOMER.objPostDataAccount.billingAccountId;
                        }

                    }

                    if (option == 'SU_HFC_XDA' || option == 'SU_ACP_XDA' || option == 'SU_LTE_RGAJ' || option == 'SU_ACP_TREAJ' || option == 'SU_LTE_TREAJ' || option == 'SU_ACP_DTRAJ' || option == 'SU_HFC_TREAJ' || option == 'SU_IFI_RAJU' || option == 'SU_IFI_TREGA') {
                        if (option == 'SU_ACP_XDA' || option == 'SU_ACP_TREAJ' || option == 'SU_IFI_RAJU' || option == 'SU_IFI_TREGA') {
                            var fono = (Session.TELEPHONE != null ? Session.TELEPHONE : "");
                            if (fono == '' || (fono == '' && Session.PSTRTELREFER == '')) {
                                modalAlert('Seleccione un teléfóno de la cuenta');
                                return false;
                            }
                        }
                        var rpt = $.Ajuste.ValidateRedirect(null, null, option);
                        if (rpt == 1) {
                            return true;
                        } else {
                            return false;
                        }

                    }


                    return true;
                }

            } else if (typeof Session.DATASERVICE == "undefined" || Session.DATASERVICE == null) {

                if (option == "SU_SIACA_TRECL" || option == "SU_SIACA_TSAR" || option == "SU_SIACA_INON" || option == "SU_SIACA_BLIEQ" || option == "SU_SIACA_DLIEQ") {
                    if (parentProduct == 'PREPAID') parentProduct = "PREPAGO";
                    $.redirect.GetParamsData(option, parentProduct);
                }

            }


        },
        getParametersTfiPermissions: function () {
            Session.CodePlanTariffTFI = "";
            Session.TFIPermissionsMenu = "";
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetParameterTerminalTPI',
                dataType: 'json',
                async: false,
                data: { strIdSession: Session.IDSESSION },
                success: function (response) {
                    Session.CodePlanTariffTFI = response.data.CodePlanTariffTFI;
                    Session.TFIPermissionsMenu = response.data.AbrvPermissions;
                },
                error: function (ex) {


                }
            });
        },
        getCovid19: function (opcion) {
            Session.COVID_19 = "";
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetCovid19',
                dataType: 'json',
                async: false,
                data: { strIdSession: Session.IDSESSION, strContratoID: Session.DATACUSTOMER.ContractID, strOpcion: opcion},
                success: function (response) {
                    if (response != null) {
                        Session.COVID_19 = response.data;
                    }
                },
                error: function (ex) {


                }
            });
        },
        getBloqSES: function (opcion) {
            Session.BloqSES = "";
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetBloqSES',
                dataType: 'json',
                async: false,
                data: { strIdSession: Session.IDSESSION, strContratoID: Session.DATACUSTOMER.ContractID, strOpcion: opcion},
                success: function (response) {
                    if (response != null) {
                        Session.BloqSES = response.data;
                    }
                },
                error: function (ex) {


                }
            });
        },
        getBloqueosClaro: function (opcion) {
            Session.BloqClaro = "";
            Session.MensajeClaro = "";
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetBloqueosClaro',
                dataType: 'json',
                async: false,
                data: { strIdSession: Session.IDSESSION, strContratoID: Session.DATACUSTOMER.ContractID, strOpcion: opcion},
                success: function (response) {
                    if (response != null) {
                        Session.BloqClaro = response.data;
                        Session.MensajeClaro = response.message;
                    }
                },
                error: function (ex) {


                }
            });
        },
        getBloqueosClaroTobe: function (opcion) {
            Session.BloqClaro = "";
            Session.MensajeClaro = "";
            Session.BloqSES = "";
            Session.COVID_19 = "";
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Postpaid/GetListaBloqueoDebloqueoTobe',
                dataType: 'json',
                async: false,
                data: { strIdSession: Session.IDSESSION, strCoidPub: Session.DATACUSTOMER.coIdPub, strLinea: Session.DATACUSTOMER.Telephone,strOpcion : opcion },
                success: function (response) {
                    if (response != null) {
                        Session.BloqClaro = response.dataBloq;
                        Session.MensajeClaro = response.strBloq;
                        Session.BloqSES = response.dataSES;
                        Session.COVID_19 = response.dataCovid;
                    }
                },
                error: function (ex) {
                    console.log("Intermitencia en getBloqueosClaroTobe");
                }
            });
        },
        DisabledToolbar: function (control, code, parentProduct) {
            if (parentProduct == 'PREPAID' || parentProduct == "PREPAGO") {
                if (code == "SU_SIACA_CLI" || code == "SU_SIACA_ZM") {
                    $(control).addClass("disabled");
                    $(control).css("pointer-events", "none");
                    $(control).css("color", "rgba(60, 50, 50, 0.4)");
                    return;
                }

            }

        },
        validateIsTfi: function (strNumberTelephone) {
            if (strNumberTelephone.charAt(0) === "9") {
                return "False";
            }
            return "True";
        },
        ValidateCode: function (code, parentProduct, title, Redirect) {
            title = ($.string.isEmptyOrNull(title) == false ? '' : title);
            if (title == '') {

                title = (parentProduct == 'PREPAID' ? 'PREPAGO' : (parentProduct == 'POSTPAID' ? 'POSTPAGO' : title));
            }
            if (Redirect) {
                if (Redirect == true) {
                    $.redirect.GetParamsData(code, parentProduct);
                    return;
                }
            }


            if ($.ValidateMenu(code, parentProduct)) {

                $.redirect.GetParamsData(code, parentProduct);

            }
        },
        aWindowOpen_click: function (direccion) {
            $.app.ajax({
                type: 'POST',
                url: '~/Dashboard/Home/Fraccionamiento',
                dataType: 'json',
                async: false,
                data: { strUrl: direccion, strUser: Session.USERACCESS.login },
                success: function (response) {
                    if (response != null) {
                        window.open(response.data);
                        window.moveTo(0, 0);
                        window.resizeTo(screen.availWidth, screen.availHeight);
                    } else {
                        showAlert("No se pudo redireccionar a la página solicitada. Intente nuevamente en breves." + data.responseText);
                    }
                },
                error: function (ex) {
                    showAlert("La página no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.");
                }
            });
        },
        functionValidateMenu: function (sender, e) {
          var keyValue = getValueConfig(['strOpcionesFraccion']).strOpcionesFraccion;
            var arrOpcion = keyValue.split("|");
            for (var i in arrOpcion) {
                if (this.code == arrOpcion[i]) {
                    $.aWindowOpen_click(this.url);
                    return false;
                }
            }
            var excluir = 'SU_ACP_AIN,SU_SIACA_INST,SU_HFC_LIN,SU_REC_RSGA';
            var i = parseInt($(sender).data('CodeValidate'));

            var exec = false;
            Session.CO = this.id;
            Session.ORIGINAPP = this.parentProduct;

            switch (i) {
                case 1:

                    exec = true;
                    break;
                case 2:
                    if (Session.ORIGINTYPE != '') { exec = false; }
                    break;
                case 3:
                    exec = true; break;
                case 4:
                    if (excluir.indexOf(this.code) >= 0) { exec = true; }
                    break;
            }
            if (exec == true) {
                if (typeof e.fn_response !== 'undefined' && $.isFunction(e.fn_response)) {
                    e.fn_response.call(e.sender, e.control, e);
                }
                return;

            } else {
                if ($.ValidateMenu(this.code, this.parentProduct)) {
                    var val = false;
                    if (typeof Session.DATASERVICE != 'undefined' && typeof Session.DATASERVICE.IsLTE != 'undefined') {
                        if (Session.DATASERVICE.IsLTE == null) {
                            Session.DATASERVICE.IsLTE = false;
                        }

                    }
                    if (this.code == 'SU_SIACA_XCE') {
                        if (Session.DATASERVICE != undefined && Session.DATASERVICE.IsTFI != undefined && Session.DATASERVICE.IsTFI == true) {
                            Session.DATASERVICE.IsTFI = "SI"; val = true;
                        }
                    }
                    var value = Session.PSTRTELREFER;
                    var phoneReference = Session.DATACUSTOMER.PhoneReference;
                    if (this.code == 'SU_ACP_GAB') {
                        Session.PSTRTELREFER = "";
                        Session.DATACUSTOMER.PhoneReference = Session.DATASERVICE.CellPhone;
                    }
                    $.redirect.GetParamsData(this.code, this.parentProduct);
                    if (this.code == 'SU_ACP_GAB') {
                        Session.PSTRTELREFER = value;
                        Session.DATACUSTOMER.PhoneReference = phoneReference;
                    }
                    if (this.code == 'SU_SIACA_XCE') {
                        if (Session.DATASERVICE != undefined && Session.DATASERVICE.IsTFI != undefined) {
                            Session.DATASERVICE.IsTFI = val;
                        }
                    }

                }
            }
        }
    });

})(jQuery, null);

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

function showAlert(message, title, callback) {
    var wu = $.window.open({
        autoSize: true,
        url: '',
        title: title || 'Alerta',
        text: message,
        modal: false,
        maximizeBox: false,
        minimizeBox: false,
        width: 220,
        height: 210,
        buttons: {
            Aceptar: {
                class: 'btn-primary',
                click: function (sender, args) {
                    this.close();
                    if (callback != null) {
                        callback.call(this);
                    }
                }
            }
        }
    });
    wu._focus();
}
function showConfirm(message, title, callback) {
    $.window.open({
        autoSize: true,
        url: '',
        title: title || 'Confirmar',
        text: message,
        modal: true,
        controlBox: false,
        maximizeBox: false,
        minimizeBox: false,
        buttons: {
            Aceptar: {
                class: 'btn-primary',
                click: function (sender, args) {
                    this.close();
                    if (callback != null) {
                        callback.call(this, true);
                    }
                }
            },
            Cancelar: {
                click: function (sender, args) {
                    this.close();
                    if (callback != null) {
                        callback.call(this, false);
                    }
                }
            }
        }
    });
}
function getValueConfig(keys) {

    var json = JSON.stringify(keys);
    var response = null;
    $.app.ajax({
        async: false,
        type: 'POST',
        url: '~/Home/GetValueConfig',
        data: { strIdSession: Session.IDSESSION, json: json },
        dataType: 'json',
        error: function (data, status) {

        },
        success: function (data) {
            if (data != null) {
                if (data.MsjErrorKey != '') {

                    modalAlert('Error al Obtener las siguientes llaves: ' + '\n' + data.MsjErrorKey.replace(';', ' '));
                }
                response = data.Config;
            }
        },
    });
    return response;
}

function getValueKeyConfig(keys, strURL) {
    var json = JSON.stringify(keys);
    var response = null;
    $.app.ajax({
        async: false,
        type: 'POST',
        url: strURL,
        data: { strIdSession: Session.IDSESSION, json: json },
        dataType: 'json',
        error: function (data, status) {

        },
        success: function (data) {
            if (data != null) {
                if (data.MsjErrorKey != '') {

                    modalAlert('Error al Obtener las siguientes llaves: ' + '\n' + data.MsjErrorKey.replace(';', ' '));
                }
                response = data.Config;
            }
        },
    });
    return response;
};

function modalAlert(message, title, callback) {
    var wu = $.window.alert({
        autoSize: true,
        url: '',
        title: title || 'Alerta',
        text: message,
        modal: true,
        width: 831,
        height: 500,
        buttons: {
            Aceptar: {
                id: 'btnAceptarAlertModal',
                class: 'btn-primary',
                click: function (sender, args) {
                    if (callback != null) {
                        callback.call(this);
                    }
                    this.close();
                }
            }
        }
    });
    wu._focus();
    setTimeout(function () {
        $('#btnAceptarAlertModal').focus();
    }, 1000);

}

function modalConfirm(message, title, callback) {
    var wu = $.window.alert({
        autoSize: true,
        url: '',
        title: title || 'Confirmar',
        text: message,
        modal: true,
        width: 931,
        height: 400,
        buttons: {
            Aceptar: {
                id: 'btnAceptarAlertConfirm',
                class: 'btn-primary',
                click: function (sender, args) {

                    if (callback != null) {
                        callback.call(this, true);
                    }
                    this.close();
                }
            },
            Cancelar: {
                click: function (sender, args) {
                    this.close();
                }
            }
        }
    });
    wu._focus();
    setTimeout(function () {
        $('#btnAceptarAlertConfirm').focus();
    }, 1000);
}

function ValidateUser(option, fn_success, fn_failled, fn_cancel, fn_error) {
    $.window.login({
        autoSize: true,
        controlBox: false,
        url: $.app.getPageUrl({ url: '~/Dashboard/Home/ValidateCustomer' }),
        type: 'POST',
        title: 'Autorización',
        modal: true,
        width: 931,
        height: 400,
        buttons: {
            Aceptar: {
                class: 'btn-primary',
                click: function (sender, args) {
                    var usu = $('#login_username').val();
                    var pass = $('#login_password').val();
                    var $this = this;
                    $.app.ajax({
                        type: "POST",
                        cache: false,
                        dataType: "json",
                        url: '~/Dashboard/PrepaidDataCall/CheckingUser',
                        data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                        error: function (ex) {
                            if (fn_error != null) {
                                fn_error.call(this, true);
                            }
                        },
                        beforeSend: function () {
                            $.blockUI({
                                message: '<div align="center"><img src="../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                baseZ: $.app.getMaxZIndex() + 1,
                                css: {
                                    border: 'none',
                                    padding: '15px',
                                    backgroundColor: '#000',
                                    '-webkit-border-radius': '10px',
                                    '-moz-border-radius': '10px',
                                    opacity: .5,
                                    color: '#fff'
                                }
                            });

                        },
                        complete: function () {
                            $.unblockUI();
                        },
                        success: function (response) {
                            if (response.result && response.result == 1) {
                                if (fn_success != null) {
                                    fn_success.call(this, true);
                                }
                                $this.close();
                            } else if (response.result == 2 || response.result == 0) {
                                modalAlert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                if (fn_failled != null) {
                                    fn_failled.call(this, true);
                                }
                            } else if (response.result == 3) {
                                modalAlert('Ocurrio un error al Validar el Usuario.');
                                if (fn_error != null) {
                                    fn_error.call(this, true);
                                }
                            }
                        }
                    });
                }
            },
            Cancelar: {
                click: function (sender, args) {

                    if (fn_cancel != null) {
                        fn_cancel.call(this, false);
                    }
                    this.close();
                }
            }
        }
    });
}

function customButtonsFooterModal($element, option) {
    var $footer = $($element[0].offsetParent.nextSibling),
        $buttons = $element[option].buttons;

    if ($footer.hasClass('modal-footer'))
        $.each($buttons, function (index, button) {
            if ($.string.isEmptyOrNull(button.text) && $.string.isEmptyOrNull(button.html)) {
                button.text = index;
            }

            var $button = new $.control.button(button);
            $footer.prepend($button.getElement());
        });
}

$(document).ajaxStop(function () {
    $.unblockUI();
});