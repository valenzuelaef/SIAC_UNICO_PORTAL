(function ($, undefined) {

    'use strict';

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidBalanceCBIOS.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblTelephoneJanus: $('#lblTelephoneJanus', $element)
            , tblBagPlanIndividual: $('#tblBagPlanIndividual', $element)
            , tblBagServicesOnTop: $('#tblBagServicesOnTop', $element)
            , tblBagCoorporativa: $('#tblBagCoorporativa', $element)
            , tblConsumo: $('#tblConsumo', $element)
            , containerJanus: $('#containerJanus', $element)

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
            that.getCBIOS();
        },
        getCBIOS: function () {
            var that = this,
                controls = that.getControls();
            controls.lblTelephoneJanus.after(Session.DATACUSTOMER.Telephone);
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                url: '~/Dashboard/PostpaidDataService/GetBalancesCBIOS',
                data: { strIdSession: Session.IDSESSION, strCustomerId: Session.DATACUSTOMER.CustomerID, strTelephone: Session.TELEPHONE },
                success: function (response) {
                    that.setTablesCBIOS(response.data);
                },
                error: function (msger) {
                    controls.containerJanus.find('table tbody').html('');
                    $.app.error({
                        id: 'LoadingJanus2',
                        message: msger,
                        click: function () {
                            that.getCBIOS();
                        }
                    });
                }
            });
        },
        setTablesCBIOS: function (data) {
            var that = this;
            console.log(data);
            that.setTableBagPlanIndividual(data.lstBolsaIndividual);
            that.setTableBagServicesOnTop(data.lstBolsaOnTop);
            that.setTableBagCoorporativa(data.lstBolsaCorporativa);
            that.setTableConsumo(data.lstBolsaCorporativa);
        },
		displayNotExistData: function (tabla,texto,strBgColor) {
		    
				var tblDefault=$('#'+tabla+' tbody tr td');
				var tblDefaultTR=$('#'+tabla+' tbody tr ');
            if(tblDefault.hasClass( "dataTables_empty" )){
	   tblDefault.html("");
	   tblDefault.remove();
	   tblDefaultTR.append('<td class="text-center" style="background-color:'+strBgColor+' ;color: #fff;">'+texto+'</td><td colspan="4" class="text-center">No existen datos</td>');
	  
  }
        },
        setTableBagPlanIndividual: function (lstBolsaIndividual) {
            var that=this;
			var controls = that.getControls();
			
            controls.tblBagPlanIndividual.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: lstBolsaIndividual
                , select: 'single'
				,ordering:false
                , columns: [
                    { "data": null },
                    { "data": "Bolsa" },
                    { "data": "Saldo" },
                    { "data": "Consumo" },
                    { "data": "UnidadesAsignadas" }
                ]
                , columnDefs: [
                    {"width": "20%",
                        className: "text-center",
                       targets: [0, 1, 2, 3, 4],
					 
                    }
                ]
                 , rowCallback: function (row, data, iDisplayIndex) {

					 var tamanio=lstBolsaIndividual.length;
					  if(iDisplayIndex!=0){
						  $('td:eq(0)', row).html("");
						  $('td:eq(0)', row).remove();
                     
					  }else{
						   $('td:eq(0)', row).html("Bolsa Plan Básica").attr("rowspan", tamanio);
						   $('td:eq(0)', row).css({ "background-color": "#ec776f", "color": "#fff" ,"text-align":"center","vertical-align":"middle"});
					  }

                 }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },

            });
			
		
			that.displayNotExistData("tblBagPlanIndividual","Bolsa Plan Básica","#ec776f");
   

        },
        setTableBagServicesOnTop: function (lstBolsaOnTop) {
			 var that=this;
            var controls = that.getControls();

            controls.tblBagServicesOnTop.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: lstBolsaOnTop
                , select: 'single'
				,ordering:false
                , columns: [
					
                    { "data": null },
                    { "data": "Bolsa" },
                    { "data": "Saldo" },
                    { "data": "Consumo" },
                    { "data": "UnidadesAsignadas" }
                ]
                , columnDefs: [
                    {"width": "20%",
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4],
						
						
                    }
                ]
                  , rowCallback: function (row, data, iDisplayIndex) {

				  var tamanio=lstBolsaOnTop.length;
					  if(iDisplayIndex!=0){
						  $('td:eq(0)', row).html("");
						  $('td:eq(0)', row).remove();
						
					  }else{
						   $('td:eq(0)', row).html("Bolsas Servicios On top").attr("rowspan", tamanio);
						   $('td:eq(0)', row).css({ "background-color": "rgb(107, 143, 241)", "color": "#fff" ,"text-align":"center","vertical-align":"middle"});
					  }
                     
                  }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
			that.displayNotExistData("tblBagServicesOnTop","Bolsas Servicios On top","rgb(107, 143, 241)");
        },
        setTableBagCoorporativa: function (lstBolsaCorporativa) {
             var that=this;
			var controls = that.getControls();
            controls.tblBagCoorporativa.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: lstBolsaCorporativa
                , select: 'single'
				,ordering:false
                , columns: [
				
                    { "data": null },
                    { "data": "Bolsa" },
                    { "data": "Saldo" },
                    { "data": "Consumo" },
                    { "data": "UnidadesAsignadas" }
                ]
                , columnDefs: [
                    {
						"width": "20%",
                        className: "text-center",
                       targets: [0, 1, 2, 3, 4],
						 
                    }
                ]
                  , rowCallback: function (row, data, iDisplayIndex) {
					  var tamanio=lstBolsaCorporativa.length;
					  if(iDisplayIndex!=0){
						  $('td:eq(0)', row).html("");
						  $('td:eq(0)', row).remove();
						
					  }else{
						   $('td:eq(0)', row).html("Bolsa Corporativa").attr("rowspan", tamanio);
						   $('td:eq(0)', row).css({ "background-color": "rgb(106, 140, 84)", "color": "#fff" ,"text-align":"center","vertical-align":"middle"});
					  }

                      
                  }
                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
			that.displayNotExistData("tblBagCoorporativa","Bolsa Corporativa","rgb(106, 140, 84)");
        },
        setTableConsumo: function (lstBolsaCorporativa) {
            var controls = this.getControls();

            controls.tblConsumo.DataTable({
                info: false
                , paging: false
                , destroy: true
                , searching: false
                , scrollX: true
                , sScrollXInner: "100%"
                , autoWidth: true
                , data: lstBolsaCorporativa
                , select: 'single'
                , columns: [
				
                    { "data": "CodigoConsumo" },
                    { "data": "Bolsa" },
                    { "data": "lineConsumption" },
                    { "data": "lineLimit" },
                    { "data": "LimiteDisponible" }
                ]
                , columnDefs: [
                    {
                        className: "text-center",
                        targets: [0, 1, 2, 3, 4]
                    }
                ]

                , language: {
                    lengthMenu: "Display _MENU_ records per page",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.href
    }

    $.fn.PostpaidBalanceCBIOS = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidBalanceCBIOS'),
                options = $.extend({}, $.fn.PostpaidBalanceCBIOS.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidBalanceCBIOS', data);
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

    $.fn.PostpaidBalanceCBIOS.defaults = {
    }

    $('#containerConsumptionBalance', $('.modal:last')).PostpaidBalanceCBIOS();

})(jQuery);