(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormPostpaidPhoneList.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tbServicePost: $('#tbServicePost', $element)
            , tbServiceHFC: $('#tbServiceHFC', $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();

            controls.tbServicePost.find('tr').find('td:eq(6)').each(function () {
                var $this = $(this);
                $(this).find('span').on("click", function () { that.SearchDirectionPost($this) });
            });
            controls.tbServiceHFC.find('tr').find('td:eq(4)').each(function () {
                var $this = $(this);
                $(this).find('span').on("click", function () { that.SearchDirectionHFC($this) });
            });
            controls.tbServiceHFC.find('tr').find('td:eq(6)').each(function () {
                var $this = $(this);
                $(this).find('span').on("click", function () { that.SearchTelephone($this) });
            });

            that.getDataTable();

        },
        SearchDirectionHFC: function (t) {
            var td = t.parent().find('td');
            var strContractID = td.eq(1).text();
            var strApplication = td.eq(5).text();

            Session.ORIGINTYPESERVICE = strApplication;
            Session.CONTRACTIDSERVICE = strContractID;

            var objCustomer = Session.DATACUSTOMER;
            objCustomer.BannerMessage = '';
            $.window.open({
                modal: false,
                title: "Dirección de Instalación",
                url: '~/Dashboard/PostpaidDataService/InstallationDirection',
                data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication, strUserId: Session.USERACCESS.userId },
                width: 1024,
                height: 600,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        SearchDirectionPost: function (t) {
            var td = t.parent().find('td');
            var strContractID = td.eq(1).text();
            var strApplication = td.eq(5).text();
            if (strContractID.substr(0, 2) !== '21') {
                showAlert('Los Servicios de tipo Móvil no tienen Dirección de Instalación', 'Alerta');
                return;
            }
            $.window.open({
                modal: false,
                title: "Dirección de Instalación",
                url: '~/Dashboard/PostpaidDataService/InstallationDirection',
                data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication },
                width: 1024,
                height: 600,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
            
        },
        SearchTelephone: function (t) {
            var td = t.parent().find('td');
            var strContractID = td.eq(1).text();
            var strApplication = td.eq(5).text();

            Session.ORIGINTYPESERVICE = strApplication;
            Session.CONTRACTIDSERVICE = strContractID;

            //INICIATIVA 616
            if (Session.DATACUSTOMER.objPostDataAccount.plataformaAT == "TOBE")
            {
                $.window.open({
                    modal: false,
                    title: "Listado de Teléfonos",
                    url: '~/Dashboard/PostpaidDataService/PhoneListAltTobe',
                    data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication, plataforma: Session.DATACUSTOMER.objPostDataAccount.plataformaAT },
                    width: 1000,
                    height: 500,
                    buttons: {
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            } else
            {
                $.window.open({
                    modal: false,
                    title: "Listado de Teléfonos",
                    url: '~/Dashboard/PostpaidDataService/PhoneListAlt',
                    data: { strIdSession: Session.IDSESSION, strContractID: strContractID, strApplication: strApplication },
                    width: 1000,
                    height: 500,
                    buttons: {
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            }


            
        },
        getDataTable: function () {

            var controls = this.getControls();

            controls.tbServicePost.DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "order": [[1, 'asc']],
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "emptyTable": "No existen datos"
                }
                , "columnDefs": [{
                    orderable: false,
                    className: 'select-radio',
                    targets: 0
                }],
                select: {
                    style: 'os',
                    info: false
                }
            })
            .on('select', function () {
                controls.tbServiceHFC.DataTable().rows().deselect();
            });

            controls.tbServiceHFC.DataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "emptyTable": "No existen datos"
                }
                , "columnDefs": [{
                    orderable: false,
                    className: 'select-radio',
                    targets: 0
                }],
                select: {
                    style: 'os',
                    info: false
                }
            })
            .on('select', function () {
                controls.tbServicePost.DataTable().rows().deselect();
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },

    };


    $.fn.FormPostpaidPhoneList = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormPostpaidPhoneList'),
                options = $.extend({}, $.fn.FormPostpaidPhoneList.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormPostpaidPhoneList', data);
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



    $.fn.FormPostpaidPhoneList.defaults = {
    }

    $('#PostpaidPhoneList', $('.modal:last')).FormPostpaidPhoneList();

})(jQuery);